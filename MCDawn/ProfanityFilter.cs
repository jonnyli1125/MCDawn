// Written by jonnyli1125 for MCDawn :D
using System;
using System.Collections.Generic;
using System.IO;
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
                                     p.level.ChatLevel(p.color + p.name + "&g was slapped into the roof for excessive use of swear words!");
                                     break;
                                 }
                             }
 
                             if (foundHeight == 0)
                             {
                                p.level.ChatLevel(p.color + p.name + "&g was slapped sky high for excessive use of swear words!");
                                foundHeight = 1000;
                             }

                             unchecked { p.SendPos((byte)-1, p.pos[0], (ushort)(foundHeight * 32), p.pos[2], p.rot[0], p.rot[1]); }
                             break;
                         default: goto case "Kick";
                    }

            if (Server.swearWarnPlayer && p != null)
            {
                Player.SendMessage(p, "&cYou have been warned for using a swear word!");
                Player.GlobalMessageOps("To Ops: Warned " + p.color + p.name + "&g for using a swear word!");
                Server.s.Log("Warned " + p.name + " for using a swear word!");
            }
        }

        public static Dictionary<string, string> BadWordPairs
        {
            get
            {
                string path = "text/swearwords.txt";
                var retval = new Dictionary<string, string>();
                if (File.Exists(path))
                {
                    foreach (string line in File.ReadAllLines(path))
                        if (!String.IsNullOrEmpty(line) && line.Trim()[0] != '#')
                            retval.Add(line.Trim().Contains(" ") ? line.Split(' ')[0].ToLower() : line.Trim(), line.Trim().Contains(" ") ? line.Split(' ')[1] : "<censored>");
                }
                else { File.Create(path).Close(); }
                return retval;
            }
        }

        public static string Filter(Player p, string message)
        {
            foreach (KeyValuePair<string, string> pair in BadWordPairs)
                if (Regex.IsMatch(message, @pair.Key.ToLower(), RegexOptions.IgnoreCase))
                {
                    message = Regex.Replace(message, @pair.Key, "&c" + pair.Value + "&f", RegexOptions.IgnoreCase);
                    if (p != null) Warn(p);
                }
            return message;
        }
    }
}
