using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace MCDawn
{
    public class Remote // remote/client side shit
    {
        public static UTF8Encoding enc = new UTF8Encoding();
        public static List<Remote> remotes = new List<Remote>();
        public string name = ""; // name, or host state
        public string username = ""; // login username
        public string password = ""; // login password
        public string ip = ""; // ip address
        public bool loggedIn = false;
        public bool disconnected = false;
        public Socket connectionSocket { get; private set; }
        byte[] tempbuffer = new byte[0xFF];
        byte[] buffer = new byte[0];

        public Remote(Socket s)
        {
            try
            {
                loggedIn = false;
                connectionSocket = s;
                ip = connectionSocket.RemoteEndPoint.ToString().Split(':')[0];
                Server.s.Log("Remote Console at " + ip + " connecting...");
                connectionSocket.BeginReceive(tempbuffer, 0, tempbuffer.Length, SocketFlags.None, new AsyncCallback(Receive), this);
            }
            catch (Exception e) { Disconnect("Login failed!"); Server.ErrorLog(e); }
        }

        static void Receive(IAsyncResult result)
        {
            try
            {
                //Server.s.Log(result.AsyncState.ToString());
                Remote p = (Remote)result.AsyncState; // crashes when this happens, only after a disconnect :/
                if (p.disconnected) return;
                try
                {
                    int length = p.connectionSocket.EndReceive(result);
                    if (length == 0) { p.Disconnect(); return; }

                    byte[] b = new byte[p.buffer.Length + length];
                    Buffer.BlockCopy(p.buffer, 0, b, 0, p.buffer.Length);
                    Buffer.BlockCopy(p.tempbuffer, 0, b, p.buffer.Length, length);

                    p.buffer = p.HandleMessage(b);
                    p.connectionSocket.BeginReceive(p.tempbuffer, 0, p.tempbuffer.Length, SocketFlags.None, new AsyncCallback(Receive), p);
                }
                catch (ObjectDisposedException) { p.Disconnect(); return; }
                catch (SocketException) { p.Disconnect("Error!"); return; }
                catch (Exception e)
                {
                    Server.ErrorLog(e);
                    if (p != null) p.Disconnect("Error!");
                    Server.s.Log("Attempting to restart socket for Remote Console...");
                    RemoteServer.listen = null;
                    if (RemoteServer.Setup()) { Server.s.Log("Listening socket on " + RemoteServer.port + " for Remote Console restarted."); }
                    else { Server.s.Log("Failed to restart listening socket for Remote Console."); }
                }
            }
            catch { }
        }

        byte[] HandleMessage(byte[] buffer)
        {
            try
            {
                int length = 0; byte msg = buffer[0];
                // Get the length of the message by checking the first byte
                switch (msg)
                {
                    case 0:
                        length = 257;
                        break; // login authentication
                    case 2:
                        if (!loggedIn)
                            goto default;
                        length = 64;
                        break; // chat
                    default:
                        Disconnect("Unhandled remote console message id \"" + msg + "\"!");
                        return new byte[0];
                }
                if (buffer.Length > length)
                {
                    byte[] message = new byte[length];
                    Buffer.BlockCopy(buffer, 1, message, 0, length);

                    byte[] tempbuffer = new byte[buffer.Length - length - 1];
                    Buffer.BlockCopy(buffer, length + 1, tempbuffer, 0, buffer.Length - length - 1);

                    buffer = tempbuffer;

                    // Thread thread = null; 
                    switch (msg)
                    {
                        case 0:
                            HandleLogin(message);
                            break;
                        case 2:
                            if (!loggedIn)
                                break;
                            HandleChat(message);
                            break;
                    }
                    //thread.Start((object)message);
                    if (buffer.Length > 0)
                        buffer = HandleMessage(buffer);
                    else
                        return new byte[0];
                }
            }
            catch (Exception e) { Server.ErrorLog(e); }
            return buffer;
        }

        string Hash(string password) 
        { 
            byte[] bytes = new SHA256Managed().ComputeHash(new UTF8Encoding().GetBytes(password));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++) { sb.Append(bytes[i].ToString("x2")); }
            return sb.ToString();
        }

        public void HandleLogin(byte[] message)
        {
            try
            {
                if (loggedIn) return;

                byte version = message[0];
                username = enc.GetString(message, 1, 64).Trim();
                password = enc.GetString(message, 65, 64).Trim();
                string key = enc.GetString(message, 129, 64).Trim();
                name = enc.GetString(message, 193, 64).Trim();

                if (String.IsNullOrEmpty(name)) { name = "Alive"; }

                loggedIn = true;

                if (version != RemoteServer.rcProtocolVersion) { Disconnect("Wrong Protocol Version!"); return; }
                if (key != Hash(RemoteServer.rcpass)) { Disconnect("Wrong server password!"); return; }
                int found = 0;
                foreach (RemoteServer.User ru in RemoteServer.rcusers)
                    if (ru.name.ToLower() == username.ToLower()) { found++; if (Hash(ru.password) != password.ToLower()) { Disconnect("Wrong user password!"); return; } }
                if (found <= 0 || found > 1) { Disconnect("Remote user not found."); return; }
                foreach (Remote r in remotes)
                    if (r.name.ToLower().Trim() == name.ToLower().Trim()) { Disconnect("Already logged in!"); return; }

                remotes.Add(this);
                RemoteServer.RemoteListUpdate();

                if (!Server.useMaxMind)
                {
                    Server.s.Log("Remote Console user " + name + " connected.");
                    Player.GlobalMessageAdmins("To Admins: Remote Console user &a" + name + "&g connected.");
                }
                else
                {
                    Server.s.Log("Remote Console user " + name + " connected from " + Server.iploopup.getCountry(IPAddress.Parse(ip)).getName() + ".");
                    Player.GlobalMessageAdmins("To Admins: Remote Console user &a" + name + "&g connected from " + Server.iploopup.getCountry(IPAddress.Parse(ip)).getName() + ".");
                }

                Server.s.OnLog += SendMessage;
                Server.s.OnCommand += SendMessage;
                Server.s.OnError += SendMessage;

                SendMessage("Remote Console connected!");
            }
            catch (Exception ex) { Server.ErrorLog(ex); }
        }

        void HandleChat(byte[] message)
        {
            try
            {
                if (!loggedIn) return;

                string text = enc.GetString(message, 0, 64).Trim();
                text = Regex.Replace(text, @"\s\s+", " ");

                if (text.Length == 0)
                    return;

                if (text.StartsWith("//") || text.StartsWith("./")) { text = text.Remove(0, 1); goto chat; }
                if (text == "/") { HandleCommand("repeat", ""); return; }
                if (text[0] == '/' || text[0] == '!')
                {
                    text = text.Remove(0, 1);
                    int pos = text.IndexOf(' ');
                    if (pos == -1) { HandleCommand(text.ToLower(), ""); return; }
                    string cmd = text.Substring(0, pos).ToLower();
                    string msg = text.Substring(pos + 1);
                    HandleCommand(cmd, msg);
                    return;
                }
                if (text[0] == '@')
                {
                    text = text.Remove(0, 1).Trim();
                    string[] words = text.Split(new char[] { ' ' }, 2);
                    Player who = Player.Find(words[0]);
                    if (who == null) { Server.s.Log("Player could not be found!"); return; }
                    who.SendMessage("&bFrom Remote Console: &f" + words[1]);
                    Server.s.Log("(whispers to " + who.name + ") <CONSOLE> " + words[1]);
                    if (!Server.devs.Contains(who.name.ToLower()))
                    {
                        Player.GlobalMessageDevs("To Devs &f-" + "&gRemote Console &b[>] " + who.color + who.name + "&f- " + words[1]);
                    }
                }
                if (text[0] == '#')
                {
                    string newtext = text;
                    if (text[0] == '#') newtext = text.Remove(0, 1).Trim();

                    Player.GlobalMessageOps("To Ops &f-Remote Console [&a" + name + "&g]&f- " + newtext);
                    Server.s.Log("(OPs): Remote Console [" + name + "] " + newtext);
                    IRCBot.Say(name + ": " + newtext, true);
                    try { if (!Server.cli) { MCDawn.Gui.Window.thisWindow.WriteOpLine("<REMOTE CONSOLE [" + name + "]> " + newtext); } }
                    catch { }
                    return;
                }
                if (text[0] == ';')
                {
                    string newtext = text;
                    if (text[0] == ';') newtext = text.Remove(0, 1).Trim();
                    Player.GlobalMessageAdmins("To Admins &f-Remote Console [&a" + name + "&g]&f- " + newtext);
                    Server.s.Log("(Admins): Remote Console [" + name + "]: " + newtext);
                    try { if (!Server.cli) { MCDawn.Gui.Window.thisWindow.WriteAdminLine("<REMOTE CONSOLE [" + name + "]> " + newtext); } }
                    catch { }
                    //IRCBot.Say(name + ": " + newtext, true);
                    //AllServerChat.Say("(Admins) " + prefix + name + ": " + text);
                    return;
                }

            chat:
                Player.GlobalChat(null, "Remote Console [&a" + name + "&g]:&f " + text, false);
                IRCBot.Say("Remote Console [" + name + "]: " + text);
                Server.s.Log("<REMOTE CONSOLE [" + name + "]> " + text);
            }
            catch (Exception e) { Server.ErrorLog(e); Player.GlobalMessage("An error occurred: " + e.Message); }
        }

        void HandleCommand(string cmd, string message)
        {
            try
            {
                if (cmd == "") { SendMessage("No command entered."); return; }
                if (Command.all.Find(cmd).name == "remote" || Command.all.Find(cmd).name == "remoteuser") { SendMessage("Cannot use these commands."); return; }
                if (cmd == null) { SendMessage("Unknown command \"" + cmd + "\"!"); return; }
                Server.s.CommandUsed("REMOTE CONSOLE [" + name + "] used /" + cmd + " " + message);
                Command.all.Find(cmd).Use(null, message);
            }
            catch (Exception e) { Server.ErrorLog(e); SendMessage("Command failed."); }
        }

        public void Disconnect(string reason = "")
        {
            try
            {
                disconnected = true;
                SendKick(reason);
                if (loggedIn)
                {
                    Server.s.Log("Remote Console user " + name + " disconnected.");
                    Player.GlobalMessageAdmins("To Admins: Remote Console user &a" + name + "&g disconnected.");
                    remotes.Remove(this);
                }
                else { Server.s.Log("Remote Console at " + ip + " disconnected."); }
                try
                {
                    connectionSocket.Shutdown(SocketShutdown.Both);
                    connectionSocket.Close();
                }
                catch (Exception e) { Server.ErrorLog(e); }
                RemoteServer.RemoteListUpdate();
            }
            catch (Exception ex) { Server.ErrorLog(ex); }
        }

        public void SendMessage(string message)
        {
            try
            {
                byte[] buffer = new byte[64];

                for (byte totalTries = 0; ; totalTries++)
                {
                    try
                    {
                        try
                        {
                            this.SendPing();
                        }
                        catch (Exception)
                        {
                            return;
                        }

                        foreach (string line in Wordwrap(message))
                        {
                            string newLine = line;
                            if (newLine.TrimEnd(' ')[newLine.TrimEnd(' ').Length - 1] < '!')
                                newLine += '\'';

                            StringFormat(newLine, 64).CopyTo(buffer, 0);
                            SendRaw(1, buffer);
                        }
                        break;
                    }
                    catch (Exception e)
                    {
                        if (totalTries >= 10)
                            Server.ErrorLog(e);
                    }
                }
            }
            catch { }
        }

        public static byte[] StringFormat(string str, int size)
        {
            byte[] bytes = new byte[size];
            bytes = enc.GetBytes(str.PadRight(size).Substring(0, size));
            return bytes;
        }

        static List<string> Wordwrap(string message)
        {
            List<string> lines = new List<string>();
            message = Regex.Replace(message, @"(&[0-9a-f])+(&[0-9a-f])", "$2");
            message = Regex.Replace(message, @"(&[0-9a-f])+$", "");

            int limit = 64;

            while (message.Length > 0)
            {
                //if (Regex.IsMatch(message, "&a")) break;

                if (lines.Count > 0)
                {
                    if (message[0].ToString() == "&")
                        message = "> " + message.Trim();
                    else
                        message = "> " + message.Trim();
                }

                if (message.Length <= limit) { lines.Add(message); break; }
                for (int i = limit - 1; i > limit - 20; --i)
                    if (message[i] == ' ')
                    {
                        lines.Add(message.Substring(0, i));
                        goto Next;
                    }

            retry:
                if (message.Length == 0 || limit == 0) { return lines; }

                try
                {
                    if (message.Substring(limit - 2, 1) == "&" || message.Substring(limit - 1, 1) == "&")
                    {
                        message = message.Remove(limit - 2, 1);
                        limit -= 2;
                        goto retry;
                    }
                    else if (message[limit - 1] < 32 || message[limit - 1] > 127)
                    {
                        message = message.Remove(limit - 1, 1);
                        limit -= 1;
                        //goto retry;
                    }
                }
                catch { return lines; }
                lines.Add(message.Substring(0, limit));

            Next: message = message.Substring(lines[lines.Count - 1].Length);
                if (lines.Count == 1) limit = 60;

                int index = lines[lines.Count - 1].LastIndexOf('&');
                if (index != -1)
                {
                    if (index < lines[lines.Count - 1].Length - 1)
                    {
                        char next = lines[lines.Count - 1][index + 1];
                        if (index == lines[lines.Count - 1].Length - 1)
                        {
                            lines[lines.Count - 1] = lines[lines.Count - 1].Substring(0, lines[lines.Count - 1].Length - 2);
                        }
                    }
                    else if (message.Length != 0)
                    {
                        char next = message[0];
                        lines[lines.Count - 1] = lines[lines.Count - 1].Substring(0, lines[lines.Count - 1].Length - 1);
                        message = message.Substring(1);
                    }
                }
            } return lines;
        }

        void SendPing() { /*pingDelay = 0; pingDelayTimer.Start(); */ SendRaw(4); }
        void SendKick(string message)
        {
            byte[] buffer = new byte[64];
            StringFormat(message.Trim(), 64).CopyTo(buffer, 0);
            SendRaw(3, buffer);
        }
        public void SendRaw(int id) { SendRaw(id, new byte[0]); }
        public void SendRaw(int id, byte[] send)
        {
            try
            {
                byte[] buffer = new byte[send.Length + 1];
                buffer[0] = (byte)id;

                Buffer.BlockCopy(send, 0, buffer, 1, send.Length);
                string TxStr = "";
                for (int i = 0; i < buffer.Length; i++)
                {
                    TxStr += buffer[i] + " ";
                }
                int tries = 0;
            retry: try
                {

                    connectionSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, delegate(IAsyncResult result) { }, null);
                    /*      if (buffer[0] != 1)
                          {
                              Server.s.Log("Buffer ID: " + buffer[0]);
                              Server.s.Log("BUFFER LENGTH: " + buffer.Length);
                              Server.s.Log(TxStr);
                          }*/

                }
                catch (SocketException)
                {
                    tries++;
                    if (tries > 2)
                        Disconnect();
                    else goto retry;
                }
                catch (Exception ex) { Server.ErrorLog(ex); }
            }
            catch { }
        }

        public static Remote Find(string name)
        {
            //if (name[0] == '!') { name.Remove(0, 1); exp = true; }
            List<Remote> tempList = new List<Remote>();
            tempList.AddRange(remotes);
            Remote tempPlayer = null; bool returnNull = false;

            foreach (Remote p in tempList)
            {
                if (p.username.ToLower() == name.ToLower()) return p;
                if (p.username.ToLower().IndexOf(name.ToLower()) != -1)
                {
                    if (tempPlayer == null) tempPlayer = p;
                    else returnNull = true;
                }
            }

            if (returnNull == true) return null;
            if (tempPlayer != null) return tempPlayer;
            return null;
        }
    }
}