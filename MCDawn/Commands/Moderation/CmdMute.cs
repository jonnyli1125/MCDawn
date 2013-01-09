using System;

namespace MCDawn
{
    public class CmdMute : Command
    {
        public override string name { get { return "mute"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdMute() { }

        public override void Use(Player p, string message)
        {
            if (message == "" || message.Split(' ').Length > 2) { Help(p); return; }
            Player who = Player.Find(message);
            if (who == null)
            {
                Player.SendMessage(p, "The player entered is not online.");
                return;
            }
            if (Server.devs.Contains(who.originalName.ToLower()))
            {
                Player.SendMessage(p, "Woah!! You can't mute a MCDawn Developer!");
                if (p == null)
                {
                    Player.GlobalMessage("The Console is crazy! Trying to mute a MCDawn Developer!");
                }
                else
                {
                    Player.GlobalMessage(p.color + p.name + "&g is crazy! Trying to mute a MCDawn Developer!");
                }
                return;
            }
            if (who.muted)
            {
                who.muted = false;
                Player.GlobalChat(null, who.color + who.name + "&g has been &bun-muted", false);
            }
            else
            {
                if (p != null)
                {
                    if (who != p) if (who.group.Permission > p.group.Permission) { Player.SendMessage(p, "Cannot mute someone of a higher rank."); return; }
                }
                who.muted = true;
                Player.GlobalChat(null, who.color + who.name + "&g has been &8muted", false);
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/mute <player> - Mutes or unmutes the player.");
        }
    }
}