package com.mcdawn.full.Commands;

import org.bukkit.command.*;

public abstract class PluginCommand implements CommandExecutor {
	public abstract String getName();
	public abstract String[] getAliases();
	public abstract CommandType getType();
	public abstract int getPermission();
	@Override
	public abstract boolean onCommand(CommandSender sender, Command cmd, String label, String[] args);
}
