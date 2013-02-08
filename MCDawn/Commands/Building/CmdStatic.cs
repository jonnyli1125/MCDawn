using System;
using System.IO;

namespace MCDawn
{
    public class CmdStatic : Command
    {
        public override string name { get { return "static"; } }
        public override string[] aliases { get { return new string[] { "t" }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdStatic() { }

        public override void Use(Player p, string message)
        {
            p.staticCommands = !p.staticCommands;
            p.ClearBlockchange();
            p.BlockAction = 0;

            Player.SendMessage(p, "Static mode: &a" + p.staticCommands.ToString());

            try
            {
                if (message != "")
                {
                    if (message.IndexOf(' ') == -1)
                    {
                        if (p.group.CanExecute(Command.all.Find(message)))
                            Command.all.Find(message).Use(p, "");
                        else
                            Player.SendMessage(p, "Cannot use that command.");
                    }
                    else
                    {
                        if (p.group.CanExecute(Command.all.Find(message.Split(' ')[0])))
                            Command.all.Find(message.Split(' ')[0]).Use(p, message.Substring(message.IndexOf(' ') + 1));
                        else
                            Player.SendMessage(p, "Cannot use that command.");
                    }
                }
            }
            catch { Player.SendMessage(p, "Could not find specified command"); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/static [command] - Makes every command a toggle.");
            Player.SendMessage(p, "If [command] is given, then that command is used");
        }
    }
}