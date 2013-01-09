using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using Meebey.SmartIrc4net;

namespace MCDawn
{
    class GlobalChatBot
    {
        static IrcClient globalchat = new IrcClient();
        static string server = "irc.synirc.net";
        static string channel = "#MCDawn";
        static string devchannel = "#MCDawn.Devs";
        static string nick = Server.globalNick;
        static Thread globalThread;

        static string[] names;

        public GlobalChatBot()
        {
            // The IRC Bot must run in a seperate thread, or else the server will freeze.
            globalThread = new Thread(new ThreadStart(delegate
            {
                // Attach event handlers
                globalchat.OnConnecting += new EventHandler(OnConnecting);
                globalchat.OnConnected += new EventHandler(OnConnected);
                globalchat.OnChannelMessage += new IrcEventHandler(OnChanMessage);
                globalchat.OnJoin += new JoinEventHandler(OnJoin);
                globalchat.OnPart += new PartEventHandler(OnPart);
                globalchat.OnQuit += new QuitEventHandler(OnQuit);
                globalchat.OnNickChange += new NickChangeEventHandler(OnNickChange);
                globalchat.OnDisconnected += new EventHandler(OnDisconnected);
                globalchat.OnNames += new NamesEventHandler(OnNames);
                globalchat.OnChannelAction += new ActionEventHandler(OnAction);

                // Attempt to connect to the IRC server
                try { globalchat.Connect(server, 6667); }
                catch (Exception ex) { if (Server.useglobal) { Server.s.Log("Unable to connect to MCDawn Global Chat Server: " + ex.Message); } }
            }));
            globalThread.Start();
        }

        // While connecting
        void OnConnecting(object sender, EventArgs e)
        {
        }
        // When connected
        void OnConnected(object sender, EventArgs e)
        {
            try
            {
                globalchat.Login(nick, nick, 0, nick);
                if (Server.globalIdentify && Server.globalPassword != string.Empty)
                {
                    if (Server.useglobal) { Server.s.Log("Identifying with Nickserv"); }
                    globalchat.SendMessage(SendType.Message, "nickserv", "IDENTIFY " + Server.globalPassword);
                }
                if (Server.useglobal)
                {
                    Server.s.Log("Server joined MCDawn Global Chat channel!");
                    globalchat.RfcJoin(channel);
                }
                globalchat.RfcJoin(devchannel);
                globalchat.Listen();
            }
            catch (Exception ex) { Server.ErrorLog(ex); }
        }

        void OnNames(object sender, NamesEventArgs e)
        {
            names = e.UserList;
        }
        void OnDisconnected(object sender, EventArgs e)
        {
            if (Server.useglobal) Server.s.Log("Server disconnected from MCDawn Global Chat channel!");
            try { globalchat.Connect(server, 6667); }
            catch { if (Server.useglobal) Server.s.Log("Failed to reconnect to MCDawn Global Chat"); }
        }

        public static void DisplayMessage(string temp, IrcEventArgs e)
        {
            try
            {
                string storedNick = e.Data.Nick;
                foreach (Player pl in Player.players)
                {
                    if (!Server.ignoreGlobal.Contains(pl.name.ToLower()) && !pl.ignoreList.Contains(temp.Split(':')[0]) && !Server.GlobalBanned().Contains(e.Data.Nick.ToLower()) && !Server.GlobalBanned().Contains(temp.Split(':')[0]) && !pl.ignoreList.Contains(temp.Split(':')[0]) && !Server.OmniBanned().Contains(e.Data.Nick.ToLower()) && !Server.OmniBanned().Contains(temp.Split(':')[0]))
                    {
                        // Color code crash exploit patched, the anti-crash defenses no longer needed.
                        if (Server.devs.Contains(temp.Split(':')[0]) && !temp.StartsWith("[Developer] ")) { temp = "[Developer] " + temp; }
                        if (Server.staff.Contains(temp.Split(':')[0]) && !temp.StartsWith("[MCDawn Staff] ")) { temp = "[MCDawn Staff] " + temp; }
                        if (Server.administration.Contains(temp.Split(':')[0]) && !temp.StartsWith("[Administrator] ")) { temp = "[Administrator] " + temp; }

                        if (storedNick.ToLower() == "staff" || storedNick.ToLower() == "devs" || storedNick.ToLower() == "updates") { pl.SendMessage(">[Global] &6" + storedNick + ": &f" + temp); }
                        else { pl.SendMessage(">[Global] " + Server.GlobalChatColour + storedNick + ": &f" + temp); }
                    }
                }
                Server.s.Log(">[Global] " + Server.GlobalChatColour + storedNick + ": &0" + temp);
                try { if (!Server.cli) { MCDawn.Gui.Window.thisWindow.WriteGlobalLine(">[Global] " + storedNick + ": " + temp); } }
                catch { }
            }
            catch { }
        }

