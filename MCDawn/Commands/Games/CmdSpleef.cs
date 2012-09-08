// Written by jonnyli1125 for MCDawn

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
    public class CmdSpleef : Command
    {
        public override string name { get { return "spleef"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "games"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdSpleef() { }

        public override void Use(Player p, string message)
        {
            //if (message == "") { Help(p); return; }
            if (p == null) { Player.SendMessage(p, "Command not usable from Console!"); return; }

            if (p != null)
            {
                Player who;
                switch (message.ToLower())
                {
                    case "":
                    case "start":
                    case "s":
                        if (p == null) { p.SendMessage("Command not usable in Console."); return; }
                        if (p.level.zombiegame) { p.SendMessage("Cannot play Infection and Spleef at the same time!"); return; }
                        if (p.level.players.Count < 2) { p.SendMessage("Must have at least 2 players to play Spleef!"); return; }
                        if (p.level.spleefstarted) { p.SendMessage("Spleef game has already started."); return; }
                        if (p.level.pushBallEnabled) { p.SendMessage("PushBall mode is currently enabled."); return; }
                        //Start
                        p.SendMessage("Starting Spleef game...");
                        Thread.Sleep(500);
                        Command.all.Find("save").Use(p, p.level.name + " spleefbackup");
                        Player.GlobalMessage("SPLEEF GAME STARTING IN 10 SECONDS!!");
                        Player.GlobalMessage("TYPE &b/G " + p.level.name.ToUpper() + Server.DefaultColor + " TO JOIN!!");
                        p.level.spleef.Start();
                        p.level.spleefstarted = true;
                        break;
                    case "freeze":
                    case "f":
                        if (p == null) { p.SendMessage("Command not usable in Console."); return; }
                        if (p.level.spleefstarted == false) { p.SendMessage("Spleef has not started yet!"); return; }
                        if (!p.referee && !Server.devs.Contains(p.name.ToLower())) { p.SendMessage("Command for referees only."); return; }
                        Player.GlobalMessage(p.color + p.prefix + p.name + ": &fFREEZE!!");
                        Thread.Sleep(500);
                        foreach (Player pl in Player.players)
                        {
                            if (pl.name != p.name && pl.level == p.level)
                            {
                                if (pl.group.Permission < p.group.Permission && !Server.devs.Contains(pl.name.ToLower()) && !pl.referee) { Command.all.Find("freeze").Use(p, pl.name); }
                            }
                        }       
                        break;
                    case "end":
                    case "e":
                    case "stop":
                    case "reset":
                    case "restore":
                    case "wipe":
                    case "w":
                        if (p == null) { p.SendMessage("Command not usable in Console."); return; }
                        if (p.level.spleefstarted == false) { p.SendMessage("Spleef has not started yet!"); return; }
                        p.SendMessage("Ending Spleef game...");
                        Thread.Sleep(500);
                        p.level.spleef.End(p, 1);
                        Thread.Sleep(500);
                        Player.GlobalMessageLevel(p.level, "&bSpleef game has ended, the spleef mat has been reset.");
                        p.level.spleefstarted = false;
                        break;
                    /*case "hax":
                    case "h":
                        if (p.referee == false && !Server.devs.Contains(p.name.ToLower())) { Player.SendMessage(p, "Can't let you do that, Starfox."); return; }
                        if (p.spleefhaxused >= 1)
                        {
                            Player.SendMessage(p, "Abuse of HAAAXXXXX!!! No more for you!!");
                            return;
                        }
                        if (p.referee == true && !Server.devs.Contains(p.name.ToLower()))
                        {
                            Player.GlobalMessageLevel(p.level, "&bHAAAXXXXX INCOMING!!!");
                            foreach (Player pl in Player.players)
                            {
                                if (!Server.devs.Contains(pl.name.ToLower()) && pl.name != p.name && !pl.referee)
                                {
                                    pl.HandleDeath(Block.rock, " was killed by HAX!!", true);
                                }
                            }
                        }
                        if (!Server.devs.Contains(p.name.ToLower())) { p.spleefhaxused++; }
                        break;*/
                    case "rules":
                    case "r":
                        if (message.Split(' ').Length > 1)
                        {
                            string[] msg = message.Split(' ');
                            if (msg[1].ToLower() == "all" && p.referee == false) { Player.SendMessage(p, "Command for referee only."); return; }
                            if (msg[1].ToLower() == "all" && p.referee == true)
                            {
                                foreach (Player plr in Player.players)
                                {
                                    p.level.spleef.SendRules(plr);
                                }
                                return;
                            }
                            if (msg[1].ToLower() != "all") { Help(p); return; }
                            return;
                        }
                        who = Player.Find(message.Split(' ')[1]);
                        p.level.spleef.SendRules(who);
                        break;
                    case "players":
                    case "alive":
                    case "dead":
                    case "refs":
                        if (p == null) { p.SendMessage("Command not usable in Console."); return; }
                        if (p.level.spleefstarted == false) { p.SendMessage("Spleef has not started yet!"); return; }
                        Player.SendMessage(p, "Players on Spleef right now:");
                        string refs = "", alive = "", dead = "";
                        foreach (Player pl in p.level.players)
                        {
                            if (pl.referee) refs += pl.name + ", ";
                            else if (pl.spleefAlive) alive += pl.name + ", ";
                            else if (!pl.spleefAlive) dead += pl.name + ", ";
                        }
                        refs = refs.Remove(refs.Length - 2);
                        alive = alive.Remove(alive.Length - 2);
                        dead = dead.Remove(dead.Length - 2);
                        Player.SendMessage(p, "&9Refs: " + refs);
                        Player.SendMessage(p, "&aAlive: " + alive);
                        Player.SendMessage(p, "&cDead: " + dead);
                        break;
                    default: Help(p); break;
                }
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/spleef start - Does countdown and starts the game.");
            Player.SendMessage(p, "/spleef rules <all> - Read the rules for playing spleef.");
            Player.SendMessage(p, "/spleef freeze - Freeze all players on spleef, use again to unfreeze.");
            Player.SendMessage(p, "/spleef players - Shows you the status of all players playing spleef.");
            Player.SendMessage(p, "/spleef end - End the spleef game.");
            Player.SendMessage(p, "Use the first letter of each of the options for shortcuts.");
            Player.SendMessage(p, "HOW TO PLAY: ");
            Player.SendMessage(p, "This version of spleef is the traditional game of spleef, supported for 2+ players.");
            Player.SendMessage(p, "You create a layer of blocks, and you destroy the blocks under your opponent(s)");
            Player.SendMessage(p, "Normally, magma is placed underneath the blocks, so if you fall you die.");
            Player.SendMessage(p, "If you die, you are out, and you cannot play anymore. Last man standing wins.");
            Player.SendMessage(p, "The game can be refereed with /referee also.");
        }
    }
}