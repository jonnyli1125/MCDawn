using System.IO;

namespace MCDawn
{
	public class CmdCopyLvl : Command
	{
		public override string name { get { return "copylvl"; } }
		public override string[] aliases { get { return new string[] { "clvl" }; } }
		public override string type { get { return "build"; } }
		public override bool museumUsable { get { return false; } }
		public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
		public CmdCopyLvl() { }

		public override void Use(Player p, string message)
		{
			try
			{
				if (message.Trim() == "" || message.Split(' ').Length != 2) { Help(p); return; }
				if (message.Contains("/") || message.Contains(".")) { Player.SendMessage(p, "Invalid level name."); return; } // So an evil admin can't place levels in random locations.
				if (!File.Exists("levels/" + message.Split(' ')[0].ToLower().Trim() + ".lvl")) { Player.SendMessage(p, "The level " + message.Split(' ')[0] + " doesn't exist."); return; }
				if (File.Exists("levels/" + message.Split(' ')[1].ToLower().Trim() + ".lvl")) File.Delete("levels/" + message.Split(' ')[1].ToLower().Trim() + ".lvl");
				File.Copy("levels/" + message.Split(' ')[0].ToLower().Trim() + ".lvl", "levels/" + message.Split(' ')[1].ToLower().Trim() + ".lvl");
                Player.SendMessage(p, "Level copied over.");
                Command.all.Find("load").Use(null, message.Split(' ')[1]);
			}
			catch (IOException ex) { Server.ErrorLog(ex); Player.SendMessage(p, "Level copying failed."); return; }
			catch { Help(p); return; }
		}

		public override void Help(Player p)
		{
			Player.SendMessage(p, "/copylvl <level> <newlevel> - Copy a level to a new level.");
			Player.SendMessage(p, "&cWARNING: &eIf you copy a level to an already existing level name, it will overwrite the old level with that name.");
		}
	}
}