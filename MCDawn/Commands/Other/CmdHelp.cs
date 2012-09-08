using System;
using System.Collections.Generic;
using System.Linq;

namespace MCDawn
{
    public class CmdHelp : Command
    {
        public override string name { get { return "help"; } }
        public override string[] aliases { get { return new string[] { "cmdlist", "commands", "helpop", "cmdhelp" }; } }
        public override string type { get { return "information"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdHelp() { }

        public override void Use(Player p, string message)
        {
            try
            {
                message.ToLower();
                switch (message)
                {
                    case "":
                        if (Server.oldHelp)
                        {
                            goto case "old";
                        }
                        else
                        {
                            Player.SendMessage(p, "Use &b/help ranks" + Server.DefaultColor + " for a list of ranks.");
                            Player.SendMessage(p, "Use &b/help build" + Server.DefaultColor + " for a list of building commands.");
                            Player.SendMessage(p, "Use &b/help mod" + Server.DefaultColor + " for a list of moderation commands.");
                            Player.SendMessage(p, "Use &b/help homes" + Server.DefaultColor + " for a list of home commands.");
                            Player.SendMessage(p, "Use &b/help games" + Server.DefaultColor + " for a list of game commands.");
                            Player.SendMessage(p, "Use &b/help colors" + Server.DefaultColor + " for a list of chat colors.");
                            Player.SendMessage(p, "Use &b/help groups" + Server.DefaultColor + " for a list of player group commands.");
                            Player.SendMessage(p, "Use &b/help information" + Server.DefaultColor + " for a list of information commands.");
                            Player.SendMessage(p, "Use &b/help other" + Server.DefaultColor + " for a list of other commands.");
                            Player.SendMessage(p, "Use &b/help old" + Server.DefaultColor + " to view the Old help menu.");
                            Player.SendMessage(p, "Use &b/help [command] or /help [block] " + Server.DefaultColor + "to view more info.");
                            if (p != null && Server.devs.Contains(p.name.ToLower())) { Player.SendMessage(p, "Use &b/devcmd help" + Server.DefaultColor + " for a list of DevCmd commands."); }
                        } break;
                    case "ranks":
                        message = "";
                        bool devsOnline = false;
                        foreach (Player pl in Player.players) { if (Server.devs.Contains(pl.name.ToLower()) && !pl.hidden) { devsOnline = true; } }
                        foreach (Group grp in Group.GroupList)
                        {
                            if (grp.name != "nobody")
                                Player.SendMessage(p, grp.color + grp.name + " - &bCommand limit: " + grp.maxBlocks + " - &cPermission: " + (int)grp.Permission);
                        }
                        if (devsOnline) { Player.SendMessage(p, "&9Developer - &bCommand limit: Unlimited - &cPermission: 120"); }
                        break;
                    case "build":
                        message = "";
                        foreach (Command comm in Command.all.commands)
                        {
                            if (p == null || p.group.commands.All().Contains(comm))
                            {
                                if (comm.type.Contains("build")) message += ", " + getColor(comm.name) + comm.name;
                            }
                        }

                        if (message == "") { Player.SendMessage(p, "No commands of this type are available to you."); break; }
                        Player.SendMessage(p, "Building commands you may use:");
                        Player.SendMessage(p, message.Remove(0, 2) + ".");
                        break;
                    case "mod": case "moderation":
                        message = "";
                        foreach (Command comm in Command.all.commands)
                        {
                            if (p == null || p.group.commands.All().Contains(comm))
                            {
                                if (comm.type.Contains("mod")) message += ", " + getColor(comm.name) + comm.name;
                            }
                        }

                        if (message == "") { Player.SendMessage(p, "No commands of this type are available to you."); break; }
                        Player.SendMessage(p, "Moderation commands you may use:");
                        Player.SendMessage(p, message.Remove(0, 2) + ".");
                        break;
                    case "homes": case "pmaps":
                        message = "";
                        foreach (Command comm in Command.all.commands)
                        {
                            if (p == null || p.group.commands.All().Contains(comm))
                            {
                                if (comm.type.Contains("homes")) message += ", " + getColor(comm.name) + comm.name;
                            }
                        }

                        if (message == "") { Player.SendMessage(p, "No commands of this type are available to you."); break; }
                        Player.SendMessage(p, "Home commands you may use:");
                        Player.SendMessage(p, message.Remove(0, 2) + ".");
                        break;
                    case "groups":
                        message = "";
                        foreach (Command comm in Command.all.commands)
                        {
                            if (p == null || p.group.commands.All().Contains(comm))
                            {
                                if (comm.type.Contains("groups")) message += ", " + getColor(comm.name) + comm.name;
                            }
                        }

                        if (message == "") { Player.SendMessage(p, "No commands of this type are available to you."); break; }
                        Player.SendMessage(p, "Group commands you may use:");
                        Player.SendMessage(p, message.Remove(0, 2) + ".");
                        break;
                    case "information":
                        message = "";
                        foreach (Command comm in Command.all.commands)
                        {
                            if (p == null || p.group.commands.All().Contains(comm))
                            {
                                if (comm.type.Contains("info")) message += ", " + getColor(comm.name) + comm.name;
                            }
                        }

                        if (message == "") { Player.SendMessage(p, "No commands of this type are available to you."); break; }
                        Player.SendMessage(p, "Information commands you may use:");
                        Player.SendMessage(p, message.Remove(0, 2) + ".");
                        break;
                    case "other":
                        message = "";
                        foreach (Command comm in Command.all.commands)
                        {
                            if (p == null || p.group.commands.All().Contains(comm))
                            {
                                if (comm.type.Contains("other")) message += ", " + getColor(comm.name) + comm.name;
                            }
                        }

                        if (message == "") { Player.SendMessage(p, "No commands of this type are available to you."); break; }
                        Player.SendMessage(p, "Other commands you may use:");
                        Player.SendMessage(p, message.Remove(0, 2) + ".");
                        break;
                    /*case "short":
                        message = "";
                        foreach (Command comm in Command.all.commands)
                        {
                            if (p == null || p.group.commands.All().Contains(comm))
                            {
                                if (comm.shortcut != "") message += ", &b" + comm.shortcut + " " + Server.DefaultColor + "[" + comm.name + "]";
                            }
                        }
                        Player.SendMessage(p, "Available shortcuts:");
                        Player.SendMessage(p, message.Remove(0, 2) + ".");
                        break;*/ // Removed due to new 'Aliases' system for commands.
                    case "old":
                        string commandsFound = "";
                        foreach (Command comm in Command.all.commands)
                        {
                            if (p == null || p.group.commands.All().Contains(comm))
                            {
                                try { commandsFound += ", " + comm.name; } catch { }
                            }
                        }
                        Player.SendMessage(p, "Available commands:");
                        Player.SendMessage(p, commandsFound.Remove(0, 2));
                        Player.SendMessage(p, "Type \"/help <command>\" for more help.");
                        Player.SendMessage(p, "Type \"/help shortcuts\" for shortcuts.");
                        break;
                    case "game":
                    case "games":
                        message = "";
                        foreach (Command comm in Command.all.commands)
                        {
                            if (p == null || p.group.commands.All().Contains(comm))
                            {
                                if (comm.type.Contains("games")) message += ", " + getColor(comm.name) + comm.name;
                            }
                        }

                        if (message == "") { Player.SendMessage(p, "No commands of this type are available to you."); break; }
                        Player.SendMessage(p, "Game commands you may use:");
                        Player.SendMessage(p, message.Remove(0, 2) + ".");
                        break;
                    case "chat":
                    case "colors":
                        Player.SendMessage(p, "Chat Color Codes");
                        Player.SendMessage(p, "&1%%11 - Navy");
                        Player.SendMessage(p, "&2%%22 - Green");
                        Player.SendMessage(p, "&3%%33 - Teal");
                        Player.SendMessage(p, "&4%%44 - Maroon");
                        Player.SendMessage(p, "&5%%55 - Purple");
                        Player.SendMessage(p, "&6%%66 - Gold");
                        Player.SendMessage(p, "&7%%77 - Silver");
                        Player.SendMessage(p, "&8%%88 - Gray");
                        Player.SendMessage(p, "&9%%99 - Blue");
                        Player.SendMessage(p, "&a%%aa - Lime");
                        Player.SendMessage(p, "&b%%bb - Aqua");
                        Player.SendMessage(p, "&c%%cc - Red");
                        Player.SendMessage(p, "&d%%dd - Pink");
                        Player.SendMessage(p, "&e%%ee - Yellow");
                        Player.SendMessage(p, "&f%%ff - White");
                        break;
                    default:
                        Command cmd = Command.all.Find(message);
                        if (cmd != null)
                        {
                            cmd.Help(p);
                            string foundRank = Level.PermissionToName(GrpCommands.allowedCommands.Find(grpComm => grpComm.commandName == cmd.name).lowestRank);
                            Player.SendMessage(p, "Rank needed: " + getColor(cmd.name) + foundRank);
                            int count = 0; string aliases = "";
                            // Le foreach no work for some reason, so I did le old fashioned for loop :P
                            for (int i = 0; i < cmd.aliases.Length; i++) 
                            { 
                                if (cmd.aliases[i] != "") 
                                {
                                    count++;
                                    if (i + 1 < cmd.aliases.Length) { aliases += cmd.aliases[i] + ", "; }
                                    else { aliases += cmd.aliases[i]; }
                                } 
                            }
                            if (count > 0) { Player.SendMessage(p, "Aliases: &a" + aliases); }
                            else { Player.SendMessage(p, "Aliases: &anone"); }
                            return;
                        }
                        byte b = Block.Byte(message);
                        if (b != Block.Zero)
                        {
                            Player.SendMessage(p, "Block \"" + message + "\" appears as &b" + Block.Name(Block.Convert(b)));
                            string foundRank = Level.PermissionToName(Block.BlockList.Find(bs => bs.type == b).lowestRank);
                            Player.SendMessage(p, "Rank needed: " + foundRank);
                            return;
                        }
                        Player.SendMessage(p, "Could not find command or block specified.");
                        break;
                }
            }
            catch (Exception e) { Server.ErrorLog(e); Player.SendMessage(p, "An error occured"); }
        }

        private string getColor(string commName)
        {
            foreach (GrpCommands.rankAllowance aV in GrpCommands.allowedCommands)
            {
                if (aV.commandName == commName)
                {
                    if (Group.findPerm(aV.lowestRank) != null)
                        return Group.findPerm(aV.lowestRank).color;
                }
            }

            return "&f";
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "...really? Wow. Just...wow.");
        }
    }
}