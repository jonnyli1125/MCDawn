using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCDawn.Commands.GameControl
{
    abstract public class Game
    {
        public Level level;
        public string name;
        public List<Player> GameOPs = new List<Player>();

        protected bool inProgress;
        public bool IsInProgress()
        {
            return inProgress;
        }
        /// <summary>
        /// Sets parameter for game.
        /// </summary>
        /// <param name="param">Array of parameters without keyword "set".</param>
        /// <param name="check">Get or not any error, true - no error.</param>
        /// <param name="message">Message to show.</param>
        abstract public void SetParameters(string[] param, ref bool check, ref string[] message);
        /// <summary>
        /// Start choosen game.
        /// State of running will record into check and message.
        /// </summary>
        /// <param name="param">Array of parameters without keyword "set".</param>
        /// <param name="check">Show result, false - game not started.</param>
        /// <param name="message">Message to show.</param>
        abstract public void Start(string[] param, ref bool check, ref string[] message);
        /// <summary>
        /// Manual end of the game.
        /// </summary>
        abstract public void ForceEnd(string[] param);
        /// <summary>
        /// Stops and clean up all changes game made before, like changing names, colours, titles.
        /// </summary>
        abstract public void Dispose(string[] param);
        /// <summary>
        /// Get info of game, like scores, teams, deaths, and more.
        /// </summary>
        abstract public string[] GetCurrentInfo(string[] param);
        /// <summary>
        /// Return full help for choosen game, rules, parameters and so on.
        /// </summary>
        abstract public string[] GetHelp();
        /// <summary>
        /// Message to level at game start.
        /// </summary>
        abstract public string[] GetStartMessage();
        /// <summary>
        /// Message to level at manual game end.
        /// </summary>
        abstract public string[] GetForceEndMessage();
    }
}
