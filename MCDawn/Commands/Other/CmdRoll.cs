using System;

namespace MCDawn
{
    public class CmdRoll : Command
    {
        public override string name { get { return "roll"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdRoll() { }

        public override void Use(Player p, string message)
        {
            int min, max; Random rand = new Random();
            try { min = int.Parse(message.Split(' ')[0]); }
            catch { min = 1; }
            try { max = int.Parse(message.Split(' ')[1]); }
            catch { max = 7; }

            Player.GlobalMessage(p.color + p.name + "&g rolled a &a" + rand.Next(Math.Min(min, max), Math.Max(min, max) + 1).ToString() + "&g (" + Math.Min(min, max) + "|" + Math.Max(min, max) + ")");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/roll [min] [max] - Rolls a random number between [min] and [max].");
        }
    }
}