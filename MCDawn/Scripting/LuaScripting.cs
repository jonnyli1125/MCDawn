/*
Copyright (c) 2012 by Gamemakergm
This work is licensed under the Attribution-NonCommercial-NoDerivs License. To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/3.0/ or send a letter to Creative Commons, 444 Castro Street, Suite 900, Mountain View, California, 94041, USA.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using LuaInterface;

namespace MCDawn
{
    public class LuaScripting
    {
        Lua lua = new Lua();

        public Lua Lua
        {
            get { return lua; }
        }

        public static void Init()
        {
            LuaScripting scripting = Server.scripting;

            Server.s.Log("Initiating Lua Scripting");

            try
            {
                scripting.lua.RegisterFunction("GlobalMessage", scripting, scripting.GetType().GetMethod("GlobalMessage"));
                scripting.lua.RegisterFunction("ServerLog", scripting, scripting.GetType().GetMethod("ServerLog"));
                scripting.lua.RegisterFunction("GetPlayerByName", scripting, scripting.GetType().GetMethod("GetPlayerByName"));
                scripting.lua.RegisterFunction("Debug", scripting, scripting.GetType().GetMethod("CreatePlayer"));
                scripting.lua.RegisterFunction("PlayerBotFind", scripting, scripting.GetType().GetMethod("PlayerBotFind"));
                scripting.lua.RegisterFunction("UseCommand", scripting, scripting.GetType().GetMethod("CommandOverlord"));

            }
            catch (Exception e)
            {
                Server.s.Log("Lua Scripting could not initialize!");
                Server.s.Log("Error sent to ErrorLog");
                Server.ErrorLog(e);
            }
            Server.s.Log("Lua Scripting Initialized Succesfully!");
        }

        public void GlobalMessage(string message)
        {
            Player.GlobalMessage(message);
            Server.s.Log(message);
        }

        public void ServerLog(string log)
        {
            Server.s.Log(log);
        }

        public void CommandOverlord(string command, Player p, string message)
        {
            Command.all.Find(command).Use(p, message);
        }

        public Player GetPlayerByName(string message)
        {
            Player who = Player.Find(message);
            //Server.scripting.lua["target"] = who;
            return who;
        }
        public PlayerBot PlayerBotFind(string message)
        {
            PlayerBot who = PlayerBot.Find(message);
            return who;
        }
        public void CreatePlayer() //Debug
        {
            PlayerBot gamemakergm = new PlayerBot("gamemakergm", Server.mainLevel);
            PlayerBot.playerbots.Add(gamemakergm);
        }
    }
    //Don't remove this just yet :3
    /*public class Plugin
    {
            
        public struct Plugin
        {
            public string name;
            public int id;
            public Plugin next;
                
        }

        public struct Hook
        {
            public string hook;
            public int function;
            public Plugin plugin;
            public Hook next;

        }
        public struct PendingHook
        {
            public string hook;
            public Player p;
            public PendingHook next;

            public PendingHook(string _hook, Player _p)
            {
                hook = _hook;
                p = _p;
            }
        }
    }
    
    public class Hooks
    {
        public event EventHandler OnPlayerMove;
        protected virtual void OnPlayerMoveChange(EventArgs e)
        {
            if (OnPlayerMove != null)
            {
                OnPlayerMoveChange(e);
            }
        }
    }
    public class PluginManager
    {
        /*public void AddHook(string _hook, int _function, Plugin.Plugin _plugin)
        {
            Plugin.Hook newhook = new Plugin.Hook();
            newhook.function = _function;
            newhook.hook = _hook;
            newhook.plugin = _plugin;
        }

        public static void CallHook(string _hook, Player _p)
        {
            Plugin.PendingHook pendinghook = new Plugin.PendingHook(_hook, _p);
        }

        public void RunHook(Plugin.PendingHook _hook)
        {
            CallHook(_hook.hook, _hook.p);
            _hook = _hook.next;
        }
        
    }*/
}

