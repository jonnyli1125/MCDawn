using System;

namespace MCDawn
{
    public class CmdGlobal : Command
    {
        public override string name { get { return "global"; } }
        public override string[] aliases { get { return new string[] { "gl", "gc" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdGlobal() { }
        public override void Use(Player p, string message)
        {
            string[] args = message.Split(new char[] { ' ' }, 2);
            if (message == "") { Help(p); return; }
            if (!Server.useglobal) { Player.SendMessage(p, "MCDawn Global Chat has been disabled by the Server Owner."); return; }
            if (!GlobalChatBot.IsConnected()) { Player.SendMessage(p, "Global Chat Bot has been disconnected, attempting to reconnect now..."); GlobalChatBot.Reset(); return; }
            // Console Global
            if (p == null)
            {
                if (Server.GlobalBanned().Contains("console/" + Server.GetIPAddress()))
                {
                    try { if (!Server.cli) { MCDawn.Gui.Window.thisWindow.WriteGlobalLine("You have been Global-Banned. Visit www.mcdawn.com for appeal."); } }
                    catch { }
                    Server.s.Log("You have been Global-Banned. Visit www.mcdawn.com for appeal.");
                    return;
                }
                if (Server.profanityFilter == true) { if (Server.profanityFilterOp) { if (p.group.Permission >= LevelPermission.Operator) { message = ProfanityFilter.Filter(null, message); } } }
                GlobalChatBot.Say("Console [" + Server.ZallState + "]: " + message);
                GCMessage(message);
                try { if (!Server.cli) { MCDawn.Gui.Window.thisWindow.WriteGlobalLine("<[Global] Console [" + Server.ZallState + "]: " + message); } }
                catch { }
                Server.s.Log("<[Global] Console [" + Server.ZallState + "]: " + message);
                return;
            }
            //if (message.Contains("%") || message.Contains("&") || message.ToLower().Contains("$color")) { p.SendMessage("Percents and Color Codes are not allowed in Global Chat."); return; }

            bool globalBanned = false;
            for (int i = 0; i < Server.GlobalBanned().Count; i++)
                if (Server.GlobalBanned()[i].Contains("*") && (p.name.ToLower().StartsWith(Server.GlobalBanned()[i].ToLower().Replace("*", "")) || p.ip.StartsWith(Server.GlobalBanned()[i].ToLower().Replace("*", ""))))
                    globalBanned = true;

            if ((Server.GlobalBanned().Contains(p.ip) || Server.GlobalBanned().Contains(p.name.ToLower()) || globalBanned) && p != null) { p.SendMessage("You have been Global-Banned. Visit www.mcdawn.com for appeal."); return; }
            if (p != null && p.muted) { Player.SendMessage(p, "You are muted."); return; }
            if (p != null && Server.ignoreGlobal.Contains(p.name.ToLower())) { Player.SendMessage(p, "You currently have Global Chat ignored."); return; }

            // Profanity Filter
            if (Server.profanityFilter == true)
            {
                if (!Server.profanityFilterOp) { if (p.group.Permission < LevelPermission.Operator) { message = ProfanityFilter.Filter(p, message); } }
                else { message = ProfanityFilter.Filter(p, message); }
            }

            GlobalChatBot.Say(p.name + ": " + message);
            GCMessage(p, message);

            if (Server.devs.Contains(p.name.ToLower())) { Server.s.Log("<[Global] [Developer] " + p.name + ": " + message); }
            else if (Server.staff.Contains(p.name.ToLower())) { Server.s.Log("<[Global] [MCDawn Staff] " + p.name + ": " + message); }
            else if (Server.administration.Contains(p.name.ToLower())) { Server.s.Log("<[Global] " + "[Administrator] " + p.name + ": " + message); }
            else { Server.s.Log("<[Global] " + p.name + ": " + message); }

            try
            {
                if (!Server.cli)
                {
                    if (Server.devs.Contains(p.name.ToLower())) { MCDawn.Gui.Window.thisWindow.WriteGlobalLine("<[Global] [Developer] " + p.name + ": " + message); }
                    else if (Server.staff.Contains(p.name.ToLower())) { MCDawn.Gui.Window.thisWindow.WriteGlobalLine("<[Global] [MCDawn Staff] " + p.name + ": " + message); }
                    else if (Server.administration.Contains(p.name.ToLower())) { MCDawn.Gui.Window.thisWindow.WriteGlobalLine("<[Global] " + "[Administrator] " + p.name + ": " + message); }
                    else { MCDawn.Gui.Window.thisWindow.WriteGlobalLine("<[Global] " + p.name + ": " + message); }
                }
            }
            catch { }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/global [message] - Send a message to every MCDawn Server (Global Chat).");
            //Player.SendMessage(p, "/global toggle - Toggle talking into Global Chat or Server Chat.");
        }
        public void GCMessage(Player p, string message)
        {
            foreach (Player pl in Player.players)
            {
                //Server.UpdateGlobalBan(); Server.UpdateOmniBan();
                if (!Server.ignoreGlobal.Contains(pl.name.ToLower()) && (!pl.ignoreList.Contains(p.name.ToLower()) && p != null) && (!Server.GlobalBanned().Contains(p.name.ToLower()) && p != null) && (!pl.ignoreList.Contains(p.name.ToLower()) && p != null) && (!Server.OmniBanned().Contains(p.name.ToLower())) && p != null)
                {
                    if (p == null) { pl.SendMessage("<[Global] Console [&a" + Server.ZallState + "&g]: &f" + message); return; }
                    if (Server.devs.Contains(p.name.ToLower()) && p != null) { pl.SendMessage("<[Global] " + p.color + "[" + p.titlecolor + "Developer" + p.color + "] " + p.name + ": &f" + message); }
                    else if (Server.staff.Contains(p.name.ToLower()) && p != null) { pl.SendMessage("<[Global] " + p.color + "[" + p.titlecolor + "MCDawn Staff" + p.color + "] " + p.name + ": &f" + message); }
                    else if (Server.administration.Contains(p.name.ToLower()) && p != null) { pl.SendMessage("<[Global] " + p.color + "[" + p.titlecolor + "Administrator" + p.color + "] " + p.name + ": &f" + message); }
                    else { pl.SendMessage("<[Global] " + p.color + p.name + ": &f" + message); }
                }
            }
        }
        public void GCMessage(string message)
        {
            foreach (Player pl in Player.players)
            {
                if (!Server.ignoreGlobal.Contains(pl.name.ToLower()))
                {
                    pl.SendMessage("<[Global] Console [&a" + Server.ZallState + "&g]: &f" + message);
                }
            }
        }
    }
}