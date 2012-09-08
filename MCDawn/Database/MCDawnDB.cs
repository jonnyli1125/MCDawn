using System;
using System.IO;

// MCDawn Database Text File Database - Written by jonnyli1125 for MCDawn Only.
// Much more efficient! fCraft does it like this, and fragmah is kewl :P
// Eventually, we should switch all servers to this format. We should make like, a MySQL-MCDawnDB converter or something o.o

namespace MCDawn
{
    public static class MCDawnDB
    {
        #region Paths
        public static string playerpath = "database/player.txt";
        public static string blockpath = "database/block.txt";
        public static string portalpath = "database/portals.txt";
        public static string zonepath = "database/zone.txt";
        public static string messagespath = "database/messages.txt";
        public static string cmdblockpath = "database/cmdblock.txt";
        #endregion

        public static bool DatabaseExists()
        {
            if (Directory.Exists("database") && Directory.GetFiles("database").Length >= 6) { return true; }
            return false;
        }

        public static void CreateNewDatabase()
        {
            // check if db already exists
            if (DatabaseExists()) { Server.s.Log("MCDawn Database already exists."); return; }
            // create database
            try
            {
                if (!Directory.Exists("database")) Directory.CreateDirectory("database");
                if (!File.Exists(playerpath)) File.Create(playerpath).Close(); // Players: Name,DisplayName,IP,FirstLogin,LastLogin,totalLogin,Title,TitleColor,Color,TotalDeaths,Money,totalBlocks,blocksBuilt,blocksDestroyed,totalKicked,TimeSpent,rankChangeRreason(includes ban/unban),totalAFKTime
                if (!File.Exists(blockpath)) File.Create(blockpath).Close(); // Block<level>: Username, TimePerformed,X,Y,Z,Type,Deleted
                if (!File.Exists(portalpath)) File.Create(portalpath).Close(); //Portals<level>: EntryX,EntryY,EntryZ,ExitMap,ExitX,ExitY,ExitZ
                if (!File.Exists(zonepath)) File.Create(zonepath).Close(); // Zone<level>: SmallX,SmallY,SmallZ,BigX,BigY,BigZ,Owner
                if (!File.Exists(messagespath)) File.Create(messagespath).Close(); // Messages<level>: X,Y,Z,Message
                if (!File.Exists(cmdblockpath)) File.Create(cmdblockpath).Close(); // Commandblocks<level>: X,Y,Z,Message
                Server.s.Log("New MCDawn Database created.");
            }
            catch (Exception ex) { Server.s.Log("MCDawn Database creation failed."); Server.ErrorLog(ex); }
        }

        public static void Init()
        {
            if (!DatabaseExists()) { CreateNewDatabase(); }
            
        }

        #region Player
        public static void AddPlayer(string name, string displayname, string ip, DateTime firstlogin, DateTime lastlogin, int totallogin, string title, string titlecolor, string color, int totaldeaths, int money, int totalblocks, int blocksbuilt, int blocksdestroyed, int totalkicked, string timespent, string rankchangereason, string afktime)
        {

        }

        public static void DelPlayer(string name)
        {

        }
        public static void DelPlayer(string name, bool ip)
        {

        }
        public static void DelPlayer(int indexOfDbFile)
        {

        }
        #endregion
        #region Block
        public static void Blockchange(string level, string username, DateTime timeperformed, ushort x, ushort y, ushort z, byte type, bool deleted)
        {

        }
        #endregion
        #region Portals
        public static void AddPortal(string level, ushort entryx, ushort entryy, ushort entryz, string exitmap, ushort exitx, ushort exity, ushort exitz)
        {

        }
        #endregion
        #region Zone
        public static void AddZone(string level, ushort smallx, ushort smally, ushort smallz, ushort bigx, ushort bigy, ushort bigz, string owner)
        {
        }
        #endregion
        #region Messages
        public static void AddMessage(string level, ushort x, ushort y, ushort z, string message)
        {
        }
        #endregion
        #region CmdBlock
        public static void AddCmdBLock(string level, ushort x, ushort y, ushort z, string message)
        {
        }
        #endregion
    }
}
