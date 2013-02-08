using System;
using System.IO;

namespace MCDawn
{
    public class CmdHide : Command
    {
        public override string name { get { return "hide"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdHide() { }

        public override void Use(Player p, string message)
        {
            try
            {
                if (p.possess != "")
                {
                    Player.SendMessage(p, "Stop your current possession first.");
                    return;
                }
                if (p.following != "")
                {
                    Player.SendMessage(p, "Stop your follow first.");
                    return;
                }
                p.hidden = !p.hidden;
                if (message == "")
                {
                    if (p.hidden)
                    {
                        Player.GlobalDie(p, true);
                        Player.GlobalMessageOps("To Ops: " + p.color + p.name + "&g is now &finvisible" + "&g.");
                        if (!String.IsNullOrEmpty(p.logoutmessage)) { Player.GlobalChat(null, "&c- " + p.color + p.prefix + p.displayName + " &g" + p.logoutmessage, false); }
                        else { Player.GlobalChat(null, "&c- " + p.color + p.prefix + p.displayName + " &gdisconnected.", false); }
                        if (Server.womText) { Player.WomDisc(p); }
                        //Player.SendMessage(p, "You're now &finvisible&e.");
                    }
                    else
                    {
                        Player.GlobalSpawn(p, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], false);
                        Player.GlobalMessageOps("To Ops: " + p.color + p.name + "&g is now &fvisible" + "&g.");
                        if (!String.IsNullOrEmpty(p.loginmessage)) { Player.GlobalChat(null, "&a+ " + p.color + p.prefix + p.displayName + " &g" + p.loginmessage, false); }
                        else if (!Server.useMaxMind) { Player.GlobalChat(null, "&a+ " + p.color + p.prefix + p.displayName + " &gjoined the game.", false); }
                        else { Player.GlobalChat(null, "&a+ " + p.color + p.prefix + p.displayName + " &gjoined the game from " + p.countryName + ".", false); }
                        if (Server.womText) { Player.WomJoin(p); }
                        //Player.SendMessage(p, "You're now &fvisible&e.");
                    }
                }
                if (message.ToLower() == "s")
                {
                    if (p.hidden)
                    {
                        Player.GlobalDie(p, true);
                        Player.GlobalMessageAdmins("To Admins: " + p.color + p.name + "&g is now &finvisible" + "&g.");
                        Player.SendMessage(p, "You're now &finvisible&e.");
                    }
                    else
                    {
                        Player.GlobalSpawn(p, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], false);
                        Player.GlobalMessageAdmins("To Admins: " + p.color + p.name + "&g is now &fvisible" + "&g.");
                        Player.SendMessage(p, "You're now &fvisible&e.");
                    }
                }
            }
            catch { Help(p); return; }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/hide - Makes yourself (in)visible to other players.");
            Player.SendMessage(p, "/hide s - Stealth hide/unhide.");
        }
    }
}