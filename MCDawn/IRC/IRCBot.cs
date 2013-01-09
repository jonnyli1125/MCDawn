using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using Meebey.SmartIrc4net;

namespace MCDawn
{
    class IRCBot
    {
        static IrcClient irc = new IrcClient();
        static string server = Server.ircServer;
        static string channel = Server.ircChannel;
        static string opchannel = Server.ircOpChannel;
        static string nick = Server.ircNick;
        static Thread ircThread;

        static Dictionary<string, List<string>> ChannelUsers = new Dictionary<string, List<string>>();

        public IRCBot()
        {
            // The IRC Bot must run in a seperate thread, or else the server will freeze.
            ircThread = new Thread(new ThreadStart(delegate
            {
                // Attach event handlers
                irc.OnConnecting += new EventHandler(OnConnecting);
                irc.OnConnected += new EventHandler(OnConnected);
                irc.OnChannelMessage += new IrcEventHandler(OnChanMessage);
                irc.OnJoin += new JoinEventHandler(OnJoin);
                irc.OnPart += new PartEventHandler(OnPart);
                irc.OnQuit += new QuitEventHandler(OnQuit);
                irc.OnNickChange += new NickChangeEventHandler(OnNickChange);
                irc.OnDisconnected += new EventHandler(OnDisconnected);
                irc.OnQueryMessage += new IrcEventHandler(OnPrivMsg);
                irc.OnNames += new NamesEventHandler(OnNames);
                irc.OnChannelAction += new ActionEventHandler(OnAction);
                irc.OnKick += new KickEventHandler(OnKick);

                // Attempt to connect to the IRC server
                try { irc.Connect(server, Server.ircPort); }
                catch (Exception ex) { Server.s.Log("Unable to connect to IRC server: " + ex.Message); }
            }));
            ircThread.Start();
        }

        // While connecting
        void OnConnecting(object sender, EventArgs e)
        {
            Server.s.Log("Connecting to IRC");
        }
        // When connected
        void OnConnected(object sender, EventArgs e)
        {
            Server.s.Log("Connected to IRC");
            irc.Login(nick, nick, 0, nick);

            // Check to see if we want to register our bot with nickserv

            if (Server.ircIdentify && Server.ircPassword != string.Empty)
            {
                Server.s.Log("Identifying with Nickserv");
                irc.SendMessage(SendType.Message, "nickserv", "IDENTIFY " + Server.ircPassword);
            }

            Server.s.Log("Joining channels");
            irc.RfcJoin(channel);
            irc.RfcJoin(opchannel);

            irc.Listen();
        }

        void OnNames(object sender, NamesEventArgs e)
        {
            if (ChannelUsers.ContainsKey(e.Data.Channel))
                ChannelUsers.Remove(e.Data.Channel);
            var userlist = new List<string>();
            foreach (string s in e.UserList)
                if (!String.IsNullOrEmpty(s.Trim()))
                    userlist.Add(s);
            for (int i = 0; i < userlist.Count; i++)
                if (!String.IsNullOrEmpty(userlist[i]))
                    if (!Player.IsValidIRCNick(userlist[i]))
                        userlist[i] = Player.ValidIRCNick(userlist[i]);
            ChannelUsers.Add(e.Data.Channel, userlist);
        }

        public static List<string> GetChannelUsers(string channel)
        {
            List<string> users = new List<string>();
            if (ChannelUsers.TryGetValue(channel, out users))
                return users;
            else return new List<string>();
        }

        void OnDisconnected(object sender, EventArgs e)
        {
            try { irc.Connect(server, 6667); }
            catch { Console.WriteLine("Failed to reconnect to IRC"); }
        }

