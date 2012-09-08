using System;
using System.Collections.Generic;
using System.Linq;

namespace MCDawn
{
    public sealed class CommandList
    {
        public List<Command> commands = new List<Command>();
        public CommandList() { }
        public void Add(Command cmd) { commands.Add(cmd); }
        public void AddRange(List<Command> listCommands)
        {
            listCommands.ForEach(delegate(Command cmd) { commands.Add(cmd); });
        }
        public List<string> commandNames()
        {
            List<string> tempList = new List<string>();

            commands.ForEach(delegate(Command cmd)
            {
                tempList.Add(cmd.name);
            });

            return tempList;
        }

        public bool Remove(Command cmd) { return commands.Remove(cmd); }
        public bool Contains(Command cmd) { return commands.Contains(cmd); }
        public bool Contains(string name)
        {
            name = name.ToLower(); foreach (Command cmd in commands)
            {
                if (cmd.name == name.ToLower()) { return true; }
            } return false;
        }
        public Command Find(string name)
        {
            name = name.ToLower(); foreach (Command cmd in commands)
            {
                if (cmd.name == name.ToLower() || cmd.aliases.Contains(name.ToLower())) { return cmd; }
            } return null;
        }

        public string FindShort(string shortcut)
        {
            if (shortcut == "") return "";

            shortcut = shortcut.ToLower();
            foreach (Command cmd in commands)
            {
                if (cmd.aliases.Contains(shortcut)) return cmd.name;
            }
            return "";
        }

        public List<Command> All() { return new List<Command>(commands); }
    }
}