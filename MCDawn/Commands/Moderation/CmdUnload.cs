using System;
using System.IO;
using System.Collections.Generic;


namespace MCDawn
{
    public class CmdUnload : Command
    {
        public override string name { get { return "unload"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdUnload() { }

        public override void Use(Player p, string message)
        {
            if (message.ToLower() == "empty")
            {
                try
                {
                    List<Level> toUnload = new List<Level>();
                    foreach (Level l in Server.levels)
                        if (l.unload && Server.mainLevel != l && l.players.Count == 0)
                            toUnload.Add(l);
                    if (toUnload.Count == 0) { Player.SendMessage(p, "No levels were empty."); return; }
                    foreach (Level l in toUnload) l.Unload();
                }
                catch (Exception ex) { Server.ErrorLog(ex); Player.SendMessage(p, "Error unloading empty levels."); }
                return;
            }

            Level level = Level.Find(message);
            if (level != null)
            {
                if (p != null && p.hidden)
                {
                    if (!level.Unload(true)) Player.SendMessage(p, "You cannot unload the main level.");
                    return;
                }
                else
                {
                    if (!level.Unload()) Player.SendMessage(p, "You cannot unload the main level.");
                    return;
                }
            }

            Player.SendMessage(p, "There is no level \"" + message + "\" loaded.");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/unload [level] - Unloads a level.");
            Player.SendMessage(p, "/unload empty - Unloads an empty level.");
        }
    }
}