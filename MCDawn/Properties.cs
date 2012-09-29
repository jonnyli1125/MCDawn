using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MCDawn
{
    public static class Properties
    {
        public static void Load(string givenPath, bool skipsalt = false)
        {
            if (!skipsalt)
            {
                RandomNumberGenerator prng = RandomNumberGenerator.Create();
                StringBuilder sb = new StringBuilder();
                byte[] oneChar = new byte[1];
                while (sb.Length < 16)
                {
                    prng.GetBytes(oneChar);
                    if (Char.IsLetterOrDigit((char)oneChar[0]))
                    {
                        sb.Append((char)oneChar[0]);
                    }
                }
                Server.salt = sb.ToString();
            }
            if (File.Exists(givenPath))
            {
                string[] lines = File.ReadAllLines(givenPath);

                foreach (string line in lines)
                {
                    if (line != "" && line[0] != '#')
                    {
                        //int index = line.IndexOf('=') + 1; // not needed if we use Split('=')
                        string key = line.Split('=')[0].Trim();
                        string value = "";
                        if (line.IndexOf('=') >= 0)
                            value = line.Substring(line.IndexOf('=') + 1).Trim();
                        string color = "";

                        switch (key.ToLower())
                        {
                            case "server-name":
                                if (ValidString(value, "![]:.,{}~-+()?_/\\ "))
                                {
                                    Server.name = value;
                                }
                                else { Server.s.Log("server-name invalid! setting to default."); }
                                break;
                            case "description":
                                if (ValidString(value, "![]:.,{}~-+()?_/\\ "))
                                {
                                    Server.description = value;
                                }
                                else { Server.s.Log("Description invalid! Setting to default."); }
                                break;
                            case "flags":
                                if (ValidString(value, "![]:.,{}~-+()?_/\\ "))
                                {
                                    Server.flags = value;
                                }
                                else { Server.s.Log("Flags invalid! Setting to default."); }
                                break;
                            case "motd":
                                if (ValidString(value, "=![]&:.,{}~-+()?_/\\' "))
                                {
                                    Server.motd = value;
                                }
                                else { Server.s.Log("motd invalid! setting to default."); }
                                break;
                            case "port":
                                try { Server.port = Convert.ToInt32(value); }
                                catch { Server.s.Log("port invalid! setting to default."); }
                                break;
                            case "use-upnp":
                                Server.upnp = (value.ToLower() == "true") ? true : false;
                                break;
                            case "verify-names":
                                Server.verify = (value.ToLower() == "true") ? true : false;
                                break;
                            case "allowproxy":
                                Server.allowproxy = (value.ToLower() == "true") ? true : false;
                                break;
                            case "public":
                                Server.pub = (value.ToLower() == "true") ? true : false;
                                break;
                            case "world-chat":
                                Server.worldChat = (value.ToLower() == "true") ? true : false;
                                break;
                            case "guest-goto":
                                Server.guestGoto = (value.ToLower() == "true") ? true : false;
                                break;
                            case "max-players":
                                try { Server.players = Convert.ToInt32(value); }
                                catch { Server.s.Log("max-players invalid! setting to default."); }
                                break;
                            case "max-guests":
                                try { Server.maxguests = Convert.ToInt32(value); }
                                catch { Server.s.Log("max-guests invalid! setting to default."); }
                                break;
                            case "max-maps":
                                try
                                {
                                    if (Convert.ToByte(value) > 100)
                                    {
                                        value = "100";
                                        Server.s.Log("Max maps has been lowered to 100.");
                                    }
                                    else if (Convert.ToByte(value) < 1)
                                    {
                                        value = "1";
                                        Server.s.Log("Max maps has been increased to 1.");
                                    }
                                    Server.maps = Convert.ToByte(value);
                                }
                                catch
                                {
                                    Server.s.Log("max-maps invalid! setting to default.");
                                }
                                break;
                            case "irc":
                                Server.irc = (value.ToLower() == "true") ? true : false;
                                break;
                            case "irc-server":
                                Server.ircServer = value;
                                break;
                            case "irc-nick":
                                Server.ircNick = value;
                                break;
                            case "irc-channel":
                                Server.ircChannel = value;
                                break;
                            case "irc-opchannel":
                                Server.ircOpChannel = value;
                                break;
                            case "irc-port":
                                try
                                {
                                    Server.ircPort = Convert.ToInt32(value);
                                }
                                catch
                                {
                                    Server.s.Log("irc-port invalid! setting to default.");
                                }
                                break;
                            case "irc-identify":
                                try
                                {
                                    Server.ircIdentify = Convert.ToBoolean(value);
                                }
                                catch
                                {
                                    Server.s.Log("irc-identify boolean value invalid! Setting to the default of: " + Server.ircIdentify + ".");
                                }
                                break;
                            case "irc-password":
                                Server.ircPassword = value;
                                break;
                            case "anti-tunnels":
                                Server.antiTunnel = (value.ToLower() == "true") ? true : false;
                                break;
                            case "max-depth":
                                try
                                {
                                    Server.maxDepth = Convert.ToByte(value);
                                }
                                catch
                                {
                                    Server.s.Log("maxDepth invalid! setting to default.");
                                }
                                break;

                            case "rplimit":
                                try { Server.rpLimit = Convert.ToInt16(value); }
                                catch { Server.s.Log("rpLimit invalid! setting to default."); }
                                break;
                            case "rplimit-norm":
                                try { Server.rpNormLimit = Convert.ToInt16(value); }
                                catch { Server.s.Log("rpLimit-norm invalid! setting to default."); }
                                break;


                            case "report-back":
                                Server.reportBack = (value.ToLower() == "true") ? true : false;
                                break;
                            case "backup-time":
                                if (Convert.ToInt32(value) > 1) { Server.backupInterval = Convert.ToInt32(value); }
                                break;
                            case "backup-location":
                                if (!value.Contains("System.Windows.Forms.TextBox, Text:"))
                                    Server.backupLocation = value;
                                break;

                            case "console-only":
                                Server.console = (value.ToLower() == "true") ? true : false;
                                break;

                            case "physicsrestart":
                                Server.physicsRestart = (value.ToLower() == "true") ? true : false;
                                break;
                            case "deathcount":
                                Server.deathcount = (value.ToLower() == "true") ? true : false;
                                break;

                            case "usemysql":
                                Server.useMySQL = (value.ToLower() == "true") ? true : false;
                                break;
                            case "host":
                                Server.MySQLHost = value;
                                break;
                            case "sqlport":
                                Server.MySQLPort = value;
                                break;
                            case "username":
                                Server.MySQLUsername = value;
                                break;
                            case "password":
                                Server.MySQLPassword = value;
                                break;
                            case "databasename":
                                Server.MySQLDatabaseName = value;
                                break;
                            case "pooling":
                                try { Server.MySQLPooling = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "defaultcolor":
                                color = c.Parse(value);
                                if (color == "")
                                {
                                    color = c.Name(value); if (color != "") color = value; else { Server.s.Log("Could not find " + value); return; }
                                }
                                Server.DefaultColor = color;
                                break;
                            case "irc-color":
                                color = c.Parse(value);
                                if (color == "")
                                {
                                    color = c.Name(value); if (color != "") color = value; else { Server.s.Log("Could not find " + value); return; }
                                }
                                Server.IRCColour = color;
                                break;
                            case "old-help":
                                try { Server.oldHelp = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "opchat-perm":
                                try
                                {
                                    sbyte parsed = sbyte.Parse(value);
                                    if (parsed < -50 || parsed > 120)
                                    {
                                        throw new FormatException();
                                    }
                                    Server.opchatperm = (LevelPermission)parsed;
                                }
                                catch { Server.s.Log("Invalid " + key + ".  Using default."); break; }
                                break;
                            case "adminchat-perm":
                                try
                                {
                                    sbyte parsed = sbyte.Parse(value);
                                    if (parsed < -50 || parsed > 120)
                                    {
                                        throw new FormatException();
                                    }
                                    Server.adminchatperm = (LevelPermission)parsed;
                                }
                                catch { Server.s.Log("Invalid " + key + ".  Using default."); break; }
                                break;
                            case "log-heartbeat":
                                try { Server.logbeat = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ".  Using default."); break; }
                                break;
                            case "force-cuboid":
                                try { Server.forceCuboid = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ".  Using default."); break; }
                                break;
                            case "cheapmessage":
                                try { Server.cheapMessage = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "cheap-message-given":
                                if (value != "") Server.cheapMessageGiven = value;
                                break;
                            case "uncheapmessage":
                                try { Server.unCheapMessage = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "uncheap-message-given":
                                if (value != "") Server.unCheapMessageGiven = value;
                                break;
                            case "custom-ban":
                                try { Server.customBan = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "custom-ban-message":
                                if (value != "") Server.customBanMessage = value;
                                break;
                            case "custom-shutdown":
                                try { Server.customShutdown = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "custom-shutdown-message":
                                if (value != "") Server.customShutdownMessage = value;
                                break;
                            case "rank-super":
                                try { Server.rankSuper = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "default-rank":
                                try { Server.defaultRank = value.ToLower(); }
                                catch { }
                                break;
                            case "afk-minutes":
                                try
                                {
                                    Server.afkminutes = Convert.ToInt32(value);
                                }
                                catch
                                {
                                    Server.s.Log("irc-port invalid! setting to default.");
                                }
                                break;
                            case "afk-kick":
                                try { Server.afkkick = Convert.ToInt32(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); }
                                break;
                            case "check-updates":
                                try { Server.checkUpdates = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "autoload":
                                try { Server.AutoLoad = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "auto-restart":
                                try { Server.autorestart = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "restarttime":
                                try { Server.restarttime = DateTime.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using defualt."); break; }
                                break;
                            case "parse-emotes":
                                try { Server.parseSmiley = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "use-whitelist":
                                Server.useWhitelist = (value.ToLower() == "true") ? true : false;
                                break;
                            case "main-name":
                                if (Player.ValidName(value)) Server.level = value;
                                else Server.s.Log("Invalid main name");
                                break;
                            case "dollar-before-dollar":
                                try { Server.dollardollardollar = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); }
                                break;
                            case "money-name":
                                if (value != "") Server.moneys = value;
                                break;
                            case "mono":
                                try { Server.mono = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); }
                                break;
                            case "restart-on-error":
                                try { Server.restartOnError = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); }
                                break;
                            case "repeat-messages":
                                try { Server.repeatMessage = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); }
                                break;
                            case "host-state":
                                if (value != "")
                                    Server.ZallState = value;
                                break;
                            /*case "salt":
                                try { Server.salt = value; }
                                catch { }
                                break;*/
                            case "antispam":
                                try { Server.antiSpam = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "antispamop":
                                try { Server.antiSpamOp = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "antispamstyle":
                                Server.antiSpamStyle = value;
                                break;
                            case "msgsrequired":
                                try
                                {
                                    Server.spamCounter = Convert.ToInt32(value);
                                }
                                catch
                                {
                                    Server.s.Log("Messages Required invalid! Setting to default.");
                                }
                                break;
                            case "anticaps":
                                try { Server.antiCaps = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "anticapsop":
                                try { Server.antiCapsOp = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "anticapsstyle":
                                Server.antiCapsStyle = value;
                                break;
                            case "capsrequired":
                                try
                                {
                                    Server.capsRequired = Convert.ToInt32(value);
                                }
                                catch
                                {
                                    Server.s.Log("Caps Required invalid! Setting to default.");
                                }
                                break;
                            case "useglobal":
                                try { Server.useglobal = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "globalnick":
                                if (value != "") Server.globalNick = validNick(value);
                                break;
                            case "global-identify":
                                try { Server.globalIdentify = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "global-password":
                                if (value != "") Server.globalPassword = value;
                                break;
                            case "adminsecurity":
                                try { Server.adminsecurity = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "adminsecurityrank":
                                try
                                {
                                    sbyte parsed = sbyte.Parse(value);
                                    if (parsed < -50 || parsed > 120)
                                    {
                                        throw new FormatException();
                                    }
                                    Server.adminsecurityrank = (LevelPermission)parsed;
                                }
                                catch { Server.s.Log("Invalid " + key + ".  Using default."); break; }
                                break;
                            case "join-on-maintenence":
                                try
                                {
                                    sbyte parsed = sbyte.Parse(value);
                                    if (parsed < -50 || parsed > 120)
                                    {
                                        throw new FormatException();
                                    }
                                    Server.canjoinmaint = (LevelPermission)parsed;
                                }
                                catch { Server.s.Log("Invalid " + key + ".  Using default."); break; }
                                break;
                            case "global-color":
                                color = c.Parse(value);
                                if (color == "")
                                {
                                    color = c.Name(value); if (color != "") color = value; else { Server.s.Log("Could not find " + value); return; }
                                }
                                Server.GlobalChatColour = color;
                                break;
                            case "adminsjoinsilent":
                                try { Server.adminsjoinsilent = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "adminsjoinhidden":
                                try { Server.adminsjoinhidden = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "countryonjoin":
                                Server.useMaxMind = (value.ToLower() == "true") ? true : false;
                                break;
                            case "agreetorules":
                                Server.agreeToRules = (value.ToLower() == "true") ? true : false;
                                break;
                            case "agreepass":
                                Server.agreePass = value;
                                break;
                            case "consolesound":
                                try { Server.consoleSound = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "home-perm":
                                try
                                {
                                    sbyte parsed = sbyte.Parse(value);
                                    if (parsed < -50 || parsed > 120)
                                    {
                                        throw new FormatException();
                                    }
                                    Server.HomeRank = (LevelPermission)parsed;
                                }
                                catch { Server.s.Log("Invalid " + key + ".  Using default."); break; }
                                break;
                            case "homeprefix":
                                Server.HomePrefix = value;
                                break;
                            case "home-x":
                                try
                                {
                                    Server.HomeX = Convert.ToInt32(value);
                                }
                                catch
                                {
                                    Server.s.Log("Home X invalid! Setting to default.");
                                }
                                break;
                            case "home-y":
                                try
                                {
                                    Server.HomeY = Convert.ToInt32(value);
                                }
                                catch
                                {
                                    Server.s.Log("Home Y invalid! Setting to default.");
                                }
                                break;
                            case "home-z":
                                try
                                {
                                    Server.HomeZ = Convert.ToInt32(value);
                                }
                                catch
                                {
                                    Server.s.Log("Home Z invalid! Setting to default.");
                                }
                                break;
                            case "show-attempted-logins":
                                try { Server.showAttemptedLogins = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "profanityfilter":
                                try { Server.profanityFilter = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "warnplayer":
                                try { Server.swearWarnPlayer = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "swearwordsrequired":
                                try
                                {
                                    Server.swearWordsRequired = Convert.ToInt32(value);
                                }
                                catch
                                {
                                    Server.s.Log("Swear words Required invalid! Setting to default.");
                                }
                                break;
                            case "apply-to-op":
                                try { Server.profanityFilterOp = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "profanityfilterstyle":
                                Server.profanityFilterStyle = value;
                                break;
                            case "useantigrief":
                                try { Server.useAntiGrief = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "allow-ignore-ops":
                                try { Server.allowIgnoreOps = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "use-timerank":
                                try { Server.useTimeRank = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "timerank-cmd":
                                Server.timeRankCommand = value;
                                break;
                            case "use-wom":
                                try { Server.useWOM = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "wom-text":
                                try { Server.womText = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "use-discourager":
                                try { Server.useDiscourager = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "use-remote":
                                try { Server.useRemote = bool.Parse(value); }
                                catch { Server.s.Log("Invalid " + key + ". Using default."); break; }
                                break;
                            case "rc-port":
                                try { RemoteServer.port = Convert.ToUInt16(value); }
                                catch { Server.s.Log("rc-port invalid! setting to default."); }
                                break;
                            case "rc-pass":
                                if (value != "") { RemoteServer.rcpass = value; }
                                else
                                {
                                    RemoteServer.rcpass = "";
                                    string rndchars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                                    Random rnd = new Random();
                                    for (int i = 0; i < 8; ++i) { RemoteServer.rcpass += rndchars[rnd.Next(rndchars.Length)]; }
                                }
                                break;
                            case "throttle":
                                try { if (Server.throttle <= 10 && (Server.Version == "1.0.1.4" || Server.Version == "1.0.1.3")) Server.throttle = 200; else Server.throttle = int.Parse(value); } // o_o... dun wan dem to stay on throttle 10 lolz :/
                                catch { Server.s.Log("throttle invalid! setting to default."); }
                                break;
                            case "usewompasswords":
                                try { Server.useWOMPasswords = bool.Parse(value); }
                                catch { Server.s.Log("usewompasswords invalid! setting to default."); }
                                break;
                            case "womipaddress":
                                if (value != "") Server.WOMIPAddress = value;
                                else Server.WOMIPAddress = Server.GetIPAddress();
                                break;
                        }
                    }
                }
                Server.s.SettingsUpdate();
                Save(givenPath);
            }
            else Save(givenPath);
        }
        public static bool ValidString(string str, string allowed)
        {
            string allowedchars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890" + allowed;
            foreach (char ch in str)
            {
                if (allowedchars.IndexOf(ch) == -1)
                {
                    return false;
                }
            } return true;
        }

        public static void Save(string givenPath)
        {
            try
            {
                StreamWriter w = new StreamWriter(File.Create(givenPath));
                if (givenPath.IndexOf("server") != -1)
                {
                    w.WriteLine("# Edit the settings below to modify how your server operates. This is an explanation of what each setting does.");
                    w.WriteLine("#   server-name\t=\tThe name which displays on minecraft.net");
                    w.WriteLine("#   motd\t=\tThe message which displays when a player connects");
                    w.WriteLine("#   port\t=\tThe port to operate from");
                    w.WriteLine("#   console-only\t=\tRun without a GUI (useful for Linux servers with mono)");
                    w.WriteLine("#   verify-names\t=\tVerify the validity of names");
                    w.WriteLine("#   public\t=\tSet to true to appear in the public server list");
                    w.WriteLine("#   max-players\t=\tThe maximum number of connections");
                    w.WriteLine("#   max-maps\t=\tThe maximum number of maps loaded at once");
                    w.WriteLine("#   world-chat\t=\tSet to true to enable world chat");
                    w.WriteLine("#   guest-goto\t=\tSet to true to give guests goto and levels commands");
                    w.WriteLine("#   irc\t=\tSet to true to enable the IRC bot");
                    w.WriteLine("#   irc-nick\t=\tThe name of the IRC bot");
                    w.WriteLine("#   irc-server\t=\tThe server to connect to");
                    w.WriteLine("#   irc-channel\t=\tThe channel to join");
                    w.WriteLine("#   irc-opchannel\t=\tThe channel to join (posts OpChat)");
                    w.WriteLine("#   irc-port\t=\tThe port to use to connect");
                    w.WriteLine("#   irc-identify\t=(true/false)\tDo you want the IRC bot to Identify itself with nickserv. Note: You will need to register it's name with nickserv manually.");
                    w.WriteLine("#   irc-password\t=\tThe password you want to use if you're identifying with nickserv");
                    w.WriteLine("#   anti-tunnels\t=\tStops people digging below max-depth");
                    w.WriteLine("#   max-depth\t=\tThe maximum allowed depth to dig down");
                    w.WriteLine("#   backup-time\t=\tThe number of seconds between automatic backups");
                    w.WriteLine("#   overload\t=\tThe higher this is, the longer the physics is allowed to lag. Default 1500");
                    w.WriteLine("#   use-whitelist\t=\tSwitch to allow use of a whitelist to override IP bans for certain players.  Default false.");
                    w.WriteLine("#   force-cuboid\t=\tRun cuboid until the limit is hit, instead of canceling the whole operation.  Default false.");
                    w.WriteLine();
                    w.WriteLine("#   Host\t=\tThe host name for the database (usually 127.0.0.1)");
                    w.WriteLine("#   SQLPort\t=\tPort number to be used for MySQL.  Unless you manually changed the port, leave this alone.  Default 3306.");
                    w.WriteLine("#   Username\t=\tThe username you used to create the database (usually root)");
                    w.WriteLine("#   Password\t=\tThe password set while making the database");
                    w.WriteLine("#   DatabaseName\t=\tThe name of the database stored (Default = MCDawn)");
                    w.WriteLine();
                    w.WriteLine("#   defaultColor\t=\tThe color code of the default messages (Default = &e)");
                    w.WriteLine();
                    w.WriteLine("#   Super-limit\t=\tThe limit for building commands for SuperOPs");
                    w.WriteLine("#   Op-limit\t=\tThe limit for building commands for Operators");
                    w.WriteLine("#   Adv-limit\t=\tThe limit for building commands for AdvBuilders");
                    w.WriteLine("#   Builder-limit\t=\tThe limit for building commands for Builders");
                    w.WriteLine();
                    w.WriteLine();
                    w.WriteLine("# Server options");
                    w.WriteLine("server-name = " + Server.name);
                    w.WriteLine("description = " + Server.description);
                    w.WriteLine("flags = " + Server.flags);
                    w.WriteLine("motd = " + Server.motd);
                    w.WriteLine("port = " + Server.port.ToString());
                    w.WriteLine("use-upnp = " + Server.upnp.ToString().ToLower());
                    w.WriteLine("verify-names = " + Server.verify.ToString().ToLower());
                    w.WriteLine("public = " + Server.pub.ToString().ToLower());
                    w.WriteLine("max-players = " + Server.players.ToString());
                    w.WriteLine("max-guests = " + Server.maxguests.ToString());
                    w.WriteLine("max-maps = " + Server.maps.ToString());
                    w.WriteLine("world-chat = " + Server.worldChat.ToString().ToLower());
                    w.WriteLine("check-updates = " + Server.checkUpdates.ToString().ToLower());
                    w.WriteLine("autoload = " + Server.AutoLoad.ToString().ToLower());
                    w.WriteLine("auto-restart = " + Server.autorestart.ToString().ToLower());
                    w.WriteLine("restarttime = " + Server.restarttime.ToShortTimeString());
                    w.WriteLine("restart-on-error = " + Server.restartOnError);
                    w.WriteLine("main-name = " + Server.level);
                    w.WriteLine("use-wom = " + Server.useWOM.ToString().ToLower());
                    w.WriteLine();
                    w.WriteLine("# irc bot options");
                    w.WriteLine("irc = " + Server.irc.ToString().ToLower());
                    w.WriteLine("irc-nick = " + validNick(Server.ircNick));
                    w.WriteLine("irc-server = " + Server.ircServer);
                    w.WriteLine("irc-channel = " + Server.ircChannel);
                    w.WriteLine("irc-opchannel = " + Server.ircOpChannel);
                    w.WriteLine("irc-port = " + Server.ircPort.ToString());
                    w.WriteLine("irc-identify = " + Server.ircIdentify.ToString());
                    w.WriteLine("irc-password = " + Server.ircPassword);
                    w.WriteLine();
                    w.WriteLine("# other options");
                    w.WriteLine("anti-tunnels = " + Server.antiTunnel.ToString().ToLower());
                    w.WriteLine("max-depth = " + Server.maxDepth.ToString().ToLower());
                    w.WriteLine("rplimit = " + Server.rpLimit.ToString().ToLower());
                    w.WriteLine("rplimit-norm = " + Server.rpNormLimit.ToString().ToLower());
                    w.WriteLine("physicsrestart = " + Server.physicsRestart.ToString().ToLower());
                    w.WriteLine("old-help = " + Server.oldHelp.ToString().ToLower());
                    w.WriteLine("deathcount = " + Server.deathcount.ToString().ToLower());
                    w.WriteLine("afk-minutes = " + Server.afkminutes.ToString());
                    w.WriteLine("afk-kick = " + Server.afkkick.ToString());
                    w.WriteLine("parse-emotes = " + Server.parseSmiley.ToString().ToLower());
                    w.WriteLine("dollar-before-dollar = " + Server.dollardollardollar.ToString().ToLower());
                    w.WriteLine("use-whitelist = " + Server.useWhitelist.ToString().ToLower());
                    w.WriteLine("money-name = " + Server.moneys);
                    w.WriteLine("opchat-perm = " + ((sbyte)Server.opchatperm).ToString());
                    w.WriteLine("adminchat-perm = " + ((sbyte)Server.adminchatperm).ToString());
                    w.WriteLine("log-heartbeat = " + Server.logbeat.ToString());
                    w.WriteLine("force-cuboid = " + Server.forceCuboid.ToString());
                    w.WriteLine("repeat-messages = " + Server.repeatMessage.ToString());
                    w.WriteLine("host-state = " + Server.ZallState.ToString());
                    w.WriteLine();
                    w.WriteLine("# backup options");
                    w.WriteLine("backup-time = " + Server.backupInterval.ToString());
                    w.WriteLine("backup-location = " + Server.backupLocation);
                    w.WriteLine();
                    w.WriteLine("#Error logging");
                    w.WriteLine("report-back = " + Server.reportBack.ToString().ToLower());
                    w.WriteLine();
                    w.WriteLine("#MySQL information");
                    w.WriteLine("UseMySQL = " + Server.useMySQL);
                    w.WriteLine("Host = " + Server.MySQLHost);
                    w.WriteLine("SQLPort = " + Server.MySQLPort);
                    w.WriteLine("Username = " + Server.MySQLUsername);
                    w.WriteLine("Password = " + Server.MySQLPassword);
                    w.WriteLine("DatabaseName = " + Server.MySQLDatabaseName);
                    w.WriteLine("Pooling = " + Server.MySQLPooling);
                    w.WriteLine();
                    w.WriteLine("#Colors");
                    w.WriteLine("defaultColor = " + Server.DefaultColor);
                    w.WriteLine("irc-color = " + Server.IRCColour);
                    w.WriteLine();
                    w.WriteLine("#Running on mono?");
                    w.WriteLine("mono = " + Server.mono);
                    w.WriteLine();
                    w.WriteLine("#Custom Messages");
                    w.WriteLine("custom-ban = " + Server.customBan.ToString().ToLower());
                    w.WriteLine("custom-ban-message = " + Server.customBanMessage);
                    w.WriteLine("custom-shutdown = " + Server.customShutdown.ToString().ToLower());
                    w.WriteLine("custom-shutdown-message = " + Server.customShutdownMessage);
                    w.WriteLine();
                    w.WriteLine("cheapmessage = " + Server.cheapMessage.ToString().ToLower());
                    w.WriteLine("cheap-message-given = " + Server.cheapMessageGiven);
                    w.WriteLine("uncheapmessage = " + Server.unCheapMessage.ToString().ToLower());
                    w.WriteLine("uncheap-message-given = " + Server.unCheapMessageGiven);
                    w.WriteLine("rank-super = " + Server.rankSuper.ToString().ToLower());
                    try { w.WriteLine("default-rank = " + Server.defaultRank); }
                    catch { w.WriteLine("default-rank = guest"); }
                    w.WriteLine();
                    w.WriteLine("#Homes");
                    w.WriteLine("home-perm = " + ((sbyte)Server.HomeRank).ToString());
                    w.WriteLine("homeprefix = " + Server.HomePrefix);
                    w.WriteLine("home-x = " + Server.HomeX.ToString());
                    w.WriteLine("home-y = " + Server.HomeY.ToString());
                    w.WriteLine("home-z = " + Server.HomeZ.ToString());
                    w.WriteLine();
                    w.WriteLine("#Time Ranks");
                    w.WriteLine("use-timerank = " + Server.useTimeRank.ToString().ToLower());
                    w.WriteLine("timerank-cmd = " + Server.timeRankCommand.ToLower());
                    w.WriteLine();
                    w.WriteLine("#Profanity Filter");
                    w.WriteLine("profanityfilter = " + Server.profanityFilter.ToString().ToLower());
                    w.WriteLine("warnplayer = " + Server.swearWarnPlayer.ToString().ToLower());
                    w.WriteLine("swearwordsrequired = " + Server.swearWordsRequired.ToString().ToLower());
                    w.WriteLine("apply-to-op = " + Server.profanityFilterOp.ToString().ToLower());
                    w.WriteLine("profanityfilterstyle = " + Server.profanityFilterStyle);
                    w.WriteLine();
                    w.WriteLine("#More Features");
                    w.WriteLine("antispam = " + Server.antiSpam.ToString().ToLower());
                    w.WriteLine("antispamop = " + Server.antiSpamOp.ToString().ToLower());
                    w.WriteLine("antispamstyle = " + Server.antiSpamStyle);
                    w.WriteLine("msgsrequired = " + Server.spamCounter.ToString());
                    w.WriteLine("anticaps = " + Server.antiCaps.ToString().ToLower());
                    w.WriteLine("anticapsop = " + Server.antiCapsOp.ToString().ToLower());
                    w.WriteLine("anticapsstyle = " + Server.antiCapsStyle);
                    w.WriteLine("capsrequired = " + Server.capsRequired.ToString());
                    w.WriteLine("useglobal = " + Server.useglobal.ToString().ToLower());
                    w.WriteLine("globalnick = " + validNick(Server.globalNick));
                    w.WriteLine("global-identify = " + Server.globalIdentify.ToString().ToLower());
                    w.WriteLine("global-password = " + Server.globalPassword);
                    w.WriteLine("adminsecurity = " + Server.adminsecurity.ToString().ToLower());
                    w.WriteLine("adminsecurityrank = " + ((sbyte)Server.adminsecurityrank).ToString());
                    w.WriteLine("join-on-maintenence = " + ((sbyte)Server.canjoinmaint).ToString());
                    w.WriteLine("global-color = " + Server.GlobalChatColour);
                    w.WriteLine("adminsjoinsilent = " + Server.adminsjoinsilent.ToString().ToLower());
                    w.WriteLine("adminsjoinhidden = " + Server.adminsjoinhidden.ToString().ToLower());
                    w.WriteLine("countryonjoin = " + Server.useMaxMind.ToString().ToLower());
                    w.WriteLine("allowproxy = " + Server.allowproxy.ToString().ToLower());
                    w.WriteLine("agreetorules = " + Server.agreeToRules.ToString().ToLower());
                    w.WriteLine("agreepass = " + Server.agreePass);
                    w.WriteLine("consolesound = " + Server.consoleSound.ToString().ToLower());
                    w.WriteLine("show-attempted-logins = " + Server.showAttemptedLogins.ToString().ToLower());
                    w.WriteLine("useantigrief = " + Server.useAntiGrief.ToString().ToLower());
                    w.WriteLine("allow-ignore-ops = " + Server.allowIgnoreOps.ToString().ToLower());
                    w.WriteLine("wom-text = " + Server.womText.ToString().ToLower());
                    w.WriteLine("use-discourager = " + Server.useDiscourager.ToString().ToLower());
                    w.WriteLine("throttle = " + Server.throttle);
                    w.WriteLine("usewompasswords = " + Server.useWOMPasswords.ToString().ToLower());
                    w.WriteLine("womipaddress = " + Server.WOMIPAddress);
                    w.WriteLine();
                    w.WriteLine("#Remote Console");
                    w.WriteLine("use-remote = " + Server.useRemote.ToString().ToLower());
                    w.WriteLine("rc-port = " + RemoteServer.port);
                    w.WriteLine("rc-pass = " + RemoteServer.rcpass);
                    w.WriteLine();
                }
                w.Flush();
                w.Close();
                w.Dispose();
            }
            catch
            {
                Server.s.Log("SAVE FAILED! " + givenPath);
            }
        }
        static string validNick(string value)
        {
            while (!Char.IsLetter(value[0])) { value = value.Substring(1); }
            foreach (char ch in value) { if (!Char.IsNumber(ch) && !Char.IsLetterOrDigit(ch)) { value = value.Replace(ch.ToString(), ""); } }
            value = System.Text.RegularExpressions.Regex.Replace(value, @"\s+", "");
            return value;
        }
    }
}
