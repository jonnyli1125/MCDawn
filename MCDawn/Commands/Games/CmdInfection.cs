//Coded by Gamemakergm
//Improved and continued by jonnyli1125

using System;
using System.IO;

namespace MCDawn
{
    public class CmdInfection : Command
    {
        public override string name { get { return "infection"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "games"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdInfection() { }
        public override void Use(Player p, string message)
        {
            if (p == null) { Player.SendMessage(p, "Command not usable in Console."); return; }
            switch (message.Split(' ')[0].ToLower())
            {
                case "":
                case "start":
                    if (p.level.players.Count < 3) { p.SendMessage("You must have at least 3 players on your level to play Infection!"); return; }
                    if (p.level.spleefstarted == true) { p.SendMessage("Cannot play Infection while playing spleef."); return; }
                    if (p.level.ctfmode == true) { p.SendMessage("Cannot play Infection while playing CTF."); return; }
                    if (p.level.zombiegame == true) { p.SendMessage("Infection game is already in progress!"); return; }
                    if (p.level.pushBallEnabled) { p.SendMessage("PushBall mode is currently enabled."); return; }
                    Player.GlobalMessage("&4**** " + p.color + p.name + " &4HAS STARTED THE INFECTION GAME!! ****");
                    p.level.infection.Start();
                    p.level.zombiegame = true;
                    Server.infectionGames++;
                    break;
                case "stop":
                    if (message.IndexOf(' ') != -1 && message.Split(' ')[1].ToLower() == "all")
                    {
                        if (Server.infectionGames == 0) { p.SendMessage("No Infection games are running."); return; }
                        Player.GlobalMessage("&4**** " + p.color + p.originalName + " HAS STOPPED ALL INFECTION GAMES!! ****");
                        Server.infectionGames = 0;
                        foreach (Level l in Server.levels) { if (l.zombiegame) { l.infection.End(); } }
                    }
                    else
                    {
                        if (Server.infectionGames == 0) { p.SendMessage("Infection game has not started yet."); return; }
                        Player.GlobalMessage("&4**** " + p.color + p.originalName + " HAS STOPPED THE INFECTION GAME!! ****");
                        Server.infectionGames--;
                        p.level.infection.End();
                    }
                    break;
                case "powerpill":
                    if (p.level.players.Count < 3) { p.SendMessage("You must have at least 3 players on your level to play Infection!"); return; }
                    if (p.level.spleefstarted == true) { p.SendMessage("Cannot play Infection while playing spleef."); return; }
                    if (p.level.ctfmode == true) { p.SendMessage("Cannot play Infection while playing CTF."); return; }
                    //if (p.level.zombiegame == true) { p.SendMessage("Infection game is already in progress!"); return; }
                    if (p.level.infection.powerpill != true) { p.level.infection.powerpill = true; }
                    else { p.level.infection.powerpill = false; }
                    if (p.level.zombiegame) { p.level.infection.Powerpill(); }
                    else { p.level.infection.Start(true); }
                    if (!p.level.zombiegame) { Player.GlobalMessage("&4**** " + p.color + p.name + " &4HAS STARTED THE INFECTION GAME!! ****"); p.level.zombiegame = true; Server.infectionGames++; }
                    break;
                default: Help(p); break;
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/infection - Play the Minecraft version of the popular zombie game, Infection!");
            Player.SendMessage(p, "/infection start - Start an Infection game on your level");
            Player.SendMessage(p, "/infection stop <all> - Stops an infection game on your level or stop all infection games.");
            Player.SendMessage(p, "/infection powerpill - Infection, but with a little bit of a twist. :D");
        }
}
}
