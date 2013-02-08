using System;
using System.IO;

namespace MCDawn
{
    public class CmdKill : Command
    {
        public override string name { get { return "kill"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdKill() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }

            Player who; string killMsg; int killMethod = 0;
            if (message.IndexOf(' ') == -1)
            {
                who = Player.Find(message);
                if (Server.devs.Contains(who.originalName.ToLower()))
                {
                    Player.SendMessage(p, "Woah!! You can't kill a MCDawn Developer!");
                    if (p == null)
                    {
                        Player.GlobalMessage("The Console is crazy! Trying to kill a Developer!");
                    }
                    else
                    {
                        Player.GlobalMessage(p.color + p.name + "&g is crazy! Trying to kill a Developer!");
                    }
                    return;
                }
                if (p != null)
                {
                    killMsg = " was killed by " + p.color + p.name;
                }
                else
                {
                    killMsg = " was killed by the Console.";
                }
            }
            else
            {
                who = Player.Find(message.Split(' ')[0]);
                message = message.Substring(message.IndexOf(' ') + 1);
                if (Server.devs.Contains(who.name.ToLower()))
                {
                    if (p == null)
                    {
                        Player.GlobalMessage("The Console is crazy! Trying to kill a Developer!");
                    }
                    else
                    {
                        Player.GlobalMessage(p.color + p.name + "&g is crazy! Trying to kill a Developer!");
                    }
                    return;
                }
                if (message.IndexOf(' ') == -1)
                {
                    if (message.ToLower() == "explode")
                    {
                        if (p != null)
                        {
                            killMsg = " was exploded by " + p.color + p.name;
                        }
                        else
                        {
                            killMsg = " was exploded by the Console.";
                        }
                        killMethod = 1;
                    }
                    else
                    {
                        killMsg = " " + message;
                    }
                }
                else
                {
                    if (message.Split(' ')[0].ToLower() == "explode")
                    {
                        killMethod = 1;
                        message = message.Substring(message.IndexOf(' ') + 1);
                    }

                    killMsg = " " + message;
                }
            }

            if (who == null)
            {
                p.HandleDeath(Block.rock, " killed itself in its confusion");
                Player.SendMessage(p, "Could not find player");
                return;
            }

            if (who.group.Permission > p.group.Permission)
            {
                p.HandleDeath(Block.rock, " was killed by " + who.color + who.name);
                Player.SendMessage(p, "Cannot kill someone of higher rank");
                return;
            }

            if (killMethod == 1)
                who.HandleDeath(Block.rock, killMsg, true);
            else
                who.HandleDeath(Block.rock, killMsg);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/kill <name> [explode] <message>");
            Player.SendMessage(p, "Kills <name> with <message>. Causes explosion if [explode] is written");
        }
    }
}