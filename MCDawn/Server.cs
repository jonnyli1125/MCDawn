using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
//using MySql.Data.MySqlClient;
//using MySql.Data.Types;

using MonoTorrent.Client;
using NATUPNPLib;

namespace MCDawn
{
    public class Server
    {
        public static LookupService iploopup = new LookupService("GeoIP.dat", LookupService.GEOIP_MEMORY_CACHE);
        public delegate void LogHandler(string message);
        public delegate void HeartBeatHandler();
        public delegate void MessageEventHandler(string message);
        public delegate void PlayerListHandler(List<Player> playerList);
        public delegate void PlayerBotListHandler(List<PlayerBot> botList);
        public delegate void VoidHandler();

        public event LogHandler OnLog;
        public event LogHandler OnSystem;
        public event LogHandler OnCommand;
        public event LogHandler OnError;
        public event LogHandler OnOp;
        public event LogHandler OnAdmin;
        public event HeartBeatHandler HeartBeatFail;
        public event MessageEventHandler OnURLChange;
        public event PlayerListHandler OnPlayerListChange;
        public event PlayerBotListHandler OnPlayerBotListChange;
        public event VoidHandler OnSettingsUpdate;

        // Plugin Events

        public delegate void OnServerStartEventHandler();
        public event OnServerStartEventHandler OnServerStartEvent = null;

        public delegate void OnServerExitEventHandler();
        public static event OnServerExitEventHandler OnServerExitEvent = null;

        public static bool noShutdown = false;

        public static Thread locationChecker;

        public static Thread blockThread;
        public static List<MySql.Data.MySqlClient.MySqlCommand> mySQLCommands = new List<MySql.Data.MySqlClient.MySqlCommand>();

        public static int speedPhysics = 250;

        public static string Version { get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }
        public static string LatestVersion()
        {
            try
            {
                string temp = "";
                using (WebClient w = new WebClient()) 
                    temp = w.DownloadString("http://updates.mcdawn.com/curversion.txt");
                int current, latest;
                if (!int.TryParse(Version.Replace(".", ""), out current)) return Version;
                if (int.TryParse(temp.Replace(".", ""), out latest))
                    if (latest > current)
                        return temp;
                return Version;
            }
            catch { return Version; }
        }

        // URL
        public static string URL = String.Empty;

        // Worlds loaded
        public static string getWorlds()
        {
            string worlds = "";
            foreach (Level l in Server.levels) { worlds += l.name + ", "; }
            return worlds;
        }

        public static Socket listen;
        public static System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess();
        public static System.Timers.Timer updateTimer = new System.Timers.Timer(100);
        //static System.Timers.Timer heartbeatTimer = new System.Timers.Timer(60000);     //Every 45 seconds
        static System.Timers.Timer messageTimer = new System.Timers.Timer(60000 * 5);   //Every 5 mins
        public static System.Timers.Timer cloneTimer = new System.Timers.Timer(5000);

        //public static Thread physThread;
        //public static bool physPause;
        //public static DateTime physResume = DateTime.Now;
        //public static System.Timers.Timer physTimer = new System.Timers.Timer(1000);
        // static Thread botsThread;

        //CTF STUFF
        public static List<CTFGame> CTFGames = new List<CTFGame>();

        public static PlayerList bannedIP;
        public static PlayerList whiteList;
        public static PlayerList ircControllers;
        public static PlayerList agreedToRules;
        // Player Ignore and Global Ignore
        public static PlayerList ignoreGlobal;
        public static bool allowIgnoreOps = false;
        // Global Chat Moderators
        //internal static readonly List<string> globalChatMods = new List<string>(new string[] { "Jonny", "[Dev]Jonny", "[Op]Jonny", "GameMakerGm", "[Op]Game", "_", "Katz", "Notch", "ScHmIdTy56789", "sillyboyization", "Sandford27", "ddeckys", "[Mod]ddeckys", "Incedo", "Speedkicks6" });

        public static readonly List<string> devs = new List<string>(new string[] { "jonnyli1125", "ceddral", "herocane", "schmidty56789" });
        public static readonly List<string> staff = new List<string>(new string[] { });
        public static readonly List<string> administration = new List<string>(new string[] { "jonnyli1125", "sillyboyization", "storm_resurge" });
        // Booleans, easier and faster than Server.devs.Contains etc :P
        public static bool hasProtection(string name)
        {
            if (devs.Contains(name.ToLower().Trim())) return true;
            if (staff.Contains(name.ToLower().Trim())) return true;
            if (administration.Contains(name.ToLower().Trim())) return true;
            return false;
        }
        public static bool isDev(string name) { if (devs.Contains(name.ToLower().Trim())) { return true; } return false; }
        public static bool isStaff(string name) { if (staff.Contains(name.ToLower().Trim())) { return true; } return false; }
        public static bool isAdministration(string name) { if (administration.Contains(name.ToLower().Trim())) { return true; } return false; }

        public static List<TempBan> tempBans = new List<TempBan>();
        public struct TempBan { public string name; public DateTime allowedJoin; }

        public static MapGenerator MapGen;

        // Homes (pmaps)
        public static LevelPermission HomeRank = LevelPermission.AdvBuilder;
        public static string HomePrefix = "";
        public static int HomeX = 128;
        public static int HomeY = 128;
        public static int HomeZ = 128;

        // Use WOM Direct
        public static bool useWOM = true;
        public static bool womText = false; // Top right hand corner messages.
        // Anti-Grief
        public static bool useAntiGrief = true;

