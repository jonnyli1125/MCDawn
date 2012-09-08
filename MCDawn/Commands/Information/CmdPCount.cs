using System;
using System.Data;
using System.IO;
//using MySql.Data.MySqlClient;
//using MySql.Data.Types;

namespace MCDawn
{
    class CmdPCount : Command
    {
        public override string name { get { return "pcount"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "information"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdPCount() { }

        public override void Use(Player p, string message)
        {
            int bancount = Group.findPerm(LevelPermission.Banned).playerList.All().Count;

            try
            {
                DataTable count = MySQL.fillData("SELECT COUNT(id) FROM players");
                Player.SendMessage(p, "A total of " + count.Rows[0]["COUNT(id)"] + " unique players have visited this server.");
                Player.SendMessage(p, "Of these players, " + bancount + " have been banned.");
                count.Dispose();
            }
            catch 
            {
                Player.SendMessage(p, "A total of " + (int)(Player.players.Count + Player.left.Count) + " unique players have visited this server.");
                Player.SendMessage(p, "Of these players, " + bancount + " have been banned.");
            }

            int playerCount = 0;
            int hiddenCount = 0;
           
            foreach (Player pl in Player.players)
            {
                if (!pl.hidden || p.group.Permission > LevelPermission.AdvBuilder || Server.devs.Contains(p.name.ToLower()))
                {
                    playerCount++;
                    if (pl.hidden && (p.group.Permission > LevelPermission.AdvBuilder || Server.devs.Contains(p.name.ToLower())))
                    {
                        hiddenCount++;
                    }
                }
            }
            if (playerCount == 1)
            {
                if (hiddenCount == 0)
                {
                    Player.SendMessage(p, "There is 1 player currently online.");
                }
                else
                {
                    Player.SendMessage(p, "There is 1 player currently online (" + hiddenCount + " hidden).");
                }
            }
            else
            {
                if (hiddenCount == 0)
                {
                    Player.SendMessage(p, "There are " + playerCount + " players online.");
                }
                else
                {
                    Player.SendMessage(p, "There are " + playerCount + " players online (" + hiddenCount + " hidden).");
                }
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/pcount - Displays the number of players online and total.");
        }
    }
}
