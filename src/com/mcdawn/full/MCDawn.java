package com.mcdawn.full;

import java.util.logging.Logger;

import org.bukkit.plugin.java.JavaPlugin;

import com.mcdawn.full.Utils.*;
import com.mcdawn.full.Commands.*;
import com.mcdawn.full.Commands.PluginCommand;

public class MCDawn extends JavaPlugin {
	public static String version = "1.0.0";
	public static String extraVersion = "alpha";
	
	public static Config config;
	public static Logger logger;
	
	@Override
	public void onEnable() {
		try {
			logger = getLogger();
			logger.info("Enabled MCDawn, v" + version + (extraVersion == "" ? "" : "-" + extraVersion + "."));
			// initialize configuration
			config = new Config(getConfig());
			// initialize commands
			for (PluginCommand cmd : CommandList.getCommands()) {
				getCommand(cmd.getName()).setExecutor(cmd);
				for (String alias : cmd.getAliases())
					getCommand(alias).setExecutor(cmd);
			}
		} catch (Exception ex) { ex.printStackTrace(); }
	}
	
	@Override
	public void onDisable() {
		logger.info("Disabled MCDawn.");
	}
}
