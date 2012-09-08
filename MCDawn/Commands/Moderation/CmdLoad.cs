using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace MCDawn
{
	public class CmdLoad : Command
	{
		public override string name { get { return "load"; } }
		public override string[] aliases { get { return new string[] { "mapload" }; } }
		public override string type { get { return "mod"; } }
		public override bool museumUsable { get { return true; } }
		public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
		public CmdLoad() { }

		public override void Use(Player p, string message)
		{
			try
			{
				if (message == "") { Help(p); return; }
				if (message.Split(' ').Length > 2) { Help(p); return; }
				int pos = message.IndexOf(' ');
				string phys = "0";
				if (pos != -1)
				{
					phys = message.Substring(pos + 1);
					message = message.Substring(0, pos).ToLower();
				}
				else
				{
					message = message.ToLower();
				}

				foreach (Level l in Server.levels)
				{
					if (l.name == message) { Player.SendMessage(p, message + " is already loaded!"); return; }
				}

				if (Server.levels.Count == Server.levels.Capacity)
				{
					if (Server.levels.Capacity == 1)
					{
						Player.SendMessage(p, "You can't load any levels!");
					}
					else
					{
						Command.all.Find("unload").Use(p, "empty");
						if (Server.levels.Capacity == 1)
						{
							Player.SendMessage(p, "No maps are empty to unload. Cannot load map.");
							return;
						}
					}
				}

				if (!File.Exists("levels/" + message + ".lvl"))
				{
					Player.SendMessage(p, "Level \"" + message + "\" doesn't exist!"); return;
				}
				
				if (!Player.ValidName(name)) { Player.SendMessage(p, "Invalid level name."); return; }

				Level level = Level.Load(message);

				if (level == null)
				{
					if (File.Exists("levels/" + message + ".lvl.backup"))
					{
						Server.s.Log("Attempting to load backup.");
						File.Copy("levels/" + message + ".lvl.backup", "levels/" + message + ".lvl", true);
						level = Level.Load(message);
						if (level == null)
						{
							Player.SendMessage(p, "Backup of " + message + " failed.");
							return;
						}
					}
					else
					{
						Player.SendMessage(p, "Backup of " + message + " does not exist.");
						return;
					}
				}

				if (p != null) if (level.permissionvisit > p.group.Permission)
					{
						Player.SendMessage(p, "This map is for " + Level.PermissionToName(level.permissionvisit) + " only!");
						GC.Collect();
						GC.WaitForPendingFinalizers();
						return;
					}

				foreach (Level l in Server.levels)
				{
					if (l.name == message)
					{
						Player.SendMessage(p, message + " is already loaded!");
						GC.Collect();
						GC.WaitForPendingFinalizers();
						return;
					}
				}

				lock (Server.levels) {
					Server.addLevel(level);
				}

				level.physThread.Start();

				if (p != null)
				{
					if (p.hidden) { Player.GlobalMessageAdmins("To Admins: Level &3" + level.name + Server.DefaultColor + " stealth loaded."); }
					else { Player.GlobalMessage("Level &3" + level.name + Server.DefaultColor + " loaded."); }
				}
				else { Player.GlobalMessage("Level &3" + level.name + Server.DefaultColor + " loaded."); }
				try
				{
					int temp = int.Parse(phys);
					if (temp >= 1 && temp <= 4)
					{
						level.setPhysics(temp);
					}
				}
				catch
				{
					Player.SendMessage(p, "Physics variable invalid");
				}
			}
			catch (Exception e)
			{
				Player.GlobalMessage("An error occured with /load");
				Server.ErrorLog(e);
			}
			finally
			{
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}
		}
		public override void Help(Player p)
		{
			Player.SendMessage(p, "/load <level> <physics> - Loads a level.");
		}
	}
}