// Written by jonnyli1125 for MCDawn only.
using System;
using System.Collections.Generic;
using System.IO;

namespace MCDawn
{
    class CmdChatroom : Command
    {
        public override string name { get { return "chatroom"; } }
        public override string[] aliases { get { return new string[] { "cr" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdChatroom() { }

        public override void Use(Player p, string message)
        {
            //if (!Directory.Exists("extra/chatrooms")) { Directory.CreateDirectory("extra/chatrooms"); }
            switch (message.Split(' ')[0].ToLower())
            {
                case "list":
                    if (message.Trim().Split(' ').Length == 1) 
                    {
                        List<string> chatrooms = new List<string>();
                        Player.players.ForEach(delegate(Player pl) { if (pl.chatroom != "") { if (!chatrooms.Contains(pl.chatroom)) { chatrooms.Add(pl.chatroom); } } });
                        if (chatrooms.Count > 0)
                        {
                            Player.SendMessage(p, "All active chatrooms:");
                            chatrooms.ForEach(delegate(string room) { Player.SendMessage(p, "&b" + room); });
                        }
                        else { Player.SendMessage(p, "No chatrooms are active."); }
                    }
                    else if (message.Trim().Split(' ').Length == 2)
                    {
                        bool found = false;
                        string cname = "";
                        Player.players.ForEach(delegate(Player pl) { if (pl.chatroom.ToLower() == message.Split(' ')[1].ToLower()) { found = true; cname = pl.chatroom; } });
                        if (!found) { Player.SendMessage(p, "No active chatrooms found with that name."); return; }
                        Player.SendMessage(p, "All players in the chatroom &b" + cname);
                        Player.players.ForEach(delegate(Player pl) { if (pl.chatroom.ToLower() == message.Trim().Split(' ')[1].ToLower()) { Player.SendMessage(p, pl.color + pl.name); } });
                    }
                    else { Help(p); return; }
                    break;
                case "join":
                    if (message.Split(' ')[1] == "") { Player.SendMessage(p, "Please specify a chatroom name."); return; }
                    if (message.Split(' ')[1] == "!") { Player.SendMessage(p, "Channel name is erranous."); return; }
                    if (p.chatroom.ToLower() == "map" || p.chatroom.ToLower() == "level" || p.chatroom.ToLower() == "world") { if (!p.levelchat) { Command.all.Find("private").Use(p, ""); } return; }
                    //p.chatroomOp = false; p.chatroomAdmin = false; bool banned = false;
                    /*if (File.Exists("extra/chatrooms/" + p.chatroom.ToLower() + ".txt"))
                    {
                        string[] lines = File.ReadAllLines("extra/chatrooms/" + p.chatroom.ToLower() + ".txt");
                        foreach (string s in lines)
                        {
                            if (s.StartsWith(p.name + " ") && s.Split(' ')[1].ToLower() == "admin") { p.chatroomAdmin = true; }
                            else if (s.StartsWith(p.name + " ") && s.Split(' ')[1].ToLower() == "operator") { p.chatroomOp = true; }
                            else if (s.StartsWith(p.name + " ") && s.Split(' ')[1].ToLower() == "banned") { banned = true; }
                        }
                    }
                    else
                    {
                        bool notEmpty = false;
                        Player.players.ForEach(delegate(Player pl) { if (pl.chatroom != "" && pl.chatroom == p.chatroom && pl != p) { notEmpty = true; } });
                        if (!notEmpty) { p.chatroomOp = true; }
                    }*/
                    //if (banned) { Player.SendMessage(p, "You are banned from this chatroom."); return; }
                    bool pub = true;
                    foreach (Player pl in Player.players) { if (pl.chatroom != "" && (pl.chatroom[0] == '!' && pl.chatroom.ToLower() == message.Split(' ')[1].ToLower())) { pub = false; } }
                    if (!pub) { Player.SendMessage(p, "Chatroom is invite only."); return; }
                    if (p.chatroom != "") { Command.all.Find("chatroom").Use(p, "leave"); }
                    p.chatroom = message.Split(' ')[1];
                    Player.SendMessage(p, "Joined chatroom &b" + p.chatroom + "&g.");
                    Player.ChatroomMessage(p.chatroom, "&b<" + p.chatroom + "> " + p.color + p.displayName + "&g joined the chatroom.");
                    break;
                case "leave":
                    if (p.chatroom.ToLower() == "map" || p.chatroom.ToLower() == "level" || p.chatroom.ToLower() == "world") { if (p.levelchat) { Command.all.Find("private").Use(p, ""); } return; }
                    if (p.chatroom == "") { Player.SendMessage(p, "You are not currently in a chatroom."); return; }
                    //string oldchatroom = p.chatroom; int count = 0; List<string> curplayers = new List<string>();
                    /*if (!File.Exists("extra/chatrooms/" + p.chatroom.ToLower() + ".txt"))  
                    { 
                        curplayers.Clear();
                        foreach (Player pl in Player.players) { if (pl.chatroom.ToLower() == oldchatroom.ToLower()) { count++; curplayers.Add(pl.name); } } 
                        if (count > 0)
                        {
                            Player newOp = Player.Find(curplayers[0]);
                            newOp.chatroomOp = true;
                        }
                    }*/
                    Player.ChatroomMessage(p.chatroom, "&b<" + p.chatroom + "> " + p.color + p.displayName + "&g left the chatroom.");
                    Player.SendMessage(p, "Left chatroom &b" + p.chatroom + "&g.");
                    p.chatroom = "";
                    break;
                case "invite":
                    Player who = Player.Find(message.Split(' ')[1]);
                    if (message.Split(' ')[1] == "") { Player.SendMessage(p, "Please specify a player name."); return; }
                    if (who == null) { Player.SendMessage(p, "Player could not be found."); return; }
                    if (p.chatroom[0] != '!') { Player.SendMessage(p, "Chatroom is public, invites are disabled."); return; }
                    who.chatroomInvite = p.chatroom;
                    Player.SendMessage(who, "You have recieved an invite to join the chatroom &b" + p.chatroom.Remove(0, 1) + "&g. Type &1/chatroom accept&g to accept this invitation.");
                    Player.SendMessage(p, "Invite sent to " + who.color + who.name + "&g.");
                    break;
                case "accept":
                    if (p.chatroomInvite == "") { Player.SendMessage(p, "Nobody has sent a chatroom invite to you."); return; }
                    if (p.chatroom != "") { Command.all.Find("chatroom").Use(p, "leave"); }
                    p.chatroom = p.chatroomInvite;
                    Player.SendMessage(p, "Joined chatroom &b" + p.chatroom + "&g.");
                    Player.ChatroomMessage(p.chatroom, "&b<" + p.chatroom + "> " + p.color + p.displayName + "&g joined the chatroom.");
                    break;
                /*case "register": // Meh... lol, couldnt get teh OP/Admin derps fixed, so, removing for nao.
                    if (p.chatroom == "") { Player.SendMessage(p, "You are not currently in a chatroom."); return; } 
                    if (!p.chatroomOp && !p.chatroomAdmin) { Player.SendMessage(p, "Must be Channel OP+ to use this."); return; }
                    if (!File.Exists("extra/chatrooms/" + p.chatroom.ToLower() + ".txt"))
                    {
                        File.WriteAllText("extra/chatrooms/" + p.chatroom.ToLower() + ".txt", p.name + " admin");
                        Player.SendMessage(p, "You have registered the chatroom &b" + p.chatroom + "&g.");
                        Server.s.Log(p.name + " registered the chatroom " + p.chatroom + ".");
                    }
                    else { Player.SendMessage(p, "This chatroom is already registered."); return; }
                    break;
                case "drop":
                    if (p.chatroom == "") { Player.SendMessage(p, "You are not currently in a chatroom."); return; } 
                    if (!p.chatroomAdmin) { Player.SendMessage(p, "Must be Channel Admin to use this."); return; }
                    if (File.Exists("extra/chatrooms/" + p.chatroom.ToLower() + ".txt"))
                    {
                        foreach (Player pl in Player.players)
                        {
                            if (pl.chatroom.ToLower() == p.chatroom.ToLower() && (pl.chatroomAdmin)) 
                            { 
                                pl.chatroomAdmin = false;
                            }
                            else if (pl.chatroom.ToLower() == p.chatroom.ToLower() && (pl.chatroomOp))
                            {
                                pl.chatroomOp = false;
                            }
                        }
                        File.Delete("extra/chatrooms/" + p.chatroom.ToLower() + ".txt");
                        Player.SendMessage(p, "You have dropped the chatroom &b" + p.chatroom + "&g.");
                        Server.s.Log(p.name + " dropped the chatroom " + p.chatroom + ".");
                    }
                    else { Player.SendMessage(p, "This chatroom hasn't been registered."); return; }
                    break;
                case "op":
                    if (p.chatroom == "") { Player.SendMessage(p, "You are not currently in a chatroom."); return; }
                    if (!p.chatroomAdmin) { Player.SendMessage(p, "Must be Channel Admin to use this."); return; }
                    Player who3 = Player.Find(message.Split(' ')[1]);
                    if (who3 == null) { Player.SendMessage(p, "Player could not be found."); return; }
                    Player.ChatroomMessage(p.chatroom, who3.color + who3.name + " has been opped by " + Player.ChatroomName(p));
                    string[] persons = File.ReadAllLines("extra/chatrooms/" + p.chatroom.ToLower() + ".txt");
                    List<string> listnew = new List<string>();
                    foreach (string person in persons) { listnew.Add(person); }
                    listnew.Add(who3.name + " operator");
                    string[] peoplesnew = listnew.ToArray();
                    File.WriteAllLines("extra/chatrooms/" + p.chatroom.ToLower() + ".txt", peoplesnew);
                    who3.chatroomOp = true;
                    break;
                case "deop":
                    if (p.chatroom == "") { Player.SendMessage(p, "You are not currently in a chatroom."); return; }
                    if (!p.chatroomAdmin) { Player.SendMessage(p, "Must be Channel Admin to use this."); return; }
                    Player plr = Player.Find(message.Split(' ')[1]);
                    if (plr == null) { Player.SendMessage(p, "Player could not be found."); return; }
                    Player.ChatroomMessage(p.chatroom, plr.color + plr.name + " has been deopped by " + Player.ChatroomName(p));
                    string[] array = File.ReadAllLines("extra/chatrooms/" + p.chatroom.ToLower() + ".txt");
                    List<string> newList = new List<string>();
                    foreach (string person in newList) { newList.Add(person); }
                    newList.Remove(plr.name + " operator");
                    string[] newPeoples = newList.ToArray();
                    File.WriteAllLines("extra/chatrooms/" + p.chatroom.ToLower() + ".txt", newPeoples);
                    plr.chatroomOp = false;
                    break;
                case "kick":
                    if (p.chatroom == "") { Player.SendMessage(p, "You are not currently in a chatroom."); return; }
                    if (!p.chatroomOp && !p.chatroomAdmin) { Player.SendMessage(p, "Must be Channel OP+ to use this."); return; }
                    Player who = Player.Find(message.Split(' ')[1]);
                    if (who == null) { Player.SendMessage(p, "Player could not be found."); return; }
                    if (who.group.Permission >= p.group.Permission || (Server.devs.Contains(who.name.ToLower()) || Server.staff.Contains(who.name.ToLower()) || Server.administration.Contains(who.name.ToLower()))) { Player.SendMessage(p, "Cannot kick players of higher or equal rank."); return; }
                    Player.SendMessage(who, "You have been kicked from chatroom &b" + who.chatroom  + "&g by " + Player.ChatroomName(p) + "&g.");
                    who.chatroom = "";
                    Player.ChatroomMessage(p.chatroom, who.color + who.name + " has been kicked from the chatroom by " + Player.ChatroomName(p));
                    break;
                case "ban":
                    if (p.chatroom == "") { Player.SendMessage(p, "You are not currently in a chatroom."); return; }
                    if (!p.chatroomOp && !p.chatroomAdmin) { Player.SendMessage(p, "Must be Channel OP+ to use this."); return; }
                    Player who2 = Player.Find(message.Split(' ')[1]); // Don't judge me, I'm lazy and I wrote it wrong, lol :/
                    if (who2 == null) { Player.SendMessage(p, "Player could not be found."); return; }
                    if (who2.group.Permission >= p.group.Permission || (Server.devs.Contains(who2.name.ToLower()) || Server.staff.Contains(who2.name.ToLower()) || Server.administration.Contains(who2.name.ToLower()))) { Player.SendMessage(p, "Cannot ban players of higher or equal rank."); return; }
                    Player.SendMessage(who2, "You have been banned from chatroom &b" + who2.chatroom  + "&g by " + Player.ChatroomName(p) + "&g.");
                    who2.chatroom = "";
                    Player.ChatroomMessage(p.chatroom, who2.color + who2.name + " has been banned from the chatroom by " + Player.ChatroomName(p));
                    string[] peoples = File.ReadAllLines("extra/chatrooms/" + p.chatroom.ToLower() + ".txt");
                    List<string> newlist = new List<string>();
                    foreach (string person in peoples) { newlist.Add(person); }
                    newlist.Add(who2.name + " banned");
                    string[] newpeoples = newlist.ToArray();
                    File.WriteAllLines("extra/chatrooms/" + p.chatroom.ToLower() + ".txt", newpeoples);
                    break;
                case "unban":
                    if (p.chatroom == "") { Player.SendMessage(p, "You are not currently in a chatroom."); return; }
                    if (!p.chatroomOp && !p.chatroomAdmin) { Player.SendMessage(p, "Must be Channel Admin to use this."); return; }
                    Player plr2 = Player.Find(message.Split(' ')[1]); // Don't judge me, I'm lazy and I wrote it wrong, lol :/
                    if (plr2 == null) { Player.SendMessage(p, "Player could not be found."); return; }
                    Player.SendMessage(plr2, "You have been unbanned from chatroom &b" + plr2.chatroom  + "&g by " + Player.ChatroomName(p) + "&g.");
                    Player.ChatroomMessage(p.chatroom, plr2.color + plr2.name + " has been unbanned from the chatroom by " + Player.ChatroomName(p));
                    string[] ppls = File.ReadAllLines("extra/chatrooms/" + p.chatroom.ToLower() + ".txt");
                    List<string> list = new List<string>();
                    foreach (string person in ppls) { list.Add(person); }
                    list.Remove(plr2.name + " banned");
                    string[] newppls = list.ToArray();
                    File.WriteAllLines("extra/chatrooms/" + p.chatroom.ToLower() + ".txt", newppls);
                    break;*/
                case "":
                default:
                    Help(p);
                    break;
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/chatroom list - Shows a list of all current active chatrooms.");
            Player.SendMessage(p, "/chatroom list <chatroom> - Shows a list of all players in <chatroom>.");
            Player.SendMessage(p, "/chatroom join <chatroom> - Join a chatroom. If <chatroom> starts with !, the channel will be invite only.");
            Player.SendMessage(p, "/chatroom leave - Leaves the current chatroom you are in.");
            Player.SendMessage(p, "/chatroom invite <player> - Invite player to your current chatroom.");
            Player.SendMessage(p, "/chatroom accept - Accept last invitation sent to you to join a chatroom.");
            //Player.SendMessage(p, "/chatroom register - Registers the current chatroom you're in (Channel OP+).");
            //Player.SendMessage(p, "/chatroom drop - Drop registration of the current chatroom you're in (Channel Admin).");
            //Player.SendMessage(p, "/chatroom op <player> - Op <player> from chatroom (Channel Admin).");
            //Player.SendMessage(p, "/chatroom deop <player> - Deop <player> from chatroom (Channel Admin).");
            //Player.SendMessage(p, "/chatroom kick <player> - Kick <player> from chatroom (Channel OP+).");
            //Player.SendMessage(p, "/chatroom ban <player> - Ban <player> from chatroom (Channel OP+).");
            Player.SendMessage(p, "This is basically an in-game, mini-IRC system that allows users to talk privately in chatrooms, and including moderation tools and commands within each chatroom also.");
        }
    }
}
