/*-----------------------------------------------------------------------------------------------------
This script/command was made for use with MCDawn.
This script/command was written by Jonny Li, also known as jonnyli1125
Sunday, April 8, 2012
Version 1.0
----------------------------------------------------------------------------------------------------
Licensed under a Creative Commons Attribution-NonCommercial-NoDerivs 3.0 Unported License.
http://creativecommons.org/licenses/by-nc-nd/3.0/

You must attribute the work in the manner specified by the author or licensor.
You may not use this work for commercial purposes. 
You may not alter, transform, or build upon this work.
 
Any of the above conditions can be waived if you get written permission from the copyright holder.  
----------------------------------------------------------------------------------------------------*/

using System;
using System.Threading;
using System.Collections;
using System.Net;
using System.IO;
using System.Timers;
using System.Collections.Generic;
using System.Text;

namespace MCDawn
{
    public class Spleef
    {
        public Level level;
        public int count = 0;
        System.Timers.Timer countDown = new System.Timers.Timer(1000);
        public void Start()
        {
            if (level.players.Count < 2) { End(null, 4); return; }

            //BuildPermission save
            LevelPermission savedLevelPerm = level.permissionbuild;
            level.permissionbuild = LevelPermission.Nobody;

            level.countdown = true;
            //level.setPhysics(Server.spleefPhysics);
            level.timeLeft = 10;
            countDown = new System.Timers.Timer(1000);
            countDown.Elapsed += delegate
            {
                if (level.timeLeft >= 10)
                {
                    Player.GlobalMessageLevel(level, "&b10 SECONDS REMAINING!!");
                    level.timeLeft--;
                }
                else if (level.timeLeft <= 9 && level.timeLeft >= 1)
                {
                    Player.GlobalMessageLevel(level, "&b" + level.timeLeft);
                    level.timeLeft--;
                }
                else if (level.timeLeft <= 0)
                {
                    countDown.Stop();
                    //countDown.Dispose();
                    level.countdown = false;
                    //level.setPhysics(0);
                    level.permissionbuild = savedLevelPerm;
                    Player.GlobalMessageLevel(level, "&bSTART THE SPLEEF!!");
                    foreach (Player pl in level.players)
                    {
                        if (pl.hidden && !pl.referee) { Command.all.Find("hide").Use(pl, "s"); }
                        if (pl.invincible && !pl.referee) { Command.all.Find("invincible").Use(pl, ""); }
                        if (pl.isFlying && !pl.referee) { Command.all.Find("fly").Use(pl, ""); }
                        if (!pl.spleefAlive) { pl.spleefAlive = true; }
                    }
                }
            }; countDown.Start();
        }
        public void End(Player p, int style) // Player p is the "cause" of the end (winner, manual ender, etc).
        {
            level.spleefstarted = false;
            //foreach (Player pl in Player.players) { if (!pl.spleefAlive) { pl.spleefAlive = true; }
            if (level.countdown == true) { level.countdown = false; countDown.Stop(); }
            foreach (Player pl in level.players)
            {
                if (pl.hidden && !pl.referee) { Command.all.Find("hide").Use(pl, "s"); }
                if (pl.invincible && !pl.referee) { Command.all.Find("invincible").Use(pl, ""); }
                if (pl.isFlying && !pl.referee) { Command.all.Find("fly").Use(pl, ""); }
            }
            switch (style)
            {
                case 1: // Manual Ending
                    Player.GlobalMessage(p.color + p.name + " &b**** HAS ENDED THE SPLEEF GAME!! ****");
                    break;
                case 2: // Player win
                    Player.GlobalMessage(p.color + p.name + " &b**** HAS WON THE SPLEEF GAME!! ****");
                    break;
            }
            Command.all.Find("restore").Use(p, "spleefbackup");
        }
        public void SendRules(Player p)
        {
            List<string> rules = new List<string>();
            if (!File.Exists("text/spleefrules.txt"))
            {
                File.WriteAllText("text/spleefrules.txt", "No Spleef Rules entered yet!");
            }
            StreamReader r = File.OpenText("text/spleefrules.txt");
            while (!r.EndOfStream)
                rules.Add(r.ReadLine());

            r.Close();
            r.Dispose();

            p.SendMessage("Spleef Rules:");
            foreach (string s in rules)
                p.SendMessage(s);
        }
    }
}