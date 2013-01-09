using System;

namespace MCDawn
{
    public class CmdBlockSet : Command
    {
        public override string name { get { return "blockset"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdBlockSet() { }

        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') == -1) { Help(p); return; }

            byte foundBlock = Block.Byte(message.Split(' ')[0]);
            if (foundBlock == Block.Zero) { Player.SendMessage(p, "Could not find block entered"); return; }
            LevelPermission newPerm = Level.PermissionFromName(message.Split(' ')[1]);
            if (newPerm == LevelPermission.Null) { Player.SendMessage(p, "Could not find rank specified"); return; }
            if (p != null && newPerm > p.group.Permission) { Player.SendMessage(p, "Cannot set to a rank higher than yourself."); return; }

            if (p != null && !Block.canPlace(p, foundBlock)) { Player.SendMessage(p, "Cannot modify a block set for a higher rank"); return; }

            Block.Blocks newBlock = Block.BlockList.Find(bs => bs.type == foundBlock);
            newBlock.lowestRank = newPerm;

            Block.BlockList[Block.BlockList.FindIndex(bL => bL.type == foundBlock)] = newBlock;

            Block.SaveBlocks(Block.BlockList);

            Player.GlobalMessage("&d" + Block.Name(foundBlock) + "&g's permission was changed to " + Level.PermissionToName(newPerm));
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/blockset [block] [rank] - Changes [block] rank to [rank]");
            Player.SendMessage(p, "Only blocks you can use can be modified");
            Player.SendMessage(p, "Available ranks: " + Group.concatList());
        }
    }
}