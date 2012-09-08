using System;
using System.Collections.Generic;

// Black Ops style gungame O.o
namespace MCDawn
{
    public class GunGame
    {
        public Level level;
        public List<string> guns = new List<string>(new string[] { "melee", "tnt", "bigtnt", "gun", "sniper", "rocket", "rpg", "airstrike", "nuke" });
        // start as melee, same code as biting player from infection somewhat
        // tnt is tnt
        // bigtnt is bigtnt
        // gun is just /gun
        // sniper is /gun laser, without explosions
        // rocket is rocketstart fired /gun style
        // rpg is /gun laser with explosions
        // airstrike is a copy pasted version of a plane, moving cross map, and den fire appearing below it on the closest ground block
        // nuke is just an explosion on where everyone is standing.
        public System.Timers.Timer countDown = new System.Timers.Timer();

        public void Start()
        {
            if (level.players.Count < 2) { End(null, 4); return; }

            //BuildPermission save
            LevelPermission savedLevelPerm = level.permissionbuild;
            level.permissionbuild = LevelPermission.Nobody;

            level.countdown = true;
            level.timeLeft = 10;
            countDown.Elapsed += delegate
            {
                if (level.timeLeft >= 10)
                {
                    Player.GlobalMessageLevel(level, "&c10 SECONDS REMAINING!!");
                    level.timeLeft--;
                }
                else if (level.timeLeft <= 9 && level.timeLeft >= 1)
                {
                    Player.GlobalMessageLevel(level, "&c" + level.timeLeft);
                    level.timeLeft--;
                }
                else if (level.timeLeft <= 0)
                {
                    countDown.Stop();
                    //countDown.Dispose();
                    level.countdown = false;
                    level.setPhysics(3);
                    level.permissionbuild = savedLevelPerm;
                    Player.GlobalMessageLevel(level, "&eGungame started. &cKill your opponents to upgrade your weapon.");
                    foreach (Player pl in level.players)
                    {
                        if (pl.hidden && !pl.referee && !Server.devs.Contains(pl.name.ToLower())) { Command.all.Find("hide").Use(pl, "s"); }
                        if (pl.invincible && !pl.referee && !Server.devs.Contains(pl.name.ToLower())) { Command.all.Find("invincible").Use(pl, ""); }
                        if (pl.isFlying && !pl.referee && !Server.devs.Contains(pl.name.ToLower())) { Command.all.Find("fly").Use(pl, ""); }
                    }
                }
            }; countDown.Start();
        }

        public void End(Player p, int style) // Player p is the "cause" of the end (winner, manual ender, etc).
        {
            level.gunGameActive = false;
            if (level.countdown == true) { level.countdown = false; countDown.Stop(); }
            foreach (Player pl in level.players)
            {
                if (pl.hidden && !pl.referee && !Server.devs.Contains(pl.name.ToLower())) { Command.all.Find("hide").Use(pl, "s"); }
                if (pl.invincible && !pl.referee && !Server.devs.Contains(pl.name.ToLower())) { Command.all.Find("invincible").Use(pl, ""); }
                if (pl.isFlying && !pl.referee && !Server.devs.Contains(pl.name.ToLower())) { Command.all.Find("fly").Use(pl, ""); }
            }
            switch (style)
            {
                case 1: // Manual Ending
                    Player.GlobalMessage(p.color + p.name + " &cHAS STOPPED THE GUN GAME!!");
                    break;
                case 2: // Player win
                    Player.GlobalMessage(p.color + p.name + " &cHAS WON THE GUN GAME!!");
                    break;
                case 3: // Restart Game
                    Start();
                    break;
                case 4: // Other (Leave blank, unless you need to put in something)
                    break;
            }
            Command.all.Find("restore").Use(p, "wipe");
        }
    }
}
