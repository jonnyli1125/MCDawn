using System;
using System.IO;

namespace MCDawn
{
    public class CmdVisible : Command
    {
        public override string name { get { return "visible"; } }
        public override string[] aliases { get { return new string[] { "hidden" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdVisible() { }

        public override void Use(Player p, string message)
        {
            Player who;
            if (message != "")
            {
                who = Player.Find(message);
            }
            else
            {
                who = p;
            }

            if (who == null)
            {
                Player.SendMessage(p, "Cannot find player.");
                return;
            }

            if (who.group.Permission > p.group.Permission)
            {
                Player.SendMessage(p, "Cannot view visibility status of higher ranked players.");
                return;
            }

            if (who.hidden == true)
            {
                if (who == p)
                {
                    Player.SendMessage(p, "You are invisible and hidden!");
                }
                else
                {
                    Player.SendMessage(p, who.color + who.name + "&g is currently invisible and hidden!");
                }
            }
            else
            {
                if (who == p)
                {
                    Player.SendMessage(p, "You are visible to other players! You are not hidden!");
                }
                else
                {
                    Player.SendMessage(p, who.color + who.name + "&g is currently visible to other players!");
                }
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/visible - Check if you are hidden..");
            Player.SendMessage(p, "/visible <player> - If <player> is given, you can check other people's visiblity.");
        }
    }
}