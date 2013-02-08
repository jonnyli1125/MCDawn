using System;
using System.Collections.Generic;
using System.Text;

namespace MCDawn
{
    class CmdReview : Command
    {
        public override string name { get { return "review"; } }
        public override string[] aliases { get { return new string[] { "rev" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdReview() { }

        public override void Use(Player p, string message)
        {
            // For /review next permissions, that way the message only goes to people who can use promote or setrank.
            Command promote = Command.all.Find("promote");
            Command setrank = Command.all.Find("setrank");
            switch (message.ToLower())
            {
                case "":
                    if (p == null) { p.SendMessage("Command not usable from Console."); return; }
                    if (p.usereview == true)
                    {
                        Server.reviewlist.Add(p.name);
                        int reviewlistpos = Server.reviewlist.IndexOf(p.name);
                        Player.SendMessage(p, "You have requested a review and have entered the Review Queue.");
                        Player.SendMessage(p, "Please wait by your build, someone should be with you soon.");
                        foreach (Player pl in Player.players)
                        {
                            if (pl.group.Permission >= Server.opchatperm && (pl.group.CanExecute(promote) || pl.group.CanExecute(setrank)))
                            {
                                Player.SendMessage(pl, "To Ops: " + p.color + p.name + "&g has been added to the review queue.");
                                Player.SendMessage(pl, " Type &3/review next");
                            }
                        }
                        //Player.GlobalMessageOps("To Ops: " + p.color + p.name + "&g has been added to the review queue.");
                        //Player.GlobalMessageOps(" Type &3/review next");
                        p.usereview = false;
                    }
                    else { Player.SendMessage(p, "You have already entered the review queue!"); }
                    break;

                case "list":
                    if (p == null)
                    {
                        if (Server.reviewlist.Count != 0)
                        {
                            Player.SendMessage(p, "Players in the review queue:");
                            string revlist = "";
                            foreach (string str in Server.reviewlist) { revlist += str + ", "; }
                            Player.SendMessage(p, revlist);
                        }
                        else { Player.SendMessage(p, "There are no players in the review queue!"); }
                        return;
                    }
                    if (p != null)
                    {
                        if (Server.reviewlist.Count != 0)
                        {
                            Player.SendMessage(p, "Players in the review queue:");
                            string revlist = "";
                            if (Server.reviewlist.Count == 1) { foreach (string str in Server.reviewlist) { revlist += str; } }
                            else { foreach (string str in Server.reviewlist) { revlist += str + ", "; } }
                            Player.SendMessage(p, revlist);
                        }
                        else { Player.SendMessage(p, "There are no players in the review queue!"); }
                    }
                    break;

                case "remove":
                    if (p == null) { Player.SendMessage(p, "Command not avaible in Console."); }
                    else
                    {
                        bool alreadyrev = false;
                        foreach (string str in Server.reviewlist) { if (str == p.name) { alreadyrev = true; } }
                        if (!alreadyrev) { Player.SendMessage(p, "You have not requested a review yet!"); }
                        Server.reviewlist.Remove(p.name);
                        Player.SendMessage(p, "You have removed yourself from the review queue!");
                        p.usereview = true;
                    }
                    break;
                case "next":
                    if (p == null) { Player.SendMessage(p, "Command not usable in Console."); return; }
                    if (p.group.Permission >= Server.reviewnext && (p.group.CanExecute(promote) || p.group.CanExecute(setrank)))
                    {
                        if (Server.reviewlist.Count == 0) { Player.SendMessage(p, "There are no players in the review queue!"); return; }
                        string[] user = Server.reviewlist.ToArray();
                        Player who = Player.Find(user[0]);
                        if (who == null)
                        {
                            Player.SendMessage(p, "Player's review has already been answered or is offline!");
                            Player.SendMessage(p, "Player has been removed from the review queue.");
                            Server.reviewlist.Remove(user[0]);
                            return;
                        }
                        if (who == p)
                        {
                            Player.SendMessage(p, "You can't review yourself!");
                            Player.SendMessage(p, "You have been removed from the review queue.");
                            Server.reviewlist.Remove(user[0]);
                            return;
                        }
                        Server.reviewlist.Remove(user[0]);
                        Command.all.Find("tp").Use(p, who.name);
                        Player.GlobalMessageOps("To Ops: &3REVIEW: " + p.color + p.name + "&g is currently reviewing " + who.color + who.name);
                        Player.SendMessage(p, "You are reviewing " + who.group.color + user[0] + " (" + who.group.name + ")");
                        Player.SendMessage(p, "If you dont like the build, you may decline with &4/decline <player>.");
                        Player.SendMessage(who, "Your build is currently being reviewed by " + p.color + p.name + ".");
                        who.usereview = true;
                    }
                    else 
                    { 
                        Player.SendMessage(p, "Command reserved for OP+ Only. ");
                        Player.SendMessage(p, "NOTE: You must have permissions for &4/promote " + "&g or &4/setrank " + "&g to use &3/review next."); 
                    }
                    break;

                case "clear":
                    if (p == null)
                    {
                        foreach (Player pl in Player.players) { if (!pl.usereview) { pl.usereview = true; } }
                        Server.reviewlist.Clear();
                        Player.SendMessage(p, "Successfully cleared review queue.");
                        return;
                    }
                    if (p.group.Permission >= Server.reviewclear)
                    {
                        foreach (Player pl in Player.players) { if (!pl.usereview) { pl.usereview = true; } }
                        Server.reviewlist.Clear();
                        Player.SendMessage(p, "Successfully cleared review queue.");
                        return;
                    }
                    else { Player.SendMessage(p, "Command reserved for OP+ Only."); }
                    break;
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/review - Request an Operator to review your build for promotion.");
            Player.SendMessage(p, "/review remove - Remove your review request from the queue.");
            Player.SendMessage(p, "/review list - View the Review Queue. &c<Operator+ Only>.");
            Player.SendMessage(p, "/review clear - Clear the Review Queue. &C<Operator+ Only>.");
            Player.SendMessage(p, "/review next - Answer the next review request. &c<Operator+ Only>.");
            Player.SendMessage(p, "Note to OPs: Type /decline <player> to decline a player.");
        }
    }
}