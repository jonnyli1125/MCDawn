using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MCDawn.Commands.GameControl.Games
{
    public class Spleef : Game
    {
        private double timeForRound;
        private double currentTime;
        private List<Player> playersAlive = new List<Player>();
        private int repeat = 0;
        
        public Spleef(Level l, double timeMinutes)
        {
            name = "Spleef";
            level = l;
            timeForRound = timeMinutes;
        }
        public override void Start(string[] param, ref bool check, ref string[] message) 
        {
            repeat++;
            bool minuteMessageShowed = false;
            while (repeat > 0)
            {
                currentTime = timeForRound;

                CreatePlayField(param);

                inProgress = true;

                Player.OnPlayerDeathEvent += new Player.OnPlayerDeathEventHandler(OnPlayerDeath);

                // minutes to secs
                currentTime *= 60;

                foreach (Player p in level.players)
                {
                    playersAlive.Add(p);
                }

                while (inProgress)
                {
                    CheckForAmountOfPlayersLeft();

                    CheckForLeaversFromMap();

                    Thread.Sleep(125);
                    currentTime -= 0.125;
                    if (!minuteMessageShowed)
                    {
                        CheckIfMinuteLeft(currentTime);
                        minuteMessageShowed = true;
                    }
                }
                
                repeat--;
                if (repeat > 0)
                {
                    level.ChatLevel("Repeating...");
                    string[] startMes = GetStartMessage();
                    level.ChatLevel(repeat.ToString() + " times left");
                    for (int i = 0; i < startMes.Length; i++)
                    {
                        level.ChatLevel(startMes[i]);
                    }
                    minuteMessageShowed = false;
                }

                playersAlive.Clear();
                Player.OnPlayerDeathEvent -= new Player.OnPlayerDeathEventHandler(OnPlayerDeath);
            }
        }
        public override void ForceEnd(string[] param) 
        {
            level.ChatLevel("Force end!");
            playersAlive.Clear();
            inProgress = false;
        }
        public override void Dispose(string[] param) 
        {
            ForceEnd(param);
        }
        public override void SetParameters(string[] param, ref bool success, ref string[] message)
        {
            if (param[0] == "time")
            {
                try
                {
                    param[1] = param[1].Replace('.', ',');
                    double time = double.Parse(param[1]);
                    timeForRound = time;
                    message = new string[] { ("Time sucessefully setted to " + time + " minutes.") };
                    success = true;
                }
                catch
                {
                    message = new string[] { ("Error, invalid time.") };
                    success = false;
                }
            }
            else if (param[0] == "repeat")
            {
                try
                {
                    repeat = int.Parse(param[1]);
                    message = new string[] { ("Game will repeat " + repeat + " times.") };
                    success = true;
                }
                catch
                {
                    message = new string[] { ("Error, invalid repeat amount.") };
                    success = false;
                }
            }
            else
            {
                message = new string[] { ("Error, invalid parameters.") };
                success = false;
            }
        }
        public override string[] GetCurrentInfo(string[] param) 
        {
            if (inProgress)
            {
                List<string> s = new List<string>();
                s.Add("Currently playing: ");
                string[] temp = GetListOfPlayers();
                for (int i = 0; i < temp.Length; i++)
                {
                    s.Add(temp[i]);
                }
                s.Add("Time left: " + currentTime.ToString());
                return s.ToArray();
            }
            else
            {
                return new string[] {
                    ("Time: " + timeForRound + " minutes."),
                    ("Repeat " + repeat + " times.")
                };
            }
        }
        public override string[] GetHelp() 
        {
            return new string[] 
            {
                "Spleef game - easy and known game!",
                "Available set parameters:",
                "time <amount> - sets timef for game round in minutes;",
                "repeat <number> - repeats game [number] time;"
            };
        }
        public override string[] GetStartMessage() 
        {
            return new string[] { "Spleef begin!" };
        }
        public override string[] GetForceEndMessage() 
        {
            return new string[] { "Spleef was force ended." };
        }

        #region private methods
        private void OnPlayerDeath(Player p, byte b, string message, bool explode)
        {
            if (playersAlive.Contains(p))
            {
                playersAlive.Remove(p);
                message = "Player " + p.name + " died!";
            }
        }
        private void CheckForLeaversFromMap()
        {
            for (int i = 0; i < playersAlive.Count; i++)
            {
                if (!playersAlive[i].level.Equals(level))
                {
                    level.ChatLevel(playersAlive[i] + " left the game.");
                    playersAlive.RemoveAt(i);
                    i--;
                }
            }
        }
        private void CheckForAmountOfPlayersLeft()
        {
            if (playersAlive.Count < 2)
            {
                if (playersAlive.Count == 1)
                {
                    level.ChatLevel("Spleef ended! The winner is " + playersAlive[0].name + ".");
                }
                else if (playersAlive.Count < 1)
                {
                    level.ChatLevel("Spleef ended with draft!");
                }
                inProgress = false;
            }
        }
        private void CheckIfMinuteLeft(double time)
        {
            if (time <= 60)
            {
                level.ChatLevel("Minute left!");
            }
        }
        private void CreatePlayField(string[] param)
        {
            if (param.Length == 0)
            {
                for (ushort x = 0; x < level.width; x++)
                {
                    for (ushort y = 0; y < level.depth; y++)
                    {
                        level.Blockchange(x, (ushort)(level.height / 2), y, Block.glass);
                        level.Blockchange(x, (ushort)(level.height / 2 - 1), y, Block.activedeathlava);
                    }
                }
            }
        }
        private string[] GetListOfPlayers()
        {
            int k = 0;
            string[] ret = new string[(playersAlive.Count / 3) + ((playersAlive.Count % 3) / 2) + ((playersAlive.Count % 3) % 2)];
            int groupParam = playersAlive.Count / 3;
            if (groupParam == 0) { groupParam = 1; }
            for (int i = 0; i < playersAlive.Count; i++, k++)
            {
                ret[k % groupParam] += playersAlive[i].name + " ";
            }
            return ret;
        }
        #endregion
    }
}
