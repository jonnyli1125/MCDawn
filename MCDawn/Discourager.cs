using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace MCDawn
{
    public static class Discourager
    {
        public static List<string> discouraged = new List<string>();
        public static string filepath = "text/discouraged.txt";

        public static void LoadDiscouraged()
        {
            if (!File.Exists(filepath)) { File.Create(filepath).Close(); }
            if (!String.IsNullOrEmpty(File.ReadAllText(filepath)) && File.Exists(filepath))
            {
                discouraged = new List<string>(File.ReadAllLines(filepath));
                discouraged.ForEach(delegate(string s) { if (String.IsNullOrEmpty(s.Trim())) { discouraged.Remove(s); } });
                //Server.s.Log("Discouraged players list loaded.");
            }
        }

        public static void SaveDiscouraged()
        {
            if (!File.Exists("text/discouraged.txt")) { File.Create("text/discouraged.txt").Close(); }
            if (discouraged != null) { File.WriteAllLines(filepath, discouraged.ToArray()); }
            //Server.s.Log("Discouraged players list saved.");
        }

        public static void AddDiscouraged(string playername)
        {
            discouraged.Add(playername.ToLower().Trim());
            SaveDiscouraged();
        }

        public static void RemoveDiscouraged(string playername)
        {
            discouraged.Remove(playername.ToLower().Trim());
            SaveDiscouraged();
        }
    }
}
