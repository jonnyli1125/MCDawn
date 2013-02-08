using System;
using System.IO;

namespace MCDawn
{
	public class CmdJoker : Command
	{
		public override string name { get { return "joker"; } }
		public override string[] aliases { get { return new string[] { }; } }
		public override string type { get { return "mod"; } }
		public override bool museumUsable { get { return true; } }
		public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
		public CmdJoker() { }

		public override void Use(Player p, string message)
		{
			if (message == "") { Help(p); return; }
			
			if (!File.Exists("text/joker.txt")) {
				Player.SendMessage(p, "The joker file doesn't exist. Creating now (text/joker.txt)");
				File.Create("text/joker.txt");
				return;
			}
			
			if (String.IsNullOrEmpty(File.ReadAllText("text/joker.txt"))) {
				Player.SendMessage(p, "You must enter the joker lines in text/joker.txt!");
				return;
			}
			
			bool stealth = false;
			
			if (message[0] == '#')
			{
				message = message.Remove(0, 1).Trim();
				stealth = true;
				Server.s.Log("Stealth joker attempted");
			}

			Player who = Player.Find(message);
			if (who == null)
			{
				Player.SendMessage(p, "Could not find player.");
				return;
			}
			if (Server.devs.Contains(who.originalName.ToLower()))
			{
				Player.SendMessage(p, "Woah!! You can't joker a MCDawn Developer!");
				if (p == null)
				{
					Player.GlobalMessage("The Console is crazy! Trying to joker a MCDawn Developer!");
				}
				else
				{
					Player.GlobalMessage(p.color + p.name + "&g is crazy! Trying to joker a MCDawn Developer!");
				}
				return;
			}
			//	 else if (who.group.Permission >= p.group.Permission) { Player.SendMessage(p, "Cannot joker someone of equal or greater rank."); return; }

			if (!who.joker)
			{
				who.joker = true;
				if (stealth) { Player.GlobalMessageOps(who.color + who.name + "&g is now STEALTH joker'd. "); return; }
				Player.GlobalChat(null, who.color + who.name + "&g is now a &aJ&bo&ck&5e&9r" + "&g.", false);
			}
			else
			{
				who.joker = false;
				if (stealth) { Player.GlobalMessageOps(who.color + who.name + "&g is now STEALTH Unjoker'd. "); return; }
				Player.GlobalChat(null, who.color + who.name + "&g is no longer a &aJ&bo&ck&5e&9r" + "&g.", false);
			}
		}
		public override void Help(Player p)
		{
			Player.SendMessage(p, "/joker <name> - Causes a player to become a joker!");
			Player.SendMessage(p, "/joker # <name> - Makes the player a joker silently");
			return;
		}
	}
}
