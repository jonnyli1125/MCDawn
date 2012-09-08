using System;

namespace MCDawn
{
    public class CmdZz : Command
    {
        public override string name { get { return "zz"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdZz() { }

        public override void Use(Player p, string message)
        {
            if (p == null) { Player.SendMessage(p, "Command not usable in Console."); return; }
            if (!p.group.CanExecute(Command.all.Find("static")) || !p.group.CanExecute(Command.all.Find("cuboid"))) { p.SendMessage("You must have access to both /static and /cuboid commands."); return; }
            if (p.staticCommands) { p.staticCommands = false; p.ClearBlockchange(); p.BlockAction = 0; p.SendMessage("/zz has finished."); }
            else { Command.all.Find("static").Use(p, "cuboid " + message); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/zz - Toggles \"static cuboid\".");
        }
    }
}