        public static void DisplayAction(string temp, IrcEventArgs e) // too lazy to rewrite displaymessage... .-.
        {
            try
            {
                string storedNick = e.Data.Nick;
                foreach (Player pl in Player.players)
                {
                    if (!Server.ignoreGlobal.Contains(pl.name.ToLower()) && !pl.ignoreList.Contains(temp.Split(':')[0]) && !Server.GlobalBanned().Contains(e.Data.Nick.ToLower()) && !Server.GlobalBanned().Contains(temp.Split(':')[0]) && !pl.ignoreList.Contains(temp.Split(':')[0]) && !Server.OmniBanned().Contains(e.Data.Nick.ToLower()) && !Server.OmniBanned().Contains(temp.Split(':')[0]))
                    {
                        if (storedNick.ToLower() == "staff" || storedNick.ToLower() == "devs" || storedNick.ToLower() == "updates") { pl.SendMessage(">[Global] &6*" + storedNick + " " + temp); }
                        else { pl.SendMessage(">[Global] " + Server.GlobalChatColour + "*" + storedNick + " " + temp); }
                    }
                }
                Server.s.Log(">[Global] " + Server.GlobalChatColour + "*" + storedNick + " " + temp);
                try { if (!Server.cli) { MCDawn.Gui.Window.thisWindow.WriteGlobalLine(">[Global] *" + storedNick + " " + temp); } }
                catch { }
            }
            catch { }
        }

        // On public channel message
        void OnChanMessage(object sender, IrcEventArgs e)
        {
            try
            {
                // TODO: make this irctominecraftcolor shit work
                string temp = IRCColor.IRCToMinecraftColor(e.Data.Message); string storedNick = e.Data.Nick;

                if (e.Data.Channel == devchannel)
                {
                    // Commands in DevGlobal.
                    //string[] splitted = temp.Split(new char[] { ' ' }, 2);
                    if (temp.ToLower().StartsWith("^serverinfo ") || temp.ToLower().StartsWith("^sinfo ") || temp.ToLower().StartsWith("^getinfo "))
                    {
                        if (Server.globalNick.ToLower() == temp.Split(' ')[1].ToLower())
                        {
                            Say("^NAME: " + Server.name, true);
                            Say("^IP: " + Server.GetIPAddress(), true);
                            Say("^PORT: " + Server.port, true);
                            Say("^MOTD: " + Server.motd, true);
                            Say("^VERSION: " + Server.Version, true);
                            Say("^GLOBAL NAME: " + Server.globalNick, true);
                            Say("^URL: " + Server.URL, true);
                            Say("^PLAYERS: " + Player.players.Count + "/" + Server.players, true);
                            if (Server.irc)
                            {
                                Say("^IRC: " + Server.ircServer + " > " + Server.ircChannel, true);
                                Say("^IRC OP: " + Server.ircServer + " > " + Server.ircOpChannel, true);
                            }
                        }
                    }
                    if (temp.ToLower().StartsWith("^whois ") || temp.ToLower().StartsWith("^ipget "))
                    {
                        foreach (Player p in Player.players)
                        {
                            if (p.name.ToLower() == temp.Split(' ')[1].ToLower())
                            {
                                Say("^NAME: " + p.name, true);
                                Say("^LEVEL: " + p.level.name, true);
                                Say("^RANK: " + p.group.name, true);
                                if (Server.useMaxMind)
                                {
                                    string countryname = Server.iploopup.getCountry(IPAddress.Parse(p.ip)).getName();
                                    Say("^COUNTRY: " + countryname, true);
                                }
                                Say("^IP: " + p.ip, true);
                                if (Server.useWhitelist) { if (Server.whiteList.Contains(p.name)) { Say("^Player is Whitelisted", true); } }
                                if (Server.devs.Contains(p.name.ToLower())) { Say("^Player is a Developer", true); }
                                if (Server.staff.Contains(p.name.ToLower())) { Say("^Player is a MCDawn Staff", true); }
                                if (Server.administration.Contains(p.name.ToLower())) { Say("^Player is a MCDawn Administrator", true); }
                            }
                        }
                    }
                    if (temp.ToLower().StartsWith("^update "))
                    {
                        try
                        {
                            if (Server.globalNick.ToLower() == temp.Split(' ')[1].ToLower()) 
                                MCDawn_.Gui.Program.PerformUpdate(false);
                        }
                        catch { }
                    }
                    if (temp.ToLower().StartsWith("^ircreset ") || temp.ToLower().StartsWith("^resetbot ")) { GlobalChatBot.Reset(); }
                    if (temp.ToLower().Contains("^updatebans")) { Server.OmniBanned(); Server.GlobalBanned(); return; }
                    if (temp[0] == '^')
                    {
                        //format is: ^(servernick) (command) (message)
                        string message = temp.Remove(0, 1);
                        if (message.Trim().Split(' ')[0].ToLower().Trim() == Server.globalNick.ToLower().Trim())
                            if (message.Trim().Split(' ').Length > 2)
                                Command.all.Find(message.Split(' ')[1]).Use(null, message.Split(new char[] { ' ' }, 3)[2]);
                            else
                                Command.all.Find(message.Split(' ')[1]).Use(null, "");
                    }
                    if (temp.StartsWith("^")) { return; }
                    if (temp.Contains("$color") || temp.Contains("&") || temp.Contains("&")) { return; }
                    Player.GlobalMessageDevsStaff(">[DevGlobal] " + Server.GlobalChatColour + storedNick + ": &f" + temp);
                }
                else DisplayMessage(temp, e);

                //if (temp.IndexOf(':') < temp.IndexOf(' ')) {
                //    storedNick = temp.Substring(0, temp.IndexOf(':'));
                //    temp = temp.Substring(temp.IndexOf(' ') + 1);
                //}

                //s.Log("IRC: " + e.Data.Nick + ": " + e.Data.Message);
                //Player.GlobalMessage("IRC: &1" + e.Data.Nick + ": &f" + e.Data.Message);
            }
            catch { }
        }

