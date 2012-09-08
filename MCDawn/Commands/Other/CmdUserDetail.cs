using System;

namespace MCDawn
{
    public class CmdUserDetail : Command
    {
        public override string name { get { return "userdetail"; } }
        public override string[] aliases { get { return new string[] { "userline", "detailuser" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdUserDetail() { }

        public override void Use(Player p, string message)
        {
            if (!p.haswom) { Player.SendMessage(p, "You are not using WOM Game Client."); return; }
            switch (message.Split(' ')[0].ToLower().Trim())
            {
                case "blockinfo":
                case "compass":
                case "games":
                case "infection":
                case "team":
                case "welcome":
                case "default":
                case "clear":
                    p.userlinetype = message.Split(' ')[0].ToLower().Trim();
                    break;
                case "custom":
                    if (message.Split(' ').Length <= 2) { Help(p); return; }
                    p.userlinetype = message.Split(' ')[0].ToLower().Trim();
                    p.customuserline = message.Split(new char[] { ' ' }, 2)[1].Trim();
                    break;
                case "":
                default:
                    Help(p); 
                    return;
            }
            Player.SendMessage(p, "User Detail line type set to &c" + message.Split(' ')[0]);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/userdetail <option> [text] - Set WOM userdetail as <option>. If you are not in WOM, this will not work.");
            Player.SendMessage(p, "Valid options are: &cblockinfo, compass, games, welcome, default, custom, clear");
            Player.SendMessage(p, "If [text] is given, it will only show if the <type> is set to &ccustom&g.");
        }
    }
}