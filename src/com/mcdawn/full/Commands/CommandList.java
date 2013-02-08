package com.mcdawn.full.Commands;

import java.util.*;

public class CommandList {
	public static ArrayList<PluginCommand> getCommands() {
		ArrayList<PluginCommand> commands = new ArrayList<PluginCommand>();
		commands.add(new CmdHelp());
		return commands;
	}
}
