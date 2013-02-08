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

namespace MCDawn
{
    public class CmdTempBan : Command
    {
        public override string name { get { return "tempban"; } }
        public override string[] aliases { get { return new string[] { "tb" }; } }
        public override string type { get { return "moderation"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }
        public CmdTempBan() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            if (message.IndexOf(' ') == -1) message = message + " 30";

            Player who = Player.Find(message.Split(' ')[0]);
            if (who == null) { Player.SendMessage(p, "Could not find player"); return; }
            if (who.group.Permission >= p.group.Permission) { Player.SendMessage(p, "Cannot ban someone of the same rank"); return; }
            if (Server.devs.Contains(who.originalName.ToLower()))
            {
                Player.SendMessage(p, "Woah!! You can't tempban a MCDawn Developer!");
                if (p == null)
                {
                    Player.GlobalMessage("The Console is crazy! Trying to tempban a MCDawn Developer!");
                }
                else
                {
                    Player.GlobalMessage(p.color + p.name + "&g is crazy! Trying to tempban a MCDawn Developer!");
                }
                return;
            }

            if (Server.staff.Contains(who.originalName.ToLower()))
            {
                if (p == null)
                {
                    Player.GlobalMessage("The Console is crazy! Trying to tempban a MCDawn Staff Member!");
                }
                else
                {
                    Player.GlobalMessage(p.color + p.name + "&g is crazy! Trying to tempban a MCDawn Staff Member!");
                }
                return;
            }
            if (Server.administration.Contains(who.originalName.ToLower()))
            {
                if (p == null)
                {
                    Player.GlobalMessage("The Console is crazy! Trying to tempban a MCDawn Administrator!");
                }
                else
                {
                    Player.GlobalMessage(p.color + p.name + "&g is crazy! Trying to tempban a MCDawn Administrator!");
                }
                return;
            }

            int minutes;
            try
            {
                minutes = int.Parse(message.Split(' ')[1]);
            } catch { Player.SendMessage(p, "Invalid minutes"); return; }
            if (minutes > 60) { Player.SendMessage(p, "Cannot ban for more than an hour"); return; }
            if (minutes < 1) { Player.SendMessage(p, "Cannot ban someone for less than a minute"); return; }
            
            Server.TempBan tBan;
            tBan.name = who.name;
            tBan.allowedJoin = DateTime.Now.AddMinutes(minutes);
            Server.tempBans.Add(tBan);
            who.Kick("Banned for " + minutes + " minutes!");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tempban <name> <minutes> - Bans <name> for <minutes>");
            Player.SendMessage(p, "Max time is 60. Default is 30");
            Player.SendMessage(p, "Temp bans will reset on server restart");
        }
    }
}