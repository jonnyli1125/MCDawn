using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using MCDawn;

namespace MCDawn_.Gui
{
    public static class Program
    {
        public static bool usingConsole = false;
        public static string parent = Path.GetFileName(Assembly.GetEntryAssembly().Location);

        [DllImport("kernel32")]
        public static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public static void GlobalExHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Server.ErrorLog(ex);
            Thread.Sleep(500);

            if (!Server.restartOnError)
                Program.restartMe();
            else
                Program.restartMe(false);
        }

        public static void ThreadExHandler(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            Server.ErrorLog(ex);
            Thread.Sleep(500);

            if (!Server.restartOnError)
                Program.restartMe();
            else
                Program.restartMe(false);
        }

        public static void Main(string[] args)
        {
            if (Process.GetProcessesByName("MCDawn").Length != 1)
            {
                foreach (Process pr in Process.GetProcessesByName("MCDawn"))
                {
                    if (pr.MainModule.BaseAddress == Process.GetCurrentProcess().MainModule.BaseAddress)
                        if (pr.Id != Process.GetCurrentProcess().Id)
                            pr.Kill();
                }
            }

            PidgeonLogger.Init();
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.GlobalExHandler);
            AppDomain.CurrentDomain.UnhandledException += GlobalExHandler;
            //Application.ThreadException += new ThreadExceptionEventHandler(Program.ThreadExHandler);
            Application.ThreadException += ThreadExHandler;
            bool skip = false;
        remake:
            try
            {
                if (!File.Exists("Viewmode.cfg") || skip)
                {
                    StreamWriter SW = new StreamWriter(File.Create("Viewmode.cfg"));
                    SW.WriteLine("#This file controls how the console window is shown to the server host");
                    SW.WriteLine("#cli:             True or False (Determines whether a CLI interface is used) (Set True if on Mono)");
                    SW.WriteLine("#high-quality:    True or false (Determines whether the GUI interface uses higher quality objects)");
                    SW.WriteLine();
                    SW.WriteLine("cli = false");
                    SW.WriteLine("high-quality = true");
                    SW.Flush();
                    SW.Close();
                    SW.Dispose();
                }

                if (File.ReadAllText("Viewmode.cfg") == "") { skip = true; goto remake; }

                string[] foundView = File.ReadAllLines("Viewmode.cfg");
                if (foundView[0][0] != '#') { skip = true; goto remake; }

                if (foundView[4].Split(' ')[2].ToLower() == "true")
                {
                    Server s = new Server();
                    s.OnLog += Console.WriteLine;
                    s.OnCommand += Console.WriteLine;
                    s.OnSystem += Console.WriteLine;
                    s.Start();

                    Console.Title = Server.name + " - MCDawn Version: " + Server.Version;
                    usingConsole = true;
                    handleChat(Console.ReadLine());
                    Server.cli = true;
                    //Application.Run();
                }
                else
                {
                    IntPtr hConsole = GetConsoleWindow();
                    if (IntPtr.Zero != hConsole)
                    {
                        ShowWindow(hConsole, 0);
                    }
                    UpdateCheck(true);
                    if (foundView[5].Split(' ')[2].ToLower() == "true")
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                    }

                    updateTimer.Elapsed += delegate { UpdateCheck(); }; updateTimer.Start();

                    Application.Run(new MCDawn.Gui.Window());
                }
            }
            catch (Exception e) { Server.ErrorLog(e); handleChat(Console.ReadLine()); return; }
        }

        public static void handleChat(string s)
        {
            string st = "";
            switch (s[0])
            {
                case '!':
                case '/':
                    handleComm(s.Remove(0, 1));
                    break;
                case '@':
                    st = s.Remove(0, 1).Trim();
                    string[] words = st.Split(new char[] { ' ' }, 2);
                    Player who = Player.Find(words[0]);
                    if (who == null) { Console.WriteLine("Player could not be found!"); handleChat(Console.ReadLine()); return; }
                    who.SendMessage("&bFrom Console: &f" + words[1]);
                    Server.s.Log("(whispers to " + who.name + ") <CONSOLE> " + words[1]);
                    if (!Server.devs.Contains(who.name.ToLower()))
                    {
                        Player.GlobalMessageDevs("To Devs &f-" + Server.DefaultColor + "Console &b[>] " + who.color + who.name + "&f- " + words[1]);
                    }
                    //AllServerChat.Say("(whispers to " + who.name + ") <CONSOLE> " + words[1]);
                    break;
                case '#':
                    st = s.Remove(0, 1);
                    Player.GlobalMessageOps("To Ops &f-" + Server.DefaultColor + " Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + st);
                    Server.s.Log("(OPs): Console: " + st);
                    //Server.s.OpLog("Console: " + text);
                    IRCBot.Say("Console [" + Server.ZallState + "]: " + st, true);
                    //AllServerChat.Say("(OPs) Console [" + Server.ZallState + "]: " + st);
                    break;
                case ';':
                    st = s.Remove(0, 1);
                    Player.GlobalMessageAdmins("To Admins &f-" + Server.DefaultColor + " Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + st);
                    Server.s.Log("(Admins): Console: " + st);
                    //Server.s.OpLog("Console: " + text);
                    //IRCBot.Say("Console [" + Server.ZallState + "]: " + st, true);
                    //AllServerChat.Say("(Admins) Console [" + Server.ZallState + "]: " + st);
                    break;
                default:
                    Player.GlobalChat(null, "Console [&a" + Server.ZallState + Server.DefaultColor + "]: &f" + s, false);
                    Server.s.Log("<CONSOLE> " + s);
                    IRCBot.Say("Console [" + Server.ZallState + "]: " + s);
                    break;
            }
            handleChat(Console.ReadLine());
        }

        public static void handleComm(string s)
        {
            string[] split;
            if (s.Trim().IndexOf(' ') != -1) { split = s.Split(new char[] { ' ' }, 2); }
            else { split = new[] { s, "" }; }
            string sentCmd = split[0].Trim(), sentMsg = split[1];

            try
            {
                Command cmd = Command.all.Find(sentCmd);
                if (cmd != null)
                {
                    cmd.Use(null, sentMsg);
                    Console.WriteLine("CONSOLE: USED /" + sentCmd + " " + sentMsg);
                    handleChat(Console.ReadLine());
                    return;
                }
                else
                {
                    Console.WriteLine("Command could not be found.");
                    handleChat(Console.ReadLine());
                    return;
                }
            }
            catch (Exception e)
            {
                Server.ErrorLog(e);
                Console.WriteLine("CONSOLE: Failed command.");
                handleChat(Console.ReadLine());
                return;
            }
        }

        public static bool CurrentUpdate = false;
        static bool msgOpen = false;
        public static System.Timers.Timer updateTimer = new System.Timers.Timer(120 * 60 * 1000);

        public static void UpdateCheck(bool wait = false, Player p = null)
        {

            CurrentUpdate = true;
            Thread updateThread = new Thread(new ThreadStart(delegate
            {
                try
                {
                    if (wait) { if (!Server.checkUpdates) return; Thread.Sleep(10000); }
                    try
                    {
                        if (Server.LatestVersion() != Server.Version)
                        {
                            if (Server.autoupdate == true || p != null)
                            {
                                if (Server.autonotify == true || p != null)
                                {
                                    Player.GlobalMessage("Update found. Restarting Server.");
                                    Server.s.Log("Update found. Restarting Server.");
                                    Player.GlobalMessage("---UPDATING SERVER---");
                                    Server.s.Log("---UPDATING SERVER---");
                                    PerformUpdate(false);
                                }
                                else
                                {
                                    PerformUpdate(false);
                                }
                            }
                            else
                            {
                                if (!msgOpen && !usingConsole)
                                {
                                    msgOpen = true;
                                    if (MessageBox.Show("New version found. Would you like to update?", "Update?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    {
                                        PerformUpdate(false);
                                    }
                                    msgOpen = false;
                                }
                                else
                                {
                                    ConsoleColor prevColor = Console.ForegroundColor;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("An update was found!");
                                    Console.WriteLine("Update using the file at updates.mcdawn.com/MCDawn_.dll and placing it over the top of your current MCDawn_.dll!");
                                    Console.ForegroundColor = prevColor;
                                }
                            }
                        }
                        else
                        {
                            Player.SendMessage(p, "No update found!");
                        }
                    }
                    catch { Server.s.Log("No web server found to update on."); }
                }
                catch { }
                CurrentUpdate = false;
            })); updateThread.Start();
        }

        public static void PerformUpdate(bool oldrevision)
        {
            try
            {
                StreamWriter SW;
                if (!Server.mono)
                {
                    if (!File.Exists("Update.bat"))
                        SW = new StreamWriter(File.Create("Update.bat"));
                    else
                    {
                        if (File.ReadAllLines("Update.bat")[0] != "::Version 3")
                        {
                            SW = new StreamWriter(File.Create("Update.bat"));
                        }
                        else
                        {
                            SW = new StreamWriter(File.Create("Update_generated.bat"));
                        }
                    }
                    SW.WriteLine("::Version 3");
                    SW.WriteLine("TASKKILL /pid %2 /F");
                    SW.WriteLine("if exist MCDawn_.dll.backup (erase MCDawn_.dll.backup)");
                    SW.WriteLine("if exist MCDawn_.dll (rename MCDawn_.dll MCDawn_.dll.backup)");
                    SW.WriteLine("if exist MCDawn_.new (rename MCDawn_.new MCDawn_.dll)");
                    SW.WriteLine("if exist MCDawn.exe.backup (erase MCDawn.exe.backup)");
                    SW.WriteLine("if exist MCDawn.exe (rename MCDawn.exe MCDawn.exe.backup)");
                    SW.WriteLine("if exist MCDawn.new (rename MCDawn.new MCDawn.exe)");
                    SW.WriteLine("start MCDawn.exe");
                }
                else
                {
                    if (!File.Exists("Update.sh"))
                        SW = new StreamWriter(File.Create("Update.sh"));
                    else
                    {
                        if (File.ReadAllLines("Update.sh")[0] != "#Version 2")
                        {
                            SW = new StreamWriter(File.Create("Update.sh"));
                        }
                        else
                        {
                            SW = new StreamWriter(File.Create("Update_generated.sh"));
                        }
                    }
                    SW.WriteLine("#Version 2");
                    SW.WriteLine("#!/bin/bash");
                    SW.WriteLine("kill $2");
                    SW.WriteLine("rm MCDawn_.dll.backup");
                    SW.WriteLine("mv MCDawn_.dll MCDawn.dll_.backup");
                    SW.WriteLine("wget http://updates.mcdawn.com/MCDawn_.dll");
                    SW.WriteLine("rm MCDawn.exe.backup");
                    SW.WriteLine("mv MCDawn.dll MCDawn.exe.backup");
                    SW.WriteLine("wget http://updates.mcdawn.com/exe/MCDawn.exe");
                    SW.WriteLine("mono MCDawn.exe");
                }

                SW.Flush(); SW.Close(); SW.Dispose();

                string filelocation = "";
                string verscheck = "";
                Process proc = Process.GetCurrentProcess();
                string assemblyname = proc.ProcessName + ".exe";
                if (!oldrevision)
                {
                    Server.selectedrevision = Server.LatestVersion();
                }
                verscheck = Server.selectedrevision.TrimStart('r');
                int vers = int.Parse(verscheck.Split('.')[0]);
                if (oldrevision) { filelocation = ("http://updates.mcdawn.com/dll/" + Server.selectedrevision + ".dll"); }
                if (!oldrevision) { filelocation = ("http://updates.mcdawn.com/MCDawn_.dll"); }
                using (WebClient Client = new WebClient())
                {
                    if (!File.Exists("MCDawn_.new")) { Client.DownloadFile(filelocation, "MCDawn_.new"); }
                    Client.DownloadFile("http://updates.mcdawn.com/Changelog.txt", "Changelog.txt");
                    if (!File.Exists("MCDawn.new")) { Client.DownloadFile("http://updates.mcdawn.com/exe/MCDawn.exe", "MCDawn.new"); }
                }
                foreach (Level l in Server.levels) l.Save();
                foreach (Player pl in Player.players) pl.save();

                string fileName;
                if (!Server.mono) fileName = "Update.bat";
                else fileName = "Update.sh";

                try
                {
                    if (MCDawn.Gui.Window.thisWindow.notifyIcon1 != null)
                    {
                        MCDawn.Gui.Window.thisWindow.notifyIcon1.Icon = null;
                        MCDawn.Gui.Window.thisWindow.notifyIcon1.Visible = false;
                    }
                }
                catch { }

                Process p = Process.Start(fileName, "main " + System.Diagnostics.Process.GetCurrentProcess().Id.ToString());
                p.WaitForExit();
            }
            catch (Exception e) { Server.ErrorLog(e); }
        }

        static public void ExitProgram(Boolean AutoRestart)
        {
            Thread exitThread;
            Server.Exit();

            exitThread = new Thread(new ThreadStart(delegate
            {
                try
                {
                    if (MCDawn.Gui.Window.thisWindow.notifyIcon1 != null)
                    {
                        MCDawn.Gui.Window.thisWindow.notifyIcon1.Icon = null;
                        MCDawn.Gui.Window.thisWindow.notifyIcon1.Visible = false;
                    }
                }
                catch { }

                try
                {
                    saveAll();

                    if (AutoRestart == true) restartMe();
                    else Server.process.Kill();
                }
                catch
                {
                    Server.process.Kill();
                }
            })); exitThread.Start();
        }

        static public void restartMe(bool fullRestart = true)
        {
            Thread restartThread = new Thread(new ThreadStart(delegate
            {
                saveAll();

                Server.shuttingDown = true;
                try
                {
                    if (MCDawn.Gui.Window.thisWindow.notifyIcon1 != null)
                    {
                        MCDawn.Gui.Window.thisWindow.notifyIcon1.Icon = null;
                        MCDawn.Gui.Window.thisWindow.notifyIcon1.Visible = false;
                    }
                }
                catch { }

                if (!Server.mono || fullRestart)
                {
                    Application.Restart();
                    Server.process.Kill();
                }
                else
                {
                    Server.s.Start();
                }
            }));
            restartThread.Start();
        }
        static public void saveAll()
        {
            try
            {
                /*List<Player> kickList = new List<Player>();
                kickList.AddRange(Player.players);
                foreach (Player p in kickList) { p.Kick("Server restarted! Rejoin!"); }*/
                Player.players.ForEach(delegate(Player p)
                {
                    if (!Server.hasProtection(p.name))
                        p.Kick("Server restarted! Rejoin!");
                    else
                        p.Disconnect();
                });
            }
            catch (Exception exc) { Server.ErrorLog(exc); }

            try
            {
                string level = null;
                foreach (Level l in Server.levels)
                {
                    level = level + l.name + "=" + l.physics + System.Environment.NewLine;
                    l.Save();
                    l.saveChanges();
                }

                File.WriteAllText("text/autoload.txt", level);
            }
            catch (Exception exc) { Server.ErrorLog(exc); }
        }
    }
}
