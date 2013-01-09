using System;
using System.Collections.Generic;

namespace MCDawn
{
    public class CmdPushBall : Command
    {
        public override string name { get { return "pushball"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "games"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdPushBall() { }

        public override void Use(Player p, string message)
        {
            if (p == null) { Player.SendMessage(p, "Command not usable in Console."); return; }
            if (message == "") { Help(p); return; }
            switch (message.Split(' ')[0].ToLower())
            {
                case "enable":
                    if (p.level.ctfmode) { Player.SendMessage(p, "CTF game is in progress."); return; }
                    if (p.level.spleefstarted) { Player.SendMessage(p, "Spleef game is in progress."); return; }
                    if (p.level.zombiegame) { Player.SendMessage(p, "Infection game is in progress."); return; }
                    if (p.level.pushBallEnabled) { Player.SendMessage(p, "PushBall is already enabled on this level. Use /pushball disable to disable."); return; }
                    p.level.pushBallEnabled = true;
                    p.level.ChatLevel("PushBall mode enabled.");
                    break;
                case "disable":
                    if (!p.level.pushBallEnabled) { Player.SendMessage(p, "PushBall has not been enabled yet on this level. Use /pushball enable to enable."); return; }
                    if (p.level.pushBallStarted) { Command.all.Find("pushball").Use(p, "end"); }
                    p.level.pushBallEnabled = false;
                    p.level.ChatLevel("PushBall mode disabled.");
                    break;
                case "start":
                    if (!p.level.pushBallEnabled) { Player.SendMessage(p, "Please enable PushBall mode with /pushball enable, first."); return; }
                    if (p.level.players.Count < 2) { Player.SendMessage(p, "Must have at least 2 players to play."); return; }
                    if (p.level.pushBall.pushBallTeams.Count < 2) { Player.SendMessage(p, "Must have at least 2 teams to play."); return; }
                    if (!p.level.pushBall.ballSpawnSet) { Player.SendMessage(p, "Set the ball spawn first with /pushball ballspawn."); return; }
                    p.level.pushBallStarted = true;
                    p.level.pushBall.Start();
                    break;
                case "stop": case "end":
                    if (!p.level.pushBallStarted) { Player.SendMessage(p, "PushBall game has not started."); return; }
                    p.level.pushBallStarted = false;
                    p.level.pushBall.End(null);
                    p.level.ChatLevel(p.color + p.name + " &ehas ended the PushBall game!");
                    break;
                case "ballspawn":
                    PushBall.Pos ballSpawn = new PushBall.Pos();
                    ballSpawn.x = (ushort)(p.pos[0] / 32);
                    ballSpawn.y = (ushort)(p.pos[1] / 32);
                    ballSpawn.z = (ushort)(p.pos[2] / 32);
                    p.level.pushBall.ballSpawn = ballSpawn;
                    p.level.pushBall.ballSpawnSet = true;
                    Player.SendMessage(p, "Ball spawn point has been set.");
                    break;
                case "points":
                    if (message.Split(' ').Length <= 1) { Help(p); return; }
                    try { p.level.pushBall.winPoints = int.Parse(message.Split(' ')[1]); }
                    catch { Player.SendMessage(p, "Max points value must be numeric."); return; }
                    Player.SendMessage(p, "Max points set to " + p.level.pushBall.winPoints + ".");
                    break;
                case "spawn":
                    if (c.Parse(message.Split(' ')[1]) == "") { Player.SendMessage(p, "Invalid team color chosen."); return; }
                    char teamCol = (char)c.Parse(message.Split(' ')[1])[1];
                    if (p.level.pushBall.pushBallTeams.Find(team => team.color == teamCol) == null) { Player.SendMessage(p, "Invalid team color chosen."); return; }
                    AddSpawn(p, c.Parse(message.Split(' ')[1]));
                    break;
                case "goal":
                    if (message.Split(' ').Length != 2) { Help(p); return; }
                    if (c.Parse(message.Split(' ')[1]) == "") { Player.SendMessage(p, "Invalid team color chosen."); return; }
                    switch ((char)c.Parse(message.Split(' ')[1])[1])
                    {
                        case '2':
                        case '5':
                        case '8':
                        case '9':
                        case 'c':
                        case 'e':
                        case 'f':
                            goalPosColor = c.Parse(message.Split(' ')[1]);
                            Player.SendMessage(p, "Start placing blocks to select your goal area.");
                            p.ClearBlockchange();
                            p.Blockchange += new Player.BlockchangeEventHandler(AddGoalPos);
                            break;
                        default:
                            Player.SendMessage(p, "Invalid team color chosen.");
                            return;
                    }
                    break;
                case "team":
                    string color = c.Parse(message.Split(' ')[2]);
                    if (color == "") { Player.SendMessage(p, "Invalid team color chosen."); return; }
                    switch ((char)color[1])
                    {
                        case '2':
                        case '5':
                        case '8':
                        case '9':
                        case 'c':
                        case 'e':
                        case 'f':
                            if (message.Split(' ')[1].ToLower() == "add") AddTeam(p, color);
                            else if (message.Split(' ')[1].ToLower() == "del") RemoveTeam(p, color);
                            else Help(p);
                            break;
                        default:
                            Player.SendMessage(p, "Invalid team color chosen.");
                            return;
                    }
                    break;
                case "clear":
                    List<PushBallTeam> storedT = new List<PushBallTeam>();
                    for (int i = 0; i < p.level.pushBall.pushBallTeams.Count; i++)
                    {
                        storedT.Add(p.level.pushBall.pushBallTeams[i]);
                    }
                    foreach (PushBallTeam t in storedT)
                    {
                        p.level.pushBall.RemoveTeam("&" + t.color);
                    }
                    //p.level.ctfgame.onTeamCheck.Stop();
                    //p.level.ctfgame.onTeamCheck.Dispose();
                    p.level.pushBallStarted = false;
                    p.level.pushBallEnabled = false;
                    p.level.pushBall = new PushBall();
                    p.level.pushBall.level = p.level;
                    Player.SendMessage(p, "PushBall data has been cleared.");
                    break;
                case "instructions":
                    Player.SendMultiple(p, new string[] { 
                        "&9How to play PushBall by MCDawn:",
                        "1. Enable PushBall mode with /pushball enable.",
                        "2. Add ball spawn point with /pushball ballspawn.",
                        "3. Set maxpoints if needed with /pushball points <number>. Default is 3.",
                        "4. Add at least 2 teams to play with with /pushball team add/del <color>.",
                        "5. Add spawns for each team with /pushball spawn <team>. There can be more than one spawn.",
                        "6. Add goal positions for each team with /pushball goal <team>.",
                        "7. Start the game with /pushball start, and play :D.",
                        "If you want to stop mid-game, you can use /pushball end, and then disabling PushBall mode with /pushball disable.",
                        "To clear all PushBall data from the level, use /pushball clear."
                    });
                    break;
                default: Help(p); return;
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/pushball enable - Enable PushBall on level. This is required to start/stop the game.");
            Player.SendMessage(p, "/pushball disable - Disable PushBall on level.");
            Player.SendMessage(p, "/pushball start - Start pushball game.");
            Player.SendMessage(p, "/pushball end - End pushball game.");
            Player.SendMessage(p, "/pushball ballspawn - Sets the spawn position of the ball at where you're standing.");
            Player.SendMessage(p, "/pushball points <maxpoints> - Set the number of points needed to win the round. Default is 3.");
            Player.SendMessage(p, "/pushball spawn <team> - Set spawn point of <team>.");
            Player.SendMessage(p, "/pushball goal <team> - Select area of goal for <team>.");
            Player.SendMessage(p, "/pushball team add <color> - Add a <color> team.");
            Player.SendMessage(p, "/pushball team del <color> - Delete <color> team.");
            Player.SendMessage(p, "/pushball clear - Clear all PushBall data from the level.");
            Player.SendMessage(p, "/pushball instructions - How to play!");
        }

        public void AddSpawn(Player p, string color)
        {
            char teamCol = (char)color[1];
            ushort x, y, z, rotx;
            x = (ushort)(p.pos[0] / 32);
            y = (ushort)(p.pos[1] / 32);
            z = (ushort)(p.pos[2] / 32);
            rotx = (ushort)(p.rot[0]);
            p.level.pushBall.pushBallTeams.Find(team => team.color == teamCol).AddSpawn(x, y, z, rotx, 0);
            Player.SendMessage(p, "Added spawn for " + p.level.pushBall.pushBallTeams.Find(team => team.color == teamCol).teamstring);
        }

        public void AddTeam(Player p, string color)
        {
            char teamCol = (char)color[1];
            if (p.level.pushBall.pushBallTeams.Find(team => team.color == teamCol) != null) { Player.SendMessage(p, "That team already exists."); return; }
            p.level.pushBall.AddTeam(color);
        }

        public void RemoveTeam(Player p, string color)
        {
            char teamCol = (char)color[1];
            if (p.level.pushBall.pushBallTeams.Find(team => team.color == teamCol) == null) { Player.SendMessage(p, "That team does not exist."); return; }
            p.level.pushBall.RemoveTeam(color);
        }

        public string goalPosColor = "";
        public List<PushBallTeam.Pos> goalBuffer = new List<PushBallTeam.Pos>();
        public void AddGoalPos(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            p.level.Blockchange(x, y, z, Block.air);
            if (type == Block.red && goalBuffer.Count > 0)
            {
                p.SendBlockchange(x, y, z, Block.air);
                foreach (PushBallTeam.Pos pp in goalBuffer)
                {
                    p.level.pushBall.pushBallTeams.Find(team => team.color == goalPosColor[1]).goalPositions.Add(pp);
                    p.SendBlockchange(pp.x, pp.y, pp.z, Block.air);
                }
                Player.SendMessage(p, "Goal positions selected for " + p.level.pushBall.pushBallTeams.Find(team => team.color == goalPosColor[1]).teamstring);
                goalBuffer.Clear();
                goalPosColor = "";
            }
            else
            {
                p.SendBlockchange(x, y, z, Block.green);
                PushBallTeam.Pos gp = new PushBallTeam.Pos();
                gp.x = x; gp.y = y; gp.z = z;
                goalBuffer.Add(gp);
                Player.SendMessage(p, "&aGoal position added. &cPlace a red block to finish.");
                p.Blockchange += new Player.BlockchangeEventHandler(AddGoalPos);
            }
        }
    }
}