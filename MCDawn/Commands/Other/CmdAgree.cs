using System;

namespace MCDawn
{
    class CmdAgree : Command
    {
        public override string name { get { return "agree"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdAgree() { }

        public override void Use(Player p, string message)
        {
            if (Server.agreePass.Split(' ').Length > 1) { Server.agreePass = Server.agreePass.Split(' ')[0]; }
            if (p == null) { p.SendMessage("Command not usable from Console."); return; }
            if (!Server.agreeToRules) { p.SendMessage("Agree To Rules is currently off!"); return; }
            if (Server.agreedToRules.Contains(p.name)) { p.SendMessage("You have already agreed to the rules!"); return; }
            if (message == "" && Server.agreePass.Length > 0) { p.SendMessage("Please type /agree <password>; you must read /rules to get it."); return; }
            if (message.Split(' ').Length > 1 && Server.agreePass.Length > 0) { p.SendMessage("Agree password does not contain spaces."); return; }
            if (!p.readRules) { p.SendMessage("You must read the rules before agreeing to them!"); return; }

            message = message.Split(' ')[0];
            Player.GlobalMessageOps("To Ops: " + p.color + p.name + Server.DefaultColor + " agreed to the rules.");
            p.SendMessage("Thank you for agreeing to the rules!");
            Server.s.Log(p.name + " agreed to the rules.");
            p.AgreeToRules();
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/agree - Agree to the /rules.");
        }
    }
}
