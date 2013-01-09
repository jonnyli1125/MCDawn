using System;

namespace MCDawn
{
    public class CmdP2P : Command
    {
        public override string name { get { return "p2p"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdP2P() { }

        public override void Use(Player p, string message)
        {
            if (message == "" || message.Split(' ').Length < 2) { Help(p); return; }
            Player toSend = Player.Find(message.Split(' ')[0]);
            Player target = Player.Find(message.Split(' ')[1]);
            if (toSend == null || target == null) { Player.SendMessage(p, "Player could not be found."); return; }
            if (toSend == p)
            {
                if (target == p) { Player.SendMessage(p, "You can't teleport yourself to yourself."); return; }
                else { Player.SendMessage(p, "Use /tp <player> next time. Teleporting now..."); Command.all.Find("tp").Use(p, target.name); }
                return;
            }
            if (target == p) { Player.SendMessage(p, "Use /summon <player> next time. Summoning now..."); Command.all.Find("summon").Use(p, target.name); return; }
            if (p.group.Permission < toSend.group.Permission) { Player.SendMessage(p, "Cannot teleport a player of higher rank."); return; }
            if (target.level.name.Contains("cMuseum") || toSend.level.name.Contains("cMuseum")) { Player.SendMessage(p, "Player is in a museum!"); return; }

            if (toSend.level != target.level) 
            { 
                Command.all.Find("goto").Use(toSend, target.level.name); 
                while (toSend.Loading) { } 
            }
            if (toSend.level == target.level)
            {
                if (target.Loading)
                {
                    Player.SendMessage(p, "Waiting for " + target.color + target.name + "&g to spawn...");
                    while (target.Loading) { }
                }
                while (toSend.Loading) { }  //Wait for player to spawn in new map
                unchecked { toSend.SendPos((byte)-1, target.pos[0], target.pos[1], target.pos[2], target.rot[0], 0); }
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/p2p <player1> <player2> - Teleport <player1> to <player2>'s location.");
        }
    }
}