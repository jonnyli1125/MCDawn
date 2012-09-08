/*-----------------------------------------------------------------------------------------------------
* This command was made for use with MCDawn, by Jonny Li, also known as jonnyli1125
* Friday, July 11, 2012, Version 1.0
* Licensed under a Creative Commons Attribution-NonCommercial-NoDerivs 3.0 Unported License.
* http://creativecommons.org/licenses/by-nc-nd/3.0/
* Any of the conditions stated in the license can be waived if you get written permission from the copyright holder.  
* ----------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace MCDawn
{
    public class Infection
    {
        public Level level;
        public bool powerpill = false;
        public int count = 0;
        System.Timers.Timer countDown;
        public void Start()
        {
            if (level.players.Count < 3) { End(); return; }

            level.countdown = true;
            level.timeLeft = 10;
            countDown = new System.Timers.Timer(1000);
            countDown.Elapsed += delegate
            {
                if (level.timeLeft >= 10)
                {
                    Player.GlobalMessageLevel(level, "&4INFECTION IN 10 SECONDS!! RUN!!");
                    level.players.ForEach(delegate(Player pl) { Command.all.Find("spawn").Use(pl, ""); });
                    level.timeLeft--;
                }
                else if (level.timeLeft <= 9 && level.timeLeft >= 1)
                {
                    Player.GlobalMessageLevel(level, "&4" + level.timeLeft);
                    level.timeLeft--;
                }
                else if (level.timeLeft <= 0)
                {
                    countDown.Stop();
                    //countDown.Dispose();
                    level.countdown = false;
                    level.zombiegame = true;
                    foreach (Player pl in level.players)
                    {
                        if (pl.hidden && !pl.referee) { Command.all.Find("hide").Use(pl, "s"); }
                        if (pl.invincible && !pl.referee) { Command.all.Find("invincible").Use(pl, ""); }
                        if (pl.isFlying && !pl.referee) { Command.all.Find("fly").Use(pl, ""); }
                        pl.userlinetype = "games";
                        pl.killstreak = 0;
                    }
                    if (level.queuename != "" && Player.Find(level.queuename) != null)
                    {
                        Player queue = Player.Find(level.queuename);
                        Player.GlobalMessageLevel(level, queue.color + queue.name + " &4has been INFECTED!!");
                        ToInfected(queue);
                        level.queuename = "";
                    }
                    else
                    {
                        Player firstzomb = level.humans[new Random().Next(level.humans.Count)];
                        Player.GlobalMessageLevel(level, firstzomb.color + firstzomb.name + " &4has been INFECTED!!");
                        ToInfected(firstzomb);
                    }
                    Thread.Sleep(3000);
                    while (level.zombiegame) { Infect(); }
                }
            }; countDown.Start();
        }

        public void Start(bool Powerpill)
        {
            if (!Powerpill) { Start(); return; }
            if (level.players.Count < 3) { End(); return; }

            powerpill = true;
            foreach (Player p in level.players) { if (!p.infected) { ToInfected(p); } }

            Player.GlobalMessageLevel(level, "&9Powerpill activated!");
            Player.GlobalMessageLevel(level, "&4Humans found the cure for Zombies!");
            Player.GlobalMessageLevel(level, "&4Zombies Run!");
            Thread.Sleep(500);

            level.countdown = true;
            level.timeLeft = 10;
            countDown = new System.Timers.Timer(1000);
            countDown.Elapsed += delegate
            {
                if (level.timeLeft >= 10)
                {
                    Player.GlobalMessageLevel(level, "&4INFECTION CURE TO BE FOUND IN 10 SECONDS!! RUN!!");
                    level.players.ForEach(delegate(Player pl) { Command.all.Find("spawn").Use(pl, ""); });
                    level.timeLeft--;
                }
                else if (level.timeLeft <= 9 && level.timeLeft >= 1)
                {
                    Player.GlobalMessageLevel(level, "&4" + level.timeLeft);
                    level.timeLeft--;
                }
                else if (level.timeLeft <= 0)
                {
                    countDown.Stop();
                    //countDown.Dispose();
                    level.countdown = false;
                    level.zombiegame = true;
                    foreach (Player pl in level.players)
                    {
                        if (pl.hidden && !pl.referee) { Command.all.Find("hide").Use(pl, "s"); }
                        if (pl.invincible && !pl.referee) { Command.all.Find("invincible").Use(pl, ""); }
                        if (pl.isFlying && !pl.referee) { Command.all.Find("fly").Use(pl, ""); }
                        pl.userlinetype = "games";
                        pl.killstreak = 0;
                    }
                    if (level.queuename != "" & Player.Find(level.queuename) != null)
                    {
                        Player queue = Player.Find(level.queuename);
                        Player.GlobalMessageLevel(level, queue.color + queue.name + " &2has been CURED!!");
                        ToHuman(queue);
                        level.queuename = "";
                    }
                    else
                    {
                        Player firstzomb = level.zombies[new Random().Next(level.zombies.Count)];
                        Player.GlobalMessageLevel(level, firstzomb.color + firstzomb.name + " &2has been CURED!!");
                        ToHuman(firstzomb);
                    }
                    Thread.Sleep(3000);
                    while (level.zombiegame) { Infect(); }
                }
            }; countDown.Start();
        }

        public void Powerpill()
        {
            if (level.zombiegame) // Mid-Game switch
            {
                if (powerpill == true)
                {
                    Player.GlobalMessageLevel(level, "&9Powerpill activated!");
                    Player.GlobalMessageLevel(level, "&4Humans found the cure for Zombies!");
                    Player.GlobalMessageLevel(level, "&4Zombies Run!");
                }
                else
                {
                    Player.GlobalMessageLevel(level, "&cPowerpill deactivated!");
                    Player.GlobalMessageLevel(level, "&4Zombies can start nomming on humans now!");
                }
            }
            else { Start(true); }
        }

        public void End() // Player p is the "cause" of the end (winner, manual ender, etc).
        {
            level.zombiegame = false;
            //foreach (Player pl in Player.players) { if (!pl.spleefAlive) { pl.spleefAlive = true; }
            if (level.countdown == true) { level.countdown = false; countDown.Stop(); }
            foreach (Player pl in level.players)
            {
                if (pl.hidden && !pl.referee) { Command.all.Find("hide").Use(pl, "s"); }
                if (pl.invincible && !pl.referee) { Command.all.Find("invincible").Use(pl, ""); }
                if (pl.isFlying && !pl.referee) { Command.all.Find("fly").Use(pl, ""); }
                pl.userlinetype = "blockinfo";
                pl.killstreak = 0;
            }
            if (powerpill)
            {
                Player.GlobalMessageLevel(level, "&4THE INFECTION ROUND HAS FINISHED!");
                //Thread.Sleep(3000);
                Player.GlobalMessageLevel(level, "&cZombies survived from cure:");
                foreach (Player p in level.zombies)
                    Player.GlobalMessageLevel(level, p.color + p.originalName);
            }
            else
            {
                Player.GlobalMessageLevel(level, "&4THE INFECTION ROUND HAS FINISHED!");
                //Thread.Sleep(3000);
                Player.GlobalMessageLevel(level, "&9Humans survived from infection:");
                foreach (Player p in level.humans)
                    Player.GlobalMessageLevel(level, p.color + p.originalName);
            }
            foreach (Player pl in level.players) ToHuman(pl);
        }

        public void Infect()
        {
            if (level.zombiegame)
            {
                try
                {
                    level.zombies.ForEach(delegate(Player zombie)
                    {
                        level.humans.ForEach(delegate(Player human)
                        {
                            if ((ushort)(zombie.pos[0] / 32) == (ushort)(human.pos[0] / 32) || (ushort)(zombie.pos[0] / 32 + 1) == (ushort)(human.pos[0] / 32) || (ushort)(zombie.pos[0] / 32 - 1) == (ushort)(human.pos[0] / 32))
                            {
                                //Player.GlobalMessage("x");
                                if ((ushort)(zombie.pos[1] / 32) == (ushort)(human.pos[1] / 32) || (ushort)(zombie.pos[1] / 32 + 1) == (ushort)(human.pos[1] / 32) || (ushort)(zombie.pos[1] / 32 - 1) == (ushort)(human.pos[1] / 32))
                                {
                                    //Player.GlobalMessage("y");
                                    if ((ushort)(zombie.pos[2] / 32) == (ushort)(human.pos[2] / 32) || (ushort)(zombie.pos[2] / 32 + 1) == (ushort)(human.pos[2] / 32) || (ushort)(zombie.pos[2] / 32 - 1) == (ushort)(human.pos[2] / 32))
                                    {
                                        //Player.GlobalMessage("z");
                                        if (!powerpill)
                                            Kill(zombie, human);
                                        else
                                            Kill(human, zombie);
                                        Thread.Sleep(750);
                                    }
                                }
                            }
                        });
                    });
                    Thread.Sleep(750);
                }
                catch (Exception ex) { Server.ErrorLog(ex); }
            }
        }

        public void Check() 
        {
            if (powerpill) { if (level.zombies.Count <= 1) { End(); } }
            else { if (level.humans.Count <= 1) { End(); } }
        }

        public void ToInfected(Player p)
        {
            if (p.infected) { return; }
            p.infected = true;
            Player.GlobalDie(p, false);
            Player.SkinChange(p, p.color + "__Undead__", p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], "");
            p.UpdateDetail();
        }

        public void ToHuman(Player p)
        {
            if (!p.infected) { return; }
            p.infected = false;
            Player.GlobalDie(p, false);
            Player.GlobalSpawn(p, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], false);
            p.UpdateDetail();
        }

        public void Kill(Player attacker, Player victim)
        {
            Announce(attacker, victim);
            if (powerpill)
                ToHuman(victim);
            else
                ToInfected(victim);
            Check();
            attacker.killstreak++;
        }

        public void Announce(Player attacker, Player victim)
        {
            if (powerpill)
                Player.GlobalMessageLevel(level, victim.color + victim.name + " &9was CURED by " + attacker.color + attacker.name);
            else
                Player.GlobalMessageLevel(level, victim.color + victim.name + " &cwas BITTEN by " + attacker.color + attacker.name);
            if (attacker.killstreak == 3 || attacker.killstreak == 5 || attacker.killstreak >= 7)
            { 
                Player.GlobalMessageLevel(level, attacker.color + attacker.name + " &4is on a killstreak of " + attacker.killstreak + "!");
                Player.SendMessage(attacker, "You're on a killstreak of &b" + attacker.killstreak + "&g!");
            }
        }
    }
}