        // When someone joins the IRC
        void OnJoin(object sender, JoinEventArgs e)
        {
            try
            {
                Player.GlobalMessageDevs("To Devs: " + Server.GlobalChatColour + e.Data.Nick + "&g has joined the " + (e.Data.Channel == devchannel ? "Dev " : "") + "Global Chat Channel.");
                globalchat.RfcNames(channel);
                globalchat.RfcNames(devchannel);
            }
            catch (Exception ex) { Server.ErrorLog(ex); }
        }
        // When someone leaves the IRC
        void OnPart(object sender, PartEventArgs e)
        {
            try
            {
                Player.GlobalMessageDevs("To Devs: " + Server.GlobalChatColour + e.Data.Nick + "&g has left the " + (e.Data.Channel == devchannel ? "Dev " : "") + "Global Chat Channel" + (!String.IsNullOrEmpty(e.PartMessage) ? " (" + e.PartMessage + ")" : ""));
                globalchat.RfcNames(channel);
                globalchat.RfcNames(devchannel);
            }
            catch (Exception ex) { Server.ErrorLog(ex); }
        }
        void OnQuit(object sender, QuitEventArgs e)
        {
            try
            {
                Player.GlobalMessageDevs("To Devs: " + Server.GlobalChatColour + e.Data.Nick + "&g has quit the Global Chat IRC" + (!String.IsNullOrEmpty(e.QuitMessage) ? " (" + e.QuitMessage + ")" : ""));
                globalchat.RfcNames(channel);
                globalchat.RfcNames(devchannel);
            }
            catch (Exception ex) { Server.ErrorLog(ex); }
        }

        public void OnNickChange(object sender, NickChangeEventArgs e)
        {
            try
            {
                Player.GlobalMessageDevs("To Devs: " + Server.GlobalChatColour + e.OldNickname + "&g is now known as " + e.NewNickname);
                globalchat.RfcNames(channel);
                globalchat.RfcNames(devchannel);
            }
            catch (Exception ex) { Server.ErrorLog(ex); }
        }
        void OnAction(object sender, ActionEventArgs e)
        {
            try
            {
                if (e.Data.Channel == devchannel) Player.GlobalMessageDevsStaff("<[DevGlobal] *" + Server.GlobalChatColour + e.Data.Nick + " &g" + e.ActionMessage);
                else DisplayAction(e.ActionMessage, e);
            }
            catch (Exception ex) { Server.ErrorLog(ex); }
        }

        /// <summary>
        /// A simple say method for use outside the bot class
        /// </summary>
        /// <param name="msg">what to send</param>
        public static void Say(string msg, bool devchat = false)
        {
            if (IsConnected())
            {
                if (!devchat && Server.useglobal) globalchat.SendMessage(SendType.Message, channel, IRCColor.MinecraftToIRCColor(msg));
                else if (devchat) globalchat.SendMessage(SendType.Message, devchannel, IRCColor.MinecraftToIRCColor(msg));
            }
        }

        public static bool IsConnected() { return (globalchat == null) ? false : globalchat.IsConnected; }

        public static void Reset()
        {
            if (globalchat.IsConnected)
                globalchat.Disconnect();
            globalThread = new Thread(new ThreadStart(delegate
            {
                try { globalchat.Connect(server, 6667); }
                catch (Exception e)
                {
                    Server.s.Log("Error Connecting to MCDawn Global Chat");
                    Server.s.Log(e.ToString());
                }
            }));
            globalThread.Start();
        }
        public static string[] GetConnectedUsers()
        {
            return names;
        }

        public static void ShutDown()
        {
            globalchat.Disconnect();
            globalThread.Abort();
        }
    }
}
