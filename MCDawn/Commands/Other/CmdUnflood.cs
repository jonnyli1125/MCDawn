using System;
using System.Threading;

namespace MCDawn
{
    public class CmdUnflood : Command
    {
        public override string name { get { return "unflood"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public override void Help(Player p) { Player.SendMessage(p, "/unflood - Unfloods the map you are on, of liquids."); }
        public override void Use(Player p, string message)
        {
            if (p == null) { Player.SendMessage(p, "Command not usable in Console."); return; }
            if (message != "") { Help(p); return; }
            int phys = p.level.physics;
            Command.all.Find("physics").Use(p, "0");
            if (!p.level.Instant)
                Command.all.Find("map").Use(p, "instant");

            Command.all.Find("replaceall").Use(p, "lavafall air");
            Command.all.Find("replaceall").Use(p, "waterfall air");
            Command.all.Find("replaceall").Use(p, "active_lava air");
            Command.all.Find("replaceall").Use(p, "active_water air");
            Command.all.Find("replaceall").Use(p, "active_hot_lava air");
            Command.all.Find("replaceall").Use(p, "active_cold_water air");
            Command.all.Find("replaceall").Use(p, "magma air");
            Command.all.Find("replaceall").Use(p, "geyser air");

            if (p.level.Instant)
                Command.all.Find("map").Use(p, "instant");
            p.level.players.ForEach(pl => {
                Thread t = new Thread(new ThreadStart(delegate {
                    try { Command.all.Find("reveal").Use(pl, ""); }
                    catch (Exception ex) { Server.ErrorLog(ex); }
                }));
                t.Start();
            });
            Command.all.Find("physics").Use(p, phys.ToString());
            Player.GlobalMessage("Map has been Unflooded!");
        
        }
    }
}