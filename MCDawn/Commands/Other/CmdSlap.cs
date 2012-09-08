using System;

namespace MCDawn
{
    public class CmdSlap : Command
    {
        public override string name { get { return "slap"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdSlap() { }

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
                            Command.all.Find("slap").Use(p, pl.name);
                        }
                    }
                }
                return;
            }
            if (Server.devs.Contains(who.originalName.ToLower()))
            {
                Player.SendMessage(p, "Woah!! You can't slap a MCDawn Developer!");
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
            ushort foundHeight = 0;

            for (ushort yy = currentY; yy <= 1000; yy++)
            {
                if (p != null)
                {
                    if (!Block.Walkthrough(p.level.GetTile(currentX, yy, currentZ)) && p.level.GetTile(currentX, yy, currentZ) != Block.Zero)
                    {
                        foundHeight = (ushort)(yy - 1);
                        Player.GlobalMessage(who.color + who.name + Server.DefaultColor + " was slapped into the roof by " + p.color + p.name);
                        break;
                    }
                }
                else
                {
                    if (!Block.Walkthrough(who.level.GetTile(currentX, yy, currentZ)) && who.level.GetTile(currentX, yy, currentZ) != Block.Zero)
                    {
                        foundHeight = (ushort)(yy - 1);
                        Player.GlobalMessage(who.color + who.name + Server.DefaultColor + " was slapped into the roof by the Console.");
                        break;
                    }
                }
            }
            if (foundHeight == 0)
            {
                if (p != null)
                {
                    Player.GlobalMessage(who.color + who.name + Server.DefaultColor + " was slapped sky high by " + p.color + p.name);
                }
                else
                {
                    Player.GlobalMessage(who.color + who.name + Server.DefaultColor + " was slapped sky high by the Console.");
                }
                foundHeight = 1000;
            }

            unchecked { who.SendPos((byte)-1, who.pos[0], (ushort)(foundHeight * 32), who.pos[2], who.rot[0], who.rot[1]); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/slap <name/mapname> - Slaps <name> or everyone on <mapname>, knocking them into the air");
        }
    }
}