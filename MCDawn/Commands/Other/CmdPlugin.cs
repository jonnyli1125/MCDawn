using System;
using System.IO;
using System.Text;

namespace MCDawn
{
	// Command for handling all plugin-related things.
	public class CmdPlugin : Command
	{
		public override string name { get { return "plugin"; } }
		public override string[] aliases { get { return new string[] { }; } }
		public override string type { get { return "other"; } }
		public override bool museumUsable { get { return true; } }
		public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
		public CmdPlugin() { }

		public override void Use(Player p, string message) {
			string[] parameters = message.Split(' ');
			
			if (parameters.Length < 1) { Help(p); return; }
			
			if (parameters[0].ToLower() == "info") {
				if (parameters.Length < 2) { Help(p); return; }
				string plugin = parameters[1];
				
				PluginManager pm = null;
				
				try {
					pm = PluginManager.Load(plugin);
				} catch (Exception e) {
					Player.SendMessage(p, "An error occured when loading the plugin.");
					Player.SendMessage(p, "If you are a server admin, see the log file at \"logs/errors/plugin.log\".");
					LogError(e);
					return;
				}
				
				Player.SendMessage(p, "Plugin's Name: " + pm._PluginName);
				Player.SendMessage(p, "Plugin's Version: " + pm._PluginVersion);
				Player.SendMessage(p, "Plugin's Lowest Compatible MCDawn Version: " + pm._LowestCompatibleMCDawnVersion);
				
				return;
			} else if (parameters[0].ToLower() == "load") {
				if (parameters.Length < 2) { Help(p); return; }
				string plugin = parameters[1];

				if (!File.Exists("extra/plugins/" + plugin + ".dll")) {
					Player.SendMessage(p, "Invalid plugin: Plugin file not found.");
					return;
				}
				
				PluginManager pm = null;
				
				try {
					pm = PluginManager.Load(plugin);
				} catch (Exception e) {
					Player.SendMessage(p, "An error occured when loading the plugin.");
					if (p != null) Player.SendMessage(p, "If you are a server admin, see the log file at \"logs/errors/plugin.log\".");
					LogError(e);
					return;
				}
				
				if (!IsValidName(pm._PluginName)) {
					Player.SendMessage(p, "An error occured when loading the plugin.");
                    if (p != null) Player.SendMessage(p, "If you are a server admin, see the log file at \"logs/errors/plugin.log\".");
					LogError(new Exception("Invalid name of plugin. Plugin file: extra/plugins/" + plugin + ".dll"));
					return;
				}
				
				Player.SendMessage(p, "Plugin " + pm._PluginName + " successfully loaded and running.");
				
				try {
					pm.Invoke();
				} catch (Exception e) {
					Player.SendMessage(p, "An error occured when running the plugin.");
                    if (p != null) Player.SendMessage(p, "If you are a server admin, see the log file at \"logs/errors/plugin.log\".");
					LogError(e);
					return;
				}
				
			} else {
				Help(p); return;
			}
		}
		
		private bool IsValidName(string pluginName) {
			string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_- ";
			bool found = false;
			foreach (char c in pluginName) {
				foreach (char ch in alphabet)
					if (ch == c) found = true;
				if (!found) return false;
				else found = false;
			}

			return true;
		}
		
		private void LogError(Exception e) {
			try {
				bool exists = File.Exists("logs/errors/plugin.log");
				if (!exists)
					File.Create("logs/errors/plugin.log");
			
				StringBuilder sb = new StringBuilder();
				if (exists) {
					string divider = new string('-', 25);
					sb.AppendLine();
					sb.AppendLine(divider);
					sb.AppendLine();
				}
				sb.AppendLine("Message: " + e.Message);
				sb.AppendLine("Source: " + e.Source);
				sb.AppendLine("Stack Trace: " + e.StackTrace);
				
				using (StreamWriter sw = new StreamWriter("logs/errors/plugin.log"))
					sw.Write(sb.ToString());
                Server.s.Log("See logs/errors/plugin.log for error log.");
			} catch (Exception e2) {
				Server.s.Log("An error occured when writing to the plugin log file. See today's error log file for more information.", false);
				Server.ErrorLog(e2);
			}
		}
		
		public override void Help(Player p) {
			Player.SendMessage(p, "This command has many uses.");
			Player.SendMessage(p, "/plugin info <plugin> - Gives you the name, version, and lowest compatible MCDawn version of that plugin.");
			Player.SendMessage(p, "Example: \"/plugin info hello\" would give you the info about \"extra/plugins/hello.dll\"");
			Player.SendMessage(p, "");
			Player.SendMessage(p, "/plugin load <plugin> - Loads the plugin at extra/plugins/<plugin>.dll");
			Player.SendMessage(p, "Example: \"/plugin load hello\" would load \"extra/plugins/hello.dll\"");
		}
	}
}
