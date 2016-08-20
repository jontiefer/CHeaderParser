using DotCoolControls;

namespace CHeaderParser
{
    partial class frmCHeaderExtract
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCHeaderExtract));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbStructs = new System.Windows.Forms.ListBox();
            this.pnlStructUnion = new System.Windows.Forms.GroupBox();
            this.rbSUBoth = new System.Windows.Forms.RadioButton();
            this.rbSUUnion = new System.Windows.Forms.RadioButton();
            this.rbSUStruct = new System.Windows.Forms.RadioButton();
            this.MainTab = new System.Windows.Forms.TabControl();
            this.tabFileLoad = new System.Windows.Forms.TabPage();
            this.btnClearFileNames = new DotCoolControls.DotCoolButton();
            this.btnParse = new DotCoolControls.DotCoolButton();
            this.btnHeaderFileSearch = new DotCoolControls.DotCoolButton();
            this.txtHeaderFileNames = new System.Windows.Forms.TextBox();
            this.lblHeaderFileNamesInfoHdr = new System.Windows.Forms.Label();
            this.lblHeaderFileNamesHdr = new System.Windows.Forms.Label();
            this.tabDisplayExport = new System.Windows.Forms.TabPage();
            this.btnQuery = new DotCoolControls.DotCoolButton();
            this.pnlQuerySettings = new System.Windows.Forms.GroupBox();
            this.pnlSortOrder = new System.Windows.Forms.GroupBox();
            this.rbSONone = new System.Windows.Forms.RadioButton();
            this.rbSODescending = new System.Windows.Forms.RadioButton();
            this.rbSOAscending = new System.Windows.Forms.RadioButton();
            this.pnlQryDataSize = new System.Windows.Forms.Panel();
            this.txtQryDataSizeMaxSize = new System.Windows.Forms.TextBox();
            this.txtQryDataSizeMinSize = new System.Windows.Forms.TextBox();
            this.lblQryDataSizeMinSizeHdr = new System.Windows.Forms.Label();
            this.lblQryDataSizeMaxSizeHdr = new System.Windows.Forms.Label();
            this.rbQryDataSize = new System.Windows.Forms.RadioButton();
            this.pnlQryRegex = new System.Windows.Forms.Panel();
            this.txtQryRegEx = new System.Windows.Forms.TextBox();
            this.rbQryRegex = new System.Windows.Forms.RadioButton();
            this.pnlQryWildcard = new System.Windows.Forms.Panel();
            this.chbQryWildcardMatchCase = new System.Windows.Forms.CheckBox();
            this.txtQryWildcard = new System.Windows.Forms.TextBox();
            this.rbQryWildcard = new System.Windows.Forms.RadioButton();
            this.pnlQryName = new System.Windows.Forms.Panel();
            this.chbQryNameMatchCase = new System.Windows.Forms.CheckBox();
            this.chbQryNameExactMatch = new System.Windows.Forms.CheckBox();
            this.chbQryNameMatchAny = new System.Windows.Forms.CheckBox();
            this.txtQryName = new System.Windows.Forms.TextBox();
            this.rbQryName = new System.Windows.Forms.RadioButton();
            this.rbQryDisplayAll = new System.Windows.Forms.RadioButton();
            this.pnlDataExport = new System.Windows.Forms.GroupBox();
            this.rbExportServerData = new System.Windows.Forms.RadioButton();
            this.rbExportLocalData = new System.Windows.Forms.RadioButton();
            this.pnlExportServerFile = new System.Windows.Forms.Panel();
            this.txtExportServerFileName = new System.Windows.Forms.TextBox();
            this.btnExportServerFileNameSearch = new DotCoolControls.DotCoolButton();
            this.lblExportServerFileNameHdr = new System.Windows.Forms.Label();
            this.btnExport = new DotCoolControls.DotCoolButton();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.pnlTesting = new System.Windows.Forms.GroupBox();
            this.chbShowParseTime = new System.Windows.Forms.CheckBox();
            this.pnlDefaultSizeSettings = new System.Windows.Forms.GroupBox();
            this.lblEnumSizeHdr = new System.Windows.Forms.Label();
            this.txtEnumSize = new System.Windows.Forms.TextBox();
            this.lblPointerSizeHdr = new System.Windows.Forms.Label();
            this.txtPointerSize = new System.Windows.Forms.TextBox();
            this.pnlStruct = new System.Windows.Forms.Panel();
            this.lblStructElements = new System.Windows.Forms.Label();
            this.lblStructElementsHdr = new System.Windows.Forms.Label();
            this.lblStructFieldCount = new System.Windows.Forms.Label();
            this.lblStructFieldCountHdr = new System.Windows.Forms.Label();
            this.lblStructSize = new System.Windows.Forms.Label();
            this.lblStructSizeHdr = new System.Windows.Forms.Label();
            this.lblStructName = new System.Windows.Forms.Label();
            this.lblStructNameHdr = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gridFields = new DotCoolControls.DotCoolGridView();
            this.fieldByteOffsetDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fieldTypeNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fieldNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fieldIndexDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataSizeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.elementsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fieldKeyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dsHeaderDataView = new CHeaderParser.Data.CHeaderDataSet();
            this.pnlStructUnion.SuspendLayout();
            this.MainTab.SuspendLayout();
            this.tabFileLoad.SuspendLayout();
            this.tabDisplayExport.SuspendLayout();
            this.pnlQuerySettings.SuspendLayout();
            this.pnlSortOrder.SuspendLayout();
            this.pnlQryDataSize.SuspendLayout();
            this.pnlQryRegex.SuspendLayout();
            this.pnlQryWildcard.SuspendLayout();
            this.pnlQryName.SuspendLayout();
            this.pnlDataExport.SuspendLayout();
            this.pnlExportServerFile.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.pnlTesting.SuspendLayout();
            this.pnlDefaultSizeSettings.SuspendLayout();
            this.pnlStruct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsHeaderDataView)).BeginInit();
            this.SuspendLayout();
            // 
            // lbStructs
            // 
            this.lbStructs.BackColor = System.Drawing.Color.Lavender;
            this.lbStructs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbStructs.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStructs.FormattingEnabled = true;
            this.lbStructs.ItemHeight = 16;
            this.lbStructs.Location = new System.Drawing.Point(0, 0);
            this.lbStructs.Name = "lbStructs";
            this.lbStructs.Size = new System.Drawing.Size(275, 464);
            this.lbStructs.TabIndex = 1;
            this.lbStructs.SelectedIndexChanged += new System.EventHandler(this.lbStructs_SelectedIndexChanged);
            // 
            // pnlStructUnion
            // 
            this.pnlStructUnion.Controls.Add(this.rbSUBoth);
            this.pnlStructUnion.Controls.Add(this.rbSUUnion);
            this.pnlStructUnion.Controls.Add(this.rbSUStruct);
            this.pnlStructUnion.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlStructUnion.Location = new System.Drawing.Point(639, 117);
            this.pnlStructUnion.Name = "pnlStructUnion";
            this.pnlStructUnion.Size = new System.Drawing.Size(294, 42);
            this.pnlStructUnion.TabIndex = 4;
            this.pnlStructUnion.TabStop = false;
            this.pnlStructUnion.Text = "Structure/Union";
            // 
            // rbSUBoth
            // 
            this.rbSUBoth.AutoSize = true;
            this.rbSUBoth.Checked = true;
            this.rbSUBoth.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSUBoth.Location = new System.Drawing.Point(212, 15);
            this.rbSUBoth.Name = "rbSUBoth";
            this.rbSUBoth.Size = new System.Drawing.Size(56, 21);
            this.rbSUBoth.TabIndex = 2;
            this.rbSUBoth.TabStop = true;
            this.rbSUBoth.Text = "Both";
            this.rbSUBoth.UseVisualStyleBackColor = true;
            // 
            // rbSUUnion
            // 
            this.rbSUUnion.AutoSize = true;
            this.rbSUUnion.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSUUnion.Location = new System.Drawing.Point(132, 15);
            this.rbSUUnion.Name = "rbSUUnion";
            this.rbSUUnion.Size = new System.Drawing.Size(63, 21);
            this.rbSUUnion.TabIndex = 1;
            this.rbSUUnion.Text = "Union";
            this.rbSUUnion.UseVisualStyleBackColor = true;
            // 
            // rbSUStruct
            // 
            this.rbSUStruct.AutoSize = true;
            this.rbSUStruct.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSUStruct.Location = new System.Drawing.Point(29, 15);
            this.rbSUStruct.Name = "rbSUStruct";
            this.rbSUStruct.Size = new System.Drawing.Size(86, 21);
            this.rbSUStruct.TabIndex = 0;
            this.rbSUStruct.Text = "Structure";
            this.rbSUStruct.UseVisualStyleBackColor = true;
            // 
            // MainTab
            // 
            this.MainTab.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTab.Controls.Add(this.tabFileLoad);
            this.MainTab.Controls.Add(this.tabDisplayExport);
            this.MainTab.Controls.Add(this.tabSettings);
            this.MainTab.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainTab.Location = new System.Drawing.Point(6, 477);
            this.MainTab.Name = "MainTab";
            this.MainTab.SelectedIndex = 0;
            this.MainTab.Size = new System.Drawing.Size(1275, 273);
            this.MainTab.TabIndex = 6;
            // 
            // tabFileLoad
            // 
            this.tabFileLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.tabFileLoad.Controls.Add(this.btnClearFileNames);
            this.tabFileLoad.Controls.Add(this.btnParse);
            this.tabFileLoad.Controls.Add(this.btnHeaderFileSearch);
            this.tabFileLoad.Controls.Add(this.txtHeaderFileNames);
            this.tabFileLoad.Controls.Add(this.lblHeaderFileNamesInfoHdr);
            this.tabFileLoad.Controls.Add(this.lblHeaderFileNamesHdr);
            this.tabFileLoad.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabFileLoad.Location = new System.Drawing.Point(4, 25);
            this.tabFileLoad.Name = "tabFileLoad";
            this.tabFileLoad.Padding = new System.Windows.Forms.Padding(3);
            this.tabFileLoad.Size = new System.Drawing.Size(1267, 244);
            this.tabFileLoad.TabIndex = 0;
            this.tabFileLoad.Text = "Header File Loading/Parsing";
            // 
            // btnClearFileNames
            // 
            this.btnClearFileNames.BackGradientColor1 = System.Drawing.Color.PowderBlue;
            this.btnClearFileNames.BackGradientColor2 = System.Drawing.Color.MidnightBlue;
            this.btnClearFileNames.BackGradientColorMouseDown1 = System.Drawing.Color.Purple;
            this.btnClearFileNames.BackGradientColorMouseDown2 = System.Drawing.Color.Thistle;
            this.btnClearFileNames.BackGradientColorMouseOver1 = System.Drawing.Color.LightCyan;
            this.btnClearFileNames.BackGradientColorMouseOver2 = System.Drawing.Color.Blue;
            this.btnClearFileNames.BackGradientType = DotCoolControls.Tools.GradientType.BackwardDiagonal;
            this.btnClearFileNames.BackGradientTypeMouseDown = DotCoolControls.Tools.GradientType.BackwardDiagonal;
            this.btnClearFileNames.BackGradientTypeMouseOver = DotCoolControls.Tools.GradientType.BackwardDiagonal;
            this.btnClearFileNames.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearFileNames.BackgroundImage")));
            this.btnClearFileNames.DrawBackgroundGradient = true;
            this.btnClearFileNames.EnableTextMouseDown = true;
            this.btnClearFileNames.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearFileNames.FontMouseDown = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearFileNames.FontMouseOver = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearFileNames.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClearFileNames.ForeColorMouseDown = System.Drawing.SystemColors.ControlText;
            this.btnClearFileNames.ForeColorMouseOver = System.Drawing.SystemColors.ControlText;
            this.btnClearFileNames.Location = new System.Drawing.Point(6, 156);
            this.btnClearFileNames.Name = "btnClearFileNames";
            this.btnClearFileNames.Size = new System.Drawing.Size(132, 39);
            this.btnClearFileNames.TabIndex = 11;
            this.btnClearFileNames.Text = "Clear";
            this.btnClearFileNames.UseVisualStyleBackColor = true;
            this.btnClearFileNames.Click += new System.EventHandler(this.btnClearFileNames_Click);
            // 
            // btnParse
            // 
            this.btnParse.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnParse.BackGradientColor1 = System.Drawing.Color.PowderBlue;
            this.btnParse.BackGradientColor2 = System.Drawing.Color.MidnightBlue;
            this.btnParse.BackGradientColorMouseDown1 = System.Drawing.Color.MistyRose;
            this.btnParse.BackGradientColorMouseDown2 = System.Drawing.Color.Red;
            this.btnParse.BackGradientColorMouseOver1 = System.Drawing.Color.LightCyan;
            this.btnParse.BackGradientColorMouseOver2 = System.Drawing.Color.RoyalBlue;
            this.btnParse.BackGradientType = DotCoolControls.Tools.GradientType.Ellipsis;
            this.btnParse.BackGradientTypeMouseDown = DotCoolControls.Tools.GradientType.Triangular;
            this.btnParse.BackGradientTypeMouseOver = DotCoolControls.Tools.GradientType.Ellipsis;
            this.btnParse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnParse.BackgroundImage")));
            this.btnParse.DrawBackgroundGradient = true;
            this.btnParse.EnableTextMouseDown = true;
            this.btnParse.EnableTextMouseOver = true;
            this.btnParse.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnParse.FontMouseDown = new System.Drawing.Font("Arial", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnParse.FontMouseOver = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnParse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnParse.ForeColorMouseDown = System.Drawing.Color.Yellow;
            this.btnParse.ForeColorMouseOver = System.Drawing.Color.Maroon;
            this.btnParse.Image = ((System.Drawing.Image)(resources.GetObject("btnParse.Image")));
            this.btnParse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnParse.Location = new System.Drawing.Point(720, 51);
            this.btnParse.Name = "btnParse";
            this.btnParse.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnParse.Size = new System.Drawing.Size(486, 105);
            this.btnParse.TabIndex = 10;
            this.btnParse.Text = "Parse Header Files";
            this.btnParse.UseVisualStyleBackColor = false;
            this.btnParse.Click += new System.EventHandler(this.btnParse_Click);
            // 
            // btnHeaderFileSearch
            // 
            this.btnHeaderFileSearch.BackGradientColor1 = System.Drawing.Color.PowderBlue;
            this.btnHeaderFileSearch.BackGradientColor2 = System.Drawing.Color.MidnightBlue;
            this.btnHeaderFileSearch.BackGradientColorMouseDown1 = System.Drawing.Color.Purple;
            this.btnHeaderFileSearch.BackGradientColorMouseDown2 = System.Drawing.Color.Thistle;
            this.btnHeaderFileSearch.BackGradientColorMouseOver1 = System.Drawing.Color.LightCyan;
            this.btnHeaderFileSearch.BackGradientColorMouseOver2 = System.Drawing.Color.Blue;
            this.btnHeaderFileSearch.BackGradientType = DotCoolControls.Tools.GradientType.BackwardDiagonal;
            this.btnHeaderFileSearch.BackGradientTypeMouseDown = DotCoolControls.Tools.GradientType.BackwardDiagonal;
            this.btnHeaderFileSearch.BackGradientTypeMouseOver = DotCoolControls.Tools.GradientType.BackwardDiagonal;
            this.btnHeaderFileSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnHeaderFileSearch.BackgroundImage")));
            this.btnHeaderFileSearch.DrawBackgroundGradient = true;
            this.btnHeaderFileSearch.EnableTextMouseDown = true;
            this.btnHeaderFileSearch.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHeaderFileSearch.FontMouseDown = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHeaderFileSearch.FontMouseOver = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHeaderFileSearch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnHeaderFileSearch.ForeColorMouseDown = System.Drawing.SystemColors.ControlText;
            this.btnHeaderFileSearch.ForeColorMouseOver = System.Drawing.SystemColors.ControlText;
            this.btnHeaderFileSearch.Location = new System.Drawing.Point(6, 84);
            this.btnHeaderFileSearch.Name = "btnHeaderFileSearch";
            this.btnHeaderFileSearch.Size = new System.Drawing.Size(132, 69);
            this.btnHeaderFileSearch.TabIndex = 8;
            this.btnHeaderFileSearch.Text = "&Search";
            this.btnHeaderFileSearch.UseVisualStyleBackColor = true;
            this.btnHeaderFileSearch.Click += new System.EventHandler(this.btnHeaderFileSearch_Click);
            // 
            // txtHeaderFileNames
            // 
            this.txtHeaderFileNames.BackColor = System.Drawing.Color.AliceBlue;
            this.txtHeaderFileNames.Location = new System.Drawing.Point(144, 9);
            this.txtHeaderFileNames.Multiline = true;
            this.txtHeaderFileNames.Name = "txtHeaderFileNames";
            this.txtHeaderFileNames.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHeaderFileNames.Size = new System.Drawing.Size(525, 186);
            this.txtHeaderFileNames.TabIndex = 6;
            // 
            // lblHeaderFileNamesInfoHdr
            // 
            this.lblHeaderFileNamesInfoHdr.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderFileNamesInfoHdr.Location = new System.Drawing.Point(9, 33);
            this.lblHeaderFileNamesInfoHdr.Name = "lblHeaderFileNamesInfoHdr";
            this.lblHeaderFileNamesInfoHdr.Size = new System.Drawing.Size(135, 54);
            this.lblHeaderFileNamesInfoHdr.TabIndex = 7;
            this.lblHeaderFileNamesInfoHdr.Text = "Comma, Semi-Colon, Line Separated.  Place in order of dependancy.";
            // 
            // lblHeaderFileNamesHdr
            // 
            this.lblHeaderFileNamesHdr.AutoSize = true;
            this.lblHeaderFileNamesHdr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderFileNamesHdr.Location = new System.Drawing.Point(9, 15);
            this.lblHeaderFileNamesHdr.Name = "lblHeaderFileNamesHdr";
            this.lblHeaderFileNamesHdr.Size = new System.Drawing.Size(129, 16);
            this.lblHeaderFileNamesHdr.TabIndex = 5;
            this.lblHeaderFileNamesHdr.Text = "Header File Names";
            // 
            // tabDisplayExport
            // 
            this.tabDisplayExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.tabDisplayExport.Controls.Add(this.btnQuery);
            this.tabDisplayExport.Controls.Add(this.pnlQuerySettings);
            this.tabDisplayExport.Controls.Add(this.pnlDataExport);
            this.tabDisplayExport.Location = new System.Drawing.Point(4, 25);
            this.tabDisplayExport.Name = "tabDisplayExport";
            this.tabDisplayExport.Padding = new System.Windows.Forms.Padding(3);
            this.tabDisplayExport.Size = new System.Drawing.Size(1267, 244);
            this.tabDisplayExport.TabIndex = 1;
            this.tabDisplayExport.Text = "Display/Export";
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.BackGradientColor1 = System.Drawing.Color.Purple;
            this.btnQuery.BackGradientColor2 = System.Drawing.Color.Thistle;
            this.btnQuery.BackGradientColorMouseDown1 = System.Drawing.Color.DarkBlue;
            this.btnQuery.BackGradientColorMouseDown2 = System.Drawing.Color.PaleVioletRed;
            this.btnQuery.BackGradientColorMouseOver1 = System.Drawing.Color.DeepPink;
            this.btnQuery.BackGradientColorMouseOver2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnQuery.BackGradientType = DotCoolControls.Tools.GradientType.Triangular;
            this.btnQuery.BackGradientTypeMouseDown = DotCoolControls.Tools.GradientType.TenPointPoly;
            this.btnQuery.BackGradientTypeMouseOver = DotCoolControls.Tools.GradientType.Triangular;
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.DrawBackgroundGradient = true;
            this.btnQuery.EnableTextMouseDown = true;
            this.btnQuery.EnableTextMouseOver = true;
            this.btnQuery.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuery.FontMouseDown = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuery.FontMouseOver = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuery.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQuery.ForeColorMouseDown = System.Drawing.Color.Cyan;
            this.btnQuery.ForeColorMouseOver = System.Drawing.Color.Lime;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuery.Location = new System.Drawing.Point(987, 6);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnQuery.Size = new System.Drawing.Size(242, 63);
            this.btnQuery.TabIndex = 9;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // pnlQuerySettings
            // 
            this.pnlQuerySettings.Controls.Add(this.pnlSortOrder);
            this.pnlQuerySettings.Controls.Add(this.pnlQryDataSize);
            this.pnlQuerySettings.Controls.Add(this.rbQryDataSize);
            this.pnlQuerySettings.Controls.Add(this.pnlStructUnion);
            this.pnlQuerySettings.Controls.Add(this.pnlQryRegex);
            this.pnlQuerySettings.Controls.Add(this.rbQryRegex);
            this.pnlQuerySettings.Controls.Add(this.pnlQryWildcard);
            this.pnlQuerySettings.Controls.Add(this.rbQryWildcard);
            this.pnlQuerySettings.Controls.Add(this.pnlQryName);
            this.pnlQuerySettings.Controls.Add(this.rbQryName);
            this.pnlQuerySettings.Controls.Add(this.rbQryDisplayAll);
            this.pnlQuerySettings.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlQuerySettings.Location = new System.Drawing.Point(6, 0);
            this.pnlQuerySettings.Name = "pnlQuerySettings";
            this.pnlQuerySettings.Size = new System.Drawing.Size(939, 246);
            this.pnlQuerySettings.TabIndex = 7;
            this.pnlQuerySettings.TabStop = false;
            this.pnlQuerySettings.Text = "Structure/Union Query Filters";
            // 
            // pnlSortOrder
            // 
            this.pnlSortOrder.Controls.Add(this.rbSONone);
            this.pnlSortOrder.Controls.Add(this.rbSODescending);
            this.pnlSortOrder.Controls.Add(this.rbSOAscending);
            this.pnlSortOrder.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSortOrder.Location = new System.Drawing.Point(639, 165);
            this.pnlSortOrder.Name = "pnlSortOrder";
            this.pnlSortOrder.Size = new System.Drawing.Size(294, 42);
            this.pnlSortOrder.TabIndex = 14;
            this.pnlSortOrder.TabStop = false;
            this.pnlSortOrder.Text = "Sort Order";
            // 
            // rbSONone
            // 
            this.rbSONone.AutoSize = true;
            this.rbSONone.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSONone.Location = new System.Drawing.Point(222, 15);
            this.rbSONone.Name = "rbSONone";
            this.rbSONone.Size = new System.Drawing.Size(60, 21);
            this.rbSONone.TabIndex = 2;
            this.rbSONone.Text = "None";
            this.rbSONone.UseVisualStyleBackColor = true;
            // 
            // rbSODescending
            // 
            this.rbSODescending.AutoSize = true;
            this.rbSODescending.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSODescending.Location = new System.Drawing.Point(114, 15);
            this.rbSODescending.Name = "rbSODescending";
            this.rbSODescending.Size = new System.Drawing.Size(104, 21);
            this.rbSODescending.TabIndex = 1;
            this.rbSODescending.Text = "Descending";
            this.rbSODescending.UseVisualStyleBackColor = true;
            // 
            // rbSOAscending
            // 
            this.rbSOAscending.AutoSize = true;
            this.rbSOAscending.Checked = true;
            this.rbSOAscending.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSOAscending.Location = new System.Drawing.Point(5, 15);
            this.rbSOAscending.Name = "rbSOAscending";
            this.rbSOAscending.Size = new System.Drawing.Size(94, 21);
            this.rbSOAscending.TabIndex = 0;
            this.rbSOAscending.TabStop = true;
            this.rbSOAscending.Text = "Ascending";
            this.rbSOAscending.UseVisualStyleBackColor = true;
            // 
            // pnlQryDataSize
            // 
            this.pnlQryDataSize.Controls.Add(this.txtQryDataSizeMaxSize);
            this.pnlQryDataSize.Controls.Add(this.txtQryDataSizeMinSize);
            this.pnlQryDataSize.Controls.Add(this.lblQryDataSizeMinSizeHdr);
            this.pnlQryDataSize.Controls.Add(this.lblQryDataSizeMaxSizeHdr);
            this.pnlQryDataSize.Enabled = false;
            this.pnlQryDataSize.Location = new System.Drawing.Point(639, 57);
            this.pnlQryDataSize.Name = "pnlQryDataSize";
            this.pnlQryDataSize.Size = new System.Drawing.Size(294, 51);
            this.pnlQryDataSize.TabIndex = 13;
            // 
            // txtQryDataSizeMaxSize
            // 
            this.txtQryDataSizeMaxSize.AcceptsReturn = true;
            this.txtQryDataSizeMaxSize.BackColor = System.Drawing.Color.AliceBlue;
            this.txtQryDataSizeMaxSize.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQryDataSizeMaxSize.Location = new System.Drawing.Point(153, 21);
            this.txtQryDataSizeMaxSize.Name = "txtQryDataSizeMaxSize";
            this.txtQryDataSizeMaxSize.Size = new System.Drawing.Size(136, 22);
            this.txtQryDataSizeMaxSize.TabIndex = 12;
            this.txtQryDataSizeMaxSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQry_KeyPress);
            // 
            // txtQryDataSizeMinSize
            // 
            this.txtQryDataSizeMinSize.AcceptsReturn = true;
            this.txtQryDataSizeMinSize.BackColor = System.Drawing.Color.AliceBlue;
            this.txtQryDataSizeMinSize.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQryDataSizeMinSize.Location = new System.Drawing.Point(6, 21);
            this.txtQryDataSizeMinSize.Name = "txtQryDataSizeMinSize";
            this.txtQryDataSizeMinSize.Size = new System.Drawing.Size(136, 22);
            this.txtQryDataSizeMinSize.TabIndex = 11;
            this.txtQryDataSizeMinSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQry_KeyPress);
            // 
            // lblQryDataSizeMinSizeHdr
            // 
            this.lblQryDataSizeMinSizeHdr.AutoSize = true;
            this.lblQryDataSizeMinSizeHdr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQryDataSizeMinSizeHdr.Location = new System.Drawing.Point(3, 6);
            this.lblQryDataSizeMinSizeHdr.Name = "lblQryDataSizeMinSizeHdr";
            this.lblQryDataSizeMinSizeHdr.Size = new System.Drawing.Size(99, 16);
            this.lblQryDataSizeMinSizeHdr.TabIndex = 6;
            this.lblQryDataSizeMinSizeHdr.Text = "Minimum Size";
            // 
            // lblQryDataSizeMaxSizeHdr
            // 
            this.lblQryDataSizeMaxSizeHdr.AutoSize = true;
            this.lblQryDataSizeMaxSizeHdr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQryDataSizeMaxSizeHdr.Location = new System.Drawing.Point(150, 6);
            this.lblQryDataSizeMaxSizeHdr.Name = "lblQryDataSizeMaxSizeHdr";
            this.lblQryDataSizeMaxSizeHdr.Size = new System.Drawing.Size(103, 16);
            this.lblQryDataSizeMaxSizeHdr.TabIndex = 10;
            this.lblQryDataSizeMaxSizeHdr.Text = "Maximum Size";
            // 
            // rbQryDataSize
            // 
            this.rbQryDataSize.AutoSize = true;
            this.rbQryDataSize.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbQryDataSize.Location = new System.Drawing.Point(624, 36);
            this.rbQryDataSize.Name = "rbQryDataSize";
            this.rbQryDataSize.Size = new System.Drawing.Size(227, 21);
            this.rbQryDataSize.TabIndex = 9;
            this.rbQryDataSize.Text = "Query Structures By Data Size";
            this.rbQryDataSize.UseVisualStyleBackColor = true;
            this.rbQryDataSize.CheckedChanged += new System.EventHandler(this.rbQry_CheckedChanged);
            // 
            // pnlQryRegex
            // 
            this.pnlQryRegex.Controls.Add(this.txtQryRegEx);
            this.pnlQryRegex.Enabled = false;
            this.pnlQryRegex.Location = new System.Drawing.Point(24, 195);
            this.pnlQryRegex.Name = "pnlQryRegex";
            this.pnlQryRegex.Size = new System.Drawing.Size(585, 48);
            this.pnlQryRegex.TabIndex = 8;
            // 
            // txtQryRegEx
            // 
            this.txtQryRegEx.AcceptsReturn = true;
            this.txtQryRegEx.BackColor = System.Drawing.Color.AliceBlue;
            this.txtQryRegEx.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQryRegEx.Location = new System.Drawing.Point(9, 3);
            this.txtQryRegEx.Name = "txtQryRegEx";
            this.txtQryRegEx.Size = new System.Drawing.Size(567, 22);
            this.txtQryRegEx.TabIndex = 3;
            this.txtQryRegEx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQry_KeyPress);
            // 
            // rbQryRegex
            // 
            this.rbQryRegex.AutoSize = true;
            this.rbQryRegex.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbQryRegex.Location = new System.Drawing.Point(9, 174);
            this.rbQryRegex.Name = "rbQryRegex";
            this.rbQryRegex.Size = new System.Drawing.Size(311, 21);
            this.rbQryRegex.TabIndex = 7;
            this.rbQryRegex.Text = "Query Structures Using Regular Expression";
            this.rbQryRegex.UseVisualStyleBackColor = true;
            this.rbQryRegex.CheckedChanged += new System.EventHandler(this.rbQry_CheckedChanged);
            // 
            // pnlQryWildcard
            // 
            this.pnlQryWildcard.Controls.Add(this.chbQryWildcardMatchCase);
            this.pnlQryWildcard.Controls.Add(this.txtQryWildcard);
            this.pnlQryWildcard.Enabled = false;
            this.pnlQryWildcard.Location = new System.Drawing.Point(24, 126);
            this.pnlQryWildcard.Name = "pnlQryWildcard";
            this.pnlQryWildcard.Size = new System.Drawing.Size(585, 48);
            this.pnlQryWildcard.TabIndex = 6;
            // 
            // chbQryWildcardMatchCase
            // 
            this.chbQryWildcardMatchCase.AutoSize = true;
            this.chbQryWildcardMatchCase.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbQryWildcardMatchCase.Location = new System.Drawing.Point(18, 27);
            this.chbQryWildcardMatchCase.Name = "chbQryWildcardMatchCase";
            this.chbQryWildcardMatchCase.Size = new System.Drawing.Size(113, 20);
            this.chbQryWildcardMatchCase.TabIndex = 6;
            this.chbQryWildcardMatchCase.Text = "Case-Sensitive";
            this.chbQryWildcardMatchCase.UseVisualStyleBackColor = true;
            // 
            // txtQryWildcard
            // 
            this.txtQryWildcard.AcceptsReturn = true;
            this.txtQryWildcard.BackColor = System.Drawing.Color.AliceBlue;
            this.txtQryWildcard.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQryWildcard.Location = new System.Drawing.Point(9, 3);
            this.txtQryWildcard.Name = "txtQryWildcard";
            this.txtQryWildcard.Size = new System.Drawing.Size(567, 22);
            this.txtQryWildcard.TabIndex = 3;
            this.txtQryWildcard.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQry_KeyPress);
            // 
            // rbQryWildcard
            // 
            this.rbQryWildcard.AutoSize = true;
            this.rbQryWildcard.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbQryWildcard.Location = new System.Drawing.Point(9, 105);
            this.rbQryWildcard.Name = "rbQryWildcard";
            this.rbQryWildcard.Size = new System.Drawing.Size(325, 21);
            this.rbQryWildcard.TabIndex = 5;
            this.rbQryWildcard.Text = "Query Structures By Name (Wildcard Search)";
            this.rbQryWildcard.UseVisualStyleBackColor = true;
            this.rbQryWildcard.CheckedChanged += new System.EventHandler(this.rbQry_CheckedChanged);
            // 
            // pnlQryName
            // 
            this.pnlQryName.Controls.Add(this.chbQryNameMatchCase);
            this.pnlQryName.Controls.Add(this.chbQryNameExactMatch);
            this.pnlQryName.Controls.Add(this.chbQryNameMatchAny);
            this.pnlQryName.Controls.Add(this.txtQryName);
            this.pnlQryName.Enabled = false;
            this.pnlQryName.Location = new System.Drawing.Point(24, 57);
            this.pnlQryName.Name = "pnlQryName";
            this.pnlQryName.Size = new System.Drawing.Size(585, 48);
            this.pnlQryName.TabIndex = 4;
            // 
            // chbQryNameMatchCase
            // 
            this.chbQryNameMatchCase.AutoSize = true;
            this.chbQryNameMatchCase.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbQryNameMatchCase.Location = new System.Drawing.Point(411, 27);
            this.chbQryNameMatchCase.Name = "chbQryNameMatchCase";
            this.chbQryNameMatchCase.Size = new System.Drawing.Size(113, 20);
            this.chbQryNameMatchCase.TabIndex = 6;
            this.chbQryNameMatchCase.Text = "Case-Sensitive";
            this.chbQryNameMatchCase.UseVisualStyleBackColor = true;
            // 
            // chbQryNameExactMatch
            // 
            this.chbQryNameExactMatch.AutoSize = true;
            this.chbQryNameExactMatch.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbQryNameExactMatch.Location = new System.Drawing.Point(285, 27);
            this.chbQryNameExactMatch.Name = "chbQryNameExactMatch";
            this.chbQryNameExactMatch.Size = new System.Drawing.Size(101, 20);
            this.chbQryNameExactMatch.TabIndex = 5;
            this.chbQryNameExactMatch.Text = "Exact Match";
            this.chbQryNameExactMatch.UseVisualStyleBackColor = true;
            // 
            // chbQryNameMatchAny
            // 
            this.chbQryNameMatchAny.AutoSize = true;
            this.chbQryNameMatchAny.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbQryNameMatchAny.Location = new System.Drawing.Point(60, 27);
            this.chbQryNameMatchAny.Name = "chbQryNameMatchAny";
            this.chbQryNameMatchAny.Size = new System.Drawing.Size(200, 20);
            this.chbQryNameMatchAny.TabIndex = 4;
            this.chbQryNameMatchAny.Text = "Match Any Part of Expression";
            this.chbQryNameMatchAny.UseVisualStyleBackColor = true;
            // 
            // txtQryName
            // 
            this.txtQryName.AcceptsReturn = true;
            this.txtQryName.BackColor = System.Drawing.Color.AliceBlue;
            this.txtQryName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQryName.Location = new System.Drawing.Point(9, 3);
            this.txtQryName.Name = "txtQryName";
            this.txtQryName.Size = new System.Drawing.Size(567, 22);
            this.txtQryName.TabIndex = 3;
            this.txtQryName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQry_KeyPress);
            // 
            // rbQryName
            // 
            this.rbQryName.AutoSize = true;
            this.rbQryName.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbQryName.Location = new System.Drawing.Point(9, 36);
            this.rbQryName.Name = "rbQryName";
            this.rbQryName.Size = new System.Drawing.Size(190, 21);
            this.rbQryName.TabIndex = 1;
            this.rbQryName.Text = "Find Structures By Name";
            this.rbQryName.UseVisualStyleBackColor = true;
            this.rbQryName.CheckedChanged += new System.EventHandler(this.rbQry_CheckedChanged);
            // 
            // rbQryDisplayAll
            // 
            this.rbQryDisplayAll.AutoSize = true;
            this.rbQryDisplayAll.Checked = true;
            this.rbQryDisplayAll.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbQryDisplayAll.Location = new System.Drawing.Point(9, 15);
            this.rbQryDisplayAll.Name = "rbQryDisplayAll";
            this.rbQryDisplayAll.Size = new System.Drawing.Size(92, 21);
            this.rbQryDisplayAll.TabIndex = 0;
            this.rbQryDisplayAll.TabStop = true;
            this.rbQryDisplayAll.Text = "Display All";
            this.rbQryDisplayAll.UseVisualStyleBackColor = true;
            this.rbQryDisplayAll.CheckedChanged += new System.EventHandler(this.rbQry_CheckedChanged);
            // 
            // pnlDataExport
            // 
            this.pnlDataExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDataExport.Controls.Add(this.rbExportServerData);
            this.pnlDataExport.Controls.Add(this.rbExportLocalData);
            this.pnlDataExport.Controls.Add(this.pnlExportServerFile);
            this.pnlDataExport.Controls.Add(this.btnExport);
            this.pnlDataExport.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlDataExport.Location = new System.Drawing.Point(951, 75);
            this.pnlDataExport.Name = "pnlDataExport";
            this.pnlDataExport.Size = new System.Drawing.Size(309, 165);
            this.pnlDataExport.TabIndex = 15;
            this.pnlDataExport.TabStop = false;
            this.pnlDataExport.Text = "Data Export";
            // 
            // rbExportServerData
            // 
            this.rbExportServerData.AutoSize = true;
            this.rbExportServerData.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbExportServerData.Location = new System.Drawing.Point(9, 48);
            this.rbExportServerData.Name = "rbExportServerData";
            this.rbExportServerData.Size = new System.Drawing.Size(185, 21);
            this.rbExportServerData.TabIndex = 1;
            this.rbExportServerData.Text = "Export Web Server Data";
            this.rbExportServerData.UseVisualStyleBackColor = true;
            this.rbExportServerData.CheckedChanged += new System.EventHandler(this.rbExport_CheckedChanged);
            // 
            // rbExportLocalData
            // 
            this.rbExportLocalData.AutoSize = true;
            this.rbExportLocalData.Checked = true;
            this.rbExportLocalData.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbExportLocalData.Location = new System.Drawing.Point(9, 21);
            this.rbExportLocalData.Name = "rbExportLocalData";
            this.rbExportLocalData.Size = new System.Drawing.Size(215, 21);
            this.rbExportLocalData.TabIndex = 0;
            this.rbExportLocalData.TabStop = true;
            this.rbExportLocalData.Text = "Export Local Web Page Data";
            this.rbExportLocalData.UseVisualStyleBackColor = true;
            this.rbExportLocalData.CheckedChanged += new System.EventHandler(this.rbExport_CheckedChanged);
            // 
            // pnlExportServerFile
            // 
            this.pnlExportServerFile.Controls.Add(this.txtExportServerFileName);
            this.pnlExportServerFile.Controls.Add(this.btnExportServerFileNameSearch);
            this.pnlExportServerFile.Controls.Add(this.lblExportServerFileNameHdr);
            this.pnlExportServerFile.Enabled = false;
            this.pnlExportServerFile.Location = new System.Drawing.Point(9, 69);
            this.pnlExportServerFile.Name = "pnlExportServerFile";
            this.pnlExportServerFile.Size = new System.Drawing.Size(303, 45);
            this.pnlExportServerFile.TabIndex = 15;
            // 
            // txtExportServerFileName
            // 
            this.txtExportServerFileName.AcceptsReturn = true;
            this.txtExportServerFileName.BackColor = System.Drawing.Color.AliceBlue;
            this.txtExportServerFileName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExportServerFileName.Location = new System.Drawing.Point(3, 18);
            this.txtExportServerFileName.Name = "txtExportServerFileName";
            this.txtExportServerFileName.Size = new System.Drawing.Size(240, 22);
            this.txtExportServerFileName.TabIndex = 12;
            // 
            // btnExportServerFileNameSearch
            // 
            this.btnExportServerFileNameSearch.BackGradientColor1 = System.Drawing.Color.LavenderBlush;
            this.btnExportServerFileNameSearch.BackGradientColor2 = System.Drawing.Color.Indigo;
            this.btnExportServerFileNameSearch.BackGradientColorMouseDown1 = System.Drawing.Color.DarkBlue;
            this.btnExportServerFileNameSearch.BackGradientColorMouseDown2 = System.Drawing.Color.Lavender;
            this.btnExportServerFileNameSearch.BackGradientColorMouseOver1 = System.Drawing.SystemColors.Control;
            this.btnExportServerFileNameSearch.BackGradientColorMouseOver2 = System.Drawing.SystemColors.Control;
            this.btnExportServerFileNameSearch.BackGradientType = DotCoolControls.Tools.GradientType.Ellipsis;
            this.btnExportServerFileNameSearch.BackGradientTypeMouseDown = DotCoolControls.Tools.GradientType.Ellipsis;
            this.btnExportServerFileNameSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExportServerFileNameSearch.BackgroundImage")));
            this.btnExportServerFileNameSearch.DrawBackgroundGradient = true;
            this.btnExportServerFileNameSearch.EnableTextMouseDown = true;
            this.btnExportServerFileNameSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportServerFileNameSearch.FontMouseDown = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportServerFileNameSearch.FontMouseOver = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportServerFileNameSearch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExportServerFileNameSearch.ForeColorMouseDown = System.Drawing.Color.WhiteSmoke;
            this.btnExportServerFileNameSearch.ForeColorMouseOver = System.Drawing.SystemColors.ControlText;
            this.btnExportServerFileNameSearch.Location = new System.Drawing.Point(243, 18);
            this.btnExportServerFileNameSearch.Name = "btnExportServerFileNameSearch";
            this.btnExportServerFileNameSearch.Size = new System.Drawing.Size(57, 24);
            this.btnExportServerFileNameSearch.TabIndex = 14;
            this.btnExportServerFileNameSearch.Text = "&Search";
            this.btnExportServerFileNameSearch.UseVisualStyleBackColor = true;
            this.btnExportServerFileNameSearch.Click += new System.EventHandler(this.btnExportServerFileNameSearch_Click);
            // 
            // lblExportServerFileNameHdr
            // 
            this.lblExportServerFileNameHdr.AutoSize = true;
            this.lblExportServerFileNameHdr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExportServerFileNameHdr.Location = new System.Drawing.Point(3, 3);
            this.lblExportServerFileNameHdr.Name = "lblExportServerFileNameHdr";
            this.lblExportServerFileNameHdr.Size = new System.Drawing.Size(164, 16);
            this.lblExportServerFileNameHdr.TabIndex = 13;
            this.lblExportServerFileNameHdr.Text = "Server Export File Name";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.BackGradientColor1 = System.Drawing.Color.DarkOrchid;
            this.btnExport.BackGradientColor2 = System.Drawing.Color.Thistle;
            this.btnExport.BackGradientColorMouseDown1 = System.Drawing.Color.LightPink;
            this.btnExport.BackGradientColorMouseDown2 = System.Drawing.Color.MediumVioletRed;
            this.btnExport.BackGradientColorMouseOver1 = System.Drawing.Color.MediumOrchid;
            this.btnExport.BackGradientColorMouseOver2 = System.Drawing.Color.LavenderBlush;
            this.btnExport.BackGradientType = DotCoolControls.Tools.GradientType.Triangular;
            this.btnExport.BackGradientTypeMouseDown = DotCoolControls.Tools.GradientType.Circular;
            this.btnExport.BackGradientTypeMouseOver = DotCoolControls.Tools.GradientType.Triangular;
            this.btnExport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExport.BackgroundImage")));
            this.btnExport.DrawBackgroundGradient = true;
            this.btnExport.EnableTextMouseDown = true;
            this.btnExport.EnableTextMouseOver = true;
            this.btnExport.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.FontMouseDown = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.FontMouseOver = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExport.ForeColorMouseDown = System.Drawing.Color.WhiteSmoke;
            this.btnExport.ForeColorMouseOver = System.Drawing.Color.Lime;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(39, 120);
            this.btnExport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Padding = new System.Windows.Forms.Padding(10, 0, 0, 2);
            this.btnExport.Size = new System.Drawing.Size(230, 45);
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // tabSettings
            // 
            this.tabSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.tabSettings.Controls.Add(this.pnlTesting);
            this.tabSettings.Controls.Add(this.pnlDefaultSizeSettings);
            this.tabSettings.Location = new System.Drawing.Point(4, 25);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(1267, 244);
            this.tabSettings.TabIndex = 2;
            this.tabSettings.Text = "Settings";
            // 
            // pnlTesting
            // 
            this.pnlTesting.Controls.Add(this.chbShowParseTime);
            this.pnlTesting.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTesting.Location = new System.Drawing.Point(9, 96);
            this.pnlTesting.Name = "pnlTesting";
            this.pnlTesting.Size = new System.Drawing.Size(297, 57);
            this.pnlTesting.TabIndex = 15;
            this.pnlTesting.TabStop = false;
            this.pnlTesting.Text = "Testing";
            // 
            // chbShowParseTime
            // 
            this.chbShowParseTime.AutoSize = true;
            this.chbShowParseTime.Location = new System.Drawing.Point(12, 21);
            this.chbShowParseTime.Name = "chbShowParseTime";
            this.chbShowParseTime.Size = new System.Drawing.Size(139, 20);
            this.chbShowParseTime.TabIndex = 13;
            this.chbShowParseTime.Text = "Show Parse Time";
            this.chbShowParseTime.UseVisualStyleBackColor = true;
            // 
            // pnlDefaultSizeSettings
            // 
            this.pnlDefaultSizeSettings.Controls.Add(this.lblEnumSizeHdr);
            this.pnlDefaultSizeSettings.Controls.Add(this.txtEnumSize);
            this.pnlDefaultSizeSettings.Controls.Add(this.lblPointerSizeHdr);
            this.pnlDefaultSizeSettings.Controls.Add(this.txtPointerSize);
            this.pnlDefaultSizeSettings.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlDefaultSizeSettings.Location = new System.Drawing.Point(9, 6);
            this.pnlDefaultSizeSettings.Name = "pnlDefaultSizeSettings";
            this.pnlDefaultSizeSettings.Size = new System.Drawing.Size(297, 75);
            this.pnlDefaultSizeSettings.TabIndex = 14;
            this.pnlDefaultSizeSettings.TabStop = false;
            this.pnlDefaultSizeSettings.Text = "Default Size Settings";
            // 
            // lblEnumSizeHdr
            // 
            this.lblEnumSizeHdr.AutoSize = true;
            this.lblEnumSizeHdr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnumSizeHdr.Location = new System.Drawing.Point(159, 24);
            this.lblEnumSizeHdr.Name = "lblEnumSizeHdr";
            this.lblEnumSizeHdr.Size = new System.Drawing.Size(122, 16);
            this.lblEnumSizeHdr.TabIndex = 14;
            this.lblEnumSizeHdr.Text = "Enum Size (Bytes)";
            // 
            // txtEnumSize
            // 
            this.txtEnumSize.BackColor = System.Drawing.Color.AliceBlue;
            this.txtEnumSize.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEnumSize.Location = new System.Drawing.Point(159, 42);
            this.txtEnumSize.Name = "txtEnumSize";
            this.txtEnumSize.Size = new System.Drawing.Size(129, 22);
            this.txtEnumSize.TabIndex = 15;
            this.txtEnumSize.Text = "4";
            this.txtEnumSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPointerSizeHdr
            // 
            this.lblPointerSizeHdr.AutoSize = true;
            this.lblPointerSizeHdr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPointerSizeHdr.Location = new System.Drawing.Point(9, 24);
            this.lblPointerSizeHdr.Name = "lblPointerSizeHdr";
            this.lblPointerSizeHdr.Size = new System.Drawing.Size(132, 16);
            this.lblPointerSizeHdr.TabIndex = 12;
            this.lblPointerSizeHdr.Text = "Pointer Size (Bytes)";
            // 
            // txtPointerSize
            // 
            this.txtPointerSize.BackColor = System.Drawing.Color.AliceBlue;
            this.txtPointerSize.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPointerSize.Location = new System.Drawing.Point(9, 42);
            this.txtPointerSize.Name = "txtPointerSize";
            this.txtPointerSize.Size = new System.Drawing.Size(129, 22);
            this.txtPointerSize.TabIndex = 13;
            this.txtPointerSize.Text = "4";
            this.txtPointerSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pnlStruct
            // 
            this.pnlStruct.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlStruct.Controls.Add(this.lblStructElements);
            this.pnlStruct.Controls.Add(this.lblStructElementsHdr);
            this.pnlStruct.Controls.Add(this.lblStructFieldCount);
            this.pnlStruct.Controls.Add(this.lblStructFieldCountHdr);
            this.pnlStruct.Controls.Add(this.lblStructSize);
            this.pnlStruct.Controls.Add(this.lblStructSizeHdr);
            this.pnlStruct.Controls.Add(this.lblStructName);
            this.pnlStruct.Controls.Add(this.lblStructNameHdr);
            this.pnlStruct.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStruct.Location = new System.Drawing.Point(0, 0);
            this.pnlStruct.Name = "pnlStruct";
            this.pnlStruct.Size = new System.Drawing.Size(994, 51);
            this.pnlStruct.TabIndex = 14;
            // 
            // lblStructElements
            // 
            this.lblStructElements.BackColor = System.Drawing.Color.AliceBlue;
            this.lblStructElements.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStructElements.ForeColor = System.Drawing.Color.Black;
            this.lblStructElements.Location = new System.Drawing.Point(684, 21);
            this.lblStructElements.Name = "lblStructElements";
            this.lblStructElements.Size = new System.Drawing.Size(90, 22);
            this.lblStructElements.TabIndex = 21;
            this.lblStructElements.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStructElementsHdr
            // 
            this.lblStructElementsHdr.AutoSize = true;
            this.lblStructElementsHdr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStructElementsHdr.Location = new System.Drawing.Point(684, 6);
            this.lblStructElementsHdr.Name = "lblStructElementsHdr";
            this.lblStructElementsHdr.Size = new System.Drawing.Size(66, 16);
            this.lblStructElementsHdr.TabIndex = 20;
            this.lblStructElementsHdr.Text = "Elements";
            // 
            // lblStructFieldCount
            // 
            this.lblStructFieldCount.BackColor = System.Drawing.Color.AliceBlue;
            this.lblStructFieldCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStructFieldCount.ForeColor = System.Drawing.Color.Black;
            this.lblStructFieldCount.Location = new System.Drawing.Point(579, 21);
            this.lblStructFieldCount.Name = "lblStructFieldCount";
            this.lblStructFieldCount.Size = new System.Drawing.Size(90, 22);
            this.lblStructFieldCount.TabIndex = 19;
            this.lblStructFieldCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStructFieldCountHdr
            // 
            this.lblStructFieldCountHdr.AutoSize = true;
            this.lblStructFieldCountHdr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStructFieldCountHdr.Location = new System.Drawing.Point(579, 6);
            this.lblStructFieldCountHdr.Name = "lblStructFieldCountHdr";
            this.lblStructFieldCountHdr.Size = new System.Drawing.Size(81, 16);
            this.lblStructFieldCountHdr.TabIndex = 18;
            this.lblStructFieldCountHdr.Text = "Field Count";
            // 
            // lblStructSize
            // 
            this.lblStructSize.BackColor = System.Drawing.Color.AliceBlue;
            this.lblStructSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStructSize.ForeColor = System.Drawing.Color.Black;
            this.lblStructSize.Location = new System.Drawing.Point(387, 21);
            this.lblStructSize.Name = "lblStructSize";
            this.lblStructSize.Size = new System.Drawing.Size(177, 22);
            this.lblStructSize.TabIndex = 17;
            this.lblStructSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStructSizeHdr
            // 
            this.lblStructSizeHdr.AutoSize = true;
            this.lblStructSizeHdr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStructSizeHdr.Location = new System.Drawing.Point(387, 6);
            this.lblStructSizeHdr.Name = "lblStructSizeHdr";
            this.lblStructSizeHdr.Size = new System.Drawing.Size(144, 16);
            this.lblStructSizeHdr.TabIndex = 16;
            this.lblStructSizeHdr.Text = "Structure Size (Bytes)";
            // 
            // lblStructName
            // 
            this.lblStructName.BackColor = System.Drawing.Color.AliceBlue;
            this.lblStructName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStructName.ForeColor = System.Drawing.Color.Black;
            this.lblStructName.Location = new System.Drawing.Point(9, 21);
            this.lblStructName.Name = "lblStructName";
            this.lblStructName.Size = new System.Drawing.Size(372, 22);
            this.lblStructName.TabIndex = 15;
            this.lblStructName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStructNameHdr
            // 
            this.lblStructNameHdr.AutoSize = true;
            this.lblStructNameHdr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStructNameHdr.Location = new System.Drawing.Point(9, 6);
            this.lblStructNameHdr.Name = "lblStructNameHdr";
            this.lblStructNameHdr.Size = new System.Drawing.Size(107, 16);
            this.lblStructNameHdr.TabIndex = 13;
            this.lblStructNameHdr.Text = "Structure Name";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lbStructs);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridFields);
            this.splitContainer1.Panel2.Controls.Add(this.pnlStruct);
            this.splitContainer1.Size = new System.Drawing.Size(1281, 468);
            this.splitContainer1.SplitterDistance = 279;
            this.splitContainer1.TabIndex = 15;
            // 
            // gridFields
            // 
            this.gridFields.AutoGenerateColumns = false;
            this.gridFields.BackGradientType = DotCoolControls.Tools.GradientType.Ellipsis;
            this.gridFields.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.gridFields.BackgroundGradientColor1 = System.Drawing.Color.GhostWhite;
            this.gridFields.BackgroundGradientColor2 = System.Drawing.Color.Navy;
            this.gridFields.CellTransAlpha = 45;
            this.gridFields.CellTransColor = System.Drawing.Color.Maroon;
            this.gridFields.ColHeaderGradientBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.gridFields.ColHeaderGradientBorderWidth = 2F;
            this.gridFields.ColHeaderGradientColor1 = System.Drawing.Color.LightSteelBlue;
            this.gridFields.ColHeaderGradientColor2 = System.Drawing.Color.DarkBlue;
            this.gridFields.ColHeaderGradientType = DotCoolControls.Tools.GradientType.Triangular;
            this.gridFields.ColHeadersFont = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridFields.ColHeaderTransAlpha = 50;
            this.gridFields.ColHeaderTransColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridFields.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridFields.ColumnHeadersHeight = 25;
            this.gridFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fieldByteOffsetDataGridViewTextBoxColumn,
            this.fieldTypeNameDataGridViewTextBoxColumn,
            this.fieldNameDataGridViewTextBoxColumn,
            this.fieldIndexDataGridViewTextBoxColumn,
            this.dataSizeDataGridViewTextBoxColumn,
            this.dataTypeDataGridViewTextBoxColumn,
            this.elementsDataGridViewTextBoxColumn,
            this.fieldKeyDataGridViewTextBoxColumn});
            this.gridFields.DataMember = "tblFieldsView";
            this.gridFields.DataSource = this.dsHeaderDataView;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridFields.DefaultCellStyle = dataGridViewCellStyle10;
            this.gridFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridFields.DrawBackgroundGradient = true;
            this.gridFields.DrawCellTransColor = true;
            this.gridFields.DrawColHeaderGradient = true;
            this.gridFields.EnableHeadersVisualStyles = false;
            this.gridFields.GridColor = System.Drawing.Color.Navy;
            this.gridFields.Location = new System.Drawing.Point(0, 51);
            this.gridFields.Name = "gridFields";
            this.gridFields.ReadOnly = true;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridFields.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.gridFields.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridFields.Size = new System.Drawing.Size(994, 413);
            this.gridFields.TabIndex = 3;
            // 
            // fieldByteOffsetDataGridViewTextBoxColumn
            // 
            this.fieldByteOffsetDataGridViewTextBoxColumn.DataPropertyName = "FieldByteOffset";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Transparent;
            this.fieldByteOffsetDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.fieldByteOffsetDataGridViewTextBoxColumn.HeaderText = "Offset";
            this.fieldByteOffsetDataGridViewTextBoxColumn.Name = "fieldByteOffsetDataGridViewTextBoxColumn";
            this.fieldByteOffsetDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fieldTypeNameDataGridViewTextBoxColumn
            // 
            this.fieldTypeNameDataGridViewTextBoxColumn.DataPropertyName = "FieldTypeName";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Transparent;
            this.fieldTypeNameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.fieldTypeNameDataGridViewTextBoxColumn.HeaderText = "Type";
            this.fieldTypeNameDataGridViewTextBoxColumn.Name = "fieldTypeNameDataGridViewTextBoxColumn";
            this.fieldTypeNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fieldTypeNameDataGridViewTextBoxColumn.Width = 245;
            // 
            // fieldNameDataGridViewTextBoxColumn
            // 
            this.fieldNameDataGridViewTextBoxColumn.DataPropertyName = "FieldName";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Transparent;
            this.fieldNameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.fieldNameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.fieldNameDataGridViewTextBoxColumn.Name = "fieldNameDataGridViewTextBoxColumn";
            this.fieldNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fieldNameDataGridViewTextBoxColumn.Width = 250;
            // 
            // fieldIndexDataGridViewTextBoxColumn
            // 
            this.fieldIndexDataGridViewTextBoxColumn.DataPropertyName = "FieldIndex";
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Transparent;
            this.fieldIndexDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.fieldIndexDataGridViewTextBoxColumn.HeaderText = "Index";
            this.fieldIndexDataGridViewTextBoxColumn.Name = "fieldIndexDataGridViewTextBoxColumn";
            this.fieldIndexDataGridViewTextBoxColumn.ReadOnly = true;
            this.fieldIndexDataGridViewTextBoxColumn.Width = 60;
            // 
            // dataSizeDataGridViewTextBoxColumn
            // 
            this.dataSizeDataGridViewTextBoxColumn.DataPropertyName = "DataSize";
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Transparent;
            this.dataSizeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataSizeDataGridViewTextBoxColumn.HeaderText = "Size";
            this.dataSizeDataGridViewTextBoxColumn.Name = "dataSizeDataGridViewTextBoxColumn";
            this.dataSizeDataGridViewTextBoxColumn.ReadOnly = true;
            this.dataSizeDataGridViewTextBoxColumn.Width = 110;
            // 
            // dataTypeDataGridViewTextBoxColumn
            // 
            this.dataTypeDataGridViewTextBoxColumn.DataPropertyName = "DataType";
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Transparent;
            this.dataTypeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataTypeDataGridViewTextBoxColumn.HeaderText = "Data Type";
            this.dataTypeDataGridViewTextBoxColumn.Name = "dataTypeDataGridViewTextBoxColumn";
            this.dataTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.dataTypeDataGridViewTextBoxColumn.Width = 110;
            // 
            // elementsDataGridViewTextBoxColumn
            // 
            this.elementsDataGridViewTextBoxColumn.DataPropertyName = "Elements";
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Transparent;
            this.elementsDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.elementsDataGridViewTextBoxColumn.HeaderText = "Elements";
            this.elementsDataGridViewTextBoxColumn.Name = "elementsDataGridViewTextBoxColumn";
            this.elementsDataGridViewTextBoxColumn.ReadOnly = true;
            this.elementsDataGridViewTextBoxColumn.Width = 75;
            // 
            // fieldKeyDataGridViewTextBoxColumn
            // 
            this.fieldKeyDataGridViewTextBoxColumn.DataPropertyName = "FieldKey";
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Transparent;
            this.fieldKeyDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle9;
            this.fieldKeyDataGridViewTextBoxColumn.HeaderText = "FieldKey";
            this.fieldKeyDataGridViewTextBoxColumn.Name = "fieldKeyDataGridViewTextBoxColumn";
            this.fieldKeyDataGridViewTextBoxColumn.ReadOnly = true;
            this.fieldKeyDataGridViewTextBoxColumn.Visible = false;
            // 
            // dsHeaderDataView
            // 
            this.dsHeaderDataView.DataSetName = "CHeaderDataSet";
            this.dsHeaderDataView.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // frmCHeaderExtract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1283, 753);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.MainTab);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmCHeaderExtract";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C Header Structure/Union Extractor";
            this.Load += new System.EventHandler(this.frmCHeaderExtract_Load);
            this.pnlStructUnion.ResumeLayout(false);
            this.pnlStructUnion.PerformLayout();
            this.MainTab.ResumeLayout(false);
            this.tabFileLoad.ResumeLayout(false);
            this.tabFileLoad.PerformLayout();
            this.tabDisplayExport.ResumeLayout(false);
            this.pnlQuerySettings.ResumeLayout(false);
            this.pnlQuerySettings.PerformLayout();
            this.pnlSortOrder.ResumeLayout(false);
            this.pnlSortOrder.PerformLayout();
            this.pnlQryDataSize.ResumeLayout(false);
            this.pnlQryDataSize.PerformLayout();
            this.pnlQryRegex.ResumeLayout(false);
            this.pnlQryRegex.PerformLayout();
            this.pnlQryWildcard.ResumeLayout(false);
            this.pnlQryWildcard.PerformLayout();
            this.pnlQryName.ResumeLayout(false);
            this.pnlQryName.PerformLayout();
            this.pnlDataExport.ResumeLayout(false);
            this.pnlDataExport.PerformLayout();
            this.pnlExportServerFile.ResumeLayout(false);
            this.pnlExportServerFile.PerformLayout();
            this.tabSettings.ResumeLayout(false);
            this.pnlTesting.ResumeLayout(false);
            this.pnlTesting.PerformLayout();
            this.pnlDefaultSizeSettings.ResumeLayout(false);
            this.pnlDefaultSizeSettings.PerformLayout();
            this.pnlStruct.ResumeLayout(false);
            this.pnlStruct.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsHeaderDataView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox lbStructs;
        private CHeaderParser.Data.CHeaderDataSet dsHeaderDataView;
        private System.Windows.Forms.GroupBox pnlStructUnion;
        private System.Windows.Forms.RadioButton rbSUUnion;
        private System.Windows.Forms.RadioButton rbSUStruct;
        private System.Windows.Forms.TabControl MainTab;
        private System.Windows.Forms.TabPage tabFileLoad;
        private DotCoolControls.DotCoolButton btnParse;
        private DotCoolControls.DotCoolButton btnHeaderFileSearch;
        private System.Windows.Forms.TextBox txtHeaderFileNames;
        private System.Windows.Forms.Label lblHeaderFileNamesInfoHdr;
        private System.Windows.Forms.Label lblHeaderFileNamesHdr;
        private System.Windows.Forms.TabPage tabDisplayExport;
        private System.Windows.Forms.GroupBox pnlQuerySettings;
        private System.Windows.Forms.RadioButton rbQryName;
        private System.Windows.Forms.RadioButton rbQryDisplayAll;
        private System.Windows.Forms.Label lblQryDataSizeMinSizeHdr;
        private System.Windows.Forms.Panel pnlQryName;
        private System.Windows.Forms.CheckBox chbQryNameMatchAny;
        private System.Windows.Forms.TextBox txtQryName;
        private System.Windows.Forms.Panel pnlQryWildcard;
        private System.Windows.Forms.CheckBox chbQryWildcardMatchCase;
        private System.Windows.Forms.TextBox txtQryWildcard;
        private System.Windows.Forms.RadioButton rbQryWildcard;
        private System.Windows.Forms.CheckBox chbQryNameMatchCase;
        private System.Windows.Forms.CheckBox chbQryNameExactMatch;
        private System.Windows.Forms.Panel pnlQryDataSize;
        private System.Windows.Forms.TextBox txtQryDataSizeMaxSize;
        private System.Windows.Forms.TextBox txtQryDataSizeMinSize;
        private System.Windows.Forms.Label lblQryDataSizeMaxSizeHdr;
        private System.Windows.Forms.RadioButton rbQryDataSize;
        private System.Windows.Forms.Panel pnlQryRegex;
        private System.Windows.Forms.TextBox txtQryRegEx;
        private System.Windows.Forms.RadioButton rbQryRegex;
        private System.Windows.Forms.RadioButton rbSUBoth;
        private System.Windows.Forms.GroupBox pnlSortOrder;
        private System.Windows.Forms.RadioButton rbSONone;
        private System.Windows.Forms.RadioButton rbSODescending;
        private System.Windows.Forms.RadioButton rbSOAscending;
        private DotCoolControls.DotCoolButton btnClearFileNames;
        private DotCoolControls.DotCoolButton btnExport;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.GroupBox pnlDefaultSizeSettings;
        private System.Windows.Forms.Label lblEnumSizeHdr;
        private System.Windows.Forms.TextBox txtEnumSize;
        private System.Windows.Forms.Label lblPointerSizeHdr;
        private System.Windows.Forms.TextBox txtPointerSize;
        private DotCoolControls.DotCoolButton btnQuery;
        private System.Windows.Forms.Panel pnlStruct;
        private System.Windows.Forms.Label lblStructFieldCount;
        private System.Windows.Forms.Label lblStructFieldCountHdr;
        private System.Windows.Forms.Label lblStructSize;
        private System.Windows.Forms.Label lblStructSizeHdr;
        private System.Windows.Forms.Label lblStructName;
        private System.Windows.Forms.Label lblStructNameHdr;
        private System.Windows.Forms.Label lblStructElements;
        private System.Windows.Forms.Label lblStructElementsHdr;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox pnlTesting;
        private System.Windows.Forms.CheckBox chbShowParseTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn fieldByteOffsetDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fieldTypeNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fieldNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fieldIndexDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataSizeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn elementsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fieldKeyDataGridViewTextBoxColumn;
        private DotCoolControls.DotCoolButton btnExportServerFileNameSearch;
        private System.Windows.Forms.TextBox txtExportServerFileName;
        private System.Windows.Forms.Label lblExportServerFileNameHdr;
        private System.Windows.Forms.GroupBox pnlDataExport;
        private System.Windows.Forms.RadioButton rbExportServerData;
        private System.Windows.Forms.RadioButton rbExportLocalData;
        private System.Windows.Forms.Panel pnlExportServerFile;
        private DotCoolControls.DotCoolGridView gridFields;
    }
}

