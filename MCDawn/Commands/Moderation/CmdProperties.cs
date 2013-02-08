using System;
using System.Collections.Generic;
using System.IO;

namespace MCDawn
{
    public class CmdProperties : Command
    {
        public override string name { get { return "properties"; } }
        public override string[] aliases { get { return new string[] { "prop" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Nobody; } }
        public CmdProperties() { }

        public override void Use(Player p, string message)
        {
            string path = "properties/server.properties";
            if (String.IsNullOrEmpty(message)) { Help(p); return; }
            string mode = message.Split(' ')[0].ToLower();
            if (mode != "get" && mode != "set") { Help(p); return; }
            message = (message.Split(' ').Length >= 2) ? message.Substring(message.IndexOf(" ") + 1).Trim() : "";
            var lines = new List<string>(File.ReadAllLines(path));
            if (mode == "get")
            {
                string property = ((!String.IsNullOrEmpty(message)) ? message.Split(' ')[0].ToLower() : "");
                bool exp = (property.Length > 0 ? property[0] == '!' : false);
                if (exp) property = property.Substring(1);
                if (IsPassword(property)) { Player.SendMessage(p, "nope.avi"); return; }
                int counter = 0;
                var buffer = new List<string>();
                for (int i = 0; i < lines.Count; i++)
                    if (!String.IsNullOrEmpty(lines[i]) && lines[i][0] != '#')
                    {
                        string temp = lines[i].Split('=')[0].Trim().ToLower();
                        if (temp.Contains(property) && !IsPassword(temp))
                        {
                            counter++;
                            if (IsColor(temp))
                            {
                                string tempvalue = lines[i].Split('=')[1].Trim();
                                buffer.Add(temp + " = " + tempvalue + c.Name(tempvalue));
                            }
                            else buffer.Add(lines[i]);
                            if (exp && temp == property) break;
                        }
                    }
                if (counter == 0) Player.SendMessage(p, "Property could not be found.");
                else if (!exp && counter > 0) Player.SendMessage(p, counter + " results found:");
                for (int i = 0; i < buffer.Count; i++) Player.SendMessage(p, buffer[i]);
            }
            if (mode == "set")
            {
                if (String.IsNullOrEmpty(message)) { Help(p); return; }
                string property = message.Split(' ')[0];
                if (IsPassword(property.ToLower())) { Player.SendMessage(p, "nope.avi"); return; }
                string value = (message.Split(' ').Length >= 2) ? message.Substring(message.IndexOf(" ") + 1) : "";
                var suggestions = new List<string>();
                for (int i = 0; i < lines.Count; i++)
                    if (!String.IsNullOrEmpty(lines[i]) && lines[i][0] != '#')
                    {
                        string temp = lines[i].Split('=')[0].Trim();
                        if (temp.ToLower() == property.ToLower() && !IsPassword(temp.ToLower()))
                        {
                            if (IsColor(property) && value.Length > 2) { value = c.Parse(value); }
                            lines[i] = property + " = " + value;
                            File.WriteAllLines(path, lines.ToArray());
                            Player.SendMessage(p, "Property value set.");
                            Use(p, "get !" + property);
                            Properties.Load(path);
                            return;
                        }
                        else if (temp.ToLower().Contains(property.ToLower()) && !IsPassword(temp.ToLower())) suggestions.Add(temp.ToLower());
                    }
                Player.SendMessage(p, "Property not found" + ((suggestions.Count > 0) ? "; perhaps you meant one of the below?" : "."));
                for (int i = 0; i < suggestions.Count; i++) Player.SendMessage(p, suggestions[i]);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "NOTICE: This command manages your server.properties file.");
            Player.SendMessage(p, "/properties get [property] - Get the value of [property]. Add an ! infront of [property] to look only exactly for that property.");
            Player.SendMessage(p, "/properties set [property] [value] - Set the value of [property] to [value].");
        }

        public bool IsPassword(string property)
        {
            switch (property.ToLower())
            {
                case "irc-password":
                case "password":
                case "global-password":
                case "rc-pass":
                    return true;
                default: return false;
            }
        }

        public bool IsColor(string property)
        {
            switch (property.ToLower())
            {
                case "defaultcolor":
                case "irc-color":
                case "global-color":
                    return true;
                default: return false;
            }
        }
    }
}