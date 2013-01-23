using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MCDawn
{
    public class CmdWayPoint : Command
    {
        public override string name { get { return "waypoint"; } }
        public override string[] aliases { get { return new string[] { "wp", "warp" }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }
        public CmdWayPoint() { }

        public override void Use(Player p, string message)
        {
            if (p == null) { Player.SendMessage(p, "Command not usable from Console."); return; }
            if (!Directory.Exists("extra/waypoints")) Directory.CreateDirectory("extra/waypoints");
            if (!File.Exists("extra/waypoints/" + p.name.ToLower() + ".txt")) File.Create("extra/waypoints/" + p.name.ToLower() + ".txt").Close();
            var wpFile = new List<string>(File.ReadAllLines("extra/waypoints/" + p.name.ToLower() + ".txt")).Where(s => !String.IsNullOrEmpty(s)).Distinct().ToList();
            var wpNames = new List<string>();
            foreach (string s in wpFile)
                if (!String.IsNullOrEmpty(s.Split('|')[0]))
                    wpNames.Add(s.Split('|')[0]);
            // Waypoint format:
            // one line per waypoint, stored in extra/waypoints/<name>.txt
            // name|level|x|y|z|rotx|roty
            switch (message.Split(' ')[0].ToLower())
            {
                case "":
                case "list":
                    if (wpFile.Count <= 0) { Player.SendMessage(p, "No waypoints saved yet."); Help(p); return; }
                    Player.SendMessage(p, "Your waypoints:");
                    for (int i = 0; i < wpFile.Count; i++)
                    {
                        string[] wp = wpFile[i].Split('|');
                        Player.SendMessage(p, (i + 1) + ": &9" + wp[0] + " - " + wp[1] + " > " + wp[2] + "/" + wp[3] + "/" + wp[4]);
                    }
                    break;
                case "tp":
                case "teleport":
                case "go":
                case "goto":
                    if (message.Split(' ').Length > 2 || message.Split(' ').Length <= 1) { Help(p); return; }
                    if (wpFile.Count <= 0) { Player.SendMessage(p, "No waypoints saved yet."); Help(p); return; }
                    if (!wpNames.Contains(message.Split(' ')[1].ToLower())) { Player.SendMessage(p, "Waypoint could not be found."); return; }
                    string[] values = wpFile[wpNames.IndexOf(message.Split(' ')[1])].Split('|');
                    if (!File.Exists("levels/" + values[1] + ".lvl")) { Player.SendMessage(p, "Level does not exist."); return; }
                    Level endLevel = Level.Find(values[1]);
                    if (endLevel == null || p.level != endLevel)
                    {
                        if (!Server.AutoLoad) Command.all.Find("load").Use(p, values[1]);
                        Command.all.Find("goto").Use(p, values[1]);
                    }
                    unchecked { p.SendPos((byte)-1, (ushort)(ushort.Parse(values[2]) * 32), (ushort)(ushort.Parse(values[3]) * 32), (ushort)(ushort.Parse(values[4]) * 32), byte.Parse(values[5]), byte.Parse(values[6])); }
                    Player.SendMessage(p, "Sent you to waypoint: &9" + values[0] + "&g.");
                    break;
                case "add":
                case "save":
                    if (message.Split(' ').Length > 2 || message.Split(' ').Length <= 1) { Help(p); return; }
                    bool existing = false;
                    if (wpNames.Contains(message.Split(' ')[1].ToLower().Trim())) { existing = true; wpFile.RemoveAt(wpNames.IndexOf(message.Split(' ')[1].ToLower().Trim())); }
                    var toAdd = new List<string>();
                    toAdd.Add(message.Split(' ')[1].ToLower().Trim().Replace("|", ""));
                    toAdd.Add(p.level.name.ToLower());
                    toAdd.Add(((ushort)(p.pos[0] / 32)).ToString());
                    toAdd.Add(((ushort)(p.pos[1] / 32)).ToString());
                    toAdd.Add(((ushort)(p.pos[2] / 32)).ToString());
                    toAdd.Add(p.rot[0].ToString());
                    toAdd.Add(p.rot[1].ToString());
                    wpFile.Add(String.Join("|", toAdd.ToArray()));
                    File.WriteAllLines("extra/waypoints/" + p.name.ToLower() + ".txt", wpFile.ToArray());
                    Player.SendMessage(p, "Waypoint &9" + message.Split(' ')[1].ToLower().Trim() + "&g " + (existing ? "saved" : "added") + ".");
                    break;
                case "del":
                case "remove":
                    if (message.Split(' ').Length > 2 || message.Split(' ').Length <= 1) { Help(p); return; }
                    if (!wpNames.Contains(message.Split(' ')[1].ToLower())) { Player.SendMessage(p, "That waypoint does not exist."); return; }
                    wpFile.RemoveAt(wpNames.IndexOf(message.Split(' ')[1].ToLower()));
                    File.WriteAllLines("extra/waypoints/" + p.name.ToLower() + ".txt", wpFile.ToArray());
                    Player.SendMessage(p, "Waypoint deleted.");
                    break;
                default: Help(p); return;
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/waypoint - List your waypoints.");
            Player.SendMessage(p, "/waypoint tp <name> - Teleports you to your waypoint that has the name of <name>.");
            Player.SendMessage(p, "/waypoint add/save <name> - Adds or saves your current location as a waypoint.");
            Player.SendMessage(p, "/waypoint del <name> - Deletes the waypoint with the name of <name>.");
        }
    }
}