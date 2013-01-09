/*-----------------------------------------------------------------------------------------------------
* This code was written by Jonny Li, also known as jonnyli1125, for use with MCDawn only.
* Licensed under a Creative Commons Attribution-NonCommercial-NoDerivs 3.0 Unported License.
* http://creativecommons.org/licenses/by-nc-sa/3.0/
* Any of the conditions stated in the license can be waived if you get written permission from the copyright holder.  
* ----------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Threading;

namespace MCDawn
{
    public class PushBall
    {
        public Level level;
        public Player lastTouched;
        public int winPoints = 3;
        System.Timers.Timer countDown;
        public bool ballSpawnSet = false;
        public Pos ballSpawn = new Pos();
        public List<PushBallTeam> pushBallTeams = new List<PushBallTeam>();
        public void Start()
        {
            if (level.players.Count < 2 || !ballSpawnSet || pushBallTeams.Count < 2) { return; }

            level.countdown = true;
            level.timeLeft = 10;
            countDown = new System.Timers.Timer(1000);
            countDown.Elapsed += delegate
            {
                if (level.timeLeft >= 10)
                {
                    SpawnBall();
                    Player.GlobalMessageLevel(level, "&ePushBall game to be started in 10 seconds!");
                    level.timeLeft--;
                }
                else if (level.timeLeft <= 9 && level.timeLeft >= 1)
                {
                    Player.GlobalMessageLevel(level, "&e" + level.timeLeft);
                    level.timeLeft--;
                }
                else if (level.timeLeft <= 0)
                {
                    countDown.Stop();
                    //countDown.Dispose();
                    Player.GlobalMessageLevel(level, "&eGO!");
                    level.countdown = false;
                    level.pushBallStarted = true;
                    foreach (Player pl in level.players)
                    {
                        if (pl.hidden && !pl.referee) { Command.all.Find("hide").Use(pl, "s"); }
                        if (pl.invincible && !pl.referee) { Command.all.Find("invincible").Use(pl, ""); }
                        if (pl.isFlying && !pl.referee) { Command.all.Find("fly").Use(pl, ""); }
                        if (!pl.referee) pl.pushBallTeam.SpawnPlayer(pl);
                        pl.userlinetype = "games";
                    }
                    foreach (PushBallTeam pbt in pushBallTeams)
                        foreach (PushBallTeam.Pos goalPos in pbt.goalPositions)
                            level.Blockchange(goalPos.x, goalPos.y, goalPos.z, Block.air);
                    while (level.pushBallStarted) { Push(); }
                }
            }; countDown.Start();
        }

        public void End(PushBallTeam winningTeam) // Player p is the "cause" of the end (winner, manual ender, etc).
        {
            level.pushBallStarted = false;
            if (level.countdown == true) { level.countdown = false; countDown.Stop(); }
            foreach (Player pl in level.players)
            {
                if (pl.hidden && !pl.referee) { Command.all.Find("hide").Use(pl, "s"); }
                if (pl.invincible && !pl.referee) { Command.all.Find("invincible").Use(pl, ""); }
                if (pl.isFlying && !pl.referee) { Command.all.Find("fly").Use(pl, ""); }
                pl.userlinetype = "blockinfo";
            }
            if (winningTeam != null)
            {
                SpawnBall();
                Player.GlobalMessageLevel(level, winningTeam.color + winningTeam.teamstring + " &ehas won the PushBall game, with " + winningTeam.points + " points!");
                Player mvp = null; var mvpGoals = 0;
                foreach (Player p in level.players)
                {
                    if (p.pushBallGoals > mvpGoals) mvpGoals = p.pushBallGoals; mvp = p;
                    p.pushBallGoals = 0;
                }
                if (mvp != null) { if (mvp.pushBallGoals == mvpGoals) { Player.GlobalMessageLevel(level, "&eThis game's MVP was: " + mvp.color + mvp.prefix + mvp.name + "&e!"); } }
                foreach (PushBallTeam pbt in pushBallTeams) pbt.points = 0;
            }
            else { Player.GlobalMessageLevel(level, "The game was ended manually, so no winner was decided."); }
        }

        public Pos currentBallPos = new Pos();
        public void Push()
        {
            if (level.pushBallStarted)
            {
                try
                {
                    UpdateBallPos();
                    foreach (Player p in level.players) 
                        MoveBall(p);
                    foreach (PushBallTeam pbt in pushBallTeams)
                        foreach (PushBallTeam.Pos goal in pbt.goalPositions)
                            if (goal.x == currentBallPos.x && goal.y == currentBallPos.y && goal.z == currentBallPos.z)
                            {
                                ScorePoint(lastTouched, pbt);
                                break;
                            }
                    Thread.Sleep(100);
                }
                catch (Exception ex) { Server.ErrorLog(ex); }
            }
        }

        public void SpawnBall()
        {
            ushort x, y, z;
            for (x = 0; x <= level.width; x++)
                for (y = 0; y <= level.width; y++)
                    for (z = 0; z <= level.width; z++)
                        if (level.GetTile(x, y, z) == Block.pushball)
                            level.Blockchange(x, y, z, Block.air);
            level.Blockchange(ballSpawn.x, ballSpawn.y, ballSpawn.z, Block.pushball);
            UpdateBallPos();
        }

        public void UpdateBallPos()
        {
            /*Thread t = new Thread(new ThreadStart(delegate
            {
                try
                {*/
            ushort x, y, z;
            for (x = 0; x <= level.width; x++)
                for (y = 0; y <= level.width; y++)
                    for (z = 0; z <= level.width; z++)
                        if (level.GetTile(x, y, z) == Block.pushball)
                        {
                            Pos p = new Pos();
                            p.x = x; p.y = y; p.z = z;
                            currentBallPos = p;
                            return;
                        }
            /*}
            catch (Exception e) { Server.ErrorLog(e); }
        })); t.IsBackground = true; t.Priority = ThreadPriority.Lowest; t.Start();*/
        }

        public void MoveBall(Player p)
        {
            if (p.referee || p.pushBallTeam == null) return;
            UpdateBallPos();
            bool moved = false;
            if (Block.Walkthrough(level.GetTile((ushort)(currentBallPos.x + 2), currentBallPos.x, currentBallPos.z)) && currentBallPos.x == (ushort)((p.pos[0] / 32) + 1) && currentBallPos.y == (ushort)(p.pos[1] / 32) && currentBallPos.z == (ushort)(p.pos[2] / 32))
            {
                level.Blockchange((ushort)(currentBallPos.x + 1), currentBallPos.y, currentBallPos.z, Block.air);
                level.Blockchange((ushort)(currentBallPos.x + 2), currentBallPos.y, currentBallPos.z, Block.pushball);
                lastTouched = p;
                moved = true;
            }
            if (Block.Walkthrough(level.GetTile(currentBallPos.x, currentBallPos.y, (ushort)(currentBallPos.z + 2))) && currentBallPos.x == (ushort)(p.pos[0] / 32) && currentBallPos.y == (ushort)(p.pos[1] / 32) && currentBallPos.z == (ushort)((p.pos[0] / 32) + 1))
            {
                level.Blockchange(currentBallPos.x, currentBallPos.y, (ushort)(currentBallPos.z + 1), Block.air);
                level.Blockchange(currentBallPos.x, currentBallPos.y, (ushort)(currentBallPos.z + 2), Block.pushball);
                lastTouched = p;
                moved = true;
            }
            if (Block.Walkthrough(level.GetTile((ushort)(currentBallPos.x - 2), currentBallPos.x, currentBallPos.z)) && currentBallPos.x == (ushort)((p.pos[0] / 32) - 1) && currentBallPos.y == (ushort)(p.pos[1] / 32) && currentBallPos.z == (ushort)(p.pos[2] / 32))
            {
                level.Blockchange((ushort)(currentBallPos.x - 1), currentBallPos.y, currentBallPos.z, Block.air);
                level.Blockchange((ushort)(currentBallPos.x - 2), currentBallPos.y, currentBallPos.z, Block.pushball);
                lastTouched = p;
                moved = true;
            }
            if (Block.Walkthrough(level.GetTile(currentBallPos.x, currentBallPos.y, (ushort)(currentBallPos.z - 2))) && currentBallPos.x == (ushort)(p.pos[0] / 32) && currentBallPos.y == (ushort)(p.pos[1] / 32) && currentBallPos.z == (ushort)((p.pos[0] / 32) - 1))
            {
                level.Blockchange(currentBallPos.x, currentBallPos.y, (ushort)(currentBallPos.z - 1), Block.air);
                level.Blockchange(currentBallPos.x, currentBallPos.y, (ushort)(currentBallPos.z - 2), Block.pushball);
                lastTouched = p;
                moved = true;
            }
            if (moved) Player.GlobalMessage(p.name + " moved the ball"); // debug
            UpdateBallPos();
            if (Block.Walkthrough(level.GetTile(currentBallPos.x, (ushort)(currentBallPos.y - 1), currentBallPos.z)))
            {
                level.Blockchange(currentBallPos.x, currentBallPos.y, currentBallPos.z, Block.air);
                level.Blockchange(currentBallPos.x, (ushort)(currentBallPos.y - 1), currentBallPos.z, Block.pushball);
            }
            UpdateBallPos();
        }

        public void ScorePoint(Player p, PushBallTeam scoredOn)
        {
            p.pushBallGoals++;
            p.pushBallTeam.points++;
            SpawnBall();
            if (p.pushBallTeam != scoredOn) Player.GlobalMessageLevel(level, p.color + p.prefix + p.name + "(" + p.pushBallTeam.teamstring + ") &escored a point on the " + scoredOn.teamstring + "&e!");
            else Player.GlobalMessageLevel(level, p.color + p.prefix + p.name + "(" + p.pushBallTeam.teamstring + ") &escored on his own goal!");
            if (scoredOn.points >= winPoints) { End(scoredOn); }
        }

        public void AddTeam(string color)
        {
            char teamCol = (char)color[1];

            PushBallTeam workteam = new PushBallTeam();

            workteam.color = teamCol;
            workteam.points = 0;
            workteam.level = level;
            char[] temp = c.Name("&" + teamCol).ToCharArray();
            temp[0] = char.ToUpper(temp[0]);
            string tempstring = new string(temp);
            workteam.teamstring = "&" + teamCol + tempstring + " team&g";

            pushBallTeams.Add(workteam);

            level.ChatLevel(workteam.teamstring + "&e has been added to the PushBall game!");
        }

        public void RemoveTeam(string color)
        {
            char teamCol = (char)color[1];

            PushBallTeam workteam = pushBallTeams.Find(team => team.color == teamCol);
            List<Player> storedP = new List<Player>();

            for (int i = 0; i < workteam.players.Count; i++)
                storedP.Add(workteam.players[i]);
            foreach (Player p in storedP)
                workteam.RemoveMember(p);
        }

        /*public static int getDirection(byte rotx) // raw bytes (256 scale)
        {
            if (rotx >= 32 && rotx <= 95) // add to x coord
                return 0;
            else if (rotx >= 96 && rotx <= 159) // add to the z coord
                return 1;
            else if (rotx >= 160 && rotx <= 223) // subtract the x coord
                return 2;
            else if ((rotx >= 224 && rotx <= 255) || (rotx >= 0 && rotx <= 31)) // subtract the z coord
                return 3;
            else // idk, lolz.
                return 3;
        }

        public static int getDirection(byte rotx) // raw bytes (256 scale)
        {
            if (rotx >= 160 && rotx <= 223) // add to x coord
                return 0;
            else if ((rotx >= 224 && rotx <= 255) || (rotx >= 0 && rotx <= 31)) // add to the z coord
                return 1;
            else if (rotx >= 32 && rotx <= 95) // subtract the x coord
                return 2;
            else if (rotx >= 96 && rotx <= 159) // subtract the z coord
                return 3;
            else // idk, lolz.
                return 3;
        }*/

        public struct Pos { public ushort x, y, z; }
    }
}