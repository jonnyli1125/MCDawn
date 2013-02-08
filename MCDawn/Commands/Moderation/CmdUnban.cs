using System;

namespace MCDawn
{
    public class CmdUnban : Command
    {
        public override string name { get { return "unban"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdUnban() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            bool totalUnban = false;
            if (message[0] == '@')
            {
                totalUnban = true;
                message = message.Remove(0, 1).Trim();
            }

            Player who = Player.Find(message);

            if (who == null)
            {
                if (Group.findPlayerGroup(message) != Group.findPerm(LevelPermission.Banned))
                {
                    foreach (Server.TempBan tban in Server.tempBans)
                    {
                        if (tban.name.ToLower() == message.ToLower())
                        {
                            Server.tempBans.Remove(tban);
                            Player.GlobalMessage(message + " has had their temporary ban lifted.");
                            return;
                        }
                    }
                    Player.SendMessage(p, "Player is not banned.");
                    return;
                }
                Player.GlobalMessage(message + " &8(banned)" + "&g is now " + Group.standard.color + Group.standard.name + "&g!");
                Group.findPerm(LevelPermission.Banned).playerList.Remove(message);
            }
            else
            {
                if (Group.findPlayerGroup(message) != Group.findPerm(LevelPermission.Banned))
                {
                    foreach (Server.TempBan tban in Server.tempBans)
                    {
                        if (tban.name == who.name)
                        {
                            Server.tempBans.Remove(tban);
                            Player.GlobalMessage(who.color + who.prefix + who.name + "&ghas had their temporary ban lifted.");
                            return;
                        }
                    }
                    Player.SendMessage(p, "Player is not banned.");
                    return;
                }
                Player.GlobalChat(who, who.color + who.prefix + who.name + "&g is now " + Group.standard.color + Group.standard.name + "&g!", false);
                who.group = Group.standard; who.color = who.group.color; Player.GlobalDie(who, false);
                Player.GlobalSpawn(who, who.pos[0], who.pos[1], who.pos[2], who.rot[0], who.rot[1], false);
                Group.findPerm(LevelPermission.Banned).playerList.Remove(message);
            }

            Group.findPerm(LevelPermission.Banned).playerList.Save(); 
            if (totalUnban)
            {
                Command.all.Find("unbanip").Use(p, "@" + message);
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/unban <player> - Unbans a player.  This includes temporary bans.");
        }
    }
}