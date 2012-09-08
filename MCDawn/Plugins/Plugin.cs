// Interface for plugins. Like the abstract plugin class from before, except it's an interface.

namespace MCDawn {
	/// <summary>
	/// Interface for Plugins. Contains all the information a plugin will need for the PluginManager to read it.
	/// </summary>
	public abstract class Plugin {
		/// <summary>
		/// Name of the plugin.
		/// </summary>
		public abstract string Name { get; }
		/// <summary>
		/// Version of the plugin.
		/// </summary>
		public abstract string PluginVersion { get; }
		/// <summary>
		/// Lowest version of MCDawn that the plugin is compatible with.
		/// </summary>
		public abstract string LowestCompatibleMCDawnVersion { get; } // This is a weak way of doing it. Should be changed to what MyBB does for compatibility.

		/// <summary>
		/// First method to be called in the plugin.
		/// This is where everything important to the plugin should go, such as events, variable initializations, etc..
		/// </summary>
		public abstract void LoadPlugin();
	}
}
