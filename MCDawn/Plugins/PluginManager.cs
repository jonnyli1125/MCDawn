using System;
using System.IO;
using System.Reflection;

namespace MCDawn
{
	// The current way to load plugins will create a new instance for every plugin.
	// While this does work, it would be nice to redo the system afterwards to use every plugin in one instance.
	internal class PluginManager
	{
		private PluginManager() { }

		public Assembly _Assembly { get; private set; }
		public Plugin _Plugin { get; private set; }
		public String _PluginName { get; private set; }
		public String _PluginVersion { get; private set; }
		public String _MCDawnVersion { get; private set; }

		// This will throw an exception if it didn't load right.
		public static PluginManager Load(string plugin) {
			PluginManager pm = new PluginManager();

			// If full path isn't used then the loading method will throw an exception.
			string fullPath = System.IO.Path.GetFullPath(String.Format("extra/plugins/{0}.dll", plugin));
			pm._Assembly = Assembly.LoadFile(fullPath);
			
			foreach (Type type in pm._Assembly.GetTypes()) {
				if (type.BaseType == typeof(Plugin)) {
					pm._Plugin = (Plugin)Activator.CreateInstance(type);
					break;
				}
			}

			if (plugin == null)
				throw new Exception("Could not find child class of Plugin in DLL.");
			
			{
				string[] version = pm._Plugin.MCDawnVersion.Split('.');
				
				if (version.Length != 4)
					throw new Exception("Invalid MCDawn version specified by plugin.");

                int ii = 0;
                if (!int.TryParse(pm._Plugin.MCDawnVersion.Replace(".", ""), out ii))
                    throw new Exception("Invalid MCDawn version specified by plugin.");

                if (pm._Plugin.MCDawnVersion != Server.Version)
					throw new Exception("Plugin is incompatible with current MCDawn version (" + Server.Version + "). Created for " + version + ".");
			}
			
			pm._PluginName = pm._Plugin.Name;
			pm._PluginVersion = pm._Plugin.PluginVersion;
			pm._MCDawnVersion = pm._Plugin.MCDawnVersion;

			return pm;
		}

		// This will throw an exception if it didn't invoke right.
		public void Invoke() {
			this._Plugin.LoadPlugin();
		}

        public static void AutoLoad()
        {
            if (!File.Exists("text/pluginautoload.txt"))
                File.Create("text/pluginautoload.txt").Close();
            foreach (string s in File.ReadAllLines("text/pluginautoload.txt"))
                if (s.Trim() != "" && s != null)
                    Command.all.Find("plugin").Use(null, "load " + s);
        }
	}
}
