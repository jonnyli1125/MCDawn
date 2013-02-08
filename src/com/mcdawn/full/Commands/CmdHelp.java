package com.mcdawn.full.Commands;

import org.bukkit.command.*;

public class CmdHelp extends PluginCommand {
	@Override
	public String getName() { return "help"; }
	@Override
	public String[] getAliases() { return new String[] { "helpop" }; }
	@Override
	public CommandType getType() { return CommandType.Information; }
	@Override
	public int getPermission() { return 0; }
	
	public CmdHelp() { }
	
	@Override
	public boolean onCommand(CommandSender sender, Command cmd, String label, String[] args) {
		
		return false;
	}
}
