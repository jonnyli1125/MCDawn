using System;
using System.Drawing;
using System.Windows.Forms;

namespace MCDawn.Gui
{
    public partial class Chat : Form
    {
        public Chat()
        {
            InitializeComponent();
        }

        public static Chat thisChat;

        delegate void LogDelegate(string message);

        public Color getColor(string s) // get color of first char in string.
        {
        redo:
            s = s.Replace("&", ""); s = s.Replace("%", "");
            switch (s[0].ToString().ToLower())
            {
                case "a": return Color.LimeGreen;
                case "b": return Color.Aqua;
                case "c": return Color.Red;
                case "d": return Color.Pink;
                case "e": return Color.Yellow;
                case "f": return Color.Black;
                case "g": s = Server.DefaultColor; goto redo;
                case "0": return Color.WhiteSmoke;
                case "1": return Color.Navy;
                case "2": return Color.Green;
                case "3": return Color.Teal;
                case "4": return Color.Maroon;
                case "5": return Color.Purple;
                case "6": return Color.Gold;
                case "7": return Color.Silver;
                case "8": return Color.DarkGray;
                case "9": return Color.Blue;
                default: return Color.Black;
            }
        }

        public void Log(string s)
        {
            if (String.IsNullOrEmpty(s)) { return; }
            if (this.InvokeRequired)
            {
                LogDelegate d = new LogDelegate(Log);
                this.Invoke(d, new object[] { s });
            }
            else
            {
                try
                {
                    if (!s.Contains("%") && !s.Contains("&"))
                    {
                        txtLog.AppendText(s + "\r\n");
                        txtLog.Select(txtLog.Text.Length - s.Length, txtLog.Text.Length);
                        txtLog.SelectionColor = Color.Black;
                        txtLog.DeselectAll();
                        return;
                    }
                    //int looped = 0;
                    string[] split = s.Split('%', '&');
                    foreach (string str in split)
                    {
                        if (String.IsNullOrEmpty(str)) { continue; }
                        int start = txtLog.Text.Length + (s.Length - str.Length) + 1;
                        for (int I = 0; I < 10; I++)
                        {
                            s = s.Replace("&" + I, ""); // remove codes
                            s = s.Replace("%" + I, "");
                        }
                        for (char ch = 'a'; ch <= 'f'; ch++)
                        {
                            s = s.Replace("&" + ch, "");
                            s = s.Replace("%" + ch, "");
                        }
                    }
                    txtLog.AppendText(s + "\r\n");
                    txtLog.Select(txtLog.Text.Length - s.Length, txtLog.Text.Length);
                    txtLog.SelectionColor = Color.Black;
                    txtLog.DeselectAll();
                    int torecolor = 0;
                    for (int i = 0; i < split.Length; i++)
                    {
                        try
                        {
                            if (i >= 1)
                            {
                                int total = 0;
                                total = s.Length;
                                total -= split[0].Length;
                                if (i >= 2)
                                {
                                    torecolor++;
                                    for (int times = 0; times < torecolor; times++)
                                        total -= split[i - 1].Length - 1;
                                }
                                int start = txtLog.Text.Length - total;
                                txtLog.Select(start - 1, start + split[i].Length);
                                txtLog.SelectionColor = getColor(split[i][0].ToString());
                                txtLog.DeselectAll();
                            }
                        }
                        catch { continue; }
                    }
                }
                catch (Exception ex) { Server.ErrorLog(ex); }
            }
            if (Server.consoleSound && Window.thisWindow.WindowState == FormWindowState.Minimized/* && !s.StartsWith("<CONSOLE>")*/) { MCDawn.Gui.Window.thisWindow.consoleSound.Play(); }
        }

        public void WriteCommand(string s)
        {
            Log(s);
            try { Window.thisWindow.newCommand(s); }
            catch { }
        }

