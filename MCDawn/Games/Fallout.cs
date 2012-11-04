using System;
using System.Collections.Generic;
using System.IO;

// Game where there's a grid, and there are 2x2 squares, they start out yellow, turn orange, then red, and then disappear, last man surviving wins.
// Basically "automated spleef".
// Example: http://www.minecraftwiki.net/wiki/Custom_servers/D3#Fallout

namespace MCDawn
{
    public class Fallout
    {
        public Level Level;
        public bool GameActive = false;

        public void Start()
        {

        }

        public void CreateFalloutLevel()
        {
            Player.GlobalMessageLevel(Level, "Creating Fallout Level...");
            Level.Instant = true;
            var olp = Level.permissionbuild;
            Level.permissionbuild = LevelPermission.Nobody;
            for (ushort x = 0; x < Level.width; x++)
                for (ushort y = 0; y < Level.width; y++)
                    for (ushort z = 0; z < Level.width; z++)
                        Level.SetTile(x, y, z, Block.air);
            ushort FalloutMatY = (ushort)Math.Round((decimal)(Level.height / 4));
            for (ushort x = 0; x < Level.width; x++)
                for (ushort z = 0; z < Level.depth; z++)
                    Level.SetTile(x, FalloutMatY, z, Block.op_glass);
            int cx = 0, cz = 0;
            for (ushort x = 0; x < Level.width; x++)
                for (ushort z = 0; z < Level.depth; z++)
                {
                    if ((cx == 2 || cx == 3) && (cz == 2 || cz == 3))
                        Level.SetTile(x, FalloutMatY, z, Block.yellow);
                    cx += (cx < 4) ? 1 : -4;
                    cz += (cz < 4) ? 1 : -4;
                }
            Level.Instant = false;
            Level.permissionbuild = olp;
            if (Level.players.Count > 0)
                Level.players.ForEach(p => { Command.all.Find("reveal").Use(p, ""); });
            Player.GlobalMessageLevel(Level, "Finished creating Fallout level.");
        }

        private void RestoreLevel(string backupName)
        {
            if (File.Exists(@Server.backupLocation + "/" + Level.name + "/" + backupName + "/" + Level.name + ".lvl"))
            {
                try
                {
                    File.Copy(@Server.backupLocation + "/" + Level.name + "/" + backupName + "/" + Level.name + ".lvl", "levels/" + Level.name + ".lvl", true);
                    Level temp = Level.Load(Level.name);
                    temp.physThread.Start();
                    if (temp != null)
                    {
                        Level.spawnx = temp.spawnx;
                        Level.spawny = temp.spawny;
                        Level.spawnz = temp.spawnz;

                        Level.height = temp.height;
                        Level.width = temp.width;
                        Level.depth = temp.depth;

                        Level.blocks = temp.blocks;
                        Level.setPhysics(0);
                        Level.ClearPhysics();

                        if (Level.players.Count > 0)
                            Level.players.ForEach(p => { Command.all.Find("reveal").Use(p, ""); });
                    }
                    else
                    {
                        Server.s.Log("Restore nulled");
                        File.Copy("levels/" + Level.name + ".lvl.backup", "levels/" + Level.name + ".lvl", true);
                    }

                }
                catch { Server.s.Log("Restore fail"); }
            }
        }

        public void End()
        {
        }

        public List<Player> GetAlive()
        {
            List<Player> ReturnValue = new List<Player>();
            Level.players.ForEach(p => { if (p.FalloutAlive && !ReturnValue.Contains(p)) ReturnValue.Add(p); });
            return ReturnValue;
        }

        public List<Player> GetDead()
        {
            List<Player> ReturnValue = new List<Player>();
            Level.players.ForEach(p => { if (!p.FalloutAlive && !ReturnValue.Contains(p)) ReturnValue.Add(p); });
            return ReturnValue;
        }
    }
}