        public static PerformanceCounter PCCounter = null;
        public static PerformanceCounter ProcessCounter = null;

        // Network Usage
        public static void GetNetworkUsage()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                return;

            NetworkInterface[] interfaces
                = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface ni in interfaces)
            {
                Server.kbSent = Convert.ToInt32(ni.GetIPv4Statistics().BytesSent) * 1000;
                /*Console.WriteLine("    Bytes Sent: {0}",
                    ni.GetIPv4Statistics().BytesSent);*/
                Server.kbRecieved = Convert.ToInt32(ni.GetIPv4Statistics().BytesSent) * 1000;
                /*Console.WriteLine("    Bytes Received: {0}",
                    ni.GetIPv4Statistics().BytesReceived);*/
            }
        }
        public static int kbSent = 0;
        public static int kbRecieved = 0;

        public static Level mainLevel;
        public static List<Level> levels;
        // Review List
        public static List<string> reviewlist = new List<string>();
        //public static List<levelID> allLevels = new List<levelID>();
        public struct levelID { public int ID; public string name; }

        public static bool opAfk = true;
        public static List<string> afkset = new List<string>();
        public static List<string> afkmessages = new List<string>();
        public static List<string> messages = new List<string>();

        public static DateTime timeOnline;

        //auto updater stuff
        public static bool autoupdate;
        public static bool autonotify;
        public static string selectedrevision = "";
        public static bool autorestart;
        public static DateTime restarttime;

        public static bool chatmod = false;

        // Infection
        public static int infectionGames = 0;

        // Spleef
        public static int spleefPhysics = 0;

        //Lua
        //public static LuaScripting scripting = new LuaScripting();

        // Custom Command variables. Allows custom command makers to have more control and abilites to do things.
        public struct CustomCommand
        {
            public string[] str;
            public int[] i;
            public bool[] b;
            public object[] obj;
        }
        public static CustomCommand customCommand;

        //Settings
        #region Server Settings
        public const byte version = 7;
        public static string salt { get; internal set; }

        public static string name = "[MCDawn] Default";
        public static string motd = "Welcome! +hax";
        public static string description = "MCDawn Server";
        public static string flags = "MCDawn";
        public static int players = 50;
        public static int maxguests = 40;
        public static byte maps = 10;
        public static int port = 25565;
        public static bool upnp = false;
        public static bool upnpRunning = false;
        public static bool pub = true;
        public static bool verify = true;
        public static bool worldChat = true;
        public static bool guestGoto = false;
        public static bool maintenance = false;
        public static bool cli = false;
        public static LevelPermission canjoinmaint = LevelPermission.Admin;
        public static LevelPermission adminsecurityrank = LevelPermission.Operator;
        public static bool tpToHigher = false;
        // MaxMind GeoIP
        public static bool useMaxMind = true;
        public static bool allowproxy = true;
        // Anti-Spam
        public static bool antiSpam = true;
        public static int spamCounter = 3;
        // antiSpamStyle - 0: Kick 1: TempBan 2: Mute 3: Slap
        public static string antiSpamStyle = "Kick";
        public static int antiSpamTempBanTime = 5;
        public static bool antiSpamOp = false;

        // Anti-Caps
        public static bool antiCaps = true;
        public static int capsRequired = 7;
        // antiCapsStyle - 0: Kick 1: TempBan 2: Mute 3: Slap
        public static int antiCapsTempBanTime = 5;
        public static string antiCapsStyle = "Kick";
        public static bool antiCapsOp = false;

        // Profanity Filter
        public static bool profanityFilter = false;
        public static bool swearWarnPlayer = false;
        public static int swearWordsRequired = 10;
        public static bool profanityFilterOp = false;
        public static string profanityFilterStyle = "Kick";
        public static int profanityFilterTempBanTime = 5;

        // Agree to Rules
        public static bool agreeToRules = false;
        public static string agreePass = "";

        // Playing sounds on chat update in Console
        public static bool consoleSound = false;

        // Log/Show attempted logins
        public static bool showAttemptedLogins = false;
        public static string ZallState = "Alive";

        // Griefer Option
        public static bool useGriefer = false;
        public static string grieferRank = "Griefer";
        public static string grieferCommand = "griefer";

        // Time ranking
        public static bool useTimeRank = false;
        public static string timeRankCommand = "timerank";

        public static bool useDiscourager = false;

        // Remote Console
        public static bool useRemote = false;

        // Cuboid Throttle
        public static int throttle = 200;
        public static bool pauseCuboids = false;

        //public static string[] userMOTD;

        public static string level = "main";
        public static string errlog = "error.log";

        public static bool console = false;
        public static bool reportBack = true;

        // IRC
        public static bool irc = false;
        public static int ircPort = 6667;
        public static string ircNick = "MCDawnServer";
        public static string ircServer = "irc.freenode.net";
        public static string ircChannel = "#changethis";
        public static string ircOpChannel = "#changethistoo";
        public static bool ircIdentify = false;
        public static string ircPassword = "";

        // Global Chat
        public static bool useglobal = true;
        //public static string globalNick = "MC" + new Random().Next();
        public static string globalNick = generateGlobalNick();
        public static string generateGlobalNick()
        {
            Random randomNumberGenerator = new Random();
            string s = string.Concat(
                randomNumberGenerator.Next(0, 9),
                randomNumberGenerator.Next(0, 9),
                randomNumberGenerator.Next(0, 9),
                randomNumberGenerator.Next(0, 9));
            return "MC" + s;
        }
        public static bool globalIdentify = false;
        public static string globalPassword = "";

        public static string GetIPAddress()
        {
            using (WebClient w = new WebClient())
                return w.DownloadString("http://checkip.dyndns.org/").Split(':')[1].Trim().Split('<')[0].Trim();
        }

        // WOM Passwords System
        public static bool useWOMPasswords = false;
        public static string WOMIPAddress = GetIPAddress();

        // Admin Security System
        public static bool adminsecurity = true;

        public static bool restartOnError = false;

        public static bool antiTunnel = true;
        public static byte maxDepth = 4;
        public static int Overload = 1500;
        public static int rpLimit = 500;
        public static int rpNormLimit = 10000;

        public static int backupInterval = 300;
        public static int blockInterval = 60;
        public static string backupLocation = Application.StartupPath + "/levels/backups";

        public static bool physicsRestart = true;
        public static bool deathcount = true;
        public static bool AutoLoad = false;
        public static int physUndo = 60000;
        public static int totalUndo = 200;
        public static bool rankSuper = true;
        public static bool oldHelp = false;
        public static bool parseSmiley = true;
        public static bool useWhitelist = false;
        public static bool forceCuboid = false;
        public static bool repeatMessage = false;

        public static bool checkUpdates = false;

        public static bool useMySQL = false;
        public static string MySQLHost = "127.0.0.1";
        public static string MySQLPort = "3306";
        public static string MySQLUsername = "root";
        public static string MySQLPassword = "password";
        public static string MySQLDatabaseName = "MCDawnDB";
        public static bool MySQLPooling = true;

        public static string DefaultColor = "&e";
        public static string IRCColour = "&5";
        public static string GlobalChatColour = "&3";

        public static int afkminutes = 10;
        public static int afkkick = 45;

        public static string defaultRank = "guest";

        public static bool dollardollardollar = true;

        public static bool cheapMessage = true;
        public static string cheapMessageGiven = " is now being cheap and being immortal";
        public static bool unCheapMessage = true;
        public static string unCheapMessageGiven = " has stopped being immortal";
        public static bool customBan = false;
        public static string customBanMessage = "You're banned!";
        public static bool customShutdown = false;
        public static string customShutdownMessage = "Server shutdown. Rejoin in 10 seconds.";
        public static string moneys = "moneys";
        public static LevelPermission opchatperm = LevelPermission.Operator;
        public static LevelPermission adminchatperm = LevelPermission.Admin;
        public static bool adminsjoinsilent = false;
        public static bool adminsjoinhidden = false;
        public static bool logbeat = false;

        public static bool mono = false;

        public static bool flipHead = false;

        public static bool shuttingDown = false;

        // OP Review Powers
        public static LevelPermission reviewnext = LevelPermission.Operator;
        public static LevelPermission reviewclear = LevelPermission.Operator;

        // Spleef
        public static LevelPermission spleefperm = LevelPermission.Operator;
        #endregion

        public static MainLoop ml;
        public static Server s;
        public Server()
        {
            ml = new MainLoop("server");
            Server.s = this;
        }
        public void Start()
        {
            salt = "";
            shuttingDown = false;
            Log("Starting Server");

            if (!Directory.Exists("properties")) Directory.CreateDirectory("properties");
            if (!Directory.Exists("bots")) Directory.CreateDirectory("bots");
            if (!Directory.Exists("text")) Directory.CreateDirectory("text");

            if (!Directory.Exists("extra")) Directory.CreateDirectory("extra");
            if (!Directory.Exists("extra/undo")) Directory.CreateDirectory("extra/undo");
            if (!Directory.Exists("extra/undoPrevious")) Directory.CreateDirectory("extra/undoPrevious");
            if (!Directory.Exists("extra/copy/")) { Directory.CreateDirectory("extra/copy/"); }
            if (!Directory.Exists("extra/copyBackup/")) { Directory.CreateDirectory("extra/copyBackup/"); }
            if (!Directory.Exists("extra/sounds/")) { Directory.CreateDirectory("extra/sounds/"); }
            if (!Directory.Exists("levels/")) { Directory.CreateDirectory("levels/"); }
            if (!Directory.Exists("levels/backups/")) { Directory.CreateDirectory("levels/backups/"); }
            if (!Directory.Exists("remote")) { Directory.CreateDirectory("remote"); }
            if (!Directory.Exists("remote/users/")) { Directory.CreateDirectory("remote/users/"); }

            try
            {
                if (File.Exists("server.properties")) File.Move("server.properties", "properties/server.properties");
                if (File.Exists("rules.txt")) File.Move("rules.txt", "text/rules.txt");
                if (File.Exists("oprules.txt")) File.Move("oprules.txt", "text/oprules.txt");
                if (File.Exists("welcome.txt")) File.Move("welcome.txt", "text/welcome.txt");
                if (File.Exists("messages.txt")) File.Move("messages.txt", "text/messages.txt");
                if (File.Exists("externalurl.txt")) File.Move("eagreedtorulesxternalurl.txt", "text/externalurl.txt");
                if (File.Exists("autoload.txt")) File.Move("autoload.txt", "text/autoload.txt");
                if (File.Exists("IRC_Controllers.txt")) File.Move("IRC_Controllers.txt", "ranks/IRC_Controllers.txt");
                if (File.Exists("agreedtorules.txt")) File.Move("agreedtorules.txt", "text/agreedtorules.txt");
                if (Server.useWhitelist) if (File.Exists("whitelist.txt")) File.Move("whitelist.txt", "ranks/whitelist.txt");
            } catch { }

            Properties.Load("properties/server.properties");
            //Updater.Load("properties/update.properties");

            Group.InitAll();
            Command.InitAll();
            GrpCommands.fillRanks();
            Block.SetBlocks();
            Awards.Load();

            if (File.Exists("MCDawn Updater.exe")) { File.Delete("MCDawn Updater.exe"); }
            if (File.Exists("MCDawn.new")) { File.Delete("MCDawn.new"); }
            if (File.Exists("MCDawn_.new")) { File.Delete("MCDawn_.new"); }

            // switch to xml instead of txt o.o
            if (Directory.Exists("passwords"))
                foreach (FileInfo f in new DirectoryInfo("passwords").GetFiles())
                {
                    try { if (!f.Name.EndsWith(".xml") && !File.Exists(f.Name.Remove(f.Name.Length - 4) + ".txt")) File.Move("passwords/" + f.Name, "passwords/" + f.Name.Remove(f.Name.Length - 4) + ".xml"); }
                    catch { continue; }
                    finally { if (f.Name.EndsWith(".txt")) File.Delete("passwords/" + f.Name); }
                }

            //if (Directory.Exists("extra/logins")) { Directory.Delete("extra/logins", true); }
            //if (Directory.Exists("extra/logouts")) { Directory.Delete("extra/logouts", true); }

            if (File.ReadAllLines("Viewmode.cfg")[4].Split(' ')[2].ToLower() == "true") { cli = true; }

            //Lua
            //LuaScripting.Init();

            if (File.Exists("text/emotelist.txt"))
            {
                foreach (string s in File.ReadAllLines("text/emotelist.txt"))
                {
                    Player.emoteList.Add(s);
                }
            }
            else
            {
                File.Create("text/emotelist.txt");
            }

            timeOnline = DateTime.Now;

            Exception MySQLError;
            if (!MySQL.CanConnect(out MySQLError) && Server.useMySQL)
            {
                if (MySQLError != null) Server.ErrorLog(MySQLError);
                Server.s.Log("MySQL settings have not been set! Please reference the MySQL_Setup.txt file on setting up MySQL!");
                Server.s.Log("MySQL has been turned off for now, until you correctly configure it.");
                Server.useMySQL = false;
                Properties.Save("properties/server.properties");
            }

            try { MySQL.executeQuery("CREATE DATABASE if not exists `" + MySQLDatabaseName + "`", true); }
            catch (Exception e)
            {
                Server.s.Log("MySQL settings have not been set! Please reference the MySQL_Setup.txt file on setting up MySQL!");
                Server.ErrorLog(e);
                //process.Kill();
                return;
            }

            MySQL.executeQuery("CREATE TABLE if not exists Players (ID MEDIUMINT not null auto_increment, Name VARCHAR(20), displayName VARCHAR(60), IP CHAR(15), FirstLogin DATETIME, LastLogin DATETIME, totalLogin MEDIUMINT, Title CHAR(60), TotalDeaths SMALLINT, Money MEDIUMINT UNSIGNED, totalBlocks BIGINT, destroyedBlocks BIGINT, totalKicked MEDIUMINT, color VARCHAR(6), title_color VARCHAR(6), TimeSpent VARCHAR(20), titleBracket MEDIUMINT, HasWOM VARCHAR(20), lastRankReason VARCHAR(255), PRIMARY KEY (ID));");

            // Check if the color column exists.
            DataTable colorExists = MySQL.fillData("SHOW COLUMNS FROM Players WHERE `Field`='color'");
            if (colorExists.Rows.Count == 0)
                MySQL.executeQuery("ALTER TABLE Players ADD COLUMN color VARCHAR(6) AFTER totalKicked");
            colorExists.Dispose();

            // Check if the title color column exists.
            DataTable tcolorExists = MySQL.fillData("SHOW COLUMNS FROM Players WHERE `Field`='title_color'");
            if (tcolorExists.Rows.Count == 0)
                MySQL.executeQuery("ALTER TABLE Players ADD COLUMN title_color VARCHAR(6) AFTER color");
            tcolorExists.Dispose();

            // Check if the title bracket column exists.
            DataTable timeSpentExists = MySQL.fillData("SHOW COLUMNS FROM Players WHERE `Field`='TimeSpent'");
            if (timeSpentExists.Rows.Count == 0)
                MySQL.executeQuery("ALTER TABLE Players ADD COLUMN titleBracket MEDIUMINT AFTER title_color");
            timeSpentExists.Dispose();

            // Check if the title bracket column exists.
            DataTable tbracketExists = MySQL.fillData("SHOW COLUMNS FROM Players WHERE `Field`='titleBracket'");
            if (tcolorExists.Rows.Count == 0)
                MySQL.executeQuery("ALTER TABLE Players ADD COLUMN titleBracket MEDIUMINT AFTER TimeSpent");
            tbracketExists.Dispose();

            // Check if the password column exists. Admin Security moved to text files now.
            DataTable passwordExists = MySQL.fillData("SHOW COLUMNS FROM Players WHERE `Field`='Password'");
            if (passwordExists.Rows.Count != 0)
                MySQL.executeQuery("ALTER TABLE Players DROP COLUMN Password");
            passwordExists.Dispose();

            // Country column.
            DataTable countryExists = MySQL.fillData("SHOW COLUMNS FROM Players WHERE `Field`='countryName'");
            if (countryExists.Rows.Count == 0)
                MySQL.executeQuery("ALTER TABLE Players ADD COLUMN countryName VARCHAR(50) AFTER title_color");
            countryExists.Dispose();

            // Change title column length from 20 to 60 (For multicolored titles).
            DataTable titleExists = MySQL.fillData("SHOW COLUMNS FROM Players WHERE `Field`='Title'");
            if (titleExists.Rows.Count == 0)
                MySQL.executeQuery("ALTER TABLE Players MODIFY COLUMN Title VARCHAR(60)");
            titleExists.Dispose();

            // Check if the displayName column exists.
            DataTable displayNameExists = MySQL.fillData("SHOW COLUMNS FROM Players WHERE `Field`='displayName'");
            if (displayNameExists.Rows.Count == 0)
                MySQL.executeQuery("ALTER TABLE Players ADD COLUMN displayName VARCHAR(60) AFTER Name");
            else
                MySQL.executeQuery("ALTER TABLE Players MODIFY COLUMN displayName VARCHAR(60)");
            displayNameExists.Dispose();

            // Check if HasWOM column exists.
            DataTable haswomExists = MySQL.fillData("SHOW COLUMNS FROM Players WHERE `Field`='HasWOM'");
            if (haswomExists.Rows.Count == 0)
                MySQL.executeQuery("ALTER TABLE Players ADD COLUMN HasWOM VARCHAR(20) AFTER titleBracket");
            haswomExists.Dispose();

            // Check if destroyedBlocks column exists.
            DataTable destroyedBlocksExists = MySQL.fillData("SHOW COLUMNS FROM Players WHERE `Field`='destroyedBlocks'");
            if (destroyedBlocksExists.Rows.Count == 0)
                MySQL.executeQuery("ALTER TABLE Players ADD COLUMN destroyedBlocks BIGINT AFTER totalBlocks");
            destroyedBlocksExists.Dispose();

            // Check if lastRankReason column exists.
            DataTable lastRankReasonExists = MySQL.fillData("SHOW COLUMNS FROM Players WHERE `Field`='lastRankReason'");
            if (lastRankReasonExists.Rows.Count == 0)
                MySQL.executeQuery("ALTER TABLE Players ADD COLUMN lastRankReason VARCHAR(255) AFTER HasWOM");
            lastRankReasonExists.Dispose();

            if (levels != null)
                foreach (Level l in levels) { l.Unload(); }
            ml.Queue(delegate
            {
                try
                {
                    levels = new List<Level>(Server.maps);
                    MapGen = new MapGenerator();

                    Random random = new Random();

                    if (File.Exists("levels/" + Server.level + ".lvl"))
                    {
                        mainLevel = Level.Load(Server.level);
                        mainLevel.unload = false;
                        if (mainLevel == null)
                        {
                            if (File.Exists("levels/" + Server.level + ".lvl.backup"))
                            {
                                Log("Attempting to load backup.");
                                File.Copy("levels/" + Server.level + ".lvl.backup", "levels/" + Server.level + ".lvl", true);
                                mainLevel = Level.Load(Server.level);
                                if (mainLevel == null)
                                {
                                    Log("BACKUP FAILED!");
                                    Console.ReadLine(); return;
                                }
                            }
                            else
                            {
                                Log("mainlevel not found");
                                mainLevel = new Level(Server.level, 128, 64, 128, "flat");

                                mainLevel.permissionvisit = LevelPermission.Guest;
                                mainLevel.permissionbuild = LevelPermission.Guest;
                                mainLevel.Save();
                            }
                        }
                    }
                    else
                    {
                        Log("mainlevel not found");
                        mainLevel = new Level(Server.level, 128, 64, 128, "flat");

                        mainLevel.permissionvisit = LevelPermission.Guest;
                        mainLevel.permissionbuild = LevelPermission.Guest;
                        mainLevel.Save();
                    }
                    addLevel(mainLevel);
                    mainLevel.physThread.Start();
                } catch (Exception e) { Server.ErrorLog(e); }
            });

            ml.Queue(delegate
            {
                bannedIP = PlayerList.Load("banned-ip.txt", null);
                ircControllers = PlayerList.Load("IRC_Controllers.txt", null);
                agreedToRules = PlayerList.Load(true);
                ignoreGlobal = PlayerList.GCIgnoreLoad();

                foreach (Group grp in Group.GroupList)
                    grp.playerList = PlayerList.Load(grp.fileName, grp);
                if (Server.useWhitelist)
                    whiteList = PlayerList.Load("whitelist.txt", null);
            });

            ml.Queue(delegate
            {
                if (File.Exists("text/autoload.txt"))
                {
                    try
                    {
                        string[] lines = File.ReadAllLines("text/autoload.txt");
                        foreach (string line in lines)
                        {
                            //int temp = 0;
                            string _line = line.Trim();
                            try
                            {
                                if (_line == "") { continue; }
                                if (_line[0] == '#') { continue; }
                                int index = _line.IndexOf("=");

                                string key = _line.Split('=')[0].Trim();
                                string value;
                                try
                                {
                                    value = _line.Split('=')[1].Trim();
                                }
                                catch
                                {
                                    value = "0";
                                }

                                if (!key.Equals(mainLevel.name))
                                {
                                    Command.all.Find("load").Use(null, key + " " + value);
                                    Level l = Level.FindExact(key);
                                }
                                else
                                {
                                    try
                                    {
                                        int temp = int.Parse(value);
                                        if (temp >= 0 && temp <= 3)
                                        {
                                            mainLevel.setPhysics(temp);
                                        }
                                    }
                                    catch
                                    {
                                        Server.s.Log("Physics variable invalid");
                                    }
                                }


                            }
                            catch
                            {
                                Server.s.Log(_line + " failed.");
                            }
                        }
                    }
                    catch
                    {
                        Server.s.Log("autoload.txt error");
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                else
                {
                    Log("autoload.txt does not exist");
                }
            });

            ml.Queue(delegate
            {
                Log("Creating listening socket on port " + Server.port + "... ");
                if (Setup())
                {
                    if (upnp)
                    {
                        if (UpnpSetup())
                        {
                            s.Log("Ports have been forwarded with UPnP.");
                            upnpRunning = true;
                        }
                        else
                        {
                            s.Log("Could not auto forward ports with UPnP. Make sure you have UPnP enabled on your router.");
                            upnpRunning = false;
                        }

                    }
                    if (!upnp || upnp && upnpRunning)
                        s.Log("Done.");
                }
                else
                {
                    s.Log("Could not create socket connection.  Shutting down.");
                    return;
                }
            });

            ml.Queue(delegate
            {
                updateTimer.Elapsed += delegate
                {
                    Player.GlobalUpdate();
                    PlayerBot.GlobalUpdatePosition();
                };

                updateTimer.Start();
            });


            // Heartbeat code here:

            ml.Queue(delegate
            {
                try
                {
                    Heartbeat.Init();
                }
                catch (Exception e)
                {
                    Server.ErrorLog(e);
                }
            });
            // END Heartbeat code

            /*
            Thread processThread = new Thread(new ThreadStart(delegate
            {
                try
                {
                    PCCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                    ProcessCounter = new PerformanceCounter("Process", "% Processor Time", Process.GetCurrentProcess().ProcessName);
                    PCCounter.BeginInit();
                    ProcessCounter.BeginInit();
                    PCCounter.NextValue();
                    ProcessCounter.NextValue();
                }
                catch { }
            }));
            processThread.Start();
            */

            ml.Queue(delegate
            {
                messageTimer.Elapsed += delegate
                {
                    RandomMessage();
                };
                messageTimer.Start();

                process = System.Diagnostics.Process.GetCurrentProcess();

                if (File.Exists("text/messages.txt"))
                {
                    using (StreamReader r = File.OpenText("text/messages.txt"))
                        while (!r.EndOfStream)
                            messages.Add(r.ReadLine());
                }
                else File.Create("text/messages.txt").Close();

                if (useRemote) RemoteServer.Start();

                if (Server.irc) new IRCBot();
                new GlobalChatBot();
                //new AllServerChat();

                //if (Server.profanityFilter) ProfanityFilter.Load();

                // List Players that have agreed to rules
                if (!File.Exists("text/agreedtorules.txt")) { File.Create("text/agreedtorules.txt"); }
                // Updating shiz.
                try 
                {
                    using (WebClient w = new WebClient())
                    {
                        w.DownloadFile("http://updates.mcdawn.com/Changelog.txt", "Changelog.txt");
                        w.DownloadFile("http://updates.mcdawn.com/License.txt", "License.txt");
                        if (!File.Exists("GeoIP.dat")) { w.DownloadFile("http://updates.mcdawn.com/dll/GeoIP.dat", "GeoIP.dat"); }
                        if (!File.Exists("extra/sounds/chatupdate.wav")) { w.DownloadFile("http://dl.dropbox.com/u/43809284/chatupdate.wav", "extra/sounds/chatupdate.wav"); }
                        if (!File.Exists("Interop.NATUPNPLib.dll")) { w.DownloadFile("http://updates.mcdawn.com/dll/Interop.NATUPNPLib.dll", "Interop.NATUPNPLib.dll"); }
                    }
                }
                catch { }
                //      string CheckName = "FROSTEDBUTTS";

                //       if (Server.name.IndexOf(CheckName.ToLower())!= -1){ Server.s.Log("FROSTEDBUTTS DETECTED");}
                new AutoSaver(Server.backupInterval);     //2 and a half mins

                blockThread = new Thread(new ThreadStart(delegate
                {
                    while (true)
                    {
                        try
                        {
                            Thread.Sleep(blockInterval * 1000);
                            foreach (Level l in levels)
                            {
                                l.saveChanges();
                            }
                        }
                        catch { }
                    }
                }));
                blockThread.Start();

                locationChecker = new Thread(new ThreadStart(delegate
                {
                    while (true)
                    {
                        try
                        {
                            Thread.Sleep(3);
                            for (int i = 0; i < Player.players.Count; i++)
                            {
                                try
                                {
                                    Player p = Player.players[i];

                                    if (p.frozen)
                                    {
                                        unchecked { p.SendPos((byte)-1, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1]); } continue;
                                    }
                                    else if (p.following != "")
                                    {
                                        Player who = Player.Find(p.following);
                                        if (who == null || who.level != p.level)
                                        {
                                            p.following = "";
                                            if (!p.canBuild)
                                            {
                                                p.canBuild = true;
                                            }
                                            if (who != null && who.possess == p.name)
                                            {
                                                who.possess = "";
                                            }
                                            continue;
                                        }
                                        if (p.canBuild)
                                        {
                                            unchecked { p.SendPos((byte)-1, who.pos[0], (ushort)(who.pos[1] - 16), who.pos[2], who.rot[0], who.rot[1]); }
                                        }
                                        else
                                        {
                                            unchecked { p.SendPos((byte)-1, who.pos[0], who.pos[1], who.pos[2], who.rot[0], who.rot[1]); }
                                        }
                                    }
                                    else if (p.possess != "")
                                    {
                                        Player who = Player.Find(p.possess);
                                        if (who == null || who.level != p.level)
                                            p.possess = "";
                                    }

                                    ushort x = (ushort)(p.pos[0] / 32);
                                    ushort y = (ushort)(p.pos[1] / 32);
                                    ushort z = (ushort)(p.pos[2] / 32);

                                    if (p.level.Death)
                                        p.RealDeath(x, y, z);
                                    p.CheckBlock(x, y, z);

                                    p.oldBlock = (ushort)(x + y + z);
                                }
                                catch (Exception e) { Server.ErrorLog(e); }
                            }
                        }
                        catch { }
                    }
                }));
                ml.Queue(delegate
                {
                    Server.s.Log("MCDawn Omni-Ban list loaded.");
                    Server.s.Log("MCDawn Global-Ban list loaded.");
                    if (useDiscourager)
                    {
                        Discourager.LoadDiscouraged();
                        Server.s.Log("Discouraged users list loaded.");
                    }
                });

                locationChecker.Start();

                Log("Finished setting up server");

                ml.Queue(delegate
                {
                    try { PluginManager.AutoLoad(); }
                    catch (Exception ex)
                    {
                        ErrorLog(ex);
                        Log("Plugin load failed. Check error log for more info.");
                    }
                    if (s.OnServerStartEvent != null) s.OnServerStartEvent();
                });
            });
        }
        
        public static bool Setup()
        {
            try
            {
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, Server.port);
                listen = new Socket(endpoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listen.Bind(endpoint);
                listen.Listen((int)SocketOptionName.MaxConnections);

                listen.BeginAccept(new AsyncCallback(Accept), null);
                return true;
            }
            catch (SocketException e) { ErrorLog(e); return false; }
            catch (Exception e) { ErrorLog(e); return false; }
        }

        public static bool UpnpSetup()
        {
            try
            {
                ushort ar = Convert.ToUInt16(port);
                UpnpHelper Upnp = new UpnpHelper();
                if (Upnp.AddMapping(ar, "TCP", "MCDawn") == true)
                    return true;
                else return false;
            }
            catch (Exception e) { ErrorLog(e); Server.s.Log("Failed. Your router may not be compatible with UPnP."); return false; }
        }

        static void Accept(IAsyncResult result)
        {
            if (shuttingDown == false)
            {
                // found information: http://www.codeguru.com/csharp/csharp/cs_network/sockets/article.php/c7695
                // -Descention
                Player p = null;
                try
                {
                    p = new Player(listen.EndAccept(result));
                    listen.BeginAccept(new AsyncCallback(Accept), null);
                }
                catch (SocketException)
                {
                    if (p != null)
                        p.Disconnect();
                }
                catch (Exception e)
                {
                    ErrorLog(e);
                    if (p != null)
                        p.Disconnect();
                }
            }
        }

        public static void Exit()
        {
            if (OnServerExitEvent != null) OnServerExitEvent();

            if (noShutdown) return;

            if (useRemote) RemoteServer.Exit();

            for (int i = 0; i < Player.players.Count; i++)
            {
                try
                {
                    if (Player.players[i] != null)
                        if (!Server.customShutdown)
                            Player.players[i].Kick("Server shutdown. Rejoin in 10 seconds.");
                        else
                            Player.players[i].Kick(Server.customShutdownMessage);
                }
                catch { continue; }
            }

            //Player.players.ForEach(delegate(Player p) { p.Kick("Server shutdown. Rejoin in 10 seconds."); });
            Player.connections.ForEach(
            delegate(Player p)
            {
                if (!Server.customShutdown)
                {
                    p.Kick("Server shutdown. Rejoin in 10 seconds.");
                }
                else
                {
                    p.Kick(Server.customShutdownMessage);
                }
            }
            );
            try
            {
                if (upnpRunning)
                {
                    UPnPNATClass u = new UPnPNATClass();
                    //u.StaticPortMappingCollection.Remove(Server.port, "UDP");
                    u.StaticPortMappingCollection.Remove(Server.port, "TCP");
                    Server.s.Log("UPnP forwarded ports have been closed.");
                    Thread.Sleep(750);
                }
            }
            catch { }
            shuttingDown = true;
            if (listen != null)
                listen.Close();
        }

        public static void addLevel(Level level)
        {
            levels.Add(level);
        }

        public void PlayerListUpdate()
        {
            if (Server.s.OnPlayerListChange != null) Server.s.OnPlayerListChange(Player.players);
        }

        public void PlayerBotListUpdate()
        {
            if (Server.s.OnPlayerBotListChange != null) Server.s.OnPlayerBotListChange(PlayerBot.playerbots);
        }

        public void FailBeat()
        {
            if (HeartBeatFail != null) HeartBeatFail();
        }

        public void UpdateUrl(string url)
        {
            if (OnURLChange != null) OnURLChange(url);
        }

        public void Log(string message, bool systemMsg = false)
        {
            if (cli) { message = StripIllegalChars(message); }
            if (OnLog != null)
            {
                if (!systemMsg)
                {
                    OnLog(DateTime.Now.ToString("(HH:mm:ss) ") + message);
                }
                else
                {
                    OnSystem(DateTime.Now.ToString("(HH:mm:ss) ") + message);
                }
            }

            Logger.Write(DateTime.Now.ToString("(HH:mm:ss) ") + message + Environment.NewLine);
        }

        public void Log(string message)
        {
            if (cli) { message = StripIllegalChars(message); }
            if (OnLog != null)
            {
                OnLog(DateTime.Now.ToString("(HH:mm:ss) ") + message);
            }

            Logger.Write(DateTime.Now.ToString("(HH:mm:ss) ") + message + Environment.NewLine);
        }

        public string StripIllegalChars(string message) { return Regex.Replace(message, @"(&[0-9a-f])|(%[0-9a-f])", ""); }

        public void OpLog(string message, bool systemMsg = false)
        {
            if (OnOp != null)
            {
                if (!systemMsg)
                {
                    OnOp(DateTime.Now.ToString("(HH:mm:ss) ") + message);
                }
                else
                {
                    OnSystem(DateTime.Now.ToString("(HH:mm:ss) ") + message);
                }
            }

            Logger.Write(DateTime.Now.ToString("(HH:mm:ss) ") + message + Environment.NewLine);
        }

        public void AdminLog(string message, bool systemMsg = false)
        {
            if (OnAdmin != null)
            {
                if (!systemMsg)
                {
                    OnAdmin(DateTime.Now.ToString("(HH:mm:ss) ") + message);
                }
                else
                {
                    OnSystem(DateTime.Now.ToString("(HH:mm:ss) ") + message);
                }
            }

            Logger.Write(DateTime.Now.ToString("(HH:mm:ss) ") + message + Environment.NewLine);
        }

        public void ErrorCase(string message)
        {
            if (OnError != null)
                OnError(message);
        }

        public void CommandUsed(string message)
        {
            if (OnCommand != null) OnCommand(DateTime.Now.ToString("(HH:mm:ss) ") + message);
            Logger.Write(DateTime.Now.ToString("(HH:mm:ss) ") + message + Environment.NewLine);
        }

        public static void ErrorLog(Exception ex)
        {
            Logger.WriteError(ex);
            try
            {
                s.Log("!!!Error! See " + Logger.ErrorLogPath + " for more information.");
            } catch { }
            Player.GlobalMessageDevs(ex.ToString());
        }

        public static void RandomMessage()
        {
            if (Player.number != 0 && messages.Count > 0)
                Player.GlobalMessage(messages[new Random().Next(0, messages.Count)]);
        }

        internal void SettingsUpdate()
        {
            if (OnSettingsUpdate != null) OnSettingsUpdate();
        }

        public static string FindColor(string Username)
        {
            foreach (Group grp in Group.GroupList)
            {
                if (grp.playerList.Contains(Username)) return grp.color;
            }
            return Group.standard.color;
        }

        // Omni-Bans and Global-Bans
        internal static List<string> lastObUpdate = new List<string>();
        public static List<string> OmniBanned()
        {
            string url = "http://mcdawn.com/omniban.txt";
            List<string> backup = new List<string>("pinevil hawtcake 178.23.100.* legorek 80.3.166.* jetsviewfromcod1 valenx64 72.222.165.* creatorfromhell 98.236.* jackthekat9 173.170.182.*".Split(' ')); // offline list
            Thread t = new Thread(new ThreadStart(delegate
            {
                try
                {
                    using (WebClient w = new WebClient())
                        lastObUpdate = new List<string>(w.DownloadString(url).Split(' '));
                }
                catch { lastObUpdate = backup; }
            })); t.IsBackground = true; t.Priority = ThreadPriority.Lowest; t.Start();
            if (lastObUpdate.Count > 0) { return lastObUpdate; }
            else
            {
                List<string> toUpdate = new List<string>();
                try
                {
                    using (WebClient w = new WebClient())
                        toUpdate = new List<string>(w.DownloadString(url).Split(' '));
                }
                catch { return backup; }
                if (toUpdate.Count <= 0) { return backup; }
                return toUpdate;
            }
        }

        internal static List<string> lastGbUpdate = new List<string>();
        public static List<string> GlobalBanned()
        {
            string url = "http://mcdawn.com/globalban.txt";
            List<string> backup = new List<string>("notch herobrine".Split(' ')); // offline list
            Thread t = new Thread(new ThreadStart(delegate
            {
                try
                {
                    using (WebClient w = new WebClient())
                        lastGbUpdate = new List<string>(w.DownloadString(url).Split(' '));
                }
                catch { lastGbUpdate = backup; }
            })); t.IsBackground = true; t.Priority = ThreadPriority.Lowest; t.Start();
            if (lastGbUpdate.Count > 0) { return lastGbUpdate; }
            else
            {
                List<string> toUpdate = new List<string>();
                try
                {
                    using (WebClient w = new WebClient())
                        toUpdate = new List<string>(w.DownloadString(url).Split(' '));
                }
                catch { return backup; }
                if (toUpdate.Count <= 0) { return backup; }
                return toUpdate;
            }
        }

        public static string GetTextureMotd()
        {
            try
            {
                if (Server.motd.ToLower().Contains("cfg="))
                    using (WebClient w = new WebClient())
                    {
                        string temp = w.DownloadString("http://" + Server.motd.Substring(Server.motd.IndexOf("cfg=") + 4));
                        temp = temp.Substring(temp.ToLower().IndexOf("server.detail = ") + 16);
                        temp = temp.Remove(temp.ToLower().IndexOf("detail.user = "));
                        return temp.Trim();
                    }

                else return "";
            }
            catch { return ""; }
        }

        public static string GetTextureMotd(string s)
        {
            try
            {
                if (s.ToLower().Contains("cfg="))
                    using (WebClient w = new WebClient())
                    {
                        string temp = w.DownloadString("http://" + s.Substring(s.IndexOf("cfg=") + 4));
                        temp = temp.Substring(temp.ToLower().IndexOf("server.detail = ") + 16);
                        temp = temp.Remove(temp.ToLower().IndexOf("detail.user = "));
                        return temp.Trim();
                    }

                else return "";
            }
            catch { return ""; }
        }
    }
}