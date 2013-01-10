/*
	Copyright 2010 MCSharp team (Modified for use with MCZall/MCLawl) Licensed under the
	Educational Community License, Version 2.0 (the "License"); you may
	not use this file except in compliance with the License. You may
	obtain a copy of the License at
	
	http://www.osedu.org/licenses/ECL-2.0
	
	Unless required by applicable law or agreed to in writing,
	software distributed under the License is distributed on an "AS IS"
	BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
	or implied. See the License for the specific language governing
	permissions and limitations under the License.
*/
using System;
using System.Windows.Forms;

namespace MCDawn.Gui
{
    public partial class Window
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void WndProc(ref Message msg)
        {
            const int WM_SIZE = 0x0005;
            const int SIZE_MINIMIZED = 1;

            if ((msg.Msg == WM_SIZE) && ((int)msg.WParam == SIZE_MINIMIZED) && (Window.Minimize != null))
            {
                this.Window_Minimize(this, EventArgs.Empty);
            }

            base.WndProc(ref msg);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
            this.playerStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.whoisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.banToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.voiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapsStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.physicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.unloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.finiteModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animalAIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgeWaterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.growingGrassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.survivalDeathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.killerBlocksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rPChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrRestart = new System.Windows.Forms.Timer(this.components);
            this.iconContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openConsole = new System.Windows.Forms.ToolStripMenuItem();
            this.shutdownServer = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.label37 = new System.Windows.Forms.Label();
            this.liAdministration = new System.Windows.Forms.ListBox();
            this.grpStats = new System.Windows.Forms.GroupBox();
            this.lblThreads = new System.Windows.Forms.Label();
            this.lblCPU = new System.Windows.Forms.Label();
            this.lblMemory = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.lblUptime = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.grpPlayers = new System.Windows.Forms.GroupBox();
            this.lblTotalOps = new System.Windows.Forms.Label();
            this.lblTotalPlayersBanned = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.lblTotalPlayersVisited = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.lblTotalGuests = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.lblTotalPlayers = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.grpVersion = new System.Windows.Forms.GroupBox();
            this.lblLatestVersion = new System.Windows.Forms.Label();
            this.lblCurVersion = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.linkSite = new System.Windows.Forms.LinkLabel();
            this.linkForums = new System.Windows.Forms.LinkLabel();
            this.liStaff = new System.Windows.Forms.ListBox();
            this.label28 = new System.Windows.Forms.Label();
            this.liDevs = new System.Windows.Forms.ListBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtErrors = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnUpdateChangelog = new System.Windows.Forms.Button();
            this.txtChangelog = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.txtSystem = new System.Windows.Forms.TextBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.grpMapViewer = new System.Windows.Forms.GroupBox();
            this.txtMapViewerZ = new System.Windows.Forms.TextBox();
            this.txtMapViewerY = new System.Windows.Forms.TextBox();
            this.txtMapViewerX = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.btnMapViewerSave = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.btnMapViewerUpdate = new System.Windows.Forms.Button();
            this.txtMapViewerLevelName = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.picMapViewer = new System.Windows.Forms.PictureBox();
            this.grpMapEditor = new System.Windows.Forms.GroupBox();
            this.btnMapEditorUpdate = new System.Windows.Forms.Button();
            this.btnMapEditorChange = new System.Windows.Forms.Button();
            this.txtMapEditorChangeBlock = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtMapEditorCurrentBlock = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtMapEditorZ = new System.Windows.Forms.TextBox();
            this.txtMapEditorY = new System.Windows.Forms.TextBox();
            this.txtMapEditorX = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtMapEditorLevelName = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.grpProperties = new System.Windows.Forms.GroupBox();
            this.chkAutoLoad = new System.Windows.Forms.CheckBox();
            this.chkAllowGuns = new System.Windows.Forms.CheckBox();
            this.btnPropertiesSave = new System.Windows.Forms.Button();
            this.chkRPChat = new System.Windows.Forms.CheckBox();
            this.txtDrown = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtFall = new System.Windows.Forms.NumericUpDown();
            this.chkSurvivalDeath = new System.Windows.Forms.CheckBox();
            this.txtPhysicsOverload = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPhysicsSpeed = new System.Windows.Forms.TextBox();
            this.chkInstant = new System.Windows.Forms.CheckBox();
            this.chkAutoPhysics = new System.Windows.Forms.CheckBox();
            this.chkUnload = new System.Windows.Forms.CheckBox();
            this.chkKillerBlocks = new System.Windows.Forms.CheckBox();
            this.chkGrassGrowing = new System.Windows.Forms.CheckBox();
            this.chkEdgeWater = new System.Windows.Forms.CheckBox();
            this.chkAnimalAI = new System.Windows.Forms.CheckBox();
            this.chkFiniteMode = new System.Windows.Forms.CheckBox();
            this.txtPhysics = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLevelMotd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnKillPhysics = new System.Windows.Forms.Button();
            this.btnUpdateLevelList = new System.Windows.Forms.Button();
            this.grpNewDelRenLevel = new System.Windows.Forms.GroupBox();
            this.cmbLevelType = new System.Windows.Forms.ComboBox();
            this.btnNewLevel = new System.Windows.Forms.Button();
            this.txtZDim = new System.Windows.Forms.TextBox();
            this.txtYDim = new System.Windows.Forms.TextBox();
            this.txtXDim = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLevelName = new System.Windows.Forms.TextBox();
            this.grpNewLevel = new System.Windows.Forms.GroupBox();
            this.grpDeleteLevel = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnDeleteLevel = new System.Windows.Forms.Button();
            this.txtDeleteLevelName = new System.Windows.Forms.TextBox();
            this.grpRenameLevel = new System.Windows.Forms.GroupBox();
            this.btnRenameLevel = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtNewName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCurName = new System.Windows.Forms.TextBox();
            this.txtLevelLog = new System.Windows.Forms.TextBox();
            this.grpLevels = new System.Windows.Forms.GroupBox();
            this.btnUnloadLevel = new System.Windows.Forms.Button();
            this.btnLoadLevel = new System.Windows.Forms.Button();
            this.liLoadedLevels = new System.Windows.Forms.ListBox();
            this.liUnloadedLevels = new System.Windows.Forms.ListBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.grpRanking = new System.Windows.Forms.GroupBox();
            this.btnDemote = new System.Windows.Forms.Button();
            this.btnPromote = new System.Windows.Forms.Button();
            this.grpChatCommands = new System.Windows.Forms.GroupBox();
            this.btnChatModeration = new System.Windows.Forms.Button();
            this.btnMute = new System.Windows.Forms.Button();
            this.btnVoice = new System.Windows.Forms.Button();
            this.btnKick = new System.Windows.Forms.Button();
            this.btnFreeze = new System.Windows.Forms.Button();
            this.grpPlayerLogs = new System.Windows.Forms.GroupBox();
            this.liPlayers = new System.Windows.Forms.ListBox();
            this.txtPlayerLog = new System.Windows.Forms.TextBox();
            this.btnInvincible = new System.Windows.Forms.Button();
            this.btnWarn = new System.Windows.Forms.Button();
            this.btnJoker = new System.Windows.Forms.Button();
            this.grpInGameCmds = new System.Windows.Forms.GroupBox();
            this.btnKill = new System.Windows.Forms.Button();
            this.btnSlap = new System.Windows.Forms.Button();
            this.btnJail = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.grpBanCmds = new System.Windows.Forms.GroupBox();
            this.btnXBan = new System.Windows.Forms.Button();
            this.btnBanIP = new System.Windows.Forms.Button();
            this.btnBan = new System.Windows.Forms.Button();
            this.grpPlayerInfo = new System.Windows.Forms.GroupBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtDeaths = new System.Windows.Forms.TextBox();
            this.lblDeaths = new System.Windows.Forms.Label();
            this.txtKicks = new System.Windows.Forms.TextBox();
            this.lblKicks = new System.Windows.Forms.Label();
            this.txtModified = new System.Windows.Forms.TextBox();
            this.lblModified = new System.Windows.Forms.Label();
            this.txtLevel = new System.Windows.Forms.TextBox();
            this.lblLevel = new System.Windows.Forms.Label();
            this.txtRank = new System.Windows.Forms.TextBox();
            this.lblRank = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.txtPlayerName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label45 = new System.Windows.Forms.Label();
            this.txtOpInput = new System.Windows.Forms.TextBox();
            this.txtOpLog = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label44 = new System.Windows.Forms.Label();
            this.txtGlobalInput = new System.Windows.Forms.TextBox();
            this.txtGlobalLog = new System.Windows.Forms.TextBox();
            this.grpAdmin = new System.Windows.Forms.GroupBox();
            this.label43 = new System.Windows.Forms.Label();
            this.txtAdminInput = new System.Windows.Forms.TextBox();
            this.txtAdminLog = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkChatColors = new System.Windows.Forms.CheckBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.label38 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.liPlayerBots = new System.Windows.Forms.ListBox();
            this.chkConsoleSounds = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnProperties = new System.Windows.Forms.Button();
            this.chkMaintenance = new System.Windows.Forms.CheckBox();
            this.btnRestart = new System.Windows.Forms.Button();
            this.liMaps = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.gBCommands = new System.Windows.Forms.GroupBox();
            this.txtCommandsUsed = new System.Windows.Forms.TextBox();
            this.gBChat = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.txtCommands = new System.Windows.Forms.TextBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.liClients = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.label47 = new System.Windows.Forms.Label();
            this.grpRCUsers = new System.Windows.Forms.GroupBox();
            this.btnRemoveRCUser = new System.Windows.Forms.Button();
            this.btnAddRCUser = new System.Windows.Forms.Button();
            this.liRCUsers = new System.Windows.Forms.ListBox();
            this.label46 = new System.Windows.Forms.Label();
            this.grpRCSettings = new System.Windows.Forms.GroupBox();
            this.lblRCCheckPortResult = new System.Windows.Forms.Label();
            this.btnRCPortCheck = new System.Windows.Forms.Button();
            this.chkUseRemote = new System.Windows.Forms.CheckBox();
            this.grpConnectedRCs = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.rtbDescription = new System.Windows.Forms.RichTextBox();
            this.txtRCWhisper = new System.Windows.Forms.TextBox();
            this.txtRCDisc = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.liConnectedRCs = new System.Windows.Forms.ListBox();
            this.txtRCPort = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.btnGenerateRCKey = new System.Windows.Forms.Button();
            this.txtRCKey = new System.Windows.Forms.TextBox();
            this.txtMapViewerRotation = new System.Windows.Forms.NumericUpDown();
            this.playerStrip.SuspendLayout();
            this.mapsStrip.SuspendLayout();
            this.iconContext.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.grpStats.SuspendLayout();
            this.grpPlayers.SuspendLayout();
            this.grpVersion.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.grpMapViewer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMapViewer)).BeginInit();
            this.grpMapEditor.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.grpProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDrown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhysics)).BeginInit();
            this.grpNewDelRenLevel.SuspendLayout();
            this.grpDeleteLevel.SuspendLayout();
            this.grpRenameLevel.SuspendLayout();
            this.grpLevels.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.grpRanking.SuspendLayout();
            this.grpChatCommands.SuspendLayout();
            this.grpPlayerLogs.SuspendLayout();
            this.grpInGameCmds.SuspendLayout();
            this.grpBanCmds.SuspendLayout();
            this.grpPlayerInfo.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpAdmin.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gBCommands.SuspendLayout();
            this.gBChat.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.grpRCUsers.SuspendLayout();
            this.grpRCSettings.SuspendLayout();
            this.grpConnectedRCs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMapViewerRotation)).BeginInit();
            this.SuspendLayout();
            // 
            // playerStrip
            // 
            this.playerStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.whoisToolStripMenuItem,
            this.kickToolStripMenuItem,
            this.banToolStripMenuItem,
            this.voiceToolStripMenuItem});
            this.playerStrip.Name = "playerStrip";
            this.playerStrip.Size = new System.Drawing.Size(106, 92);
            // 
            // whoisToolStripMenuItem
            // 
            this.whoisToolStripMenuItem.Name = "whoisToolStripMenuItem";
            this.whoisToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.whoisToolStripMenuItem.Text = "whois";
            this.whoisToolStripMenuItem.Click += new System.EventHandler(this.whoisToolStripMenuItem_Click);
            // 
            // kickToolStripMenuItem
            // 
            this.kickToolStripMenuItem.Name = "kickToolStripMenuItem";
            this.kickToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.kickToolStripMenuItem.Text = "kick";
            this.kickToolStripMenuItem.Click += new System.EventHandler(this.kickToolStripMenuItem_Click);
            // 
            // banToolStripMenuItem
            // 
            this.banToolStripMenuItem.Name = "banToolStripMenuItem";
            this.banToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.banToolStripMenuItem.Text = "ban";
            this.banToolStripMenuItem.Click += new System.EventHandler(this.banToolStripMenuItem_Click);
            // 
            // voiceToolStripMenuItem
            // 
            this.voiceToolStripMenuItem.Name = "voiceToolStripMenuItem";
            this.voiceToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.voiceToolStripMenuItem.Text = "voice";
            this.voiceToolStripMenuItem.Click += new System.EventHandler(this.voiceToolStripMenuItem_Click);
            // 
            // mapsStrip
            // 
            this.mapsStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.physicsToolStripMenuItem,
            this.unloadToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.mapsStrip.Name = "mapsStrip";
            this.mapsStrip.Size = new System.Drawing.Size(117, 92);
            // 
            // physicsToolStripMenuItem
            // 
            this.physicsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.physicsToolStripMenuItem.Name = "physicsToolStripMenuItem";
            this.physicsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.physicsToolStripMenuItem.Text = "Physics";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem2.Text = "0";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem3.Text = "1";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem4.Text = "2";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem5.Text = "3";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem6.Text = "4";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // unloadToolStripMenuItem
            // 
            this.unloadToolStripMenuItem.Name = "unloadToolStripMenuItem";
            this.unloadToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.unloadToolStripMenuItem.Text = "Unload";
            this.unloadToolStripMenuItem.Click += new System.EventHandler(this.unloadToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.finiteModeToolStripMenuItem,
            this.animalAIToolStripMenuItem,
            this.edgeWaterToolStripMenuItem,
            this.growingGrassToolStripMenuItem,
            this.survivalDeathToolStripMenuItem,
            this.killerBlocksToolStripMenuItem,
            this.rPChatToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // finiteModeToolStripMenuItem
            // 
            this.finiteModeToolStripMenuItem.Name = "finiteModeToolStripMenuItem";
            this.finiteModeToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.finiteModeToolStripMenuItem.Text = "Finite Mode";
            this.finiteModeToolStripMenuItem.Click += new System.EventHandler(this.finiteModeToolStripMenuItem_Click);
            // 
            // animalAIToolStripMenuItem
            // 
            this.animalAIToolStripMenuItem.Name = "animalAIToolStripMenuItem";
            this.animalAIToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.animalAIToolStripMenuItem.Text = "Animal AI";
            this.animalAIToolStripMenuItem.Click += new System.EventHandler(this.animalAIToolStripMenuItem_Click);
            // 
            // edgeWaterToolStripMenuItem
            // 
            this.edgeWaterToolStripMenuItem.Name = "edgeWaterToolStripMenuItem";
            this.edgeWaterToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.edgeWaterToolStripMenuItem.Text = "Edge Water";
            this.edgeWaterToolStripMenuItem.Click += new System.EventHandler(this.edgeWaterToolStripMenuItem_Click);
            // 
            // growingGrassToolStripMenuItem
            // 
            this.growingGrassToolStripMenuItem.Name = "growingGrassToolStripMenuItem";
            this.growingGrassToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.growingGrassToolStripMenuItem.Text = "Grass Growing";
            this.growingGrassToolStripMenuItem.Click += new System.EventHandler(this.growingGrassToolStripMenuItem_Click);
            // 
            // survivalDeathToolStripMenuItem
            // 
            this.survivalDeathToolStripMenuItem.Name = "survivalDeathToolStripMenuItem";
            this.survivalDeathToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.survivalDeathToolStripMenuItem.Text = "Survival Death";
            this.survivalDeathToolStripMenuItem.Click += new System.EventHandler(this.survivalDeathToolStripMenuItem_Click);
            // 
            // killerBlocksToolStripMenuItem
            // 
            this.killerBlocksToolStripMenuItem.Name = "killerBlocksToolStripMenuItem";
            this.killerBlocksToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.killerBlocksToolStripMenuItem.Text = "Killer Blocks";
            this.killerBlocksToolStripMenuItem.Click += new System.EventHandler(this.killerBlocksToolStripMenuItem_Click);
            // 
            // rPChatToolStripMenuItem
            // 
            this.rPChatToolStripMenuItem.Name = "rPChatToolStripMenuItem";
            this.rPChatToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.rPChatToolStripMenuItem.Text = "RP Chat";
            this.rPChatToolStripMenuItem.Click += new System.EventHandler(this.rPChatToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // tmrRestart
            // 
            this.tmrRestart.Enabled = true;
            this.tmrRestart.Interval = 1000;
            // 
            // iconContext
            // 
            this.iconContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openConsole,
            this.shutdownServer});
            this.iconContext.Name = "iconContext";
            this.iconContext.Size = new System.Drawing.Size(164, 48);
            // 
            // openConsole
            // 
            this.openConsole.Name = "openConsole";
            this.openConsole.Size = new System.Drawing.Size(163, 22);
            this.openConsole.Text = "Open Console";
            this.openConsole.Click += new System.EventHandler(this.openConsole_Click);
            // 
            // shutdownServer
            // 
            this.shutdownServer.Name = "shutdownServer";
            this.shutdownServer.Size = new System.Drawing.Size(163, 22);
            this.shutdownServer.Text = "Shutdown Server";
            this.shutdownServer.Click += new System.EventHandler(this.shutdownServer_Click);
            // 
            // tabPage8
            // 
            this.tabPage8.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage8.Controls.Add(this.label37);
            this.tabPage8.Controls.Add(this.liAdministration);
            this.tabPage8.Controls.Add(this.grpStats);
            this.tabPage8.Controls.Add(this.grpPlayers);
            this.tabPage8.Controls.Add(this.grpVersion);
            this.tabPage8.Controls.Add(this.linkSite);
            this.tabPage8.Controls.Add(this.linkForums);
            this.tabPage8.Controls.Add(this.liStaff);
            this.tabPage8.Controls.Add(this.label28);
            this.tabPage8.Controls.Add(this.liDevs);
            this.tabPage8.Controls.Add(this.label27);
            this.tabPage8.Controls.Add(this.label26);
            this.tabPage8.Controls.Add(this.txtDescription);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(786, 553);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "About/Stats";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(547, 369);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(170, 14);
            this.label37.TabIndex = 12;
            this.label37.Text = "MCDawn Administration Team";
            // 
            // liAdministration
            // 
            this.liAdministration.FormattingEnabled = true;
            this.liAdministration.Location = new System.Drawing.Point(504, 386);
            this.liAdministration.Name = "liAdministration";
            this.liAdministration.Size = new System.Drawing.Size(254, 69);
            this.liAdministration.TabIndex = 11;
            // 
            // grpStats
            // 
            this.grpStats.Controls.Add(this.lblThreads);
            this.grpStats.Controls.Add(this.lblCPU);
            this.grpStats.Controls.Add(this.lblMemory);
            this.grpStats.Controls.Add(this.label42);
            this.grpStats.Controls.Add(this.label41);
            this.grpStats.Controls.Add(this.label40);
            this.grpStats.Controls.Add(this.lblUptime);
            this.grpStats.Controls.Add(this.label34);
            this.grpStats.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpStats.Location = new System.Drawing.Point(23, 23);
            this.grpStats.Name = "grpStats";
            this.grpStats.Size = new System.Drawing.Size(434, 163);
            this.grpStats.TabIndex = 10;
            this.grpStats.TabStop = false;
            this.grpStats.Text = "Server Statistics";
            // 
            // lblThreads
            // 
            this.lblThreads.AutoSize = true;
            this.lblThreads.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThreads.Location = new System.Drawing.Point(168, 127);
            this.lblThreads.Name = "lblThreads";
            this.lblThreads.Size = new System.Drawing.Size(20, 18);
            this.lblThreads.TabIndex = 11;
            this.lblThreads.Text = "...";
            // 
            // lblCPU
            // 
            this.lblCPU.AutoSize = true;
            this.lblCPU.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCPU.Location = new System.Drawing.Point(168, 93);
            this.lblCPU.Name = "lblCPU";
            this.lblCPU.Size = new System.Drawing.Size(20, 18);
            this.lblCPU.TabIndex = 10;
            this.lblCPU.Text = "...";
            // 
            // lblMemory
            // 
            this.lblMemory.AutoSize = true;
            this.lblMemory.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemory.Location = new System.Drawing.Point(168, 60);
            this.lblMemory.Name = "lblMemory";
            this.lblMemory.Size = new System.Drawing.Size(20, 18);
            this.lblMemory.TabIndex = 9;
            this.lblMemory.Text = "...";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(81, 130);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(54, 14);
            this.label42.TabIndex = 8;
            this.label42.Text = "Threads:";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(68, 96);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(67, 14);
            this.label41.TabIndex = 7;
            this.label41.Text = "CPU Usage:";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(45, 63);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(90, 14);
            this.label40.TabIndex = 6;
            this.label40.Text = "Memory Usage:";
            // 
            // lblUptime
            // 
            this.lblUptime.AutoSize = true;
            this.lblUptime.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUptime.Location = new System.Drawing.Point(168, 27);
            this.lblUptime.Name = "lblUptime";
            this.lblUptime.Size = new System.Drawing.Size(20, 18);
            this.lblUptime.TabIndex = 2;
            this.lblUptime.Text = "...";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(86, 30);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(49, 14);
            this.label34.TabIndex = 0;
            this.label34.Text = "Uptime:";
            // 
            // grpPlayers
            // 
            this.grpPlayers.Controls.Add(this.lblTotalOps);
            this.grpPlayers.Controls.Add(this.lblTotalPlayersBanned);
            this.grpPlayers.Controls.Add(this.label32);
            this.grpPlayers.Controls.Add(this.lblTotalPlayersVisited);
            this.grpPlayers.Controls.Add(this.label39);
            this.grpPlayers.Controls.Add(this.lblTotalGuests);
            this.grpPlayers.Controls.Add(this.label33);
            this.grpPlayers.Controls.Add(this.lblTotalPlayers);
            this.grpPlayers.Controls.Add(this.label35);
            this.grpPlayers.Controls.Add(this.label31);
            this.grpPlayers.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPlayers.Location = new System.Drawing.Point(23, 192);
            this.grpPlayers.Name = "grpPlayers";
            this.grpPlayers.Size = new System.Drawing.Size(434, 221);
            this.grpPlayers.TabIndex = 9;
            this.grpPlayers.TabStop = false;
            this.grpPlayers.Text = "Player Statistics";
            // 
            // lblTotalOps
            // 
            this.lblTotalOps.AutoSize = true;
            this.lblTotalOps.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalOps.Location = new System.Drawing.Point(168, 104);
            this.lblTotalOps.Name = "lblTotalOps";
            this.lblTotalOps.Size = new System.Drawing.Size(20, 18);
            this.lblTotalOps.TabIndex = 5;
            this.lblTotalOps.Text = "...";
            // 
            // lblTotalPlayersBanned
            // 
            this.lblTotalPlayersBanned.AutoSize = true;
            this.lblTotalPlayersBanned.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPlayersBanned.Location = new System.Drawing.Point(168, 179);
            this.lblTotalPlayersBanned.Name = "lblTotalPlayersBanned";
            this.lblTotalPlayersBanned.Size = new System.Drawing.Size(20, 18);
            this.lblTotalPlayersBanned.TabIndex = 5;
            this.lblTotalPlayersBanned.Text = "...";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(64, 107);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(71, 14);
            this.label32.TabIndex = 4;
            this.label32.Text = "Ops Online:";
            // 
            // lblTotalPlayersVisited
            // 
            this.lblTotalPlayersVisited.AutoSize = true;
            this.lblTotalPlayersVisited.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPlayersVisited.Location = new System.Drawing.Point(168, 141);
            this.lblTotalPlayersVisited.Name = "lblTotalPlayersVisited";
            this.lblTotalPlayersVisited.Size = new System.Drawing.Size(20, 18);
            this.lblTotalPlayersVisited.TabIndex = 3;
            this.lblTotalPlayersVisited.Text = "...";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(11, 182);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(124, 14);
            this.label39.TabIndex = 4;
            this.label39.Text = "Total Players Banned:";
            // 
            // lblTotalGuests
            // 
            this.lblTotalGuests.AutoSize = true;
            this.lblTotalGuests.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalGuests.Location = new System.Drawing.Point(168, 66);
            this.lblTotalGuests.Name = "lblTotalGuests";
            this.lblTotalGuests.Size = new System.Drawing.Size(20, 18);
            this.lblTotalGuests.TabIndex = 3;
            this.lblTotalGuests.Text = "...";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(47, 69);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(88, 14);
            this.label33.TabIndex = 2;
            this.label33.Text = "Guests Online:";
            // 
            // lblTotalPlayers
            // 
            this.lblTotalPlayers.AutoSize = true;
            this.lblTotalPlayers.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPlayers.Location = new System.Drawing.Point(168, 27);
            this.lblTotalPlayers.Name = "lblTotalPlayers";
            this.lblTotalPlayers.Size = new System.Drawing.Size(20, 18);
            this.lblTotalPlayers.TabIndex = 1;
            this.lblTotalPlayers.Text = "...";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(14, 144);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(121, 14);
            this.label35.TabIndex = 1;
            this.label35.Text = "Total Players Visited:";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(46, 30);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(89, 14);
            this.label31.TabIndex = 0;
            this.label31.Text = "Players Online:";
            // 
            // grpVersion
            // 
            this.grpVersion.BackColor = System.Drawing.SystemColors.Control;
            this.grpVersion.Controls.Add(this.lblLatestVersion);
            this.grpVersion.Controls.Add(this.lblCurVersion);
            this.grpVersion.Controls.Add(this.label30);
            this.grpVersion.Controls.Add(this.label29);
            this.grpVersion.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpVersion.Location = new System.Drawing.Point(23, 419);
            this.grpVersion.Name = "grpVersion";
            this.grpVersion.Size = new System.Drawing.Size(434, 109);
            this.grpVersion.TabIndex = 8;
            this.grpVersion.TabStop = false;
            this.grpVersion.Text = "MCDawn Version";
            // 
            // lblLatestVersion
            // 
            this.lblLatestVersion.AutoSize = true;
            this.lblLatestVersion.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLatestVersion.Location = new System.Drawing.Point(168, 67);
            this.lblLatestVersion.Name = "lblLatestVersion";
            this.lblLatestVersion.Size = new System.Drawing.Size(20, 18);
            this.lblLatestVersion.TabIndex = 3;
            this.lblLatestVersion.Text = "...";
            // 
            // lblCurVersion
            // 
            this.lblCurVersion.AutoSize = true;
            this.lblCurVersion.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurVersion.Location = new System.Drawing.Point(168, 30);
            this.lblCurVersion.Name = "lblCurVersion";
            this.lblCurVersion.Size = new System.Drawing.Size(20, 18);
            this.lblCurVersion.TabIndex = 2;
            this.lblCurVersion.Text = "...";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(48, 70);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(87, 14);
            this.label30.TabIndex = 1;
            this.label30.Text = "Latest Version:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(42, 33);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(93, 14);
            this.label29.TabIndex = 0;
            this.label29.Text = "Current Version:";
            // 
            // linkSite
            // 
            this.linkSite.AutoSize = true;
            this.linkSite.Location = new System.Drawing.Point(106, 531);
            this.linkSite.Name = "linkSite";
            this.linkSite.Size = new System.Drawing.Size(91, 13);
            this.linkSite.TabIndex = 7;
            this.linkSite.TabStop = true;
            this.linkSite.Text = "MCDawn Website";
            this.linkSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSite_LinkClicked);
            // 
            // linkForums
            // 
            this.linkForums.AutoSize = true;
            this.linkForums.Location = new System.Drawing.Point(274, 532);
            this.linkForums.Name = "linkForums";
            this.linkForums.Size = new System.Drawing.Size(86, 13);
            this.linkForums.TabIndex = 6;
            this.linkForums.TabStop = true;
            this.linkForums.Text = "MCDawn Forums";
            this.linkForums.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkForums_LinkClicked);
            // 
            // liStaff
            // 
            this.liStaff.FormattingEnabled = true;
            this.liStaff.Location = new System.Drawing.Point(504, 475);
            this.liStaff.Name = "liStaff";
            this.liStaff.Size = new System.Drawing.Size(254, 69);
            this.liStaff.TabIndex = 5;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(579, 458);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(113, 14);
            this.label28.TabIndex = 4;
            this.label28.Text = "MCDawn Staff Team";
            // 
            // liDevs
            // 
            this.liDevs.FormattingEnabled = true;
            this.liDevs.Location = new System.Drawing.Point(504, 284);
            this.liDevs.Name = "liDevs";
            this.liDevs.Size = new System.Drawing.Size(254, 82);
            this.liDevs.TabIndex = 3;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(552, 267);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(162, 14);
            this.label27.TabIndex = 2;
            this.label27.Text = "MCDawn Development Team";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(579, 23);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(91, 15);
            this.label26.TabIndex = 1;
            this.label26.Text = "About MCDawn";
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.SystemColors.Window;
            this.txtDescription.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDescription.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(480, 41);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(299, 213);
            this.txtDescription.TabIndex = 0;
            this.txtDescription.Text = resources.GetString("txtDescription.Text");
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Transparent;
            this.tabPage3.Controls.Add(this.txtErrors);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(786, 553);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Errors";
            // 
            // txtErrors
            // 
            this.txtErrors.BackColor = System.Drawing.Color.White;
            this.txtErrors.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtErrors.Location = new System.Drawing.Point(8, 6);
            this.txtErrors.Multiline = true;
            this.txtErrors.Name = "txtErrors";
            this.txtErrors.ReadOnly = true;
            this.txtErrors.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtErrors.Size = new System.Drawing.Size(772, 538);
            this.txtErrors.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.btnUpdateChangelog);
            this.tabPage2.Controls.Add(this.txtChangelog);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(786, 553);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Changelog";
            // 
            // btnUpdateChangelog
            // 
            this.btnUpdateChangelog.Location = new System.Drawing.Point(12, 6);
            this.btnUpdateChangelog.Name = "btnUpdateChangelog";
            this.btnUpdateChangelog.Size = new System.Drawing.Size(127, 23);
            this.btnUpdateChangelog.TabIndex = 1;
            this.btnUpdateChangelog.Text = "Update Changelog";
            this.btnUpdateChangelog.UseVisualStyleBackColor = true;
            this.btnUpdateChangelog.Click += new System.EventHandler(this.btnUpdateChangelog_Click);
            // 
            // txtChangelog
            // 
            this.txtChangelog.BackColor = System.Drawing.Color.White;
            this.txtChangelog.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtChangelog.Location = new System.Drawing.Point(3, 35);
            this.txtChangelog.Multiline = true;
            this.txtChangelog.Name = "txtChangelog";
            this.txtChangelog.ReadOnly = true;
            this.txtChangelog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChangelog.Size = new System.Drawing.Size(776, 512);
            this.txtChangelog.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Transparent;
            this.tabPage4.Controls.Add(this.txtSystem);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(786, 553);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "System";
            // 
            // txtSystem
            // 
            this.txtSystem.BackColor = System.Drawing.Color.White;
            this.txtSystem.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtSystem.Location = new System.Drawing.Point(7, 6);
            this.txtSystem.Multiline = true;
            this.txtSystem.Name = "txtSystem";
            this.txtSystem.ReadOnly = true;
            this.txtSystem.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSystem.Size = new System.Drawing.Size(772, 538);
            this.txtSystem.TabIndex = 1;
            // 
            // tabPage7
            // 
            this.tabPage7.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage7.Controls.Add(this.grpMapViewer);
            this.tabPage7.Controls.Add(this.grpMapEditor);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(786, 553);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Map Editor/Viewer";
            // 
            // grpMapViewer
            // 
            this.grpMapViewer.Controls.Add(this.txtMapViewerRotation);
            this.grpMapViewer.Controls.Add(this.txtMapViewerZ);
            this.grpMapViewer.Controls.Add(this.txtMapViewerY);
            this.grpMapViewer.Controls.Add(this.txtMapViewerX);
            this.grpMapViewer.Controls.Add(this.label24);
            this.grpMapViewer.Controls.Add(this.btnMapViewerSave);
            this.grpMapViewer.Controls.Add(this.label23);
            this.grpMapViewer.Controls.Add(this.btnMapViewerUpdate);
            this.grpMapViewer.Controls.Add(this.txtMapViewerLevelName);
            this.grpMapViewer.Controls.Add(this.label22);
            this.grpMapViewer.Controls.Add(this.picMapViewer);
            this.grpMapViewer.Location = new System.Drawing.Point(7, 98);
            this.grpMapViewer.Name = "grpMapViewer";
            this.grpMapViewer.Size = new System.Drawing.Size(772, 449);
            this.grpMapViewer.TabIndex = 1;
            this.grpMapViewer.TabStop = false;
            this.grpMapViewer.Text = "Map Viewer";
            // 
            // txtMapViewerZ
            // 
            this.txtMapViewerZ.Location = new System.Drawing.Point(540, 14);
            this.txtMapViewerZ.Name = "txtMapViewerZ";
            this.txtMapViewerZ.ReadOnly = true;
            this.txtMapViewerZ.Size = new System.Drawing.Size(34, 21);
            this.txtMapViewerZ.TabIndex = 19;
            this.txtMapViewerZ.Text = "0";
            // 
            // txtMapViewerY
            // 
            this.txtMapViewerY.Location = new System.Drawing.Point(500, 14);
            this.txtMapViewerY.Name = "txtMapViewerY";
            this.txtMapViewerY.ReadOnly = true;
            this.txtMapViewerY.Size = new System.Drawing.Size(34, 21);
            this.txtMapViewerY.TabIndex = 18;
            this.txtMapViewerY.Text = "0";
            // 
            // txtMapViewerX
            // 
            this.txtMapViewerX.Location = new System.Drawing.Point(460, 14);
            this.txtMapViewerX.Name = "txtMapViewerX";
            this.txtMapViewerX.ReadOnly = true;
            this.txtMapViewerX.Size = new System.Drawing.Size(34, 21);
            this.txtMapViewerX.TabIndex = 17;
            this.txtMapViewerX.Text = "0";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(371, 17);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(83, 13);
            this.label24.TabIndex = 16;
            this.label24.Text = "Level Size (X,Y,Z):";
            // 
            // btnMapViewerSave
            // 
            this.btnMapViewerSave.Location = new System.Drawing.Point(691, 12);
            this.btnMapViewerSave.Name = "btnMapViewerSave";
            this.btnMapViewerSave.Size = new System.Drawing.Size(75, 23);
            this.btnMapViewerSave.TabIndex = 15;
            this.btnMapViewerSave.Text = "Save Image";
            this.btnMapViewerSave.UseVisualStyleBackColor = true;
            this.btnMapViewerSave.Click += new System.EventHandler(this.btnMapViewerSave_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(184, 17);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(50, 13);
            this.label23.TabIndex = 13;
            this.label23.Text = "Rotation:";
            // 
            // btnMapViewerUpdate
            // 
            this.btnMapViewerUpdate.Location = new System.Drawing.Point(290, 12);
            this.btnMapViewerUpdate.Name = "btnMapViewerUpdate";
            this.btnMapViewerUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnMapViewerUpdate.TabIndex = 12;
            this.btnMapViewerUpdate.Text = "Update";
            this.btnMapViewerUpdate.UseVisualStyleBackColor = true;
            this.btnMapViewerUpdate.Click += new System.EventHandler(this.btnMapViewerUpdate_Click);
            // 
            // txtMapViewerLevelName
            // 
            this.txtMapViewerLevelName.Location = new System.Drawing.Point(78, 14);
            this.txtMapViewerLevelName.Name = "txtMapViewerLevelName";
            this.txtMapViewerLevelName.Size = new System.Drawing.Size(100, 21);
            this.txtMapViewerLevelName.TabIndex = 2;
            this.txtMapViewerLevelName.Text = "main";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(7, 17);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(65, 13);
            this.label22.TabIndex = 1;
            this.label22.Text = "Level Name:";
            // 
            // picMapViewer
            // 
            this.picMapViewer.Location = new System.Drawing.Point(6, 41);
            this.picMapViewer.Name = "picMapViewer";
            this.picMapViewer.Size = new System.Drawing.Size(760, 402);
            this.picMapViewer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picMapViewer.TabIndex = 0;
            this.picMapViewer.TabStop = false;
            // 
            // grpMapEditor
            // 
            this.grpMapEditor.Controls.Add(this.btnMapEditorUpdate);
            this.grpMapEditor.Controls.Add(this.btnMapEditorChange);
            this.grpMapEditor.Controls.Add(this.txtMapEditorChangeBlock);
            this.grpMapEditor.Controls.Add(this.label21);
            this.grpMapEditor.Controls.Add(this.txtMapEditorCurrentBlock);
            this.grpMapEditor.Controls.Add(this.label20);
            this.grpMapEditor.Controls.Add(this.txtMapEditorZ);
            this.grpMapEditor.Controls.Add(this.txtMapEditorY);
            this.grpMapEditor.Controls.Add(this.txtMapEditorX);
            this.grpMapEditor.Controls.Add(this.label19);
            this.grpMapEditor.Controls.Add(this.txtMapEditorLevelName);
            this.grpMapEditor.Controls.Add(this.label18);
            this.grpMapEditor.Location = new System.Drawing.Point(7, 3);
            this.grpMapEditor.Name = "grpMapEditor";
            this.grpMapEditor.Size = new System.Drawing.Size(772, 89);
            this.grpMapEditor.TabIndex = 0;
            this.grpMapEditor.TabStop = false;
            this.grpMapEditor.Text = "Map Editor";
            // 
            // btnMapEditorUpdate
            // 
            this.btnMapEditorUpdate.Location = new System.Drawing.Point(621, 18);
            this.btnMapEditorUpdate.Name = "btnMapEditorUpdate";
            this.btnMapEditorUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnMapEditorUpdate.TabIndex = 11;
            this.btnMapEditorUpdate.Text = "Update";
            this.btnMapEditorUpdate.UseVisualStyleBackColor = true;
            this.btnMapEditorUpdate.Click += new System.EventHandler(this.btnMapEditorUpdate_Click);
            // 
            // btnMapEditorChange
            // 
            this.btnMapEditorChange.Location = new System.Drawing.Point(245, 50);
            this.btnMapEditorChange.Name = "btnMapEditorChange";
            this.btnMapEditorChange.Size = new System.Drawing.Size(75, 23);
            this.btnMapEditorChange.TabIndex = 10;
            this.btnMapEditorChange.Text = "Change";
            this.btnMapEditorChange.UseVisualStyleBackColor = true;
            this.btnMapEditorChange.Click += new System.EventHandler(this.btnMapEditorChange_Click);
            // 
            // txtMapEditorChangeBlock
            // 
            this.txtMapEditorChangeBlock.Location = new System.Drawing.Point(139, 52);
            this.txtMapEditorChangeBlock.Name = "txtMapEditorChangeBlock";
            this.txtMapEditorChangeBlock.Size = new System.Drawing.Size(100, 21);
            this.txtMapEditorChangeBlock.TabIndex = 9;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 55);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(127, 13);
            this.label21.TabIndex = 8;
            this.label21.Text = "Change Selected Block to:";
            // 
            // txtMapEditorCurrentBlock
            // 
            this.txtMapEditorCurrentBlock.Location = new System.Drawing.Point(515, 20);
            this.txtMapEditorCurrentBlock.Name = "txtMapEditorCurrentBlock";
            this.txtMapEditorCurrentBlock.ReadOnly = true;
            this.txtMapEditorCurrentBlock.Size = new System.Drawing.Size(100, 21);
            this.txtMapEditorCurrentBlock.TabIndex = 7;
            this.txtMapEditorCurrentBlock.Text = "none";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(436, 23);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(73, 13);
            this.label20.TabIndex = 6;
            this.label20.Text = "Current Block:";
            // 
            // txtMapEditorZ
            // 
            this.txtMapEditorZ.Location = new System.Drawing.Point(386, 20);
            this.txtMapEditorZ.Name = "txtMapEditorZ";
            this.txtMapEditorZ.Size = new System.Drawing.Size(34, 21);
            this.txtMapEditorZ.TabIndex = 5;
            this.txtMapEditorZ.Text = "0";
            // 
            // txtMapEditorY
            // 
            this.txtMapEditorY.Location = new System.Drawing.Point(346, 20);
            this.txtMapEditorY.Name = "txtMapEditorY";
            this.txtMapEditorY.Size = new System.Drawing.Size(34, 21);
            this.txtMapEditorY.TabIndex = 4;
            this.txtMapEditorY.Text = "0";
            // 
            // txtMapEditorX
            // 
            this.txtMapEditorX.Location = new System.Drawing.Point(306, 20);
            this.txtMapEditorX.Name = "txtMapEditorX";
            this.txtMapEditorX.Size = new System.Drawing.Size(34, 21);
            this.txtMapEditorX.TabIndex = 3;
            this.txtMapEditorX.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(194, 23);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(106, 13);
            this.label19.TabIndex = 2;
            this.label19.Text = "Selected Block (X,Y,Z):";
            // 
            // txtMapEditorLevelName
            // 
            this.txtMapEditorLevelName.Location = new System.Drawing.Point(78, 20);
            this.txtMapEditorLevelName.Name = "txtMapEditorLevelName";
            this.txtMapEditorLevelName.Size = new System.Drawing.Size(100, 21);
            this.txtMapEditorLevelName.TabIndex = 1;
            this.txtMapEditorLevelName.Text = "main";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(7, 23);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "Level Name:";
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage6.Controls.Add(this.grpProperties);
            this.tabPage6.Controls.Add(this.btnKillPhysics);
            this.tabPage6.Controls.Add(this.btnUpdateLevelList);
            this.tabPage6.Controls.Add(this.grpNewDelRenLevel);
            this.tabPage6.Controls.Add(this.txtLevelLog);
            this.tabPage6.Controls.Add(this.grpLevels);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(786, 553);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Levels";
            // 
            // grpProperties
            // 
            this.grpProperties.Controls.Add(this.chkAutoLoad);
            this.grpProperties.Controls.Add(this.chkAllowGuns);
            this.grpProperties.Controls.Add(this.btnPropertiesSave);
            this.grpProperties.Controls.Add(this.chkRPChat);
            this.grpProperties.Controls.Add(this.txtDrown);
            this.grpProperties.Controls.Add(this.label16);
            this.grpProperties.Controls.Add(this.label15);
            this.grpProperties.Controls.Add(this.txtFall);
            this.grpProperties.Controls.Add(this.chkSurvivalDeath);
            this.grpProperties.Controls.Add(this.txtPhysicsOverload);
            this.grpProperties.Controls.Add(this.label14);
            this.grpProperties.Controls.Add(this.label11);
            this.grpProperties.Controls.Add(this.txtPhysicsSpeed);
            this.grpProperties.Controls.Add(this.chkInstant);
            this.grpProperties.Controls.Add(this.chkAutoPhysics);
            this.grpProperties.Controls.Add(this.chkUnload);
            this.grpProperties.Controls.Add(this.chkKillerBlocks);
            this.grpProperties.Controls.Add(this.chkGrassGrowing);
            this.grpProperties.Controls.Add(this.chkEdgeWater);
            this.grpProperties.Controls.Add(this.chkAnimalAI);
            this.grpProperties.Controls.Add(this.chkFiniteMode);
            this.grpProperties.Controls.Add(this.txtPhysics);
            this.grpProperties.Controls.Add(this.label9);
            this.grpProperties.Controls.Add(this.txtLevelMotd);
            this.grpProperties.Controls.Add(this.label3);
            this.grpProperties.Location = new System.Drawing.Point(476, 279);
            this.grpProperties.Name = "grpProperties";
            this.grpProperties.Size = new System.Drawing.Size(305, 265);
            this.grpProperties.TabIndex = 46;
            this.grpProperties.TabStop = false;
            this.grpProperties.Text = "Properties";
            // 
            // chkAutoLoad
            // 
            this.chkAutoLoad.AutoSize = true;
            this.chkAutoLoad.Location = new System.Drawing.Point(131, 241);
            this.chkAutoLoad.Name = "chkAutoLoad";
            this.chkAutoLoad.Size = new System.Drawing.Size(73, 17);
            this.chkAutoLoad.TabIndex = 25;
            this.chkAutoLoad.Text = "Auto Load";
            this.chkAutoLoad.UseVisualStyleBackColor = true;
            // 
            // chkAllowGuns
            // 
            this.chkAllowGuns.AutoSize = true;
            this.chkAllowGuns.Location = new System.Drawing.Point(214, 241);
            this.chkAllowGuns.Name = "chkAllowGuns";
            this.chkAllowGuns.Size = new System.Drawing.Size(78, 17);
            this.chkAllowGuns.TabIndex = 24;
            this.chkAllowGuns.Text = "Allow Guns";
            this.chkAllowGuns.UseVisualStyleBackColor = true;
            // 
            // btnPropertiesSave
            // 
            this.btnPropertiesSave.Location = new System.Drawing.Point(212, 50);
            this.btnPropertiesSave.Name = "btnPropertiesSave";
            this.btnPropertiesSave.Size = new System.Drawing.Size(78, 23);
            this.btnPropertiesSave.TabIndex = 23;
            this.btnPropertiesSave.Text = "Save";
            this.btnPropertiesSave.UseVisualStyleBackColor = true;
            this.btnPropertiesSave.Click += new System.EventHandler(this.btnPropertiesSave_Click);
            // 
            // chkRPChat
            // 
            this.chkRPChat.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRPChat.AutoSize = true;
            this.chkRPChat.Location = new System.Drawing.Point(143, 50);
            this.chkRPChat.Name = "chkRPChat";
            this.chkRPChat.Size = new System.Drawing.Size(53, 23);
            this.chkRPChat.TabIndex = 22;
            this.chkRPChat.Text = "RP Chat";
            this.chkRPChat.UseVisualStyleBackColor = true;
            // 
            // txtDrown
            // 
            this.txtDrown.Location = new System.Drawing.Point(143, 214);
            this.txtDrown.Name = "txtDrown";
            this.txtDrown.Size = new System.Drawing.Size(147, 21);
            this.txtDrown.TabIndex = 21;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(140, 198);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(38, 13);
            this.label16.TabIndex = 20;
            this.label16.Text = "Drown";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(140, 158);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(24, 13);
            this.label15.TabIndex = 19;
            this.label15.Text = "Fall";
            // 
            // txtFall
            // 
            this.txtFall.Location = new System.Drawing.Point(143, 174);
            this.txtFall.Name = "txtFall";
            this.txtFall.Size = new System.Drawing.Size(147, 21);
            this.txtFall.TabIndex = 18;
            // 
            // chkSurvivalDeath
            // 
            this.chkSurvivalDeath.AutoSize = true;
            this.chkSurvivalDeath.Location = new System.Drawing.Point(10, 227);
            this.chkSurvivalDeath.Name = "chkSurvivalDeath";
            this.chkSurvivalDeath.Size = new System.Drawing.Size(94, 17);
            this.chkSurvivalDeath.TabIndex = 17;
            this.chkSurvivalDeath.Text = "Survival Death";
            this.chkSurvivalDeath.UseVisualStyleBackColor = true;
            // 
            // txtPhysicsOverload
            // 
            this.txtPhysicsOverload.Location = new System.Drawing.Point(143, 134);
            this.txtPhysicsOverload.Name = "txtPhysicsOverload";
            this.txtPhysicsOverload.Size = new System.Drawing.Size(147, 21);
            this.txtPhysicsOverload.TabIndex = 16;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(140, 118);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(87, 13);
            this.label14.TabIndex = 15;
            this.label14.Text = "Physics Overload";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(140, 78);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "Physics Speed";
            // 
            // txtPhysicsSpeed
            // 
            this.txtPhysicsSpeed.Location = new System.Drawing.Point(143, 94);
            this.txtPhysicsSpeed.Name = "txtPhysicsSpeed";
            this.txtPhysicsSpeed.Size = new System.Drawing.Size(147, 21);
            this.txtPhysicsSpeed.TabIndex = 13;
            // 
            // chkInstant
            // 
            this.chkInstant.AutoSize = true;
            this.chkInstant.Location = new System.Drawing.Point(10, 208);
            this.chkInstant.Name = "chkInstant";
            this.chkInstant.Size = new System.Drawing.Size(100, 17);
            this.chkInstant.TabIndex = 11;
            this.chkInstant.Text = "Instant Building";
            this.chkInstant.UseVisualStyleBackColor = true;
            // 
            // chkAutoPhysics
            // 
            this.chkAutoPhysics.AutoSize = true;
            this.chkAutoPhysics.Location = new System.Drawing.Point(10, 188);
            this.chkAutoPhysics.Name = "chkAutoPhysics";
            this.chkAutoPhysics.Size = new System.Drawing.Size(85, 17);
            this.chkAutoPhysics.TabIndex = 10;
            this.chkAutoPhysics.Text = "Auto Physics";
            this.chkAutoPhysics.UseVisualStyleBackColor = true;
            // 
            // chkUnload
            // 
            this.chkUnload.AutoSize = true;
            this.chkUnload.Location = new System.Drawing.Point(10, 168);
            this.chkUnload.Name = "chkUnload";
            this.chkUnload.Size = new System.Drawing.Size(85, 17);
            this.chkUnload.TabIndex = 9;
            this.chkUnload.Text = "Auto-Unload";
            this.chkUnload.UseVisualStyleBackColor = true;
            // 
            // chkKillerBlocks
            // 
            this.chkKillerBlocks.AutoSize = true;
            this.chkKillerBlocks.Location = new System.Drawing.Point(10, 148);
            this.chkKillerBlocks.Name = "chkKillerBlocks";
            this.chkKillerBlocks.Size = new System.Drawing.Size(83, 17);
            this.chkKillerBlocks.TabIndex = 8;
            this.chkKillerBlocks.Text = "Killer Blocks";
            this.chkKillerBlocks.UseVisualStyleBackColor = true;
            // 
            // chkGrassGrowing
            // 
            this.chkGrassGrowing.AutoSize = true;
            this.chkGrassGrowing.Location = new System.Drawing.Point(10, 108);
            this.chkGrassGrowing.Name = "chkGrassGrowing";
            this.chkGrassGrowing.Size = new System.Drawing.Size(94, 17);
            this.chkGrassGrowing.TabIndex = 7;
            this.chkGrassGrowing.Text = "Grass Growing";
            this.chkGrassGrowing.UseVisualStyleBackColor = true;
            // 
            // chkEdgeWater
            // 
            this.chkEdgeWater.AutoSize = true;
            this.chkEdgeWater.Location = new System.Drawing.Point(10, 128);
            this.chkEdgeWater.Name = "chkEdgeWater";
            this.chkEdgeWater.Size = new System.Drawing.Size(80, 17);
            this.chkEdgeWater.TabIndex = 6;
            this.chkEdgeWater.Text = "Edge Water";
            this.chkEdgeWater.UseVisualStyleBackColor = true;
            // 
            // chkAnimalAI
            // 
            this.chkAnimalAI.AutoSize = true;
            this.chkAnimalAI.Location = new System.Drawing.Point(10, 89);
            this.chkAnimalAI.Name = "chkAnimalAI";
            this.chkAnimalAI.Size = new System.Drawing.Size(70, 17);
            this.chkAnimalAI.TabIndex = 5;
            this.chkAnimalAI.Text = "Animal AI";
            this.chkAnimalAI.UseVisualStyleBackColor = true;
            // 
            // chkFiniteMode
            // 
            this.chkFiniteMode.AutoSize = true;
            this.chkFiniteMode.Location = new System.Drawing.Point(10, 69);
            this.chkFiniteMode.Name = "chkFiniteMode";
            this.chkFiniteMode.Size = new System.Drawing.Size(82, 17);
            this.chkFiniteMode.TabIndex = 4;
            this.chkFiniteMode.Text = "Finite Mode";
            this.chkFiniteMode.UseVisualStyleBackColor = true;
            // 
            // txtPhysics
            // 
            this.txtPhysics.Location = new System.Drawing.Point(66, 45);
            this.txtPhysics.Name = "txtPhysics";
            this.txtPhysics.Size = new System.Drawing.Size(55, 21);
            this.txtPhysics.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Physics:";
            // 
            // txtLevelMotd
            // 
            this.txtLevelMotd.Location = new System.Drawing.Point(58, 18);
            this.txtLevelMotd.Name = "txtLevelMotd";
            this.txtLevelMotd.Size = new System.Drawing.Size(232, 21);
            this.txtLevelMotd.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "MOTD:";
            // 
            // btnKillPhysics
            // 
            this.btnKillPhysics.Location = new System.Drawing.Point(239, 287);
            this.btnKillPhysics.Name = "btnKillPhysics";
            this.btnKillPhysics.Size = new System.Drawing.Size(232, 27);
            this.btnKillPhysics.TabIndex = 48;
            this.btnKillPhysics.Text = "Kill All Physics";
            this.btnKillPhysics.UseVisualStyleBackColor = true;
            this.btnKillPhysics.Click += new System.EventHandler(this.btnKillPhysics_Click_1);
            // 
            // btnUpdateLevelList
            // 
            this.btnUpdateLevelList.Location = new System.Drawing.Point(8, 287);
            this.btnUpdateLevelList.Name = "btnUpdateLevelList";
            this.btnUpdateLevelList.Size = new System.Drawing.Size(224, 27);
            this.btnUpdateLevelList.TabIndex = 47;
            this.btnUpdateLevelList.Text = "Refresh/Update Level List";
            this.btnUpdateLevelList.UseVisualStyleBackColor = true;
            this.btnUpdateLevelList.Click += new System.EventHandler(this.btnUpdateLevelList_Click);
            // 
            // grpNewDelRenLevel
            // 
            this.grpNewDelRenLevel.Controls.Add(this.cmbLevelType);
            this.grpNewDelRenLevel.Controls.Add(this.btnNewLevel);
            this.grpNewDelRenLevel.Controls.Add(this.txtZDim);
            this.grpNewDelRenLevel.Controls.Add(this.txtYDim);
            this.grpNewDelRenLevel.Controls.Add(this.txtXDim);
            this.grpNewDelRenLevel.Controls.Add(this.label8);
            this.grpNewDelRenLevel.Controls.Add(this.label7);
            this.grpNewDelRenLevel.Controls.Add(this.label6);
            this.grpNewDelRenLevel.Controls.Add(this.label5);
            this.grpNewDelRenLevel.Controls.Add(this.label4);
            this.grpNewDelRenLevel.Controls.Add(this.txtLevelName);
            this.grpNewDelRenLevel.Controls.Add(this.grpNewLevel);
            this.grpNewDelRenLevel.Controls.Add(this.grpDeleteLevel);
            this.grpNewDelRenLevel.Controls.Add(this.grpRenameLevel);
            this.grpNewDelRenLevel.Location = new System.Drawing.Point(357, 3);
            this.grpNewDelRenLevel.Name = "grpNewDelRenLevel";
            this.grpNewDelRenLevel.Size = new System.Drawing.Size(453, 277);
            this.grpNewDelRenLevel.TabIndex = 45;
            this.grpNewDelRenLevel.TabStop = false;
            this.grpNewDelRenLevel.Text = "New/Delete/Rename Level";
            // 
            // cmbLevelType
            // 
            this.cmbLevelType.FormattingEnabled = true;
            this.cmbLevelType.Items.AddRange(new object[] {
            "Island",
            "Mountains",
            "Forest",
            "Ocean",
            "Flat",
            "Pixel",
            "Desert",
            "Nether",
            "Arctic",
            "Sphere"});
            this.cmbLevelType.Location = new System.Drawing.Point(63, 208);
            this.cmbLevelType.Name = "cmbLevelType";
            this.cmbLevelType.Size = new System.Drawing.Size(86, 21);
            this.cmbLevelType.TabIndex = 11;
            // 
            // btnNewLevel
            // 
            this.btnNewLevel.Location = new System.Drawing.Point(17, 239);
            this.btnNewLevel.Name = "btnNewLevel";
            this.btnNewLevel.Size = new System.Drawing.Size(132, 27);
            this.btnNewLevel.TabIndex = 10;
            this.btnNewLevel.Text = "Create Level";
            this.btnNewLevel.UseVisualStyleBackColor = true;
            this.btnNewLevel.Click += new System.EventHandler(this.btnNewLevel_Click);
            // 
            // txtZDim
            // 
            this.txtZDim.Location = new System.Drawing.Point(63, 172);
            this.txtZDim.Name = "txtZDim";
            this.txtZDim.Size = new System.Drawing.Size(86, 21);
            this.txtZDim.TabIndex = 9;
            // 
            // txtYDim
            // 
            this.txtYDim.Location = new System.Drawing.Point(63, 133);
            this.txtYDim.Name = "txtYDim";
            this.txtYDim.Size = new System.Drawing.Size(86, 21);
            this.txtYDim.TabIndex = 8;
            // 
            // txtXDim
            // 
            this.txtXDim.Location = new System.Drawing.Point(63, 92);
            this.txtXDim.Name = "txtXDim";
            this.txtXDim.Size = new System.Drawing.Size(86, 21);
            this.txtXDim.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 210);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Type:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Z Dim:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Y Dim:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "X Dim:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Name:";
            // 
            // txtLevelName
            // 
            this.txtLevelName.Location = new System.Drawing.Point(63, 54);
            this.txtLevelName.Name = "txtLevelName";
            this.txtLevelName.Size = new System.Drawing.Size(86, 21);
            this.txtLevelName.TabIndex = 1;
            // 
            // grpNewLevel
            // 
            this.grpNewLevel.Location = new System.Drawing.Point(7, 28);
            this.grpNewLevel.Name = "grpNewLevel";
            this.grpNewLevel.Size = new System.Drawing.Size(149, 242);
            this.grpNewLevel.TabIndex = 24;
            this.grpNewLevel.TabStop = false;
            this.grpNewLevel.Text = "New Level";
            // 
            // grpDeleteLevel
            // 
            this.grpDeleteLevel.Controls.Add(this.label10);
            this.grpDeleteLevel.Controls.Add(this.btnDeleteLevel);
            this.grpDeleteLevel.Controls.Add(this.txtDeleteLevelName);
            this.grpDeleteLevel.Location = new System.Drawing.Point(163, 28);
            this.grpDeleteLevel.Name = "grpDeleteLevel";
            this.grpDeleteLevel.Size = new System.Drawing.Size(263, 111);
            this.grpDeleteLevel.TabIndex = 25;
            this.grpDeleteLevel.TabStop = false;
            this.grpDeleteLevel.Text = "Delete Level";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 36);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Name:";
            // 
            // btnDeleteLevel
            // 
            this.btnDeleteLevel.Location = new System.Drawing.Point(24, 63);
            this.btnDeleteLevel.Name = "btnDeleteLevel";
            this.btnDeleteLevel.Size = new System.Drawing.Size(147, 27);
            this.btnDeleteLevel.TabIndex = 17;
            this.btnDeleteLevel.Text = "Delete Level";
            this.btnDeleteLevel.UseVisualStyleBackColor = true;
            this.btnDeleteLevel.Click += new System.EventHandler(this.btnDeleteLevel_Click);
            // 
            // txtDeleteLevelName
            // 
            this.txtDeleteLevelName.Location = new System.Drawing.Point(51, 33);
            this.txtDeleteLevelName.Name = "txtDeleteLevelName";
            this.txtDeleteLevelName.Size = new System.Drawing.Size(197, 21);
            this.txtDeleteLevelName.TabIndex = 15;
            // 
            // grpRenameLevel
            // 
            this.grpRenameLevel.Controls.Add(this.btnRenameLevel);
            this.grpRenameLevel.Controls.Add(this.label13);
            this.grpRenameLevel.Controls.Add(this.txtNewName);
            this.grpRenameLevel.Controls.Add(this.label12);
            this.grpRenameLevel.Controls.Add(this.txtCurName);
            this.grpRenameLevel.Location = new System.Drawing.Point(163, 145);
            this.grpRenameLevel.Name = "grpRenameLevel";
            this.grpRenameLevel.Size = new System.Drawing.Size(259, 125);
            this.grpRenameLevel.TabIndex = 26;
            this.grpRenameLevel.TabStop = false;
            this.grpRenameLevel.Text = "Rename Level";
            // 
            // btnRenameLevel
            // 
            this.btnRenameLevel.Location = new System.Drawing.Point(24, 93);
            this.btnRenameLevel.Name = "btnRenameLevel";
            this.btnRenameLevel.Size = new System.Drawing.Size(147, 27);
            this.btnRenameLevel.TabIndex = 23;
            this.btnRenameLevel.Text = "Rename Level";
            this.btnRenameLevel.UseVisualStyleBackColor = true;
            this.btnRenameLevel.Click += new System.EventHandler(this.btnRenameLevel_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 66);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(61, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "New Name:";
            // 
            // txtNewName
            // 
            this.txtNewName.Location = new System.Drawing.Point(87, 62);
            this.txtNewName.Name = "txtNewName";
            this.txtNewName.Size = new System.Drawing.Size(161, 21);
            this.txtNewName.TabIndex = 22;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 13);
            this.label12.TabIndex = 19;
            this.label12.Text = "Current Name:";
            // 
            // txtCurName
            // 
            this.txtCurName.Location = new System.Drawing.Point(105, 27);
            this.txtCurName.Name = "txtCurName";
            this.txtCurName.Size = new System.Drawing.Size(143, 21);
            this.txtCurName.TabIndex = 20;
            // 
            // txtLevelLog
            // 
            this.txtLevelLog.BackColor = System.Drawing.SystemColors.Window;
            this.txtLevelLog.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtLevelLog.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLevelLog.Location = new System.Drawing.Point(9, 320);
            this.txtLevelLog.Multiline = true;
            this.txtLevelLog.Name = "txtLevelLog";
            this.txtLevelLog.ReadOnly = true;
            this.txtLevelLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLevelLog.Size = new System.Drawing.Size(462, 224);
            this.txtLevelLog.TabIndex = 37;
            // 
            // grpLevels
            // 
            this.grpLevels.Controls.Add(this.btnUnloadLevel);
            this.grpLevels.Controls.Add(this.btnLoadLevel);
            this.grpLevels.Controls.Add(this.liLoadedLevels);
            this.grpLevels.Controls.Add(this.liUnloadedLevels);
            this.grpLevels.Location = new System.Drawing.Point(8, 3);
            this.grpLevels.Name = "grpLevels";
            this.grpLevels.Size = new System.Drawing.Size(342, 277);
            this.grpLevels.TabIndex = 39;
            this.grpLevels.TabStop = false;
            this.grpLevels.Text = "Levels";
            // 
            // btnUnloadLevel
            // 
            this.btnUnloadLevel.Location = new System.Drawing.Point(7, 239);
            this.btnUnloadLevel.Name = "btnUnloadLevel";
            this.btnUnloadLevel.Size = new System.Drawing.Size(160, 27);
            this.btnUnloadLevel.TabIndex = 44;
            this.btnUnloadLevel.Text = "Unload Level";
            this.btnUnloadLevel.UseVisualStyleBackColor = true;
            this.btnUnloadLevel.Click += new System.EventHandler(this.btnUnloadLevel_Click);
            // 
            // btnLoadLevel
            // 
            this.btnLoadLevel.Location = new System.Drawing.Point(174, 239);
            this.btnLoadLevel.Name = "btnLoadLevel";
            this.btnLoadLevel.Size = new System.Drawing.Size(157, 27);
            this.btnLoadLevel.TabIndex = 43;
            this.btnLoadLevel.Text = "Load Level";
            this.btnLoadLevel.UseVisualStyleBackColor = true;
            this.btnLoadLevel.Click += new System.EventHandler(this.btnLoadLevel_Click);
            // 
            // liLoadedLevels
            // 
            this.liLoadedLevels.ContextMenuStrip = this.playerStrip;
            this.liLoadedLevels.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.liLoadedLevels.FormattingEnabled = true;
            this.liLoadedLevels.Location = new System.Drawing.Point(7, 17);
            this.liLoadedLevels.Name = "liLoadedLevels";
            this.liLoadedLevels.ScrollAlwaysVisible = true;
            this.liLoadedLevels.Size = new System.Drawing.Size(159, 212);
            this.liLoadedLevels.TabIndex = 38;
            this.liLoadedLevels.Click += new System.EventHandler(this.liLoadedLevels_Click);
            // 
            // liUnloadedLevels
            // 
            this.liUnloadedLevels.ContextMenuStrip = this.playerStrip;
            this.liUnloadedLevels.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.liUnloadedLevels.FormattingEnabled = true;
            this.liUnloadedLevels.Location = new System.Drawing.Point(174, 17);
            this.liUnloadedLevels.Name = "liUnloadedLevels";
            this.liUnloadedLevels.ScrollAlwaysVisible = true;
            this.liUnloadedLevels.Size = new System.Drawing.Size(157, 212);
            this.liUnloadedLevels.TabIndex = 40;
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage5.Controls.Add(this.grpRanking);
            this.tabPage5.Controls.Add(this.grpChatCommands);
            this.tabPage5.Controls.Add(this.btnKick);
            this.tabPage5.Controls.Add(this.btnFreeze);
            this.tabPage5.Controls.Add(this.grpPlayerLogs);
            this.tabPage5.Controls.Add(this.btnInvincible);
            this.tabPage5.Controls.Add(this.btnWarn);
            this.tabPage5.Controls.Add(this.btnJoker);
            this.tabPage5.Controls.Add(this.grpInGameCmds);
            this.tabPage5.Controls.Add(this.grpBanCmds);
            this.tabPage5.Controls.Add(this.grpPlayerInfo);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(786, 553);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Players";
            // 
            // grpRanking
            // 
            this.grpRanking.Controls.Add(this.btnDemote);
            this.grpRanking.Controls.Add(this.btnPromote);
            this.grpRanking.Location = new System.Drawing.Point(366, 299);
            this.grpRanking.Name = "grpRanking";
            this.grpRanking.Size = new System.Drawing.Size(413, 66);
            this.grpRanking.TabIndex = 40;
            this.grpRanking.TabStop = false;
            this.grpRanking.Text = "Ranking";
            // 
            // btnDemote
            // 
            this.btnDemote.Location = new System.Drawing.Point(175, 23);
            this.btnDemote.Name = "btnDemote";
            this.btnDemote.Size = new System.Drawing.Size(163, 27);
            this.btnDemote.TabIndex = 1;
            this.btnDemote.Text = "Demote";
            this.btnDemote.UseVisualStyleBackColor = true;
            this.btnDemote.Click += new System.EventHandler(this.btnDemote_Click);
            // 
            // btnPromote
            // 
            this.btnPromote.Location = new System.Drawing.Point(12, 23);
            this.btnPromote.Name = "btnPromote";
            this.btnPromote.Size = new System.Drawing.Size(156, 27);
            this.btnPromote.TabIndex = 0;
            this.btnPromote.Text = "Promote";
            this.btnPromote.UseVisualStyleBackColor = true;
            this.btnPromote.Click += new System.EventHandler(this.btnPromote_Click);
            // 
            // grpChatCommands
            // 
            this.grpChatCommands.Controls.Add(this.btnChatModeration);
            this.grpChatCommands.Controls.Add(this.btnMute);
            this.grpChatCommands.Controls.Add(this.btnVoice);
            this.grpChatCommands.Location = new System.Drawing.Point(366, 228);
            this.grpChatCommands.Name = "grpChatCommands";
            this.grpChatCommands.Size = new System.Drawing.Size(413, 63);
            this.grpChatCommands.TabIndex = 39;
            this.grpChatCommands.TabStop = false;
            this.grpChatCommands.Text = "Chat ";
            // 
            // btnChatModeration
            // 
            this.btnChatModeration.Location = new System.Drawing.Point(12, 23);
            this.btnChatModeration.Name = "btnChatModeration";
            this.btnChatModeration.Size = new System.Drawing.Size(87, 27);
            this.btnChatModeration.TabIndex = 36;
            this.btnChatModeration.Text = "ChatMod On";
            this.btnChatModeration.UseVisualStyleBackColor = true;
            this.btnChatModeration.Click += new System.EventHandler(this.btnChatModeration_Click);
            // 
            // btnMute
            // 
            this.btnMute.Location = new System.Drawing.Point(138, 23);
            this.btnMute.Name = "btnMute";
            this.btnMute.Size = new System.Drawing.Size(87, 27);
            this.btnMute.TabIndex = 33;
            this.btnMute.Text = "Mute";
            this.btnMute.UseVisualStyleBackColor = true;
            this.btnMute.Click += new System.EventHandler(this.btnMute_Click);
            // 
            // btnVoice
            // 
            this.btnVoice.Location = new System.Drawing.Point(251, 23);
            this.btnVoice.Name = "btnVoice";
            this.btnVoice.Size = new System.Drawing.Size(87, 27);
            this.btnVoice.TabIndex = 0;
            this.btnVoice.Text = "Voice";
            this.btnVoice.UseVisualStyleBackColor = true;
            this.btnVoice.Click += new System.EventHandler(this.btnVoice_Click);
            // 
            // btnKick
            // 
            this.btnKick.Location = new System.Drawing.Point(498, 70);
            this.btnKick.Name = "btnKick";
            this.btnKick.Size = new System.Drawing.Size(87, 27);
            this.btnKick.TabIndex = 29;
            this.btnKick.Text = "Kick";
            this.btnKick.UseVisualStyleBackColor = true;
            this.btnKick.Click += new System.EventHandler(this.btnKick_Click);
            // 
            // btnFreeze
            // 
            this.btnFreeze.Location = new System.Drawing.Point(378, 70);
            this.btnFreeze.Name = "btnFreeze";
            this.btnFreeze.Size = new System.Drawing.Size(87, 27);
            this.btnFreeze.TabIndex = 28;
            this.btnFreeze.Text = "Freeze";
            this.btnFreeze.UseVisualStyleBackColor = true;
            this.btnFreeze.Click += new System.EventHandler(this.btnFreeze_Click);
            // 
            // grpPlayerLogs
            // 
            this.grpPlayerLogs.Controls.Add(this.liPlayers);
            this.grpPlayerLogs.Controls.Add(this.txtPlayerLog);
            this.grpPlayerLogs.Location = new System.Drawing.Point(7, 3);
            this.grpPlayerLogs.Name = "grpPlayerLogs";
            this.grpPlayerLogs.Size = new System.Drawing.Size(331, 547);
            this.grpPlayerLogs.TabIndex = 38;
            this.grpPlayerLogs.TabStop = false;
            this.grpPlayerLogs.Text = "Players and Logs";
            // 
            // liPlayers
            // 
            this.liPlayers.ContextMenuStrip = this.playerStrip;
            this.liPlayers.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.liPlayers.FormattingEnabled = true;
            this.liPlayers.Location = new System.Drawing.Point(7, 17);
            this.liPlayers.Name = "liPlayers";
            this.liPlayers.ScrollAlwaysVisible = true;
            this.liPlayers.Size = new System.Drawing.Size(315, 251);
            this.liPlayers.TabIndex = 38;
            this.liPlayers.SelectedIndexChanged += new System.EventHandler(this.liPlayers_SelectedIndexChanged);
            // 
            // txtPlayerLog
            // 
            this.txtPlayerLog.BackColor = System.Drawing.SystemColors.Window;
            this.txtPlayerLog.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtPlayerLog.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlayerLog.Location = new System.Drawing.Point(6, 274);
            this.txtPlayerLog.Multiline = true;
            this.txtPlayerLog.Name = "txtPlayerLog";
            this.txtPlayerLog.ReadOnly = true;
            this.txtPlayerLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPlayerLog.Size = new System.Drawing.Size(315, 267);
            this.txtPlayerLog.TabIndex = 37;
            // 
            // btnInvincible
            // 
            this.btnInvincible.Location = new System.Drawing.Point(617, 21);
            this.btnInvincible.Name = "btnInvincible";
            this.btnInvincible.Size = new System.Drawing.Size(87, 27);
            this.btnInvincible.TabIndex = 27;
            this.btnInvincible.Text = "Invincible";
            this.btnInvincible.UseVisualStyleBackColor = true;
            this.btnInvincible.Click += new System.EventHandler(this.btnInvincible_Click);
            // 
            // btnWarn
            // 
            this.btnWarn.Location = new System.Drawing.Point(498, 21);
            this.btnWarn.Name = "btnWarn";
            this.btnWarn.Size = new System.Drawing.Size(87, 27);
            this.btnWarn.TabIndex = 26;
            this.btnWarn.Text = "Warn";
            this.btnWarn.UseVisualStyleBackColor = true;
            this.btnWarn.Click += new System.EventHandler(this.btnWarn_Click);
            // 
            // btnJoker
            // 
            this.btnJoker.Location = new System.Drawing.Point(378, 21);
            this.btnJoker.Name = "btnJoker";
            this.btnJoker.Size = new System.Drawing.Size(87, 27);
            this.btnJoker.TabIndex = 25;
            this.btnJoker.Text = "Joker";
            this.btnJoker.UseVisualStyleBackColor = true;
            this.btnJoker.Click += new System.EventHandler(this.btnJoker_Click);
            // 
            // grpInGameCmds
            // 
            this.grpInGameCmds.Controls.Add(this.btnKill);
            this.grpInGameCmds.Controls.Add(this.btnSlap);
            this.grpInGameCmds.Controls.Add(this.btnJail);
            this.grpInGameCmds.Controls.Add(this.btnHide);
            this.grpInGameCmds.Location = new System.Drawing.Point(366, 3);
            this.grpInGameCmds.Name = "grpInGameCmds";
            this.grpInGameCmds.Size = new System.Drawing.Size(413, 153);
            this.grpInGameCmds.TabIndex = 34;
            this.grpInGameCmds.TabStop = false;
            this.grpInGameCmds.Text = "In-Game General";
            // 
            // btnKill
            // 
            this.btnKill.Location = new System.Drawing.Point(251, 114);
            this.btnKill.Name = "btnKill";
            this.btnKill.Size = new System.Drawing.Size(87, 27);
            this.btnKill.TabIndex = 4;
            this.btnKill.Text = "Kill";
            this.btnKill.UseVisualStyleBackColor = true;
            this.btnKill.Click += new System.EventHandler(this.btnKill_Click);
            // 
            // btnSlap
            // 
            this.btnSlap.Location = new System.Drawing.Point(12, 114);
            this.btnSlap.Name = "btnSlap";
            this.btnSlap.Size = new System.Drawing.Size(87, 27);
            this.btnSlap.TabIndex = 3;
            this.btnSlap.Text = "Slap";
            this.btnSlap.UseVisualStyleBackColor = true;
            this.btnSlap.Click += new System.EventHandler(this.btnSlap_Click);
            // 
            // btnJail
            // 
            this.btnJail.Location = new System.Drawing.Point(251, 67);
            this.btnJail.Name = "btnJail";
            this.btnJail.Size = new System.Drawing.Size(87, 27);
            this.btnJail.TabIndex = 2;
            this.btnJail.Text = "Jail";
            this.btnJail.UseVisualStyleBackColor = true;
            this.btnJail.Click += new System.EventHandler(this.btnJail_Click);
            // 
            // btnHide
            // 
            this.btnHide.Location = new System.Drawing.Point(132, 114);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(87, 27);
            this.btnHide.TabIndex = 1;
            this.btnHide.Text = "Hide";
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // grpBanCmds
            // 
            this.grpBanCmds.Controls.Add(this.btnXBan);
            this.grpBanCmds.Controls.Add(this.btnBanIP);
            this.grpBanCmds.Controls.Add(this.btnBan);
            this.grpBanCmds.Location = new System.Drawing.Point(366, 164);
            this.grpBanCmds.Name = "grpBanCmds";
            this.grpBanCmds.Size = new System.Drawing.Size(413, 58);
            this.grpBanCmds.TabIndex = 35;
            this.grpBanCmds.TabStop = false;
            this.grpBanCmds.Text = "Banning";
            // 
            // btnXBan
            // 
            this.btnXBan.Location = new System.Drawing.Point(12, 23);
            this.btnXBan.Name = "btnXBan";
            this.btnXBan.Size = new System.Drawing.Size(87, 27);
            this.btnXBan.TabIndex = 32;
            this.btnXBan.Text = "XBan";
            this.btnXBan.UseVisualStyleBackColor = true;
            this.btnXBan.Click += new System.EventHandler(this.btnXBan_Click);
            // 
            // btnBanIP
            // 
            this.btnBanIP.Location = new System.Drawing.Point(251, 23);
            this.btnBanIP.Name = "btnBanIP";
            this.btnBanIP.Size = new System.Drawing.Size(87, 27);
            this.btnBanIP.TabIndex = 31;
            this.btnBanIP.Text = "IP Ban";
            this.btnBanIP.UseVisualStyleBackColor = true;
            this.btnBanIP.Click += new System.EventHandler(this.btnBanIP_Click);
            // 
            // btnBan
            // 
            this.btnBan.Location = new System.Drawing.Point(132, 23);
            this.btnBan.Name = "btnBan";
            this.btnBan.Size = new System.Drawing.Size(87, 27);
            this.btnBan.TabIndex = 30;
            this.btnBan.Text = "Ban";
            this.btnBan.UseVisualStyleBackColor = true;
            this.btnBan.Click += new System.EventHandler(this.btnBan_Click);
            // 
            // grpPlayerInfo
            // 
            this.grpPlayerInfo.Controls.Add(this.txtStatus);
            this.grpPlayerInfo.Controls.Add(this.lblStatus);
            this.grpPlayerInfo.Controls.Add(this.txtDeaths);
            this.grpPlayerInfo.Controls.Add(this.lblDeaths);
            this.grpPlayerInfo.Controls.Add(this.txtKicks);
            this.grpPlayerInfo.Controls.Add(this.lblKicks);
            this.grpPlayerInfo.Controls.Add(this.txtModified);
            this.grpPlayerInfo.Controls.Add(this.lblModified);
            this.grpPlayerInfo.Controls.Add(this.txtLevel);
            this.grpPlayerInfo.Controls.Add(this.lblLevel);
            this.grpPlayerInfo.Controls.Add(this.txtRank);
            this.grpPlayerInfo.Controls.Add(this.lblRank);
            this.grpPlayerInfo.Controls.Add(this.txtIP);
            this.grpPlayerInfo.Controls.Add(this.lblIP);
            this.grpPlayerInfo.Controls.Add(this.txtPlayerName);
            this.grpPlayerInfo.Controls.Add(this.lblName);
            this.grpPlayerInfo.Location = new System.Drawing.Point(366, 372);
            this.grpPlayerInfo.Name = "grpPlayerInfo";
            this.grpPlayerInfo.Size = new System.Drawing.Size(413, 151);
            this.grpPlayerInfo.TabIndex = 42;
            this.grpPlayerInfo.TabStop = false;
            this.grpPlayerInfo.Text = "Player Info";
            // 
            // txtStatus
            // 
            this.txtStatus.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtStatus.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatus.Location = new System.Drawing.Point(344, 115);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(63, 21);
            this.txtStatus.TabIndex = 56;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(298, 117);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 55;
            this.lblStatus.Text = "Status:";
            // 
            // txtDeaths
            // 
            this.txtDeaths.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDeaths.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeaths.Location = new System.Drawing.Point(187, 114);
            this.txtDeaths.Name = "txtDeaths";
            this.txtDeaths.ReadOnly = true;
            this.txtDeaths.Size = new System.Drawing.Size(103, 21);
            this.txtDeaths.TabIndex = 54;
            // 
            // lblDeaths
            // 
            this.lblDeaths.AutoSize = true;
            this.lblDeaths.Location = new System.Drawing.Point(137, 117);
            this.lblDeaths.Name = "lblDeaths";
            this.lblDeaths.Size = new System.Drawing.Size(44, 13);
            this.lblDeaths.TabIndex = 53;
            this.lblDeaths.Text = "Deaths:";
            // 
            // txtKicks
            // 
            this.txtKicks.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtKicks.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKicks.Location = new System.Drawing.Point(48, 114);
            this.txtKicks.Name = "txtKicks";
            this.txtKicks.ReadOnly = true;
            this.txtKicks.Size = new System.Drawing.Size(83, 21);
            this.txtKicks.TabIndex = 52;
            // 
            // lblKicks
            // 
            this.lblKicks.AutoSize = true;
            this.lblKicks.Location = new System.Drawing.Point(8, 118);
            this.lblKicks.Name = "lblKicks";
            this.lblKicks.Size = new System.Drawing.Size(34, 13);
            this.lblKicks.TabIndex = 51;
            this.lblKicks.Text = "Kicks:";
            // 
            // txtModified
            // 
            this.txtModified.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtModified.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModified.Location = new System.Drawing.Point(245, 84);
            this.txtModified.Name = "txtModified";
            this.txtModified.ReadOnly = true;
            this.txtModified.Size = new System.Drawing.Size(162, 21);
            this.txtModified.TabIndex = 50;
            // 
            // lblModified
            // 
            this.lblModified.AutoSize = true;
            this.lblModified.Location = new System.Drawing.Point(187, 87);
            this.lblModified.Name = "lblModified";
            this.lblModified.Size = new System.Drawing.Size(52, 13);
            this.lblModified.TabIndex = 49;
            this.lblModified.Text = "Modified:";
            // 
            // txtLevel
            // 
            this.txtLevel.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtLevel.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLevel.Location = new System.Drawing.Point(49, 83);
            this.txtLevel.Name = "txtLevel";
            this.txtLevel.ReadOnly = true;
            this.txtLevel.Size = new System.Drawing.Size(129, 21);
            this.txtLevel.TabIndex = 48;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(8, 87);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(35, 13);
            this.lblLevel.TabIndex = 47;
            this.lblLevel.Text = "Level:";
            // 
            // txtRank
            // 
            this.txtRank.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtRank.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRank.Location = new System.Drawing.Point(269, 48);
            this.txtRank.Name = "txtRank";
            this.txtRank.ReadOnly = true;
            this.txtRank.Size = new System.Drawing.Size(138, 21);
            this.txtRank.TabIndex = 46;
            // 
            // lblRank
            // 
            this.lblRank.AutoSize = true;
            this.lblRank.Location = new System.Drawing.Point(230, 52);
            this.lblRank.Name = "lblRank";
            this.lblRank.Size = new System.Drawing.Size(33, 13);
            this.lblRank.TabIndex = 45;
            this.lblRank.Text = "Rank:";
            // 
            // txtIP
            // 
            this.txtIP.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtIP.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIP.Location = new System.Drawing.Point(33, 48);
            this.txtIP.Name = "txtIP";
            this.txtIP.ReadOnly = true;
            this.txtIP.Size = new System.Drawing.Size(189, 21);
            this.txtIP.TabIndex = 44;
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(8, 52);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(19, 13);
            this.lblIP.TabIndex = 43;
            this.lblIP.Text = "IP:";
            // 
            // txtPlayerName
            // 
            this.txtPlayerName.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtPlayerName.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlayerName.Location = new System.Drawing.Point(52, 16);
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.ReadOnly = true;
            this.txtPlayerName.Size = new System.Drawing.Size(355, 21);
            this.txtPlayerName.TabIndex = 42;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(8, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 41;
            this.lblName.Text = "Name:";
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.groupBox4);
            this.tabPage9.Controls.Add(this.groupBox3);
            this.tabPage9.Controls.Add(this.grpAdmin);
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(786, 553);
            this.tabPage9.TabIndex = 8;
            this.tabPage9.Text = "Chat";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label45);
            this.groupBox4.Controls.Add(this.txtOpInput);
            this.groupBox4.Controls.Add(this.txtOpLog);
            this.groupBox4.Location = new System.Drawing.Point(7, 188);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(772, 175);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "OP Chat";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(6, 152);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(35, 13);
            this.label45.TabIndex = 2;
            this.label45.Text = "Input:";
            // 
            // txtOpInput
            // 
            this.txtOpInput.Location = new System.Drawing.Point(47, 149);
            this.txtOpInput.Name = "txtOpInput";
            this.txtOpInput.Size = new System.Drawing.Size(719, 21);
            this.txtOpInput.TabIndex = 1;
            this.txtOpInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOpInput_KeyDown);
            // 
            // txtOpLog
            // 
            this.txtOpLog.Location = new System.Drawing.Point(6, 20);
            this.txtOpLog.Multiline = true;
            this.txtOpLog.Name = "txtOpLog";
            this.txtOpLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOpLog.Size = new System.Drawing.Size(760, 123);
            this.txtOpLog.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label44);
            this.groupBox3.Controls.Add(this.txtGlobalInput);
            this.groupBox3.Controls.Add(this.txtGlobalLog);
            this.groupBox3.Location = new System.Drawing.Point(7, 372);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(772, 175);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Global Chat";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(6, 152);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(35, 13);
            this.label44.TabIndex = 2;
            this.label44.Text = "Input:";
            // 
            // txtGlobalInput
            // 
            this.txtGlobalInput.Location = new System.Drawing.Point(47, 149);
            this.txtGlobalInput.Name = "txtGlobalInput";
            this.txtGlobalInput.Size = new System.Drawing.Size(719, 21);
            this.txtGlobalInput.TabIndex = 1;
            this.txtGlobalInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGlobalInput_KeyDown);
            // 
            // txtGlobalLog
            // 
            this.txtGlobalLog.Location = new System.Drawing.Point(6, 20);
            this.txtGlobalLog.Multiline = true;
            this.txtGlobalLog.Name = "txtGlobalLog";
            this.txtGlobalLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGlobalLog.Size = new System.Drawing.Size(760, 123);
            this.txtGlobalLog.TabIndex = 0;
            // 
            // grpAdmin
            // 
            this.grpAdmin.Controls.Add(this.label43);
            this.grpAdmin.Controls.Add(this.txtAdminInput);
            this.grpAdmin.Controls.Add(this.txtAdminLog);
            this.grpAdmin.Location = new System.Drawing.Point(7, 3);
            this.grpAdmin.Name = "grpAdmin";
            this.grpAdmin.Size = new System.Drawing.Size(772, 175);
            this.grpAdmin.TabIndex = 0;
            this.grpAdmin.TabStop = false;
            this.grpAdmin.Text = "Admin Chat";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(6, 152);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(35, 13);
            this.label43.TabIndex = 2;
            this.label43.Text = "Input:";
            // 
            // txtAdminInput
            // 
            this.txtAdminInput.Location = new System.Drawing.Point(47, 149);
            this.txtAdminInput.Name = "txtAdminInput";
            this.txtAdminInput.Size = new System.Drawing.Size(719, 21);
            this.txtAdminInput.TabIndex = 1;
            this.txtAdminInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAdminInput_KeyDown);
            // 
            // txtAdminLog
            // 
            this.txtAdminLog.Location = new System.Drawing.Point(6, 20);
            this.txtAdminLog.Multiline = true;
            this.txtAdminLog.Name = "txtAdminLog";
            this.txtAdminLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAdminLog.Size = new System.Drawing.Size(760, 123);
            this.txtAdminLog.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.chkChatColors);
            this.tabPage1.Controls.Add(this.btnPlay);
            this.tabPage1.Controls.Add(this.label38);
            this.tabPage1.Controls.Add(this.label36);
            this.tabPage1.Controls.Add(this.liPlayerBots);
            this.tabPage1.Controls.Add(this.chkConsoleSounds);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.btnProperties);
            this.tabPage1.Controls.Add(this.chkMaintenance);
            this.tabPage1.Controls.Add(this.btnRestart);
            this.tabPage1.Controls.Add(this.liMaps);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.btnClose);
            this.tabPage1.Controls.Add(this.gBCommands);
            this.tabPage1.Controls.Add(this.gBChat);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtHost);
            this.tabPage1.Controls.Add(this.txtCommands);
            this.tabPage1.Controls.Add(this.txtInput);
            this.tabPage1.Controls.Add(this.txtUrl);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.liClients);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(786, 553);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            // 
            // chkChatColors
            // 
            this.chkChatColors.AutoSize = true;
            this.chkChatColors.Location = new System.Drawing.Point(499, 201);
            this.chkChatColors.Name = "chkChatColors";
            this.chkChatColors.Size = new System.Drawing.Size(80, 17);
            this.chkChatColors.TabIndex = 47;
            this.chkChatColors.Text = "Chat Colors";
            this.chkChatColors.UseVisualStyleBackColor = true;
            this.chkChatColors.CheckedChanged += new System.EventHandler(this.chkChatColors_CheckedChanged);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(440, 8);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(45, 23);
            this.btnPlay.TabIndex = 46;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(12, 13);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(28, 13);
            this.label38.TabIndex = 45;
            this.label38.Text = "URL:";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(592, 42);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(93, 13);
            this.label36.TabIndex = 44;
            this.label36.Text = "Active Player Bots:";
            // 
            // liPlayerBots
            // 
            this.liPlayerBots.ContextMenuStrip = this.playerStrip;
            this.liPlayerBots.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.liPlayerBots.FormattingEnabled = true;
            this.liPlayerBots.Location = new System.Drawing.Point(551, 59);
            this.liPlayerBots.Name = "liPlayerBots";
            this.liPlayerBots.ScrollAlwaysVisible = true;
            this.liPlayerBots.Size = new System.Drawing.Size(170, 134);
            this.liPlayerBots.TabIndex = 43;
            // 
            // chkConsoleSounds
            // 
            this.chkConsoleSounds.AutoSize = true;
            this.chkConsoleSounds.Location = new System.Drawing.Point(585, 201);
            this.chkConsoleSounds.Name = "chkConsoleSounds";
            this.chkConsoleSounds.Size = new System.Drawing.Size(60, 17);
            this.chkConsoleSounds.TabIndex = 42;
            this.chkConsoleSounds.Text = "Sounds";
            this.chkConsoleSounds.UseVisualStyleBackColor = true;
            this.chkConsoleSounds.CheckedChanged += new System.EventHandler(this.chkConsoleSounds_CheckedChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(497, 227);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(78, 13);
            this.label17.TabIndex = 41;
            this.label17.Text = "Console Name:";
            // 
            // btnProperties
            // 
            this.btnProperties.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProperties.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProperties.Location = new System.Drawing.Point(497, 6);
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(65, 27);
            this.btnProperties.TabIndex = 34;
            this.btnProperties.Text = "Properties";
            this.btnProperties.UseVisualStyleBackColor = true;
            this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click_1);
            // 
            // chkMaintenance
            // 
            this.chkMaintenance.AutoSize = true;
            this.chkMaintenance.Location = new System.Drawing.Point(651, 201);
            this.chkMaintenance.Name = "chkMaintenance";
            this.chkMaintenance.Size = new System.Drawing.Size(118, 17);
            this.chkMaintenance.TabIndex = 38;
            this.chkMaintenance.Text = "Maintenance Mode";
            this.chkMaintenance.UseVisualStyleBackColor = true;
            this.chkMaintenance.CheckedChanged += new System.EventHandler(this.chkMaintenance_CheckedChanged);
            // 
            // btnRestart
            // 
            this.btnRestart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestart.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestart.Location = new System.Drawing.Point(639, 6);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(65, 27);
            this.btnRestart.TabIndex = 37;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // liMaps
            // 
            this.liMaps.ContextMenuStrip = this.mapsStrip;
            this.liMaps.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.liMaps.FormattingEnabled = true;
            this.liMaps.Location = new System.Drawing.Point(255, 39);
            this.liMaps.Name = "liMaps";
            this.liMaps.ScrollAlwaysVisible = true;
            this.liMaps.Size = new System.Drawing.Size(230, 212);
            this.liMaps.TabIndex = 33;
            this.liMaps.MouseDown += new System.Windows.Forms.MouseEventHandler(this.liMaps_MouseDown);
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(568, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 27);
            this.button1.TabIndex = 36;
            this.button1.Text = "Updater";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(710, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(65, 27);
            this.btnClose.TabIndex = 35;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // gBCommands
            // 
            this.gBCommands.Controls.Add(this.txtCommandsUsed);
            this.gBCommands.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gBCommands.Location = new System.Drawing.Point(491, 257);
            this.gBCommands.Name = "gBCommands";
            this.gBCommands.Size = new System.Drawing.Size(278, 262);
            this.gBCommands.TabIndex = 34;
            this.gBCommands.TabStop = false;
            this.gBCommands.Text = "Commands";
            // 
            // txtCommandsUsed
            // 
            this.txtCommandsUsed.BackColor = System.Drawing.Color.White;
            this.txtCommandsUsed.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtCommandsUsed.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommandsUsed.Location = new System.Drawing.Point(7, 23);
            this.txtCommandsUsed.Multiline = true;
            this.txtCommandsUsed.Name = "txtCommandsUsed";
            this.txtCommandsUsed.ReadOnly = true;
            this.txtCommandsUsed.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCommandsUsed.Size = new System.Drawing.Size(262, 233);
            this.txtCommandsUsed.TabIndex = 0;
            this.txtCommandsUsed.TextChanged += new System.EventHandler(this.txtCommandsUsed_TextChanged);
            // 
            // gBChat
            // 
            this.gBChat.Controls.Add(this.txtLog);
            this.gBChat.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gBChat.Location = new System.Drawing.Point(15, 257);
            this.gBChat.Name = "gBChat";
            this.gBChat.Size = new System.Drawing.Size(470, 262);
            this.gBChat.TabIndex = 32;
            this.gBChat.TabStop = false;
            this.gBChat.Text = "Chat";
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.Window;
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLog.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(6, 20);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtLog.Size = new System.Drawing.Size(458, 236);
            this.txtLog.TabIndex = 2;
            this.txtLog.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(491, 528);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Command:";
            // 
            // txtHost
            // 
            this.txtHost.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHost.Location = new System.Drawing.Point(581, 224);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(179, 21);
            this.txtHost.TabIndex = 28;
            this.txtHost.Text = "Alive";
            this.txtHost.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtHost.TextChanged += new System.EventHandler(this.txtHost_TextChanged);
            this.txtHost.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommands_KeyDown);
            // 
            // txtCommands
            // 
            this.txtCommands.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommands.Location = new System.Drawing.Point(554, 525);
            this.txtCommands.Name = "txtCommands";
            this.txtCommands.Size = new System.Drawing.Size(215, 21);
            this.txtCommands.TabIndex = 28;
            this.txtCommands.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommands_KeyDown);
            // 
            // txtInput
            // 
            this.txtInput.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInput.Location = new System.Drawing.Point(59, 525);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(426, 21);
            this.txtInput.TabIndex = 27;
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
            // 
            // txtUrl
            // 
            this.txtUrl.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtUrl.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrl.Location = new System.Drawing.Point(46, 10);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.ReadOnly = true;
            this.txtUrl.Size = new System.Drawing.Size(388, 21);
            this.txtUrl.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 528);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Chat:";
            // 
            // liClients
            // 
            this.liClients.ContextMenuStrip = this.playerStrip;
            this.liClients.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.liClients.FormattingEnabled = true;
            this.liClients.Location = new System.Drawing.Point(15, 39);
            this.liClients.Name = "liClients";
            this.liClients.ScrollAlwaysVisible = true;
            this.liClients.Size = new System.Drawing.Size(234, 212);
            this.liClients.TabIndex = 23;
            this.liClients.MouseDown += new System.Windows.Forms.MouseEventHandler(this.liClients_MouseDown);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage9);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Controls.Add(this.tabPage10);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControl1.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.tabControl1.Location = new System.Drawing.Point(1, 14);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(794, 579);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // tabPage10
            // 
            this.tabPage10.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage10.Controls.Add(this.label47);
            this.tabPage10.Controls.Add(this.grpRCUsers);
            this.tabPage10.Controls.Add(this.grpRCSettings);
            this.tabPage10.Location = new System.Drawing.Point(4, 22);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Size = new System.Drawing.Size(786, 553);
            this.tabPage10.TabIndex = 9;
            this.tabPage10.Text = "Remote";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(281, 39);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(73, 13);
            this.label47.TabIndex = 3;
            this.label47.Text = "Server RC Key:";
            // 
            // grpRCUsers
            // 
            this.grpRCUsers.Controls.Add(this.btnRemoveRCUser);
            this.grpRCUsers.Controls.Add(this.btnAddRCUser);
            this.grpRCUsers.Controls.Add(this.liRCUsers);
            this.grpRCUsers.Controls.Add(this.label46);
            this.grpRCUsers.Location = new System.Drawing.Point(3, 3);
            this.grpRCUsers.Name = "grpRCUsers";
            this.grpRCUsers.Size = new System.Drawing.Size(245, 544);
            this.grpRCUsers.TabIndex = 2;
            this.grpRCUsers.TabStop = false;
            this.grpRCUsers.Text = "Users";
            // 
            // btnRemoveRCUser
            // 
            this.btnRemoveRCUser.Location = new System.Drawing.Point(142, 48);
            this.btnRemoveRCUser.Name = "btnRemoveRCUser";
            this.btnRemoveRCUser.Size = new System.Drawing.Size(86, 23);
            this.btnRemoveRCUser.TabIndex = 2;
            this.btnRemoveRCUser.Text = "Remove User";
            this.btnRemoveRCUser.UseVisualStyleBackColor = true;
            this.btnRemoveRCUser.Click += new System.EventHandler(this.btnRemoveRCUser_Click);
            // 
            // btnAddRCUser
            // 
            this.btnAddRCUser.Location = new System.Drawing.Point(9, 48);
            this.btnAddRCUser.Name = "btnAddRCUser";
            this.btnAddRCUser.Size = new System.Drawing.Size(86, 23);
            this.btnAddRCUser.TabIndex = 0;
            this.btnAddRCUser.Text = "Add User";
            this.btnAddRCUser.UseVisualStyleBackColor = true;
            this.btnAddRCUser.Click += new System.EventHandler(this.btnAddRCUser_Click);
            // 
            // liRCUsers
            // 
            this.liRCUsers.FormattingEnabled = true;
            this.liRCUsers.Location = new System.Drawing.Point(9, 85);
            this.liRCUsers.Name = "liRCUsers";
            this.liRCUsers.Size = new System.Drawing.Size(219, 446);
            this.liRCUsers.TabIndex = 1;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(60, 24);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(113, 13);
            this.label46.TabIndex = 0;
            this.label46.Text = "Remote Console Users";
            // 
            // grpRCSettings
            // 
            this.grpRCSettings.Controls.Add(this.lblRCCheckPortResult);
            this.grpRCSettings.Controls.Add(this.btnRCPortCheck);
            this.grpRCSettings.Controls.Add(this.chkUseRemote);
            this.grpRCSettings.Controls.Add(this.grpConnectedRCs);
            this.grpRCSettings.Controls.Add(this.txtRCPort);
            this.grpRCSettings.Controls.Add(this.label48);
            this.grpRCSettings.Controls.Add(this.btnGenerateRCKey);
            this.grpRCSettings.Controls.Add(this.txtRCKey);
            this.grpRCSettings.Location = new System.Drawing.Point(254, 3);
            this.grpRCSettings.Name = "grpRCSettings";
            this.grpRCSettings.Size = new System.Drawing.Size(529, 544);
            this.grpRCSettings.TabIndex = 4;
            this.grpRCSettings.TabStop = false;
            this.grpRCSettings.Text = "Settings / Connected Remotes";
            // 
            // lblRCCheckPortResult
            // 
            this.lblRCCheckPortResult.AutoSize = true;
            this.lblRCCheckPortResult.Location = new System.Drawing.Point(320, 63);
            this.lblRCCheckPortResult.Name = "lblRCCheckPortResult";
            this.lblRCCheckPortResult.Size = new System.Drawing.Size(133, 13);
            this.lblRCCheckPortResult.TabIndex = 9;
            this.lblRCCheckPortResult.Text = "Port Check not started yet!";
            // 
            // btnRCPortCheck
            // 
            this.btnRCPortCheck.Location = new System.Drawing.Point(244, 58);
            this.btnRCPortCheck.Name = "btnRCPortCheck";
            this.btnRCPortCheck.Size = new System.Drawing.Size(70, 23);
            this.btnRCPortCheck.TabIndex = 8;
            this.btnRCPortCheck.Text = "Check Port";
            this.btnRCPortCheck.UseVisualStyleBackColor = true;
            this.btnRCPortCheck.Click += new System.EventHandler(this.btnRCPortCheck_Click);
            // 
            // chkUseRemote
            // 
            this.chkUseRemote.AutoSize = true;
            this.chkUseRemote.Location = new System.Drawing.Point(394, 20);
            this.chkUseRemote.Name = "chkUseRemote";
            this.chkUseRemote.Size = new System.Drawing.Size(123, 17);
            this.chkUseRemote.TabIndex = 7;
            this.chkUseRemote.Text = "Use Remote Console";
            this.chkUseRemote.UseVisualStyleBackColor = true;
            this.chkUseRemote.CheckedChanged += new System.EventHandler(this.chkUseRemote_CheckedChanged);
            // 
            // grpConnectedRCs
            // 
            this.grpConnectedRCs.Controls.Add(this.textBox1);
            this.grpConnectedRCs.Controls.Add(this.label51);
            this.grpConnectedRCs.Controls.Add(this.rtbDescription);
            this.grpConnectedRCs.Controls.Add(this.txtRCWhisper);
            this.grpConnectedRCs.Controls.Add(this.txtRCDisc);
            this.grpConnectedRCs.Controls.Add(this.label50);
            this.grpConnectedRCs.Controls.Add(this.label49);
            this.grpConnectedRCs.Controls.Add(this.liConnectedRCs);
            this.grpConnectedRCs.Location = new System.Drawing.Point(6, 87);
            this.grpConnectedRCs.Name = "grpConnectedRCs";
            this.grpConnectedRCs.Size = new System.Drawing.Size(517, 444);
            this.grpConnectedRCs.TabIndex = 6;
            this.grpConnectedRCs.TabStop = false;
            this.grpConnectedRCs.Text = "Connected Remote Consoles";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(255, 201);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(256, 21);
            this.textBox1.TabIndex = 10;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(252, 185);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(96, 13);
            this.label51.TabIndex = 9;
            this.label51.Text = "Change Host State:";
            // 
            // rtbDescription
            // 
            this.rtbDescription.BackColor = System.Drawing.SystemColors.Control;
            this.rtbDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbDescription.Location = new System.Drawing.Point(252, 285);
            this.rtbDescription.Name = "rtbDescription";
            this.rtbDescription.Size = new System.Drawing.Size(259, 138);
            this.rtbDescription.TabIndex = 8;
            this.rtbDescription.Text = resources.GetString("rtbDescription.Text");
            // 
            // txtRCWhisper
            // 
            this.txtRCWhisper.Location = new System.Drawing.Point(255, 138);
            this.txtRCWhisper.Name = "txtRCWhisper";
            this.txtRCWhisper.Size = new System.Drawing.Size(256, 21);
            this.txtRCWhisper.TabIndex = 7;
            // 
            // txtRCDisc
            // 
            this.txtRCDisc.Location = new System.Drawing.Point(252, 79);
            this.txtRCDisc.Name = "txtRCDisc";
            this.txtRCDisc.Size = new System.Drawing.Size(259, 21);
            this.txtRCDisc.TabIndex = 6;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(252, 122);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(50, 13);
            this.label50.TabIndex = 2;
            this.label50.Text = "Whisper:";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(252, 63);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(86, 13);
            this.label49.TabIndex = 1;
            this.label49.Text = "Kick/Disconnect:";
            // 
            // liConnectedRCs
            // 
            this.liConnectedRCs.FormattingEnabled = true;
            this.liConnectedRCs.Location = new System.Drawing.Point(6, 20);
            this.liConnectedRCs.Name = "liConnectedRCs";
            this.liConnectedRCs.Size = new System.Drawing.Size(240, 407);
            this.liConnectedRCs.TabIndex = 0;
            // 
            // txtRCPort
            // 
            this.txtRCPort.Location = new System.Drawing.Point(106, 60);
            this.txtRCPort.Name = "txtRCPort";
            this.txtRCPort.Size = new System.Drawing.Size(132, 21);
            this.txtRCPort.TabIndex = 5;
            this.txtRCPort.TextChanged += new System.EventHandler(this.txtRCPort_TextChanged);
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(24, 63);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(76, 13);
            this.label48.TabIndex = 4;
            this.label48.Text = "Server RC Port:";
            // 
            // btnGenerateRCKey
            // 
            this.btnGenerateRCKey.Location = new System.Drawing.Point(244, 31);
            this.btnGenerateRCKey.Name = "btnGenerateRCKey";
            this.btnGenerateRCKey.Size = new System.Drawing.Size(125, 23);
            this.btnGenerateRCKey.TabIndex = 1;
            this.btnGenerateRCKey.Text = "Generate New RC Key";
            this.btnGenerateRCKey.UseVisualStyleBackColor = true;
            this.btnGenerateRCKey.Click += new System.EventHandler(this.btnGenerateRCKey_Click);
            // 
            // txtRCKey
            // 
            this.txtRCKey.Location = new System.Drawing.Point(106, 33);
            this.txtRCKey.Name = "txtRCKey";
            this.txtRCKey.Size = new System.Drawing.Size(132, 21);
            this.txtRCKey.TabIndex = 0;
            this.txtRCKey.TextChanged += new System.EventHandler(this.txtRCKey_TextChanged);
            // 
            // txtMapViewerRotation
            // 
            this.txtMapViewerRotation.Location = new System.Drawing.Point(240, 14);
            this.txtMapViewerRotation.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.txtMapViewerRotation.Name = "txtMapViewerRotation";
            this.txtMapViewerRotation.Size = new System.Drawing.Size(44, 21);
            this.txtMapViewerRotation.TabIndex = 20;
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 595);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Menu = this.mainMenu1;
            this.Name = "Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Window_FormClosing);
            this.Load += new System.EventHandler(this.Window_Load);
            this.Resize += new System.EventHandler(this.Window_Resize);
            this.playerStrip.ResumeLayout(false);
            this.mapsStrip.ResumeLayout(false);
            this.iconContext.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            this.grpStats.ResumeLayout(false);
            this.grpStats.PerformLayout();
            this.grpPlayers.ResumeLayout(false);
            this.grpPlayers.PerformLayout();
            this.grpVersion.ResumeLayout(false);
            this.grpVersion.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.grpMapViewer.ResumeLayout(false);
            this.grpMapViewer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMapViewer)).EndInit();
            this.grpMapEditor.ResumeLayout(false);
            this.grpMapEditor.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.grpProperties.ResumeLayout(false);
            this.grpProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDrown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhysics)).EndInit();
            this.grpNewDelRenLevel.ResumeLayout(false);
            this.grpNewDelRenLevel.PerformLayout();
            this.grpDeleteLevel.ResumeLayout(false);
            this.grpDeleteLevel.PerformLayout();
            this.grpRenameLevel.ResumeLayout(false);
            this.grpRenameLevel.PerformLayout();
            this.grpLevels.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.grpRanking.ResumeLayout(false);
            this.grpChatCommands.ResumeLayout(false);
            this.grpPlayerLogs.ResumeLayout(false);
            this.grpPlayerLogs.PerformLayout();
            this.grpInGameCmds.ResumeLayout(false);
            this.grpBanCmds.ResumeLayout(false);
            this.grpPlayerInfo.ResumeLayout(false);
            this.grpPlayerInfo.PerformLayout();
            this.tabPage9.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grpAdmin.ResumeLayout(false);
            this.grpAdmin.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.gBCommands.ResumeLayout(false);
            this.gBCommands.PerformLayout();
            this.gBChat.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.tabPage10.PerformLayout();
            this.grpRCUsers.ResumeLayout(false);
            this.grpRCUsers.PerformLayout();
            this.grpRCSettings.ResumeLayout(false);
            this.grpRCSettings.PerformLayout();
            this.grpConnectedRCs.ResumeLayout(false);
            this.grpConnectedRCs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMapViewerRotation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Timer tmrRestart;
        private ContextMenuStrip iconContext;
        private ToolStripMenuItem openConsole;
        private ToolStripMenuItem shutdownServer;
        private ContextMenuStrip playerStrip;
        private ToolStripMenuItem whoisToolStripMenuItem;
        private ToolStripMenuItem kickToolStripMenuItem;
        private ToolStripMenuItem banToolStripMenuItem;
        private ToolStripMenuItem voiceToolStripMenuItem;
        private ContextMenuStrip mapsStrip;
        private ToolStripMenuItem physicsToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem unloadToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem finiteModeToolStripMenuItem;
        private ToolStripMenuItem animalAIToolStripMenuItem;
        private ToolStripMenuItem edgeWaterToolStripMenuItem;
        private ToolStripMenuItem growingGrassToolStripMenuItem;
        private ToolStripMenuItem survivalDeathToolStripMenuItem;
        private ToolStripMenuItem killerBlocksToolStripMenuItem;
        private ToolStripMenuItem rPChatToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private MainMenu mainMenu1;
        private TabPage tabPage8;
        private Label label37;
        private ListBox liAdministration;
        private GroupBox grpStats;
        private Label lblThreads;
        private Label lblCPU;
        private Label lblMemory;
        private Label label42;
        private Label label41;
        private Label label40;
        private Label lblUptime;
        private Label label34;
        private GroupBox grpPlayers;
        private Label lblTotalOps;
        private Label lblTotalPlayersBanned;
        private Label label32;
        private Label lblTotalPlayersVisited;
        private Label label39;
        private Label lblTotalGuests;
        private Label label33;
        private Label lblTotalPlayers;
        private Label label35;
        private Label label31;
        private GroupBox grpVersion;
        private Label lblLatestVersion;
        private Label lblCurVersion;
        private Label label30;
        private Label label29;
        private LinkLabel linkSite;
        private LinkLabel linkForums;
        private ListBox liStaff;
        private Label label28;
        private ListBox liDevs;
        private Label label27;
        private Label label26;
        private TextBox txtDescription;
        private TabPage tabPage3;
        private TextBox txtErrors;
        private TabPage tabPage2;
        private Button btnUpdateChangelog;
        private TextBox txtChangelog;
        private TabPage tabPage4;
        private TextBox txtSystem;
        private TabPage tabPage7;
        private TabPage tabPage6;
        private GroupBox grpProperties;
        private CheckBox chkAutoLoad;
        private CheckBox chkAllowGuns;
        private Button btnPropertiesSave;
        private CheckBox chkRPChat;
        private NumericUpDown txtDrown;
        private Label label16;
        private Label label15;
        private NumericUpDown txtFall;
        private CheckBox chkSurvivalDeath;
        private TextBox txtPhysicsOverload;
        private Label label14;
        private Label label11;
        private TextBox txtPhysicsSpeed;
        private CheckBox chkInstant;
        private CheckBox chkAutoPhysics;
        private CheckBox chkUnload;
        private CheckBox chkKillerBlocks;
        private CheckBox chkGrassGrowing;
        private CheckBox chkEdgeWater;
        private CheckBox chkAnimalAI;
        private CheckBox chkFiniteMode;
        private NumericUpDown txtPhysics;
        private Label label9;
        private TextBox txtLevelMotd;
        private Label label3;
        private Button btnKillPhysics;
        private Button btnUpdateLevelList;
        private GroupBox grpNewDelRenLevel;
        private ComboBox cmbLevelType;
        private Button btnNewLevel;
        private TextBox txtZDim;
        private TextBox txtYDim;
        private TextBox txtXDim;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private TextBox txtLevelName;
        private GroupBox grpNewLevel;
        private GroupBox grpDeleteLevel;
        private Label label10;
        private Button btnDeleteLevel;
        private TextBox txtDeleteLevelName;
        private GroupBox grpRenameLevel;
        private Button btnRenameLevel;
        private Label label13;
        private TextBox txtNewName;
        private Label label12;
        private TextBox txtCurName;
        private TextBox txtLevelLog;
        private GroupBox grpLevels;
        private Button btnUnloadLevel;
        private Button btnLoadLevel;
        private ListBox liLoadedLevels;
        private ListBox liUnloadedLevels;
        private TabPage tabPage5;
        private GroupBox grpRanking;
        private Button btnDemote;
        private Button btnPromote;
        private GroupBox grpChatCommands;
        private Button btnChatModeration;
        private Button btnMute;
        private Button btnVoice;
        private Button btnKick;
        private Button btnFreeze;
        private GroupBox grpPlayerLogs;
        private ListBox liPlayers;
        private TextBox txtPlayerLog;
        private Button btnInvincible;
        private Button btnWarn;
        private Button btnJoker;
        private GroupBox grpInGameCmds;
        private Button btnKill;
        private Button btnSlap;
        private Button btnJail;
        private Button btnHide;
        private GroupBox grpBanCmds;
        private Button btnXBan;
        private Button btnBanIP;
        private Button btnBan;
        private GroupBox grpPlayerInfo;
        private TextBox txtStatus;
        private Label lblStatus;
        private TextBox txtDeaths;
        private Label lblDeaths;
        private TextBox txtKicks;
        private Label lblKicks;
        private TextBox txtModified;
        private Label lblModified;
        private TextBox txtLevel;
        private Label lblLevel;
        private TextBox txtRank;
        private Label lblRank;
        private TextBox txtIP;
        private Label lblIP;
        private TextBox txtPlayerName;
        private Label lblName;
        private TabPage tabPage9;
        private GroupBox groupBox4;
        private Label label45;
        private TextBox txtOpInput;
        private TextBox txtOpLog;
        private GroupBox groupBox3;
        private Label label44;
        private TextBox txtGlobalInput;
        private TextBox txtGlobalLog;
        private GroupBox grpAdmin;
        private Label label43;
        private TextBox txtAdminInput;
        private TextBox txtAdminLog;
        private TabPage tabPage1;
        private Button btnPlay;
        private Label label38;
        private Label label36;
        private ListBox liPlayerBots;
        private CheckBox chkConsoleSounds;
        private Label label17;
        private Button btnProperties;
        public CheckBox chkMaintenance;
        private Button btnRestart;
        private ListBox liMaps;
        private Button button1;
        private Button btnClose;
        private GroupBox gBCommands;
        private TextBox txtCommandsUsed;
        private GroupBox gBChat;
        private Label label2;
        private TextBox txtHost;
        private TextBox txtCommands;
        private TextBox txtInput;
        private TextBox txtUrl;
        private Label label1;
        private ListBox liClients;
        private TabControl tabControl1;
        private TabPage tabPage10;
        private Button btnAddRCUser;
        private GroupBox grpRCUsers;
        private Label label46;
        private Button btnRemoveRCUser;
        private Label label47;
        private GroupBox grpRCSettings;
        private Button btnGenerateRCKey;
        private TextBox txtRCKey;
        private TextBox txtRCPort;
        private Label label48;
        private GroupBox grpConnectedRCs;
        private TextBox txtRCWhisper;
        private TextBox txtRCDisc;
        private Label label50;
        private Label label49;
        private ListBox liConnectedRCs;
        private RichTextBox rtbDescription;
        private TextBox textBox1;
        private Label label51;
        public ListBox liRCUsers;
        private CheckBox chkUseRemote;
        private Button btnRCPortCheck;
        private Label lblRCCheckPortResult;
        private GroupBox grpMapEditor;
        private TextBox txtMapEditorLevelName;
        private Label label18;
        private TextBox txtMapEditorX;
        private Label label19;
        private TextBox txtMapEditorZ;
        private TextBox txtMapEditorY;
        private Label label20;
        private TextBox txtMapEditorCurrentBlock;
        private Label label21;
        private TextBox txtMapEditorChangeBlock;
        private Button btnMapEditorChange;
        private Button btnMapEditorUpdate;
        private GroupBox grpMapViewer;
        private PictureBox picMapViewer;
        private TextBox txtMapViewerLevelName;
        private Label label22;
        private Button btnMapViewerUpdate;
        private Label label23;
        private Button btnMapViewerSave;
        private Label label24;
        private TextBox txtMapViewerZ;
        private TextBox txtMapViewerY;
        private TextBox txtMapViewerX;
        internal RichTextBox txtLog;
        private CheckBox chkChatColors;
        private NumericUpDown txtMapViewerRotation;
    }
}