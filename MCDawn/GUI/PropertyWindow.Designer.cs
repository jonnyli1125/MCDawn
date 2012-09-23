namespace MCDawn.Gui
{
    partial class PropertyWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertyWindow));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grpAdminSecurity = new System.Windows.Forms.GroupBox();
            this.chkAdminSecurity = new System.Windows.Forms.CheckBox();
            this.cmbAdminSecurityRank = new System.Windows.Forms.ComboBox();
            this.label41 = new System.Windows.Forms.Label();
            this.grpMaintenence = new System.Windows.Forms.GroupBox();
            this.cmbMaintJoin = new System.Windows.Forms.ComboBox();
            this.label37 = new System.Windows.Forms.Label();
            this.grpServSettings = new System.Windows.Forms.GroupBox();
            this.chkUPnP = new System.Windows.Forms.CheckBox();
            this.label55 = new System.Windows.Forms.Label();
            this.txtFlags = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnPortCheck = new System.Windows.Forms.Button();
            this.lblPortCheckResult = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMOTD = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpAntiTunnel = new System.Windows.Forms.GroupBox();
            this.ChkTunnels = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDepth = new System.Windows.Forms.TextBox();
            this.grpHomes = new System.Windows.Forms.GroupBox();
            this.label57 = new System.Windows.Forms.Label();
            this.cmbHomePerm = new System.Windows.Forms.ComboBox();
            this.cmbHomeZ = new System.Windows.Forms.ComboBox();
            this.cmbHomeY = new System.Windows.Forms.ComboBox();
            this.label56 = new System.Windows.Forms.Label();
            this.cmbHomeX = new System.Windows.Forms.ComboBox();
            this.lblHomePrefix = new System.Windows.Forms.Label();
            this.txtHomePrefix = new System.Windows.Forms.TextBox();
            this.grpServOptions = new System.Windows.Forms.GroupBox();
            this.chkUseDiscourager = new System.Windows.Forms.CheckBox();
            this.label65 = new System.Windows.Forms.Label();
            this.txtMaxGuests = new System.Windows.Forms.TextBox();
            this.chkShowAttemptedLogins = new System.Windows.Forms.CheckBox();
            this.chkAgreeToRules = new System.Windows.Forms.CheckBox();
            this.label29 = new System.Windows.Forms.Label();
            this.cmbDefaultRank = new System.Windows.Forms.ComboBox();
            this.chkLogBeat = new System.Windows.Forms.CheckBox();
            this.chkAllowProxy = new System.Windows.Forms.CheckBox();
            this.chkPublic = new System.Windows.Forms.CheckBox();
            this.chkRestart = new System.Windows.Forms.CheckBox();
            this.chkVerify = new System.Windows.Forms.CheckBox();
            this.chkWorld = new System.Windows.Forms.CheckBox();
            this.chkUseMaxMind = new System.Windows.Forms.CheckBox();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.chkMono = new System.Windows.Forms.CheckBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.chkUpdates = new System.Windows.Forms.CheckBox();
            this.txtPlayers = new System.Windows.Forms.TextBox();
            this.chkAutoload = new System.Windows.Forms.CheckBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtMaps = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtMain = new System.Windows.Forms.TextBox();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.grpCreateNewText = new System.Windows.Forms.GroupBox();
            this.label63 = new System.Windows.Forms.Label();
            this.btnCreateNewText = new System.Windows.Forms.Button();
            this.txtCreateNewText = new System.Windows.Forms.TextBox();
            this.chkWomText = new System.Windows.Forms.CheckBox();
            this.chkAllowIgnoreOps = new System.Windows.Forms.CheckBox();
            this.grpProfanityFilter = new System.Windows.Forms.GroupBox();
            this.btnEditSwearWords = new System.Windows.Forms.Button();
            this.cmbProfanityFilterStyle = new System.Windows.Forms.ComboBox();
            this.label60 = new System.Windows.Forms.Label();
            this.txtSwearWordsRequired = new System.Windows.Forms.TextBox();
            this.label59 = new System.Windows.Forms.Label();
            this.chkSwearWarnPlayer = new System.Windows.Forms.CheckBox();
            this.chkProfanityFilterOp = new System.Windows.Forms.CheckBox();
            this.chkProfanityFilter = new System.Windows.Forms.CheckBox();
            this.btnEditText = new System.Windows.Forms.Button();
            this.grpAntiCaps = new System.Windows.Forms.GroupBox();
            this.txtCapsRequired = new System.Windows.Forms.TextBox();
            this.cmbAntiCapsConsequence = new System.Windows.Forms.ComboBox();
            this.label50 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.chkAntiCapsOp = new System.Windows.Forms.CheckBox();
            this.chkAntiCaps = new System.Windows.Forms.CheckBox();
            this.grpAntiSpam = new System.Windows.Forms.GroupBox();
            this.txtMsgsRequired = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.cmbAntiSpamConsequence = new System.Windows.Forms.ComboBox();
            this.chkAntiSpamOp = new System.Windows.Forms.CheckBox();
            this.chkAntiSpam = new System.Windows.Forms.CheckBox();
            this.grpChatChannels = new System.Windows.Forms.GroupBox();
            this.label51 = new System.Windows.Forms.Label();
            this.cmbAdminChat = new System.Windows.Forms.ComboBox();
            this.cmbOpChat = new System.Windows.Forms.ComboBox();
            this.label52 = new System.Windows.Forms.Label();
            this.grpChatColors = new System.Windows.Forms.GroupBox();
            this.lblGlobalColour = new System.Windows.Forms.Label();
            this.lblIRC = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.cmbGlobalColour = new System.Windows.Forms.ComboBox();
            this.lblDefault = new System.Windows.Forms.Label();
            this.cmbIRCColour = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbDefaultColour = new System.Windows.Forms.ComboBox();
            this.label53 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.txtNick = new System.Windows.Forms.TextBox();
            this.txtChannel = new System.Windows.Forms.TextBox();
            this.txtIRCServer = new System.Windows.Forms.TextBox();
            this.chkIRC = new System.Windows.Forms.CheckBox();
            this.grpGlobalChat = new System.Windows.Forms.GroupBox();
            this.label68 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.txtGlobalPass = new System.Windows.Forms.TextBox();
            this.chkGlobalIdentify = new System.Windows.Forms.CheckBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.txtGlobalNick = new System.Windows.Forms.TextBox();
            this.chkGlobalChat = new System.Windows.Forms.CheckBox();
            this.label38 = new System.Windows.Forms.Label();
            this.grpIRC = new System.Windows.Forms.GroupBox();
            this.txtIrcPass = new System.Windows.Forms.TextBox();
            this.chkIrcIdentify = new System.Windows.Forms.CheckBox();
            this.txtOpChannel = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.txtWOMIPAddress = new System.Windows.Forms.TextBox();
            this.label69 = new System.Windows.Forms.Label();
            this.chkUseWOMPasswords = new System.Windows.Forms.CheckBox();
            this.chkUseWOM = new System.Windows.Forms.CheckBox();
            this.chkUseAntiGrief = new System.Windows.Forms.CheckBox();
            this.txtUnCheap = new System.Windows.Forms.TextBox();
            this.chkUnCheap = new System.Windows.Forms.CheckBox();
            this.chkRepeatMessages = new System.Windows.Forms.CheckBox();
            this.chkForceCuboid = new System.Windows.Forms.CheckBox();
            this.txtShutdown = new System.Windows.Forms.TextBox();
            this.txtBanMessage = new System.Windows.Forms.TextBox();
            this.chkShutdown = new System.Windows.Forms.CheckBox();
            this.chkBanMessage = new System.Windows.Forms.CheckBox();
            this.chkrankSuper = new System.Windows.Forms.CheckBox();
            this.chkCheap = new System.Windows.Forms.CheckBox();
            this.chkDeath = new System.Windows.Forms.CheckBox();
            this.chk17Dollar = new System.Windows.Forms.CheckBox();
            this.chkPhysicsRest = new System.Windows.Forms.CheckBox();
            this.chkSmile = new System.Windows.Forms.CheckBox();
            this.chkHelp = new System.Windows.Forms.CheckBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.txtNormRp = new System.Windows.Forms.TextBox();
            this.txtRP = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txtAFKKick = new System.Windows.Forms.TextBox();
            this.txtafk = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBackup = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.txtBackupLocation = new System.Windows.Forms.TextBox();
            this.txtMoneys = new System.Windows.Forms.TextBox();
            this.txtCheap = new System.Windows.Forms.TextBox();
            this.txtRestartTime = new System.Windows.Forms.TextBox();
            this.chkRestartTime = new System.Windows.Forms.CheckBox();
            this.grpThrottle = new System.Windows.Forms.GroupBox();
            this.txtThrottleDesc = new System.Windows.Forms.TextBox();
            this.label67 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.tbThrottle = new System.Windows.Forms.TrackBar();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label62 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.txtReqHours = new System.Windows.Forms.TextBox();
            this.chkTpToHigher = new System.Windows.Forms.CheckBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.cmbColor = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtLimit = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPermission = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtRankName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAddRank = new System.Windows.Forms.Button();
            this.listRanks = new System.Windows.Forms.ListBox();
            this.grpAdminJoin = new System.Windows.Forms.GroupBox();
            this.chkAdminsJoinHidden = new System.Windows.Forms.CheckBox();
            this.chkAdminsJoinSilent = new System.Windows.Forms.CheckBox();
            this.grpTimeRank = new System.Windows.Forms.GroupBox();
            this.txtTimeRankCmd = new System.Windows.Forms.TextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.chkUseTimeRank = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnCmdHelp = new System.Windows.Forms.Button();
            this.txtCmdRanks = new System.Windows.Forms.TextBox();
            this.txtCmdAllow = new System.Windows.Forms.TextBox();
            this.txtCmdLowest = new System.Windows.Forms.TextBox();
            this.txtCmdDisallow = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.listCommands = new System.Windows.Forms.ListBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.btnBlHelp = new System.Windows.Forms.Button();
            this.txtBlRanks = new System.Windows.Forms.TextBox();
            this.txtBlAllow = new System.Windows.Forms.TextBox();
            this.txtBlLowest = new System.Windows.Forms.TextBox();
            this.txtBlDisallow = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.listBlocks = new System.Windows.Forms.ListBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.btnDownloadMySQL = new System.Windows.Forms.Button();
            this.chkUseMySQL = new System.Windows.Forms.CheckBox();
            this.label46 = new System.Windows.Forms.Label();
            this.txtMySQLHost = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.txtMySQLPort = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.txtMySQLUser = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.txtMySQLPass = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.txtMySQLDatabase = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDiscard = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.grpAdminSecurity.SuspendLayout();
            this.grpMaintenence.SuspendLayout();
            this.grpServSettings.SuspendLayout();
            this.grpAntiTunnel.SuspendLayout();
            this.grpHomes.SuspendLayout();
            this.grpServOptions.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.grpCreateNewText.SuspendLayout();
            this.grpProfanityFilter.SuspendLayout();
            this.grpAntiCaps.SuspendLayout();
            this.grpAntiSpam.SuspendLayout();
            this.grpChatChannels.SuspendLayout();
            this.grpChatColors.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.grpGlobalChat.SuspendLayout();
            this.grpIRC.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.grpThrottle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbThrottle)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.grpAdminJoin.SuspendLayout();
            this.grpTimeRank.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage8);
            this.tabControl.Controls.Add(this.tabPage6);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage5);
            this.tabControl.Controls.Add(this.tabPage7);
            this.tabControl.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(0, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(452, 484);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.grpAdminSecurity);
            this.tabPage1.Controls.Add(this.grpMaintenence);
            this.tabPage1.Controls.Add(this.grpServSettings);
            this.tabPage1.Controls.Add(this.grpAntiTunnel);
            this.tabPage1.Controls.Add(this.grpHomes);
            this.tabPage1.Controls.Add(this.grpServOptions);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(444, 458);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Server";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // grpAdminSecurity
            // 
            this.grpAdminSecurity.Controls.Add(this.chkAdminSecurity);
            this.grpAdminSecurity.Controls.Add(this.cmbAdminSecurityRank);
            this.grpAdminSecurity.Controls.Add(this.label41);
            this.grpAdminSecurity.Location = new System.Drawing.Point(282, 149);
            this.grpAdminSecurity.Name = "grpAdminSecurity";
            this.grpAdminSecurity.Size = new System.Drawing.Size(158, 81);
            this.grpAdminSecurity.TabIndex = 37;
            this.grpAdminSecurity.TabStop = false;
            this.grpAdminSecurity.Text = "Admin Security";
            // 
            // chkAdminSecurity
            // 
            this.chkAdminSecurity.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkAdminSecurity.AutoSize = true;
            this.chkAdminSecurity.Location = new System.Drawing.Point(15, 20);
            this.chkAdminSecurity.Name = "chkAdminSecurity";
            this.chkAdminSecurity.Size = new System.Drawing.Size(123, 23);
            this.chkAdminSecurity.TabIndex = 35;
            this.chkAdminSecurity.Text = "Admin Security System";
            this.toolTip.SetToolTip(this.chkAdminSecurity, "This adds an extra layer of security on to prevent others from impersonating you " +
        "and your admins.");
            this.chkAdminSecurity.UseVisualStyleBackColor = true;
            // 
            // cmbAdminSecurityRank
            // 
            this.cmbAdminSecurityRank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAdminSecurityRank.FormattingEnabled = true;
            this.cmbAdminSecurityRank.Location = new System.Drawing.Point(45, 49);
            this.cmbAdminSecurityRank.Name = "cmbAdminSecurityRank";
            this.cmbAdminSecurityRank.Size = new System.Drawing.Size(107, 21);
            this.cmbAdminSecurityRank.TabIndex = 34;
            this.toolTip.SetToolTip(this.cmbAdminSecurityRank, "Minimum Rank for Admin Security System");
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(6, 52);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(33, 13);
            this.label41.TabIndex = 33;
            this.label41.Text = "Rank:";
            // 
            // grpMaintenence
            // 
            this.grpMaintenence.Controls.Add(this.cmbMaintJoin);
            this.grpMaintenence.Controls.Add(this.label37);
            this.grpMaintenence.Location = new System.Drawing.Point(8, 149);
            this.grpMaintenence.Name = "grpMaintenence";
            this.grpMaintenence.Size = new System.Drawing.Size(149, 81);
            this.grpMaintenence.TabIndex = 33;
            this.grpMaintenence.TabStop = false;
            this.grpMaintenence.Text = "Maintenence";
            // 
            // cmbMaintJoin
            // 
            this.cmbMaintJoin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaintJoin.FormattingEnabled = true;
            this.cmbMaintJoin.Location = new System.Drawing.Point(7, 49);
            this.cmbMaintJoin.Name = "cmbMaintJoin";
            this.cmbMaintJoin.Size = new System.Drawing.Size(123, 21);
            this.cmbMaintJoin.TabIndex = 31;
            this.toolTip.SetToolTip(this.cmbMaintJoin, "Rank required to join while server is in maintenence.");
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(3, 25);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(133, 13);
            this.label37.TabIndex = 32;
            this.label37.Text = "Join on Maintenence Rank:";
            // 
            // grpServSettings
            // 
            this.grpServSettings.Controls.Add(this.chkUPnP);
            this.grpServSettings.Controls.Add(this.label55);
            this.grpServSettings.Controls.Add(this.txtFlags);
            this.grpServSettings.Controls.Add(this.label54);
            this.grpServSettings.Controls.Add(this.txtDescription);
            this.grpServSettings.Controls.Add(this.btnPortCheck);
            this.grpServSettings.Controls.Add(this.lblPortCheckResult);
            this.grpServSettings.Controls.Add(this.txtPort);
            this.grpServSettings.Controls.Add(this.label3);
            this.grpServSettings.Controls.Add(this.txtMOTD);
            this.grpServSettings.Controls.Add(this.label2);
            this.grpServSettings.Controls.Add(this.txtName);
            this.grpServSettings.Controls.Add(this.label1);
            this.grpServSettings.Location = new System.Drawing.Point(6, 3);
            this.grpServSettings.Name = "grpServSettings";
            this.grpServSettings.Size = new System.Drawing.Size(435, 140);
            this.grpServSettings.TabIndex = 35;
            this.grpServSettings.TabStop = false;
            this.grpServSettings.Text = "Server Settings";
            // 
            // chkUPnP
            // 
            this.chkUPnP.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkUPnP.AutoSize = true;
            this.chkUPnP.Location = new System.Drawing.Point(298, 100);
            this.chkUPnP.Name = "chkUPnP";
            this.chkUPnP.Size = new System.Drawing.Size(131, 23);
            this.chkUPnP.TabIndex = 26;
            this.chkUPnP.Text = "Use UPnP (Port Forward)";
            this.chkUPnP.UseVisualStyleBackColor = true;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(288, 76);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(34, 13);
            this.label55.TabIndex = 9;
            this.label55.Text = "Flags:";
            // 
            // txtFlags
            // 
            this.txtFlags.Location = new System.Drawing.Point(332, 73);
            this.txtFlags.Name = "txtFlags";
            this.txtFlags.Size = new System.Drawing.Size(97, 21);
            this.txtFlags.TabIndex = 8;
            this.toolTip.SetToolTip(this.txtFlags, "WOM Direct Flags.");
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(6, 49);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(64, 13);
            this.label54.TabIndex = 7;
            this.label54.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(76, 46);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(353, 21);
            this.txtDescription.TabIndex = 6;
            this.toolTip.SetToolTip(this.txtDescription, "The WOM Direct Description.");
            // 
            // btnPortCheck
            // 
            this.btnPortCheck.Location = new System.Drawing.Point(212, 100);
            this.btnPortCheck.Name = "btnPortCheck";
            this.btnPortCheck.Size = new System.Drawing.Size(80, 23);
            this.btnPortCheck.TabIndex = 5;
            this.btnPortCheck.Text = "Port Check";
            this.btnPortCheck.UseVisualStyleBackColor = true;
            this.btnPortCheck.Click += new System.EventHandler(this.btnPortCheck_Click);
            // 
            // lblPortCheckResult
            // 
            this.lblPortCheckResult.AutoSize = true;
            this.lblPortCheckResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPortCheckResult.Location = new System.Drawing.Point(143, 105);
            this.lblPortCheckResult.Name = "lblPortCheckResult";
            this.lblPortCheckResult.Size = new System.Drawing.Size(63, 15);
            this.lblPortCheckResult.TabIndex = 4;
            this.lblPortCheckResult.Text = "Not Started";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(41, 102);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(96, 21);
            this.txtPort.TabIndex = 2;
            this.toolTip.SetToolTip(this.txtPort, "The port that the server will output on.\nDefault = 25565\n\nChanging will reset you" +
        "r ExternalURL.");
            this.txtPort.TextChanged += new System.EventHandler(this.txtPort_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Port:";
            // 
            // txtMOTD
            // 
            this.txtMOTD.Location = new System.Drawing.Point(50, 73);
            this.txtMOTD.Name = "txtMOTD";
            this.txtMOTD.Size = new System.Drawing.Size(232, 21);
            this.txtMOTD.TabIndex = 0;
            this.toolTip.SetToolTip(this.txtMOTD, "The MOTD of the server.\nUse \"+hax\" to allow any WoM hack, \"-hax\" to disallow any " +
        "hacks at all and use \"-fly\" and whatnot to disallow other things.");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "MOTD:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(50, 19);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(379, 21);
            this.txtName.TabIndex = 0;
            this.toolTip.SetToolTip(this.txtName, "The name of the server.\nPick something good!");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // grpAntiTunnel
            // 
            this.grpAntiTunnel.Controls.Add(this.ChkTunnels);
            this.grpAntiTunnel.Controls.Add(this.label7);
            this.grpAntiTunnel.Controls.Add(this.txtDepth);
            this.grpAntiTunnel.Location = new System.Drawing.Point(163, 149);
            this.grpAntiTunnel.Name = "grpAntiTunnel";
            this.grpAntiTunnel.Size = new System.Drawing.Size(113, 81);
            this.grpAntiTunnel.TabIndex = 36;
            this.grpAntiTunnel.TabStop = false;
            this.grpAntiTunnel.Text = "Anti Tunneling";
            // 
            // ChkTunnels
            // 
            this.ChkTunnels.Appearance = System.Windows.Forms.Appearance.Button;
            this.ChkTunnels.AutoSize = true;
            this.ChkTunnels.Location = new System.Drawing.Point(11, 20);
            this.ChkTunnels.Name = "ChkTunnels";
            this.ChkTunnels.Size = new System.Drawing.Size(82, 23);
            this.ChkTunnels.TabIndex = 4;
            this.ChkTunnels.Text = "Anti tunneling";
            this.toolTip.SetToolTip(this.ChkTunnels, "Should guests be limited to digging a certain depth?");
            this.ChkTunnels.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Depth:";
            // 
            // txtDepth
            // 
            this.txtDepth.Location = new System.Drawing.Point(51, 49);
            this.txtDepth.Name = "txtDepth";
            this.txtDepth.Size = new System.Drawing.Size(54, 21);
            this.txtDepth.TabIndex = 2;
            this.toolTip.SetToolTip(this.txtDepth, "Depth which guests can dig.\nDefault = 4");
            this.txtDepth.TextChanged += new System.EventHandler(this.txtDepth_TextChanged);
            // 
            // grpHomes
            // 
            this.grpHomes.Controls.Add(this.label57);
            this.grpHomes.Controls.Add(this.cmbHomePerm);
            this.grpHomes.Controls.Add(this.cmbHomeZ);
            this.grpHomes.Controls.Add(this.cmbHomeY);
            this.grpHomes.Controls.Add(this.label56);
            this.grpHomes.Controls.Add(this.cmbHomeX);
            this.grpHomes.Controls.Add(this.lblHomePrefix);
            this.grpHomes.Controls.Add(this.txtHomePrefix);
            this.grpHomes.Location = new System.Drawing.Point(3, 411);
            this.grpHomes.Name = "grpHomes";
            this.grpHomes.Size = new System.Drawing.Size(437, 44);
            this.grpHomes.TabIndex = 28;
            this.grpHomes.TabStop = false;
            this.grpHomes.Text = "Homes";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(98, 17);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(53, 13);
            this.label57.TabIndex = 37;
            this.label57.Text = "Rank Req:";
            // 
            // cmbHomePerm
            // 
            this.cmbHomePerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHomePerm.FormattingEnabled = true;
            this.cmbHomePerm.Location = new System.Drawing.Point(157, 12);
            this.cmbHomePerm.Name = "cmbHomePerm";
            this.cmbHomePerm.Size = new System.Drawing.Size(85, 21);
            this.cmbHomePerm.TabIndex = 36;
            this.toolTip.SetToolTip(this.cmbHomePerm, "Rank required to join while server is in maintenence.");
            // 
            // cmbHomeZ
            // 
            this.cmbHomeZ.FormattingEnabled = true;
            this.cmbHomeZ.Items.AddRange(new object[] {
            "16",
            "32",
            "64",
            "128",
            "256",
            "512"});
            this.cmbHomeZ.Location = new System.Drawing.Point(387, 14);
            this.cmbHomeZ.Name = "cmbHomeZ";
            this.cmbHomeZ.Size = new System.Drawing.Size(44, 21);
            this.cmbHomeZ.TabIndex = 35;
            // 
            // cmbHomeY
            // 
            this.cmbHomeY.FormattingEnabled = true;
            this.cmbHomeY.Items.AddRange(new object[] {
            "16",
            "32",
            "64",
            "128",
            "256",
            "512"});
            this.cmbHomeY.Location = new System.Drawing.Point(337, 14);
            this.cmbHomeY.Name = "cmbHomeY";
            this.cmbHomeY.Size = new System.Drawing.Size(44, 21);
            this.cmbHomeY.TabIndex = 34;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(248, 17);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(33, 13);
            this.label56.TabIndex = 31;
            this.label56.Text = "X/Y/Z:";
            // 
            // cmbHomeX
            // 
            this.cmbHomeX.FormattingEnabled = true;
            this.cmbHomeX.Items.AddRange(new object[] {
            "16",
            "32",
            "64",
            "128",
            "256",
            "512"});
            this.cmbHomeX.Location = new System.Drawing.Point(287, 14);
            this.cmbHomeX.Name = "cmbHomeX";
            this.cmbHomeX.Size = new System.Drawing.Size(44, 21);
            this.cmbHomeX.TabIndex = 28;
            // 
            // lblHomePrefix
            // 
            this.lblHomePrefix.AutoSize = true;
            this.lblHomePrefix.Location = new System.Drawing.Point(6, 17);
            this.lblHomePrefix.Name = "lblHomePrefix";
            this.lblHomePrefix.Size = new System.Drawing.Size(37, 13);
            this.lblHomePrefix.TabIndex = 25;
            this.lblHomePrefix.Text = "Prefix:";
            // 
            // txtHomePrefix
            // 
            this.txtHomePrefix.Location = new System.Drawing.Point(49, 12);
            this.txtHomePrefix.Name = "txtHomePrefix";
            this.txtHomePrefix.Size = new System.Drawing.Size(43, 21);
            this.txtHomePrefix.TabIndex = 26;
            // 
            // grpServOptions
            // 
            this.grpServOptions.Controls.Add(this.chkUseDiscourager);
            this.grpServOptions.Controls.Add(this.label65);
            this.grpServOptions.Controls.Add(this.txtMaxGuests);
            this.grpServOptions.Controls.Add(this.chkShowAttemptedLogins);
            this.grpServOptions.Controls.Add(this.chkAgreeToRules);
            this.grpServOptions.Controls.Add(this.label29);
            this.grpServOptions.Controls.Add(this.cmbDefaultRank);
            this.grpServOptions.Controls.Add(this.chkLogBeat);
            this.grpServOptions.Controls.Add(this.chkAllowProxy);
            this.grpServOptions.Controls.Add(this.chkPublic);
            this.grpServOptions.Controls.Add(this.chkRestart);
            this.grpServOptions.Controls.Add(this.chkVerify);
            this.grpServOptions.Controls.Add(this.chkWorld);
            this.grpServOptions.Controls.Add(this.chkUseMaxMind);
            this.grpServOptions.Controls.Add(this.txtHost);
            this.grpServOptions.Controls.Add(this.chkMono);
            this.grpServOptions.Controls.Add(this.label30);
            this.grpServOptions.Controls.Add(this.label21);
            this.grpServOptions.Controls.Add(this.chkUpdates);
            this.grpServOptions.Controls.Add(this.txtPlayers);
            this.grpServOptions.Controls.Add(this.chkAutoload);
            this.grpServOptions.Controls.Add(this.label22);
            this.grpServOptions.Controls.Add(this.txtMaps);
            this.grpServOptions.Controls.Add(this.label27);
            this.grpServOptions.Controls.Add(this.txtMain);
            this.grpServOptions.Location = new System.Drawing.Point(3, 236);
            this.grpServOptions.Name = "grpServOptions";
            this.grpServOptions.Size = new System.Drawing.Size(437, 171);
            this.grpServOptions.TabIndex = 34;
            this.grpServOptions.TabStop = false;
            this.grpServOptions.Text = "Server Options";
            // 
            // chkUseDiscourager
            // 
            this.chkUseDiscourager.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkUseDiscourager.AutoSize = true;
            this.chkUseDiscourager.Location = new System.Drawing.Point(147, 113);
            this.chkUseDiscourager.Name = "chkUseDiscourager";
            this.chkUseDiscourager.Size = new System.Drawing.Size(94, 23);
            this.chkUseDiscourager.TabIndex = 28;
            this.chkUseDiscourager.Text = "Use Discourager";
            this.toolTip.SetToolTip(this.chkUseDiscourager, resources.GetString("chkUseDiscourager.ToolTip"));
            this.chkUseDiscourager.UseVisualStyleBackColor = true;
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(146, 17);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(11, 13);
            this.label65.TabIndex = 27;
            this.label65.Text = "/";
            // 
            // txtMaxGuests
            // 
            this.txtMaxGuests.Location = new System.Drawing.Point(160, 14);
            this.txtMaxGuests.Name = "txtMaxGuests";
            this.txtMaxGuests.Size = new System.Drawing.Size(29, 21);
            this.txtMaxGuests.TabIndex = 26;
            this.toolTip.SetToolTip(this.txtMaxGuests, "The total number of players which can login.\nDefault = 12");
            // 
            // chkShowAttemptedLogins
            // 
            this.chkShowAttemptedLogins.AutoSize = true;
            this.chkShowAttemptedLogins.Location = new System.Drawing.Point(9, 94);
            this.chkShowAttemptedLogins.Name = "chkShowAttemptedLogins";
            this.chkShowAttemptedLogins.Size = new System.Drawing.Size(135, 17);
            this.chkShowAttemptedLogins.TabIndex = 25;
            this.chkShowAttemptedLogins.Text = "Show Attempted Logins";
            this.chkShowAttemptedLogins.UseVisualStyleBackColor = true;
            // 
            // chkAgreeToRules
            // 
            this.chkAgreeToRules.AutoSize = true;
            this.chkAgreeToRules.Location = new System.Drawing.Point(9, 75);
            this.chkAgreeToRules.Name = "chkAgreeToRules";
            this.chkAgreeToRules.Size = new System.Drawing.Size(192, 17);
            this.chkAgreeToRules.TabIndex = 8;
            this.chkAgreeToRules.Text = "Force new players to Agree to Rules";
            this.chkAgreeToRules.UseVisualStyleBackColor = true;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(257, 73);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(68, 13);
            this.label29.TabIndex = 20;
            this.label29.Text = "Default rank:";
            // 
            // cmbDefaultRank
            // 
            this.cmbDefaultRank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDefaultRank.FormattingEnabled = true;
            this.cmbDefaultRank.Location = new System.Drawing.Point(335, 70);
            this.cmbDefaultRank.Name = "cmbDefaultRank";
            this.cmbDefaultRank.Size = new System.Drawing.Size(97, 21);
            this.cmbDefaultRank.TabIndex = 21;
            this.toolTip.SetToolTip(this.cmbDefaultRank, "Default rank assigned to new visitors to the server.");
            // 
            // chkLogBeat
            // 
            this.chkLogBeat.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkLogBeat.AutoSize = true;
            this.chkLogBeat.Location = new System.Drawing.Point(114, 142);
            this.chkLogBeat.Name = "chkLogBeat";
            this.chkLogBeat.Size = new System.Drawing.Size(89, 23);
            this.chkLogBeat.TabIndex = 24;
            this.chkLogBeat.Text = "Log Heartbeat?";
            this.toolTip.SetToolTip(this.chkLogBeat, "Debugging feature -- Toggles whether to log heartbeat activity.\r\nUseful when your" +
        " server gets a URL slowly or not at all.");
            this.chkLogBeat.UseVisualStyleBackColor = true;
            // 
            // chkAllowProxy
            // 
            this.chkAllowProxy.AutoSize = true;
            this.chkAllowProxy.Location = new System.Drawing.Point(9, 56);
            this.chkAllowProxy.Name = "chkAllowProxy";
            this.chkAllowProxy.Size = new System.Drawing.Size(89, 17);
            this.chkAllowProxy.TabIndex = 7;
            this.chkAllowProxy.Text = "Allow Proxies";
            this.chkAllowProxy.UseVisualStyleBackColor = true;
            // 
            // chkPublic
            // 
            this.chkPublic.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkPublic.AutoSize = true;
            this.chkPublic.Location = new System.Drawing.Point(9, 114);
            this.chkPublic.Name = "chkPublic";
            this.chkPublic.Size = new System.Drawing.Size(46, 23);
            this.chkPublic.TabIndex = 4;
            this.chkPublic.Text = "Public";
            this.toolTip.SetToolTip(this.chkPublic, "Whether or not the server will appear on the server list.");
            this.chkPublic.UseVisualStyleBackColor = true;
            // 
            // chkRestart
            // 
            this.chkRestart.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRestart.AutoSize = true;
            this.chkRestart.Location = new System.Drawing.Point(281, 142);
            this.chkRestart.Name = "chkRestart";
            this.chkRestart.Size = new System.Drawing.Size(153, 23);
            this.chkRestart.TabIndex = 4;
            this.chkRestart.Text = "Restart when an error occurs";
            this.chkRestart.UseVisualStyleBackColor = true;
            // 
            // chkVerify
            // 
            this.chkVerify.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkVerify.AutoSize = true;
            this.chkVerify.Location = new System.Drawing.Point(359, 113);
            this.chkVerify.Name = "chkVerify";
            this.chkVerify.Size = new System.Drawing.Size(78, 23);
            this.chkVerify.TabIndex = 4;
            this.chkVerify.Text = "Verify Names";
            this.toolTip.SetToolTip(this.chkVerify, "Make sure the user is who they claim to be. If you don\'t have Admin Security, ple" +
        "ase turn on Verify Names.");
            this.chkVerify.UseVisualStyleBackColor = true;
            // 
            // chkWorld
            // 
            this.chkWorld.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkWorld.AutoSize = true;
            this.chkWorld.Location = new System.Drawing.Point(208, 142);
            this.chkWorld.Name = "chkWorld";
            this.chkWorld.Size = new System.Drawing.Size(69, 23);
            this.chkWorld.TabIndex = 4;
            this.chkWorld.Text = "World chat";
            this.toolTip.SetToolTip(this.chkWorld, "If disabled, every map has isolated chat.\nIf enabled, every map is able to commun" +
        "icate without special letters.");
            this.chkWorld.UseVisualStyleBackColor = true;
            // 
            // chkUseMaxMind
            // 
            this.chkUseMaxMind.AutoSize = true;
            this.chkUseMaxMind.Location = new System.Drawing.Point(9, 37);
            this.chkUseMaxMind.Name = "chkUseMaxMind";
            this.chkUseMaxMind.Size = new System.Drawing.Size(136, 17);
            this.chkUseMaxMind.TabIndex = 6;
            this.chkUseMaxMind.Text = "Country on Join / Geo IP";
            this.chkUseMaxMind.UseVisualStyleBackColor = true;
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(335, 43);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(96, 21);
            this.txtHost.TabIndex = 2;
            this.txtHost.TextChanged += new System.EventHandler(this.txtPort_TextChanged);
            // 
            // chkMono
            // 
            this.chkMono.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkMono.AutoSize = true;
            this.chkMono.Location = new System.Drawing.Point(247, 114);
            this.chkMono.Name = "chkMono";
            this.chkMono.Size = new System.Drawing.Size(106, 23);
            this.chkMono.TabIndex = 4;
            this.chkMono.Text = "Using Mono/Linux?";
            this.chkMono.UseVisualStyleBackColor = true;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(234, 46);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(95, 13);
            this.label30.TabIndex = 3;
            this.label30.Text = "Default host state:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 17);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(104, 13);
            this.label21.TabIndex = 3;
            this.label21.Text = "Max Players/Guests:";
            // 
            // chkUpdates
            // 
            this.chkUpdates.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkUpdates.AutoSize = true;
            this.chkUpdates.Location = new System.Drawing.Point(9, 142);
            this.chkUpdates.Name = "chkUpdates";
            this.chkUpdates.Size = new System.Drawing.Size(101, 23);
            this.chkUpdates.TabIndex = 4;
            this.chkUpdates.Text = "Check for updates";
            this.chkUpdates.UseVisualStyleBackColor = true;
            // 
            // txtPlayers
            // 
            this.txtPlayers.Location = new System.Drawing.Point(116, 14);
            this.txtPlayers.Name = "txtPlayers";
            this.txtPlayers.Size = new System.Drawing.Size(29, 21);
            this.txtPlayers.TabIndex = 2;
            this.toolTip.SetToolTip(this.txtPlayers, "The total number of players which can login.\nDefault = 12");
            this.txtPlayers.TextChanged += new System.EventHandler(this.txtPlayers_TextChanged);
            // 
            // chkAutoload
            // 
            this.chkAutoload.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkAutoload.AutoSize = true;
            this.chkAutoload.Location = new System.Drawing.Point(61, 114);
            this.chkAutoload.Name = "chkAutoload";
            this.chkAutoload.Size = new System.Drawing.Size(81, 23);
            this.chkAutoload.TabIndex = 4;
            this.chkAutoload.Text = "Load on /goto";
            this.toolTip.SetToolTip(this.chkAutoload, "Load a map when a user wishes to go to it, and unload empty maps");
            this.chkAutoload.UseVisualStyleBackColor = true;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(195, 17);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(58, 13);
            this.label22.TabIndex = 3;
            this.label22.Text = "Max Maps:";
            // 
            // txtMaps
            // 
            this.txtMaps.Location = new System.Drawing.Point(259, 14);
            this.txtMaps.Name = "txtMaps";
            this.txtMaps.Size = new System.Drawing.Size(26, 21);
            this.txtMaps.TabIndex = 2;
            this.toolTip.SetToolTip(this.txtMaps, "The total number of maps which can be loaded at once.\nDefault = 5");
            this.txtMaps.TextChanged += new System.EventHandler(this.txtMaps_TextChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(291, 17);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(63, 13);
            this.label27.TabIndex = 3;
            this.label27.Text = "Main name:";
            // 
            // txtMain
            // 
            this.txtMain.Location = new System.Drawing.Point(360, 14);
            this.txtMain.Name = "txtMain";
            this.txtMain.Size = new System.Drawing.Size(71, 21);
            this.txtMain.TabIndex = 2;
            this.txtMain.TextChanged += new System.EventHandler(this.txtMaps_TextChanged);
            // 
            // tabPage8
            // 
            this.tabPage8.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage8.Controls.Add(this.grpCreateNewText);
            this.tabPage8.Controls.Add(this.chkWomText);
            this.tabPage8.Controls.Add(this.chkAllowIgnoreOps);
            this.tabPage8.Controls.Add(this.grpProfanityFilter);
            this.tabPage8.Controls.Add(this.btnEditText);
            this.tabPage8.Controls.Add(this.grpAntiCaps);
            this.tabPage8.Controls.Add(this.grpAntiSpam);
            this.tabPage8.Controls.Add(this.grpChatChannels);
            this.tabPage8.Controls.Add(this.grpChatColors);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(444, 458);
            this.tabPage8.TabIndex = 8;
            this.tabPage8.Text = "Chat";
            // 
            // grpCreateNewText
            // 
            this.grpCreateNewText.Controls.Add(this.label63);
            this.grpCreateNewText.Controls.Add(this.btnCreateNewText);
            this.grpCreateNewText.Controls.Add(this.txtCreateNewText);
            this.grpCreateNewText.Location = new System.Drawing.Point(8, 303);
            this.grpCreateNewText.Name = "grpCreateNewText";
            this.grpCreateNewText.Size = new System.Drawing.Size(189, 41);
            this.grpCreateNewText.TabIndex = 48;
            this.grpCreateNewText.TabStop = false;
            this.grpCreateNewText.Text = "Create New Text Files";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(6, 17);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(54, 13);
            this.label63.TabIndex = 48;
            this.label63.Text = "Filename:";
            // 
            // btnCreateNewText
            // 
            this.btnCreateNewText.Location = new System.Drawing.Point(129, 12);
            this.btnCreateNewText.Name = "btnCreateNewText";
            this.btnCreateNewText.Size = new System.Drawing.Size(54, 23);
            this.btnCreateNewText.TabIndex = 46;
            this.btnCreateNewText.Text = "Create";
            this.btnCreateNewText.UseVisualStyleBackColor = true;
            this.btnCreateNewText.Click += new System.EventHandler(this.btnCreateNewText_Click);
            // 
            // txtCreateNewText
            // 
            this.txtCreateNewText.Location = new System.Drawing.Point(60, 14);
            this.txtCreateNewText.Name = "txtCreateNewText";
            this.txtCreateNewText.Size = new System.Drawing.Size(63, 21);
            this.txtCreateNewText.TabIndex = 47;
            // 
            // chkWomText
            // 
            this.chkWomText.AutoSize = true;
            this.chkWomText.Location = new System.Drawing.Point(319, 325);
            this.chkWomText.Name = "chkWomText";
            this.chkWomText.Size = new System.Drawing.Size(73, 17);
            this.chkWomText.TabIndex = 45;
            this.chkWomText.Text = "WOM Text";
            this.chkWomText.UseVisualStyleBackColor = true;
            // 
            // chkAllowIgnoreOps
            // 
            this.chkAllowIgnoreOps.AutoSize = true;
            this.chkAllowIgnoreOps.Location = new System.Drawing.Point(319, 348);
            this.chkAllowIgnoreOps.Name = "chkAllowIgnoreOps";
            this.chkAllowIgnoreOps.Size = new System.Drawing.Size(112, 17);
            this.chkAllowIgnoreOps.TabIndex = 44;
            this.chkAllowIgnoreOps.Text = "Allow Ignoring Ops";
            this.chkAllowIgnoreOps.UseVisualStyleBackColor = true;
            // 
            // grpProfanityFilter
            // 
            this.grpProfanityFilter.Controls.Add(this.btnEditSwearWords);
            this.grpProfanityFilter.Controls.Add(this.cmbProfanityFilterStyle);
            this.grpProfanityFilter.Controls.Add(this.label60);
            this.grpProfanityFilter.Controls.Add(this.txtSwearWordsRequired);
            this.grpProfanityFilter.Controls.Add(this.label59);
            this.grpProfanityFilter.Controls.Add(this.chkSwearWarnPlayer);
            this.grpProfanityFilter.Controls.Add(this.chkProfanityFilterOp);
            this.grpProfanityFilter.Controls.Add(this.chkProfanityFilter);
            this.grpProfanityFilter.Location = new System.Drawing.Point(8, 175);
            this.grpProfanityFilter.Name = "grpProfanityFilter";
            this.grpProfanityFilter.Size = new System.Drawing.Size(429, 122);
            this.grpProfanityFilter.TabIndex = 43;
            this.grpProfanityFilter.TabStop = false;
            this.grpProfanityFilter.Text = "Profanity Filter";
            // 
            // btnEditSwearWords
            // 
            this.btnEditSwearWords.Location = new System.Drawing.Point(9, 85);
            this.btnEditSwearWords.Name = "btnEditSwearWords";
            this.btnEditSwearWords.Size = new System.Drawing.Size(124, 23);
            this.btnEditSwearWords.TabIndex = 11;
            this.btnEditSwearWords.Text = "Edit Swear Words List";
            this.btnEditSwearWords.UseVisualStyleBackColor = true;
            this.btnEditSwearWords.Click += new System.EventHandler(this.btnEditSwearWords_Click);
            // 
            // cmbProfanityFilterStyle
            // 
            this.cmbProfanityFilterStyle.FormattingEnabled = true;
            this.cmbProfanityFilterStyle.Items.AddRange(new object[] {
            "Kick",
            "TempBan",
            "Mute",
            "Slap"});
            this.cmbProfanityFilterStyle.Location = new System.Drawing.Point(294, 48);
            this.cmbProfanityFilterStyle.Name = "cmbProfanityFilterStyle";
            this.cmbProfanityFilterStyle.Size = new System.Drawing.Size(121, 21);
            this.cmbProfanityFilterStyle.TabIndex = 10;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(320, 30);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(74, 13);
            this.label60.TabIndex = 9;
            this.label60.Text = "Consequence:";
            // 
            // txtSwearWordsRequired
            // 
            this.txtSwearWordsRequired.Location = new System.Drawing.Point(208, 53);
            this.txtSwearWordsRequired.Name = "txtSwearWordsRequired";
            this.txtSwearWordsRequired.Size = new System.Drawing.Size(38, 21);
            this.txtSwearWordsRequired.TabIndex = 9;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(6, 56);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(196, 13);
            this.label59.TabIndex = 3;
            this.label59.Text = "Swear Words required for Consequence:";
            // 
            // chkSwearWarnPlayer
            // 
            this.chkSwearWarnPlayer.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkSwearWarnPlayer.AutoSize = true;
            this.chkSwearWarnPlayer.Location = new System.Drawing.Point(180, 20);
            this.chkSwearWarnPlayer.Name = "chkSwearWarnPlayer";
            this.chkSwearWarnPlayer.Size = new System.Drawing.Size(75, 23);
            this.chkSwearWarnPlayer.TabIndex = 2;
            this.chkSwearWarnPlayer.Text = "Warn Player";
            this.chkSwearWarnPlayer.UseVisualStyleBackColor = true;
            // 
            // chkProfanityFilterOp
            // 
            this.chkProfanityFilterOp.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkProfanityFilterOp.AutoSize = true;
            this.chkProfanityFilterOp.Location = new System.Drawing.Point(99, 20);
            this.chkProfanityFilterOp.Name = "chkProfanityFilterOp";
            this.chkProfanityFilterOp.Size = new System.Drawing.Size(75, 23);
            this.chkProfanityFilterOp.TabIndex = 1;
            this.chkProfanityFilterOp.Text = "Apply to OP+";
            this.chkProfanityFilterOp.UseVisualStyleBackColor = true;
            // 
            // chkProfanityFilter
            // 
            this.chkProfanityFilter.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkProfanityFilter.AutoSize = true;
            this.chkProfanityFilter.Location = new System.Drawing.Point(6, 20);
            this.chkProfanityFilter.Name = "chkProfanityFilter";
            this.chkProfanityFilter.Size = new System.Drawing.Size(87, 23);
            this.chkProfanityFilter.TabIndex = 0;
            this.chkProfanityFilter.Text = "Profanity Filter";
            this.chkProfanityFilter.UseVisualStyleBackColor = true;
            // 
            // btnEditText
            // 
            this.btnEditText.Location = new System.Drawing.Point(203, 344);
            this.btnEditText.Name = "btnEditText";
            this.btnEditText.Size = new System.Drawing.Size(104, 23);
            this.btnEditText.TabIndex = 42;
            this.btnEditText.Text = "Edit Text Files";
            this.btnEditText.UseVisualStyleBackColor = true;
            this.btnEditText.Click += new System.EventHandler(this.btnEditText_Click);
            // 
            // grpAntiCaps
            // 
            this.grpAntiCaps.Controls.Add(this.txtCapsRequired);
            this.grpAntiCaps.Controls.Add(this.cmbAntiCapsConsequence);
            this.grpAntiCaps.Controls.Add(this.label50);
            this.grpAntiCaps.Controls.Add(this.label49);
            this.grpAntiCaps.Controls.Add(this.chkAntiCapsOp);
            this.grpAntiCaps.Controls.Add(this.chkAntiCaps);
            this.grpAntiCaps.Location = new System.Drawing.Point(8, 92);
            this.grpAntiCaps.Name = "grpAntiCaps";
            this.grpAntiCaps.Size = new System.Drawing.Size(429, 77);
            this.grpAntiCaps.TabIndex = 2;
            this.grpAntiCaps.TabStop = false;
            this.grpAntiCaps.Text = "Anti-Caps";
            // 
            // txtCapsRequired
            // 
            this.txtCapsRequired.Location = new System.Drawing.Point(226, 47);
            this.txtCapsRequired.Name = "txtCapsRequired";
            this.txtCapsRequired.Size = new System.Drawing.Size(38, 21);
            this.txtCapsRequired.TabIndex = 8;
            // 
            // cmbAntiCapsConsequence
            // 
            this.cmbAntiCapsConsequence.FormattingEnabled = true;
            this.cmbAntiCapsConsequence.Items.AddRange(new object[] {
            "Kick",
            "TempBan",
            "Mute",
            "Slap"});
            this.cmbAntiCapsConsequence.Location = new System.Drawing.Point(300, 42);
            this.cmbAntiCapsConsequence.Name = "cmbAntiCapsConsequence";
            this.cmbAntiCapsConsequence.Size = new System.Drawing.Size(121, 21);
            this.cmbAntiCapsConsequence.TabIndex = 7;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(320, 25);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(74, 13);
            this.label50.TabIndex = 6;
            this.label50.Text = "Consequence:";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(6, 50);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(214, 13);
            this.label49.TabIndex = 4;
            this.label49.Text = "Caps Required in message for Consequence:";
            // 
            // chkAntiCapsOp
            // 
            this.chkAntiCapsOp.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkAntiCapsOp.AutoSize = true;
            this.chkAntiCapsOp.Location = new System.Drawing.Point(76, 20);
            this.chkAntiCapsOp.Name = "chkAntiCapsOp";
            this.chkAntiCapsOp.Size = new System.Drawing.Size(75, 23);
            this.chkAntiCapsOp.TabIndex = 3;
            this.chkAntiCapsOp.Text = "Apply to OP+";
            this.chkAntiCapsOp.UseVisualStyleBackColor = true;
            // 
            // chkAntiCaps
            // 
            this.chkAntiCaps.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkAntiCaps.AutoSize = true;
            this.chkAntiCaps.Location = new System.Drawing.Point(6, 20);
            this.chkAntiCaps.Name = "chkAntiCaps";
            this.chkAntiCaps.Size = new System.Drawing.Size(61, 23);
            this.chkAntiCaps.TabIndex = 1;
            this.chkAntiCaps.Text = "Anti-Caps";
            this.chkAntiCaps.UseVisualStyleBackColor = true;
            // 
            // grpAntiSpam
            // 
            this.grpAntiSpam.Controls.Add(this.txtMsgsRequired);
            this.grpAntiSpam.Controls.Add(this.label36);
            this.grpAntiSpam.Controls.Add(this.label35);
            this.grpAntiSpam.Controls.Add(this.cmbAntiSpamConsequence);
            this.grpAntiSpam.Controls.Add(this.chkAntiSpamOp);
            this.grpAntiSpam.Controls.Add(this.chkAntiSpam);
            this.grpAntiSpam.Location = new System.Drawing.Point(8, 10);
            this.grpAntiSpam.Name = "grpAntiSpam";
            this.grpAntiSpam.Size = new System.Drawing.Size(429, 76);
            this.grpAntiSpam.TabIndex = 1;
            this.grpAntiSpam.TabStop = false;
            this.grpAntiSpam.Text = "Anti-Spam";
            // 
            // txtMsgsRequired
            // 
            this.txtMsgsRequired.Location = new System.Drawing.Point(195, 47);
            this.txtMsgsRequired.Name = "txtMsgsRequired";
            this.txtMsgsRequired.Size = new System.Drawing.Size(38, 21);
            this.txtMsgsRequired.TabIndex = 6;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(320, 25);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(74, 13);
            this.label36.TabIndex = 5;
            this.label36.Text = "Consequence:";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(6, 50);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(183, 13);
            this.label35.TabIndex = 4;
            this.label35.Text = "Messages Required for Consequence:";
            // 
            // cmbAntiSpamConsequence
            // 
            this.cmbAntiSpamConsequence.FormattingEnabled = true;
            this.cmbAntiSpamConsequence.Items.AddRange(new object[] {
            "Kick",
            "TempBan",
            "Mute",
            "Slap"});
            this.cmbAntiSpamConsequence.Location = new System.Drawing.Point(300, 42);
            this.cmbAntiSpamConsequence.Name = "cmbAntiSpamConsequence";
            this.cmbAntiSpamConsequence.Size = new System.Drawing.Size(121, 21);
            this.cmbAntiSpamConsequence.TabIndex = 1;
            // 
            // chkAntiSpamOp
            // 
            this.chkAntiSpamOp.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkAntiSpamOp.AutoSize = true;
            this.chkAntiSpamOp.Location = new System.Drawing.Point(76, 20);
            this.chkAntiSpamOp.Name = "chkAntiSpamOp";
            this.chkAntiSpamOp.Size = new System.Drawing.Size(75, 23);
            this.chkAntiSpamOp.TabIndex = 2;
            this.chkAntiSpamOp.Text = "Apply to OP+";
            this.chkAntiSpamOp.UseVisualStyleBackColor = true;
            // 
            // chkAntiSpam
            // 
            this.chkAntiSpam.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkAntiSpam.AutoSize = true;
            this.chkAntiSpam.Location = new System.Drawing.Point(6, 20);
            this.chkAntiSpam.Name = "chkAntiSpam";
            this.chkAntiSpam.Size = new System.Drawing.Size(64, 23);
            this.chkAntiSpam.TabIndex = 0;
            this.chkAntiSpam.Text = "Anti-Spam";
            this.chkAntiSpam.UseVisualStyleBackColor = true;
            // 
            // grpChatChannels
            // 
            this.grpChatChannels.Controls.Add(this.label51);
            this.grpChatChannels.Controls.Add(this.cmbAdminChat);
            this.grpChatChannels.Controls.Add(this.cmbOpChat);
            this.grpChatChannels.Controls.Add(this.label52);
            this.grpChatChannels.Location = new System.Drawing.Point(203, 373);
            this.grpChatChannels.Name = "grpChatChannels";
            this.grpChatChannels.Size = new System.Drawing.Size(234, 82);
            this.grpChatChannels.TabIndex = 41;
            this.grpChatChannels.TabStop = false;
            this.grpChatChannels.Text = "Chat Channels";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(6, 27);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(87, 13);
            this.label51.TabIndex = 28;
            this.label51.Text = "Admin Chat rank:";
            // 
            // cmbAdminChat
            // 
            this.cmbAdminChat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAdminChat.FormattingEnabled = true;
            this.cmbAdminChat.Location = new System.Drawing.Point(99, 20);
            this.cmbAdminChat.Name = "cmbAdminChat";
            this.cmbAdminChat.Size = new System.Drawing.Size(131, 21);
            this.cmbAdminChat.TabIndex = 27;
            this.toolTip.SetToolTip(this.cmbAdminChat, "Default rank required to read admin chat.");
            // 
            // cmbOpChat
            // 
            this.cmbOpChat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOpChat.FormattingEnabled = true;
            this.cmbOpChat.Location = new System.Drawing.Point(99, 47);
            this.cmbOpChat.Name = "cmbOpChat";
            this.cmbOpChat.Size = new System.Drawing.Size(131, 21);
            this.cmbOpChat.TabIndex = 30;
            this.toolTip.SetToolTip(this.cmbOpChat, "Default rank required to read op chat.");
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(23, 50);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(70, 13);
            this.label52.TabIndex = 29;
            this.label52.Text = "Op Chat rank:";
            // 
            // grpChatColors
            // 
            this.grpChatColors.Controls.Add(this.lblGlobalColour);
            this.grpChatColors.Controls.Add(this.lblIRC);
            this.grpChatColors.Controls.Add(this.label23);
            this.grpChatColors.Controls.Add(this.cmbGlobalColour);
            this.grpChatColors.Controls.Add(this.lblDefault);
            this.grpChatColors.Controls.Add(this.cmbIRCColour);
            this.grpChatColors.Controls.Add(this.label10);
            this.grpChatColors.Controls.Add(this.cmbDefaultColour);
            this.grpChatColors.Controls.Add(this.label53);
            this.grpChatColors.Location = new System.Drawing.Point(4, 350);
            this.grpChatColors.Name = "grpChatColors";
            this.grpChatColors.Size = new System.Drawing.Size(193, 105);
            this.grpChatColors.TabIndex = 40;
            this.grpChatColors.TabStop = false;
            this.grpChatColors.Text = "Chat Colors";
            // 
            // lblGlobalColour
            // 
            this.lblGlobalColour.Location = new System.Drawing.Point(143, 18);
            this.lblGlobalColour.Name = "lblGlobalColour";
            this.lblGlobalColour.Size = new System.Drawing.Size(26, 23);
            this.lblGlobalColour.TabIndex = 39;
            // 
            // lblIRC
            // 
            this.lblIRC.Location = new System.Drawing.Point(143, 45);
            this.lblIRC.Name = "lblIRC";
            this.lblIRC.Size = new System.Drawing.Size(26, 23);
            this.lblIRC.TabIndex = 40;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 23);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(68, 13);
            this.label23.TabIndex = 37;
            this.label23.Text = "Global Color:";
            // 
            // cmbGlobalColour
            // 
            this.cmbGlobalColour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGlobalColour.FormattingEnabled = true;
            this.cmbGlobalColour.Location = new System.Drawing.Point(80, 20);
            this.cmbGlobalColour.Name = "cmbGlobalColour";
            this.cmbGlobalColour.Size = new System.Drawing.Size(57, 21);
            this.cmbGlobalColour.TabIndex = 38;
            this.toolTip.SetToolTip(this.cmbGlobalColour, "The colour of the IRC nicks used in the IRC.");
            this.cmbGlobalColour.SelectedIndexChanged += new System.EventHandler(this.cmbGlobalColour_SelectedIndexChanged);
            // 
            // lblDefault
            // 
            this.lblDefault.Location = new System.Drawing.Point(143, 72);
            this.lblDefault.Name = "lblDefault";
            this.lblDefault.Size = new System.Drawing.Size(26, 23);
            this.lblDefault.TabIndex = 41;
            // 
            // cmbIRCColour
            // 
            this.cmbIRCColour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIRCColour.FormattingEnabled = true;
            this.cmbIRCColour.Location = new System.Drawing.Point(80, 47);
            this.cmbIRCColour.Name = "cmbIRCColour";
            this.cmbIRCColour.Size = new System.Drawing.Size(57, 21);
            this.cmbIRCColour.TabIndex = 35;
            this.toolTip.SetToolTip(this.cmbIRCColour, "The colour of the IRC nicks used in the IRC.");
            this.cmbIRCColour.SelectedIndexChanged += new System.EventHandler(this.cmbIRCColour_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "IRC color:";
            // 
            // cmbDefaultColour
            // 
            this.cmbDefaultColour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDefaultColour.FormattingEnabled = true;
            this.cmbDefaultColour.Location = new System.Drawing.Point(80, 74);
            this.cmbDefaultColour.Name = "cmbDefaultColour";
            this.cmbDefaultColour.Size = new System.Drawing.Size(57, 21);
            this.cmbDefaultColour.TabIndex = 31;
            this.toolTip.SetToolTip(this.cmbDefaultColour, "The colour of the default chat used in the server.\nFor example, when you are aske" +
        "d to select two corners in a cuboid.");
            this.cmbDefaultColour.SelectedIndexChanged += new System.EventHandler(this.cmbDefaultColour_SelectedIndexChanged);
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(3, 77);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(71, 13);
            this.label53.TabIndex = 33;
            this.label53.Text = "Default color:";
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage6.Controls.Add(this.txtNick);
            this.tabPage6.Controls.Add(this.txtChannel);
            this.tabPage6.Controls.Add(this.txtIRCServer);
            this.tabPage6.Controls.Add(this.chkIRC);
            this.tabPage6.Controls.Add(this.grpGlobalChat);
            this.tabPage6.Controls.Add(this.grpIRC);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(444, 458);
            this.tabPage6.TabIndex = 6;
            this.tabPage6.Text = "IRC/Global";
            // 
            // txtNick
            // 
            this.txtNick.Location = new System.Drawing.Point(81, 161);
            this.txtNick.Name = "txtNick";
            this.txtNick.Size = new System.Drawing.Size(354, 21);
            this.txtNick.TabIndex = 19;
            this.toolTip.SetToolTip(this.txtNick, "The Nick that the IRC bot will try and use.");
            // 
            // txtChannel
            // 
            this.txtChannel.Location = new System.Drawing.Point(81, 105);
            this.txtChannel.Name = "txtChannel";
            this.txtChannel.Size = new System.Drawing.Size(354, 21);
            this.txtChannel.TabIndex = 17;
            this.toolTip.SetToolTip(this.txtChannel, "The IRC channel to be used.");
            // 
            // txtIRCServer
            // 
            this.txtIRCServer.Location = new System.Drawing.Point(81, 75);
            this.txtIRCServer.Name = "txtIRCServer";
            this.txtIRCServer.Size = new System.Drawing.Size(354, 21);
            this.txtIRCServer.TabIndex = 16;
            this.toolTip.SetToolTip(this.txtIRCServer, "The IRC server to be used.");
            // 
            // chkIRC
            // 
            this.chkIRC.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkIRC.AutoSize = true;
            this.chkIRC.Location = new System.Drawing.Point(43, 32);
            this.chkIRC.Name = "chkIRC";
            this.chkIRC.Size = new System.Drawing.Size(52, 23);
            this.chkIRC.TabIndex = 5;
            this.chkIRC.Text = "Use IRC";
            this.toolTip.SetToolTip(this.chkIRC, "Whether to use the IRC bot or not.\nIRC stands for Internet Relay Chat and allows " +
        "for communication with the server while outside Minecraft.");
            this.chkIRC.UseVisualStyleBackColor = true;
            // 
            // grpGlobalChat
            // 
            this.grpGlobalChat.Controls.Add(this.label68);
            this.grpGlobalChat.Controls.Add(this.label64);
            this.grpGlobalChat.Controls.Add(this.txtGlobalPass);
            this.grpGlobalChat.Controls.Add(this.chkGlobalIdentify);
            this.grpGlobalChat.Controls.Add(this.label33);
            this.grpGlobalChat.Controls.Add(this.label39);
            this.grpGlobalChat.Controls.Add(this.label40);
            this.grpGlobalChat.Controls.Add(this.txtGlobalNick);
            this.grpGlobalChat.Controls.Add(this.chkGlobalChat);
            this.grpGlobalChat.Controls.Add(this.label38);
            this.grpGlobalChat.Location = new System.Drawing.Point(8, 227);
            this.grpGlobalChat.Name = "grpGlobalChat";
            this.grpGlobalChat.Size = new System.Drawing.Size(433, 228);
            this.grpGlobalChat.TabIndex = 26;
            this.grpGlobalChat.TabStop = false;
            this.grpGlobalChat.Text = "Global Chat";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(18, 195);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(388, 13);
            this.label68.TabIndex = 29;
            this.label68.Text = "You can ignore individual players on Global Chat with /ignore global <player> als" +
    "o.";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(18, 182);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(304, 13);
            this.label64.TabIndex = 28;
            this.label64.Text = "If players don\'t like the Global Chat, they can use /ignore global.";
            // 
            // txtGlobalPass
            // 
            this.txtGlobalPass.Location = new System.Drawing.Point(156, 80);
            this.txtGlobalPass.Name = "txtGlobalPass";
            this.txtGlobalPass.Size = new System.Drawing.Size(271, 21);
            this.txtGlobalPass.TabIndex = 27;
            // 
            // chkGlobalIdentify
            // 
            this.chkGlobalIdentify.AutoSize = true;
            this.chkGlobalIdentify.Location = new System.Drawing.Point(21, 82);
            this.chkGlobalIdentify.Name = "chkGlobalIdentify";
            this.chkGlobalIdentify.Size = new System.Drawing.Size(129, 17);
            this.chkGlobalIdentify.TabIndex = 26;
            this.chkGlobalIdentify.Text = "Identify with Nickserv:";
            this.chkGlobalIdentify.UseVisualStyleBackColor = true;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(16, 122);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(286, 13);
            this.label33.TabIndex = 20;
            this.label33.Text = "MCDawn Global Chat is a Chat accross all MCDawn Servers. ";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(16, 147);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(368, 13);
            this.label39.TabIndex = 24;
            this.label39.Text = "Connecting to it will let you send and recieve messages accross all connected";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(17, 160);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(331, 13);
            this.label40.TabIndex = 25;
            this.label40.Text = "MCDawn Servers. The channel is located at irc.synirc.net > #MCDawn.";
            // 
            // txtGlobalNick
            // 
            this.txtGlobalNick.Location = new System.Drawing.Point(73, 55);
            this.txtGlobalNick.Name = "txtGlobalNick";
            this.txtGlobalNick.Size = new System.Drawing.Size(354, 21);
            this.txtGlobalNick.TabIndex = 23;
            this.toolTip.SetToolTip(this.txtGlobalNick, "Your server\'s nick in the Global Chat.");
            // 
            // chkGlobalChat
            // 
            this.chkGlobalChat.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkGlobalChat.AutoSize = true;
            this.chkGlobalChat.Location = new System.Drawing.Point(35, 20);
            this.chkGlobalChat.Name = "chkGlobalChat";
            this.chkGlobalChat.Size = new System.Drawing.Size(92, 23);
            this.chkGlobalChat.TabIndex = 21;
            this.chkGlobalChat.Text = "Use Global Chat";
            this.toolTip.SetToolTip(this.chkGlobalChat, "Whether to use the IRC bot or not.\nIRC stands for Internet Relay Chat and allows " +
        "for communication with the server while outside Minecraft.");
            this.chkGlobalChat.UseVisualStyleBackColor = true;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(5, 58);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(62, 13);
            this.label38.TabIndex = 22;
            this.label38.Text = "Server Nick:";
            // 
            // grpIRC
            // 
            this.grpIRC.Controls.Add(this.txtIrcPass);
            this.grpIRC.Controls.Add(this.chkIrcIdentify);
            this.grpIRC.Controls.Add(this.txtOpChannel);
            this.grpIRC.Controls.Add(this.label31);
            this.grpIRC.Controls.Add(this.label5);
            this.grpIRC.Controls.Add(this.label6);
            this.grpIRC.Controls.Add(this.label4);
            this.grpIRC.Location = new System.Drawing.Point(8, 14);
            this.grpIRC.Name = "grpIRC";
            this.grpIRC.Size = new System.Drawing.Size(433, 207);
            this.grpIRC.TabIndex = 27;
            this.grpIRC.TabStop = false;
            this.grpIRC.Text = "IRC";
            // 
            // txtIrcPass
            // 
            this.txtIrcPass.Location = new System.Drawing.Point(156, 172);
            this.txtIrcPass.Name = "txtIrcPass";
            this.txtIrcPass.Size = new System.Drawing.Size(271, 21);
            this.txtIrcPass.TabIndex = 20;
            // 
            // chkIrcIdentify
            // 
            this.chkIrcIdentify.AutoSize = true;
            this.chkIrcIdentify.Location = new System.Drawing.Point(21, 174);
            this.chkIrcIdentify.Name = "chkIrcIdentify";
            this.chkIrcIdentify.Size = new System.Drawing.Size(129, 17);
            this.chkIrcIdentify.TabIndex = 19;
            this.chkIrcIdentify.Text = "Identify with Nickserv:";
            this.chkIrcIdentify.UseVisualStyleBackColor = true;
            // 
            // txtOpChannel
            // 
            this.txtOpChannel.Location = new System.Drawing.Point(73, 120);
            this.txtOpChannel.Name = "txtOpChannel";
            this.txtOpChannel.Size = new System.Drawing.Size(354, 21);
            this.txtOpChannel.TabIndex = 18;
            this.toolTip.SetToolTip(this.txtOpChannel, "The IRC channel to be used.");
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(3, 123);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(64, 13);
            this.label31.TabIndex = 14;
            this.label31.Text = "Op Channel:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Channel:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Server:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Server Nick:";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.txtWOMIPAddress);
            this.tabPage4.Controls.Add(this.label69);
            this.tabPage4.Controls.Add(this.chkUseWOMPasswords);
            this.tabPage4.Controls.Add(this.chkUseWOM);
            this.tabPage4.Controls.Add(this.chkUseAntiGrief);
            this.tabPage4.Controls.Add(this.txtUnCheap);
            this.tabPage4.Controls.Add(this.chkUnCheap);
            this.tabPage4.Controls.Add(this.chkRepeatMessages);
            this.tabPage4.Controls.Add(this.chkForceCuboid);
            this.tabPage4.Controls.Add(this.txtShutdown);
            this.tabPage4.Controls.Add(this.txtBanMessage);
            this.tabPage4.Controls.Add(this.chkShutdown);
            this.tabPage4.Controls.Add(this.chkBanMessage);
            this.tabPage4.Controls.Add(this.chkrankSuper);
            this.tabPage4.Controls.Add(this.chkCheap);
            this.tabPage4.Controls.Add(this.chkDeath);
            this.tabPage4.Controls.Add(this.chk17Dollar);
            this.tabPage4.Controls.Add(this.chkPhysicsRest);
            this.tabPage4.Controls.Add(this.chkSmile);
            this.tabPage4.Controls.Add(this.chkHelp);
            this.tabPage4.Controls.Add(this.label28);
            this.tabPage4.Controls.Add(this.label24);
            this.tabPage4.Controls.Add(this.txtNormRp);
            this.tabPage4.Controls.Add(this.txtRP);
            this.tabPage4.Controls.Add(this.label34);
            this.tabPage4.Controls.Add(this.label26);
            this.tabPage4.Controls.Add(this.label25);
            this.tabPage4.Controls.Add(this.txtAFKKick);
            this.tabPage4.Controls.Add(this.txtafk);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.txtBackup);
            this.tabPage4.Controls.Add(this.label32);
            this.tabPage4.Controls.Add(this.txtBackupLocation);
            this.tabPage4.Controls.Add(this.txtMoneys);
            this.tabPage4.Controls.Add(this.txtCheap);
            this.tabPage4.Controls.Add(this.txtRestartTime);
            this.tabPage4.Controls.Add(this.chkRestartTime);
            this.tabPage4.Controls.Add(this.grpThrottle);
            this.tabPage4.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(444, 458);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Misc";
            // 
            // txtWOMIPAddress
            // 
            this.txtWOMIPAddress.Location = new System.Drawing.Point(350, 90);
            this.txtWOMIPAddress.Name = "txtWOMIPAddress";
            this.txtWOMIPAddress.Size = new System.Drawing.Size(87, 21);
            this.txtWOMIPAddress.TabIndex = 38;
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(266, 94);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(78, 13);
            this.label69.TabIndex = 37;
            this.label69.Text = "WOM Direct IP:";
            // 
            // chkUseWOMPasswords
            // 
            this.chkUseWOMPasswords.AutoSize = true;
            this.chkUseWOMPasswords.Location = new System.Drawing.Point(269, 74);
            this.chkUseWOMPasswords.Name = "chkUseWOMPasswords";
            this.chkUseWOMPasswords.Size = new System.Drawing.Size(125, 17);
            this.chkUseWOMPasswords.TabIndex = 36;
            this.chkUseWOMPasswords.Text = "Use WOM Passwords";
            this.chkUseWOMPasswords.UseVisualStyleBackColor = true;
            // 
            // chkUseWOM
            // 
            this.chkUseWOM.AutoSize = true;
            this.chkUseWOM.Location = new System.Drawing.Point(269, 38);
            this.chkUseWOM.Name = "chkUseWOM";
            this.chkUseWOM.Size = new System.Drawing.Size(103, 17);
            this.chkUseWOM.TabIndex = 33;
            this.chkUseWOM.Text = "Use WOM Direct";
            this.chkUseWOM.UseVisualStyleBackColor = true;
            // 
            // chkUseAntiGrief
            // 
            this.chkUseAntiGrief.AutoSize = true;
            this.chkUseAntiGrief.Location = new System.Drawing.Point(269, 56);
            this.chkUseAntiGrief.Name = "chkUseAntiGrief";
            this.chkUseAntiGrief.Size = new System.Drawing.Size(90, 17);
            this.chkUseAntiGrief.TabIndex = 32;
            this.chkUseAntiGrief.Text = "Use Anti-Grief";
            this.toolTip.SetToolTip(this.chkUseAntiGrief, "Enable MCDawn\'s \"Anti-Grief\" system, which kicks players who are building too fas" +
        "t.");
            this.chkUseAntiGrief.UseVisualStyleBackColor = true;
            // 
            // txtUnCheap
            // 
            this.txtUnCheap.Location = new System.Drawing.Point(137, 177);
            this.txtUnCheap.Name = "txtUnCheap";
            this.txtUnCheap.Size = new System.Drawing.Size(300, 21);
            this.txtUnCheap.TabIndex = 31;
            // 
            // chkUnCheap
            // 
            this.chkUnCheap.AutoSize = true;
            this.chkUnCheap.Location = new System.Drawing.Point(13, 179);
            this.chkUnCheap.Name = "chkUnCheap";
            this.chkUnCheap.Size = new System.Drawing.Size(118, 17);
            this.chkUnCheap.TabIndex = 30;
            this.chkUnCheap.Text = "Un Cheap message:";
            this.toolTip.SetToolTip(this.chkUnCheap, "Is immortality cheap and unfair?");
            this.chkUnCheap.UseVisualStyleBackColor = true;
            // 
            // chkRepeatMessages
            // 
            this.chkRepeatMessages.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRepeatMessages.AutoSize = true;
            this.chkRepeatMessages.Location = new System.Drawing.Point(109, 432);
            this.chkRepeatMessages.Name = "chkRepeatMessages";
            this.chkRepeatMessages.Size = new System.Drawing.Size(127, 23);
            this.chkRepeatMessages.TabIndex = 29;
            this.chkRepeatMessages.Text = "Repeat message blocks";
            this.chkRepeatMessages.UseVisualStyleBackColor = true;
            // 
            // chkForceCuboid
            // 
            this.chkForceCuboid.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkForceCuboid.AutoSize = true;
            this.chkForceCuboid.Location = new System.Drawing.Point(258, 403);
            this.chkForceCuboid.Name = "chkForceCuboid";
            this.chkForceCuboid.Size = new System.Drawing.Size(78, 23);
            this.chkForceCuboid.TabIndex = 29;
            this.chkForceCuboid.Text = "Force Cuboid";
            this.toolTip.SetToolTip(this.chkForceCuboid, "When true, runs an attempted cuboid despite cuboid limits, until it hits the grou" +
        "p limit for that user.");
            this.chkForceCuboid.UseVisualStyleBackColor = true;
            // 
            // txtShutdown
            // 
            this.txtShutdown.Location = new System.Drawing.Point(177, 231);
            this.txtShutdown.MaxLength = 128;
            this.txtShutdown.Name = "txtShutdown";
            this.txtShutdown.Size = new System.Drawing.Size(260, 21);
            this.txtShutdown.TabIndex = 28;
            // 
            // txtBanMessage
            // 
            this.txtBanMessage.Location = new System.Drawing.Point(148, 204);
            this.txtBanMessage.MaxLength = 128;
            this.txtBanMessage.Name = "txtBanMessage";
            this.txtBanMessage.Size = new System.Drawing.Size(289, 21);
            this.txtBanMessage.TabIndex = 27;
            // 
            // chkShutdown
            // 
            this.chkShutdown.AutoSize = true;
            this.chkShutdown.Location = new System.Drawing.Point(13, 233);
            this.chkShutdown.Name = "chkShutdown";
            this.chkShutdown.Size = new System.Drawing.Size(158, 17);
            this.chkShutdown.TabIndex = 26;
            this.chkShutdown.Text = "Custom shutdown message:";
            this.chkShutdown.UseVisualStyleBackColor = true;
            // 
            // chkBanMessage
            // 
            this.chkBanMessage.AutoSize = true;
            this.chkBanMessage.Location = new System.Drawing.Point(13, 206);
            this.chkBanMessage.Name = "chkBanMessage";
            this.chkBanMessage.Size = new System.Drawing.Size(129, 17);
            this.chkBanMessage.TabIndex = 25;
            this.chkBanMessage.Text = "Custom ban message:";
            this.chkBanMessage.UseVisualStyleBackColor = true;
            // 
            // chkrankSuper
            // 
            this.chkrankSuper.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkrankSuper.AutoSize = true;
            this.chkrankSuper.Location = new System.Drawing.Point(242, 432);
            this.chkrankSuper.Name = "chkrankSuper";
            this.chkrankSuper.Size = new System.Drawing.Size(195, 23);
            this.chkrankSuper.TabIndex = 24;
            this.chkrankSuper.Text = "SuperOPs can appoint other SuperOPs";
            this.toolTip.SetToolTip(this.chkrankSuper, "Does what it says on the tin");
            this.chkrankSuper.UseVisualStyleBackColor = true;
            // 
            // chkCheap
            // 
            this.chkCheap.AutoSize = true;
            this.chkCheap.Location = new System.Drawing.Point(13, 152);
            this.chkCheap.Name = "chkCheap";
            this.chkCheap.Size = new System.Drawing.Size(103, 17);
            this.chkCheap.TabIndex = 23;
            this.chkCheap.Text = "Cheap message:";
            this.toolTip.SetToolTip(this.chkCheap, "Is immortality cheap and unfair?");
            this.chkCheap.UseVisualStyleBackColor = true;
            // 
            // chkDeath
            // 
            this.chkDeath.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDeath.AutoSize = true;
            this.chkDeath.Location = new System.Drawing.Point(13, 403);
            this.chkDeath.Name = "chkDeath";
            this.chkDeath.Size = new System.Drawing.Size(75, 23);
            this.chkDeath.TabIndex = 21;
            this.chkDeath.Text = "Death count";
            this.toolTip.SetToolTip(this.chkDeath, "\"Bob has died 10 times.\"");
            this.chkDeath.UseVisualStyleBackColor = true;
            // 
            // chk17Dollar
            // 
            this.chk17Dollar.Appearance = System.Windows.Forms.Appearance.Button;
            this.chk17Dollar.AutoSize = true;
            this.chk17Dollar.Location = new System.Drawing.Point(346, 403);
            this.chk17Dollar.Name = "chk17Dollar";
            this.chk17Dollar.Size = new System.Drawing.Size(91, 23);
            this.chk17Dollar.TabIndex = 22;
            this.chk17Dollar.Text = "$ before $name";
            this.chk17Dollar.UseVisualStyleBackColor = true;
            // 
            // chkPhysicsRest
            // 
            this.chkPhysicsRest.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkPhysicsRest.AutoSize = true;
            this.chkPhysicsRest.Location = new System.Drawing.Point(13, 432);
            this.chkPhysicsRest.Name = "chkPhysicsRest";
            this.chkPhysicsRest.Size = new System.Drawing.Size(89, 23);
            this.chkPhysicsRest.TabIndex = 22;
            this.chkPhysicsRest.Text = "Restart physics";
            this.toolTip.SetToolTip(this.chkPhysicsRest, "Restart physics on shutdown, clearing all physics blocks.");
            this.chkPhysicsRest.UseVisualStyleBackColor = true;
            // 
            // chkSmile
            // 
            this.chkSmile.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkSmile.AutoSize = true;
            this.chkSmile.Location = new System.Drawing.Point(163, 403);
            this.chkSmile.Name = "chkSmile";
            this.chkSmile.Size = new System.Drawing.Size(82, 23);
            this.chkSmile.TabIndex = 19;
            this.chkSmile.Text = "Parse emotes";
            this.chkSmile.UseVisualStyleBackColor = true;
            // 
            // chkHelp
            // 
            this.chkHelp.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkHelp.AutoSize = true;
            this.chkHelp.Location = new System.Drawing.Point(97, 403);
            this.chkHelp.Name = "chkHelp";
            this.chkHelp.Size = new System.Drawing.Size(56, 23);
            this.chkHelp.TabIndex = 20;
            this.chkHelp.Text = "Old help";
            this.toolTip.SetToolTip(this.chkHelp, "Should the old, cluttered help menu be used?");
            this.chkHelp.UseVisualStyleBackColor = true;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(140, 64);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(61, 13);
            this.label28.TabIndex = 16;
            this.label28.Text = "Normal /rp:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(153, 40);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(48, 13);
            this.label24.TabIndex = 15;
            this.label24.Text = "/rp limit:";
            this.toolTip.SetToolTip(this.label24, "Limit for custom physics set by /rp");
            // 
            // txtNormRp
            // 
            this.txtNormRp.Location = new System.Drawing.Point(207, 61);
            this.txtNormRp.Name = "txtNormRp";
            this.txtNormRp.Size = new System.Drawing.Size(53, 21);
            this.txtNormRp.TabIndex = 13;
            // 
            // txtRP
            // 
            this.txtRP.Location = new System.Drawing.Point(207, 37);
            this.txtRP.Name = "txtRP";
            this.txtRP.Size = new System.Drawing.Size(53, 21);
            this.txtRP.TabIndex = 14;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(130, 93);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(71, 13);
            this.label34.TabIndex = 11;
            this.label34.Text = "Money name:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(29, 93);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(48, 13);
            this.label26.TabIndex = 11;
            this.label26.Text = "AFK Kick:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(23, 67);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(54, 13);
            this.label25.TabIndex = 12;
            this.label25.Text = "AFK timer:";
            // 
            // txtAFKKick
            // 
            this.txtAFKKick.Location = new System.Drawing.Point(83, 91);
            this.txtAFKKick.Name = "txtAFKKick";
            this.txtAFKKick.Size = new System.Drawing.Size(41, 21);
            this.txtAFKKick.TabIndex = 9;
            this.toolTip.SetToolTip(this.txtAFKKick, "Kick the user after they have been afk for this many minutes (0 = No kick)");
            // 
            // txtafk
            // 
            this.txtafk.Location = new System.Drawing.Point(83, 64);
            this.txtafk.Name = "txtafk";
            this.txtafk.Size = new System.Drawing.Size(41, 21);
            this.txtafk.TabIndex = 10;
            this.toolTip.SetToolTip(this.txtafk, "How long the server should wait before declaring someone ask afk. (0 = No timer a" +
        "t all)");
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Backup time:";
            // 
            // txtBackup
            // 
            this.txtBackup.Location = new System.Drawing.Point(83, 37);
            this.txtBackup.Name = "txtBackup";
            this.txtBackup.Size = new System.Drawing.Size(41, 21);
            this.txtBackup.TabIndex = 5;
            this.toolTip.SetToolTip(this.txtBackup, "How often should backups be taken, in seconds.\nDefault = 300");
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(10, 15);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(44, 13);
            this.label32.TabIndex = 3;
            this.label32.Text = "Backup:";
            // 
            // txtBackupLocation
            // 
            this.txtBackupLocation.Location = new System.Drawing.Point(60, 12);
            this.txtBackupLocation.Name = "txtBackupLocation";
            this.txtBackupLocation.Size = new System.Drawing.Size(377, 21);
            this.txtBackupLocation.TabIndex = 2;
            // 
            // txtMoneys
            // 
            this.txtMoneys.Location = new System.Drawing.Point(207, 90);
            this.txtMoneys.Name = "txtMoneys";
            this.txtMoneys.Size = new System.Drawing.Size(53, 21);
            this.txtMoneys.TabIndex = 1;
            // 
            // txtCheap
            // 
            this.txtCheap.Location = new System.Drawing.Point(122, 150);
            this.txtCheap.Name = "txtCheap";
            this.txtCheap.Size = new System.Drawing.Size(315, 21);
            this.txtCheap.TabIndex = 1;
            // 
            // txtRestartTime
            // 
            this.txtRestartTime.Location = new System.Drawing.Point(151, 123);
            this.txtRestartTime.Name = "txtRestartTime";
            this.txtRestartTime.Size = new System.Drawing.Size(286, 21);
            this.txtRestartTime.TabIndex = 1;
            this.txtRestartTime.Text = "HH: mm: ss";
            // 
            // chkRestartTime
            // 
            this.chkRestartTime.AutoSize = true;
            this.chkRestartTime.Location = new System.Drawing.Point(13, 125);
            this.chkRestartTime.Name = "chkRestartTime";
            this.chkRestartTime.Size = new System.Drawing.Size(131, 17);
            this.chkRestartTime.TabIndex = 0;
            this.chkRestartTime.Text = "Restart server at time:";
            this.chkRestartTime.UseVisualStyleBackColor = true;
            // 
            // grpThrottle
            // 
            this.grpThrottle.Controls.Add(this.txtThrottleDesc);
            this.grpThrottle.Controls.Add(this.label67);
            this.grpThrottle.Controls.Add(this.label66);
            this.grpThrottle.Controls.Add(this.tbThrottle);
            this.grpThrottle.Location = new System.Drawing.Point(13, 265);
            this.grpThrottle.Name = "grpThrottle";
            this.grpThrottle.Size = new System.Drawing.Size(424, 119);
            this.grpThrottle.TabIndex = 35;
            this.grpThrottle.TabStop = false;
            this.grpThrottle.Text = "Building Commands \"Throttle\"";
            // 
            // txtThrottleDesc
            // 
            this.txtThrottleDesc.BackColor = System.Drawing.SystemColors.Control;
            this.txtThrottleDesc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtThrottleDesc.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtThrottleDesc.Location = new System.Drawing.Point(6, 18);
            this.txtThrottleDesc.Multiline = true;
            this.txtThrottleDesc.Name = "txtThrottleDesc";
            this.txtThrottleDesc.Size = new System.Drawing.Size(412, 29);
            this.txtThrottleDesc.TabIndex = 37;
            this.txtThrottleDesc.Text = "MCDawn building commands Throttle is a feature that allows you to control how fas" +
    "t your building commands (such as cuboid) place blocks. Default is 9.";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(365, 53);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(53, 13);
            this.label67.TabIndex = 36;
            this.label67.Text = "200 (Fast)";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(10, 53);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(43, 13);
            this.label66.TabIndex = 35;
            this.label66.Text = "1 (Slow)";
            // 
            // tbThrottle
            // 
            this.tbThrottle.Location = new System.Drawing.Point(6, 69);
            this.tbThrottle.Maximum = 200;
            this.tbThrottle.Minimum = 1;
            this.tbThrottle.Name = "tbThrottle";
            this.tbThrottle.Size = new System.Drawing.Size(412, 45);
            this.tbThrottle.TabIndex = 34;
            this.tbThrottle.Value = 1;
            this.tbThrottle.Scroll += new System.EventHandler(this.tbThrottle_Scroll);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.label62);
            this.tabPage2.Controls.Add(this.label61);
            this.tabPage2.Controls.Add(this.txtReqHours);
            this.tabPage2.Controls.Add(this.chkTpToHigher);
            this.tabPage2.Controls.Add(this.lblColor);
            this.tabPage2.Controls.Add(this.cmbColor);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.txtFileName);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.txtLimit);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.txtPermission);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.txtRankName);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.btnAddRank);
            this.tabPage2.Controls.Add(this.listRanks);
            this.tabPage2.Controls.Add(this.grpAdminJoin);
            this.tabPage2.Controls.Add(this.grpTimeRank);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(444, 458);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = "Ranks";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(14, 152);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(61, 13);
            this.label62.TabIndex = 22;
            this.label62.Text = "Req. Hours:";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(187, 152);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(101, 13);
            this.label61.TabIndex = 21;
            this.label61.Text = "(Leave 0 to disable.)";
            // 
            // txtReqHours
            // 
            this.txtReqHours.Location = new System.Drawing.Point(81, 149);
            this.txtReqHours.Name = "txtReqHours";
            this.txtReqHours.Size = new System.Drawing.Size(100, 21);
            this.txtReqHours.TabIndex = 20;
            this.txtReqHours.TextChanged += new System.EventHandler(this.txtReqHours_TextChanged);
            // 
            // chkTpToHigher
            // 
            this.chkTpToHigher.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkTpToHigher.AutoSize = true;
            this.chkTpToHigher.Location = new System.Drawing.Point(81, 220);
            this.chkTpToHigher.Name = "chkTpToHigher";
            this.chkTpToHigher.Size = new System.Drawing.Size(180, 23);
            this.chkTpToHigher.TabIndex = 14;
            this.chkTpToHigher.Text = "Allow Teleportation to higher ranks";
            this.toolTip.SetToolTip(this.chkTpToHigher, "If disabled, every map has isolated chat.\nIf enabled, every map is able to commun" +
        "icate without special letters.");
            this.chkTpToHigher.UseVisualStyleBackColor = true;
            this.chkTpToHigher.CheckedChanged += new System.EventHandler(this.chkTpToHigher_CheckedChanged);
            // 
            // lblColor
            // 
            this.lblColor.Location = new System.Drawing.Point(198, 119);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(26, 23);
            this.lblColor.TabIndex = 13;
            // 
            // cmbColor
            // 
            this.cmbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColor.FormattingEnabled = true;
            this.cmbColor.Location = new System.Drawing.Point(81, 121);
            this.cmbColor.Name = "cmbColor";
            this.cmbColor.Size = new System.Drawing.Size(111, 21);
            this.cmbColor.TabIndex = 12;
            this.cmbColor.SelectedIndexChanged += new System.EventHandler(this.cmbColor_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(40, 124);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 13);
            this.label16.TabIndex = 11;
            this.label16.Text = "Color:";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(81, 182);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(218, 21);
            this.txtFileName.TabIndex = 4;
            this.txtFileName.TextChanged += new System.EventHandler(this.txtFileName_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(21, 185);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Filename:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLimit
            // 
            this.txtLimit.Location = new System.Drawing.Point(81, 95);
            this.txtLimit.Name = "txtLimit";
            this.txtLimit.Size = new System.Drawing.Size(218, 21);
            this.txtLimit.TabIndex = 4;
            this.txtLimit.TextChanged += new System.EventHandler(this.txtLimit_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(41, 98);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "Limit:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPermission
            // 
            this.txtPermission.Location = new System.Drawing.Point(81, 68);
            this.txtPermission.Name = "txtPermission";
            this.txtPermission.Size = new System.Drawing.Size(218, 21);
            this.txtPermission.TabIndex = 4;
            this.txtPermission.TextChanged += new System.EventHandler(this.txtPermission_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 71);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Permission:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRankName
            // 
            this.txtRankName.Location = new System.Drawing.Point(81, 41);
            this.txtRankName.Name = "txtRankName";
            this.txtRankName.Size = new System.Drawing.Size(218, 21);
            this.txtRankName.TabIndex = 4;
            this.txtRankName.TextChanged += new System.EventHandler(this.txtRankName_TextChanged);
            this.txtRankName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRankName_KeyDown);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(37, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Name:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(380, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Del";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAddRank
            // 
            this.btnAddRank.Location = new System.Drawing.Point(305, 6);
            this.btnAddRank.Name = "btnAddRank";
            this.btnAddRank.Size = new System.Drawing.Size(57, 23);
            this.btnAddRank.TabIndex = 1;
            this.btnAddRank.Text = "Add";
            this.btnAddRank.UseVisualStyleBackColor = true;
            this.btnAddRank.Click += new System.EventHandler(this.btnAddRank_Click);
            // 
            // listRanks
            // 
            this.listRanks.FormattingEnabled = true;
            this.listRanks.Location = new System.Drawing.Point(305, 35);
            this.listRanks.Name = "listRanks";
            this.listRanks.Size = new System.Drawing.Size(132, 407);
            this.listRanks.TabIndex = 0;
            this.listRanks.SelectedIndexChanged += new System.EventHandler(this.listRanks_SelectedIndexChanged);
            // 
            // grpAdminJoin
            // 
            this.grpAdminJoin.Controls.Add(this.chkAdminsJoinHidden);
            this.grpAdminJoin.Controls.Add(this.chkAdminsJoinSilent);
            this.grpAdminJoin.Location = new System.Drawing.Point(15, 393);
            this.grpAdminJoin.Name = "grpAdminJoin";
            this.grpAdminJoin.Size = new System.Drawing.Size(284, 49);
            this.grpAdminJoin.TabIndex = 17;
            this.grpAdminJoin.TabStop = false;
            this.grpAdminJoin.Text = "Admin (Permission 100 and higher) Joining";
            // 
            // chkAdminsJoinHidden
            // 
            this.chkAdminsJoinHidden.AutoSize = true;
            this.chkAdminsJoinHidden.Location = new System.Drawing.Point(126, 20);
            this.chkAdminsJoinHidden.Name = "chkAdminsJoinHidden";
            this.chkAdminsJoinHidden.Size = new System.Drawing.Size(118, 17);
            this.chkAdminsJoinHidden.TabIndex = 16;
            this.chkAdminsJoinHidden.Text = "Admins Join Hidden";
            this.chkAdminsJoinHidden.UseVisualStyleBackColor = true;
            // 
            // chkAdminsJoinSilent
            // 
            this.chkAdminsJoinSilent.AutoSize = true;
            this.chkAdminsJoinSilent.Location = new System.Drawing.Point(9, 20);
            this.chkAdminsJoinSilent.Name = "chkAdminsJoinSilent";
            this.chkAdminsJoinSilent.Size = new System.Drawing.Size(111, 17);
            this.chkAdminsJoinSilent.TabIndex = 15;
            this.chkAdminsJoinSilent.Text = "Admins Join Silent";
            this.chkAdminsJoinSilent.UseVisualStyleBackColor = true;
            // 
            // grpTimeRank
            // 
            this.grpTimeRank.Controls.Add(this.txtTimeRankCmd);
            this.grpTimeRank.Controls.Add(this.label58);
            this.grpTimeRank.Controls.Add(this.chkUseTimeRank);
            this.grpTimeRank.Location = new System.Drawing.Point(15, 312);
            this.grpTimeRank.Name = "grpTimeRank";
            this.grpTimeRank.Size = new System.Drawing.Size(284, 75);
            this.grpTimeRank.TabIndex = 19;
            this.grpTimeRank.TabStop = false;
            this.grpTimeRank.Text = "Time Ranking";
            // 
            // txtTimeRankCmd
            // 
            this.txtTimeRankCmd.Location = new System.Drawing.Point(100, 43);
            this.txtTimeRankCmd.Name = "txtTimeRankCmd";
            this.txtTimeRankCmd.Size = new System.Drawing.Size(178, 21);
            this.txtTimeRankCmd.TabIndex = 20;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(6, 46);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(88, 13);
            this.label58.TabIndex = 19;
            this.label58.Text = "Command Name:";
            // 
            // chkUseTimeRank
            // 
            this.chkUseTimeRank.AutoSize = true;
            this.chkUseTimeRank.Location = new System.Drawing.Point(9, 20);
            this.chkUseTimeRank.Name = "chkUseTimeRank";
            this.chkUseTimeRank.Size = new System.Drawing.Size(203, 17);
            this.chkUseTimeRank.TabIndex = 18;
            this.chkUseTimeRank.Text = "Allow Time Ranking (Requires Restart)";
            this.chkUseTimeRank.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.AutoScroll = true;
            this.tabPage3.Controls.Add(this.btnCmdHelp);
            this.tabPage3.Controls.Add(this.txtCmdRanks);
            this.tabPage3.Controls.Add(this.txtCmdAllow);
            this.tabPage3.Controls.Add(this.txtCmdLowest);
            this.tabPage3.Controls.Add(this.txtCmdDisallow);
            this.tabPage3.Controls.Add(this.label17);
            this.tabPage3.Controls.Add(this.label15);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.listCommands);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(444, 458);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Commands";
            this.toolTip.SetToolTip(this.tabPage3, "Which ranks can use which commands.");
            // 
            // btnCmdHelp
            // 
            this.btnCmdHelp.Location = new System.Drawing.Point(305, 6);
            this.btnCmdHelp.Name = "btnCmdHelp";
            this.btnCmdHelp.Size = new System.Drawing.Size(132, 23);
            this.btnCmdHelp.TabIndex = 25;
            this.btnCmdHelp.Text = "Help information";
            this.btnCmdHelp.UseVisualStyleBackColor = true;
            this.btnCmdHelp.Click += new System.EventHandler(this.btnCmdHelp_Click);
            // 
            // txtCmdRanks
            // 
            this.txtCmdRanks.Location = new System.Drawing.Point(11, 122);
            this.txtCmdRanks.Multiline = true;
            this.txtCmdRanks.Name = "txtCmdRanks";
            this.txtCmdRanks.ReadOnly = true;
            this.txtCmdRanks.Size = new System.Drawing.Size(288, 320);
            this.txtCmdRanks.TabIndex = 15;
            // 
            // txtCmdAllow
            // 
            this.txtCmdAllow.Location = new System.Drawing.Point(113, 95);
            this.txtCmdAllow.Name = "txtCmdAllow";
            this.txtCmdAllow.Size = new System.Drawing.Size(186, 21);
            this.txtCmdAllow.TabIndex = 14;
            this.txtCmdAllow.LostFocus += new System.EventHandler(this.txtCmdAllow_TextChanged);
            // 
            // txtCmdLowest
            // 
            this.txtCmdLowest.Location = new System.Drawing.Point(113, 41);
            this.txtCmdLowest.Name = "txtCmdLowest";
            this.txtCmdLowest.Size = new System.Drawing.Size(186, 21);
            this.txtCmdLowest.TabIndex = 14;
            this.txtCmdLowest.LostFocus += new System.EventHandler(this.txtCmdLowest_TextChanged);
            // 
            // txtCmdDisallow
            // 
            this.txtCmdDisallow.Location = new System.Drawing.Point(113, 68);
            this.txtCmdDisallow.Name = "txtCmdDisallow";
            this.txtCmdDisallow.Size = new System.Drawing.Size(186, 21);
            this.txtCmdDisallow.TabIndex = 14;
            this.txtCmdDisallow.LostFocus += new System.EventHandler(this.txtCmdDisallow_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(57, 99);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(56, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "And allow:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(33, 72);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "But don\'t allow:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Lowest rank needed:";
            // 
            // listCommands
            // 
            this.listCommands.FormattingEnabled = true;
            this.listCommands.Location = new System.Drawing.Point(305, 35);
            this.listCommands.Name = "listCommands";
            this.listCommands.Size = new System.Drawing.Size(132, 407);
            this.listCommands.TabIndex = 0;
            this.listCommands.SelectedIndexChanged += new System.EventHandler(this.listCommands_SelectedIndexChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.Transparent;
            this.tabPage5.Controls.Add(this.btnBlHelp);
            this.tabPage5.Controls.Add(this.txtBlRanks);
            this.tabPage5.Controls.Add(this.txtBlAllow);
            this.tabPage5.Controls.Add(this.txtBlLowest);
            this.tabPage5.Controls.Add(this.txtBlDisallow);
            this.tabPage5.Controls.Add(this.label18);
            this.tabPage5.Controls.Add(this.label19);
            this.tabPage5.Controls.Add(this.label20);
            this.tabPage5.Controls.Add(this.listBlocks);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(444, 458);
            this.tabPage5.TabIndex = 5;
            this.tabPage5.Text = "Blocks";
            // 
            // btnBlHelp
            // 
            this.btnBlHelp.Location = new System.Drawing.Point(305, 6);
            this.btnBlHelp.Name = "btnBlHelp";
            this.btnBlHelp.Size = new System.Drawing.Size(132, 23);
            this.btnBlHelp.TabIndex = 23;
            this.btnBlHelp.Text = "Help information";
            this.btnBlHelp.UseVisualStyleBackColor = true;
            this.btnBlHelp.Click += new System.EventHandler(this.btnBlHelp_Click);
            // 
            // txtBlRanks
            // 
            this.txtBlRanks.Location = new System.Drawing.Point(11, 122);
            this.txtBlRanks.Multiline = true;
            this.txtBlRanks.Name = "txtBlRanks";
            this.txtBlRanks.ReadOnly = true;
            this.txtBlRanks.Size = new System.Drawing.Size(288, 320);
            this.txtBlRanks.TabIndex = 22;
            // 
            // txtBlAllow
            // 
            this.txtBlAllow.Location = new System.Drawing.Point(113, 95);
            this.txtBlAllow.Name = "txtBlAllow";
            this.txtBlAllow.Size = new System.Drawing.Size(186, 21);
            this.txtBlAllow.TabIndex = 20;
            this.txtBlAllow.LostFocus += new System.EventHandler(this.txtBlAllow_TextChanged);
            // 
            // txtBlLowest
            // 
            this.txtBlLowest.Location = new System.Drawing.Point(113, 41);
            this.txtBlLowest.Name = "txtBlLowest";
            this.txtBlLowest.Size = new System.Drawing.Size(186, 21);
            this.txtBlLowest.TabIndex = 21;
            this.txtBlLowest.LostFocus += new System.EventHandler(this.txtBlLowest_TextChanged);
            // 
            // txtBlDisallow
            // 
            this.txtBlDisallow.Location = new System.Drawing.Point(113, 68);
            this.txtBlDisallow.Name = "txtBlDisallow";
            this.txtBlDisallow.Size = new System.Drawing.Size(186, 21);
            this.txtBlDisallow.TabIndex = 21;
            this.txtBlDisallow.LostFocus += new System.EventHandler(this.txtBlDisallow_TextChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(57, 99);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(56, 13);
            this.label18.TabIndex = 18;
            this.label18.Text = "And allow:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(33, 72);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(80, 13);
            this.label19.TabIndex = 17;
            this.label19.Text = "But don\'t allow:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(8, 44);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(105, 13);
            this.label20.TabIndex = 16;
            this.label20.Text = "Lowest rank needed:";
            // 
            // listBlocks
            // 
            this.listBlocks.FormattingEnabled = true;
            this.listBlocks.Location = new System.Drawing.Point(305, 35);
            this.listBlocks.Name = "listBlocks";
            this.listBlocks.Size = new System.Drawing.Size(133, 407);
            this.listBlocks.TabIndex = 15;
            this.listBlocks.SelectedIndexChanged += new System.EventHandler(this.listBlocks_SelectedIndexChanged);
            // 
            // tabPage7
            // 
            this.tabPage7.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage7.Controls.Add(this.groupBox1);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(444, 458);
            this.tabPage7.TabIndex = 7;
            this.tabPage7.Text = "MySQL";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label47);
            this.groupBox1.Controls.Add(this.label48);
            this.groupBox1.Controls.Add(this.btnDownloadMySQL);
            this.groupBox1.Controls.Add(this.chkUseMySQL);
            this.groupBox1.Controls.Add(this.label46);
            this.groupBox1.Controls.Add(this.txtMySQLHost);
            this.groupBox1.Controls.Add(this.label45);
            this.groupBox1.Controls.Add(this.txtMySQLPort);
            this.groupBox1.Controls.Add(this.label44);
            this.groupBox1.Controls.Add(this.txtMySQLUser);
            this.groupBox1.Controls.Add(this.label43);
            this.groupBox1.Controls.Add(this.txtMySQLPass);
            this.groupBox1.Controls.Add(this.label42);
            this.groupBox1.Controls.Add(this.txtMySQLDatabase);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(432, 452);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MySQL Settings";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(49, 263);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(339, 13);
            this.label47.TabIndex = 28;
            this.label47.Text = "You must have MySQL downloaded and installed for the MySQL to work!";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(23, 422);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(395, 13);
            this.label48.TabIndex = 7;
            this.label48.Text = "If the above settings don\'t work, please edit the MySQL settings in server.proper" +
    "ties.";
            // 
            // btnDownloadMySQL
            // 
            this.btnDownloadMySQL.Location = new System.Drawing.Point(143, 225);
            this.btnDownloadMySQL.Name = "btnDownloadMySQL";
            this.btnDownloadMySQL.Size = new System.Drawing.Size(161, 23);
            this.btnDownloadMySQL.TabIndex = 27;
            this.btnDownloadMySQL.Text = "Download MySQL";
            this.btnDownloadMySQL.UseVisualStyleBackColor = true;
            this.btnDownloadMySQL.Click += new System.EventHandler(this.btnDownloadMySQL_Click);
            // 
            // chkUseMySQL
            // 
            this.chkUseMySQL.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkUseMySQL.AutoSize = true;
            this.chkUseMySQL.Location = new System.Drawing.Point(29, 27);
            this.chkUseMySQL.Name = "chkUseMySQL";
            this.chkUseMySQL.Size = new System.Drawing.Size(68, 23);
            this.chkUseMySQL.TabIndex = 6;
            this.chkUseMySQL.Text = "Use MySQL";
            this.toolTip.SetToolTip(this.chkUseMySQL, "Whether to use the IRC bot or not.\nIRC stands for Internet Relay Chat and allows " +
        "for communication with the server while outside Minecraft.");
            this.chkUseMySQL.UseVisualStyleBackColor = true;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(8, 176);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(56, 13);
            this.label46.TabIndex = 26;
            this.label46.Text = "Database:";
            // 
            // txtMySQLHost
            // 
            this.txtMySQLHost.Location = new System.Drawing.Point(70, 65);
            this.txtMySQLHost.Name = "txtMySQLHost";
            this.txtMySQLHost.Size = new System.Drawing.Size(356, 21);
            this.txtMySQLHost.TabIndex = 17;
            this.toolTip.SetToolTip(this.txtMySQLHost, "MySQL Host. Should be 127.0.0.1");
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(8, 149);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(56, 13);
            this.label45.TabIndex = 25;
            this.label45.Text = "Password:";
            // 
            // txtMySQLPort
            // 
            this.txtMySQLPort.Location = new System.Drawing.Point(70, 92);
            this.txtMySQLPort.Name = "txtMySQLPort";
            this.txtMySQLPort.Size = new System.Drawing.Size(356, 21);
            this.txtMySQLPort.TabIndex = 18;
            this.toolTip.SetToolTip(this.txtMySQLPort, "MySQL Port. Should be 3306");
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(5, 122);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(59, 13);
            this.label44.TabIndex = 24;
            this.label44.Text = "Username:";
            // 
            // txtMySQLUser
            // 
            this.txtMySQLUser.Location = new System.Drawing.Point(70, 119);
            this.txtMySQLUser.Name = "txtMySQLUser";
            this.txtMySQLUser.Size = new System.Drawing.Size(356, 21);
            this.txtMySQLUser.TabIndex = 19;
            this.toolTip.SetToolTip(this.txtMySQLUser, "Your MySQL Username");
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(32, 95);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(30, 13);
            this.label43.TabIndex = 23;
            this.label43.Text = "Port:";
            // 
            // txtMySQLPass
            // 
            this.txtMySQLPass.Location = new System.Drawing.Point(70, 146);
            this.txtMySQLPass.Name = "txtMySQLPass";
            this.txtMySQLPass.Size = new System.Drawing.Size(356, 21);
            this.txtMySQLPass.TabIndex = 20;
            this.toolTip.SetToolTip(this.txtMySQLPass, "Your MySQL Password");
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(32, 68);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(32, 13);
            this.label42.TabIndex = 22;
            this.label42.Text = "Host:";
            // 
            // txtMySQLDatabase
            // 
            this.txtMySQLDatabase.Location = new System.Drawing.Point(70, 173);
            this.txtMySQLDatabase.Name = "txtMySQLDatabase";
            this.txtMySQLDatabase.Size = new System.Drawing.Size(356, 21);
            this.txtMySQLDatabase.TabIndex = 21;
            this.toolTip.SetToolTip(this.txtMySQLDatabase, "The MySQL Database Name");
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(366, 508);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDiscard
            // 
            this.btnDiscard.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDiscard.Location = new System.Drawing.Point(12, 508);
            this.btnDiscard.Name = "btnDiscard";
            this.btnDiscard.Size = new System.Drawing.Size(75, 23);
            this.btnDiscard.TabIndex = 1;
            this.btnDiscard.Text = "Discard";
            this.btnDiscard.UseVisualStyleBackColor = true;
            this.btnDiscard.Click += new System.EventHandler(this.btnDiscard_Click);
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Location = new System.Drawing.Point(285, 508);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 8000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.IsBalloon = true;
            this.toolTip.ReshowDelay = 100;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "Information";
            this.toolTip.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip_Popup);
            // 
            // PropertyWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 537);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnDiscard);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PropertyWindow";
            this.Text = "Properties";
            this.Load += new System.EventHandler(this.PropertyWindow_Load);
            this.Disposed += new System.EventHandler(this.PropertyWindow_Unload);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.grpAdminSecurity.ResumeLayout(false);
            this.grpAdminSecurity.PerformLayout();
            this.grpMaintenence.ResumeLayout(false);
            this.grpMaintenence.PerformLayout();
            this.grpServSettings.ResumeLayout(false);
            this.grpServSettings.PerformLayout();
            this.grpAntiTunnel.ResumeLayout(false);
            this.grpAntiTunnel.PerformLayout();
            this.grpHomes.ResumeLayout(false);
            this.grpHomes.PerformLayout();
            this.grpServOptions.ResumeLayout(false);
            this.grpServOptions.PerformLayout();
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            this.grpCreateNewText.ResumeLayout(false);
            this.grpCreateNewText.PerformLayout();
            this.grpProfanityFilter.ResumeLayout(false);
            this.grpProfanityFilter.PerformLayout();
            this.grpAntiCaps.ResumeLayout(false);
            this.grpAntiCaps.PerformLayout();
            this.grpAntiSpam.ResumeLayout(false);
            this.grpAntiSpam.PerformLayout();
            this.grpChatChannels.ResumeLayout(false);
            this.grpChatChannels.PerformLayout();
            this.grpChatColors.ResumeLayout(false);
            this.grpChatColors.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.grpGlobalChat.ResumeLayout(false);
            this.grpGlobalChat.PerformLayout();
            this.grpIRC.ResumeLayout(false);
            this.grpIRC.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.grpThrottle.ResumeLayout(false);
            this.grpThrottle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbThrottle)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.grpAdminJoin.ResumeLayout(false);
            this.grpAdminJoin.PerformLayout();
            this.grpTimeRank.ResumeLayout(false);
            this.grpTimeRank.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox chkPublic;
        private System.Windows.Forms.CheckBox chkWorld;
        private System.Windows.Forms.CheckBox chkVerify;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMOTD;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDiscard;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtMaps;
        private System.Windows.Forms.TextBox txtPlayers;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox chkUpdates;
        private System.Windows.Forms.CheckBox chkAutoload;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtMain;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox txtRestartTime;
        private System.Windows.Forms.CheckBox chkRestartTime;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txtBackupLocation;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtNormRp;
        private System.Windows.Forms.TextBox txtRP;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtAFKKick;
        private System.Windows.Forms.TextBox txtafk;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBackup;
        private System.Windows.Forms.CheckBox chkrankSuper;
        private System.Windows.Forms.CheckBox chkCheap;
        private System.Windows.Forms.CheckBox chkDeath;
        private System.Windows.Forms.CheckBox chk17Dollar;
        private System.Windows.Forms.CheckBox chkPhysicsRest;
        private System.Windows.Forms.CheckBox chkSmile;
        private System.Windows.Forms.CheckBox chkHelp;
        private System.Windows.Forms.TextBox txtCheap;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox txtMoneys;
        private System.Windows.Forms.CheckBox chkMono;
        private System.Windows.Forms.TextBox txtShutdown;
        private System.Windows.Forms.TextBox txtBanMessage;
        private System.Windows.Forms.CheckBox chkShutdown;
        private System.Windows.Forms.CheckBox chkBanMessage;
        private System.Windows.Forms.ComboBox cmbDefaultRank;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.ComboBox cmbColor;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtLimit;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtPermission;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtRankName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnAddRank;
        private System.Windows.Forms.ListBox listRanks;
        private System.Windows.Forms.CheckBox chkRestart;
        private System.Windows.Forms.TextBox txtDepth;
        private System.Windows.Forms.CheckBox ChkTunnels;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkLogBeat;
        private System.Windows.Forms.TextBox txtCmdAllow;
        private System.Windows.Forms.TextBox txtCmdDisallow;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox listCommands;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TextBox txtBlAllow;
        private System.Windows.Forms.TextBox txtBlDisallow;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ListBox listBlocks;
        private System.Windows.Forms.TextBox txtCmdLowest;
        private System.Windows.Forms.TextBox txtBlLowest;
        private System.Windows.Forms.TextBox txtCmdRanks;
        private System.Windows.Forms.TextBox txtBlRanks;
        private System.Windows.Forms.Button btnBlHelp;
        private System.Windows.Forms.Button btnCmdHelp;
        private System.Windows.Forms.CheckBox chkForceCuboid;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.CheckBox chkRepeatMessages;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TextBox txtNick;
        private System.Windows.Forms.TextBox txtOpChannel;
        private System.Windows.Forms.TextBox txtChannel;
        private System.Windows.Forms.TextBox txtIRCServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkIRC;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.ComboBox cmbMaintJoin;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.CheckBox chkGlobalChat;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.GroupBox grpGlobalChat;
        private System.Windows.Forms.GroupBox grpIRC;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.CheckBox chkUseMySQL;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Button btnDownloadMySQL;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox txtMySQLDatabase;
        private System.Windows.Forms.TextBox txtMySQLPass;
        private System.Windows.Forms.TextBox txtMySQLUser;
        private System.Windows.Forms.TextBox txtMySQLPort;
        private System.Windows.Forms.TextBox txtMySQLHost;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.GroupBox grpMaintenence;
        private System.Windows.Forms.GroupBox grpServOptions;
        private System.Windows.Forms.GroupBox grpServSettings;
        private System.Windows.Forms.GroupBox grpAntiTunnel;
        private System.Windows.Forms.CheckBox chkTpToHigher;
        private System.Windows.Forms.Button btnPortCheck;
        private System.Windows.Forms.Label lblPortCheckResult;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.GroupBox grpAntiSpam;
        private System.Windows.Forms.CheckBox chkAntiSpamOp;
        private System.Windows.Forms.ComboBox cmbAntiSpamConsequence;
        private System.Windows.Forms.CheckBox chkAntiSpam;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.GroupBox grpAntiCaps;
        private System.Windows.Forms.ComboBox cmbAntiCapsConsequence;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.CheckBox chkAntiCapsOp;
        private System.Windows.Forms.CheckBox chkAntiCaps;
        public System.Windows.Forms.TextBox txtGlobalNick;
        private System.Windows.Forms.GroupBox grpAdminSecurity;
        private System.Windows.Forms.ComboBox cmbAdminSecurityRank;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.CheckBox chkAdminSecurity;
        private System.Windows.Forms.ComboBox cmbIRCColour;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.ComboBox cmbDefaultColour;
        private System.Windows.Forms.ComboBox cmbOpChat;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.ComboBox cmbAdminChat;
        private System.Windows.Forms.Label lblGlobalColour;
        private System.Windows.Forms.ComboBox cmbGlobalColour;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.GroupBox grpChatColors;
        private System.Windows.Forms.GroupBox grpChatChannels;
        private System.Windows.Forms.TextBox txtMsgsRequired;
        private System.Windows.Forms.TextBox txtCapsRequired;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.TextBox txtFlags;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDefault;
        private System.Windows.Forms.Label lblIRC;
        private System.Windows.Forms.CheckBox chkUseMaxMind;
        private System.Windows.Forms.GroupBox grpAdminJoin;
        private System.Windows.Forms.CheckBox chkAdminsJoinHidden;
        private System.Windows.Forms.CheckBox chkAdminsJoinSilent;
        private System.Windows.Forms.CheckBox chkAgreeToRules;
        private System.Windows.Forms.CheckBox chkAllowProxy;
        private System.Windows.Forms.TextBox txtUnCheap;
        private System.Windows.Forms.CheckBox chkUnCheap;
        private System.Windows.Forms.GroupBox grpHomes;
        private System.Windows.Forms.ComboBox cmbHomeX;
        private System.Windows.Forms.Label lblHomePrefix;
        private System.Windows.Forms.TextBox txtHomePrefix;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.ComboBox cmbHomeZ;
        private System.Windows.Forms.ComboBox cmbHomeY;
        private System.Windows.Forms.CheckBox chkShowAttemptedLogins;
        private System.Windows.Forms.Button btnEditText;
        private System.Windows.Forms.GroupBox grpProfanityFilter;
        private System.Windows.Forms.CheckBox chkProfanityFilterOp;
        private System.Windows.Forms.CheckBox chkProfanityFilter;
        private System.Windows.Forms.CheckBox chkSwearWarnPlayer;
        private System.Windows.Forms.Button btnEditSwearWords;
        private System.Windows.Forms.ComboBox cmbProfanityFilterStyle;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.TextBox txtSwearWordsRequired;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.CheckBox chkUseAntiGrief;
        private System.Windows.Forms.CheckBox chkAllowIgnoreOps;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.ComboBox cmbHomePerm;
        private System.Windows.Forms.GroupBox grpTimeRank;
        private System.Windows.Forms.TextBox txtTimeRankCmd;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.CheckBox chkUseTimeRank;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.TextBox txtReqHours;
        private System.Windows.Forms.CheckBox chkUseWOM;
        private System.Windows.Forms.CheckBox chkWomText;
        private System.Windows.Forms.Button btnCreateNewText;
        private System.Windows.Forms.GroupBox grpCreateNewText;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.TextBox txtCreateNewText;
        private System.Windows.Forms.CheckBox chkUPnP;
        private System.Windows.Forms.CheckBox chkIrcIdentify;
        private System.Windows.Forms.TextBox txtIrcPass;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.TextBox txtGlobalPass;
        private System.Windows.Forms.CheckBox chkGlobalIdentify;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.TextBox txtMaxGuests;
        private System.Windows.Forms.CheckBox chkUseDiscourager;
        private System.Windows.Forms.GroupBox grpThrottle;
        private System.Windows.Forms.TrackBar tbThrottle;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.TextBox txtThrottleDesc;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.CheckBox chkUseWOMPasswords;
        private System.Windows.Forms.TextBox txtWOMIPAddress;
        private System.Windows.Forms.Label label69;
    }
}