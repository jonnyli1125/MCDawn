using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCDawn.Commands.GameControl
{
    public class CmdGame : Command
    {
        public override string name { get { return "gamemaster"; } }
        public override string[] aliases { get { return new string[] { "gm" }; } }
        public override string type { get { return "games"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdGame() { }

        public override void Use(Player p, string message)
        {
            string[] param = message.Split(' ');
            for (int i = 0; i < param.Length; i++)
            {
                param[i] = param[i].ToLower();
            }

            switch (param.Length)
            {
                // /gm
                case 0:
                    this.Help(p);
                    break;
                // /gm <gamename>
                // /gm <start/end>
                // /gm info
                // /gm dispose
                // /gm leave
                case 1:
                    if (param[0] == "start")
                    {
                        StartGame(p, GameControl.nullParameters);
                    }
                    else if (param[0] == "end")
                    {
                        EndGame(p, GameControl.nullParameters);
                    }
                    else if (GameControl.availableGames.Contains(param[0]))
                    {
                        InitializeGame(p, param);
                    }
                    else if (param[0] == "info")
                    {
                        SendInfo(p, param);
                    }
                    else if (param[0] == "dispose")
                    {
                        EndAndDisposeGame(p);
                    }
                    else if (param[0] == "leave")
                    {
                        Leave(p, param);
                    }
                    else
                    {
                        p.SendMessage("Error, wrong input.");
                    }
                    break;
                // /gm help <game>
                // /gm set <parameter>
                // /gm <start/end> <parameter>
                case 2:
                    if (param[0] == "help")
                    {
                        GetHelp(p, param);
                    }
                    else if (param[0] == "set")
                    {
                        SetParam(p, param);
                    }
                    else if (param[0] == "start")
                    {
                        StartGame(p, GetSetParameters(param));
                    }
                    else if (param[0] == "end")
                    {
                        EndGame(p, GetSetParameters(param));
                    }
                    else
                    {
                        this.Help(p);
                    }
                    break;
                // /gm <start/end> <more than 1 parameter>
                // /gm set <more than 1 parameter>
                default:
                    if (param[0] == "start")
                    {
                        StartGame(p, GetSetParameters(param));
                    }
                    else if (param[0] == "end")
                    {
                        EndGame(p, GetSetParameters(param));
                    }
                    else
                    {
                        SetParam(p, param);
                    }
                    break;
            }

            CheckIsOp(p, param);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/gamemaster - Access to all games by one command!");
            Player.SendMessage(p, "/gamemaster <name> - initialize game on map you are currently on.");
            Player.SendMessage(p, "/gamemaster help <name> - general info and available parameters of choosen game.");
            Player.SendMessage(p, "/gamemaster set <parameters> - set parameters of game.");
            Player.SendMessage(p, "/gamemaster <start/end> [parameters] - begin or end game.");
            Player.SendMessage(p, "/gamemaster info - get info about choosen game.");
            Player.SendMessage(p, "/gamemaster dispose - dispose game on map you are currently on.");
            string s = "";
            foreach (string name in GameControl.availableGames)
            {
                s += " " + name;
            }
            Player.SendMessage(p, "Available games:" + s + ".");
        }


        #region Private methods
        private void CheckIsOp(Player p, string[] param)
        {
            if (GameControl.IsEnabled(p.level))
            {
                try
                {
                    if (!GameControl.FindGame(p.level).GameOPs.Contains(p) && param[0] != "leave")
                    {
                        GameControl.FindGame(p.level).GameOPs.Add(p);
                        SendToGameOPs(GameControl.FindGame(p.level), new string[] { (p.name + " added to gameOP list.") });
                    }
                }
                // no parameter
                catch
                {  }
            }
        }

        private void EndAndDisposeGame(Player p)
        {
            if (GameControl.IsEnabled(p.level))
            {
                GameControl.Delete(p.level);
                p.level.ChatLevel("Game disposed sucessefully.");
            }
            else
            {
                p.SendMessage("Error, game isn't enabled on level.");
            }
        }
        private void StartGame(Player p, string[] param)
        {
            if (GameControl.IsEnabled(p.level))
            {
                if (GameControl.FindGame(p.level).IsInProgress())
                {
                    p.SendMessage("Error, game already started.");
                }
                else
                {
                    bool isCompleted = false;
                    string[] message = new string[0];
                    SendToLevel(p.level, GameControl.FindGame(p.level).GetStartMessage());
                    GameControl.FindGame(p.level).Start(GetSetParameters(param), ref isCompleted, ref message);
                }
            }
            else
            {
                p.SendMessage("Error, no initialized game found.");
            }
        }
        private void EndGame(Player p, string[] param)
        {
            if (GameControl.IsEnabled(p.level))
            {
                if (GameControl.FindGame(p.level).IsInProgress())
                {
                    GameControl.FindGame(p.level).ForceEnd(GetSetParameters(param));
                    // Message, if game was ended manually
                    SendToLevel(p.level, GameControl.FindGame(p.level).GetForceEndMessage());
                }
                else
                {
                    p.SendMessage("Error, game isn't started.");
                }
            }
            else
            {
                p.SendMessage("Error, game isn't enabled on level.");
            }
        }
        private void InitializeGame(Player p, string[] param)
        {
            if (p.level == null)
            {
                p.SendMessage("Error, can't use /gamemaster via console.");
                return;
            }
            else
            {
                // param[0] - name of game
                if (GameControl.availableGames.Contains(param[0]))
                {
                    if (GameControl.IsEnabled(p.level))
                    {
                        p.SendMessage("Error, game " + GameControl.FindGame(p.level).name + " already initialized.");
                        return;
                    }
                    else
                    {
                        GameControl.Add(GameControl.GetGame(param[0], p.level));
                        p.level.ChatLevel("Game " + param[0] + " initialized.");
                    }
                }
                else
                {
                    p.SendMessage("Error, game name " + param[0] + " is invalid.");
                    return;
                }
            }
        }
        private void SendInfo(Player p, string[] param)
        {
            if (GameControl.IsEnabled(p.level))
            {
                string[] sa = GameControl.FindGame(p.level).GetCurrentInfo(GetSetParameters(param));
                foreach (string s in sa)
                {
                    p.level.ChatLevel(s);
                }
            }
            else
            {
                p.SendMessage("Error, game isn't enabled on level.");
            }
        }
        private void GetHelp(Player p, string[] param)
        {
            if (GameControl.availableGames.Contains(param[1]))
            {
                string[] help = GameControl.GetGame(param[1], nullLevel).GetHelp();
                for (int i = 0; i < help.Length; i++)
                {
                    p.SendMessage(help[i]);
                }
            }
            else
            {
                p.SendMessage("Error, invalid game name.");
            }
        }
        private void SetParam(Player p, string[] param)
        {
            if (GameControl.IsEnabled(p.level))
            {
                bool isCompleted = false;
                string[] message = new string[0];
                GameControl.FindGame(p.level).SetParameters(GetSetParameters(param), ref isCompleted, ref message);
                if (isCompleted)
                {
                    SendToGameOPs(GameControl.FindGame(p.level), message);
                }
                else
                {
                    SendToPlayer(p, message);
                }
            }
            else
            {
                p.SendMessage("Error, game isn't enabled on level.");
            }
        }
        private void Leave(Player p, string[] param)
        {
            if (GameControl.FindGame(p.level).GameOPs.Contains(p))
            {
                GameControl.FindGame(p.level).GameOPs.Remove(p);
                SendToGameOPs(GameControl.FindGame(p.level), new string[] { (p.name + " is no more gameOP.") });
                if (GameControl.FindGame(p.level).GameOPs.Count == 0)
                {
                    GameControl.Delete(p.level);
                    p.level.ChatLevel("Game disposed.");
                }
            }
            else
            {
                p.SendMessage("Error, you are not in gameOP list.");
            }
        }
        //Delete first keyword
        private string[] GetSetParameters(string[] param)
        {
            if (param.Length > 0)
            {
                string[] setParams = new string[param.Length - 1];
                for (int i = 0; i < param.Length - 1; i++)
                {
                    setParams[i] = param[i + 1];
                }
                return setParams;
            }
            return param;
        }

        private void SendToLevel(Level l, string[] message)
        {
            for (int i = 0; i < message.Length; i++)
            {
                l.ChatLevel(message[i]);
            }
        }
        private void SendToGameOPs(Game g, string[] message)
        {
            for (int i = 0; i < message.Length; i++)
            {
                foreach (Player p in g.GameOPs)
                {
                    p.SendMessage(message[i]);
                }
            }
        }
        private void SendToPlayer(Player p, string[] message)
        {
            for (int i = 0; i < message.Length; i++)
            {
                p.SendMessage(message[i]);
            }
        }

        private const Level nullLevel = null;
        #endregion
    }
}