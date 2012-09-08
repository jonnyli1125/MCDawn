using System;

namespace MCDawn
{
    public class CmdDiscourager : Command
    {
        public override string name { get { return "discourager"; } }
        public override string[] aliases { get { return new string[] { "discourage" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdDiscourager() { }

        public override void Use(Player p, string message)
        {
            switch (message.Split(' ')[0].ToLower().Trim())
            {
                case "on":
                    if (Server.useDiscourager) { Player.SendMessage(p, "Discourager is already enabled."); return; }
                    Server.useDiscourager = true;
                    Properties.Save("properties/server.properties");
                    Player.SendMessage(p, "Discourager enabled.");
                    break;
                case "off":
                    if (!Server.useDiscourager) { Player.SendMessage(p, "Discourager is already disabled."); return; }
                    Server.useDiscourager = false;
                    Properties.Save("properties/server.properties");
                    Player.SendMessage(p, "Discourager disabled.");
                    break;
                case "add":
                    if (!Server.useDiscourager) { Player.SendMessage(p, "Discourager is currently disabled."); return; }
                    Discourager.AddDiscouraged(message.Split(' ')[1]);
                    Discourager.SaveDiscouraged();
                    Discourager.LoadDiscouraged();
                    if (Group.Find(Group.findPlayer(message.Split(' ')[1])).Permission > Server.opchatperm) { Player.SendMessage(p, "Cannot discourage OP+."); return; }
                    if (Group.Find(Group.findPlayer(message.Split(' ')[1])).Permission >= p.group.Permission && p != null) { Player.SendMessage(p, "Cannot discourage player of equal or higher rank."); return; }
                    if (Player.Find(message.Split(' ')[1]) != null) 
                    {
                        if (Server.hasProtection(Player.Find(message.Split(' ')[1]).name)) { Player.SendMessage(p, "Cannot discourage player of equal or higher rank."); return; }
                        message.Split(' ')[1] = Player.Find(message.Split(' ')[1]).name; 
                    }
                    Player.SendMessage(p, message.Split(' ')[1] + " added to discouraged users list.");
                    break;
                case "remove":
                    if (!Server.useDiscourager) { Player.SendMessage(p, "Discourager is currently disabled."); return; }
                    Discourager.RemoveDiscouraged(message.Split(' ')[1]);
                    Discourager.SaveDiscouraged();
                    Discourager.LoadDiscouraged();
                    if (Player.Find(message.Split(' ')[1]) != null) { message.Split(' ')[1] = Player.Find(message.Split(' ')[1]).name; }
                    Player.SendMessage(p, message.Split(' ')[1] + " removed from discouraged users list.");
                    break;
                case "list":
                    if (!Server.useDiscourager) { Player.SendMessage(p, "Discourager is currently disabled."); return; }
                    if (Discourager.discouraged.Count <= 0) { Player.SendMessage(p, "Discouraged Users: None."); return; }
                    else
                    {
                        string list = String.Join(", ", Discourager.discouraged.ToArray());
                        Player.SendMessage(p, "Discouraged Users: " + list.Remove(list.Length - 2));
                    }
                    break;
                default: Help(p); return;
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/discourager <add/remove> <playername> - Add or remove a player from the discouraged players list.");
            Player.SendMessage(p, "/discourager <on/off> - Enable or disable the discourager.");
            Player.SendMessage(p, "/discourager list - Show list of current discouraged users.");
        }
    }
}