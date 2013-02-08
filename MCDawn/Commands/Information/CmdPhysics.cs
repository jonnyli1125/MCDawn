using System;

namespace MCDawn
{
    public class CmdPhysics : Command
    {
        public override string name { get { return "physics"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "information"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdPhysics() { }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                foreach (Level l in Server.levels)
                {
                    if (l.physics > 0)
                        Player.SendMessage(p, "&5" + l.name + "&g has physics at &b" + l.physics + "&g. &cChecks: " + l.lastCheck + "; Updates: " + l.lastUpdate);
                }
                return;
            }
            try
            {
                int temp = 0; Level level = null;
                if (message.Split(' ').Length == 1)
                {
                    temp = int.Parse(message);
                    if (p != null)
                    {
                        level = p.level;
                    }
                    else
                    {
                        level = Server.mainLevel;
                    }
                }
                else
                {
                    temp = System.Convert.ToInt16(message.Split(' ')[1]);
                    string nameStore = message.Split(' ')[0];
                    level = Level.Find(nameStore);
                }
                if (temp >= 0 && temp <= 5)
                {
                    level.setPhysics(temp);
                    switch (temp)
                    {
                        case 0:
                            level.ClearPhysics();
                            Player.GlobalMessage("Physics are now &cOFF" + "&g on &b" + level.name + "&g.");
                            Server.s.Log("Physics are now OFF on " + level.name + ".");
                            IRCBot.Say("Physics are now OFF on " + level.name + ".");
                            break;

                        case 1:
                            Player.GlobalMessage("Physics are now &aNormal" + "&g on &b" + level.name + "&g.");
                            Server.s.Log("Physics are now ON on " + level.name + ".");
                            IRCBot.Say("Physics are now ON on " + level.name + ".");
                            break;

                        case 2:
                            Player.GlobalMessage("Physics are now &aAdvanced" + "&g on &b" + level.name + "&g.");
                            Server.s.Log("Physics are now ADVANCED on " + level.name + ".");
                            IRCBot.Say("Physics are now ADVANCED on " + level.name + ".");
                            break;

                        case 3:
                            Player.GlobalMessage("Physics are now &aHardcore" + "&g on &b" + level.name + "&g.");
                            Server.s.Log("Physics are now HARDCORE on " + level.name + ".");
                            IRCBot.Say("Physics are now HARDCORE on " + level.name + ".");
                            break;

                        case 4:
                            Player.GlobalMessage("Physics are now &aInstant" + "&g on &b" + level.name + "&g.");
                            Server.s.Log("Physics are now INSTANT on " + level.name + ".");
                            IRCBot.Say("Physics are now INSTANT on " + level.name + ".");
                            break;
                        case 5:
                            Player.GlobalMessage("Physics are now &aDoors Only" + "&g on &b" + level.name + "&g.");
                            Server.s.Log("Physics are now DOORS ONLY on " + level.name + ".");
                            IRCBot.Say("Physics are now DOORS ONLY on " + level.name + ".");
                            break;
                    }

                    level.changed = true;
                }
                else
                {
                    Player.SendMessage(p, "Not a valid setting");
                }
            }
            catch
            {
                Player.SendMessage(p, "INVALID INPUT");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/physics [map] <0/1/2/3/4/5> - Set the [map]'s physics, 0-Off 1-On 2-Advanced 3-Hardcore 4-Instant 5-Doors Only");
            Player.SendMessage(p, "If [map] is blank, uses Current level");
        }
    }
}