package com.mcdawn.full.Utils;

import java.io.File;
import com.mcdawn.full.*;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.Color;

public class Config {
	public FileConfiguration config;
	public Config(FileConfiguration conf) {
		config = conf;
		MCDawn.logger.info("Configuration loaded.");
		File file = new File("plugins" + File.separator + "MCDawn" + File.separator + "config.yml");
		file.mkdir();
		
		// server.general
		if (!config.contains("server.general.defaultColor")) config.set("server.general.defaultConfig", Color.YELLOW);
		if (!config.contains("server.general.autoAfkSet")) config.set("server.general.autoAfkSet", 10);
		if (!config.contains("server.general.autoAfkKick")) config.set("server.general.autoAfkKick", 20);
		if (!config.contains("server.general.chatVariables")) config.set("server.general.allowChatVariables", true);
		if (!config.contains("server.general.moneyName")) config.set("server.general.moneyName", "moneys");
		if (!config.contains("server.general.rankChat")) config.set("server.general.rankChat", true);
		if (!config.contains("server.general.forceCuboid")) config.set("server.general.forceCuboid", true);
		if (!config.contains("server.general.consoleName")) config.set("server.general.consoleName", "Anonymous");
		if (!config.contains("server.general.debugMode")) config.set("server.general.debugMode", false);
		
		// server.irc
		if (!config.contains("server.irc.useIRC")) config.set("server.irc.useIRC", false);
		if (!config.contains("server.irc.server")) config.set("server.irc.server", "irc.esper.net");
		if (!config.contains("server.irc.port")) config.set("server.irc.port", 6667);
		if (!config.contains("server.irc.channel")) config.set("server.irc.channel", "#ChangeMe");
		if (!config.contains("server.irc.nick")) config.set("server.irc.nick", randomNick());
		if (!config.contains("server.irc.identify")) config.set("server.irc.identify", "");
		
		// server.global
		if (!config.contains("server.global.useGlobalChat")) config.set("server.global.useGlobalChat", true);
		if (!config.contains("server.global.nick")) config.set("server.global.nick", randomNick());
		if (!config.contains("server.global.identify")) config.set("server.global.identify", "");
	}
	
	private String randomNick() {
		String returnValue = "MC";
		for (int i = 0; i < 4; i++) {
			int randomInt = 1000 + (int)(Math.random() * ((9999 - 1000) + 1));
			returnValue += randomInt;
		}
		return returnValue;
	}
}
