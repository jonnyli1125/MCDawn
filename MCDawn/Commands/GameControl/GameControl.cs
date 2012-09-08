using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCDawn.Commands.GameControl
{
    // Note:
    // New game needed to be added in GetGame(string gameName) and AvailableGames() methods.
    static class GameControl
    {
        public static string[] nullParameters = new string[] { };
        static List<Game> enabledGames = new List<Game>();

        public static string[] availableGames = new string[] { 
                "spleef"
            };

        /// <summary>
        /// Return game with the name of gameName.
        /// </summary>
        public static Game GetGame(string gameName, Level l)
        {
            switch (gameName)
            {
                case "spleef":
                    return new Games.Spleef(l, 5);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Add game to List enabledGames.
        /// </summary>
        public static void Add(Game g)
        {
            enabledGames.Add(g);
        }

        /// <summary>
        /// Dispose and delete game out of List enabledGames.
        /// </summary>
        public static void Delete(Level l)
        {
            Game g = FindGame(l);
            if (g.IsInProgress())
            {
                g.ForceEnd(nullParameters);
            }
            enabledGames.Remove(g);
        }

        /// <summary>
        /// Return true, if any game enabled on level l, false otherwise.
        /// </summary>
        public static bool IsEnabled(Level l)
        {
            foreach (Game g in enabledGames)
            {
                if (l.Equals(g.level))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Set parameters to choosen game.
        /// param - array of parameters without keyword "set".
        /// </summary>
        /*
        public static void Set(Level lev, string[] param, ref bool isCompleted, ref string[] message)
        {
            FindGame(lev).SetParameters(param, ref isCompleted, ref message);
        }
        */
        /// <summary>
        /// Return game of selected level, or null, if game isn't enabled.
        /// </summary>
        public static Game FindGame(Level level)
        {
            foreach (Game g in enabledGames)
            {
                if (g.level.Equals(level))
                {
                    return g;
                }
            }
            return null;
        }
    }
}
