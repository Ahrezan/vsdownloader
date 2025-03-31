namespace vsd5_1
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl = new MaterialSkin.Controls.MaterialTabControl();
            this.ytPage = new System.Windows.Forms.TabPage();
            this.compCard = new MaterialSkin.Controls.MaterialCard();
            this.btnClearHistory = new MaterialSkin.Controls.MaterialButton();
            this.listViewCompYT = new MaterialSkin.Controls.MaterialListView();
            this.loadCard = new MaterialSkin.Controls.MaterialCard();
            this.btnYTCancel = new MaterialSkin.Controls.MaterialButton();
            this.listViewLoadYT = new MaterialSkin.Controls.MaterialListView();
            this.listCard = new MaterialSkin.Controls.MaterialCard();
            this.chkSelectYTAll = new MaterialSkin.Controls.MaterialCheckbox();
            this.cmbYTQuality = new MaterialSkin.Controls.MaterialComboBox();
            this.btnYTDownload = new MaterialSkin.Controls.MaterialButton();
            this.cmbYTFormat = new MaterialSkin.Controls.MaterialComboBox();
            this.listViewYT = new MaterialSkin.Controls.MaterialListView();
            this.searchCard = new MaterialSkin.Controls.MaterialCard();
            this.txtUrl = new MaterialSkin.Controls.MaterialMaskedTextBox();
            this.btnLoad = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.icon = new System.Windows.Forms.ImageList(this.components);
            this.spotPage = new System.Windows.Forms.TabPage();
            this.settingsPage = new System.Windows.Forms.TabPage();
            this.lblTheme = new MaterialSkin.Controls.MaterialLabel();
            this.lblGeneral = new MaterialSkin.Controls.MaterialLabel();
            this.btnDefaultSettings = new MaterialSkin.Controls.MaterialButton();
            this.btnSave = new MaterialSkin.Controls.MaterialButton();
            this.lblDownload = new MaterialSkin.Controls.MaterialLabel();
            this.dwnCard = new MaterialSkin.Controls.MaterialCard();
            this.materialButton1 = new MaterialSkin.Controls.MaterialButton();
            this.materialButton2 = new MaterialSkin.Controls.MaterialButton();
            this.sliderConcurrent = new MaterialSkin.Controls.MaterialSlider();
            this.chkForceIPv4 = new MaterialSkin.Controls.MaterialCheckbox();
            this.chkOverwrite = new MaterialSkin.Controls.MaterialCheckbox();
            this.btnClearCache = new MaterialSkin.Controls.MaterialButton();
            this.chkAlwaysFolder = new MaterialSkin.Controls.MaterialCheckbox();
            this.gnrlCard = new MaterialSkin.Controls.MaterialCard();
            this.cmbLang = new MaterialSkin.Controls.MaterialComboBox();
            this.txtPath = new MaterialSkin.Controls.MaterialTextBox();
            this.txtFolderParameter = new MaterialSkin.Controls.MaterialTextBox2();
            this.btnSelect = new MaterialSkin.Controls.MaterialButton();
            this.txtFileParameter = new MaterialSkin.Controls.MaterialTextBox2();
            this.thmCard = new MaterialSkin.Controls.MaterialCard();
            this.radioLight = new MaterialSkin.Controls.MaterialRadioButton();
            this.radioDark = new MaterialSkin.Controls.MaterialRadioButton();
            this.radioAuto = new MaterialSkin.Controls.MaterialRadioButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl.SuspendLayout();
            this.ytPage.SuspendLayout();
            this.compCard.SuspendLayout();
            this.loadCard.SuspendLayout();
            this.listCard.SuspendLayout();
            this.searchCard.SuspendLayout();
            this.settingsPage.SuspendLayout();
            this.dwnCard.SuspendLayout();
            this.gnrlCard.SuspendLayout();
            this.thmCard.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.ytPage);
            this.tabControl.Controls.Add(this.spotPage);
            this.tabControl.Controls.Add(this.settingsPage);
            this.tabControl.Depth = 0;
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.ImageList = this.icon;
            this.tabControl.Location = new System.Drawing.Point(3, 64);
            this.tabControl.MouseState = MaterialSkin.MouseState.HOVER;
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1274, 653);
            this.tabControl.TabIndex = 0;
            // 
            // ytPage
            // 
            this.ytPage.Controls.Add(this.compCard);
            this.ytPage.Controls.Add(this.loadCard);
            this.ytPage.Controls.Add(this.listCard);
            this.ytPage.Controls.Add(this.searchCard);
            this.ytPage.ImageKey = "youtubeLogo.png";
            this.ytPage.Location = new System.Drawing.Point(4, 35);
            this.ytPage.Name = "ytPage";
            this.ytPage.Padding = new System.Windows.Forms.Padding(3);
            this.ytPage.Size = new System.Drawing.Size(1266, 614);
            this.ytPage.TabIndex = 0;
            this.ytPage.Text = "YouTube";
            this.ytPage.UseVisualStyleBackColor = true;
            // 
            // compCard
            // 
            this.compCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.compCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.compCard.Controls.Add(this.btnClearHistory);
            this.compCard.Controls.Add(this.listViewCompYT);
            this.compCard.Depth = 0;
            this.compCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.compCard.Location = new System.Drawing.Point(1259, 85);
            this.compCard.Margin = new System.Windows.Forms.Padding(14);
            this.compCard.MouseState = MaterialSkin.MouseState.HOVER;
            this.compCard.Name = "compCard";
            this.compCard.Padding = new System.Windows.Forms.Padding(14);
            this.compCard.Size = new System.Drawing.Size(363, 497);
            this.compCard.TabIndex = 3;
            this.compCard.Visible = false;
            // 
            // btnClearHistory
            // 
            this.btnClearHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearHistory.AutoSize = false;
            this.btnClearHistory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClearHistory.BackColor = System.Drawing.Color.Transparent;
            this.btnClearHistory.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Dense;
            this.btnClearHistory.Depth = 0;
            this.btnClearHistory.HighEmphasis = true;
            this.btnClearHistory.Icon = ((System.Drawing.Image)(resources.GetObject("btnClearHistory.Icon")));
            this.btnClearHistory.Location = new System.Drawing.Point(116, 431);
            this.btnClearHistory.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnClearHistory.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnClearHistory.Name = "btnClearHistory";
            this.btnClearHistory.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnClearHistory.Size = new System.Drawing.Size(230, 49);
            this.btnClearHistory.TabIndex = 7;
            this.btnClearHistory.Text = "İndirilenleri Temizle";
            this.btnClearHistory.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnClearHistory.UseAccentColor = false;
            this.btnClearHistory.UseVisualStyleBackColor = false;
            this.btnClearHistory.Visible = false;
            // 
            // listViewCompYT
            // 
            this.listViewCompYT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewCompYT.AutoSizeTable = false;
            this.listViewCompYT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.listViewCompYT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewCompYT.Depth = 0;
            this.listViewCompYT.FullRowSelect = true;
            this.listViewCompYT.HideSelection = false;
            this.listViewCompYT.Location = new System.Drawing.Point(17, 17);
            this.listViewCompYT.MinimumSize = new System.Drawing.Size(200, 100);
            this.listViewCompYT.MouseLocation = new System.Drawing.Point(-1, -1);
            this.listViewCompYT.MouseState = MaterialSkin.MouseState.OUT;
            this.listViewCompYT.Name = "listViewCompYT";
            this.listViewCompYT.OwnerDraw = true;
            this.listViewCompYT.Size = new System.Drawing.Size(329, 405);
            this.listViewCompYT.TabIndex = 6;
            this.listViewCompYT.UseCompatibleStateImageBehavior = false;
            this.listViewCompYT.View = System.Windows.Forms.View.Details;
            // 
            // loadCard
            // 
            this.loadCard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.loadCard.Controls.Add(this.btnYTCancel);
            this.loadCard.Controls.Add(this.listViewLoadYT);
            this.loadCard.Depth = 0;
            this.loadCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.loadCard.Location = new System.Drawing.Point(640, 85);
            this.loadCard.Margin = new System.Windows.Forms.Padding(14);
            this.loadCard.MouseState = MaterialSkin.MouseState.HOVER;
            this.loadCard.Name = "loadCard";
            this.loadCard.Padding = new System.Windows.Forms.Padding(14);
            this.loadCard.Size = new System.Drawing.Size(609, 497);
            this.loadCard.TabIndex = 2;
            // 
            // btnYTCancel
            // 
            this.btnYTCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnYTCancel.AutoSize = false;
            this.btnYTCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnYTCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnYTCancel.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Dense;
            this.btnYTCancel.Depth = 0;
            this.btnYTCancel.HighEmphasis = true;
            this.btnYTCancel.Icon = ((System.Drawing.Image)(resources.GetObject("btnYTCancel.Icon")));
            this.btnYTCancel.Location = new System.Drawing.Point(383, 431);
            this.btnYTCancel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnYTCancel.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnYTCancel.Name = "btnYTCancel";
            this.btnYTCancel.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnYTCancel.Size = new System.Drawing.Size(209, 50);
            this.btnYTCancel.TabIndex = 12;
            this.btnYTCancel.Text = "İndirmeleri İptal Et";
            this.btnYTCancel.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnYTCancel.UseAccentColor = true;
            this.btnYTCancel.UseVisualStyleBackColor = false;
            // 
            // listViewLoadYT
            // 
            this.listViewLoadYT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewLoadYT.AutoSizeTable = false;
            this.listViewLoadYT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.listViewLoadYT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewLoadYT.Depth = 0;
            this.listViewLoadYT.FullRowSelect = true;
            this.listViewLoadYT.HideSelection = false;
            this.listViewLoadYT.Location = new System.Drawing.Point(17, 17);
            this.listViewLoadYT.MinimumSize = new System.Drawing.Size(200, 100);
            this.listViewLoadYT.MouseLocation = new System.Drawing.Point(-1, -1);
            this.listViewLoadYT.MouseState = MaterialSkin.MouseState.OUT;
            this.listViewLoadYT.Name = "listViewLoadYT";
            this.listViewLoadYT.OwnerDraw = true;
            this.listViewLoadYT.Size = new System.Drawing.Size(575, 405);
            this.listViewLoadYT.TabIndex = 1;
            this.listViewLoadYT.UseCompatibleStateImageBehavior = false;
            this.listViewLoadYT.View = System.Windows.Forms.View.Details;
            // 
            // listCard
            // 
            this.listCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.listCard.Controls.Add(this.chkSelectYTAll);
            this.listCard.Controls.Add(this.cmbYTQuality);
            this.listCard.Controls.Add(this.btnYTDownload);
            this.listCard.Controls.Add(this.cmbYTFormat);
            this.listCard.Controls.Add(this.listViewYT);
            this.listCard.Depth = 0;
            this.listCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.listCard.Location = new System.Drawing.Point(17, 85);
            this.listCard.Margin = new System.Windows.Forms.Padding(14);
            this.listCard.MouseState = MaterialSkin.MouseState.HOVER;
            this.listCard.Name = "listCard";
            this.listCard.Padding = new System.Windows.Forms.Padding(14);
            this.listCard.Size = new System.Drawing.Size(609, 497);
            this.listCard.TabIndex = 1;
            // 
            // chkSelectYTAll
            // 
            this.chkSelectYTAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkSelectYTAll.AutoSize = true;
            this.chkSelectYTAll.Checked = true;
            this.chkSelectYTAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSelectYTAll.Depth = 0;
            this.chkSelectYTAll.Location = new System.Drawing.Point(17, 391);
            this.chkSelectYTAll.Margin = new System.Windows.Forms.Padding(0);
            this.chkSelectYTAll.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkSelectYTAll.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkSelectYTAll.Name = "chkSelectYTAll";
            this.chkSelectYTAll.ReadOnly = false;
            this.chkSelectYTAll.Ripple = true;
            this.chkSelectYTAll.Size = new System.Drawing.Size(125, 37);
            this.chkSelectYTAll.TabIndex = 11;
            this.chkSelectYTAll.Text = "Tümünü Seç";
            this.chkSelectYTAll.UseVisualStyleBackColor = true;
            // 
            // cmbYTQuality
            // 
            this.cmbYTQuality.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbYTQuality.AutoResize = false;
            this.cmbYTQuality.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbYTQuality.Depth = 0;
            this.cmbYTQuality.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbYTQuality.DropDownHeight = 174;
            this.cmbYTQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYTQuality.DropDownWidth = 121;
            this.cmbYTQuality.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbYTQuality.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbYTQuality.FormattingEnabled = true;
            this.cmbYTQuality.IntegralHeight = false;
            this.cmbYTQuality.ItemHeight = 43;
            this.cmbYTQuality.Location = new System.Drawing.Point(130, 431);
            this.cmbYTQuality.MaxDropDownItems = 4;
            this.cmbYTQuality.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbYTQuality.Name = "cmbYTQuality";
            this.cmbYTQuality.Size = new System.Drawing.Size(174, 49);
            this.cmbYTQuality.StartIndex = 0;
            this.cmbYTQuality.TabIndex = 10;
            // 
            // btnYTDownload
            // 
            this.btnYTDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnYTDownload.AutoSize = false;
            this.btnYTDownload.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnYTDownload.BackColor = System.Drawing.Color.Transparent;
            this.btnYTDownload.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Dense;
            this.btnYTDownload.Depth = 0;
            this.btnYTDownload.HighEmphasis = true;
            this.btnYTDownload.Icon = ((System.Drawing.Image)(resources.GetObject("btnYTDownload.Icon")));
            this.btnYTDownload.Location = new System.Drawing.Point(434, 434);
            this.btnYTDownload.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnYTDownload.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnYTDownload.Name = "btnYTDownload";
            this.btnYTDownload.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnYTDownload.Size = new System.Drawing.Size(157, 49);
            this.btnYTDownload.TabIndex = 8;
            this.btnYTDownload.Text = "İndir";
            this.btnYTDownload.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnYTDownload.UseAccentColor = true;
            this.btnYTDownload.UseVisualStyleBackColor = false;
            // 
            // cmbYTFormat
            // 
            this.cmbYTFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbYTFormat.AutoResize = false;
            this.cmbYTFormat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbYTFormat.Depth = 0;
            this.cmbYTFormat.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbYTFormat.DropDownHeight = 174;
            this.cmbYTFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYTFormat.DropDownWidth = 121;
            this.cmbYTFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbYTFormat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbYTFormat.FormattingEnabled = true;
            this.cmbYTFormat.IntegralHeight = false;
            this.cmbYTFormat.ItemHeight = 43;
            this.cmbYTFormat.Location = new System.Drawing.Point(20, 431);
            this.cmbYTFormat.MaxDropDownItems = 4;
            this.cmbYTFormat.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbYTFormat.Name = "cmbYTFormat";
            this.cmbYTFormat.Size = new System.Drawing.Size(104, 49);
            this.cmbYTFormat.StartIndex = 0;
            this.cmbYTFormat.TabIndex = 9;
            // 
            // listViewYT
            // 
            this.listViewYT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewYT.AutoSizeTable = false;
            this.listViewYT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.listViewYT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewYT.Depth = 0;
            this.listViewYT.FullRowSelect = true;
            this.listViewYT.HideSelection = false;
            this.listViewYT.Location = new System.Drawing.Point(17, 17);
            this.listViewYT.MinimumSize = new System.Drawing.Size(200, 100);
            this.listViewYT.MouseLocation = new System.Drawing.Point(-1, -1);
            this.listViewYT.MouseState = MaterialSkin.MouseState.OUT;
            this.listViewYT.Name = "listViewYT";
            this.listViewYT.OwnerDraw = true;
            this.listViewYT.Size = new System.Drawing.Size(575, 376);
            this.listViewYT.TabIndex = 1;
            this.listViewYT.UseCompatibleStateImageBehavior = false;
            this.listViewYT.View = System.Windows.Forms.View.Details;
            // 
            // searchCard
            // 
            this.searchCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.searchCard.Controls.Add(this.txtUrl);
            this.searchCard.Controls.Add(this.btnLoad);
            this.searchCard.Depth = 0;
            this.searchCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.searchCard.Location = new System.Drawing.Point(17, 17);
            this.searchCard.Margin = new System.Windows.Forms.Padding(14);
            this.searchCard.MouseState = MaterialSkin.MouseState.HOVER;
            this.searchCard.Name = "searchCard";
            this.searchCard.Padding = new System.Windows.Forms.Padding(14);
            this.searchCard.Size = new System.Drawing.Size(1232, 58);
            this.searchCard.TabIndex = 0;
            // 
            // txtUrl
            // 
            this.txtUrl.AllowPromptAsInput = true;
            this.txtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUrl.AnimateReadOnly = false;
            this.txtUrl.AsciiOnly = false;
            this.txtUrl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtUrl.BeepOnError = false;
            this.txtUrl.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.txtUrl.Depth = 0;
            this.txtUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtUrl.HidePromptOnLeave = false;
            this.txtUrl.HideSelection = true;
            this.txtUrl.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Default;
            this.txtUrl.LeadingIcon = null;
            this.txtUrl.Location = new System.Drawing.Point(17, 11);
            this.txtUrl.Mask = "";
            this.txtUrl.MaxLength = 32767;
            this.txtUrl.MouseState = MaterialSkin.MouseState.OUT;
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.PasswordChar = '\0';
            this.txtUrl.PrefixSuffixText = null;
            this.txtUrl.PromptChar = '_';
            this.txtUrl.ReadOnly = false;
            this.txtUrl.RejectInputOnFirstFailure = false;
            this.txtUrl.ResetOnPrompt = true;
            this.txtUrl.ResetOnSpace = true;
            this.txtUrl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUrl.SelectedText = "";
            this.txtUrl.SelectionLength = 0;
            this.txtUrl.SelectionStart = 0;
            this.txtUrl.ShortcutsEnabled = true;
            this.txtUrl.Size = new System.Drawing.Size(1145, 36);
            this.txtUrl.SkipLiterals = true;
            this.txtUrl.TabIndex = 1;
            this.txtUrl.TabStop = false;
            this.txtUrl.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtUrl.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.txtUrl.TrailingIcon = null;
            this.txtUrl.UseSystemPasswordChar = false;
            this.txtUrl.UseTallSize = false;
            this.txtUrl.ValidatingType = null;
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Depth = 0;
            this.btnLoad.Icon = global::vsd5_1.Properties.Resources.search;
            this.btnLoad.ImageKey = "(none)";
            this.btnLoad.ImageList = this.icon;
            this.btnLoad.Location = new System.Drawing.Point(1175, 9);
            this.btnLoad.Mini = true;
            this.btnLoad.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(40, 40);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.UseVisualStyleBackColor = true;
            // 
            // icon
            // 
            this.icon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("icon.ImageStream")));
            this.icon.TransparentColor = System.Drawing.Color.Transparent;
            this.icon.Images.SetKeyName(0, "youtubeLogo.png");
            this.icon.Images.SetKeyName(1, "spotifylogo.png");
            this.icon.Images.SetKeyName(2, "settings.png");
            this.icon.Images.SetKeyName(3, "search.png");
            this.icon.Images.SetKeyName(4, "download.png");
            // 
            // spotPage
            // 
            this.spotPage.ImageKey = "spotifylogo.png";
            this.spotPage.Location = new System.Drawing.Point(4, 35);
            this.spotPage.Name = "spotPage";
            this.spotPage.Padding = new System.Windows.Forms.Padding(3);
            this.spotPage.Size = new System.Drawing.Size(1266, 614);
            this.spotPage.TabIndex = 1;
            this.spotPage.Text = "Spotify";
            this.spotPage.UseVisualStyleBackColor = true;
            // 
            // settingsPage
            // 
            this.settingsPage.Controls.Add(this.lblTheme);
            this.settingsPage.Controls.Add(this.lblGeneral);
            this.settingsPage.Controls.Add(this.btnDefaultSettings);
            this.settingsPage.Controls.Add(this.btnSave);
            this.settingsPage.Controls.Add(this.lblDownload);
            this.settingsPage.Controls.Add(this.dwnCard);
            this.settingsPage.Controls.Add(this.gnrlCard);
            this.settingsPage.Controls.Add(this.thmCard);
            this.settingsPage.ImageKey = "settings.png";
            this.settingsPage.Location = new System.Drawing.Point(4, 35);
            this.settingsPage.Name = "settingsPage";
            this.settingsPage.Size = new System.Drawing.Size(1266, 614);
            this.settingsPage.TabIndex = 2;
            this.settingsPage.Text = "Ayarlar";
            this.settingsPage.UseVisualStyleBackColor = true;
            // 
            // lblTheme
            // 
            this.lblTheme.AutoSize = true;
            this.lblTheme.Depth = 0;
            this.lblTheme.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTheme.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTheme.Location = new System.Drawing.Point(700, 9);
            this.lblTheme.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblTheme.Name = "lblTheme";
            this.lblTheme.Size = new System.Drawing.Size(42, 19);
            this.lblTheme.TabIndex = 23;
            this.lblTheme.Text = "Tema";
            this.lblTheme.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGeneral
            // 
            this.lblGeneral.AutoSize = true;
            this.lblGeneral.Depth = 0;
            this.lblGeneral.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblGeneral.Location = new System.Drawing.Point(28, 9);
            this.lblGeneral.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblGeneral.Name = "lblGeneral";
            this.lblGeneral.Size = new System.Drawing.Size(41, 19);
            this.lblGeneral.TabIndex = 18;
            this.lblGeneral.Text = "Genel";
            this.lblGeneral.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDefaultSettings
            // 
            this.btnDefaultSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefaultSettings.AutoSize = false;
            this.btnDefaultSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDefaultSettings.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnDefaultSettings.Depth = 0;
            this.btnDefaultSettings.HighEmphasis = true;
            this.btnDefaultSettings.Icon = null;
            this.btnDefaultSettings.Location = new System.Drawing.Point(839, 554);
            this.btnDefaultSettings.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnDefaultSettings.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnDefaultSettings.Name = "btnDefaultSettings";
            this.btnDefaultSettings.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnDefaultSettings.Size = new System.Drawing.Size(303, 36);
            this.btnDefaultSettings.TabIndex = 22;
            this.btnDefaultSettings.Text = "Varsayılana Dön";
            this.btnDefaultSettings.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnDefaultSettings.UseAccentColor = false;
            this.btnDefaultSettings.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.AutoSize = false;
            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSave.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSave.Depth = 0;
            this.btnSave.HighEmphasis = true;
            this.btnSave.Icon = null;
            this.btnSave.Location = new System.Drawing.Point(1150, 554);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSave.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSave.Name = "btnSave";
            this.btnSave.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSave.Size = new System.Drawing.Size(112, 36);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "Kaydet";
            this.btnSave.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSave.UseAccentColor = false;
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // lblDownload
            // 
            this.lblDownload.AutoSize = true;
            this.lblDownload.Depth = 0;
            this.lblDownload.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblDownload.Location = new System.Drawing.Point(28, 171);
            this.lblDownload.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblDownload.Name = "lblDownload";
            this.lblDownload.Size = new System.Drawing.Size(54, 19);
            this.lblDownload.TabIndex = 20;
            this.lblDownload.Text = "İndirme";
            this.lblDownload.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dwnCard
            // 
            this.dwnCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dwnCard.Controls.Add(this.materialButton1);
            this.dwnCard.Controls.Add(this.materialButton2);
            this.dwnCard.Controls.Add(this.sliderConcurrent);
            this.dwnCard.Controls.Add(this.chkForceIPv4);
            this.dwnCard.Controls.Add(this.chkOverwrite);
            this.dwnCard.Controls.Add(this.btnClearCache);
            this.dwnCard.Controls.Add(this.chkAlwaysFolder);
            this.dwnCard.Depth = 0;
            this.dwnCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dwnCard.Location = new System.Drawing.Point(14, 180);
            this.dwnCard.Margin = new System.Windows.Forms.Padding(14);
            this.dwnCard.MouseState = MaterialSkin.MouseState.HOVER;
            this.dwnCard.Name = "dwnCard";
            this.dwnCard.Padding = new System.Windows.Forms.Padding(14);
            this.dwnCard.Size = new System.Drawing.Size(621, 181);
            this.dwnCard.TabIndex = 19;
            // 
            // materialButton1
            // 
            this.materialButton1.AutoSize = false;
            this.materialButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton1.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton1.Depth = 0;
            this.materialButton1.HighEmphasis = true;
            this.materialButton1.Icon = null;
            this.materialButton1.Location = new System.Drawing.Point(391, 125);
            this.materialButton1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton1.Name = "materialButton1";
            this.materialButton1.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton1.Size = new System.Drawing.Size(204, 36);
            this.materialButton1.TabIndex = 7;
            this.materialButton1.Text = "YouTube Api";
            this.materialButton1.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton1.UseAccentColor = false;
            this.materialButton1.UseVisualStyleBackColor = true;
            // 
            // materialButton2
            // 
            this.materialButton2.AutoSize = false;
            this.materialButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton2.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton2.Depth = 0;
            this.materialButton2.HighEmphasis = true;
            this.materialButton2.Icon = null;
            this.materialButton2.Location = new System.Drawing.Point(391, 72);
            this.materialButton2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton2.Name = "materialButton2";
            this.materialButton2.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton2.Size = new System.Drawing.Size(204, 36);
            this.materialButton2.TabIndex = 8;
            this.materialButton2.Text = "SpotAPI";
            this.materialButton2.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton2.UseAccentColor = false;
            this.materialButton2.UseVisualStyleBackColor = true;
            // 
            // sliderConcurrent
            // 
            this.sliderConcurrent.Depth = 0;
            this.sliderConcurrent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.sliderConcurrent.Location = new System.Drawing.Point(26, 17);
            this.sliderConcurrent.MouseState = MaterialSkin.MouseState.HOVER;
            this.sliderConcurrent.Name = "sliderConcurrent";
            this.sliderConcurrent.RangeMax = 7;
            this.sliderConcurrent.Size = new System.Drawing.Size(344, 40);
            this.sliderConcurrent.TabIndex = 3;
            this.sliderConcurrent.Text = "Eş zamanlı indirme";
            this.sliderConcurrent.Value = 1;
            this.sliderConcurrent.ValueMax = 7;
            // 
            // chkForceIPv4
            // 
            this.chkForceIPv4.AutoSize = true;
            this.chkForceIPv4.Depth = 0;
            this.chkForceIPv4.Location = new System.Drawing.Point(17, 134);
            this.chkForceIPv4.Margin = new System.Windows.Forms.Padding(0);
            this.chkForceIPv4.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkForceIPv4.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkForceIPv4.Name = "chkForceIPv4";
            this.chkForceIPv4.ReadOnly = false;
            this.chkForceIPv4.Ripple = true;
            this.chkForceIPv4.Size = new System.Drawing.Size(107, 37);
            this.chkForceIPv4.TabIndex = 5;
            this.chkForceIPv4.Text = "IPv4 Zorla";
            this.chkForceIPv4.UseVisualStyleBackColor = true;
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.Depth = 0;
            this.chkOverwrite.Location = new System.Drawing.Point(17, 97);
            this.chkOverwrite.Margin = new System.Windows.Forms.Padding(0);
            this.chkOverwrite.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkOverwrite.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.ReadOnly = false;
            this.chkOverwrite.Ripple = true;
            this.chkOverwrite.Size = new System.Drawing.Size(118, 37);
            this.chkOverwrite.TabIndex = 11;
            this.chkOverwrite.Text = "Üzerine Yaz";
            this.chkOverwrite.UseVisualStyleBackColor = true;
            // 
            // btnClearCache
            // 
            this.btnClearCache.AutoSize = false;
            this.btnClearCache.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClearCache.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnClearCache.Depth = 0;
            this.btnClearCache.HighEmphasis = true;
            this.btnClearCache.Icon = null;
            this.btnClearCache.Location = new System.Drawing.Point(391, 20);
            this.btnClearCache.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnClearCache.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnClearCache.Name = "btnClearCache";
            this.btnClearCache.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnClearCache.Size = new System.Drawing.Size(204, 36);
            this.btnClearCache.TabIndex = 18;
            this.btnClearCache.Text = "Önbelleği Temizle";
            this.btnClearCache.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnClearCache.UseAccentColor = false;
            this.btnClearCache.UseVisualStyleBackColor = true;
            // 
            // chkAlwaysFolder
            // 
            this.chkAlwaysFolder.AutoSize = true;
            this.chkAlwaysFolder.Depth = 0;
            this.chkAlwaysFolder.Location = new System.Drawing.Point(17, 60);
            this.chkAlwaysFolder.Margin = new System.Windows.Forms.Padding(0);
            this.chkAlwaysFolder.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkAlwaysFolder.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkAlwaysFolder.Name = "chkAlwaysFolder";
            this.chkAlwaysFolder.ReadOnly = false;
            this.chkAlwaysFolder.Ripple = true;
            this.chkAlwaysFolder.Size = new System.Drawing.Size(204, 37);
            this.chkAlwaysFolder.TabIndex = 10;
            this.chkAlwaysFolder.Text = "Dosyalar için ayrı klasör";
            this.chkAlwaysFolder.UseVisualStyleBackColor = true;
            // 
            // gnrlCard
            // 
            this.gnrlCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gnrlCard.Controls.Add(this.cmbLang);
            this.gnrlCard.Controls.Add(this.txtPath);
            this.gnrlCard.Controls.Add(this.txtFolderParameter);
            this.gnrlCard.Controls.Add(this.btnSelect);
            this.gnrlCard.Controls.Add(this.txtFileParameter);
            this.gnrlCard.Depth = 0;
            this.gnrlCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.gnrlCard.Location = new System.Drawing.Point(14, 18);
            this.gnrlCard.Margin = new System.Windows.Forms.Padding(14);
            this.gnrlCard.MouseState = MaterialSkin.MouseState.HOVER;
            this.gnrlCard.Name = "gnrlCard";
            this.gnrlCard.Padding = new System.Windows.Forms.Padding(14);
            this.gnrlCard.Size = new System.Drawing.Size(654, 143);
            this.gnrlCard.TabIndex = 18;
            // 
            // cmbLang
            // 
            this.cmbLang.AutoResize = false;
            this.cmbLang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbLang.Depth = 0;
            this.cmbLang.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbLang.DropDownHeight = 174;
            this.cmbLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLang.DropDownWidth = 121;
            this.cmbLang.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbLang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbLang.FormattingEnabled = true;
            this.cmbLang.Hint = "Dil";
            this.cmbLang.IntegralHeight = false;
            this.cmbLang.ItemHeight = 43;
            this.cmbLang.Location = new System.Drawing.Point(439, 22);
            this.cmbLang.MaxDropDownItems = 4;
            this.cmbLang.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbLang.Name = "cmbLang";
            this.cmbLang.Size = new System.Drawing.Size(198, 49);
            this.cmbLang.StartIndex = 0;
            this.cmbLang.TabIndex = 17;
            // 
            // txtPath
            // 
            this.txtPath.AnimateReadOnly = false;
            this.txtPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPath.Depth = 0;
            this.txtPath.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtPath.Hint = "Kaydedilen Yol";
            this.txtPath.LeadingIcon = null;
            this.txtPath.Location = new System.Drawing.Point(17, 79);
            this.txtPath.MaxLength = 50;
            this.txtPath.MouseState = MaterialSkin.MouseState.OUT;
            this.txtPath.Multiline = false;
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(478, 50);
            this.txtPath.TabIndex = 8;
            this.txtPath.Text = "";
            this.txtPath.TrailingIcon = null;
            // 
            // txtFolderParameter
            // 
            this.txtFolderParameter.AnimateReadOnly = false;
            this.txtFolderParameter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtFolderParameter.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtFolderParameter.Depth = 0;
            this.txtFolderParameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtFolderParameter.HideSelection = true;
            this.txtFolderParameter.Hint = "Klasör Parametresi";
            this.txtFolderParameter.LeadingIcon = null;
            this.txtFolderParameter.Location = new System.Drawing.Point(228, 22);
            this.txtFolderParameter.MaxLength = 32767;
            this.txtFolderParameter.MouseState = MaterialSkin.MouseState.OUT;
            this.txtFolderParameter.Name = "txtFolderParameter";
            this.txtFolderParameter.PasswordChar = '\0';
            this.txtFolderParameter.PrefixSuffixText = null;
            this.txtFolderParameter.ReadOnly = false;
            this.txtFolderParameter.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFolderParameter.SelectedText = "";
            this.txtFolderParameter.SelectionLength = 0;
            this.txtFolderParameter.SelectionStart = 0;
            this.txtFolderParameter.ShortcutsEnabled = true;
            this.txtFolderParameter.Size = new System.Drawing.Size(205, 48);
            this.txtFolderParameter.TabIndex = 10;
            this.txtFolderParameter.TabStop = false;
            this.txtFolderParameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtFolderParameter.TrailingIcon = null;
            this.txtFolderParameter.UseSystemPasswordChar = false;
            // 
            // btnSelect
            // 
            this.btnSelect.AutoSize = false;
            this.btnSelect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSelect.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSelect.Depth = 0;
            this.btnSelect.HighEmphasis = true;
            this.btnSelect.Icon = null;
            this.btnSelect.Location = new System.Drawing.Point(502, 79);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSelect.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSelect.Size = new System.Drawing.Size(135, 50);
            this.btnSelect.TabIndex = 6;
            this.btnSelect.Text = "Seç";
            this.btnSelect.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSelect.UseAccentColor = false;
            this.btnSelect.UseVisualStyleBackColor = true;
            // 
            // txtFileParameter
            // 
            this.txtFileParameter.AnimateReadOnly = false;
            this.txtFileParameter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtFileParameter.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtFileParameter.Depth = 0;
            this.txtFileParameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtFileParameter.HideSelection = true;
            this.txtFileParameter.Hint = "Dosya Parametresi";
            this.txtFileParameter.LeadingIcon = null;
            this.txtFileParameter.Location = new System.Drawing.Point(17, 22);
            this.txtFileParameter.MaxLength = 32767;
            this.txtFileParameter.MouseState = MaterialSkin.MouseState.OUT;
            this.txtFileParameter.Name = "txtFileParameter";
            this.txtFileParameter.PasswordChar = '\0';
            this.txtFileParameter.PrefixSuffixText = null;
            this.txtFileParameter.ReadOnly = false;
            this.txtFileParameter.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFileParameter.SelectedText = "";
            this.txtFileParameter.SelectionLength = 0;
            this.txtFileParameter.SelectionStart = 0;
            this.txtFileParameter.ShortcutsEnabled = true;
            this.txtFileParameter.Size = new System.Drawing.Size(205, 48);
            this.txtFileParameter.TabIndex = 9;
            this.txtFileParameter.TabStop = false;
            this.txtFileParameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtFileParameter.TrailingIcon = null;
            this.txtFileParameter.UseSystemPasswordChar = false;
            // 
            // thmCard
            // 
            this.thmCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.thmCard.Controls.Add(this.radioLight);
            this.thmCard.Controls.Add(this.radioDark);
            this.thmCard.Controls.Add(this.radioAuto);
            this.thmCard.Depth = 0;
            this.thmCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.thmCard.Location = new System.Drawing.Point(688, 18);
            this.thmCard.Margin = new System.Windows.Forms.Padding(14);
            this.thmCard.MouseState = MaterialSkin.MouseState.HOVER;
            this.thmCard.Name = "thmCard";
            this.thmCard.Padding = new System.Windows.Forms.Padding(14);
            this.thmCard.Size = new System.Drawing.Size(233, 143);
            this.thmCard.TabIndex = 17;
            // 
            // radioLight
            // 
            this.radioLight.AutoSize = true;
            this.radioLight.Depth = 0;
            this.radioLight.Location = new System.Drawing.Point(14, 18);
            this.radioLight.Margin = new System.Windows.Forms.Padding(0);
            this.radioLight.MouseLocation = new System.Drawing.Point(-1, -1);
            this.radioLight.MouseState = MaterialSkin.MouseState.HOVER;
            this.radioLight.Name = "radioLight";
            this.radioLight.Ripple = true;
            this.radioLight.Size = new System.Drawing.Size(91, 37);
            this.radioLight.TabIndex = 0;
            this.radioLight.TabStop = true;
            this.radioLight.Text = "Aydınlık";
            this.radioLight.UseVisualStyleBackColor = true;
            // 
            // radioDark
            // 
            this.radioDark.AutoSize = true;
            this.radioDark.Depth = 0;
            this.radioDark.Location = new System.Drawing.Point(14, 55);
            this.radioDark.Margin = new System.Windows.Forms.Padding(0);
            this.radioDark.MouseLocation = new System.Drawing.Point(-1, -1);
            this.radioDark.MouseState = MaterialSkin.MouseState.HOVER;
            this.radioDark.Name = "radioDark";
            this.radioDark.Ripple = true;
            this.radioDark.Size = new System.Drawing.Size(93, 37);
            this.radioDark.TabIndex = 1;
            this.radioDark.TabStop = true;
            this.radioDark.Text = "Karanlık";
            this.radioDark.UseVisualStyleBackColor = true;
            // 
            // radioAuto
            // 
            this.radioAuto.AutoSize = true;
            this.radioAuto.Depth = 0;
            this.radioAuto.Location = new System.Drawing.Point(14, 92);
            this.radioAuto.Margin = new System.Windows.Forms.Padding(0);
            this.radioAuto.MouseLocation = new System.Drawing.Point(-1, -1);
            this.radioAuto.MouseState = MaterialSkin.MouseState.HOVER;
            this.radioAuto.Name = "radioAuto";
            this.radioAuto.Ripple = true;
            this.radioAuto.Size = new System.Drawing.Size(110, 37);
            this.radioAuto.TabIndex = 2;
            this.radioAuto.TabStop = true;
            this.radioAuto.Text = "Varsayılan";
            this.radioAuto.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblVersion,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(3, 695);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1274, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblVersion
            // 
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(1174, 17);
            this.lblVersion.Spring = true;
            this.lblVersion.Text = "Version";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(85, 17);
            this.toolStripStatusLabel1.Text = "5.1_0.2.7_alpha";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl);
            this.DrawerIndicatorWidth = 2;
            this.DrawerShowIconsWhenHidden = true;
            this.DrawerTabControl = this.tabControl;
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VSDownloader 5.1 with UI";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl.ResumeLayout(false);
            this.ytPage.ResumeLayout(false);
            this.compCard.ResumeLayout(false);
            this.loadCard.ResumeLayout(false);
            this.listCard.ResumeLayout(false);
            this.listCard.PerformLayout();
            this.searchCard.ResumeLayout(false);
            this.settingsPage.ResumeLayout(false);
            this.settingsPage.PerformLayout();
            this.dwnCard.ResumeLayout(false);
            this.dwnCard.PerformLayout();
            this.gnrlCard.ResumeLayout(false);
            this.thmCard.ResumeLayout(false);
            this.thmCard.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialTabControl tabControl;
        private System.Windows.Forms.TabPage ytPage;
        private System.Windows.Forms.TabPage spotPage;
        private System.Windows.Forms.TabPage settingsPage;
        private System.Windows.Forms.ImageList icon;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private MaterialSkin.Controls.MaterialCard searchCard;
        private MaterialSkin.Controls.MaterialCard listCard;
        private MaterialSkin.Controls.MaterialMaskedTextBox txtUrl;
        private MaterialSkin.Controls.MaterialFloatingActionButton btnLoad;
        private MaterialSkin.Controls.MaterialCard loadCard;
        private MaterialSkin.Controls.MaterialCard compCard;
        private MaterialSkin.Controls.MaterialButton btnYTCancel;
        private MaterialSkin.Controls.MaterialListView listViewLoadYT;
        private MaterialSkin.Controls.MaterialCheckbox chkSelectYTAll;
        private MaterialSkin.Controls.MaterialComboBox cmbYTQuality;
        private MaterialSkin.Controls.MaterialButton btnYTDownload;
        private MaterialSkin.Controls.MaterialComboBox cmbYTFormat;
        private MaterialSkin.Controls.MaterialListView listViewYT;
        private MaterialSkin.Controls.MaterialButton btnClearHistory;
        private MaterialSkin.Controls.MaterialListView listViewCompYT;
        private MaterialSkin.Controls.MaterialLabel lblTheme;
        private MaterialSkin.Controls.MaterialLabel lblGeneral;
        private MaterialSkin.Controls.MaterialButton btnDefaultSettings;
        private MaterialSkin.Controls.MaterialButton btnSave;
        private MaterialSkin.Controls.MaterialLabel lblDownload;
        private MaterialSkin.Controls.MaterialCard dwnCard;
        private MaterialSkin.Controls.MaterialButton materialButton1;
        private MaterialSkin.Controls.MaterialButton materialButton2;
        private MaterialSkin.Controls.MaterialSlider sliderConcurrent;
        private MaterialSkin.Controls.MaterialCheckbox chkForceIPv4;
        private MaterialSkin.Controls.MaterialCheckbox chkOverwrite;
        private MaterialSkin.Controls.MaterialButton btnClearCache;
        private MaterialSkin.Controls.MaterialCheckbox chkAlwaysFolder;
        private MaterialSkin.Controls.MaterialCard gnrlCard;
        private MaterialSkin.Controls.MaterialComboBox cmbLang;
        private MaterialSkin.Controls.MaterialTextBox txtPath;
        private MaterialSkin.Controls.MaterialTextBox2 txtFolderParameter;
        private MaterialSkin.Controls.MaterialButton btnSelect;
        private MaterialSkin.Controls.MaterialTextBox2 txtFileParameter;
        private MaterialSkin.Controls.MaterialCard thmCard;
        private MaterialSkin.Controls.MaterialRadioButton radioLight;
        private MaterialSkin.Controls.MaterialRadioButton radioDark;
        private MaterialSkin.Controls.MaterialRadioButton radioAuto;
        private System.Windows.Forms.ToolStripStatusLabel lblVersion;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

