using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace MCDawn
{
    public sealed class Player
    {
        public static List<Player> players = new List<Player>();
        public static Dictionary<string, string> left = new Dictionary<string, string>();
        public static List<Player> connections = new List<Player>(Server.players);
        public static List<string> emoteList = new List<string>();
        public static int totalMySQLFailed = 0;
        public static byte number { get { return (byte)players.Count; } }
        public static int guests
        {
            get
            {
                int count = 0;
                foreach (Player pl in players) { if (pl.group.Permission <= Group.Find(Server.defaultRank).Permission) { count++; } }
                return count;
            }
        }
        public static int ops
        {
            get
            {
                int count = 0;
                foreach (Player pl in players) { if (pl.group.Permission >= Server.opchatperm) { count++; } }
                return count;
            }
        }
        static System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
        static MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        static SHA256Managed sha2 = new SHA256Managed();
        static UTF8Encoding utf8 = new UTF8Encoding();

        public static bool storeHelp = false;
        public static string storedHelp = "";

        Socket socket;
        System.Timers.Timer timeSpentTimer = new System.Timers.Timer(1000);
        System.Timers.Timer loginTimer = new System.Timers.Timer(1000);
        public System.Timers.Timer pingTimer = new System.Timers.Timer(2000);
        System.Timers.Timer extraTimer = new System.Timers.Timer(200000);
        public System.Timers.Timer afkTimer = new System.Timers.Timer(2000);

        public int afkCount = 0;
        public DateTime afkStart;

        public bool megaBoid = false;
        public bool cmdTimer = false;

        byte[] buffer = new byte[0];
        byte[] tempbuffer = new byte[0xFF];
        public bool disconnected = false;

        public string timeSpent = "0 0 0 1";
        public string name; // internal set is kk, but i want to see if any retard tries to change name and bypass dis shiet (internal originalName will protect, dw) ;D
        public string displayName;
        internal string originalName;
        //public string skinname;
        public byte id;
        public int userID = -1;
        public string ip;
        public string color;
        public Group group;
        public bool nocaps = false;
        public bool hidden = false;
        public bool painting = false;
        public bool brushing = false;
        public bool muted = false;
        public bool deafened = false;
        public bool jailed = false;
        public bool invincible = false;
        public bool handcuffed = false;
        public bool referee = false;
        public string prefix = "";
        public string title = "";
        public string titlecolor;
        // Title Brackets
        // 0 = [], 1 = (), 2 = {}, 3 = ~~, 4 = <>
        public int titlebracket = 0;
        public string tbracketstart = "[";
        public string tbracketend = "]";
        public bool usereview = true;
        public bool levelchat = false;
        public int warnings = 0;
        public string password = "";
        public string wompassword = "";
        public string tpRequest = "";
        public string summonRequest = "";
        public bool autobuild = false;

        public int activeCuboids = 0;

        // Agreed To Rules
        //public bool agreedToRules = false;
        public bool readRules = false;

        //wom
        public bool haswom = false;
        public string womversion = "";
        public string userlinetype = "blockinfo";
        public string customuserline = "";

        public int killstreak = 0;
        //Infection
        public bool infected = false;
        //Spleef
        public bool spleefAlive = true;
        // PushBall
        public PushBallTeam pushBallTeam;
        public string PushBalltempcolor = "";
        public string PushBalltempprefix = "";
        public int pushBallGoals = 0;
        // Fallout
        public bool FalloutAlive = true;

        public bool deleteMode = false;
        public bool ignorePermission = false;
        public bool ignoreGrief = false;
        public bool parseSmiley = true;
        public bool smileySaved = true;
        public bool opchat = false;
        public bool adminchat = false;
        public bool devchat = false;
        public bool devstaffchat = false;
        public bool rankchat = false;
        public string chatroom = "";
        public string chatroomInvite = "";
        public bool chatroomOp = false;
        public bool chatroomAdmin = false;
        public int passtries = 0;
        public bool onWhitelist = false;
        public bool whisper = false;
        public string whisperTo = "";
        public bool running = false;

        public string loginmessage
        {
            get
            {
                if (File.Exists("extra/logins.txt"))
                    foreach (string line in File.ReadAllLines("extra/logins.txt"))
                        if (line.Split(' ')[0].ToLower().Trim() == name.ToLower().Trim())
                            return line.Substring(line.Trim().IndexOf(" ")).Trim();
                return "";
            }
        }
        public string logoutmessage
        {
            get
            {
                if (File.Exists("extra/logouts.txt"))
                    foreach (string line in File.ReadAllLines("extra/logouts.txt"))
                        if (line.Split(' ')[0].ToLower().Trim() == name.ToLower().Trim())
                            return line.Substring(line.Trim().IndexOf(" ")).Trim();
                return "";
            }
        }

        public string storedMessage = "";
             
        public bool trainGrab = false;
        public bool onTrain = false;

        public bool frozen = false;
        public string following = "";
        public string possess = "";
        
        // Only used for possession.
        //Using for anything else can cause unintended effects!
        public bool canBuild = true;

        public int money = 0;
        public Int64 overallBlocks = 0;
        public Int64 destroyedBlocks = 0;
        public Int64 builtBlocks
        {
            get
            {
                if (overallBlocks - destroyedBlocks < 0) return 0;
                return overallBlocks - destroyedBlocks;
            }
        }
        public int loginBlocks = 0;
        public string countryName = "";

        public DateTime timeLogged;
        public DateTime firstLogin;
        public int totalLogins = 0;
        public int totalKicked = 0;
        public int overallDeath = 0;

        public string savedcolor = "";

        public bool staticCommands = false;

        public DateTime ZoneSpam;
        public bool ZoneCheck = false;
        public bool zoneDel = false;

        public Thread commThread;
        public Thread buildingThread;
        public bool commUse = false;

        public bool aiming;
        public bool isFlying = false;

        public bool joker = false;
        // Admin Security
        public bool unverified = false;
        public bool grantpassed = false;
        // Developer Security
        public bool devUnverified = false;
        // Profanity Filter
        public int swearWordsUsed = 0;
        // Ignore List
        public List<string> ignoreList = new List<string>();

        public bool voice = false;
        public string voicestring = "";

        // Custom Command object storing. Allows custom command makers to have more control and abilites to do things.
        public object[] CustomCommand = new object[] { };

        //CTF
        public CTFTeam team;
        public CTFTeam hasflag;
        public string CTFtempcolor;
        public string CTFtempprefix;
        public bool carryingFlag;
        public bool spawning = false;
        public bool teamchat = false;
        public int health = 100;

        //Copy
        public List<CopyPos> CopyBuffer = new List<CopyPos>();
        public struct CopyPos { public ushort x, y, z; public byte type; }
        public bool copyAir = false;
        public int[] copyoffset = new int[3] { 0, 0, 0 };
        public ushort[] copystart = new ushort[3] { 0, 0, 0 };

        //Undo
        public struct UndoPos { public ushort x, y, z; public byte type, newtype; public string mapName; public DateTime timePlaced; }
        public List<UndoPos> UndoBuffer = new List<UndoPos>();
        public List<UndoPos> RedoBuffer = new List<UndoPos>();
        

        public bool showPortals = false;
        public bool showMBs = false;
        public bool showRealNames = false;

        public Level level = Server.mainLevel;

        public string prevMsg = "";


        //Movement
        public ushort oldBlock = 0;
        public ushort deathCount = 0;
        public byte deathBlock;

        //Games
        public DateTime lastDeath = DateTime.Now;

        //PlayerGroups
        public string groupInvitation = "";
        public PlayerGroup[] playerGroup;
        public GroupRank[] playerGroupRank = { GroupRank.Guest };
        
        public byte BlockAction = 0;  //0-Nothing 1-solid 2-lava 3-water 4-active_lava 5 Active_water 6 OpGlass 7 BluePort 8 OrangePort
        public byte modeType = 0;
        public byte[] bindings = new byte[128];
        public string[] cmdBind = new string[10];
        public string[] messageBind = new string[10];
        public string lastCMD = "";
        public string lastMSG = "";
        public string lastRankReason = "";

        public bool Loading = true;     //True if player is loading a map.

		// Events

		public delegate void OnPlayerKickedEventHandler(Player p, string reason);
		public static event OnPlayerKickedEventHandler OnPlayerKickedEvent = null;
		
		public delegate void OnKickedEventHandler(string reason);
		public event OnKickedEventHandler OnKickedEvent = null;

		public delegate void OnPlayerSendMessageEventHandler(Player p, string message);
		public static event OnPlayerSendMessageEventHandler OnPlayerSendMessageEvent = null;
		
		public delegate void OnSendMessageEventHandler(string message);
		public event OnSendMessageEventHandler OnSendMessageEvent = null;

		public delegate void OnPlayerJoinEventHandler(Player p);
		public static event OnPlayerJoinEventHandler OnPlayerJoinEvent = null;

		public delegate void BlockchangeEventHandler(Player p, ushort x, ushort y, ushort z, byte type);
		public event BlockchangeEventHandler Blockchange = null;

        public delegate void OnBlockchangeEventHandler(Player p, ushort x, ushort y, ushort z, byte type);
        public event OnBlockchangeEventHandler OnBlockchange = null;

		public delegate void OnDisconnectEventHandler(string kickString, bool skip);
		public event OnDisconnectEventHandler OnDisconnectEvent = null;
		
		public delegate void OnPlayerDisconnectEventHandler(Player p, string kickString, bool skip);
		public static event OnPlayerDisconnectEventHandler OnPlayerDisconnectEvent = null;

        public delegate void OnPlayerMoveEventHandler(Player p, ushort x, ushort y, ushort z, byte rotx, byte roty);
        public static event OnPlayerMoveEventHandler OnPlayerMoveEvent = null;

        public delegate void OnMoveEventHandler(ushort x, ushort y, ushort z, byte rotx, byte roty);
        public event OnMoveEventHandler OnMoveEvent = null;

        public delegate void OnPlayerChatEventHandler(Player p, string text);
        public static event OnPlayerChatEventHandler OnPlayerChatEvent = null;

        public delegate void OnChatEventHandler(string text);
        public event OnChatEventHandler OnChatEvent = null;

        public delegate void OnPlayerCommandEventHandler(Player p, string cmd, string message);
        public static event OnPlayerCommandEventHandler OnPlayerCommandEvent = null;

        public delegate void OnCommandEventHandler(string cmd, string message);
        public event OnCommandEventHandler OnCommandEvent = null;

        public delegate void OnPlayerDeathEventHandler(Player p, byte b, string customMessage, bool explode);
        public static event OnPlayerDeathEventHandler OnPlayerDeathEvent = null;

        public delegate void OnDeathEventHandler(byte b, string customMessage, bool explode);
        public event OnDeathEventHandler OnDeathEvent = null;

        public bool noKick = false, noSendMessage = false, noJoin = false, noDisconnect = false, noMove = false, noChat = false, noCommand = false, noDeath = false, noBuild = false;

		// End Events

        //public event RankChangeHandler Rankchange = null;
        //public delegate void RankChangeHandler(Player p, string newgrpname);
        //public event NameChangeHandler Namechange = null;
        //public delegate void NameChangeHandler(Player p, string newName);
        public void ClearBlockchange() { Blockchange = null; }
        public bool HasBlockchange() { return (Blockchange != null); }
        public object blockchangeObject = null;
        public ushort[] lastClick = new ushort[3] { 0, 0, 0 };

        public ushort[] pos = new ushort[3] { 0, 0, 0 };
        ushort[] oldpos = new ushort[3] { 0, 0, 0 };
        ushort[] basepos = new ushort[3] { 0, 0, 0 };
        public byte[] rot = new byte[2] { 0, 0 };
        byte[] oldrot = new byte[2] { 0, 0 };

        // Grief/Block Spam Detection
        public static int spamBlockCount = 200;
        public static int spamBlockTimer = 5;
        Queue<DateTime> spamBlockLog = new Queue<DateTime>(spamBlockCount);

        // Chat Spam Detection
        public int sameMSGs;
        public static string lastChatted;
        public int sentMSGs = 0;
        /*public static int spamChatCount = 3;
        public static int spamChatTimer = 4;
        Queue<DateTime> spamChatLog = new Queue<DateTime>(spamChatCount);*/

        public bool loggedIn = false;

        public static bool IsLocalhostIP(string ip) { return (ip.StartsWith("127.0.0.") | ip.StartsWith("192.168.") | ip.StartsWith("10.10.")); }
        public Player(Socket s)
        {
            try
            {
                socket = s;
                ip = socket.RemoteEndPoint.ToString().Split(':')[0];
                Server.s.Log(ip + " connected to the server.");
                try
                {
                    if (Server.useMaxMind) countryName = Server.iploopup.getCountry(IPAddress.Parse(IsLocalhostIP(ip) ? Server.GetIPAddress() : ip)).getName();
                    else countryName = "N/A";
                }
                catch { countryName = "N/A"; }
                if (IsLocalhostIP(ip)) countryName = "Localhost (" + countryName + ")";

                for (byte i = 0; i < 128; ++i) bindings[i] = i;

                socket.BeginReceive(tempbuffer, 0, tempbuffer.Length, SocketFlags.None, new AsyncCallback(Receive), this);
                // Time Spent on Server
                timeSpentTimer.Elapsed += delegate
                {
                    if (!Loading)
                    {
                        try
                        {
                            int Days = Convert.ToInt32(timeSpent.Split(' ')[0]);
                            int Hours = Convert.ToInt32(timeSpent.Split(' ')[1]);
                            int Minutes = Convert.ToInt32(timeSpent.Split(' ')[2]);
                            int Seconds = Convert.ToInt32(timeSpent.Split(' ')[3]);
                            Seconds++;
                            if (Seconds >= 60)
                            {
                                Minutes++;
                                Seconds = 0;
                            }
                            if (Minutes >= 60)
                            {
                                Hours++;
                                Minutes = 0;
                            }
                            if (Hours >= 24)
                            {
                                Days++;
                                Hours = 0;
                            }
                            timeSpent = Days + " " + Hours + " " + Minutes + " " + Seconds;
                        }
                        catch { timeSpent = "0 0 0 1"; }
                    }
                };
                timeSpentTimer.Start();
                loginTimer.Elapsed += delegate
                {
                    if (!Loading)
                    {
                        loginTimer.Stop();

                        if (File.Exists("text/welcome.txt"))
                        {
                            try
                            {
                                List<string> welcome = new List<string>();
                                StreamReader wm = File.OpenText("text/welcome.txt");
                                while (!wm.EndOfStream)
                                    welcome.Add(wm.ReadLine());

                                wm.Close();
                                wm.Dispose();

                                foreach (string w in welcome)
                                    SendMessage(w);
                            }
                            catch { }
                        }
                        else
                        {
                            Server.s.Log("Could not find Welcome.txt. Using default.");
                            File.WriteAllText("text/welcome.txt", "Welcome to my server!");
                        }
                        extraTimer.Start();
                    }
                }; loginTimer.Start();

                pingTimer.Elapsed += delegate { SendPing(); };
                pingTimer.Start();

                extraTimer.Elapsed += delegate
                {
                    //extraTimer.Stop();

                    try
                    {
                        if (!Group.Find("Nobody").commands.Contains("inbox") && !Group.Find("Nobody").commands.Contains("send"))
                        {
                            DataTable Inbox = MySQL.fillData("SELECT * FROM `Inbox" + name + "`", true);

                            SendMessage("&cYou have &f" + Inbox.Rows.Count + "&g &cmessages in /inbox");
                            Inbox.Dispose();
                        }
                    }
                    catch { }
                    if (Server.updateTimer.Interval > 1000) SendMessage("Lowlag mode is currently &aON.");
                    try
                    {
                        if (!Group.Find("Nobody").commands.Contains("pay") && !Group.Find("Nobody").commands.Contains("give") && !Group.Find("Nobody").commands.Contains("take")) SendMessage("You currently have &a" + money + "&g " + Server.moneys);
                    }
                    catch { }
                    SendMessage("You have modified &a" + overallBlocks + "&g blocks!");
                    if (players.Count == 1)
                        SendMessage("There is currently &a" + players.Count + " player online.");
                    else
                        SendMessage("There are currently &a" + players.Count + " players online.");
                    try
                    {
                        if (!Group.Find("Nobody").commands.Contains("award") && !Group.Find("Nobody").commands.Contains("awards") && !Group.Find("Nobody").commands.Contains("awardmod")) SendMessage("You have " + Awards.awardAmount(name) + " awards.");
                    }
                    catch { }

                    int days = Convert.ToInt32(timeSpent.Split(' ')[0]);
                    int hours = Convert.ToInt32(timeSpent.Split(' ')[1]);
                    if (days >= 1) { hours += days * 24; }

                    Group nextGroup = null; bool nextOne = false;
                    for (int i = 0; i < Group.GroupList.Count; i++)
                    {
                        Group grp = Group.GroupList[i];
                        if (nextOne)
                        {
                            if (grp.Permission >= LevelPermission.Nobody) break;
                            nextGroup = grp;
                            break;
                        }
                        if (grp == group)
                            nextOne = true;
                    }

                    if (group.reqHours > 0 && hours >= group.reqHours)
                    {
                        SendMessage("You have &b" + hours + "&g hours, enough for a promotion!");
                        SendMessage("Type &b/timerank" + "&g to rank up.");
                    }

                    int reports = 0;
                    if (!Directory.Exists("extra/reports/")) { Directory.CreateDirectory("extra/reports/"); }
                    foreach (string report in Directory.GetFiles("extra/reports/"))
                        if (File.ReadAllLines("extra/reports/" + report)[0].ToLower() == "unread")
                            reports++;
                    if (reports > 0) SendMessage("There are " + reports + " unread reports. Type &c/report view&g to see them.");

                    if (Server.useMySQL) Command.all.Find("viewlikes").Use(this, "");
                };

                afkTimer.Elapsed += delegate
                {
                    if (name == "") return;

                    if (Server.afkset.Contains(name))
                    {
                        afkCount = 0;
                        if (Server.afkkick > 0 && group.Permission < LevelPermission.Operator)
                            if (afkStart.AddMinutes(Server.afkkick) < DateTime.Now)
                                Kick("Auto-kick, AFK for " + Server.afkkick + " minutes");
                        if ((oldpos[0] != pos[0] || oldpos[1] != pos[1] || oldpos[2] != pos[2]) && (oldrot[0] != rot[0] || oldrot[1] != rot[1]))
                            Command.all.Find("afk").Use(this, "");
                    }
                    else
                    {
                        if (oldpos[0] == pos[0] && oldpos[1] == pos[1] && oldpos[2] == pos[2] && oldrot[0] == rot[0] && oldrot[1] == rot[1])
                            afkCount++;
                        else
                            afkCount = 0;

                        if (afkCount > Server.afkminutes * 30)
                        {
                            if (this != null && !String.IsNullOrEmpty(this.name))
                            {
                                Command.all.Find("afk").Use(this, "auto: Not moved for " + Server.afkminutes + " minutes");
                                afkStart = DateTime.Now;
                                afkCount = 0;
                            }
                        }
                    }
                };
                if (Server.afkminutes > 0) afkTimer.Start();

                connections.Add(this);
                if (sameMSGs > 0)
                    sameMSGs = 0;
            }
            catch (Exception e) { Kick("Login failed!"); Server.ErrorLog(e); }
        }

        public void save()
        {
            string commandString =
                "UPDATE Players SET IP='" + ip + "'" +
                ", displayName='" + displayName + "'" +
                ", LastLogin='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                ", totalLogin=" + totalLogins +
                ", totalDeaths=" + overallDeath +
                ", Money=" + money +
                ", totalBlocks=" + overallBlocks + " + " + loginBlocks +
                ", totalKicked=" + totalKicked +
                ", TimeSpent='" + timeSpent + "'" +
                ", titleBracket=" + titlebracket +
                ", countryName='" + countryName + "'" +
                ", HasWOM='" + womversion.Trim() + "'" + 
                ", destroyedBlocks=" + destroyedBlocks + 
                ", lastRankReason='" + lastRankReason + "'" + 
                " WHERE Name='" + originalName + "'";

            MySQL.executeQuery(commandString);

            try
            {
                if (!smileySaved)
                {
                    if (parseSmiley)
                        emoteList.RemoveAll(s => s == name);
                    else
                        emoteList.Add(name);

                    File.WriteAllLines("text/emotelist.txt", emoteList.ToArray());
                    smileySaved = true;
                }
            }
            catch (Exception e)
            { 
                Server.ErrorLog(e);
            }
        }

        #region == INCOMING ==
        static void Receive(IAsyncResult result)
        {
        //    Server.s.Log(result.AsyncState.ToString());
            Player p = (Player)result.AsyncState;
            if (p.disconnected)
                return;
            try
            {
                int length = p.socket.EndReceive(result);
                if (length == 0) { p.Disconnect(); return; }

                byte[] b = new byte[p.buffer.Length + length];
                Buffer.BlockCopy(p.buffer, 0, b, 0, p.buffer.Length);
                Buffer.BlockCopy(p.tempbuffer, 0, b, p.buffer.Length, length);

                p.buffer = p.HandleMessage(b);
                p.socket.BeginReceive(p.tempbuffer, 0, p.tempbuffer.Length, SocketFlags.None,
                                      new AsyncCallback(Receive), p);
            }
            catch (SocketException)
            {
                p.Disconnect();
            }
            catch (Exception e)
            {
                Server.ErrorLog(e);
                if (p != null) p.Kick("Error!");
                Server.s.Log("Attempting to restart socket...");
                Server.listen = null;
                if (Server.Setup()) { Server.s.Log("Listening socket on " + Server.port + " restarted."); }
                else { Server.s.Log("Failed to restart listening socket."); }
            }
        }
        byte[] HandleMessage(byte[] buffer)
        {
            try
            {
                int length = 0; byte msg = buffer[0];
                // Get the length of the message by checking the first byte
                switch (msg)
                {
                    case 0:
                        length = 130;
                        break; // login
                    case 5:
                        if (!loggedIn)
                            goto default;
                        length = 8;
                        break; // blockchange
                    case 8:
                        if (!loggedIn)
                            goto default;
                        length = 9;
                        break; // input
                    case 13:
                        if (!loggedIn)
                            goto default;
                        length = 65;
                        break; // chat
                    default:
                        Kick("Unhandled message id \"" + msg + "\"!");
                        return new byte[0];
                }
                if (buffer.Length > length)
                {
                    byte[] message = new byte[length];
                    Buffer.BlockCopy(buffer, 1, message, 0, length);

                    byte[] tempbuffer = new byte[buffer.Length - length - 1];
                    Buffer.BlockCopy(buffer, length + 1, tempbuffer, 0, buffer.Length - length - 1);

                    buffer = tempbuffer;

                    // Thread thread = null; 
                    switch (msg)
                    {
                        case 0:
                            HandleLogin(message);
                            break;
                        case 5:
                            if (!loggedIn)
                                break;
                            HandleBlockchange(message);
                            break;
                        case 8:
                            if (!loggedIn)
                                break;
                            HandleInput(message);
                            break;
                        case 13:
                            if (!loggedIn)
                                break;
                            HandleChat(message);
                            break;
                    }
                    //thread.Start((object)message);
                    if (buffer.Length > 0)
                        buffer = HandleMessage(buffer);
                    else
                        return new byte[0];
                }
            }
            catch (Exception e)
            {
                Server.ErrorLog(e);
            }
            return buffer;
        }
        void HandleLogin(byte[] message)
        {
            try
            {
                //byte[] message = (byte[])m;
                if (loggedIn)
                    return;

                byte version = message[0];
                name = enc.GetString(message, 1, 64).Trim();
                originalName = name;
                string verify = enc.GetString(message, 65, 32).Trim();
                byte type = message[129];

                if (Player.OnPlayerJoinEvent != null) Player.OnPlayerJoinEvent(this);

                if (noJoin)
                {
                    try { if (this != null) { Disconnect(); } }
                    catch { }
                    return;
                }

                group = Group.findPlayerGroup(name);

                try
                {
                    if (Discourager.discouraged.Contains(name.ToLower().Trim()) && Server.useDiscourager && this != null)
                        if (Convert.ToBoolean(new Random().Next(0, 1)))
                            Kick(Server.customBanMessage);
                }
                catch { }

                try
                {
                    Server.TempBan tBan = Server.tempBans.Find(tB => tB.name.ToLower() == name.ToLower());
                    if (tBan.allowedJoin < DateTime.Now)
                    {
                        Server.tempBans.Remove(tBan);
                    }
                    else
                    {
                        Kick("You're still banned (temporary ban)!");
                    }
                } catch { }

                bool omniBanned = false;
                for (int i = 0; i < Server.OmniBanned().Count; i++)
                    if (Server.OmniBanned().Contains("*") && (name.ToLower().StartsWith(Server.OmniBanned()[i].ToLower().Replace("*", "")) || ip.StartsWith(Server.OmniBanned()[i].ToLower().Replace("*", ""))))
                        omniBanned = true;

                if (Server.OmniBanned().Contains(name.ToLower()) || Server.OmniBanned().Contains(ip) || omniBanned)
                {
                    try
                    {
                        if (Server.showAttemptedLogins)
                        {
                            if (this != null)
                            {
                                GlobalMessage("[" + this.ip + "] " + this.group.color + this.name + "&g could not log in (Player is Omni-Banned).");
                                Server.s.Log("[" + this.ip + "] " + this.name + " could not log in (Player is Omni-Banned).");
                            }
                        }
                    }
                    catch { }
                    Kick("You have been Omni-Banned. Visit www.mcdawn.com for appeal."); 
                    return; 
                }
                // Whitelist check.
                if (Server.useWhitelist)
                {
                    if (Server.verify)
                    {
                        if (Server.whiteList.Contains(name) || Server.hasProtection(originalName.ToLower()))
                        {
                            onWhitelist = true;
                        }
                    }
                    else
                    {
                        // Verify Names is off.  Gotta check the hard way.
                        DataTable ipQuery = MySQL.fillData("SELECT Name FROM Players WHERE IP = '" + ip + "'");

                        if (ipQuery.Rows.Count > 0)
                        {
                            if (ipQuery.Rows.Contains(name) && Server.whiteList.Contains(name))
                            {
                                onWhitelist = true;
                            }
                        }
                        ipQuery.Dispose();
                    }
                }
                if (this != null && Server.bannedIP.Contains(ip) && (!Server.devs.Contains(originalName.ToLower()) && !Server.staff.Contains(originalName.ToLower()) && !Server.administration.Contains(originalName.ToLower())))
                {
                    if (Server.useWhitelist)
                    {
                        if (!onWhitelist)
                        {
                            try
                            {
                                if (Server.showAttemptedLogins)
                                {
                                    if (this != null)
                                    {
                                        Player.GlobalMessage("[" + this.ip + "] " + this.group.color + this.name + "&g could not log in (Player is IP-Banned).");
                                        Server.s.Log("[" + this.ip + "] " + this.name + " could not log in (Player is IP-Banned).");
                                    }
                                }
                            }
                            catch { }
                            Kick(Server.customBanMessage);
                            return;
                        }
                    }
                    else
                    {
                        try
                        {
                            if (Server.showAttemptedLogins)
                            {
                                if (this != null)
                                {
                                    Player.GlobalMessage("[" + this.ip + "] " + this.group.color + this.name + "&g could not log in (Player is IP-Banned).");
                                    Server.s.Log("[" + this.ip + "] " + this.name + " could not log in (Player is IP-Banned).");
                                }
                            }
                        }
                        catch { }
                        Kick(Server.customBanMessage);
                        return;
                    }
                }
                // Removed, some ppl having problems with this O.o
                /*if (connections.Count >= 5) 
                {
                    try
                    {
                        if (Server.showAttemptedLogins)
                        {
                            if (this != null)
                            {
                                Player.GlobalMessage("[" + this.ip + "] " + this.group.color + this.name + "&g could not log in (Too many connections).");
                                Server.s.Log("[" + this.ip + "] " + this.name + " could not log in (Too many connections).");
                            }
                        }
                    }
                    catch { }
                    Kick("Too many connections!"); 
                    return; 
                }*/

                if (Group.findPlayerGroup(name) == Group.findPerm(LevelPermission.Banned) && !Server.hasProtection(originalName.ToLower()))
                {
                    if (Server.useWhitelist)
                    {
                        if (!onWhitelist)
                        {
                            try
                            {
                                if (Server.showAttemptedLogins)
                                {
                                    if (this != null)
                                    {
                                        Player.GlobalMessage("[" + this.ip + "] " + this.group.color + this.name + "&g could not log in (Player is Banned).");
                                        Server.s.Log("[" + this.ip + "] " + this.name + " could not log in (Player is Banned).");
                                    }
                                }
                            }
                            catch { }
                            Kick(Server.customBanMessage);
                            return;
                        }
                    }
                    else
                    {
                        try
                        {
                            if (Server.showAttemptedLogins)
                            {
                                if (this != null)
                                {
                                    Player.GlobalMessage("[" + this.ip + "] " + this.group.color + this.name + "&g could not log in (Player is Banned).");
                                    Server.s.Log("[" + this.ip + "] " + this.name + " could not log in (Player is Banned).");
                                }
                            }
                        }
                        catch { }
                        Kick(Server.customBanMessage);
                        return;
                    }
                }

                if (Server.maintenance && !ip.StartsWith("127.0.0") && !ip.StartsWith("192.168.") && group.Permission < Server.canjoinmaint && !Server.hasProtection(name))
                {
                    try
                    {
                        if (Server.showAttemptedLogins)
                        {
                            if (this != null)
                            {
                                Player.GlobalMessage("[" + this.ip + "] " + this.group.color + this.name + "&g could not log in (Maintenence Mode).");
                                Server.s.Log("[" + this.ip + "] " + this.name + " could not log in (Maintenence mode).");
                            }
                        }
                    }
                    catch { }
                    Kick("Maintenance Mode! Join again Later!");
                    return;
                }
                if (((Player.players.Count >= Server.players) || (Player.guests >= Server.maxguests && group.Permission <= Group.Find(Server.defaultRank).Permission)) && !ip.StartsWith("127.0.0") && !ip.StartsWith("192.168.") && !Server.hasProtection(name))
                {
                    if (Server.showAttemptedLogins)
                    {
                        try
                        {
                            if (this != null)
                            {
                                Player.GlobalMessage("[" + this.ip + "] " + this.group.color + this.name + "&g could not log in (Server full).");
                                Server.s.Log("[" + this.ip + "] " + this.name + " could not log in (Server full).");
                            }
                        }
                        catch { }
                    }
                    Kick("Server full!"); 
                    return; 
                }
                //Proxies
                try
                {
                    if (Server.iploopup.getCountry(IPAddress.Parse(ip)).getName().Equals("Anonymous Proxy") && !ip.StartsWith("192.168.") && !ip.StartsWith("127.0.0") && !Server.allowproxy)
                    {
                        try
                        {
                            if (Server.showAttemptedLogins)
                            {
                                if (this != null)
                                {
                                    Player.GlobalMessage("[" + this.ip + "] " + this.group.color + this.name + "&g could not log in (Proxies not allowed).");
                                    Server.s.Log("[" + this.ip + "] " + this.name + " could not log in (Proxies not allowed).");
                                }
                            }
                        }
                        catch { }
                        Kick("Proxies are not allowed here");
                        return;
                    }
                    else if (Server.allowproxy && Server.iploopup.getCountry(IPAddress.Parse(ip)).getName().Equals("Anonymous Proxy") && !ip.StartsWith("192.168.") && !ip.StartsWith("127.0.0") && !ip.StartsWith("10.1."))
                    {
                        Player.GlobalMessageOps(name + " has joined via a proxy");
                        //Player.GlobalMessageOps("You can disallow proxies via the settings");
                    }
                }
                catch { }
                if (version != Server.version) 
                {
                    try
                    {
                        if (Server.showAttemptedLogins)
                        {
                            if (this != null)
                            {
                                Player.GlobalMessage("[" + this.ip + "] " + this.group.color + this.name + "&g could not log in (Wrong Version).");
                                Server.s.Log("[" + this.ip + "] " + this.name + " could not log in (Wrong Version).");
                            }
                        }
                    }
                    catch { }
                    Kick("Wrong version!"); 
                    return; 
                }
                
                if (!ValidName(name))
                {
                    try
                    {
                        if (Server.showAttemptedLogins)
                        {
                            if (this != null)
                            {
                                Player.GlobalMessage("[" + this.ip + "] " + this.group.color + this.name + "&g could not log in (" + (Regex.IsMatch(name, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$") ? "Illegal Name" : "3rd party game client with migrated account") + ").");
                                Server.s.Log("[" + this.ip + "] " + this.name + " could not log in (" + (Regex.IsMatch(name, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$") ? "Illegal Name" : "3rd party game client with migrated account") + ").");
                            }
                        }
                    }
                    catch { }
                    Kick(Regex.IsMatch(name, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$") ? "Don't use a 3rd party game client with a migrated account!" : "Illegal name!");
                    return;
                }

                bool wompasswordverified = false;
                if (Server.useWOMPasswords && File.Exists("wompasswords/" + name.ToLower() + ".xml"))
                    if (WOMPasswordFormat(verify) == File.ReadAllText("wompasswords/" + name.ToLower() + ".xml"))
                        wompasswordverified = true;

                if ((Server.verify || Server.hasProtection(name)) && !wompasswordverified)
                {
                    if (verify == "--" || verify != 
                        BitConverter.ToString(md5.ComputeHash(enc.GetBytes(Server.salt + name)))
                        .Replace("-", "").ToLower().TrimStart('0'))
                    {
                        if (!ip.StartsWith("127.0.0") && !ip.StartsWith("192.168.") || Server.hasProtection(name))
                        {
                            try
                            {
                                if (Server.showAttemptedLogins)
                                {
                                    if (this != null)
                                    {
                                        Player.GlobalMessage("[" + this.ip + "] " + this.group.color + this.name + "&g could not log in (Could not verify name).");
                                        Server.s.Log("[" + this.ip + "] " + this.name + " could not log in (Could not verify name).");
                                    }
                                }
                            }
                            catch { }
                            Kick("Login failed! Try again."); return;
                        }
                    }
                }

                foreach (Player p in players)
                {
                    if (p.name == name)
                    {
                        if (Server.verify)
                        {
                            if (!Server.devs.Contains(p.name.ToLower())) 
                                p.Kick("Someone logged in as you!"); 
                            else 
                                leftGame("Someone logged in as you!");
                            break;
                        }
                        else 
                        {
                            try
                            {
                                if (Server.showAttemptedLogins)
                                {
                                    if (this != null)
                                    {
                                        Player.GlobalMessage("[" + this.ip + "] " + this.group.color + this.name + "&g could not log in (Already logged in).");
                                        Server.s.Log("[" + this.ip + "] " + this.name + " could not log in (Already logged in).");
                                    }

                                }
                            }
                            catch { }
                            Kick("Already logged in!"); 
                            return; 
                        }
                    }
                }


                try { left.Remove(name.ToLower()); }
                catch { }

                SendMotd();
                SendMap();
                Loading = true;

                if (disconnected) return;

                loggedIn = true;
                id = FreeId();

                players.Add(this);
                connections.Remove(this);

                if (File.Exists("extra/ignore/" + name.ToLower() + ".txt")) { IgnoreLoad(this); }

                Server.s.PlayerListUpdate();
                
                if (Player.OnPlayerJoinEvent != null) Player.OnPlayerJoinEvent(this);

                if (String.IsNullOrEmpty(displayName)) { displayName = name; }

                //Test code to show when people come back with different accounts on the same IP
                string temp = "Lately known as:";
                bool found = false;
                if (!ip.StartsWith("127.0.0"))
                {
                    foreach (KeyValuePair<string, string> prev in left)
                    {
                        if (prev.Value == ip)
                        {
                            found = true;
                            temp += " " + prev.Key;
                        }
                    }
                    if (found)
                    {
                        GlobalMessageOps(temp);
                        Server.s.Log(temp);
                        IRCBot.Say(temp, true);       //Tells people in op channel on IRC
                        //AllServerChat.Say(temp);
                    }
                }
            }
            catch (Exception e)
            {
                Server.ErrorLog(e);
                Player.GlobalMessage("An error occurred: " + e.Message);
            }

            DataTable playerDb = MySQL.fillData("SELECT * FROM Players WHERE Name='" + originalName + "'");

            if (playerDb.Rows.Count == 0)
            {
                this.displayName = originalName;
                this.prefix = "";
                this.timeSpent = "0 0 0 1";
                this.titlecolor = "";
                this.titlebracket = 0;
                this.color = group.color;
                this.money = 0;
                this.firstLogin = DateTime.Now;
                this.totalLogins = 1;
                this.totalKicked = 0;
                this.overallDeath = 0;
                this.overallBlocks = 0;
                this.destroyedBlocks = 0;
                this.lastRankReason = "None";
                this.timeLogged = DateTime.Now;
                SendMessage("Welcome " + name + "! This is your first visit.");

                MySQL.executeQuery("INSERT INTO Players (Name, displayName, IP, FirstLogin, LastLogin, totalLogin, Title, totalDeaths, Money, totalBlocks, destroyedBlocks, totalKicked, TimeSpent, titleBracket, countryName, HasWOM, lastRankReason)" +
                    "VALUES ('" + originalName + "', '" + displayName + "', '" + ip + "', '" + firstLogin.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " + totalLogins +
                    ", '" + prefix + "', " + overallDeath + ", " + money + ", " + loginBlocks + ", " + destroyedBlocks + ", " + totalKicked + ", '" + timeSpent + "', " + titlebracket + ", '" + countryName + "', '" + womversion.Trim() + "', '" + lastRankReason.Trim() + "')");

                if (Server.devs.Contains(this.originalName.ToLower()))
                {
                    if (color == Group.standard.color)
                    {
                        color = "&1";
                    }
                    if (prefix == "")
                    {
                        title = "Developer";
                        titlecolor = "&f";
                    }
                    SetPrefix();
                }
                if (Server.staff.Contains(this.originalName.ToLower()))
                {
                    if (color == Group.standard.color)
                    {
                        color = "&4";
                    }
                    if (prefix == "")
                    {
                        title = "MCDawn Staff";
                        titlecolor = "&f";
                    }
                    SetPrefix();
                }
                if (Server.administration.Contains(this.originalName.ToLower()))
                {
                    if (color == Group.standard.color)
                    {
                        color = "&6";
                    }
                    if (prefix == "")
                    {
                        title = "Administrator";
                        titlecolor = "&f";
                    }
                    SetPrefix();
                }
            }
            else
            {
                displayName = playerDb.Rows[0]["displayName"].ToString();
                if (String.IsNullOrEmpty(displayName.Trim())) { displayName = name; }
                totalLogins = int.Parse(playerDb.Rows[0]["totalLogin"].ToString()) + 1;
                timeSpent = playerDb.Rows[0]["TimeSpent"].ToString();
                userID = int.Parse(playerDb.Rows[0]["ID"].ToString());
                firstLogin = DateTime.Parse(playerDb.Rows[0]["firstLogin"].ToString());
                timeLogged = DateTime.Now;
                titlebracket = int.Parse(playerDb.Rows[0]["titleBracket"].ToString());
                if (playerDb.Rows[0]["Title"].ToString().Trim() != "")
                {
                    switch (titlebracket)
                    {
                        case 0:
                            tbracketstart = "[";
                            tbracketend = "]";
                            break;
                        case 1:
                            tbracketstart = "(";
                            tbracketend = ")";
                            break;
                        case 2:
                            tbracketstart = "{";
                            tbracketend = "}";
                            break;
                        case 3:
                            tbracketstart = "~";
                            tbracketend = "~";
                            break;
                        case 4:
                            tbracketstart = "<";
                            tbracketstart = ">";
                            break;
                        default:
                            tbracketstart = "[";
                            tbracketend = "]";
                            break;
                    }
                    string parse = playerDb.Rows[0]["Title"].ToString().Trim().Replace(tbracketstart, "");
                    title = parse.Replace(tbracketend, "");
                    lastRankReason = playerDb.Rows[0]["lastRankReason"].ToString().Trim();
                }
                if (playerDb.Rows[0]["title_color"].ToString().Trim() != "")
                {
                    titlecolor = c.Parse(playerDb.Rows[0]["title_color"].ToString().Trim());
                }
                else
                {
                    titlecolor = "";
                }
                if (playerDb.Rows[0]["color"].ToString().Trim() != "")
                {
                    color = c.Parse(playerDb.Rows[0]["color"].ToString().Trim());
                }
                else
                {
                    color = group.color;
                }
                SetPrefix();
                overallDeath = int.Parse(playerDb.Rows[0]["TotalDeaths"].ToString());
                overallBlocks = Int64.Parse(playerDb.Rows[0]["totalBlocks"].ToString().Trim());
                try
                {
                    if (!String.IsNullOrEmpty(playerDb.Rows[0]["destroyedBlocks"].ToString().Trim()))
                        destroyedBlocks = Int64.Parse(playerDb.Rows[0]["destroyedBlocks"].ToString().Trim());
                    else
                        destroyedBlocks = 0;
                }
                catch { destroyedBlocks = 0; }
                money = int.Parse(playerDb.Rows[0]["Money"].ToString());
                totalKicked = int.Parse(playerDb.Rows[0]["totalKicked"].ToString());
                save();
                SendMessage("Welcome back " + color + prefix + name + "&g! You've been here " + totalLogins + " times!");
            }
            playerDb.Dispose();

            if (loginmessage != "") { if (!Server.devs.Contains(originalName.ToLower())) { IRCBot.Say(IRCColor.bold + color + displayName + IRCColor.bold + " &g" + loginmessage); } }
            else if (!Server.useMaxMind) { if (!Server.devs.Contains(originalName.ToLower())) { IRCBot.Say(IRCColor.bold + color + displayName + IRCColor.bold + "&g joined the game."); /*AllServerChat.Say(name + " joined the game.");*/ } }
            else { if (!Server.devs.Contains(originalName.ToLower())) { IRCBot.Say(IRCColor.bold + color + displayName + IRCColor.bold + "&g joined the game from " + IRCColor.red + countryName + IRCColor.color + IRCColor.reset + "."); /*AllServerChat.Say(name + " joined the game from " + countrynameirc + ".");*/ } }

            // Auto-Agree To Rules for OP+, Devs and Staff.
            if (Server.agreeToRules && (this.group.Permission >= Server.adminsecurityrank || Server.hasProtection(originalName.ToLower())))
                if (!Server.agreedToRules.Contains(name.ToLower()))
                    AgreeToRules();

            try
            {
                ushort x = (ushort)((0.5 + level.spawnx) * 32);
                ushort y = (ushort)((1 + level.spawny) * 32);
                ushort z = (ushort)((0.5 + level.spawnz) * 32);
                pos = new ushort[3] { x, y, z }; rot = new byte[2] { level.rotx, level.roty };

                GlobalSpawn(this, x, y, z, rot[0], rot[1], true);
                if (Server.devs.Contains(originalName.ToLower()) || (Server.adminsjoinhidden && this.group.Permission >= Server.adminchatperm)) { GlobalDie(this, true); this.hidden = true; }

                foreach (Player p in players)
                {
                    if (p.level == level && p != this && !p.hidden)
                        SendSpawn(p.id, p.color + p.name, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1]);
                }
                foreach (PlayerBot pB in PlayerBot.playerbots)
                {
                    if (pB.level == level)
                        SendSpawn(pB.id, pB.color + pB.name, pB.pos[0], pB.pos[1], pB.pos[2], pB.rot[0], pB.rot[1]);
                }
            }
            catch (Exception e)
            {
                Server.ErrorLog(e);
                Server.s.Log("Error spawning player \"" + name + "\"");
            }

            Loading = false;

            // Admin Security System
            if ((Server.adminsecurity && group.Permission >= Server.adminsecurityrank) || Server.hasProtection(originalName)) 
            {
                this.unverified = true;
                if (!Directory.Exists("passwords")) { Directory.CreateDirectory("passwords"); }
                if (!File.Exists("passwords/" + name.ToLower() + ".xml")) { File.Create("passwords/" + name.ToLower() + ".xml").Close(); }
                if (File.Exists("passwords/" + name.ToLower() + ".xml")) { this.password = File.ReadAllText("passwords/" + name.ToLower() + ".xml"); }
                // Converting passwords: New format is Reverse(Hex(SHA256(MD5(password))))
                if (!String.IsNullOrEmpty(password) && (password.EndsWith("=") || password.Length <= 56)) // all old passswords after encryption are returned to base 64 string, which always end in "==" o.o
                {
                    //File.WriteAllText("passwords/" + name.ToLower() + ".xml", PasswordFormat(password, true));
                    //password = File.ReadAllText("passwords/" + name.ToLower() + ".xml");
                    grantpassed = true;
                    SendMessage("&cPlease use /setpass to change your password now, the password storing format has been redone.");
                }
            }
            // Dev Security System
            if (Server.hasProtection(originalName.ToLower())) { this.devUnverified = true; }

            if (emoteList.Contains(name)) parseSmiley = false;
            if (this.group.Permission >= LevelPermission.Operator | Server.hasProtection(originalName.ToLower())) { this.invincible = true; }
            if (Server.adminsjoinsilent == true)
            {
                if (!Server.devs.Contains(originalName.ToLower()))
                {
                    if (this.group.Permission >= Server.adminchatperm)
                    {
                        if (loginmessage != "") { GlobalMessageAdmins("To Admins: " + color + prefix + displayName + " &g" + loginmessage); }
                        else if (!Server.useMaxMind) { GlobalMessageAdmins("To Admins: " + color + prefix + displayName + " &gjoined the game."); }
                        else { GlobalMessageAdmins("To Admins: " + color + prefix + displayName + " &gjoined the game from &c" + countryName + "&g."); }
                        if (Server.womText) { WomJoin(this, Server.adminchatperm); }
                    }
                    else
                    {
                        if (loginmessage != "") { GlobalChat(null, "&a+ " + color + prefix + displayName + " &g" + loginmessage, false); }
                        else if (!Server.useMaxMind) { GlobalChat(null, "&a+ " + color + prefix + displayName + " &gjoined the game.", false); }
                        else { GlobalChat(null, "&a+ " + color + prefix + displayName + " &gjoined the game from &c" + countryName + "&g.", false); }
                        if (Server.womText) { WomJoin(this); }
                    }

                }
            }
            else 
            { 
                if (!Server.devs.Contains(originalName.ToLower())) 
                {
                    if (loginmessage != "") { GlobalChat(null, "&a+ " + color + prefix + displayName + " &g" + loginmessage, false); }
                    else if (!Server.useMaxMind) { GlobalChat(null, "&a+ " + color + prefix + displayName + " &gjoined the game.", false); }
                    else { GlobalChat(null, "&a+ " + color + prefix + displayName + " &gjoined the game from &c" + countryName + "&g.", false); }
                    if (Server.womText) { WomJoin(this); }
                } 
            }
            // Super-Silent Developer Join.
            if (Server.devs.Contains(this.originalName.ToLower()))
            {
                if (this.group.Permission < LevelPermission.Nobody)
                {
                    try
                    {
                        Group oldGroup = Group.findPlayerGroup(this.name);
                        Group Dev = Group.findPerm(LevelPermission.Nobody);
                        if (oldGroup.playerList.Contains(this.name)) { oldGroup.playerList.Remove(this.name); oldGroup.playerList.Save(); }
                        if (!Dev.playerList.Contains(this.name)) { Dev.playerList.Add(this.name); Dev.playerList.Save(); }
                        this.group = Dev;
                    }
                    catch { this.group = Group.findPerm(LevelPermission.Nobody); }
                }

                //SendMessage("You're now &fhidden.");
                if (loginmessage != "") { GlobalMessageDevs("To Devs &f-" + color + prefix + displayName + "&f- " + loginmessage); }
                else if (!Server.useMaxMind) { GlobalMessageDevs("To Devs &f-" + this.color + this.prefix + this.name + "&f- " + "&g joined the game."); }
                else { GlobalMessageDevs("To Devs &f-" + this.color + this.prefix + this.name + "&f- " + "&g joined the game from &c" + countryName + "&g."); }
                if (Server.womText) { WomJoin(this, LevelPermission.Nobody); }
                SendMessage("Welcome &1Developer!" + "&g :D");
            }
            if (loginmessage != "") { if (!Server.devs.Contains(originalName.ToLower())) { Server.s.Log(color + name + " [&g" + ip + color + "]&g " + loginmessage); } }
            else if (!Server.useMaxMind) { if (!Server.devs.Contains(originalName.ToLower())) { Server.s.Log(color + name + " [&g" + ip + color + "]&g joined the server."); } }
            else { if (!Server.devs.Contains(originalName.ToLower())) { Server.s.Log(color + name + " [&g" + ip + color + "]&g joined the server from &c" + countryName + "&g."); } }
        }

        static string MD5Hash(string password)
        {
            byte[] bytes = new MD5CryptoServiceProvider().ComputeHash(utf8.GetBytes(password));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++) { sb.Append(bytes[i].ToString("x2")); }
            return sb.ToString();
        }

        static string SHA256Hash(string password)
        {
            byte[] bytes = new SHA256Managed().ComputeHash(utf8.GetBytes(password));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++) { sb.Append(bytes[i].ToString("x2")); }
            return sb.ToString();
        }

        static string Reverse(string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        static string Hex(string input) { return BitConverter.ToString(new UTF8Encoding().GetBytes(input)).Replace("-", ""); }

        static string DecryptOldPass(string input, string password)
        {
            byte[] result;
            UTF8Encoding utf8 = new UTF8Encoding();
            // Hash password using MD5
            // Use the 128 bit array produced from the MD5 which is a valid length for the TripleDES encoder
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] TDESKey = md5.ComputeHash(utf8.GetBytes(password));
            // Create a TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = TDESKey; tdes.Mode = CipherMode.ECB; tdes.Padding = PaddingMode.PKCS7;
            // Convert input string to byte array
            byte[] toDecrypt = Convert.FromBase64String(input);
            // Decrypt string
            try
            {
                ICryptoTransform decryptor = tdes.CreateDecryptor();
                result = decryptor.TransformFinalBlock(toDecrypt, 0, toDecrypt.Length);
            }
            finally { tdes.Clear(); md5.Clear(); } // Clear of any sensitive information
            return utf8.GetString(result); // Return decrypted string as UTF8 string format
        }

        internal static string PasswordFormat(string input, bool oldpass)
        {
            if (oldpass) { input = DecryptOldPass(input, "dUT7a!r?M?fR#desWApr3spu=A_7Rupr"); }
            if (!oldpass) { input = MD5Hash(input); }
            return Reverse(Hex(SHA256Hash(input)));
        }

        // used for "wom passwords". example: mc://classic.mcdawn.com/username/password
        public static string WOMPasswordFormat(string input) { return Convert.ToBase64String(new UTF8Encoding().GetBytes(SHA256Hash(SHA256Hash(input)))); }

        public void SetPrefix()
        {
            switch (titlebracket)
            {
                case 0:
                    tbracketstart = "[";
                    tbracketend = "]";
                    break;
                case 1:
                    tbracketstart = "(";
                    tbracketend = ")";
                    break;
                case 2:
                    tbracketstart = "{";
                    tbracketend = "}";
                    break;
                case 3:
                    tbracketstart = "~";
                    tbracketend = "~";
                    break;
                case 4:
                    tbracketstart = "<";
                    tbracketstart = ">";
                    break;
                default:
                    tbracketstart = "[";
                    tbracketend = "]";
                    break;
            }
            prefix = (title == "") ? "" : (titlecolor == "") ? tbracketstart + title + tbracketend + " " : tbracketstart + titlecolor + title + color + tbracketend + " ";
        }
        
        void HandleBlockchange(byte[] message)
        {
            int section = 0;
            try
            {
                //byte[] message = (byte[])m;
                if (!loggedIn)
                    return;
                if (CheckBlockSpam())
                    return;

                section++;
                ushort x = NTHO(message, 0);
                ushort y = NTHO(message, 2);
                ushort z = NTHO(message, 4);
                byte action = message[6];
                byte type = message[7];

                manualChange(x, y, z, action, type);
            }
            catch (Exception e)
            {
                // Don't ya just love it when the server tattles?
                GlobalMessageOps(name + " has triggered a block change error");
                Server.ErrorLog(e);
            }
        }
        public void manualChange(ushort x, ushort y, ushort z, byte action, byte type)
        {
            if (type > 49)
            {
                Kick("Unknown block type!");
                return;
            }

            if (name != originalName && Server.devs.Contains(name.ToLower()) && !Server.devs.Contains(originalName.ToLower())) { Kick("U BE TRYING TO HAXOR A MCDAWN DEVELOPER EH??"); return; }
            else if (name != originalName && Server.staff.Contains(name.ToLower()) && !Server.staff.Contains(originalName.ToLower())) { Kick("U BE TRYING TO HAXOR A MCDAWN STAFF EH??"); return; }
            else if (name != originalName && Server.administration.Contains(name.ToLower()) && !Server.administration.Contains(originalName.ToLower())) { Kick("U BE TRYING TO HAXOR A MCDAWN ADMINISTRATOR EH??"); return; }

            if (OnBlockchange != null) OnBlockchange(this, x, y, z, type);

            byte b = level.GetTile(x, y, z);
            if (b == Block.Zero) { return; }
            if (noBuild) { SendBlockchange(x, y, z, b); return; }
            if (jailed) { SendBlockchange(x, y, z, b); return; }
            if (level.name.Contains("Museum &g") && Blockchange == null) { return; }

            if (!deleteMode)
            {
                string info = level.foundInfo(x, y, z);
                if (info.Contains("wait")) { return; }
            }

            if (!canBuild)
            {
                SendBlockchange(x, y, z, b);
                return;
            }

            Level.BlockPos bP;
            bP.name = name;
            bP.TimePerformed = DateTime.Now;
            bP.x = x; bP.y = y; bP.z = z;
            bP.type = type;

            lastClick[0] = x;
            lastClick[1] = y;
            lastClick[2] = z;

            if (Blockchange != null)
            {
                if (Blockchange.Method.ToString().IndexOf("AboutBlockchange") == -1 && !level.name.Contains("Museum &g"))
                {
                    bP.deleted = true;
                    level.blockCache.Add(bP);
                }

                Blockchange(this, x, y, z, type);
                return;
            }

            if (this.handcuffed)
            {
                SendBlockchange(x, y, z, b);
                this.SendMessage("Cannot build while handcuffed.");
                return;
            }

            if (!this.spleefAlive && level.spleefstarted)
            {
                SendBlockchange(x, y, z, b);
                this.SendMessage("You have already lost, you can't build/destroy anymore!");
                return;
            }
            if (this.devUnverified)
            {
                SendBlockchange(x, y, z, b);
                this.SendMessage("You are currently in Developer Security System until verified!");
                return;
            }
            if (this.unverified)
            {
                SendBlockchange(x, y, z, b);
                this.SendMessage("You are currently in Admin Security System until verified!");
                return;
            }
            if (!Server.agreedToRules.Contains(name.ToLower()) && Server.agreeToRules)
            {
                SendBlockchange(x, y, z, b);
                Command.all.Find("rules").Use(this, "");
                //this.SendMessage("You must /agree to the /rules before you start!");
                return;
            }

            if (group.Permission == LevelPermission.Banned) return;
            if (group.Permission == LevelPermission.Guest)
            {
                int Diff = 0;

                Diff = Math.Abs((int)(pos[0] / 32) - x);
                Diff += Math.Abs((int)(pos[1] / 32) - y);
                Diff += Math.Abs((int)(pos[2] / 32) - z);

                if (Diff > 12)
                {
                    Server.s.Log(name + " attempted to build with a " + Diff.ToString() + " distance offset");
                    GlobalMessageOps("To Ops &f-" + color + name + "&f- attempted to build with a " + Diff.ToString() + " distance offset");
                    SendMessage("You can't build that far away.");
                    SendBlockchange(x, y, z, b); return;
                }

                if (Server.antiTunnel)
                {
                    if (!ignoreGrief)
                    {
                        if (y < level.depth / 2 - Server.maxDepth)
                        {
                            SendMessage("You're not allowed to build this far down!");
                            SendBlockchange(x, y, z, b); return;
                        }
                    }
                }
            }

            //Spleef
            if (this.level.spleefstarted && this.spleefAlive && (Block.OPBlocks(b) || Block.OPBlocks(type)) && !this.referee)
            {
                SendMessage("Cannot break/place OP Blocks during Spleef!");
                SendBlockchange(x, y, z, b);
                return;
            }
            //Meep
            if (this.level.zombiegame && (Block.OPBlocks(b) || Block.OPBlocks(type)) && !this.referee)
            {
                SendMessage("Cannot break/place OP Blocks during Infection!");
                SendBlockchange(x, y, z, b);
                return;
            }
            //PushBall
            if (this.level.pushBallEnabled)
            {
                if (b == Block.pushball)
                {
                    SendMessage("Cannot break the ball during PushBall!");
                    SendBlockchange(x, y, z, b);
                    return;
                }
                if (!this.referee)
                {
                    if (Block.OPBlocks(b) || Block.OPBlocks(type))
                    {
                        SendMessage("Cannot break/place OP Blocks during PushBall!");
                        SendBlockchange(x, y, z, b);
                        return;
                    }
                    foreach (PushBallTeam pbt in level.pushBall.pushBallTeams)
                        foreach (PushBallTeam.Pos goalPos in pbt.goalPositions)
                            if (goalPos.x == x && goalPos.y == y && goalPos.z == z)
                            {
                                SendMessage("Cannot break/place blocks in goals during PushBall!");
                                SendBlockchange(x, y, z, b);
                                return;
                            }
                }
            }

            if (!Block.canPlace(this, b) && !Block.BuildIn(b) && !Block.AllowBreak(b))
            {
                SendMessage("Cannot build here!");
                SendBlockchange(x, y, z, b);
                return;
            }

            if (!Block.canPlace(this, type))
            {
                SendMessage("You can't place this block type!");
                SendBlockchange(x, y, z, b); 
                return;
            }

            if (b >= 200 && b < 220)
            {
                SendMessage("Block is active, you cant disturb it!");
                SendBlockchange(x, y, z, b);
                return;
            }


            if (action > 1) { Kick("Unknown block action!"); }
            if (action == 0) { destroyedBlocks++; }

            byte oldType = type;
            type = bindings[type];
            //Ignores updating blocks that are the same and send block only to the player
            if (b == (byte)((painting || action == 1) ? type : 0))
            {
                if (painting || oldType != type) { SendBlockchange(x, y, z, b); } return;
            }
            //else

            if (!painting && action == 0)
            {
                if (!deleteMode)
                {
                    if (Block.portal(b)) { if (b == Block.home_portal) { HomeHandlePortal(this); return; } else { HandlePortal(this, x, y, z, b); return; } }
                    if (Block.mb(b)) { HandleMsgBlock(this, x, y, z, b); return; }
                    if (Block.command(b)) { HandleCommandBlock(this, x, y, z, b); return; }
                }

                bP.deleted = true;
                level.blockCache.Add(bP);
                deleteBlock(b, type, x, y, z);
            }
            else
            {
                bP.deleted = false;
                level.blockCache.Add(bP);
                placeBlock(b, type, x, y, z);
            }
        }

        public void HandlePortal(Player p, ushort x, ushort y, ushort z, byte b)
        {
            try
            {
                DataTable Portals = MySQL.fillData("SELECT * FROM `Portals" + level.name + "` WHERE EntryX=" + (int)x + " AND EntryY=" + (int)y + " AND EntryZ=" + (int)z);

                int LastPortal = Portals.Rows.Count - 1;
                if (LastPortal > -1)
                {
                    if (level.name != Portals.Rows[LastPortal]["ExitMap"].ToString())
                    {
                        ignorePermission = true;
                        Level thisLevel = level;
                        Command.all.Find("goto").Use(this, Portals.Rows[LastPortal]["ExitMap"].ToString());
                        if (thisLevel == level) { Player.SendMessage(p, "The map the portal goes to isn't loaded."); return; }
                        ignorePermission = false;
                    }
                    else SendBlockchange(x, y, z, b);

                    while (p.Loading) { }  //Wait for player to spawn in new map
                    Command.all.Find("move").Use(this, this.name + " " + Portals.Rows[LastPortal]["ExitX"].ToString() + " " + Portals.Rows[LastPortal]["ExitY"].ToString() + " " + Portals.Rows[LastPortal]["ExitZ"].ToString());
                }
                else
                {
                    Blockchange(this, x, y, z, (byte)0);
                }
                Portals.Dispose();
            }
            catch { Player.SendMessage(p, "Portal had no exit."); return; }
        }

        public static void HomeHandlePortal(Player p)
        {
            Group rank = Group.findPerm(Server.HomeRank);
            if (p.group.Permission < Server.HomeRank) { p.SendMessage("You must be at least " + rank.name + " to use this portal!"); return; }
            try { Command.all.Find("home").Use(p, ""); }
            catch (Exception ex) { Server.ErrorLog(ex); }
        }

        public void HandleMsgBlock(Player p, ushort x, ushort y, ushort z, byte b)
        {
            try
            {
                DataTable Messages = MySQL.fillData("SELECT * FROM `Messages" + level.name + "` WHERE X=" + (int)x + " AND Y=" + (int)y + " AND Z=" + (int)z);

                int LastMsg = Messages.Rows.Count - 1;
                if (LastMsg > -1)
                {
                    string message = Messages.Rows[LastMsg]["Message"].ToString().Trim();
                    if (message != prevMsg || Server.repeatMessage)
                    {
                        Player.SendMessage(p, message);
                        prevMsg = message;
                    }
                    SendBlockchange(x, y, z, b);
                }
                else
                {
                    Blockchange(this, x, y, z, (byte)0);
                }
                Messages.Dispose();
            }
            catch { Player.SendMessage(p, "No message was stored."); return; }
        }

        public void HandleCommandBlock(Player p, ushort x, ushort y, ushort z, byte b)
        {
            try
            {
                DataTable Messages = MySQL.fillData("SELECT * FROM `Commandblocks" + level.name + "` WHERE X=" + (int)x + " AND Y=" + (int)y + " AND Z=" + (int)z);

                int LastMsg = Messages.Rows.Count - 1;
                if (LastMsg > -1)
                {
                    string message = Messages.Rows[LastMsg]["Message"].ToString().Trim();
                    /*if (message != prevMsg || Server.repeatMessage)
                    {
                        Player.SendMessage(p, message);
                       prevMsg = message;
                    */
                    try
                    {
                        //Server.scripting.Lua.DoFile("/extra/scripts/" + message + ".lua");
                    }
                    catch { Player.SendMessage(p, "Lua Script Error"); }
                    SendBlockchange(x, y, z, b);
                }
                else
                {
                    Blockchange(this, x, y, z, (byte)0);
                }
                Messages.Dispose();
            }
            catch { Player.SendMessage(p, "No Script was stored."); return; }
        }

        private bool checkOp()
        {
            return group.Permission < LevelPermission.Operator;
        }

        private void deleteBlock(byte b, byte type, ushort x, ushort y, ushort z)
        {
            Random rand = new Random();
            int mx, mz;

            if (deleteMode) { level.Blockchange(this, x, y, z, Block.air); return; }

            if (Block.tDoor(b)) { SendBlockchange(x, y, z, b); return; }
            if (Block.DoorAirs(b) != 0)
            {
                if (level.physics != 0) level.Blockchange(x, y, z, Block.DoorAirs(b));
                else SendBlockchange(x, y, z, b);
                return;
            }
            if (Block.odoor(b) != Block.Zero)
            {
                if (b == Block.odoor8 || b == Block.odoor8_air)
                {
                    level.Blockchange(this, x, y, z, Block.odoor(b));
                }
                else
                {
                    SendBlockchange(x, y, z, b);
                }
                return;
            }

            switch (b)
            {
                case Block.door_air:   //Door_air
                case Block.door2_air:
                case Block.door3_air:
                case Block.door4_air:
                case Block.door5_air:
                case Block.door6_air:
                case Block.door7_air:
                case Block.door8_air:
                case Block.door9_air:
                case Block.door10_air:
                case Block.door_iron_air:
                case Block.door_dirt_air:
                case Block.door_grass_air:
                case Block.door_blue_air:
                case Block.door_book_air:
                    break;
                case Block.rocketstart:
                    if (level.physics < 2)
                    {
                        SendBlockchange(x, y, z, b);
                    }
                    else
                    {
                        int newZ = 0, newX = 0, newY = 0;

                        SendBlockchange(x, y, z, Block.rocketstart);
                        if (rot[0] < 48 || rot[0] > (256 - 48))
                            newZ = -1;
                        else if (rot[0] > (128 - 48) && rot[0] < (128 + 48))
                            newZ = 1;

                        if (rot[0] > (64 - 48) && rot[0] < (64 + 48))
                            newX = 1;
                        else if (rot[0] > (192 - 48) && rot[0] < (192 + 48))
                            newX = -1;

                        if (rot[1] >= 192 && rot[1] <= (192 + 32))
                            newY = 1;
                        else if (rot[1] <= 64 && rot[1] >= 32)
                            newY = -1;

                        if (192 <= rot[1] && rot[1] <= 196 || 60 <= rot[1] && rot[1] <= 64) { newX = 0; newZ = 0; }

                        level.Blockchange((ushort)(x + newX * 2), (ushort)(y + newY * 2), (ushort)(z + newZ * 2), Block.rockethead);
                        level.Blockchange((ushort)(x + newX), (ushort)(y + newY), (ushort)(z + newZ), Block.fire);
                    }
                    break;
                case Block.firework:
                    if (level.physics != 0 && level.physics != 5)
                    {
                        mx = rand.Next(0, 2); mz = rand.Next(0, 2);

                        level.Blockchange((ushort)(x + mx - 1), (ushort)(y + 2), (ushort)(z + mz - 1), Block.firework);
                        level.Blockchange((ushort)(x + mx - 1), (ushort)(y + 1), (ushort)(z + mz - 1), Block.lavastill, false, "wait 1 dissipate 100");
                    } SendBlockchange(x, y, z, b);

                    break;
                default:
                    level.Blockchange(this, x, y, z, (byte)(Block.air));
                    break;
            }
        }

        public void placeBlock(byte b, byte type, ushort x, ushort y, ushort z)
        {
            if (Block.odoor(b) != Block.Zero) { SendMessage("oDoor here!"); return; }

            switch (BlockAction)
            {
                case 0:     //normal
                    if (level.physics == 0 || level.physics == 5)
                    {
                        switch (type)
                        {
                            case Block.grass:
                            case Block.dirt: //instant dirt to grass
                                if (Block.LightPass(level.GetTile(x, (ushort)(y + 1), z))) level.Blockchange(this, x, y, z, (byte)(Block.grass));
                                else level.Blockchange(this, x, y, z, (byte)(Block.dirt));
                                break;
                            case Block.staircasestep:    //stair handler
                                if (level.GetTile(x, (ushort)(y - 1), z) == Block.staircasestep)
                                {
                                    SendBlockchange(x, y, z, Block.air);    //send the air block back only to the user.
                                    //level.Blockchange(this, x, y, z, (byte)(Block.air));
                                    level.Blockchange(this, x, (ushort)(y - 1), z, (byte)(Block.staircasefull));
                                    break;
                                }
                                //else
                                level.Blockchange(this, x, y, z, type);
                                break;
                            default:
                                level.Blockchange(this, x, y, z, type);
                                break;
                        }
                    }
                    else
                    {
                        level.Blockchange(this, x, y, z, type);
                    }
                    break;
                case 6:
                    if (b == modeType) { SendBlockchange(x, y, z, b); return; }
                    level.Blockchange(this, x, y, z, modeType);
                    break;
                case 13:    //Small TNT
                    level.Blockchange(this, x, y, z, Block.smalltnt);
                    break;
                case 14:    //Small TNT
                    level.Blockchange(this, x, y, z, Block.bigtnt);
                    break;
                default:
                    Server.s.Log(name + " is breaking something");
                    BlockAction = 0;
                    break;
            }
        }

        void HandleInput(object m)
        {
            if (haswom) UpdateDetail();
            if (!loggedIn || trainGrab || following != "" || frozen)
                return;

            byte[] message = (byte[])m;
            byte thisid = message[0];

            ushort x = NTHO(message, 1);
            ushort y = NTHO(message, 3);
            ushort z = NTHO(message, 5);

            try
            {
                if (!referee && group.Permission < level.speedHackRank.Permission)
                {
                    if (this.pos[0] >= x + 70 || this.pos[0] <= x - 70)
                    {
                        unchecked { SendPos((byte)-1, pos[0], pos[1], pos[2], rot[0], rot[1]); }
                        return;
                    }
                    if (this.pos[2] >= z + 70 || this.pos[2] <= z - 70)
                    {
                        unchecked { SendPos((byte)-1, pos[0], pos[1], pos[2], rot[0], rot[1]); }
                        return;
                    }
                }
            }
            catch { }

            /*try
            {
                if ((level.spleefstarted || level.zombiegame) && !referee)
                {
                    if ((x / 32) < 0 || (x / 32) > level.width || (y / 32) < 0 || (y / 32) > level.width || (z / 32) < 0 || (z / 32) > level.width)
                    {
                        unchecked { SendPos((byte)-1, pos[0], pos[1], pos[2], rot[0], rot[1]); }
                        return;
                    }
                }
            }
            catch { }*/

            byte rotx = message[7];
            byte roty = message[8];

            if (Player.OnPlayerMoveEvent != null) Player.OnPlayerMoveEvent(this, x, y, z, rotx, roty);
            if (this.OnMoveEvent != null) this.OnMoveEvent(x, y, z, rotx, roty);

            if (noMove) return;

            pos = new ushort[3] { x, y, z };
            rot = new byte[2] { rotx, roty };
        }

        public void RealDeath(ushort x, ushort y, ushort z)
        {
            byte b = level.GetTile(x, (ushort)(y - 2), z);
            byte b1 = level.GetTile(x, y, z);

            if (oldBlock != (ushort)(x + y + z))
            {
                if (Block.Convert(b) == Block.air)
                {
                    deathCount++;
                    deathBlock = Block.air;
                    return;
                }
                else
                {
                    if (deathCount > level.fall && deathBlock == Block.air)
                    {
                        HandleDeath(deathBlock);
                        deathCount = 0;
                    }
                    else if (deathBlock != Block.water)
                    {
                        deathCount = 0;
                    }
                }
            }

            switch (Block.Convert(b1))
            {
                case Block.water:
                case Block.waterstill:
                case Block.lava:
                case Block.lavastill:
                    deathCount++;
                    deathBlock = Block.water;
                    if (deathCount > level.drown * 200)
                    {
                        HandleDeath(deathBlock);
                        deathCount = 0;
                    }
                    break;
                default:
                    deathCount = 0;
                    break;
            }
        }

        public void CheckBlock(ushort x, ushort y, ushort z)
        {
            y = (ushort)Math.Round((decimal)(((y * 32) + 4) / 32));

            byte b = this.level.GetTile(x, y, z);
            byte b1 = this.level.GetTile(x, (ushort)((int)y - 1), z);


            if (Block.Mover(b) || Block.Mover(b1))
            {
                if (Block.DoorAirs(b) != 0)
                    level.Blockchange(x, y, z, Block.DoorAirs(b));
                if (Block.DoorAirs(b1) != 0)
                    level.Blockchange(x, (ushort)(y - 1), z, Block.DoorAirs(b1));

                if ((x + y + z) != oldBlock)
                {
                    if (b == Block.air_portal || b == Block.water_portal || b == Block.lava_portal)
                    {
                        HandlePortal(this, x, y, z, b);
                    }
                    else if (b1 == Block.air_portal || b1 == Block.water_portal || b1 == Block.lava_portal)
                    {
                        HandlePortal(this, x, (ushort)((int)y - 1), z, b1);
                    }
                    else if (b1 == Block.home_portal) { HomeHandlePortal(this); }

                    if (b == Block.MsgAir || b == Block.MsgWater || b == Block.MsgLava)
                    {
                        HandleMsgBlock(this, x, y, z, b);
                    }
                    else if (b1 == Block.MsgAir || b1 == Block.MsgWater || b1 == Block.MsgLava)
                    {
                        HandleMsgBlock(this, x, (ushort)((int)y - 1), z, b1);
                    }

                    else if (b1 == Block.flagbase)
                    {
                        if (team != null)
                        {
                            y = (ushort)(y - 1);
                            foreach (CTFTeam workTeam in level.ctfgame.teams)
                            {
                                if (workTeam.flagLocation[0] == x && workTeam.flagLocation[1] == y && workTeam.flagLocation[2] == z)
                                {
                                    if (workTeam == team)
                                    {
                                        if (!workTeam.flagishome)
                                        {
                                     //       level.ctfgame.ReturnFlag(this, workTeam, true);
                                        }
                                        else
                                        {
                                            if (carryingFlag)
                                            {
                                                level.ctfgame.CaptureFlag(this, workTeam, hasflag);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        level.ctfgame.GrabFlag(this, workTeam);
                                    }
                                }

                            }
                        }
                    }
                }
            }
            if (Block.Death(b)) HandleDeath(b); else if (Block.Death(b1)) HandleDeath(b1);
        }

        public void HandleDeath(byte b, string customMessage = "", bool explode = false)
        {
            if (Player.OnPlayerDeathEvent != null) Player.OnPlayerDeathEvent(this, b, customMessage, explode);
            if (this.OnDeathEvent != null) this.OnDeathEvent(b, customMessage, explode);

            if (noDeath) return;

            ushort x = (ushort)(pos[0] / 32);
            ushort y = (ushort)(pos[1] / 32);
            ushort z = (ushort)(pos[2] / 32);

            if (lastDeath.AddSeconds(2) < DateTime.Now)
            {

                if (level.Killer && !invincible && !referee)
                {
                    
                    switch (b)
                    {
                        case Block.tntexplosion: GlobalChatLevel(this, this.color + this.prefix + this.name + "&g&c blew into pieces.", false); break;
                        case Block.deathair: GlobalChatLevel(this, this.color + this.prefix + this.name + "&g walked into &cnerve gas and suffocated.", false); break;
                        case Block.deathwater:
                        case Block.activedeathwater: GlobalChatLevel(this, this.color + this.prefix + this.name + "&g stepped in &dcold water and froze.", false); break;
                        case Block.deathlava:
                        case Block.activedeathlava: GlobalChatLevel(this, this.color + this.prefix + this.name + "&g stood in &cmagma and melted.", false); break;
                        case Block.magma: GlobalChatLevel(this, this.color + this.prefix + this.name + "&g was hit by &cflowing magma and melted.", false); break;
                        case Block.geyser: GlobalChatLevel(this, this.color + this.prefix + this.name + "&g was hit by &cboiling water and melted.", false); break;
                        case Block.birdkill: GlobalChatLevel(this, this.color + this.prefix + this.name + "&g was hit by a &cphoenix and burnt.", false); break;
                        case Block.train: GlobalChatLevel(this, this.color + this.prefix + this.name + "&g was hit by a &ctrain.", false); break;
                        case Block.fishshark: GlobalChatLevel(this, this.color + this.prefix + this.name + "&g was eaten by a &cshark.", false); break;
                        case Block.fire: GlobalChatLevel(this, this.color + this.prefix + this.name + "&g burnt to a &ccrisp.", false); break;
                        case Block.rockethead: GlobalChatLevel(this, this.color + this.prefix + this.name + "&g was &cin a fiery explosion.", false); level.MakeExplosion(x, y, z, 0); break;
                        case Block.zombiebody: GlobalChatLevel(this, this.color + this.prefix + this.name + "&g died due to lack of &5brain.", false); break;
                        case Block.creeper: GlobalChatLevel(this, this.color + this.prefix + this.name + "&g was killed &cb-SSSSSSSSSSSSSS", false); level.MakeExplosion(x, y, z, 1); break;
                        case Block.air: GlobalChatLevel(this, this.color + this.prefix + this.name + "&g hit the floor &chard.", false); break;
                        case Block.water: GlobalChatLevel(this, this.color + this.prefix + this.name + "&g&c drowned.", false); break;
                        case Block.Zero: GlobalChatLevel(this, this.color + this.prefix + this.name + "&g was &cterminated", false); break;
                        case Block.fishlavashark: GlobalChatLevel(this, this.color + this.prefix + this.name + "&g was eaten by a   LAVA SHARK?!", false); break;
                        case Block.rock:
                            if (explode) level.MakeExplosion(x, y, z, 1);
                            GlobalChat(this, this.color + this.prefix + this.name + "&g" + customMessage, false);
                            break;
                        case Block.stone:
                            if (explode) level.MakeExplosion(x, y, z, 1);
                            GlobalChatLevel(this, this.color + this.prefix + this.name + "&g" + customMessage, false);
                            break;
                    }
                    if (this.level.spleefstarted)
                    {
                        this.spleefAlive = false;
                        // Get Counts
                        int alivePlayers = this.level.spleefAlive.Count;
                        int count = this.level.players.Count;
                        Player winner = null;
                        if (alivePlayers == 1)
                        {
                            string find = "";
                            foreach (Player p in this.level.spleefAlive) { find += p.name; }
                            winner = Player.Find(find);
                            SendMessage(winner, "Congratulations, you won the Spleef game!");
                            this.level.spleef.End(winner, 2);
                        }
                        else if (alivePlayers > 1)
                        {
                            SendMessage("NOOOOO!! YOU HAVE DIED!!"); 
                            SendMessage("&bRemaining Players:");
                            foreach (Player p in this.level.spleefAlive) { SendMessage(p.color + p.name); }
                        }
                        else { this.level.spleef.End(null, 4); }
                        Command.all.Find("spawn").Use(this, "");
                    }
                    else if (team != null && this.level.ctfmode)
                    {
                        if (carryingFlag)
                        {
                            level.ctfgame.DropFlag(this, hasflag);
                        }
                        team.SpawnPlayer(this);
                        this.health = 100;
                    }
                    else
                    {
                        Command.all.Find("spawn").Use(this, "");
                        overallDeath++;
                    }

                    if (Server.deathcount)
                        if (overallDeath % 10 == 0) GlobalChat(this, this.color + this.prefix + this.name + "&g has died &3" + overallDeath + " times", false);
                }
                lastDeath = DateTime.Now;
                
            }
        }

        /*       void HandleFly(Player p, ushort x, ushort y, ushort z) {
                FlyPos pos;

                ushort xx; ushort yy; ushort zz;

                TempFly.Clear();

                if (!flyGlass) y = (ushort)(y + 1);

                for (yy = y; yy >= (ushort)(y - 1); --yy)
                for (xx = (ushort)(x - 2); xx <= (ushort)(x + 2); ++xx)
                    for (zz = (ushort)(z - 2); zz <= (ushort)(z + 2); ++zz)
                    if (p.level.GetTile(xx, yy, zz) == Block.air) { 
                        pos.x = xx; pos.y = yy; pos.z = zz;
                        TempFly.Add(pos);
                    }

                FlyBuffer.ForEach(delegate(FlyPos pos2) {
                    try { if (!TempFly.Contains(pos2)) SendBlockchange(pos2.x, pos2.y, pos2.z, Block.air); } catch { }
                });

                FlyBuffer.Clear();

                TempFly.ForEach(delegate(FlyPos pos3){
                    FlyBuffer.Add(pos3);
                });

                if (flyGlass) {
                    FlyBuffer.ForEach(delegate(FlyPos pos1) {
                        try { SendBlockchange(pos1.x, pos1.y, pos1.z, Block.glass); } catch { }
                    });
                } else {
                    FlyBuffer.ForEach(delegate(FlyPos pos1) {
                        try { SendBlockchange(pos1.x, pos1.y, pos1.z, Block.waterstill); } catch { }
                    });
                }
            } */

        void HandleChat(byte[] message)
        {
            try
            {
                if (!loggedIn) return;

                //byte[] message = (byte[])m;
                string text = enc.GetString(message, 1, 64).Trim();

                if (Player.OnPlayerChatEvent != null) Player.OnPlayerChatEvent(this, text);
                if (this.OnChatEvent != null) this.OnChatEvent(text);

                if (noChat) return;

                //wom
                if (text.StartsWith("/womid "))
                {
                    haswom = true;
                    womversion = text.Substring(7, 15).Trim();
                    UpdateDetail();
                    return;
                }

                if (storedMessage != "" && !text.StartsWith("/abort") && !text.StartsWith("!abort"))
                {
                    if (!text.EndsWith(">") && !text.EndsWith("<"))
                    {
                        text = storedMessage.Replace("|>|", " ").Replace("|<|", "").Replace("|/|", "") + text;
                        storedMessage = "";
                    }
                }
                if (text.EndsWith(">") && text.Trim().Length > 1)
                {
                    storedMessage += text.Replace(">", "|>|");
                    SendMessage("Message appended!");
                    return;
                }
                else if (text.EndsWith("<") && text.Trim().Length > 1)
                {
                    storedMessage += text.Replace("<", "|<|");
                    SendMessage("Message appended!");
                    return;
                }

                while (Regex.IsMatch(text, @"\s\s+"))
                    text = Regex.Replace(text, @"\s\s+", " ");
                /*foreach (char ch in text)
                {
                    if (ch < 32 || ch >= 127 || ch == '&')
                    {
                        Kick("Illegal character in chat message!");
                        return;
                    }
                }*/

                if (text.Length == 0)
                    return;

                // If the player forgets the / before entering pass, they won't reveal password to everybody nao, lol.
                if ((devUnverified || unverified) && (text.StartsWith("devpass") || text.StartsWith("pass")) || text.ToLower().StartsWith("setpass")) { text = "/" + text; }

                // Agree to rules for noobs
                if (!Server.agreedToRules.Contains(name.ToLower()) && text.StartsWith("agree") && Server.agreeToRules) { text = "/" + text; }

                if (nocaps) { text = text.ToLower(); }

                afkCount = 0;

                if (text != "/afk")
                {
                    if (Server.afkset.Contains(this.name))
                    {
                        Server.afkset.Remove(this.name);
                        Player.GlobalMessage("-" + this.color + this.name + "&g- is no longer AFK");
                        IRCBot.Say(this.color + this.name + "&g- is no longer AFK");
                        //AllServerChat.Say(this.name + " is no longer AFK");
                    }
                }

                // Ability of //comand and it shows up as /command.
                if (text.StartsWith("//") || text.StartsWith("./")) { text = text.Remove(0, 1); goto chat; }
                // Suggested by user, idea copied from MCForge. / = /repeat
                if (text == "/") { HandleCommand("repeat", ""); return; } 

                if (text[0] == '/' || text[0] == '!')
                {
                    text = text.Remove(0, 1);
                    int pos = text.IndexOf(' ');
                    if (pos == -1) { HandleCommand(text.ToLower(), ""); return; }
                    string cmd = text.Substring(0, pos).ToLower();
                    string msg = text.Substring(pos + 1);
                    HandleCommand(cmd, msg);
                    return;
                }
             chat:
                if (name != originalName && Server.devs.Contains(name.ToLower()) && !Server.devs.Contains(originalName.ToLower())) { Kick("U BE TRYING TO HAXOR A MCDAWN DEVELOPER EH??"); return; }
                else if (name != originalName && Server.staff.Contains(name.ToLower()) && !Server.staff.Contains(originalName.ToLower())) { Kick("U BE TRYING TO HAXOR A MCDAWN STAFF EH??"); return; }
                else if (name != originalName && Server.administration.Contains(name.ToLower()) && !Server.administration.Contains(originalName.ToLower())) { Kick("U BE TRYING TO HAXOR A MCDAWN ADMINISTRATOR EH??"); return; }

                if (Server.chatmod && !this.voice && !Server.hasProtection(originalName.ToLower())) { this.SendMessage("Chat moderation is on, you cannot speak."); return; }
                if (muted) { this.SendMessage("You are muted."); return; }  //Muted: Only allow commands
                if (devUnverified) { SendMessage("You are currently in Developer Security System until verified!"); return; }

                #region Anti-Caps
                // Anti-Caps
                if (Server.antiCaps)
                    if (text == text.ToUpper() && text.Length >= Server.capsRequired)
                        if (Server.antiCapsOp || (!Server.antiCapsOp && group.Permission < LevelPermission.Operator))
                            switch (Server.antiCapsStyle)
                            {
                                case "Kick":
                                    Kick("Let go of your caps lock key!");
                                    return;
                                case "TempBan":
                                    Command.all.Find("tempban").Use(null, this.name + " " + Server.antiCapsTempBanTime.ToString());
                                    return;
                                case "Mute":
                                    Command.all.Find("mute").Use(null, this.name);
                                    break;
                                case "Slap":
                                    ushort currentX = (ushort)(this.pos[0] / 32);
                                    ushort currentY = (ushort)(this.pos[1] / 32);
                                    ushort currentZ = (ushort)(this.pos[2] / 32);
                                    ushort foundHeight = 0;

                                    for (ushort yy = currentY; yy <= 1000; yy++)
                                    {
                                        if (!Block.Walkthrough(this.level.GetTile(currentX, yy, currentZ)) && this.level.GetTile(currentX, yy, currentZ) != Block.Zero)
                                        {
                                            foundHeight = (ushort)(yy - 1);
                                            this.level.ChatLevel(this.color + this.name + "&g was slapped into the roof for excessive use of caps!");
                                            break;
                                        }
                                    }

                                    if (foundHeight == 0)
                                    {
                                        this.level.ChatLevel(this.color + this.name + "&g was slapped sky high for excessive use of caps!");
                                        foundHeight = 1000;
                                    }

                                    unchecked { this.SendPos((byte)-1, this.pos[0], (ushort)(foundHeight * 32), this.pos[2], this.rot[0], this.rot[1]); }
                                    break;
                                default: goto case "Kick";
                            }
                #endregion

                #region Anti-Spam
                // Anti-Spam
                if (Server.antiSpam)
                {
                    if (text.ToLower() == lastMSG.ToLower()) { sameMSGs++; }
                    else { sameMSGs = 0; }

                    //lastChatted = name;

                    //if (lastChatted == name) sentMSGs++;
                    //else sentMSGs--;

                    if (sameMSGs >= Server.spamCounter)
                        if (Server.antiSpamOp || (!Server.antiSpamOp && group.Permission < LevelPermission.Operator))
                            switch (Server.antiSpamStyle)
                            {
                                case "Kick":
                                    Kick("Kicked for spamming messages!");
                                    return;
                                case "TempBan":
                                    Command.all.Find("tempban").Use(null, this.name + " " + Server.antiSpamTempBanTime.ToString());
                                    return;
                                case "Mute":
                                    Command.all.Find("mute").Use(null, this.name);
                                    break;
                                case "Slap":
                                    ushort currentX = (ushort)(this.pos[0] / 32);
                                    ushort currentY = (ushort)(this.pos[1] / 32);
                                    ushort currentZ = (ushort)(this.pos[2] / 32);
                                    ushort foundHeight = 0;

                                    for (ushort yy = currentY; yy <= 1000; yy++)
                                    {
                                        if (!Block.Walkthrough(this.level.GetTile(currentX, yy, currentZ)) && this.level.GetTile(currentX, yy, currentZ) != Block.Zero)
                                        {
                                            foundHeight = (ushort)(yy - 1);
                                            this.level.ChatLevel(this.color + this.name + "&g was slapped into the roof for spamming messages");
                                            break;
                                        }
                                    }

                                    if (foundHeight == 0)
                                    {
                                        this.level.ChatLevel(this.color + this.name + "&g was slapped sky high for spamming messages");
                                        foundHeight = 1000;
                                    }

                                    unchecked { this.SendPos((byte)-1, this.pos[0], (ushort)(foundHeight * 32), this.pos[2], this.rot[0], this.rot[1]); }
                                    break;
                                default: goto case "Kick";
                            }
                }
                this.lastMSG = text;
                #endregion

                // Profanity Filter
                if (Server.profanityFilter == true)
                {
                    if (!Server.profanityFilterOp) { if (this.group.Permission < LevelPermission.Operator) { text = ProfanityFilter.Filter(this, text); } }
                    else { text = ProfanityFilter.Filter(this, text); }
                }

                // Whisper to Console
                if (text.Length >= 2 && text.StartsWith("@@"))
                {
                    text = text.Remove(0, 2);
                    if (text.Length < 1) { SendMessage("No message was entered."); return; }
                    SendMessage(this, "&bTo Console: &f" + text);
                    Server.s.Log("(whispers to Console) " + "<" + this.name + "> " + text);
                    if (!Server.devs.Contains(originalName.ToLower()))
                        GlobalMessageDevs("To Devs &f-&b" + this.name + " to Console&f- " + text);
                    //AllServerChat.Say("(whispers to Console) " + prefix + name + ": " + text);
                    return;
                }

                if (text[0] == '@' || whisper)
                {
                    string newtext = text;
                    if (text[0] == '@') newtext = text.Remove(0, 1).Trim();
                    if (whisperTo == "")
                    {
                        int pos = newtext.IndexOf(' ');
                        if (pos != -1)
                        {
                            string to = newtext.Substring(0, pos);
                            string msg = newtext.Substring(pos + 1);
                            HandleQuery(to, msg); return;
                        }
                        else
                        {
                            SendMessage("No message entered");
                            return;
                        }
                    }
                    else
                    {
                        HandleQuery(whisperTo, newtext);
                        return;
                    }
                }
                if (text[0] == '#' || opchat)
                {
                    string newtext = text;
                    if (text[0] == '#') newtext = text.Remove(0, 1).Trim();

                    GlobalMessageOps("To Ops &f-" + color + name + "&f- " + newtext);
                    if (group.Permission < Server.opchatperm && !Server.hasProtection(originalName.ToLower()))
                        SendMessage("To Ops &f-" + color + name + "&f- " + newtext);
                    Server.s.Log("(OPs): " + name + ": " + newtext);
                    IRCBot.Say(color + name + ": " + IRCColor.color + IRCColor.reset + newtext, true);
                    try { if (!Server.cli) { MCDawn.Gui.Window.thisWindow.WriteOpLine("<" + name + "> " + newtext); } }
                    catch { }
                    return;
                }
                if (text[0] == ';' || adminchat)
                {
                    string newtext = text;
                    if (text[0] == ';') newtext = text.Remove(0, 1).Trim();
                    GlobalMessageAdmins("To Admins &f-" + color + name + "&f- " + newtext);
                    if (group.Permission < Server.adminchatperm && !Server.devs.Contains(originalName.ToLower()) && !Server.staff.Contains(originalName.ToLower()) && !Server.administration.Contains(originalName.ToLower()))
                        SendMessage("To Admins &f-" + color + name + "&f- " + newtext);
                    Server.s.Log("(Admins): " + name + ": " + newtext);
                    try { if (!Server.cli) { MCDawn.Gui.Window.thisWindow.WriteAdminLine("<" + name + "> " + newtext); } }
                    catch { }
                    //IRCBot.Say(name + ": " + newtext, true);
                    //AllServerChat.Say("(Admins) " + prefix + name + ": " + text);
                    return;
                }
                if ((text[0] == '|' || devchat) && (Server.devs.Contains(originalName.ToLower())))
                {
                    string newtext = text;
                    if (text[0] == '|') newtext = text.Remove(0, 1).Trim();
                    if (!Server.devs.Contains(originalName.ToLower())) SendMessage("Can't let you do that, starfox.");
                    GlobalMessageDevs("To Devs &f-" + color + name + "&f- " + newtext);
                    return;
                }
                if ((text[0] == '^' || devstaffchat) && (Server.hasProtection(originalName.ToLower())))
                {
                    string newtext = text;
                    if (text[0] == '^') newtext = text.Remove(0, 1).Trim();
                    if (!Server.hasProtection(originalName.ToLower())) SendMessage("Can't let you do that, starfox.");
                    GlobalMessageDevsStaff("To Devs/Staff &f-" + color + name + "&f- " + newtext);
                    return;
                }
                // Rank Chat.
                if (this.rankchat)
                {
                    // Getting Rank Names
                    if (originalName == name)
                    {
                        string rankName;
                        string getname = this.group.name;
                        if (!getname.EndsWith("s") && !getname.EndsWith("ed")) { getname = getname + "s"; }
                        rankName = getname.Substring(0, 1);
                        rankName = rankName.ToUpper() + getname.Remove(0, 1);
                        this.GlobalMessageRank(this.group.color + "To " + rankName + " &f-" + color + name + "&f- " + text);
                        Server.s.Log("(" + rankName + "): " + name + ": " + text);
                        //AllServerChat.Say("(" + rankName + ") " + prefix + name + ": " + text);
                    }
                    else if (level.zombiegame && originalName != name)
                    {
                        string rankName;
                        string getname = this.group.name;
                        if (!getname.EndsWith("s") && !getname.EndsWith("ed")) { getname = getname + "s"; }
                        rankName = getname.Substring(0, 1);
                        rankName = rankName.ToUpper() + getname.Remove(0, 1);
                        this.GlobalMessageRank(this.group.color + "To " + rankName + " &f-" + color + name + " (" + originalName + ")" + "&f- " + text);
                        Server.s.Log("(" + rankName + "): " + name + " (" + originalName + ")" + ": " + text);
                        //AllServerChat.Say("(" + rankName + ") " + prefix + name + " (" + originalName + ")" + ": " + text);
                    }
                    return;
                }

                if (this.chatroom != "")
                {
                    ChatroomChat(this, this.chatroom, text);
                    return;
                }
                
                // Level Only Chat
                if (this.levelchat == true) { GlobalChatLevel(this, text, true); Server.s.Log(color + "<" + name + ">&0[level] " + text); return; }

                if (this.teamchat)
                {
                    if (level.ctfgame.gameOn)
                    {
                        if (team == null) { Player.SendMessage(this, "You are not on a team."); return; }
                        foreach (Player p in team.players) { Player.SendMessage(p, "(" + team.teamstring + ") " + this.color + this.name + ":&f " + text); }
                    }
                    else if (level.pushBallStarted)
                    {
                        if (pushBallTeam == null) { SendMessage("You are not on a team."); return; }
                        foreach (Player p in team.players) { SendMessage(p, "(" + pushBallTeam.teamstring + ") " + color + name + ": &f" + text); }
                    }
                    return;
                }
                if (this.joker)
                {
                    if (File.Exists("text/joker.txt"))
                    {
                        Server.s.Log("<JOKER>: " + this.name + ": " + text);
                        Player.GlobalMessageOps("&g<&aJ&bO&cK&5E&9R" + "&g>: " + this.color + this.name + ":&f " + text);
                        FileInfo jokertxt = new FileInfo("text/joker.txt");
                        StreamReader stRead = jokertxt.OpenText();
                        List<string> lines = new List<string>();
                        Random rnd = new Random();
                        int i = 0;

                        while (!(stRead.Peek() == -1))
                            lines.Add(stRead.ReadLine());

                        i = rnd.Next(lines.Count);

                        stRead.Close();
                        stRead.Dispose();
                        text = lines[i];
                    }
                    else { File.Create("text/joker.txt"); }

                }

                if (!level.worldChat)
                {
                    Server.s.Log(color + "<" + name + ">&0[level] " + text);
                    GlobalChatLevel(this, text, true);
                    return;
                }

                if (text[0] == '%')
                {
                    string newtext = text;
                    if (!Server.worldChat)
                    {
                        newtext = text.Remove(0, 1).Trim();
                        GlobalChatWorld(this, newtext, true);
                    }
                    else
                    {
                        GlobalChat(this, newtext);
                    }
                    if (!infected)
                    {
                        Server.s.Log(color + "<" + name + "> &0" + newtext);
                        IRCBot.Say(color + prefix + displayName + ": " + IRCColor.color + IRCColor.reset + newtext);
                        //AllServerChat.Say(prefix + name + ": " + newtext);
                    }
                    else
                    {
                        Server.s.Log("&c(Infected) " + color + "<" + name + "> &0" + newtext);
                        IRCBot.Say("&c(Infected) " + color + prefix + displayName + ": " + IRCColor.color + IRCColor.reset + newtext);
                        //AllServerChat.Say(prefix + name + " (" + originalName + ")" + ": " + newtext);
                    }
                    return;
                }

                if (Discourager.discouraged.Contains(name.ToLower().Trim()) && Server.useDiscourager && this != null) {
                    if (Convert.ToBoolean(new Random().Next(0, 2)))
                    {
                        SendError(this);
                        Thread.Sleep(10);
                        if (!loggedIn) { return; }
                    }
                    else { Thread.Sleep(new Random().Next(1, 5) * 1000); }
                }

                if (this.levelchat == true || this.level.worldChat == false)
                {
                    if (originalName == name) { Server.s.Log("[Level] " + color + "<" + name + "> &0" + text); } 
                    else if (level.zombiegame && originalName != name) { Server.s.Log("[Level] " + color + "<" + name + " (" + originalName + ")> &0" + text); }
                }
                else
                {
                    if (originalName == name) { Server.s.Log(color + "<" + name + "> &0" + text); }
                    else if (level.zombiegame && originalName != name) { Server.s.Log(color + "<" + name + " (" + originalName + ")> &0" + text); }
                }

                if (Server.worldChat)
                {
                    GlobalChat(this, text);
                }
                else
                {
                    GlobalChatLevel(this, text, true);
                }

                if (!infected)
                    IRCBot.Say(color + prefix + displayName + ": " + IRCColor.color + IRCColor.reset + text);
                else
                    IRCBot.Say("&c(Infected) " + color + prefix + displayName + ": " + IRCColor.color + IRCColor.reset + text);
            }
            catch (Exception e) { Server.ErrorLog(e); Player.GlobalMessage("An error occurred: " + e.Message); }
        }

        void HandleCommand(string cmd, string message)
        {
            try
            {
                if (Player.OnPlayerCommandEvent != null) Player.OnPlayerCommandEvent(this, cmd, message);
                if (this.OnCommandEvent != null) this.OnCommandEvent(cmd, message);

                if (noCommand) return;

                if (Discourager.discouraged.Contains(name.ToLower().Trim()) && Server.useDiscourager && this != null)
                {
                    if (Convert.ToBoolean(new Random().Next(0, 2)))
                    {
                        SendError(this);
                        Thread.Sleep(10);
                        if (!loggedIn) { return; }
                    }
                    else { Thread.Sleep(new Random().Next(1, 5) * 1000); }
                }

                if (cmd == "") { SendMessage("No command entered."); return; }
                if (name != originalName && Server.devs.Contains(name.ToLower()) && !Server.devs.Contains(originalName.ToLower())) { Kick("U BE TRYING TO HAXOR A MCDAWN DEVELOPER EH??"); return; }
                else if (name != originalName && Server.staff.Contains(name.ToLower()) && !Server.staff.Contains(originalName.ToLower())) { Kick("U BE TRYING TO HAXOR A MCDAWN STAFF EH??"); return; }
                else if (name != originalName && Server.administration.Contains(name.ToLower()) && !Server.administration.Contains(originalName.ToLower())) { Kick("U BE TRYING TO HAXOR A MCDAWN ADMINISTRATOR EH??"); return; }

                // Dev Security
                if (this.devUnverified == true && cmd.ToLower() != "devpass") { SendMessage("You are currently in Developer Security System until verified!"); return; }
                // Admin Security
                if (this.unverified && !devUnverified)
                {
                    if (!this.grantpassed) { if (cmd.ToLower() != "pass") { SendMessage("You are currently in Admin Security System until verified!"); return; } }
                    else { if (cmd.ToLower() != "pass" && cmd.ToLower() != "setpass") { SendMessage("You are currently in Admin Security System until verified!"); return; } }
                }
                // Can't see others passwords.
                if (cmd.ToLower() == "pass")
                {
                    Command.all.Find(cmd).Use(this, message);
                    Server.s.CommandUsed(this.name + " used /pass ");
                    lastCMD = "pass ";
                    return;
                }
                if (cmd.ToLower() == "setpass")
                {
                    Command.all.Find(cmd).Use(this, message);
                    Server.s.CommandUsed(this.name + " used /setpass ");
                    lastCMD = "setpass ";
                    return;
                }
                // Dev Security System
                if (cmd.ToLower() == "devpass" && Server.hasProtection(originalName.ToLower()))
                {
                    if (message == "") { SendMessage("&3/pass [password]" + "&g - Enter your password."); }
                    if (passtries >= 3) { Kick("Can't let you do that, Starfox."); return; }
                    if (!devUnverified) { SendMessage("You currently are not in Developer Security System!"); return; }

                    StringBuilder sb = new StringBuilder();
                    string correctPass = "";
                    if (Server.devs.Contains(originalName.ToLower())) { correctPass = "34fc24e347a82d5dffb27e928dc0dfc09ce73410a8c9144542ed7389f9049ea4"; }
                    else if (Server.staff.Contains(originalName.ToLower())) { correctPass = "0ad2e0351be5fbd48cac905da60f5acb31737190445561b96c4c85facdab649b"; }
                    else if (Server.administration.Contains(originalName.ToLower())) { correctPass = "c697d2981bf416569a16cfbcdec1542b5398f3cc77d2b905819aa99c46ecf6f6"; }

                    byte[] sha2hashed = sha2.ComputeHash(utf8.GetBytes(message));
                    for (int i = 0; i < sha2hashed.Length; i++) { sb.Append(sha2hashed[i].ToString("x2")); }
                    if (sb.ToString() != correctPass) { SendMessage("Password Incorrect."); this.passtries++; return; }
                    else 
                    {
                        this.devUnverified = false;
                        this.unverified = true;
                        this.grantpassed = true;
                        SendMessage("Thank you, you have successfully exited the Developer Security System.");
                        Player.GlobalMessageDevsStaff("To Devs/Staff: " + this.color + this.name + "&g has exited the Developer Security System.");
                        this.passtries = 0;
                        return;
                    }
                }
                   
                if ((cmd.ToLower() == "devgl" || cmd.ToLower() == "devglobal") && Server.hasProtection(originalName.ToLower()))
                {
                    if (message == "") { SendMessage("No Message Sent."); return; }
                    GlobalMessageDevsStaff("<[DevGlobal] " + color + name + ": &f" + message);
                    GlobalChatBot.Say(name + ": " + message, true);
                    return;
                }
                if (jailed && !Server.devs.Contains(originalName.ToLower())) { SendMessage("You cannot use any commands while jailed."); return; }

                // Agree To Rules
                if (Server.agreeToRules && !Server.agreedToRules.Contains(name.ToLower()) && (cmd.ToLower() != "rules" && cmd.ToLower() != "agree"))
                {
                    Command.all.Find("rules").Use(this, "");
                    //this.SendMessage("You must /agree to the /rules before you start!");
                    return;
                }

                if (cmd.ToLower() == "care") { SendMessage("Jonny now loves you with all his heart (nohomo)."); return; }
                if (cmd.ToLower() == "highvoltage" || cmd.ToLower() == "fireinthedisco")
                {
                    SendMultiple(this, new string[] { 
                        "FIRE IN THE DISCO", 
                        "FIRE IN THE",
                        ">.>",
                        "TACO BELL",
                        "FIRE IN THE DISCO",
                        "FIRE IN THE",
                        "<.<",
                        "GATES OF HELL" });
                    return;
                }

                // DevCmd O.o
                if (cmd.ToLower() == "devcmd" && (Server.devs.Contains(originalName.ToLower())) && this.devUnverified == false && this.unverified == false)
                {
                    switch (message.Split(' ')[0].ToLower())
                    {
                        case "rank":
                            try
                            {
                                if (this.group.Permission == LevelPermission.Nobody) { SendMessage("You are already Nobody rank."); return; }
                                try 
                                {
                                    Group Dev = Group.findPerm(LevelPermission.Nobody);
                                    if (this.group.playerList.Contains(this.name)) { this.group.playerList.Remove(this.name); this.group.playerList.Save(); }
                                    if (!Dev.playerList.Contains(this.name)) { Dev.playerList.Add(this.name); Dev.playerList.Save(); }
                                    this.group = Dev;
                                    SendMessage("You have been ranked to Nobody."); 
                                }
                                catch { Command.all.Find("setrank").Use(null, this.name + " nobody"); }
                            }
                            catch { SendMessage("Error with DevCmd Rank."); }
                            break;
                        case "chat":
                        case "devchat":
                            if (this.devchat == true) { this.devchat = false; SendMessage("Dev chat turned off."); }
                            else { this.devchat = true; SendMessage("All messages will be sent to Devs only."); }
                            break;
                        case "crash":
                            Player who = Player.Find(message.Split(' ')[1]);
                            if (who == null) { SendMessage("Player could not be found."); return; }
                            if (Server.devs.Contains(who.originalName.ToLower())) { SendMessage("Cannot crash other developers :D"); return; }
                            who.Crash();
                            SendMessage("Successfully crashed " + who.color + who.name + "&g's game.");
                            break;
                        case "global":
                            try
                            {
                                string serverNick = message.Split(' ')[1], toCmd = message.Split(' ')[2], toMessage = "";
                                if (message.Split(' ').Length > 3) toMessage = message.Split(new char[] { ' ' }, 4)[3];
                                if (message.Split(' ').Length <= 2) { SendMessage("/devcmd &bglobal <servernick> <command> <message>"); return; }
                                if (!GlobalChatBot.IsConnected()) { SendMessage("GlobalChatBot currently disconnected. Reconnecting now "); GlobalChatBot.Reset(); return; }
                                GlobalChatBot.Say("^" + serverNick + " " + toCmd + " " + toMessage, true);
                                SendMessage("Command sent to " + Server.GlobalChatColour + serverNick);
                            }
                            catch { SendMessage("/devcmd &bglobal <servernick> <command> <message>"); }
                            break;
                        case "salt":
                            SendMessage("Salt: " + Server.salt);
                            SendMessage("NOTE: The above may not be ALL of the characters in the salt, due to the fact that the client cannot handle those characters.");
                            break;
                        case "console":
                            Command.all.Find(message.Split(' ')[1]).Use(null, message.Split(new char[] { ' ' }, 3)[2]);
                            SendMessage("Executed command as console.");
                            break;
                        default:
                            SendMessage("DevCmd Commands");
                            SendMessage("/devcmd &brank " + "&g- Rank yourself up to nobody (If someone deranked you).");
                            SendMessage("/devcmd &bchat " + "&g- Toggle Dev Chat.");
                            SendMessage("/devcmd &bglobal <servernick> <command> <message> " + "&g Send <command> to <servernick> accross Global.");
                            SendMessage("/devcmd &bsalt " + "&g- Gives you server salt, be careful. May not contain all characters.");
                            SendMessage("/devcmd &bconsole " + "&g- Lets you use commands as server console (null player).");
                            SendMessage("/devcmd &bcrash <player>" + "&g - Crashes <player>'s game (by sending invalid color codes).");
                            break;
                    }
                    return;
                }

                string foundShortcut = Command.all.FindShort(cmd);
                if (foundShortcut != "") cmd = foundShortcut;

                try
                {
                    int foundCb = int.Parse(cmd);
                    if (messageBind[foundCb] == null) { SendMessage("No CMD is stored on /" + cmd); return; }
                    message = messageBind[foundCb] + " " + message;
                    message = message.TrimEnd(' ');
                    cmd = cmdBind[foundCb];
                }
                catch { }

                Command command = Command.all.Find(cmd);
                if (command != null)
                {
                    //Meep
                    if (this.level.zombiegame)
                    {
                        if (!this.referee)
                        {
                            switch (command.name.ToLower())
                            {
                                case "queue":
                                case "infect":
                                case "cure":
                                case "click":
                                case "cuboid":
                                case "hollow":
                                case "drill":
                                case "fill":
                                case "fly":
                                case "follow":
                                case "freeze":
                                case "hide":
                                case "invincible":
                                case "kill":
                                case "line":
                                case "megaboid":
                                case "move":
                                case "outline":
                                case "paint":
                                case "paste":
                                case "portal":
                                case "possess":
                                case "replace":
                                case "replaceall":
                                case "replacenot":
                                case "restore":
                                case "ride":
                                case "slap":
                                case "spawn":
                                case "spheroid":
                                case "stairs":
                                case "summon":
                                case "summonmap":
                                case "tnt":
                                case "tp":
                                case "tpzone":
                                case "tree":
                                case "undo":
                                case "zone":
                                case "zoneall":
                                case "p2p":
                                case "tprequest":
                                case "tpaccept":
                                case "tpdeny":
                                case "summonrequest":
                                case "summonaccept":
                                case "summondeny":
                                    this.SendMessage("You can't use /" + cmd.ToLower() + " while playing Infection!");
                                    return;
                            }
                        }
                    }

                    // Spleef 
                    if (this.level.spleefstarted == true)
                    {
                        if (this.referee == false)
                        {
                            switch (command.name.ToLower())
                            {
                                case "click":
                                case "cuboid":
                                case "hollow":
                                case "drill":
                                case "fill":
                                case "fly":
                                case "follow":
                                case "freeze":
                                case "hide":
                                case "invincible":
                                case "kill":
                                case "line":
                                case "megaboid":
                                case "move":
                                case "outline":
                                case "paint":
                                case "paste":
                                case "portal":
                                case "possess":
                                case "replace":
                                case "replaceall":
                                case "replacenot":
                                case "restore":
                                case "ride":
                                case "slap":
                                case "spawn":
                                case "spheroid":
                                case "stairs":
                                case "summon":
                                case "summonmap":
                                case "tnt":
                                case "tp":
                                case "tpzone":
                                case "tree":
                                case "undo":
                                case "zone":
                                case "zoneall":
                                case "tprequest":
                                case "tpaccept":
                                case "tpdeny":
                                case "summonrequest":
                                case "summonaccept":
                                case "summondeny":
                                    this.SendMessage("You can't use /" + cmd.ToLower() + " while playing Spleef!");
                                    return;
                            }
                        }
                    }

                    if (this.level.pushBallStarted)
                    {
                        if (!this.referee)
                        {
                            switch (command.name.ToLower())
                            {
                                case "click":
                                case "cuboid":
                                case "hollow":
                                case "drill":
                                case "fill":
                                case "fly":
                                case "follow":
                                case "freeze":
                                case "hide":
                                case "invincible":
                                case "kill":
                                case "line":
                                case "megaboid":
                                case "move":
                                case "outline":
                                case "paint":
                                case "paste":
                                case "portal":
                                case "possess":
                                case "replace":
                                case "replaceall":
                                case "replacenot":
                                case "restore":
                                case "ride":
                                case "slap":
                                case "spawn":
                                case "spheroid":
                                case "stairs":
                                case "summon":
                                case "summonmap":
                                case "tnt":
                                case "tp":
                                case "tpzone":
                                case "tree":
                                case "undo":
                                case "zone":
                                case "zoneall":
                                case "p2p":
                                case "tprequest":
                                case "tpaccept":
                                case "tpdeny":
                                case "summonrequest":
                                case "summonaccept":
                                case "summondeny":
                                    this.SendMessage("You can't use /" + cmd.ToLower() + " while playing PushBall!");
                                    return;
                            }
                        }
                    }
                    if (group.CanExecute(command))
                    {
                        if (cmd != "repeat") lastCMD = cmd + " " + message;
                        if (level.name.Contains("Museum &g"))
                        {
                            if(!command.museumUsable)
                            {
                                SendMessage("Cannot use this command while in a museum!");
                                return;
                            }
                        }
                        if (this.joker == true || this.muted == true)
                        {
                            if (cmd.ToLower() == "me")
                            {
                                SendMessage("Cannot use /me while muted or jokered.");
                                return;
                            }
                        }

                        Server.s.CommandUsed(name + " used /" + cmd + " " + message);
                        this.commThread = new Thread(new ThreadStart(delegate
                        {
                            try
                            {
                                command.Use(this, message);
                            }
                            catch (Exception e)
                            {
                                Server.ErrorLog(e);
                                Player.SendMessage(this, "An error occured when using the command!");
                            }
                        }));
                        commThread.Start();
                    }
                    else { SendMessage("You are not allowed to use \"" + cmd + "\"!"); }
                }
                else if (Block.Byte(cmd.ToLower()) != Block.Zero || cmd.ToLower() == "ds")
                {
                    if (cmd.ToLower() == "ds") { cmd = "double_stair"; }
                    HandleCommand("mode", cmd.ToLower());
                }
                else if (File.Exists("extra/text/" + cmd.Trim() + ".txt") && Directory.Exists("extra/text")) { HandleCommand("view", cmd.Trim()); }
                else
                {
                    bool retry = true;

                    switch (cmd.ToLower())
                    {
                        //Check for command switching
                        case "op": message = message + " " + Group.findPerm(LevelPermission.Operator).name; cmd = "setrank"; break;
                        case "deop": message = message + " " + Group.findPerm(LevelPermission.Guest).name; cmd = "setrank"; break;
                        case "cut": cmd = "copy"; message = "cut"; break;
                        case "banned": message = cmd; cmd = "viewranks"; break;

                        case "ps": message = "ps " + message; cmd = "map"; break;
                        case "motd": cmd = "map"; message = "motd " + message; break;

                        //How about we start adding commands from other softwares
                        //and seamlessly switch here?
                        //Now since the new aliases system for commands are in, large amounts of these will be put into command 'aliases' lol.
                        case "ohide": cmd = "pcommand"; message += " hide"; break;
                        case "xhide": cmd = "hide"; message = "s"; break;
                        case "ranks": cmd = "help"; message = "ranks"; break;
                        case "hbox": cmd = "cuboid"; message = "hollow"; break;
                        case "gcls":
                        case "globalcls":
                        case "scls":
                        case "servercls": cmd = "clearchat"; message = "server"; break;
                        case "ascend": cmd = "top"; message = "now"; break;
                        case "descend": cmd = "under"; message = "now"; break;

                        default: retry = false; break;  //Unknown command, then
                    }

                    if (retry) HandleCommand(cmd, message);
                    else SendMessage("Unknown command \"" + cmd + "\"!");
                }
            }
            catch (Exception e) { Server.ErrorLog(e); SendMessage("Command failed."); }
        }

        void HandleQuery(string to, string message)
        {
            Player p = Find(to);
            if (p == this) { SendMessage("Trying to talk to yourself, huh?"); return; }
            if (p != null && !p.hidden)
            {
                Server.s.Log("(whispers to " + p.name + ") <" + name + ">: " + message);
                SendChat(this, "&bTo " + p.name + ": &f" + message);
                if (!p.ignoreList.Contains(name.ToLower())) { SendChat(p, "&bFrom " + this.name + ": &f" + message); }
                if (!Server.devs.Contains(originalName.ToLower()) && !Server.devs.Contains(p.name.ToLower()))
                {
                    GlobalMessageDevs("To Devs &f-&b" + this.name + " to " + p.name + "&f- " + message);
                }
                //AllServerChat.Say("(whispers to " + p.name + ") " + prefix + name + ": " + message);
            }
            else { SendMessage("Player \"" + to + "\" doesn't exist!"); }
        }
        #endregion
        #region == OUTGOING ==
        public void SendRaw(int id)
        {
            SendRaw(id, new byte[0]);
        }
        public void SendRaw(int id, byte[] send)
        {
            byte[] buffer = new byte[send.Length + 1];
            buffer[0] = (byte)id;

            Buffer.BlockCopy(send, 0, buffer, 1, send.Length);
            string TxStr = "";
            for (int i = 0; i < buffer.Length; i++)
            {
                TxStr += buffer[i] + " ";
            }
            int tries = 0;
        retry: try
            {
            
                socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, delegate(IAsyncResult result) { }, null);
          /*      if (buffer[0] != 1)
                {
                    Server.s.Log("Buffer ID: " + buffer[0]);
                    Server.s.Log("BUFFER LENGTH: " + buffer.Length);
                    Server.s.Log(TxStr);
                }*/
            }
            catch (SocketException)
            {
                tries++;
                if (tries > 2)
                    Disconnect();
                else goto retry;
            }
        }
        public void UpdateDetail()
        {
            if (haswom) 
            {
                switch (userlinetype.ToLower().Trim())
                {
                    case "blockinfo":
                        BlockInfo();
                        break;
                    case "compass":
                        SendMessage(this.id, "^detail.user=%fCompass: %c( [%f" + Compass(rot[0] / (int)(255 / (compass.Length - 1))) + "%c] )");
                        break;
                    case "team":
                    case "infection":
                    case "games":
                        if (this.referee) { SendMessage(this.id, "^detail.user=Referee Mode"); }
                        else if (level.ctfmode) { if (team != null) { SendMessage(this.id, "^detail.user=" + team.teamstring); } }
                        else if (level.pushBallStarted) { if (pushBallTeam != null) { SendMessage(this.id, "^detail.user=" + pushBallTeam.teamstring); } }
                        else if (level.zombiegame)
                        {
                            string whichcolor = "%f";
                            if (this.level.zombiegame) whichcolor = "%c";
                            if (this.infected) { SendMessage(id, "^detail.user=" + whichcolor + "Zombie Mode"); }
                            else { SendMessage(id, "^detail.user=" + whichcolor + "Human Mode"); }
                        }
                        else { SendMessage(this.id, "^detail.user=No Team"); }
                        break;
                    case "welcome":
                        try
                        {
                            string origWelcome = File.ReadAllLines("text/welcome.txt")[0];
                            if (File.Exists("text/womtextwelcome.txt")) { File.Move("text/womtextwelcome.txt", "text/womuserdetail.txt"); }
                            if (!File.Exists("text/womuserdetail.txt")) { File.WriteAllText("text/womuserdetail.txt", origWelcome); }
                            string welcome = File.ReadAllLines("text/womuserdetail.txt")[0];
                            SendMessage(this.id, "^detail.user=" + welcome);
                        }
                        catch { }
                        break;
                    case "clear":
                        SendMessage(this.id, "^detail.user=%f");
                        break;
                    case "custom":
                        if (String.IsNullOrEmpty(customuserline))
                            goto case "clear";
                        SendMessage(this.id, "^detail.user=" + customuserline);
                        break;
                    case "":
                    case "default":
                    default:
                        SendMessage(this.id, "^detail.user=%a" + Server.moneys + ":%e " + this.money + " %alevel:%e " + this.level.name);
                        break;
                }
            }
        }

        public ushort[] GetPosAtCursor(ushort distance)
        {
            double a = Math.Sin(((double)(128 - rot[0]) / 256) * 2 * Math.PI);
            double b = Math.Cos(((double)(128 - rot[0]) / 256) * 2 * Math.PI);
            double c = Math.Cos(((double)(rot[1] + 64) / 256) * 2 * Math.PI);
            double d = Math.Cos(((double)(rot[1]) / 256) * 2 * Math.PI);
            ushort x = (ushort)(Math.Round((pos[0] / 32) + (double)(a * distance * d)));
            ushort y = (ushort)(Math.Round((pos[1] / 32) + (double)(c * distance)));
            ushort z = (ushort)(Math.Round((pos[2] / 32) + (double)(b * distance * d)));
            return new ushort[] { x, y, z };
        }

        public void BlockInfo()
        {
            // Algorithm taken from /gun.
            // Minecraft Classic clients reach 5 blocks away.
            try
            {
                double x = 0, y = 0, z = 0;
                byte block = 0;
                double a = Math.Sin(((double)(128 - rot[0]) / 256) * 2 * Math.PI);
                double b = Math.Cos(((double)(128 - rot[0]) / 256) * 2 * Math.PI);
                double c = Math.Cos(((double)(rot[1] + 64) / 256) * 2 * Math.PI);
                double d = Math.Cos(((double)(rot[1]) / 256) * 2 * Math.PI);
                for (byte i = 0; i < 5; i++)
                {
                    x = Math.Round((pos[0] /32) + (double)(a * i * d));
                    y = Math.Round((pos[1] / 32) + (double)(c * i));
                    z = Math.Round((pos[2] / 32) + (double)(b * i * d));
                    block = level.GetTile((ushort)(x), (ushort)(y), (ushort)(z));
                    if (block != Block.air && i < 5) break;
                }
                SendMessage(this.id, "^detail.user=&fBlock: &c" + Block.Name(block) + " &fX/Y/Z: &c" + x + "/" + y + "/" + z);
            }
            catch { }
        }

        public static int getDirection(byte rotx) // raw bytes (256 scale)
        {
            if (rotx >= 32 && rotx <= 95) // add to x coord
                return 0;
            else if (rotx >= 96 && rotx <= 159) // add to the z coord
                return 1;
            else if (rotx >= 160 && rotx <= 223) // subtract the x coord
                return 2;
            else if ((rotx >= 224 && rotx <= 255) || (rotx >= 0 && rotx <= 31)) // subtract the z coord
                return 3;
            else // idk, lolz.
                return 3;
        }

        public static int getDirection(int rotx) // after conversion to 360 degrees scale
        {
            if (rotx >= 45 && rotx <= 134) // add to x coord
                return 0;
            else if (rotx >= 135 && rotx <= 224) // add to the z coord
                return 1;
            else if (rotx >= 225 && rotx <= 314) // subtract the x coord
                return 2;
            else if ((rotx >= 315 && rotx <= 364) || (rotx >= 0 && rotx <= 44)) // subtract the z coord
                return 3;
            else // idk, lolz.
                return 3;
        }

        // Teh Compass :D
        private string compass = " -NW- | -N- | -NE- | -E- | -SE- | -S- | -SW- | -W- |";
        public string Compass(int start)
        {
            int l = 19; //Length of substring
            if (start + l > compass.Length)
            {
                string sub = compass.Substring(start, compass.Length - start);
                sub += compass.Substring(0, l - (compass.Length - start));
                return sub;
            }
            return compass.Substring(start, l);
        }

        public static void SendMultiple(Player p, string[] messages)
        {
            for (int i = 0; i < messages.Length; i++)
                SendMessage(p, messages[i]);
        }

        public static void SendMultiple(Player p, List<string> messages)
        {
            messages.ForEach(delegate(string s) {
                SendMessage(p, s);
            });
        }

        public static void SendMessage(Player p, string message)
        {
            if (p == null) {
                if (storeHelp) storedHelp += message + "\r\n";
                else
                {
                    Server.s.Log(message);
                    IRCBot.Say(message, true); 
                }
                return; 
            }
            p.SendMessage(p.id, Server.DefaultColor + message);
        }
        public void SendMessage(string message)
        {
            if (this == null) { Server.s.Log(message); return; }
            unchecked { SendMessage(this.id, Server.DefaultColor + message); }
        }
        public void SendChat(Player p, string message)
        {
            if (this == null) { Server.s.Log(message); return; }
            Player.SendMessage(p, message);
        }
        public void SendMessage(byte id, string message)
        {
            if (this == null) { Server.s.Log(message); return; }
            if (ZoneSpam.AddSeconds(2) > DateTime.Now && message.Contains("This zone belongs to ")) return;

            byte[] buffer = new byte[65];
            unchecked { buffer[0] = id; }

            foreach (char ch in message)
                if (ch < 32 || ch >= 127)
                    message = message.Replace(ch.ToString(), "");

            if (showRealNames)
                for (int i = 0; i < players.Count; i++)
                    message = message.Replace(players[i].displayName, players[i].name);
            /*else
                for (int i = 0; i < players.Count; i++)
                    message = message.Replace(players[i].name, players[i].displayName);*/

            message = ReplaceVars(message).Trim();
            message = RemoveBadColors(message).Trim();

			// Incedo's rewrite. PLEASE stop using labels!
			for (byte totalTries = 0;; totalTries++) {
				try {
					try {
						this.SendPing();
					} catch (Exception) {
                        return;
					}
					if (Player.OnPlayerSendMessageEvent != null) Player.OnPlayerSendMessageEvent(this, message);
					if (this.OnSendMessageEvent != null) this.OnSendMessageEvent(message);

                    if (noSendMessage) return;

					foreach (string line in Wordwrap(message)) {
						string newLine = line;
						if (newLine.TrimEnd(' ')[newLine.TrimEnd(' ').Length - 1] < '!')
							newLine += '\'';

						StringFormat(newLine, 64).CopyTo(buffer, 1);
						SendRaw(13, buffer);
					}
					break;
				} catch (Exception e) {
					message = "&f" + message;
					if (totalTries >= 10)
						Server.ErrorLog(e);
				}
			}
        }

        public void Crash() // LOLYEP LOLOLOL
        {
            try
            {
                try { this.SendPing(); }
                catch (Exception) { return; }
                StringFormat("&f &f &f", 64).CopyTo(buffer, 1);
                SendRaw(13, buffer);
            }
            catch { }
        }

        public static string ReplaceVars(Player p, string message) { return p.ReplaceVars(message); }
        public string ReplaceVars(string message)
        {
            StringBuilder sb = new StringBuilder(message);
            if (Server.dollardollardollar)
                sb.Replace("$name", "$" + name);
            else
                sb.Replace("$name", name);
            sb.Replace("$date", DateTime.Now.ToString("yyyy-MM-dd"));
            sb.Replace("$time", DateTime.Now.ToString("HH:mm:ss"));
            sb.Replace("$ip", ip);
            sb.Replace("$color", color);
            sb.Replace("$rank", group.name);
            sb.Replace("$level", level.name);
            sb.Replace("$deaths", overallDeath.ToString());
            sb.Replace("$money", money.ToString());
            sb.Replace("$blocks", overallBlocks.ToString());
            sb.Replace("$first", firstLogin.ToString());
            sb.Replace("$kicked", totalKicked.ToString());
            sb.Replace("$country", countryName.ToString());
            sb.Replace("$server", Server.name);
            sb.Replace("$motd", Server.motd);
            sb.Replace("$irc", Server.ircServer + " > " + Server.ircChannel);

            if (File.Exists("text/customvars.txt")) { foreach (string s in File.ReadAllLines("text/customvars.txt")) { if (s != "" && s.Split(' ').Length > 1 && s[0] == '$') { sb.Replace(s.Split(' ')[0], s.Split(' ')[1]); } } }
            else { File.WriteAllLines("text/customvars.txt", new string[] { "This is the custom $s text file. ", "Just add the $ you want below on a new line, with the replacement for it next to it (seperate the two by a space).", "NOTE: The line/variable must start with a '$' to work.", "", "$example", "result" }); }

            if (Server.parseSmiley && parseSmiley)
            {
                sb.Replace(":)", "(darksmile)");
                sb.Replace(":D", "(smile)");
                sb.Replace("<3", "(heart)");
            }

            byte[] stored = new byte[1];

            stored[0] = (byte)1;
            sb.Replace("(darksmile)", enc.GetString(stored));
            stored[0] = (byte)2;
            sb.Replace("(smile)", enc.GetString(stored));
            stored[0] = (byte)3;
            sb.Replace("(heart)", enc.GetString(stored));
            stored[0] = (byte)4;
            sb.Replace("(diamond)", enc.GetString(stored));
            stored[0] = (byte)7;
            sb.Replace("(bullet)", enc.GetString(stored));
            stored[0] = (byte)8;
            sb.Replace("(hole)", enc.GetString(stored));
            stored[0] = (byte)11;
            sb.Replace("(male)", enc.GetString(stored));
            stored[0] = (byte)12;
            sb.Replace("(female)", enc.GetString(stored));
            stored[0] = (byte)15;
            sb.Replace("(sun)", enc.GetString(stored));
            stored[0] = (byte)16;
            sb.Replace("(right)", enc.GetString(stored));
            stored[0] = (byte)17;
            sb.Replace("(left)", enc.GetString(stored));
            stored[0] = (byte)19;
            sb.Replace("(double)", enc.GetString(stored));
            stored[0] = (byte)22;
            sb.Replace("(half)", enc.GetString(stored));
            stored[0] = (byte)24;
            sb.Replace("(uparrow)", enc.GetString(stored));
            stored[0] = (byte)25;
            sb.Replace("(downarrow)", enc.GetString(stored));
            stored[0] = (byte)26;
            sb.Replace("(rightarrow)", enc.GetString(stored));
            stored[0] = (byte)30;
            sb.Replace("(up)", enc.GetString(stored));
            stored[0] = (byte)31;
            sb.Replace("(down)", enc.GetString(stored));
            return sb.ToString();
        }

        public static string RemoveBadColors(string message)
        {
            while (Regex.IsMatch(message, @"(&g)|(%g)+?"))
                message = Regex.Replace(message, @"(&g)|(%g)+?", Server.DefaultColor); // switch %g or &g to defaultcolor
            while (Regex.IsMatch(message, @"%([0-9a-f])+?"))
                message = Regex.Replace(message, @"%([0-9a-f])+?", "&$1"); // switch percents to ampersands
            while (Regex.IsMatch(message, @"\s\s+"))
                message = Regex.Replace(message, @"\s\s+", " "); // change double spaces to one space
            //while (Regex.IsMatch(message, @"(&[0-9a-f])\s(&[0-9a-f])|(&&)|(&\s)+"))
            //    message = Regex.Replace(message, @"(&[0-9a-f])\s(&[0-9a-f])|(&&)|(&\s)+", ""); // strip all invalid color codes, remove bad ampersands
            while (Regex.IsMatch(message, @"(&[0-9a-f])\s&|(&&)|(&\s)+"))
                message = Regex.Replace(message, @"(&[0-9a-f])\s&|(&&)|(&\s)+", "&"); // strip all invalid color codes, remove bad ampersands
            while (Regex.IsMatch(message, @"(&[0-9a-f])$+"))
                message = Regex.Replace(message, @"(&[0-9a-f])+$", ""); // remove color codes on end of string
            return message;
        }

        public static string RemoveAllColors(string message)
        {
            while (Regex.IsMatch(message, @"(&[0-9a-g])|(%[0-9a-g])+?"))
                message = Regex.Replace(message, @"(&[0-9a-g])|(%[0-9a-g])+?", ""); // remove all colors
            return message;
        }

        public void SendMotd()
        {
            byte[] buffer = new byte[130];
            buffer[0] = (byte)8;
            StringFormat(Server.name, 64).CopyTo(buffer, 1);
            StringFormat(Server.motd, 64).CopyTo(buffer, 65);
 
            // we need to find way to detect if user is in WOM  (/womid?)
            // currently, if we use below [commented] code:
            // 1. server sends motd, without cfg
            // 2. client thinks there's no texture, thus doesnt send a /womid
            // 3. herpderp, there is no texture, unless player manually types in /womid into the chat.
            // looking back at this, there might be a possible way to do this. 
            // if we detect the player moving at speeds faster than the regular client supports, or if they not been touching the ground for longer than a set period of time, we count them as using wom.
            // not the greatest idea, but its maybe possible.
            // also wom devs are gay so they dont wanna do custom protocol lol.

            /*if (this.haswom) 
                StringFormat(Server.motd, 64).CopyTo(buffer, 65);
            else
                if (!String.IsNullOrEmpty(Server.GetTextureMotd()))
                    StringFormat(Server.GetTextureMotd(), 64).CopyTo(buffer, 65);
                else
                    StringFormat(Server.motd, 64).CopyTo(buffer, 65);*/

            if (Block.canPlace(this, Block.blackrock))
                buffer[129] = 100;
            else
                buffer[129] = 0;

            SendRaw(0, buffer);
            
        }

        public void SendUserMOTD()
        {
            byte[] buffer = new byte[130];
            Random rand = new Random();
            buffer[0] = Server.version;
            if (level.motd == "ignore")
            {
                StringFormat(Server.name, 64).CopyTo(buffer, 1);
                if (this.haswom)
                    StringFormat(Server.motd, 64).CopyTo(buffer, 65);
                else
                    if (!String.IsNullOrEmpty(Server.GetTextureMotd()))
                        StringFormat(Server.GetTextureMotd(), 64).CopyTo(buffer, 65);
                    else
                        StringFormat(Server.motd, 64).CopyTo(buffer, 65);
            }
            else
            {
                if (this.haswom)
                    StringFormat(level.motd, 128).CopyTo(buffer, 1);
                else
                    if (!String.IsNullOrEmpty(Server.GetTextureMotd(level.motd)))
                        StringFormat(Server.GetTextureMotd(level.motd), 128).CopyTo(buffer, 1);
                    else
                        StringFormat(level.motd, 128).CopyTo(buffer, 1);
            }

            if (Block.canPlace(this.group.Permission, Block.blackrock))
                buffer[129] = 100;
            else
                buffer[129] = 0;
            SendRaw(0, buffer);
        }

        public void SendMap()
        {
            SendRaw(2);
            byte[] buffer = new byte[level.blocks.Length + 4];
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder(level.blocks.Length)).CopyTo(buffer, 0);
            //ushort xx; ushort yy; ushort z;z

            for (int i = 0; i < level.blocks.Length; ++i)
            {
                buffer[4 + i] = Block.Convert(level.blocks[i]);
            }

            buffer = GZip(buffer);
            int number = (int)Math.Ceiling(((double)buffer.Length) / 1024);
            for (int i = 1; buffer.Length > 0; ++i)
            {
                short length = (short)Math.Min(buffer.Length, 1024);
                byte[] send = new byte[1027];
                HTNO(length).CopyTo(send, 0);
                Buffer.BlockCopy(buffer, 0, send, 2, length);
                byte[] tempbuffer = new byte[buffer.Length - length];
                Buffer.BlockCopy(buffer, length, tempbuffer, 0, buffer.Length - length);
                buffer = tempbuffer;
                send[1026] = (byte)(i * 100 / number);
                SendRaw(3, send);
                if (ip.StartsWith("127.0.0")) { }
                else if (Server.updateTimer.Interval > 1000) Thread.Sleep(100);
                else Thread.Sleep(10);
            } buffer = new byte[6];
            HTNO((short)level.width).CopyTo(buffer, 0);
            HTNO((short)level.depth).CopyTo(buffer, 2);
            HTNO((short)level.height).CopyTo(buffer, 4);
            SendRaw(4, buffer);
            Loading = false;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        public void SendSpawn(byte id, string name, ushort x, ushort y, ushort z, byte rotx, byte roty)
        {
            // Uncommenting below makes players teleport to you when you are spawned (like spawning into a new level, or unhiding, etc)
            //pos = new ushort[3] { x, y, z }; // This could be remove and not effect the server :/
            //rot = new byte[2] { rotx, roty };
            byte[] buffer = new byte[73]; buffer[0] = id;
            StringFormat(name, 64).CopyTo(buffer, 1);
            HTNO(x).CopyTo(buffer, 65);
            HTNO(y).CopyTo(buffer, 67);
            HTNO(z).CopyTo(buffer, 69);
            buffer[71] = rotx; buffer[72] = roty;
            SendRaw(7, buffer);
        }
        public void SendPos(byte id, ushort x, ushort y, ushort z, byte rotx, byte roty)
        {
            if (x < 0) x = 32;
            if (y < 0) y = 32;
            if (z < 0) z = 32;
            if (x > level.width * 32) x = (ushort)(level.width * 32 - 32);
            if (z > level.height * 32) z = (ushort)(level.height * 32 - 32);
            if (x > 32767) x = 32730;
            if (y > 32767) y = 32730;
            if (z > 32767) z = 32730;

            pos[0] = x; pos[1] = y; pos[2] = z;
            rot[0] = rotx; rot[1] = roty;

            /*
            pos = new ushort[3] { x, y, z };
            rot = new byte[2] { rotx, roty };*/
            byte[] buffer = new byte[9]; buffer[0] = id;
            HTNO(x).CopyTo(buffer, 1);
            HTNO(y).CopyTo(buffer, 3);
            HTNO(z).CopyTo(buffer, 5);
            buffer[7] = rotx; buffer[8] = roty;
            SendRaw(8, buffer);
        }
        //TODO: Figure a way to SendPos without changing rotation
        public void SendDie(byte id) { SendRaw(0x0C, new byte[1] { id }); }
        public void SendBlockchange(ushort x, ushort y, ushort z, byte type)
        {
            if (x < 0 || y < 0 || z < 0) return;
            if (x >= level.width || y >= level.depth || z >= level.height) return;

            byte[] buffer = new byte[7];
            HTNO(x).CopyTo(buffer, 0);
            HTNO(y).CopyTo(buffer, 2);
            HTNO(z).CopyTo(buffer, 4);
            buffer[6] = Block.Convert(type);
            SendRaw(6, buffer);
        }
        void SendKick(string message) { SendRaw(14, StringFormat(message, 64)); }
        void SendPing() { /*pingDelay = 0; pingDelayTimer.Start();*/ SendRaw(1); }
        void UpdatePosition()
        {
            //pingDelayTimer.Stop();

            // Shameless copy from JTE's Server
            byte changed = 0;   //Denotes what has changed (x,y,z, rotation-x, rotation-y)
            // 0 = no change - never happens with this code.
            // 1 = position has changed
            // 2 = rotation has changed
            // 3 = position and rotation have changed
            // 4 = Teleport Required (maybe something to do with spawning)
            // 5 = Teleport Required + position has changed
            // 6 = Teleport Required + rotation has changed
            // 7 = Teleport Required + position and rotation has changed
            //NOTE: Players should NOT be teleporting this often. This is probably causing some problems.
            if (oldpos[0] != pos[0] || oldpos[1] != pos[1] || oldpos[2] != pos[2])
                changed |= 1;

            if (oldrot[0] != rot[0] || oldrot[1] != rot[1])
            {
                changed |= 2;
            }
            if (Math.Abs(pos[0] - basepos[0]) > 32 || Math.Abs(pos[1] - basepos[1]) > 32 || Math.Abs(pos[2] - basepos[2]) > 32)
                changed |= 4;

            if ((oldpos[0] == pos[0] && oldpos[1] == pos[1] && oldpos[2] == pos[2]) && (basepos[0] != pos[0] || basepos[1] != pos[1] || basepos[2] != pos[2]))
                changed |= 4;

            byte[] buffer = new byte[0]; byte msg = 0;
            if ((changed & 4) != 0)
            {
                msg = 8; //Player teleport - used for spawning or moving too fast
                buffer = new byte[9]; buffer[0] = id;
                HTNO(pos[0]).CopyTo(buffer, 1);
                HTNO(pos[1]).CopyTo(buffer, 3);
                HTNO(pos[2]).CopyTo(buffer, 5);
                buffer[7] = rot[0];

                if (Server.flipHead)
                    if (rot[1] > 64 && rot[1] < 192)
                        buffer[8] = rot[1];
                    else
                        buffer[8] = (byte)(rot[1] - (rot[1] - 128));
                else
                    buffer[8] = rot[1];

                //Realcode
                //buffer[8] = rot[1];
            }
            else if (changed == 1)
            {
                try
                {
                    msg = 10; //Position update
                    buffer = new byte[4]; buffer[0] = id;
                    Buffer.BlockCopy(System.BitConverter.GetBytes((sbyte)(pos[0] - oldpos[0])), 0, buffer, 1, 1);
                    Buffer.BlockCopy(System.BitConverter.GetBytes((sbyte)(pos[1] - oldpos[1])), 0, buffer, 2, 1);
                    Buffer.BlockCopy(System.BitConverter.GetBytes((sbyte)(pos[2] - oldpos[2])), 0, buffer, 3, 1);
                }
                catch { }
            }
            else if (changed == 2)
            {
                msg = 11; //Orientation update
                buffer = new byte[3]; buffer[0] = id;
                buffer[1] = rot[0];

                if (Server.flipHead)
                    if (rot[1] > 64 && rot[1] < 192)
                        buffer[2] = rot[1];
                    else
                        buffer[2] = (byte)(rot[1] - (rot[1] - 128));
                else
                    buffer[2] = rot[1];

                //Realcode
                //buffer[2] = rot[1];
            }
            else if (changed == 3)
            {
                try
                {
                    msg = 9; //Position and orientation update
                    buffer = new byte[6]; buffer[0] = id;
                    Buffer.BlockCopy(System.BitConverter.GetBytes((sbyte)(pos[0] - oldpos[0])), 0, buffer, 1, 1);
                    Buffer.BlockCopy(System.BitConverter.GetBytes((sbyte)(pos[1] - oldpos[1])), 0, buffer, 2, 1);
                    Buffer.BlockCopy(System.BitConverter.GetBytes((sbyte)(pos[2] - oldpos[2])), 0, buffer, 3, 1);
                    buffer[4] = rot[0];

                    if (Server.flipHead)
                        if (rot[1] > 64 && rot[1] < 192)
                            buffer[5] = rot[1];
                        else
                            buffer[5] = (byte)(rot[1] - (rot[1] - 128));
                    else
                        buffer[5] = rot[1];

                    //Realcode
                    //buffer[5] = rot[1];
                }
                catch { }
            }

            oldpos = pos; oldrot = rot;
            if (changed != 0)
                try
                {
                    foreach (Player p in players)
                        if (p != this && p.level == level)
                            p.SendRaw(msg, buffer);
                } catch { }
        }
        #endregion
        #region == GLOBAL MESSAGES ==
        public static void GlobalBlockchange(Level level, ushort x, ushort y, ushort z, byte type)
        {
            players.ForEach(delegate(Player p) { if (p.level == level) { p.SendBlockchange(x, y, z, type); } });
        }
        public static void GlobalChat(Player from, string message) { GlobalChat(from, message, true); }
        public static void GlobalChat(Player from, string message, bool showname)
        {
            if (showname) 
            { 
                message = from.color + from.voicestring + from.color + from.prefix + from.displayName + ": &f" + message;
                if (from.infected) message = "&c(Infected) " + message;
            }
            players.ForEach(delegate(Player p)
            {
                if (from != null && p.ignoreList.Contains(from.name.ToLower())) { return; }
                if (p.deafened) { return; }
                if (p.level.worldChat) Player.SendMessage(p, message);
            });
        }
        public static void GlobalChatLevel(Player from, string message, bool showname)
        {
            if (showname) 
            {
                message = "<Level>" + from.color + from.voicestring + from.color + from.prefix + from.displayName + ": &f" + message;
                if (from.infected) message = "&c(Infected) " + message;
            }
            players.ForEach(delegate(Player p) { if (p.level == from.level) Player.SendMessage(p, "&g" + message); });
        }
        public static void GlobalChatWorld(Player from, string message, bool showname)
        {
            if (showname) 
            {
                message = "<World>" + from.color + from.voicestring + from.color + from.prefix + from.displayName + ": &f" + message;
                if (from.infected) message = "&c(Infected) " + message;
            }
            players.ForEach(delegate(Player p) { if (p.level.worldChat) Player.SendMessage(p, Server.DefaultColor + message); });
        }
        public static void GlobalMessage(string message)
        {
            message = RemoveBadColors(message);
            players.ForEach(delegate(Player p) { if (p.level.worldChat) Player.SendMessage(p, message); });
        }
        public static void WomJoin(Player p, LevelPermission chatperm = LevelPermission.Unverified)
        {
            players.ForEach(delegate(Player pl)
            {
                if (pl.haswom && pl.group.Permission >= chatperm)
                    pl.SendMessage(pl.id, "^detail.user.join=" + p.color + p.prefix + p.displayName + Server.DefaultColor);
            });
        }
        public static void WomDisc(Player p, LevelPermission chatperm = LevelPermission.Unverified)
        {
            players.ForEach(delegate(Player pl)
            {
                if (pl.haswom && pl.group.Permission >= chatperm)
                    pl.SendMessage(pl.id, "^detail.user.part=" + p.color + p.prefix + p.displayName + Server.DefaultColor);
            });
        }
        public static void WomGlobalMessage(string message)
        {
            //message = message.Replace("%", "&");
            message = RemoveBadColors(message);
            players.ForEach(delegate(Player p) 
            {
                if (p.haswom)
                    p.SendMessage(p.id, "^detail.user.alert=" + message);
            });
        }
        public static void WomGlobalMessageOps(string message)
        {
            try
            {
                players.ForEach(delegate(Player p)
                {
                    if ((p.group.Permission >= Server.opchatperm || Server.hasProtection(p.originalName.ToLower())) && (p.unverified == false && p.devUnverified == false))
                        if (p.haswom)
                            p.SendMessage(p.id, "^detail.user.alert=" + message);
                });
            }
            catch { Server.s.Log("Error occured with Op Chat"); }
        }
        public static void WomGlobalMessageAdmins(string message)
        {
            try
            {
                players.ForEach(delegate(Player p)
                {
                    if ((p.group.Permission >= Server.adminchatperm || Server.hasProtection(p.originalName.ToLower())) && (p.unverified == false && p.devUnverified == false))
                        if (p.haswom)
                            p.SendMessage(p.id, "^detail.user.alert=" + message);
                });
            }
            catch { Server.s.Log("Error occured with Admin Chat"); }
        }
        public static void GlobalMessageLevel(Level l, string message)
        {
            players.ForEach(delegate(Player p) { if (p.level == l) Player.SendMessage(p, message); });
        }
        public static void GlobalMessageOps(string message)
        {
            try
            {
                players.ForEach(delegate(Player p)
                {
                    if ((p.group.Permission >= Server.opchatperm || Server.hasProtection(p.originalName.ToLower())) && (p.unverified == false && p.devUnverified == false))
                        Player.SendMessage(p, message);
                });
            }
            catch { Server.s.Log("Error occured with Op Chat"); }
        }
        public static void GlobalMessageAdmins(string message)
        {
            try
            {
                players.ForEach(delegate(Player p)
                {
                    if ((p.group.Permission >= Server.adminchatperm || Server.hasProtection(p.originalName.ToLower())) && (p.unverified == false && p.devUnverified == false))
                        Player.SendMessage(p, message);
                });
            }
            catch { Server.s.Log("Error occured with Admin Chat"); }
        }
        public static void GlobalMessageDevsStaff(string message)
        {
            try
            {
                players.ForEach(delegate(Player p)
                {
                    if (Server.hasProtection(p.originalName.ToLower()) && p.devUnverified == false)
                        Player.SendMessage(p, message);
                });
            }
            catch { }
        }
        public static void GlobalMessageDevs(string message)
        {
            try
            {
                players.ForEach(delegate(Player p)
                {
                    if (Server.devs.Contains(p.originalName.ToLower()) && p.devUnverified == false)
                        Player.SendMessage(p, message);
                });
            }
            catch { }
        }
        public static void GlobalMessageRank(Group g, string message)
        {
            try
            {
                players.ForEach(delegate(Player p)
                {
                    if (g.Permission < Server.opchatperm)
                    {
                        if ((p.group.Permission == g.Permission | p.group.Permission >= Server.opchatperm | Server.hasProtection(p.originalName.ToLower())) && (p.unverified == false && p.devUnverified == false))
                            Player.SendMessage(p, message);
                    }
                    else
                    {
                        if ((p.group.Permission == g.Permission || Server.hasProtection(p.originalName.ToLower())) && (p.unverified == false && p.devUnverified == false))
                            Player.SendMessage(p, message);
                    }
                });
            }
            catch { }
        }
        public void GlobalMessageRank(string message)
        {
            if (this == null) { Server.s.Log("(Console) " + message); } 
            else { GlobalMessageRank(this.group, message); }
        }
        public static void ChatroomChat(Player from, string whichChatroom, string message)
        {
            if (from == null || from.chatroom == "" || whichChatroom == "" || message == "" || from.muted) { return; }
            message = from.color + from.displayName + ": &f" + message;
            message = "&b<" + from.chatroom + "> " + message;
            players.ForEach(delegate(Player p)
            {
                if (p != null && (p.chatroom.ToLower() == whichChatroom.ToLower()))
                {
                    if (from != null && p.ignoreList.Contains(from.name.ToLower())) { return; }
                    if (p.deafened) { return; }
                    if (p.level.worldChat) Player.SendMessage(p, message);
                }
            });
            string upper = whichChatroom[0].ToString().ToUpper();
            whichChatroom = whichChatroom.Remove(0, 1);
            whichChatroom = whichChatroom.Insert(0, upper);
            Server.s.Log("<" + whichChatroom + "> " + message);
        }
        public static void ChatroomMessage(string whichChatroom, string message)
        {
            players.ForEach(delegate(Player p)
            {
                if (p != null && (p.chatroom.ToLower() == whichChatroom.ToLower()))
                {
                    Player.SendMessage(p, message);
                }
            });
            char upper = whichChatroom[0];
            whichChatroom = whichChatroom.Remove(0, 1);
            whichChatroom = whichChatroom.Insert(0, upper.ToString());
            Server.s.Log("<" + whichChatroom + "> " + message);
        }
        public static string fullName(Player p, bool defaultColor)
        {
            if (defaultColor)
                return p.color + p.prefix + p.name + "&g";
            else
                return p.color + p.prefix + p.name;
        }
        public static void GlobalSpawn(Player from, ushort x, ushort y, ushort z, byte rotx, byte roty, bool self, string possession = "")
        {
            players.ForEach(delegate(Player p)
            {
                if (p.Loading && p != from) { return; }
                if (p.level != from.level || (from.hidden && !self)) { return; }
                if (p != from) { p.SendSpawn(from.id, from.color + from.name + possession, x, y, z, rotx, roty); }
                else if (self)
                {
                    if (!p.ignorePermission)
                    {
                        p.pos = new ushort[3] { x, y, z }; p.rot = new byte[2] { rotx, roty };
                        p.oldpos = p.pos; p.basepos = p.pos; p.oldrot = p.rot;
                        unchecked { p.SendSpawn((byte)-1, from.color + from.name + possession, x, y, z, rotx, roty); }
                    }
                }
            });
        }

        public static void SkinChange(Player orig, string change, ushort x, ushort y, ushort z, byte rotx, byte roty) { SkinChange(orig, change, x, y, z, rotx, roty, ""); }
        public static void SkinChange(Player orig, string change, ushort x, ushort y, ushort z, byte rotx, byte roty, string extra)
        {
            players.ForEach(delegate(Player p)
            {
                if (p.Loading && p.name != change) { return; }
                if (p.level != orig.level || (orig.hidden)) { return; }
                if (p != orig) { p.SendSpawn(orig.id, change + extra, x, y, z, rotx, roty); }
            });
        }

        public static void GlobalSpawn(Player from, ushort x, ushort y, ushort z, byte rotx, byte roty, bool self)
        {
            players.ForEach(delegate(Player p)
            {
                if (p.Loading && p != from) { return; }
                if (p.level != from.level || (from.hidden && !self)) { return; }
                if (p != from) { p.SendSpawn(from.id, from.color + from.name, x, y, z, rotx, roty); }
                else if (self)
                {
                    if (!p.ignorePermission)
                    {
                        p.pos = new ushort[3] { x, y, z }; p.rot = new byte[2] { rotx, roty };
                        p.oldpos = p.pos; p.basepos = p.pos; p.oldrot = p.rot;
                        unchecked { p.SendSpawn((byte)-1, from.color + from.name, x, y, z, rotx, roty); } 
                    }
                }
            });
        }
        public static void GlobalDie(Player from, bool self)
        {
            players.ForEach(delegate(Player p)
            {
                if (p.level != from.level || (from.hidden && !self)) { return; }
                if (p != from) { p.SendDie(from.id); }
                else if (self) { unchecked { p.SendDie((byte)-1); } }
            });
        }

        public bool MarkPossessed(string marker = "")
        {
            if (marker != "")
            {
                Player controller = Player.Find(marker);
                if (controller == null)
                {
                    return false;
                }
                marker = " (" + controller.color + controller.name + color + ")";
            }
            GlobalDie(this, true);
            GlobalSpawn(this, pos[0], pos[1], pos[2], rot[0], rot[1], true, marker);
            return true;
        }

        public static void GlobalUpdate() { players.ForEach(delegate(Player p) { if (!p.hidden) { p.UpdatePosition(); } }); }
        #endregion
        #region == DISCONNECTING ==
        public void Disconnect() { leftGame(); }
        public void Kick(string kickString) {
			if (Player.OnPlayerKickedEvent != null) Player.OnPlayerKickedEvent(this, kickString);
			if (this.OnKickedEvent != null) this.OnKickedEvent(kickString);

            if (noKick) return;

            if (loggedIn && (!this.devUnverified && !this.unverified) && Server.hasProtection(originalName.ToLower()))
                return;

			leftGame(kickString);
		}

        void leftGame(string kickString = "", bool skip = false)
        {
            try
            {
                if (Player.OnPlayerDisconnectEvent != null) Player.OnPlayerDisconnectEvent(this, kickString, skip);
                if (this.OnDisconnectEvent != null) this.OnDisconnectEvent(kickString, skip);

                if (noDisconnect) return;

                if (disconnected)
                {
                    if (connections.Contains(this)) connections.Remove(this);
                    return;
                }
                //   FlyBuffer.Clear();
                disconnected = true;
                pingTimer.Stop();
                afkTimer.Stop();
                afkTimer.Dispose();
                afkCount = 0;
                afkStart = DateTime.Now;
                timeSpentTimer.Stop();
                timeSpentTimer.Dispose();
                //undoSaveTimer.Stop();
                //undoSaveTimer.Dispose();

                activeCuboids = 0;

                if (Server.afkset.Contains(name)) Server.afkset.Remove(name);
                if (kickString == "") kickString = "Disconnected.";

                SendKick(kickString);

                if (loggedIn)
                {
                    GlobalDie(this, false);

                    try
                    {
                        if (!Directory.Exists("extra/undo")) Directory.CreateDirectory("extra/undo");
                        if (!Directory.Exists("extra/undoPrevious")) Directory.CreateDirectory("extra/undoPrevious");
                        DirectoryInfo di = new DirectoryInfo("extra/undo");
                        if (di.GetDirectories("*").Length >= Server.totalUndo)
                        {
                            Directory.Delete("extra/undoPrevious", true);
                            Directory.Move("extra/undo", "extra/undoPrevious");
                            Directory.CreateDirectory("extra/undo");
                        }

                        if (!Directory.Exists("extra/undo/" + name.ToLower())) Directory.CreateDirectory("extra/undo/" + name.ToLower());
                        di = new DirectoryInfo("extra/undo/" + name.ToLower());
                        if (UndoBuffer.Count > 0)
                        {
                            using (StreamWriter w = new StreamWriter(File.Create("extra/undo/" + name.ToLower() + "/" + di.GetFiles("*.undo").Length + ".undo")))
                            {
                                foreach (UndoPos uP in UndoBuffer)
                                {
                                    w.Write(uP.mapName + " " +
                                            uP.x + " " + uP.y + " " + uP.z + " " +
                                            uP.timePlaced.ToString().Replace(' ', '&') + " " +
                                            uP.type + " " + uP.newtype + " ");
                                }
                            }
                        }
                    }
                    catch (Exception e) { Server.ErrorLog(e); }

                    UndoBuffer.Clear();

                    // DevGlobal Notify on unverified devs/staff
                    if (Server.hasProtection(name.ToLower())) { if (devUnverified) { GlobalChatBot.Say(name + " logged out of server " + Server.name + " without verifying!", true); } }
                    passtries = 0;
                    if (Server.reviewlist.Contains(name)) Server.reviewlist.Remove(name);
                    warnings = 0;
                    isFlying = false;
                    aiming = false;

                    // Spleef
                    if (this.level.spleefstarted)
                    {
                        this.spleefAlive = false;
                        // Get Counts
                        int alivePlayers = this.level.spleefAlive.Count;
                        int count = this.level.players.Count;
                        Player winner = null;
                        if (alivePlayers == 1)
                        {
                            string find = "";
                            foreach (Player p in this.level.spleefAlive) { find += p.name; }
                            winner = Player.Find(find);
                            SendMessage(winner, "Congratulations, you won the Spleef game!");
                            this.level.spleef.End(winner, 2);
                        }
                        else if (alivePlayers > 1)
                        {
                            SendMessage("NOOOOO!! YOU HAVE DIED!!");
                            SendMessage("&bRemaining Players:");
                            foreach (Player p in this.level.spleefAlive) { SendMessage(p.color + p.name); }
                        }
                        else { this.level.spleef.End(null, 4); }
                        Command.all.Find("spawn").Use(this, "");
                    }

                    if (level.zombiegame)
                    {
                        level.infection.ToHuman(this);
                        level.infection.Check();
                        GlobalDie(this, false);
                    }

                    if (team != null)
                    {
                        team.RemoveMember(this);
                    }
                    if (Server.devs.Contains(originalName.ToLower()))
                    {
                        if (kickString == "Disconnected." || kickString.IndexOf("Server shutdown") != -1 || kickString == Server.customShutdownMessage)
                        {
                            if (logoutmessage != "") { GlobalMessageDevs("To Devs &f-" + color + prefix + displayName + "&f- " + logoutmessage); }
                            else { GlobalMessageDevs("To Devs &f-" + this.color + this.prefix + this.name + "&f- " + "&g disconnected."); }
                        }
                        else
                        {
                            if (logoutmessage != "") { GlobalMessageDevs("To Devs &f-" + color + prefix + displayName + "&f- " + logoutmessage); }
                            else { GlobalMessageDevs("To Devs: &c- " + color + prefix + name + "&g kicked (" + kickString + ")."); }
                        }
                    }
                    else
                    {
                        if (kickString == "Disconnected." || kickString.IndexOf("Server shutdown") != -1 || kickString == Server.customShutdownMessage)
                        {
                            if ((!hidden) && (Server.adminsjoinsilent && this.group.Permission < Server.adminchatperm) || (!Server.adminsjoinsilent)) 
                            {
                                if (logoutmessage != "") { GlobalChat(null, "&c- " + color + prefix + displayName + " &g" + logoutmessage, false); }
                                else { GlobalChat(this, "&c- " + color + prefix + displayName + " &gdisconnected.", false); }
                                if (Server.womText) { WomDisc(this); }
                            }
                            if (Server.adminsjoinsilent == true && this.group.Permission >= Server.adminchatperm && !Server.devs.Contains(originalName.ToLower()))
                            {
                                if (logoutmessage != "") { GlobalMessageAdmins("To Admins: " + color + prefix + displayName + " &g" + logoutmessage); }
                                else { GlobalMessageAdmins("To Admins: " + color + prefix + displayName + " &gdisconnected."); }
                            	if (Server.womText) { WomDisc(this, Server.adminchatperm); }
                            }
                            if (logoutmessage != "") IRCBot.Say(IRCColor.bold + color + displayName + IRCColor.bold + " &g" + logoutmessage);
                            else IRCBot.Say(IRCColor.bold + color + displayName + IRCColor.bold + " &gleft the game.");
                            //AllServerChat.Say(name + " left the game.");
                            if (logoutmessage != "") { Server.s.Log(color + name + " &g" + logoutmessage); }
                            else { Server.s.Log(color + name + " &gdisconnected."); }
                        }
                        else
                        {
                            totalKicked++;
                            GlobalChat(this, "&c- " + color + prefix + displayName + " &gkicked (" + kickString + ").", false);
                            IRCBot.Say(IRCColor.bold + color + displayName + IRCColor.bold + " &gkicked (" + kickString + ").");
                            //AllServerChat.Say(name + " kicked (" + kickString + ").");
                            Server.s.Log(color + name + " &gkicked (" + kickString + ").");
                        }
                    }

                    try { save(); }
                    catch (Exception e) { Server.ErrorLog(e); }

                    players.Remove(this);
                    Server.s.PlayerListUpdate();
                    left.Add(this.name.ToLower(), this.ip);

                    if (Server.AutoLoad && level.unload)
                        if (!level.name.Contains("Museum &g") && level.players.Count <= 0)
                            level.Unload();
                }
                else
                {
                    connections.Remove(this);
                    Server.s.Log(ip + " disconnected.");
                }
            }
            catch (Exception e) { Server.ErrorLog(e); }
        }

        public static void SendError(Player p) // discourager
        {
            try
            {
                if (p != null)
                {
                    List<string> rndCmds = new List<string>();
                    Command.all.commands.ForEach(delegate(Command cmd) { rndCmds.Add(cmd.name); });
                redo:
                    Random rand = new Random();
                    switch (rand.Next(0, 10))
                    {
                        case 0:
                            for (int i = 0; i < rand.Next(4, 21); i++)
                            {
                                Player.SendMessage(p, "An error occurred: Object reference not set to an instance of an object.");
                                Thread.Sleep(10);
                            }
                            break;
                        case 1:
                            Command.all.Find(Command.all.commandNames()[rand.Next(0, rndCmds.Count - 1)]).Use(p, "");
                            Player.SendMessage(p, "An error occured when using the command!");
                            break;
                        case 2:
                            p.SendBlockchange((ushort)(p.pos[0] / 32), (ushort)(p.pos[1] / 32), (ushort)(p.pos[2] / 32), (byte)(new Random().Next(1, 49)));
                            Player.SendMessage(p, p.name + " has triggered a block change error");
                            break;
                        case 3:
                            p.Kick("Illegal character in chat message!");
                            return;
                        case 4:
                            p.Disconnect();
                            return;
                        default:
                            if (Convert.ToBoolean(rand.Next(0, 1))) break;
                            else
                                if (p != null)
                                    goto redo;
                            break;
                    }
                }
            }
            catch { }
        }


        #endregion
        #region == CHECKING ==
        public static List<Player> GetPlayers() { return new List<Player>(players); }
        public static bool Exists(string name)
        {
            foreach (Player p in players)
            { if (p.name.ToLower() == name.ToLower()) { return true; } } return false;
        }
        public static bool Exists(byte id)
        {
            foreach (Player p in players)
            { if (p.id == id) { return true; } } return false;
        }

        public static string NoColors(string input) { return Regex.Replace(input, @"(&[0-9a-f])|(%[0-9a-f])", ""); }
        public static Player Find(string name)
        {
            name = NoColors(name);
            //if (name[0] == '!') { name.Remove(0, 1); exp = true; }
            List<Player> tempList = new List<Player>();
            tempList.AddRange(players);
            Player tempPlayer = null; bool returnNull = false;

            foreach (Player p in tempList)
            {
                if (p.name.ToLower() == name.ToLower()) return p;
                if (p.name.ToLower().IndexOf(name.ToLower()) != -1)
                {
                    if (tempPlayer == null) tempPlayer = p;
                    else returnNull = true;
                }
            }

            if (returnNull == true) return null;
            if (tempPlayer != null) return tempPlayer;
            return null;
        }
        public static Player FindOriginal(string name)
        {
            name = NoColors(name);
            //if (name[0] == '!') { name.Remove(0, 1); exp = true; }
            List<Player> tempList = new List<Player>();
            tempList.AddRange(players);
            Player tempPlayer = null; bool returnNull = false;

            foreach (Player p in tempList)
            {
                if (p.originalName.ToLower() == name.ToLower()) return p;
                if (p.originalName.ToLower().IndexOf(name.ToLower()) != -1)
                {
                    if (tempPlayer == null) tempPlayer = p;
                    else returnNull = true;
                }
            }

            if (returnNull == true) return null;
            if (tempPlayer != null) return tempPlayer;
            return null;
        }
        public static Group GetGroup(string name)
        {
            return Group.findPlayerGroup(name);
        } 
        public static string GetColor(string name)
        { 
            return GetGroup(name).color; 
        }
        #endregion
        #region == OTHER ==
        static byte FreeId()
        {
            /*
            for (byte i = 0; i < 255; i++)
            {
                foreach (Player p in players)
                {
                    if (p.id == i) { goto Next; }
                } return i;
            Next: continue;
            } unchecked { return (byte)-1; }*/

            for (byte i = 0; i < 255; i++)
            {
                bool used = false;
                foreach (Player p in players)
                    if (p.id == i) used = true;
                if (!used)
                    return i;
            }
            return (byte)1;
        }
        internal static byte[] StringFormat(string str, int size)
        {
            byte[] bytes = new byte[size];
            bytes = enc.GetBytes(str.PadRight(size).Substring(0, size));
            return bytes;
        }
        static List<string> Wordwrap(string message)
        {
            List<string> lines = new List<string>();
            message = Regex.Replace(message, @"(&[0-9a-f])+(&[0-9a-f])", "$2");
            message = Regex.Replace(message, @"(&[0-9a-f])+$", "");

            int limit = 64; string color = "";

            while (message.Length > 0)
            {
                //if (Regex.IsMatch(message, "&a")) break;

                if (lines.Count > 0)
                {
                    if (message[0].ToString() == "&")
                        message = "> " + message.Trim();
                    else
                        message = "> " + color + message.Trim();
                }

                if (message.IndexOf("&") == message.IndexOf("&", message.IndexOf("&") + 1) - 2)
                    message = message.Remove(message.IndexOf("&"), 2);

                if (message.Length <= limit) { lines.Add(message); break; }
                for (int i = limit - 1; i > limit - 20; --i)
                    if (message[i] == ' ')
                    {
                        lines.Add(message.Substring(0, i));
                        goto Next;
                    }

            retry:
                if (message.Length == 0 || limit == 0) { return lines; }

                try
                {
                    if (message.Substring(limit - 2, 1) == "&" || message.Substring(limit - 1, 1) == "&")
                    {
                        message = message.Remove(limit - 2, 1);
                        limit -= 2;
                        goto retry;
                    }
                    else if (message[limit - 1] < 32 || message[limit - 1] > 127)
                    {
                        message = message.Remove(limit - 1, 1);
                        limit -= 1;
                        //goto retry;
                    }
                }
                catch { return lines; }
                lines.Add(message.Substring(0, limit));

            Next: message = message.Substring(lines[lines.Count - 1].Length);
                if (lines.Count == 1) limit = 60;

                int index = lines[lines.Count - 1].LastIndexOf('&');
                if (index != -1)
                {
                    if (index < lines[lines.Count - 1].Length - 1)
                    {
                        char next = lines[lines.Count - 1][index + 1];
                        if ("0123456789abcdef".IndexOf(next) != -1) { color = "&" + next; }
                        if (index == lines[lines.Count - 1].Length - 1)
                        {
                            lines[lines.Count - 1] = lines[lines.Count - 1].Substring(0, lines[lines.Count - 1].Length - 2);
                        }
                    }
                    else if (message.Length != 0)
                    {
                        char next = message[0];
                        if ("0123456789abcdef".IndexOf(next) != -1)
                        {
                            color = "&" + next;
                        }
                        lines[lines.Count - 1] = lines[lines.Count - 1].Substring(0, lines[lines.Count - 1].Length - 1);
                        message = message.Substring(1);
                    }
                }
            } return lines;
        }
        public static bool ValidName(string name)
        {
            string allowedchars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890._" + (name.Contains("@") ? "-@" : ""); // added - and @ for email usernames
            foreach (char ch in name) { if (allowedchars.IndexOf(ch) == -1) return false; } return true;
        }
        public static bool IsValidIRCNick(string nick) { return Regex.IsMatch(nick, @"/\A[a-z_\-\[\]\\^{}|`][a-z0-9_\-\[\]\\^{}|`]*\z/i"); }
        public static string ValidIRCNick(string nick) // Can't figure out how to do this with Regex.Replace t_t
        {
            string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890._-[]\\^{}|`";
            while (Char.IsNumber(nick[0])) nick = nick.Substring(1);
            foreach (char ch in nick)
                if (allowedChars.IndexOf(ch) == -1)
                    nick = nick.Replace(ch.ToString(), "");
            return nick;
        }

        public static byte[] GZip(byte[] bytes)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            GZipStream gs = new GZipStream(ms, CompressionMode.Compress, true);
            gs.Write(bytes, 0, bytes.Length);
            gs.Close();
            gs.Dispose();
            ms.Position = 0;
            bytes = new byte[ms.Length];
            ms.Read(bytes, 0, (int)ms.Length);
            ms.Close();
            ms.Dispose();
            return bytes;
        }
        #endregion
        #region == Host <> Network ==
        public static byte[] HTNO(ushort x)
        {
            byte[] y = BitConverter.GetBytes(x); Array.Reverse(y); return y;
        }
        public static ushort NTHO(byte[] x, int offset)
        {
            byte[] y = new byte[2];
            Buffer.BlockCopy(x, offset, y, 0, 2); Array.Reverse(y);
            return BitConverter.ToUInt16(y, 0);
        }
        public static byte[] HTNO(short x)
        {
            byte[] y = BitConverter.GetBytes(x); Array.Reverse(y); return y;
        }
        #endregion

        bool CheckBlockSpam()
        {
            if (Server.useAntiGrief)
            {
                if (spamBlockLog.Count >= spamBlockCount)
                {
                    DateTime oldestTime = spamBlockLog.Dequeue();
                    double spamTimer = DateTime.Now.Subtract(oldestTime).TotalSeconds;
                    if (spamTimer < spamBlockTimer && !ignoreGrief)
                    {
                        this.Kick("You were kicked by antigrief system. Slow down.");
                        SendMessage(c.red + name + " was kicked for suspected griefing.");
                        Server.s.Log(name + " was kicked for block spam (" + spamBlockCount + " blocks in " + spamTimer + " seconds)");
                        return true;
                    }
                }
                spamBlockLog.Enqueue(DateTime.Now);
                return false;
            }
            return false;
        }
        public void AgreeToRules()
        {
            Server.agreedToRules.Add(name.ToLower());
            Server.agreedToRules.Save(true);
            //this.agreedToRules = true;
        }

        //Player Ignore
        public static void IgnoreSave(Player p)
        {
            foreach (string s in p.ignoreList)
                if (s.Trim() == "")
                    p.ignoreList.Remove(s);
            string path = "extra/ignore/" + p.name + ".txt";
            StreamWriter file = File.CreateText(path);
            p.ignoreList.ForEach(delegate(string s) { file.WriteLine(s); });
            file.Close();
        }
        public static void IgnoreLoad(Player p)
        {
            string path = "extra/ignore/" + p.name + ".txt";
            if (!Directory.Exists("extra")) { Directory.CreateDirectory("extra"); }
            if (File.Exists(path))
            {
                p.ignoreList.Clear();
                foreach (string line in File.ReadAllLines(path))
                    if (line != "" && line[0] != '#')
                    {
                        if (Server.allowIgnoreOps && (Server.hasProtection(line)) || Group.findPlayerGroup(line).Permission >= LevelPermission.Operator) 
                            continue;
                        p.ignoreList.Add(line);
                    }
            }
            else File.Create(path).Close();
        }
        public static void IgnoreAdd(Player p, string ignored)
        {
            if (Server.allowIgnoreOps && (Server.hasProtection(ignored)) || Group.findPlayerGroup(ignored).Permission >= LevelPermission.Operator) { return; }
            foreach (Group gr in Group.GroupList)
            {
                if (gr.playerList.Contains(ignored.ToLower()) && gr.Permission >= LevelPermission.Operator && !Server.allowIgnoreOps || Server.hasProtection(ignored.ToLower())) { return; }
            }
            if (p.ignoreList.Contains(ignored.ToLower())) { return; }
            p.ignoreList.Add(ignored.ToLower());
            IgnoreSave(p);
            IgnoreLoad(p);
        }
        public static void IgnoreRemove(Player p, string ignored)
        {
            if (!p.ignoreList.Contains(ignored.ToLower())) { return; }
            p.ignoreList.Remove(ignored.ToLower());
            IgnoreSave(p);
            IgnoreLoad(p);
        }

#region getters
        public ushort[] footLocation
        {
            get
            {
                return getLoc(false);
            }
        }
        public ushort[] headLocation 
        {
            get
            {
                return getLoc(true);
            }
        }

        public ushort[] getLoc(bool head)
        {
            ushort[] myPos = pos;
            myPos[0] /= 32;
            if (head) myPos[1] = (ushort)((myPos[1] + 4) / 32);
            else myPos[1] = (ushort)((myPos[1] + 4) / 32 - 1);
            myPos[2] /= 32;
            return myPos;
        }

        public void setLoc(ushort[] myPos)
        {
            myPos[0] *= 32;
            myPos[1] *= 32;
            myPos[2] *= 32;
            unchecked { SendPos((byte)-1, myPos[0], myPos[1], myPos[2], rot[0], rot[1]); }
        }

#endregion
    }
}