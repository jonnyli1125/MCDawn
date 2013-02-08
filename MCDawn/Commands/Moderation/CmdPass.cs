using System;
using System.IO;

namespace MCDawn
{
    public class CmdPass : Command
    {
        public override string name { get { return "pass"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Unverified; } }
        public CmdPass() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            if (p.passtries >= 3) { p.Kick("Can't let you do that, Starfox."); return; }
            if (p.devUnverified) { Player.SendMessage(p, "Password Incorrect."); p.passtries++; return; }
            if (Server.adminsecurity == false && (!Server.devs.Contains(p.name.ToLower()) && !Server.staff.Contains(p.name.ToLower()) && !Server.administration.Contains(p.name.ToLower()))) { Player.SendMessage(p, "Admin Security System is currently disabled."); return; }
            if (p == null) { p.SendMessage("Command not usable from Console."); return; }
            if (p.group.Permission < Server.adminsecurityrank && p.unverified == false && (!Server.devs.Contains(p.name.ToLower()) && !Server.staff.Contains(p.name.ToLower()) && !Server.administration.Contains(p.name.ToLower()))) { Player.SendMessage(p, "Command reserved for OP+."); return; }
            if (p.unverified == false) { p.SendMessage("You currently are not in Admin Security System!"); return; }
            if (!File.Exists("passwords/" + p.name.ToLower() + ".xml") || String.IsNullOrEmpty(p.password)) { p.SendMessage("Password Incorrect."); return; }

            if (p.password != Player.PasswordFormat(message, false)) { p.SendMessage("Password Incorrect."); p.passtries++; return; }
            else
            {
                p.unverified = false;
                p.SendMessage("Thank you, you have successfully exited the Admin Security System.");
                if (p.group.Permission >= Server.adminchatperm) { Player.GlobalMessageAdmins("To Admins: " + p.color + p.name + "&g has exited the Admin Security System."); }
                else { Player.GlobalMessageOps("To Ops: " + p.color + p.name + "&g has exited the Admin Security System."); }
                p.passtries = 0;
                return;
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "&3/pass [password]" + "&g - Enter your password.");
        }
    }
}