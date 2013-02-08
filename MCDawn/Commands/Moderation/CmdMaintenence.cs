using System;

namespace MCDawn
{
    public class CmdMaintenance : Command
    {
        public override string name { get { return "maintenance"; } }
        public override string[] aliases { get { return new string[] { "maint" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdMaintenance() { }

        public override void Use(Player p, string message)
        {
            try
            {
                if (message == "")
                {
                    if (Server.maintenance)
                    {
                        Server.maintenance = false;
                        Player.GlobalMessage("Server placed in Normal mode.");
                        Server.s.Log("Server placed in Normal mode.");
                        try { if (!Server.cli) { MCDawn.Gui.Window.thisWindow.chkMaintenance.Checked = Server.maintenance; } }
                        catch { }
                        return;
                    }
                    else
                    {
                        Server.maintenance = true;
                        Player.GlobalMessage("Server placed in maintenance mode.");
                        Server.s.Log("Server placed in maintenance mode.");
                        try { if (!Server.cli) { MCDawn.Gui.Window.thisWindow.chkMaintenance.Checked = Server.maintenance; } }
                        catch { }
                        return;
                    }
                }
                else if (message.ToLower() == "status")
                {
                    if (Server.maintenance) Player.SendMessage(p, "Server is currently in maintenance mode.");
                    else Player.SendMessage(p, "Server is currently in Normal mode.");
                }
            }
            catch (Exception e)
            {
                Server.ErrorLog(e);
                Player.SendMessage(p, "Error putting Server in/out of maintenance mode.");
                Server.s.Log("Error putting Server in/out of maintenance mode.");
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/maintenance - Toggles maintenance Mode on/off.");
            Player.SendMessage(p, "/maintenance status - Displays the current server maintenance status.");
        }
    }
}