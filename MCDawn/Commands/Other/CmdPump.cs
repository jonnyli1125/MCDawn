/* Coded by Gamemakergm based on MCForge's for debugging purposes
 * 
 * 
 */
using System;
using MCDawn;
namespace MCDawn
{
    class CmdPump : Command
    {
        public override string name { get { return "pump"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Nobody; } }
        public CmdPump() { }

        public override void Use(Player p, string message)
        {
            try
            {
                //Heart.Pump(new MCForgeBeat());
                //Heartbeat.Pump(Beat.Safaree);
                //SafareeBeat.Action();
                Heartbeat.Pump(Beat.MCDawn);
            }
            catch (Exception e)
            {
                Server.s.Log("Error with MCDawn pump.");
                Server.ErrorLog(e);
            }
            Player.SendMessage(p, "Heartbeat pump sent.");
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/pump - Forces a pump for the MCDawn heartbeat.  DEBUG PURPOSES ONLY.");
        }
    }
}