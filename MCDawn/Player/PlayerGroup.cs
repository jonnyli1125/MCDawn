/*-----------------------------------------------------------------------------------------------------
This system was made for use with MCDawn.
This system was written by Jonny Li, also known as jonnyli1125
Friday, April 27, 2012
Version 1.0
----------------------------------------------------------------------------------------------------
Licensed under a Creative Commons Attribution-NonCommercial-NoDerivs 3.0 Unported License.
http://creativecommons.org/licenses/by-nc-nd/3.0/

You must attribute the work in the manner specified by the author or licensor.
You may not use this work for commercial purposes. 
You may not alter, transform, or build upon this work.
 
Any of the above conditions can be waived if you get written permission from the copyright holder.  
----------------------------------------------------------------------------------------------------
Inspired by MCStorm's player group system, but completely new and revamped :P
----------------------------------------------------------------------------------------------------*/

/*To-Do List:
- Complete basis
- Group Build / Group Visit on levels
- Support renaming of "groups" to, e.g. "factions" or "clans"
- Ranks within groups
- Commands and GUI
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MCDawn
{
    public sealed class PlayerGroupList
    {
        //public string name;
        public PlayerGroup group;
        List<string> players = new List<string>();
        public PlayerGroupList() { }
        public void Add(string p) { players.Add(p.ToLower()); }
        public bool Remove(string p)
        {
            return players.Remove(p.ToLower());
        }
        public bool Contains(string p) { return players.Contains(p.ToLower()); }
        public List<string> All() { return new List<string>(players); }
        public void Save() {
            Save(group.fileName); 
        }
        public void Save(string path)
        {
            StreamWriter file = File.CreateText("groups/" + path);
            players.ForEach(delegate(string p) { file.WriteLine(p); });
            file.Close();
        }
        public static PlayerGroupList Load(string path, PlayerGroup groupName)
        {
            if (!Directory.Exists("groups")) { Directory.CreateDirectory("groups"); }
            path = "groups/" + path;
            PlayerGroupList list = new PlayerGroupList();
            list.group = groupName;
            if (File.Exists(path))
            {
                foreach (string line in File.ReadAllLines(path)) { list.Add(line); }
            }
            else
            {
                File.Create(path).Close();
                Server.s.Log("CREATED NEW: " + path);
            } return list;
        }

        public void Clear() { players.Clear(); }
    }
    public enum GroupRank
    {
        Banned = -1,
        Guest = 0,
        Member = 1,
        Operator = 2,
        Admin = 3
    }
    public class PlayerGroup
    {
        public static string customName = ""; // Can rename PlayerGroups to factions, clans, klans (lol), teams, etc.
        public string name = "";
        public PlayerGroupList players = new PlayerGroupList();
        public bool pub = true;
        public string fileName = "";
        public static List<PlayerGroup> allPlayerGroups = new List<PlayerGroup>();

        public PlayerGroup()
        {
        }
        public PlayerGroup(string Name, bool Public)
        {
            name = Name;
            fileName = name;
            players = PlayerGroupList.Load(fileName, this);
            pub = Public;
            allPlayerGroups.Add(new PlayerGroup(name, pub));
        }

        public static void InitAll()
        {
            allPlayerGroups = new List<PlayerGroup>();

            if (File.Exists("properties/groups.properties"))
            {
                string[] lines = File.ReadAllLines("properties/groups.properties");

                PlayerGroup thisGroup = new PlayerGroup();
                //int gots = 0;

                foreach (string s in lines)
                {
                    try
                    {
                        if (s != "" && s[0] != '#')
                        {
                            if (s.Split('=').Length == 2)
                            {
                                string property = s.Split('=')[0].Trim();
                                string value = s.Split('-')[1].Trim();
                                allPlayerGroups.Add(new PlayerGroup(thisGroup.name, thisGroup.pub));
                            }
                            else { Server.s.Log("The property file groups.properties is wrongly formatted."); return; }
                        }
                    }
                    catch { }
                }
            }

            List<PlayerGroup> pgs = new List<PlayerGroup>(); int count = 0;
            foreach (Player pl in Player.players)
            {
                foreach (PlayerGroup pg in pl.playerGroup)
                {
                    if (pg != null)
                    {
                        count++;
                        pgs.Add(pg);
                        pl.playerGroup[count] = allPlayerGroups.Find(g => g.name.ToLower() == pl.playerGroup[count].name.ToLower());
                    }
                }
            }

            saveGroups(allPlayerGroups);
        }

        public static void saveGroups(List<PlayerGroup> givenList)
        {
            StreamWriter SW = new StreamWriter(File.Create("properties/groups.properties"));
            SW.WriteLine("#GroupName = string");
            SW.WriteLine("#     The name of the group, use capitalization.");
            SW.WriteLine("#");
            SW.WriteLine("#Public = bool");
            SW.WriteLine("#     This determines if the group is publicly joinable or not.");
            SW.WriteLine("#     The default is true, but if set to false, the group becomes invite-only.");
            SW.WriteLine();
            SW.WriteLine();

            foreach (PlayerGroup grp in givenList)
            {
                SW.WriteLine("RankName = " + grp.name);
                SW.WriteLine("Permission = " + grp.pub.ToString().ToLower());
            }

            SW.Flush();
            SW.Close();
        }

        public static PlayerGroup Find(string input)
        {
            foreach (PlayerGroup gr in allPlayerGroups)
            {
                if (gr.name.ToLower() == input.ToLower()) { return gr; }
            } return null;
        }

    }
}
