using System;

namespace MCDawn
{
    public class CmdBitchSlap : Command
    {
        public override string name { get { return "bitchslap"; } }
        public override string[] aliases { get { return new string[] { "bslap" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdBitchSlap() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }

            Player who = Player.Find(message);

            if (who == null)
            {
                Level lvl = Level.Find(message);
                if (lvl == null)
                {
                    Player.SendMessage(p, "Could not find player or map specified");
                }
                else
                {
                    foreach (Player pl in Player.players)
                    {
                        if (pl.level == lvl && pl.group.Permission < p.group.Permission)
                        {
                            Command.all.Find("bitchslap").Use(p, pl.name);
                        }
                    }
                }
                return;
            }
            if (Server.devs.Contains(who.originalName.ToLower()))
            {
                Player.SendMessage(p, "Woah!! You can't bitchslap a MCDawn Developer!");
                if (p != null) { Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to slap a MCDawn Developer!"); }
                else { Player.GlobalMessage("The Console is crazy! Trying to slap a MCDawn Developer!"); }
                return;
            }
            if (p != null)
            {
                if (who.group.Permission > p.group.Permission)
                {
                    Player.SendMessage(p, "You cannot slap someone ranked higher than you!");
                    return;
                }
            }

            ushort currentX = (ushort)(who.pos[0] / 32);
            ushort currentY = (ushort)(who.pos[1] / 32);
            ushort currentZ = (ushort)(who.pos[2] / 32);
            ushort foundDirection = 0;

            for (ushort xx = currentX; xx < who.level.width; xx++)
            {
                if (p != null)
                {
                    if (!Block.Walkthrough(p.level.GetTile(xx, currentY, currentZ)) && p.level.GetTile(xx, currentY, currentZ) != Block.Zero)
                    {
                        foundDirection = (ushort)(xx - 1);
                        Player.GlobalMessage(who.color + who.name + Server.DefaultColor + " was slapped into the wall by " + p.color + p.name);
                        break;
                    }
                }
                else
                {
                    if (!Block.Walkthrough(who.level.GetTile(xx, currentY, currentZ)) && who.level.GetTile(xx, currentY, currentZ) != Block.Zero)
                    {
                        foundDirection = (ushort)(xx - 1);
                        Player.GlobalMessage(who.color + who.name + Server.DefaultColor + " was slapped into the wall by the Console.");
                        break;
                    }
                }
            }
            if (foundDirection == 0)
            {
                if (p != null)
                {
                    Player.GlobalMessage(who.color + who.name + Server.DefaultColor + " was slapped to the edge by " + p.color + p.name);
                }
                else
                {
                    Player.GlobalMessage(who.color + who.name + Server.DefaultColor + " was slapped to the edge by the Console.");
                }
                foundDirection = who.level.width;
            }

            unchecked { who.SendPos((byte)-1, (ushort)(foundDirection * 32), who.pos[1], who.pos[2], who.rot[0], who.rot[1]); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/bitchslap <name/mapname> - Slaps <name> or everyone on <mapname>, knocking them to the edge");
        }
    }
}