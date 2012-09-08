using System;
using System.IO;

namespace MCDawn
{
    public class CmdJail : Command
    {
        public override string name { get { return "jail"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdJail() { }

        public override void Use(Player p, string message)
        {
            if ((message.ToLower() == "create" || message.ToLower() == "") && p != null)
            {
                p.level.jailx = p.pos[0]; p.level.jaily = p.pos[1]; p.level.jailz = p.pos[2];
                p.level.jailrotx = p.rot[0]; p.level.jailroty = p.rot[1];
                Player.SendMessage(p, "Set Jail point.");
            }
            else
            {
                Player who = Player.Find(message);
                if (who != null)
                {
                    if (Server.devs.Contains(who.originalName.ToLower()))
                    {
                        Player.SendMessage(p, "Woah!! You can't jail a MCDawn Developer!");
                        if (p == null)
                        {
                            Player.GlobalMessage("The Console is crazy! Trying to jail a MCDawn Developer!");
                        }
                        else
                        {
                            Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to jail a MCDawn Developer!");
                        }
                        return;
                    }
                    if (Server.staff.Contains(who.originalName.ToLower()))
                    {
                        Player.SendMessage(p, "Woah!! You can't jail a MCDawn Staff Member!");
                        if (p == null)
                        {
                            Player.GlobalMessage("The Console is crazy! Trying to jail a MCDawn Staff Member!");
                        }
                        else
                        {
                            Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to jail a MCDawn Staff Member!");
                        }
                        return;
                    }
                    if (Server.administration.Contains(who.originalName.ToLower()))
                    {
                        Player.SendMessage(p, "Woah!! You can't jail a MCDawn Administrator!");
                        if (p == null)
                        {
                            Player.GlobalMessage("The Console is crazy! Trying to jail a MCDawn Administrator!");
                        }
                        else
                        {
                            Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to jail a MCDawn Administrator!");
                        }
                        return;
                    }
                    if (!who.jailed)
                    {
                        if (p != null) if (who.group.Permission >= p.group.Permission) { Player.SendMessage(p, "Cannot jail someone of equal or greater rank."); return; }
                        if (who.level != p.level) Command.all.Find("goto").Use(who, p.level.name);
                        Player.GlobalDie(who, false);
                        Player.GlobalSpawn(who, p.level.jailx, p.level.jaily, p.level.jailz, p.level.jailrotx, p.level.jailroty, true);
                        who.jailed = true;
                        Player.GlobalChat(null, who.color + who.name + Server.DefaultColor + " was &8jailed", false);
                    }
                    else
                    {
                        who.jailed = false;
                        Player.GlobalChat(null, who.color + who.name + Server.DefaultColor + " was &afreed" + Server.DefaultColor + " from jail", false);
                    }
                }
                else
                {
                    Player.SendMessage(p, "Could not find specified player.");
                }
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/jail [user] - Places [user] in jail unable to use commands.");
            Player.SendMessage(p, "/jail [create] - Creates the jail point for the map.");
        }
    }
}