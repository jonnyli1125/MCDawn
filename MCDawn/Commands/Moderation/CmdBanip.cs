using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
//using MySql.Data.MySqlClient;
//using MySql.Data.Types;

namespace MCDawn
{
    public class CmdBanip : Command
    {
        public override string name { get { return "banip"; } }
        public override string[] aliases { get { return new string[] { "bi" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdBanip() { }

        public override void Use(Player p, string message)
        {
            string usingName = "";
            if (message == "") { Help(p); return; }
            if (message[0] == '@')
            {
                usingName = message.Remove(0, 1).Trim();
                message = message.Remove(0, 1).Trim();
                message = message.Replace("'", "\\'");
                message = message.Replace("--", "");
                Player who = Player.Find(message);
                if (Server.devs.Contains(message.ToLower()))
                {
                    Player.SendMessage(p, "Woah!! You can't ban a MCDawn Developer!");
                    if (p != null) Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to ban a MCDawn Developer!");
                    else Player.GlobalMessage("The Console is crazy! Trying to ban a MCDawn Developer!");
                    return;
                }
                if (Server.staff.Contains(message.ToLower()))
                {
                    Player.SendMessage(p, "Woah!! You can't ban a MCDawn Staff Member!");
                    if (p != null) Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to ban a MCDawn Staff Member!");
                    else Player.GlobalMessage("The Console is crazy! Trying to ban a MCDawn Staff Member!");
                    return;
                }
                if (Server.administration.Contains(message.ToLower()))
                {
                    Player.SendMessage(p, "Woah!! You can't ban a MCDawn Administrator!");
                    if (p != null) Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to ban a MCDawn Administrator!");
                    else Player.GlobalMessage("The Console is crazy! Trying to ban a MCDawn Administrator!");
                    return;
                }
                if (who == null)
                {
                    DataTable ip;
                    int tryCounter = 0;
            rerun:  try
                    {
                        ip = MySQL.fillData("SELECT IP FROM Players WHERE Name = '" + message + "'");
                    }
                    catch (Exception e)
                    {
                        tryCounter++;
                        if (tryCounter < 10)
                            goto rerun;
                        else
                        {
                            Server.ErrorLog(e);
                            return;
                        }
                    }
                    if (ip.Rows.Count > 0)
                        message = ip.Rows[0]["IP"].ToString();
                    else
                    {
                        Player.SendMessage(p, "Unable to find an IP address for that user.");
                        return;
                    }
                    ip.Dispose();
                }
                else
                {
                    message = who.ip;
                }
            }
            else
            {
                Player who = Player.Find(message);
                if (who != null)
                    message = who.ip;
            }

            if (message.Contains("127.0.0.") || message.Contains("192.168.")) { Player.SendMessage(p, "You can't ip-ban the server!"); return; }
            if (message.IndexOf('.') == -1) { Player.SendMessage(p, "Invalid IP!"); return; }
            if (message.Split('.').Length != 4) { Player.SendMessage(p, "Invalid IP!"); return; }
            if (p != null && p.ip == message) { Player.SendMessage(p, "You can't ip-ban yourself.!"); return; }
            if (Server.bannedIP.Contains(message)) { Player.SendMessage(p, message + " is already ip-banned."); return; }

            // Check if IP belongs to an op+
            // First get names of active ops+ with that ip
            List<string> opNamesWithThatIP = (from pl in Player.players where (pl.ip == message && pl.@group.Permission >= LevelPermission.Operator) select pl.originalName).ToList();
            List<string> devCheck = (from pl in Player.players where (pl.ip == message && (Server.devs.Contains(pl.originalName.ToLower()) || Server.staff.Contains(pl.originalName.ToLower()) || Server.administration.Contains(pl.originalName.ToLower()))) select pl.originalName).ToList();
            // Next, add names from the database
            DataTable dbnames = MySQL.fillData("SELECT Name FROM Players WHERE IP = '" + message + "'");

            foreach (DataRow row in dbnames.Rows)
            {
                opNamesWithThatIP.Add(row[0].ToString());
            }

            foreach (DataRow row in dbnames.Rows)
            {
                devCheck.Add(row[0].ToString());
            }

            if (devCheck != null && devCheck.Count > 0)
            {
                foreach (string dev in devCheck)
                {
                    if (Server.devs.Contains(dev.ToLower()))
                    {
                        Player.SendMessage(p, "Woah!! You can't ban a MCDawn Developer!");
                        if (p != null) Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to ban a MCDawn Developer!");
                        else Player.GlobalMessage("The Console is crazy! Trying to ban a MCDawn Developer!");
                        return;
                    }
                    if (Server.staff.Contains(dev.ToLower()))
                    {
                        Player.SendMessage(p, "Woah!! You can't ban a MCDawn Staff Member!");
                        if (p != null) Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to ban a MCDawn Staff Member!");
                        else Player.GlobalMessage("The Console is crazy! Trying to ban a MCDawn Staff Member!");
                        return;
                    }
                    if (Server.administration.Contains(dev.ToLower()))
                    {
                        Player.SendMessage(p, "Woah!! You can't ban a MCDawn Administrator!");
                        if (p != null) Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to ban a MCDawn Administrator!");
                        else Player.GlobalMessage("The Console is crazy! Trying to ban a MCDawn Administrator!");
                        return;
                    }
                }
            }

            if (opNamesWithThatIP != null && opNamesWithThatIP.Count > 0)
            {
                // We have at least one op+ with a matching IP
                // Check permissions of everybody who matched that IP
                foreach (string opname in opNamesWithThatIP)
                {
                    // If one of these guys is a dev, don't allow the ipban to proceed! 
                    if (Server.devs.Contains(opname.ToLower()))
                    {
                        Player.SendMessage(p, "Woah!! You can't ban a MCDawn Developer!");
                        if (p != null) Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to ban a MCDawn Developer!");
                        else Player.GlobalMessage("The Console is crazy! Trying to ban a MCDawn Developer!");
                        return;
                    }
                    if (Server.staff.Contains(opname.ToLower()))
                    {
                        Player.SendMessage(p, "Woah!! You can't ban a MCDawn Staff Member!");
                        if (p != null) Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to ban a MCDawn Staff Member!");
                        else Player.GlobalMessage("The Console is crazy! Trying to ban a MCDawn Staff Member!");
                        return;
                    }
                    if (Server.administration.Contains(opname.ToLower()))
                    {
                        Player.SendMessage(p, "Woah!! You can't ban a MCDawn Administrator!");
                        if (p != null) Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to ban a MCDawn Administrator!");
                        else Player.GlobalMessage("The Console is crazy! Trying to ban a MCDawn Administrator!");
                        return;
                    }
                    // Console can ban anybody else, so skip this section
                    if (p != null)
                    {
                        // If one of these guys matches a player with a higher rank don't allow the ipban to proceed! 
                        Group grp = Group.findPlayerGroup(opname);
                        if (grp != null)
                        {
                            if (grp.Permission >= p.group.Permission)
                            {
                                Player.SendMessage(p, "You can only IP-ban IPs of players with a lower rank.");
                                Player.SendMessage(p, Server.DefaultColor + opname + "(" + grp.color + grp.name + Server.DefaultColor + ") also has the IP of " + message + ".");
                                Server.s.Log(p.name + " attempted to IP-ban " + message + " [Player " + opname + "(" + grp.name + ") also has the IP of " + message + "]");
                                return;
                            }
                        }
                    }
                }
            }



            if (p != null)
            {
                IRCBot.Say(message.ToLower() + " is now ip-banned by " + p.name + ".");
                Server.s.Log("IP-BANNED: " + message.ToLower() + " by " + p.name + ".");
                Player.GlobalMessage(message + " is now &8ip-banned" + Server.DefaultColor + " by " + p.color + p.name + Server.DefaultColor + ".");
            }
            else
            {
                IRCBot.Say(message.ToLower() + " is now ip-banned by Console.");
                Server.s.Log("IP-BANNED: " + message.ToLower() + " by Console.");
                Player.GlobalMessage(message + " is now &8ip-banned" + Server.DefaultColor + " by Console.");
            }
            Server.bannedIP.Add(message);
            Server.bannedIP.Save("banned-ip.txt", false);

            /*try { 
                Player.players.ForEach(delegate(Player pl) { 
                    if (((Player.Find(usingName).ip == pl.ip && pl.name.ToLower() != usingName.ToLower()) || (message == pl.ip)) && pl.group.Permission < p.group.Permission) 
                        pl.Kick("Kicked by ip-ban!"); 
                }); 
            }
            catch { } // try catched for possible enumeration error >.>*/
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/banip <ip/name> - Bans an ip. Also accepts a player name when you use @ before the name.");
        }
    }
}