        public void HandleChat(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string text = txtInput.Text.Trim();
                    switch (text[0])
                    {
                        case '/':
                            if (text == "/") { Log("No command entered."); txtInput.Clear(); return; }
                            HandleCommand(text.Remove(0, 1));
                            break;
                        case '#':
                            text = text.Remove(0, 1);
                            Player.GlobalMessageOps("To Ops &f-" + Server.DefaultColor + " Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + text);
                            //WriteLine("(OPs): Console: " + text);
                            Server.s.Log("(OPs): Console: " + text);
                            //Server.s.OpLog("Console: " + text);
                            IRCBot.Say("Console: " + text, true);
                            //AllServerChat.Say("(OPs) Console [" + Server.ZallState + "]: " + text);
                            MCDawn.Gui.Window.thisWindow.WriteOpLine("<CONSOLE>" + text);
                            break;
                        case ';':
                            text = text.Remove(0, 1);
                            Player.GlobalMessageAdmins("To Admins &f-" + Server.DefaultColor + " Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + text);
                            Server.s.Log("(Admins): Console: " + text);
                            //WriteLine("(Admins): Console: " + text);
                            //Server.s.AdminLog("Console: " + text);
                            //IRCBot.Say("Console: " + text, true);
                            //AllServerChat.Say("(Admins) Console [" + Server.ZallState + "]: " + text);
                            MCDawn.Gui.Window.thisWindow.WriteAdminLine("<CONSOLE>" + text);
                            break;
                        case '@':
                            text = text.Remove(0, 1).Trim();
                            string[] words = text.Split(new char[] { ' ' }, 2);
                            Player who = Player.Find(words[0]);
                            if (who == null)
                            {
                                Server.s.Log("Player could not be found!");
                                txtInput.Clear();
                                return;
                            }
                            who.SendMessage("&bFrom Console: &f" + words[1]);
                            Server.s.Log("(whispers to " + who.name + ") <CONSOLE> " + words[1]);
                            //WriteLine("(whispers to " + who.name + ") <CONSOLE> " + words[1]);
                            if (!Server.devs.Contains(who.name.ToLower()))
                            {
                                Player.GlobalMessageDevs("To Devs &f-" + Server.DefaultColor + "Console &b[>] " + who.color + who.name + "&f- " + words[1]);
                            }
                            //AllServerChat.Say("(whispers to " + who.name + ") <CONSOLE> " + words[1]);
                            break;
                        default:
                            if (String.IsNullOrEmpty(text)) { return; }
                            Player.GlobalChat(null, "Console [&a" + Server.ZallState + Server.DefaultColor + "]:&f " + text, false);
                            IRCBot.Say("Console [" + Server.ZallState + "]: " + text);
                            Server.s.Log("<CONSOLE> " + text);
                            //AllServerChat.Say("Console [" + Server.ZallState + "]: " + text);
                            break;
                    }
                    txtInput.Clear();
                }
            }
            catch (Exception ex) { Server.ErrorLog(ex); }
        }
        public void HandleCommand(string s)
        {
            try
            {
                string[] splitted = s.Split(new char[] { ' ' }, 2);
                string cmd = splitted[0], message = "";
                if (splitted.Length > 1) { message = splitted[1]; }
                if (cmd[0] == '/') { cmd = cmd.Remove(0, 1); }
                Command commandcmd = Command.all.Find(cmd);
                if (commandcmd == null)
                {
                    Server.s.Log("Unknown command \"" + cmd.ToLower() + "\"!");
                    txtInput.Clear();
                    return;
                }
                commandcmd.Use(null, message);
                WriteCommand("CONSOLE: USED /" + cmd + " " + message);

            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                WriteCommand("CONSOLE: Failed command.");
            }
        }

        private void Chat_Load(object sender, EventArgs e)
        {
            this.Text = "Chat/Commands Only - " + Server.name + " (MCDawn " + Server.Version + ")";
            txtLog.AppendText("MCDawn Mini-Console Initiated - Chat/Commands only. You may minimize the Main Console." + Environment.NewLine);
            txtLog.AppendText("Use / for commands, # for opchat, ; for adminchat, and @ for whisper." + Environment.NewLine);
            Server.s.OnLog += Log;
            Server.s.OnCommand += WriteCommand;
            Server.s.OnSystem += Log;
            thisChat = this;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Window.thisWindow.miniConsole = false;
            Server.s.OnLog -= Log;
            Server.s.OnCommand -= WriteCommand;
            Server.s.OnSystem -= Log;
            this.Hide();
            this.Dispose();
        }

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        } 
    }
}