        // On public channel message
        void OnChanMessage(object sender, IrcEventArgs e)
        {
            // TODO: make this irctominecraftcolor shit work
            string temp = IRCColor.IRCToMinecraftColor(e.Data.Message); string storedNick = e.Data.Nick;

            if (e.Data.Message[0] == '!') { OnCommand(sender, e); return; } 
            if (e.Data.Channel == opchannel)
            {
                Server.s.Log(Server.IRCColour + "[(Op) IRC] " + e.Data.Nick + ": &0" + temp);
                Player.GlobalMessageOps(Server.IRCColour + "[(Op) IRC] " + storedNick + ": &f" + temp);
            }
            else
            {
                Server.s.Log(Server.IRCColour + "[IRC] " + e.Data.Nick + ": &0" + temp);
                Player.GlobalChat(null, Server.IRCColour + "[IRC] " + storedNick + ": &f" + temp, false);
            }

            //if (temp.IndexOf(':') < temp.IndexOf(' ')) {
            //    storedNick = temp.Substring(0, temp.IndexOf(':'));
            //    temp = temp.Substring(temp.IndexOf(' ') + 1);
            //}

            //s.Log("IRC: " + e.Data.Nick + ": " + e.Data.Message);
            //Player.GlobalMessage("IRC: &1" + e.Data.Nick + ": &f" + e.Data.Message);
        }
        // For commands, like !players.
        void OnCommand(object sender, IrcEventArgs e)
        {
            string text = "";
            if (e.Data.Message[0] == '!') { text = e.Data.Message.Remove(0, 1); }
            else { text = e.Data.Message; }

            // Commands...
            if (text.Split(' ')[0].ToLower() == "players")
            {
                string toSay = "";
                for (int i = 0; i < Player.players.Count; i++) { toSay += Player.players[i].name + " (" + Player.players[i].level.name + "), "; }
                Say("There are currently " + Player.players.Count + " players online", e.Data.Channel == opchannel);
                if (toSay.Length > 0) Say(toSay.Remove(toSay.Length - 2), e.Data.Channel == opchannel);
            }
            if (text.Split(' ')[0].ToLower() == "url") { Say("URL: " + Server.URL, e.Data.Channel == opchannel); }
        }

