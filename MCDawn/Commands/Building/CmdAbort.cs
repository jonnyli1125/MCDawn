using System;
using System.Threading;

namespace MCDawn
{
    public class CmdAbort : Command
    {
        public override string name { get { return "abort"; } }
        public override string[] aliases { get { return new string[] { "a" }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }
        public CmdAbort() { }

        public override void Use(Player p, string message)
        {
            p.ClearBlockchange();
            p.painting = false;
            p.BlockAction = 0;
            p.megaBoid = false;
            p.cmdTimer = false;
            p.staticCommands = false;
            p.deleteMode = false;
            p.ZoneCheck = false;
            p.modeType = 0;
            p.aiming = false;
            p.brushing = false;
            p.onTrain = false;
            p.autobuild = false;
            p.activeCuboids = 0;
            p.zoneDel = false;
            p.isFlying = false;
            p.storedMessage = "";
            foreach (Level l in Server.levels)
            {
                if (l.reflections.ContainsKey(p.name.ToLower()))
                    l.reflections.Remove(p.name.ToLower());
                if (l.reflectionLines.ContainsKey(p.name.ToLower()))
                    l.reflectionLines.Remove(p.name.ToLower());
            }
            // plugins
            /*p.noKick = false;
            p.noSendMessage = false;
            p.noJoin = false;
            p.noDisconnect = false;
            p.noMove = false;
            p.noChat = false;
            p.noCommand = false;
            p.noDeath = false;
            p.noBuild = false;*/
            Player.SendMessage(p, "Every toggle or action was aborted.");

        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/abort - Cancels an action.");
        }
    }
}