// Written by jonnyli1125 for MCDawn :D
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MCDawn
{
    public static class ProfanityFilter
    {
        //public static List<string> swearWords = new List<string>();

        public static void Warn(Player p)
        {
            if (Server.swearWarnPlayer && p != null) p.swearWordsUsed++;
            if (p.swearWordsUsed >= Server.swearWordsRequired && p != null)
                if (Server.profanityFilterOp || (!Server.profanityFilterOp && p.group.Permission < LevelPermission.Operator))
                    switch (Server.profanityFilterStyle)
                    {
                        case "Kick":
                            p.Kick("You were kicked for excessive use of swear words!");
                            return;
                        case "TempBan":
                             Command.all.Find("tempban").Use(null, p.name + " " + Server.antiSpamTempBanTime.ToString());
                             return;
                        case "Mute":
                             Command.all.Find("mute").Use(null, p.name);
                             break;
                         case "Slap":
                             ushort currentX = (ushort)(p.pos[0] / 32);
                             ushort currentY = (ushort)(p.pos[1] / 32);
                             ushort currentZ = (ushort)(p.pos[2] / 32);
                             ushort foundHeight = 0;

                             for (ushort yy = currentY; yy <= 1000; yy++)
                             {
                                 if (!Block.Walkthrough(p.level.GetTile(currentX, yy, currentZ)) && p.level.GetTile(currentX, yy, currentZ) != Block.Zero)
                                 {
                                    foundHeight = (ushort)(yy - 1);
                                     p.level.ChatLevel(p.color + p.name + Server.DefaultColor + " was slapped into the roof for excessive use of swear words!");
                                     break;
                                 }
                             }
 
                             if (foundHeight == 0)
                             {
                                p.level.ChatLevel(p.color + p.name + Server.DefaultColor + " was slapped sky high for excessive use of swear words!");
                                foundHeight = 1000;
                             }

                             unchecked { p.SendPos((byte)-1, p.pos[0], (ushort)(foundHeight * 32), p.pos[2], p.rot[0], p.rot[1]); }
                             break;
                         default: goto case "Kick";
                    }

            if (Server.swearWarnPlayer && p != null)
            {
                Player.SendMessage(p, "&cYou have been warned for using a swear word!");
                Player.GlobalMessageOps("To Ops: Warned " + p.color + p.name + Server.DefaultColor + " for using a swear word!");
                Server.s.Log("Warned " + p.name + " for using a swear word!");
            }
        }
        public static string Filter(Player p, string message)
        {
            try
            {
                string path = "text/swearwords.txt";
                if (File.Exists(path))
                {
                    foreach (string line in File.ReadAllLines(path))
                    {
                        if (line != null && line.Trim()[0] != '#' && line.Trim() != "")
                        {
                        recheck:
                            if (!line.Trim().Contains(" "))
                            {
                                if (message.ToLower().Contains(line.Trim().ToLower()))
                                {
                                    int savedIndex = message.ToLower().IndexOf(line.Trim().ToLower());
                                    message = message.Remove(savedIndex, line.Trim().Length);
                                    message = message.Insert(savedIndex, "&c<censored>&f");
                                    //message = message.Replace(line.Trim(), "&c<censored>&f"); // Meh too lazy to test again.
                                    if (p != null) { Warn(p); }
                                    goto recheck;
                                }
                            }
                            else
                            {
                                if (message.ToLower().Contains(line.Split(' ')[0].ToLower()))
                                {
                                    int savedIndex = message.ToLower().IndexOf(line.Trim().Split(' ')[0].ToLower());
                                    message = message.Remove(savedIndex, line.Trim().Split(' ')[0].Length);
                                    message = message.Insert(savedIndex, "&c" + line.Trim().Split(new char[] { ' ' }, 2)[1] + "&f");
                                    if (p != null) { Warn(p); }
                                    goto recheck;
                                }
                            }
                        }
                    }
                }
                else { File.Create("text/swearwords.txt").Close(); }
                return message;
            }
            catch (Exception e) { Server.ErrorLog(e); return message; }
        }
    }
}
