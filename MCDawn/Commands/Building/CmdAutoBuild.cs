using System;
using System.Threading;

namespace MCDawn
{
    public class CmdAutoBuild : Command
    {
        public override string name { get { return "autobuild"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdAutoBuild() { }

        public override void Use(Player p, string message)
        {
            if (message.Trim() == "" && !p.autobuild) { Help(p); return; }
            if (p == null) { Player.SendMessage(p, "Command not usable from Console."); return; }
            if (message.Trim().ToLower() == "off" || (p.autobuild && message.Trim() == "")) { p.autobuild = false; Player.SendMessage(p, "AutoBuild turned off."); return; }
            if (message.Split(' ').Length < 2 || message.Split(' ').Length > 3) { Help(p); return; }
            byte action; string actionString = message.Split(' ')[0].ToLower().Trim();
            if (actionString == "build" || actionString == "place" || actionString == "1") { action = 1; }
            else if (actionString == "destroy" || actionString == "break" || actionString == "0") { action = 0; }
            else { Player.SendMessage(p, "Please choose a valid block action."); return; }
            byte block = Block.Byte(message.Split(' ')[1]);
            if (block >= 255) { Player.SendMessage(p, "Unknown Block."); return; }
            if (block == Block.air) { Player.SendMessage(p, "Cannot use air as block. Set action to \"destroy\" instead."); return; }
            double interval = 1;
            if (message.Split(' ').Length > 2) 
            {
                try { interval = Math.Abs(double.Parse(message.Split(' ')[2])); }
                catch { Player.SendMessage(p, "Please choose a valid interval."); return; }
            }
            p.autobuild = true; Player.SendMessage(p, "AutoBuild mode activated!");
            Thread autoBuildThread = new Thread(new ThreadStart(delegate
            {
                try // Try/Catch block in case player is looking outside the map.
                {
                    while (p.autobuild)
                    {
                        double x, y, z;
                        for (byte i = 0; i < 4; i++)
                        {
                            x = Math.Round((p.pos[0] / 32) + (double)(Math.Sin(((double)(128 - p.rot[0]) / 256) * 2 * Math.PI) * i * Math.Cos(((double)(p.rot[1]) / 256) * 2 * Math.PI)));
                            y = Math.Round((p.pos[1] / 32) + (double)(Math.Cos(((double)(p.rot[1] + 64) / 256) * 2 * Math.PI) * i));
                            z = Math.Round((p.pos[2] / 32) + (double)(Math.Cos(((double)(128 - p.rot[0]) / 256) * 2 * Math.PI) * i * Math.Cos(((double)(p.rot[1]) / 256) * 2 * Math.PI)));
                            if (p.level.GetTile((ushort)(x), (ushort)(y), (ushort)(z)) != Block.air || i >= 3) { p.manualChange((ushort)(x), (ushort)(y), (ushort)(z), action, block); break; }
                        }
                        Thread.Sleep((int)(1000 * interval));
                    }
                }
                catch { }
            })); autoBuildThread.Start();
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/autobuild <action> <block> [interval] - Automatically build on the position of your cursor.");
            Player.SendMessage(p, "The <action> must be \"build\" or \"destroy\" (without the quotes).");
            Player.SendMessage(p, "The <block> is the type of block used.");
            Player.SendMessage(p, "The [interval] is the seconds difference between the actions. If not given, default interval is 1 (second).");
        }
    }
}