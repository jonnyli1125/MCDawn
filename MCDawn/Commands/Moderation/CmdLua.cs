/* Coded by Gamemakergm */
using System;
//using LuaInterface;

namespace MCDawn
{
    public class CmdLua : Command
    {
        public override string name { get { return "lua"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdLua() { }

        public override void Use(Player p, string message)
        {
            if (Server.mono) { p.SendMessage("Lua Scripts are not possible due to Lua not being supported on Mono."); }
            /*LuaScripting meep = Server.scripting;
            meep.Lua.DoFile("extra/scripts/" + message + ".lua");*/
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/lua <extra/scripts/[script].lua> - Loads a script :P");
        }
    }
}