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
namespace MCDawn
{
    public class CmdTempRank : Command
    {
        public override string name { get { return "temprank"; } }
        public override string[] aliases { get { return new string[] { "trank" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdTempRank() { }

        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length < 2) { Help(p); return; }
            Player who = Player.Find(message.Split(' ')[0]);
            Group newRank = Group.Find(message.Split(' ')[1]);
            string msgGave;

            if (message.Split(' ').Length > 2) msgGave = message.Substring(message.IndexOf(' ', message.IndexOf(' ') + 1)); else msgGave = "Congratulations!";
            if (newRank == null) { Player.SendMessage(p, "Could not find specified rank."); return; }

            Group bannedGroup = Group.findPerm(LevelPermission.Banned);
            if (who == null)
            {
                Player.SendMessage(p, "Player could not be found."); return;
            }
            else
            {
                //if (!Server.devs.Contains(p.name) && Server.devs.Contains(who.name)) { p.SendMessage("Can't let you do that, Starfox."); return; }
                if (p != null)
                {
                    if (who.group == bannedGroup || newRank == bannedGroup)
                    {
                        Player.SendMessage(p, "Cannot change the rank to or from \"" + bannedGroup.name + "\".");
                        return;
                    }

                    if (who.group.Permission >= p.group.Permission || newRank.Permission >= p.group.Permission)
                    {
                        Player.SendMessage(p, "Cannot change the rank of someone equal or higher to yourself."); return;
                    }
                }
                Player.GlobalChat(who, who.color + who.name + "&g's rank was temporarily set to " + newRank.color + newRank.name, false);
                Player.GlobalChat(null, "&6" + msgGave, false);
                who.group = newRank;
                who.color = who.group.color;
                Player.GlobalDie(who, false);
                who.SendMessage("You are now tempranked " + newRank.color + newRank.name + "&g, type /help for your new set of commands.");
                Player.GlobalSpawn(who, who.pos[0], who.pos[1], who.pos[2], who.rot[0], who.rot[1], false);
                MCDawn.Gui.Window.thisWindow.UpdateClientList(Player.players);
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/temprank <player> <rank> <yay> - Temporarily ranks a player <rank>.");
            Player.SendMessage(p, "You may use /trank as a shortcut");
            Player.SendMessage(p, "Valid Ranks are: " + Group.concatList(true, true));
            Player.SendMessage(p, "<yay> is a celebratory message");
            Player.SendMessage(p, "Tempranks last until the tempranked player logs out. When they log out, their rank is reset to their old rank.");
        }
    }
}
