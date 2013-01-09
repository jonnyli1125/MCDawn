using System;
using System.Collections.Generic;
using System.IO;

namespace MCDawn
{
    public class CmdProperties : Command
    {
        public override string name { get { return "properties"; } }
        public override string[] aliases { get { return new string[] { "prop" }; } }
        public override string type { get { return "information"; } }
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
                            buffer.Add(lines[i]);
                        }
                    }
                Player.SendMessage(p, ((counter == 0) ? "Property could not be found" : counter + " results found:"));
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
                            lines[i] = property + " = " + value;
                            File.WriteAllLines(path, lines.ToArray());
                            Player.SendMessage(p, "Property value set.");
                            Player.SendMessage(p, File.ReadAllLines(path)[i]);
                            Properties.Load(path);
                            return;
                        }
                        else if (temp.ToLower().Contains(property.ToLower()) && !IsPassword(temp.ToLower())) suggestions.Add(property.ToLower());
                    }
                Player.SendMessage(p, "Property not found" + ((suggestions.Count > 0) ? "; perhaps you meant one of the below?" : "."));
                for (int i = 0; i < suggestions.Count; i++) Player.SendMessage(p, suggestions[i]);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "NOTICE: This command manages your server.properties file.");
            Player.SendMessage(p, "/properties get [property] - Get the value of [property].");
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
    }
}