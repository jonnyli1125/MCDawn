using System;

namespace MCDawn
{
    public class CmdKillPhysics : Command
    {
        public override string name { get { return "killphysics"; } }
        public override string[] aliases { get { return new string[] { "kp" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            try
            {
                
                if (message == "")
                {
                    Player.GlobalMessage("Physics are now &cOFF" + "&g on all levels.");
                    foreach (Level lvl in Server.levels)
                    {
                        if (lvl.physics > 0)
                        {
                            Command.all.Find("physics").Use(null, lvl.name + " 0");
                        }
                    }
                    return;
                }
                if (message.ToLower() == "emp")
                {
                    Player.GlobalMessage("EMP Launched! All Physics are now &cOFF.");
                    foreach (Level lvl in Server.levels)
                    {
                        if (lvl.physics > 0)
                        {
                            Command.all.Find("physics").Use(null, lvl.name + " 0");
                        }
                    }
                    return;
                }
            }
            catch (Exception) 
            { 
                Player.SendMessage(p, "Error using command!"); 
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/killphysics - Sets all physics on all levels to 0.");
            Player.SendMessage(p, "/killphysics emp - Just for a little fun. :D");
            Player.SendMessage(p, "Use /kp as a shortcut.");
        }
    }
}