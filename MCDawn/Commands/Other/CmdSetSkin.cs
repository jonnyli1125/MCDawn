using System;

namespace MCDawn
{
    public class CmdSetSkin : Command
    {
        public override string name { get { return "setskin"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdSetSkin() { }

        public override void Use(Player p, string message)
        {
            if (p == null) { Player.SendMessage(p, "Command not usable in Console."); return; }
            if (p.hidden) { Player.SendMessage(p, "Unhide first to use /setskin."); return; }
            if (message.Split(' ').Length > 2) { Help(p); return; }
            if (message.Split(' ').Length == 2 && message.Split(' ')[1].ToLower().Trim() != "s") { Help(p); return; }
            if (message.Trim() == "") 
            { 
                Player.GlobalMessage(p.color + p.name + "&g's skin was reset to original skin.");
                Player.GlobalDie(p, false);
                //Player.GlobalSpawn(p, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], false);
                Player.SkinChange(p, p.color + p.name, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1]);
            }
            else if (message.ToLower().Trim() == "s")
            {
                Player.GlobalMessageAdmins("To Admins: " + p.color + p.name + "&g's skin was reset to original skin.");
                Player.GlobalDie(p, false);
                //Player.GlobalSpawn(p, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], false);
                Player.SkinChange(p, p.color + p.name, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1]);
            }
            else if (message.Split(' ').Length == 2 && message.Split(' ')[1].ToLower().Trim() == "s")
            {
                Player.GlobalMessageAdmins("To Admins: " + p.color + p.name + "&g's skin set to the skin of " + message.Split(' ')[0] + ".");
                Player.GlobalDie(p, false);
                Player.SkinChange(p, p.color + message.Split(' ')[0], p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1]);
            }
            else
            {
                Player.GlobalMessage(p.color + p.name + "&g's skin set to the skin of " + message.Split(' ')[0] + ".");
                Player.GlobalDie(p, false);
                Player.SkinChange(p, p.color + message, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1]); // Cannot use marker, that messes up the whole thing... Unless we have 2 GlobalSpawn type things, and on player's movement have those constantly stay together, but I'm way too lazy to do that lol. That would still cause a retarded half-steve, half-skin effect, so basically, spawning skinned players with uncorresponding names is not possible, unless client side hax.
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/setskin [playername] - Changes your character's skin to [playername]'s skin. If [playername] is not given, the player's skin will revert back to their original skin.");
            Player.SendMessage(p, "/setskin [playername] s - Changes skin, but in silent mode.");
            Player.SendMessage(p, "/setskin s - Reverts skin, but in silent mode.");
        }
    }
}