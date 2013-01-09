using System;
using System.Collections.Generic;

namespace MCDawn
{
    public class CmdAward : Command
    {
        public override string name { get { return "award"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdAward() { }

        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') == -1) { Help(p); return; }

            bool give = true;
            if (message.Split(' ')[0].ToLower() == "give")
            {
                give = true;
                message = message.Substring(message.IndexOf(' ') + 1);
            }
            else if (message.Split(' ')[0].ToLower() == "take")
            {
                give = false;
                message = message.Substring(message.IndexOf(' ') + 1);
            }
            
            string foundPlayer = message.Split(' ')[0];
            Player who = Player.Find(message);
            if (who != null) foundPlayer = who.name;
            string awardName = message.Substring(message.IndexOf(' ') + 1);
            if (!Awards.awardExists(awardName))
            {
                Player.SendMessage(p, "The award you entered doesn't exist");
                Player.SendMessage(p, "Use /awards for a list of awards");
                return;
            }

            if (give)
            {
                if (Awards.giveAward(foundPlayer, awardName))
                {
                    Player.GlobalChat(p, Server.FindColor(foundPlayer) + foundPlayer + "&g was awarded: &b" + Awards.camelCase(awardName), false);
                }
                else
                {
                    Player.SendMessage(p, "The player already has that award!");
                }
            }
            else
            {
                if (Awards.takeAward(foundPlayer, awardName))
                {
                    Player.GlobalChat(p, Server.FindColor(foundPlayer) + foundPlayer + "&g had their &b" + Awards.camelCase(awardName) + "&g award removed", false);
                }
                else
                {
                    Player.SendMessage(p, "The player didn't have the award you tried to take");
                }
            }

            Awards.Save();
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/award <give/take> [player] [award] - Gives [player] the [award]");
            Player.SendMessage(p, "If no Give or Take is given, Give is used");
            Player.SendMessage(p, "[award] needs to be the full award's name. Not partial");
        }
    }
}