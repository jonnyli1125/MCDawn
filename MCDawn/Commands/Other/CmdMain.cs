using System;

namespace MCDawn
{
    public class CmdMain : Command
    {
        public override string name { get { return "main"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdMain() { }

        public override void Use(Player p, string message)
        {
            try
            {
                Player who = Player.Find(message);
                if (message == "" || who == p)
                {
                    if (p.level.name == Server.mainLevel.name) { Player.SendMessage(p, "You are already on the servers main level!"); return; }
                    Command.all.Find("goto").Use(p, Server.mainLevel.name);
                }
                else
                {
                    if (who == null) { Player.SendMessage(p, "Player could not be found!"); }
                    if (who.group.Permission > p.group.Permission) { Player.SendMessage(p, "You can't move players of a higher rank to the Main level!"); return; }
                    if (who.level.name == Server.mainLevel.name) { Player.SendMessage(p, who.color + who.name + Server.DefaultColor + " is already on the servers main level!"); return; }
                    Command.all.Find("goto").Use(who, Server.mainLevel.name);
                }
            }
            catch { Help(p); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/main <player> - Takes you or <player> to the servers main level.");
        }
    }
}