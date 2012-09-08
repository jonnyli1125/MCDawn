using System;
using System.Collections.Generic;
//using MySql.Data.MySqlClient;
//using MySql.Data.Types;

namespace MCDawn
{
    public class CmdMoney : Command
    {
        public override string name { get { return "money"; } }
        public override string[] aliases { get { return new string[] { "balance", "bal" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdMoney() { }

        public override void Use(Player p, string message)
        {
            Player who = Player.Find(message);
            if (message == "") { who = p; }
            if (who == null) { p.SendMessage("Player could not be found."); return; }
            p.SendMessage(who.color + who.name + Server.DefaultColor + " currently has " + who.money + " " + Server.moneys);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/money <player> - See how much money <player> currently has.");
        }
    }
}       