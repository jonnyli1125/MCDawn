using System;
using System.IO;

namespace MCDawn
{
    public class CmdBotRemove : Command
    {
        public override string name { get { return "botremove"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public string[,] botlist;
        public CmdBotRemove() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            try
            {
                if (message.ToLower() == "all")
                {
                    for (int i = 0; i < PlayerBot.playerbots.Count; i++)
                    {
                        if (PlayerBot.playerbots[i].level == p.level)
                        {
                            //   PlayerBot.playerbots.Remove(PlayerBot.playerbots[i]);
                            PlayerBot Pb = PlayerBot.playerbots[i];
                            Pb.removeBot();
                            i--;
                        }
                    }
                }
                else
                {
                    PlayerBot who = PlayerBot.Find(message);
                    if (who == null) { Player.SendMessage(p, "There is no bot " + who + "!"); return; }
                    if (p.level != who.level) { Player.SendMessage(p, who.name + " is in a different level."); return; }
                    who.removeBot();
                    Player.SendMessage(p, "Removed bot.");
                }
            }
            catch (Exception e) { Server.ErrorLog(e); Player.SendMessage(p, "Error caught"); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/botremove <name> - Remove a bot on the same level as you");
            //   Player.SendMessage(p, "If All is used, all bots on the current level are removed");
        }
    }
}