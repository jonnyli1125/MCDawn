/*-----------------------------------------------------------------------------------------------------
* This code was written by Jonny Li, also known as jonnyli1125, for use with MCDawn only.
* Licensed under a Creative Commons Attribution-NonCommercial-NoDerivs 3.0 Unported License.
* http://creativecommons.org/licenses/by-nc-nd/3.0/
* Any of the conditions stated in the license can be waived if you get written permission from the copyright holder.  
* ----------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace MCDawn
{
    public class PushBallTeam
    {
        public char color;
        public int points = 0;
        public List<Pos> goalPositions = new List<Pos>();
        public List<Spawn> spawns = new List<Spawn>();
        public List<Player> players = new List<Player>();
        public Level level;
        public bool spawnset;
        public string teamstring = "";

        public void AddMember(Player p)
        {
            if (p.pushBallTeam != this)
            {
                if (p.pushBallTeam != null) { p.pushBallTeam.RemoveMember(p); }
                p.pushBallTeam = this;
                Player.GlobalDie(p, false);
                p.PushBalltempcolor = p.color;
                p.PushBalltempprefix = p.prefix;
                p.color = "&" + color;
                p.prefix = p.color + "[" + c.Name("&" + color).ToUpper() + "] ";
                players.Add(p);
                level.ChatLevel(p.color + p.prefix + p.name + Server.DefaultColor + "&e has joined the " + teamstring + ".");
                Player.GlobalSpawn(p, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], false);
                if (level.pushBallStarted)
                {
                    SpawnPlayer(p);
                }
            }
        }

        public void RemoveMember(Player p)
        {
            if (p.pushBallTeam == this)
            {
                p.pushBallTeam = null;
                Player.GlobalDie(p, false);
                p.color = p.PushBalltempcolor;
                p.prefix = p.PushBalltempprefix;
                players.Remove(p);
                level.ChatLevel(p.color + p.prefix + p.name + Server.DefaultColor + "&e has left the " + teamstring + ".");
                Player.GlobalSpawn(p, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], false);
            }
        }

        public void SpawnPlayer(Player p)
        {
            p.spawning = true;
            if (spawns.Count != 0)
            {
                Random random = new Random();
                int rnd = random.Next(0, spawns.Count);
                ushort x, y, z, rotx;

                x = spawns[rnd].x;
                y = spawns[rnd].y;
                z = spawns[rnd].z;

                ushort x1 = (ushort)((0.5 + x) * 32);
                ushort y1 = (ushort)((1 + y) * 32);
                ushort z1 = (ushort)((0.5 + z) * 32);
                rotx = spawns[rnd].rotx;
                unchecked
                {
                    p.SendSpawn((byte)-1, p.name, x1, y1, z1, (byte)rotx, 0);
                }
            }
            else
            {
                ushort x = (ushort)((0.5 + level.spawnx) * 32);
                ushort y = (ushort)((1 + level.spawny) * 32);
                ushort z = (ushort)((0.5 + level.spawnz) * 32);
                ushort rotx = level.rotx;
                ushort roty = level.roty;

                unchecked
                {
                    p.SendSpawn((byte)-1, p.name, x, y, z, (byte)rotx, (byte)roty);
                }
            }
            p.spawning = false;
        }

        public void AddSpawn(ushort x, ushort y, ushort z, ushort rotx, ushort roty)
        {
            Spawn workSpawn = new Spawn();
            workSpawn.x = x;
            workSpawn.y = y;
            workSpawn.z = z;
            workSpawn.rotx = rotx;
            workSpawn.roty = roty;

            spawns.Add(workSpawn);
        }

        public static byte GetColorBlock(char color)
        {
            if (color == '2')
                return Block.green;
            if (color == '5')
                return Block.purple;
            if (color == '8')
                return Block.darkgrey;
            if (color == '9')
                return Block.blue;
            if (color == 'c')
                return Block.red;
            if (color == 'e')
                return Block.yellow;
            if (color == 'f')
                return Block.white;
            else
                return Block.air;
        }

        public struct Pos { public ushort x, y, z; }
        public struct CatchPos { public ushort x, y, z; public byte type; }
        public struct Spawn { public ushort x, y, z, rotx, roty; }
    }
}