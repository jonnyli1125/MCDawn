/*
	Copyright 2010 MCSharp team (Modified for use with MCZall/MCLawl) Licensed under the
	Educational Community License, Version 2.0 (the "License"); you may
	not use this file except in compliance with the License. You may
	obtain a copy of the License at
	
	http://www.osedu.org/licenses/ECL-2.0
	
	Unless required by applicable law or agreed to in writing,
	software distributed under the License is distributed on an "AS IS"
	BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
	or implied. See the License for the specific language governing
	permissions and limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.Text;
using Meebey.SmartIrc4net;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace MCDawn
{
    class AllServerChat // ALL Servers in-game chat, will be read-only from the irc. For spying. Yes, I'm a cheap-ass.
    {
        static IrcClient globalchat = new IrcClient();
        static string server = "irc.geekshed.net";
        static string channel = "#ServerChat";
        //static string globalPass = "hopeful";
        //static string devGlobalPass = "voltage";
        static string nick = validNick(Server.name);
        static Thread globalThread;

        static string validNick(string s)
        {
            //for (int i = 0; i < s.Length; i++) { if (!Char.IsLetter(s, i)) { s = s.Replace(i, ""); } } // Didn't work for some reason... Or am I just retarded :/
            s = s.Replace("!", "");
            s = s.Replace(":", "");
            s = s.Replace(".", "");
            s = s.Replace(",", "");
            s = s.Replace("~", "");
            s = s.Replace("-", "");
            s = s.Replace("+", "");
            s = s.Replace("(", "");
            s = s.Replace(")", "");
            s = s.Replace("?", "");
            s = s.Replace("/", "");
            s = s.Replace(" ", "");
            for (int i = 0; i < 9; i++) { s = s.Replace(i.ToString(), ""); }
            return s;
        }

        static string[] names;

        public AllServerChat()
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
                globalchat.OnQueryMessage += new IrcEventHandler(OnPrivMsg);
                globalchat.OnNames += new NamesEventHandler(OnNames);
                globalchat.OnChannelAction += new ActionEventHandler(OnAction);

                // Attempt to connect to the IRC server
                try { globalchat.Connect(server, 6667); }
                catch { }
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
            //Server.s.Log("Server joined MCDawn Global Chat channel!");
            globalchat.Login(nick, nick, 0, nick);
            globalchat.RfcJoin(channel);
            globalchat.Listen();
        }

        void OnNames(object sender, NamesEventArgs e)
        {
            names = e.UserList;
        }
        void OnDisconnected(object sender, EventArgs e)
        {
            //Server.s.Log("Server disconnected from MCDawn Global Chat channel!");
            try { globalchat.Connect(server, 6667); }
            catch { }
        }

        // On public channel message
        void OnChanMessage(object sender, IrcEventArgs e)
        {
            //Player.GlobalMessageDevsStaff("To Devs/Staff: " + Server.GlobalChatColour + e.Data.Nick + ": &f" + e.Data.Message);
        }

        bool ValidString(string str)
        {
            string allowedchars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890\\";
            foreach (char ch in str)
            {
                if (allowedchars.IndexOf(ch) == -1)
                {
                    return false;
                }
            } return true;
        }
        // When someone joins the IRC
        void OnJoin(object sender, JoinEventArgs e)
        {
            //Player.GlobalMessageDevs("To Devs: " + Server.GlobalChatColour + e.Data.Nick + Server.DefaultColor + " has joined the Global Chat Channel.");
            globalchat.RfcNames(channel);
        }
        // When someone leaves the IRC
        void OnPart(object sender, PartEventArgs e)
        {
            //Player.GlobalMessageDevs("To Devs: " + Server.GlobalChatColour + e.Data.Nick + Server.DefaultColor + " has left the Global Chat Channel");
            globalchat.RfcNames(channel);
        }
        void OnQuit(object sender, QuitEventArgs e)
        {
            //Player.GlobalMessageDevs("To Devs: " + Server.GlobalChatColour + e.Data.Nick + Server.DefaultColor + " has quit the Global Chat Channel");
            globalchat.RfcNames(channel);
        }
        void OnPrivMsg(object sender, IrcEventArgs e)
        {
        	if (!String.IsNullOrEmpty(e.Data.Message.Trim())) Player.GlobalMessage(Server.GlobalChatColour + e.Data.Nick + ": &f" + e.Data.Message);
            else Player.GlobalMessage(Server.GlobalChatColour + e.Data.Nick + ": " + e.Data.Message); // No color code. EOL after color crashes clients.
            AllServerChat.Say("(ServerChat) " + e.Data.Nick + ": " + e.Data.Message);
            Server.s.Log("<" + e.Data.Nick + "> " + e.Data.Message);
        }
        public void OnNickChange(object sender, NickChangeEventArgs e)
        {
            //Player.GlobalMessageDevsStaff("To Devs/Staff: " + Server.GlobalChatColour + e.OldNickname + Server.DefaultColor + " is now known as " + e.NewNickname);
            globalchat.RfcNames(channel);
        }
        void OnAction(object sender, ActionEventArgs e)
        {
            //Player.GlobalMessageDevsStaff("To Devs/Staff: *" + Server.GlobalChatColour + e.Data.Nick + " " + Server.DefaultColor + e.ActionMessage);
        }


        /// <summary>
        /// A simple say method for use outside the bot class
        /// </summary>
        /// <param name="msg">what to send</param>
        public static void Say(string msg)
        {
            if (globalchat != null && globalchat.IsConnected && Server.useglobal)
                globalchat.SendMessage(SendType.Message, channel, msg);
        }
        public static bool IsConnected()
        {
            if (globalchat.IsConnected)
                return true;
            else
                return false;
        }


        public static void Reset()
        {
            if (globalchat.IsConnected)
                globalchat.Disconnect();
            globalThread = new Thread(new ThreadStart(delegate
            {
                try { globalchat.Connect(server, 6667); }
                catch { }
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