        // When someone joins the IRC
        void OnJoin(object sender, JoinEventArgs e)
        {
            Server.s.Log(Server.IRCColour + e.Data.Nick + "&g has joined the " + (e.Data.Channel == opchannel ? "operator " : "") + "channel");
            Player.GlobalChat(null, Server.IRCColour + e.Data.Nick + "&g has joined the " + (e.Data.Channel == opchannel ? "operator " : "") + "channel", false);
            irc.RfcNames(channel);
            irc.RfcNames(opchannel);
        }
        // When someone leaves the IRC
        void OnPart(object sender, PartEventArgs e)
        {
            Server.s.Log(Server.IRCColour + e.Data.Nick + "&g has left the " + (e.Data.Channel == opchannel ? "operator " : "") + "channel" + (!String.IsNullOrEmpty(e.PartMessage) ? " (" + e.PartMessage + ")" : ""));
            Player.GlobalChat(null, Server.IRCColour + e.Data.Nick + "&g has left the " + (e.Data.Channel == opchannel ? "operator " : "") + "channel" + (!String.IsNullOrEmpty(e.PartMessage) ? " (" + e.PartMessage + ")" : ""), false);
            irc.RfcNames(channel);
            irc.RfcNames(opchannel);
        }
        void OnQuit(object sender, QuitEventArgs e)
        {
            Server.s.Log(Server.IRCColour + e.Data.Nick + "&g has quit IRC" + (!String.IsNullOrEmpty(e.QuitMessage) ? " (" + e.QuitMessage + ")" : ""));
            Player.GlobalChat(null, Server.IRCColour + e.Data.Nick + "&g has quit IRC" + (!String.IsNullOrEmpty(e.QuitMessage) ? " (" + e.QuitMessage + ")" : ""), false);
            irc.RfcNames(channel);
            irc.RfcNames(opchannel);
        }
        void OnPrivMsg(object sender, IrcEventArgs e)
        {
            Server.s.Log(Server.IRCColour + "[IRC] " + e.Data.Nick + "&b to Console: &g" + e.Data.Message);
            if (Server.ircControllers.Contains(e.Data.Nick))
            {
                string cmd;
                string msg;
                int len = e.Data.Message.Split(' ').Length;
                cmd = e.Data.Message.Split(' ')[0];
                msg = (len > 1) ? e.Data.Message.Substring(e.Data.Message.IndexOf(' ')).Trim() : "";
                if (msg != "" || cmd == "restart" || cmd == "update")
                {
                    Server.s.Log(cmd + " : " + msg);
                    switch (cmd)
                    {
                        case "kick":
                            if (Player.Find(msg.Split()[0]) != null)
                            {
                                Command.all.Find("kick").Use(null, msg);
                            }
                            else
                            {
                                irc.SendMessage(SendType.Message, e.Data.Nick, "Player not found.");
                            }
                            break;
                        case "ban":
                            if (Player.Find(msg) != null)
                            {
                                Command.all.Find("ban").Use(null, msg);
                            }
                            else
                            {
                                irc.SendMessage(SendType.Message, e.Data.Nick, "Player not found.");
                            }
                            break;
                        case "banip":
                            if (Player.Find(msg) != null)
                            {
                                Command.all.Find("banip").Use(null, msg);
                            }
                            else
                            {
                                irc.SendMessage(SendType.Message, e.Data.Nick, "Player not found.");
                            }
                            break;
                        case "say":
                            irc.SendMessage(SendType.Message, channel, msg); break;
                        case "setrank":
                            if (Player.Find(msg.Split(' ')[0]) != null)
                            {
                                Command.all.Find("setrank").Use(null, msg);
                            }
                            else
                            {
                                irc.SendMessage(SendType.Message, e.Data.Nick, "Player not found.");
                            }
                            break;
                        case "mute":
                            if (Player.Find(msg) != null)
                            {
                                Command.all.Find("mute").Use(null, msg);
                            }
                            else
                            {
                                irc.SendMessage(SendType.Message, e.Data.Nick, "Player not found.");
                            }
                            break;
                        case "joker":
                            if (Player.Find(msg) != null)
                            {
                                Command.all.Find("joker").Use(null, msg);
                            }
                            else
                            {
                                irc.SendMessage(SendType.Message, e.Data.Nick, "Player not found.");
                            }
                            break;
                        case "physics":
                            if (Level.Find(msg.Split(' ')[0]) != null)
                            {
                                Command.all.Find("physics").Use(null, msg);
                            }
                            else
                            {
                                irc.SendMessage(SendType.Message, e.Data.Nick, "Map not found.");
                            }
                            break;
                        case "load":
                            if (Level.Find(msg.Split(' ')[0]) != null)
                            {
                                Command.all.Find("load").Use(null, msg);
                            }
                            else
                            {
                                irc.SendMessage(SendType.Message, e.Data.Nick, "Map not found.");
                            }
                            break;
                        case "unload":
                            if (Level.Find(msg) != null || msg == "empty")
                            {
                                Command.all.Find("unload").Use(null, msg);
                            }
                            else
                            {
                                irc.SendMessage(SendType.Message, e.Data.Nick, "Map not found.");
                            }
                            break;
                        case "save":
                            if (Level.Find(msg) != null)
                            {
                                Command.all.Find("save").Use(null, msg);
                            }
                            else
                            {
                                irc.SendMessage(SendType.Message, e.Data.Nick, "Map not found.");
                            }
                            break;
                        case "map":
                            if (Level.Find(msg.Split(' ')[0]) != null)
                            {
                                Command.all.Find("map").Use(null, msg);
                            }
                            else
                            {
                                irc.SendMessage(SendType.Message, e.Data.Nick, "Map not found.");
                            }
                            break;
                        case "restart":
                            Player.GlobalMessage("Restart initiated by " + e.Data.Nick);
                            IRCBot.Say("Restart initiated by " + e.Data.Nick);
                            Command.all.Find("restart").Use(null, "");
                            break;
                        case "update":
                            Player.GlobalMessage("Update check initiated by " + e.Data.Nick);
                            IRCBot.Say("Update check initiated by " + e.Data.Nick);
                            Command.all.Find("update").Use(null, "");
                            break;
                        case "warn":
                            if (Player.Find(msg) != null)
                            {
                                Command.all.Find("warn").Use(null, msg);
                            }
                            else
                            {
                                irc.SendMessage(SendType.Message, e.Data.Nick, "Player not found.");
                            }
                            break;
                        case "slap":
                            if (Player.Find(msg) != null)
                            {
                                Command.all.Find("slap").Use(null, msg);
                            }
                            else
                            {
                                irc.SendMessage(SendType.Message, e.Data.Nick, "Player not found.");
                            }
                            break;
                        default:
                            irc.SendMessage(SendType.Message, e.Data.Nick, "Invalid command."); break;
                    }
                }
                else
                {
                    irc.SendMessage(SendType.Message, e.Data.Nick, "Invalid command format.");
                }
            }
        }
        void OnNickChange(object sender, NickChangeEventArgs e)
        {
            Player.GlobalMessage(Server.IRCColour + "[IRC] " + e.OldNickname + "&g is now known as " + e.NewNickname);
            Server.s.Log(Server.IRCColour + "[IRC] " + e.OldNickname + "&g is now known as " + e.NewNickname);
            irc.RfcNames(channel);
            irc.RfcNames(opchannel);
        }
        void OnAction(object sender, ActionEventArgs e)
        {
            string temp = IRCColor.IRCToMinecraftColor(e.ActionMessage); string storedNick = e.Data.Nick;
            if (e.Data.Channel == opchannel)
            {
                Server.s.Log(Server.IRCColour + "[(Op) IRC] *" + e.Data.Nick + " " + temp);
                Player.GlobalMessageOps(Server.IRCColour + "[(Op) IRC] *" + storedNick + " " + temp);
            }
            else
            {
                Server.s.Log(Server.IRCColour + "[IRC] *" + e.Data.Nick + " " + temp);
                Player.GlobalChat(null, Server.IRCColour + "[IRC] *" + storedNick + " " + temp, false);
            }
        }
        void OnKick(object sender, KickEventArgs e)
        {
            Player.GlobalMessage(Server.IRCColour + e.Data.Nick + " was kicked from the " + (e.Data.Channel == opchannel ? "operator " : "") + "channel (" + e.KickReason + ")");
            Server.s.Log(Server.IRCColour + e.Data.Nick + " was kicked from the " + (e.Data.Channel == opchannel ? "operator " : "") + "channel (" + e.KickReason + ")");
            irc.RfcNames(channel);
            irc.RfcNames(opchannel);
        }

        /// <summary>
        /// A simple say method for use outside the bot class
        /// </summary>
        /// <param name="msg">what to send</param>
        public static void Say(string msg, bool opchat = false)
        {
            if (IsConnected() && Server.irc)
                irc.SendMessage(SendType.Message, (opchat ? opchannel : channel), IRCColor.MinecraftToIRCColor(Player.RemoveBadColors(msg)));
        }

        public static bool IsConnected() { return (irc == null) ? false : irc.IsConnected; }

        public static void Reset()
        {
            if (irc.IsConnected)
                irc.Disconnect();
            ircThread = new Thread(new ThreadStart(delegate
            {
                try { irc.Connect(server, Server.ircPort); }
                catch (Exception e)
                {
                    Server.s.Log("Error Connecting to IRC");
                    Server.s.Log(e.ToString());
                }
            }));
            ircThread.Start();
        }

        public static void ShutDown()
        {
            irc.Disconnect();
            ircThread.Abort();
        }
    }
}
