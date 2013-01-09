using System;

namespace MCDawn
{
    public class CmdIgnore : Command
    {
        public override string name { get { return "ignore"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdIgnore() { }

        public override void Use(Player p, string message)
        {
            if (p == null) { p.SendMessage("Command not usable in Console."); return; }
            if (message == "" || message.Split(' ').Length > 2) { Help(p); return; }
            switch (message.Split(' ')[0].ToLower())
            {
                case "global":
                    if (message.Split(' ').Length > 1) { goto default; }
                    if (!Server.ignoreGlobal.Contains(p.name.ToLower()))
                    {
                        Server.ignoreGlobal.Add(p.name.ToLower());
                        p.SendMessage("You are now ignoring incoming chat messages from Global Chat.");
                    }
                    else
                    {
                        Server.ignoreGlobal.Remove(p.name.ToLower());
                        p.SendMessage("You are no longer ignoring incoming chat messages from Global Chat.");
                    }
                    Server.ignoreGlobal.GCIgnoreSave();
                    Server.ignoreGlobal = PlayerList.GCIgnoreLoad();
                    break;
                default:
                    Player who = Player.Find(message.Split(' ')[0]);
                    string ignored = "";
                    if (who != null) { ignored = who.name; }
                    else { ignored = message.Split(' ')[0]; }

                    if (Server.allowIgnoreOps && (Server.hasProtection(name)) || who.group.Permission >= LevelPermission.Operator || Group.findPlayerGroup(ignored).Permission >= LevelPermission.Operator) { Player.SendMessage(p, "Cannot ignore operators."); return; }

                    if (p.ignoreList.Contains(ignored.ToLower())) 
                    {
                        Player.IgnoreRemove(p, ignored.ToLower());
                        p.SendMessage("You are no longer ignoring incoming chat messages from " + ignored + ".");
                    }
                    else 
                    {
                        foreach (Group gr in Group.GroupList)
                        {
                            if (gr.playerList.Contains(ignored.ToLower()) && gr.Permission >= LevelPermission.Operator && !Server.allowIgnoreOps || Server.devs.Contains(ignored.ToLower()) || Server.staff.Contains(ignored.ToLower()) || Server.administration.Contains(ignored.ToLower())) { p.SendMessage("You can't ignore a " + who.group.trueName + "."); return; }
                        }
                        Player.IgnoreAdd(p, ignored.ToLower());
                        p.SendMessage("You are now ignoring incoming chat messages from " + ignored + ".");
                    }
                    break;
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/ignore <player> - Ignore/unignore all incoming chat messages from <player>.");
            Player.SendMessage(p, "/ignore global - Ignore/unignore all incoming chat messages from Global Chat.");
            Player.SendMessage(p, "/ignore global <player> - Ignore/unignore all incoming chat messages from <player> on Global Chat.");
        }
    }
}