namespace WMILab
{
    partial class frmMain
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
            if (disposing)
            {
                // Seems to cause app freezing while loading namespaces
                //this.nsTreeObserver.Cancel();
                //this.classListObserver.Cancel();
            }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConnectToServer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showSystemClassesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showmappedValuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuQueryRow = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnGetAssociatorsOf = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGetReferencesOf = new System.Windows.Forms.ToolStripMenuItem();
            this.btnResultPropertiesSeparater = new System.Windows.Forms.ToolStripSeparator();
            this.btnResultProperies = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listViewClasses = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuClassList = new System.Windows.Forms.MenuStrip();
            this.txtClassFilter = new System.Windows.Forms.ToolStripTextBoxEx();
            this.btnFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.treeViewNamespaces = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabClassMembers = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.treeViewClassMembers = new System.Windows.Forms.TreeView();
            this.txtClassMemberDetail = new System.Windows.Forms.RichTextBoxEx();
            this.tabQuery = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.menuQuery = new System.Windows.Forms.MenuStrip();
            this.btnExecuteQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancelQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.gridQueryResults = new System.Windows.Forms.DataGridView();
            this.tabCode = new System.Windows.Forms.TabPage();
            this.menuStripCode = new System.Windows.Forms.MenuStrip();
            this.mnuScriptTemplates = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSaveScript = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCodeOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewLog = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuMain.SuspendLayout();
            this.menuQueryRow.SuspendLayout();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.menuClassList.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabClassMembers.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabQuery.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.menuQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridQueryResults)).BeginInit();
            this.tabCode.SuspendLayout();
            this.menuStripCode.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(985, 24);
            this.menuMain.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConnectToServer,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // btnConnectToServer
            // 
            this.btnConnectToServer.Image = global::WMILab.Properties.Resources.Connect;
            this.btnConnectToServer.Name = "btnConnectToServer";
            this.btnConnectToServer.Size = new System.Drawing.Size(167, 22);
            this.btnConnectToServer.Text = "&Connect to server";
            this.btnConnectToServer.Click += new System.EventHandler(this.btnConnectToServer_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(164, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showSystemClassesToolStripMenuItem,
            this.showmappedValuesToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // showSystemClassesToolStripMenuItem
            // 
            this.showSystemClassesToolStripMenuItem.Name = "showSystemClassesToolStripMenuItem";
            this.showSystemClassesToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.showSystemClassesToolStripMenuItem.Text = "Show &system classes";
            this.showSystemClassesToolStripMenuItem.Click += new System.EventHandler(this.showSystemClassesToolStripMenuItem_Click);
            // 
            // showmappedValuesToolStripMenuItem
            // 
            this.showmappedValuesToolStripMenuItem.Checked = true;
            this.showmappedValuesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showmappedValuesToolStripMenuItem.Name = "showmappedValuesToolStripMenuItem";
            this.showmappedValuesToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.showmappedValuesToolStripMenuItem.Text = "Show &mapped values";
            this.showmappedValuesToolStripMenuItem.Click += new System.EventHandler(this.showmappedValuesToolStripMenuItem_Click);
            // 
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.ImageList1.Images.SetKeyName(0, "Code");
            this.ImageList1.Images.SetKeyName(1, "Notes");
            this.ImageList1.Images.SetKeyName(2, "Property");
            this.ImageList1.Images.SetKeyName(3, "Query");
            this.ImageList1.Images.SetKeyName(4, "NameSpace");
            this.ImageList1.Images.SetKeyName(5, "NameSpaceOpen");
            this.ImageList1.Images.SetKeyName(6, "Method");
            this.ImageList1.Images.SetKeyName(7, "Class");
            this.ImageList1.Images.SetKeyName(8, "ClassSystem");
            this.ImageList1.Images.SetKeyName(9, "ClassInherited");
            this.ImageList1.Images.SetKeyName(10, "Event");
            this.ImageList1.Images.SetKeyName(11, "EventSystem");
            this.ImageList1.Images.SetKeyName(12, "EventInherited");
            this.ImageList1.Images.SetKeyName(13, "Home");
            this.ImageList1.Images.SetKeyName(14, "Favorites");
            this.ImageList1.Images.SetKeyName(15, "PropertyKey");
            this.ImageList1.Images.SetKeyName(16, "Object");
            this.ImageList1.Images.SetKeyName(17, "Raw");
            this.ImageList1.Images.SetKeyName(18, "ClassAssoc");
            this.ImageList1.Images.SetKeyName(19, "Performance");
            this.ImageList1.Images.SetKeyName(20, "StaticMethod");
            this.ImageList1.Images.SetKeyName(21, "Information");
            this.ImageList1.Images.SetKeyName(22, "Critical");
            this.ImageList1.Images.SetKeyName(23, "Warning");
            this.ImageList1.Images.SetKeyName(24, "Success");
            // 
            // menuQueryRow
            // 
            this.menuQueryRow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGetAssociatorsOf,
            this.btnGetReferencesOf,
            this.btnResultPropertiesSeparater,
            this.btnResultProperies});
            this.menuQueryRow.Name = "menuQueryRow";
            this.menuQueryRow.Size = new System.Drawing.Size(172, 76);
            // 
            // btnGetAssociatorsOf
            // 
            this.btnGetAssociatorsOf.Name = "btnGetAssociatorsOf";
            this.btnGetAssociatorsOf.Size = new System.Drawing.Size(171, 22);
            this.btnGetAssociatorsOf.Text = "Get Associators Of";
            this.btnGetAssociatorsOf.Click += new System.EventHandler(this.btnGetAssociatorsOf_Click);
            // 
            // btnGetReferencesOf
            // 
            this.btnGetReferencesOf.Name = "btnGetReferencesOf";
            this.btnGetReferencesOf.Size = new System.Drawing.Size(171, 22);
            this.btnGetReferencesOf.Text = "Get References Of";
            this.btnGetReferencesOf.Click += new System.EventHandler(this.btnGetReferencesOf_Click);
            // 
            // btnResultPropertiesSeparater
            // 
            this.btnResultPropertiesSeparater.Name = "btnResultPropertiesSeparater";
            this.btnResultPropertiesSeparater.Size = new System.Drawing.Size(168, 6);
            // 
            // btnResultProperies
            // 
            this.btnResultProperies.Image = global::WMILab.Properties.Resources.Properties;
            this.btnResultProperies.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnResultProperies.Name = "btnResultProperies";
            this.btnResultProperies.Size = new System.Drawing.Size(171, 22);
            this.btnResultProperies.Text = "Properties";
            this.btnResultProperies.Click += new System.EventHandler(this.btnResultProperies_Click);
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer5.Location = new System.Drawing.Point(0, 24);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.listViewLog);
            this.splitContainer5.Size = new System.Drawing.Size(985, 616);
            this.splitContainer5.SplitterDistance = 515;
            this.splitContainer5.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(985, 515);
            this.splitContainer1.SplitterDistance = 275;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listViewClasses);
            this.splitContainer2.Panel1.Controls.Add(this.menuClassList);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.treeViewNamespaces);
            this.splitContainer2.Size = new System.Drawing.Size(275, 515);
            this.splitContainer2.SplitterDistance = 317;
            this.splitContainer2.TabIndex = 0;
            // 
            // listViewClasses
            // 
            this.listViewClasses.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewClasses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewClasses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewClasses.FullRowSelect = true;
            this.listViewClasses.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewClasses.HideSelection = false;
            this.listViewClasses.Location = new System.Drawing.Point(0, 27);
            this.listViewClasses.MultiSelect = false;
            this.listViewClasses.Name = "listViewClasses";
            this.listViewClasses.Size = new System.Drawing.Size(275, 290);
            this.listViewClasses.SmallImageList = this.ImageList1;
            this.listViewClasses.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewClasses.TabIndex = 0;
            this.listViewClasses.UseCompatibleStateImageBehavior = false;
            this.listViewClasses.View = System.Windows.Forms.View.Details;
            this.listViewClasses.SelectedIndexChanged += new System.EventHandler(this.listViewClasses_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Class";
            this.columnHeader1.Width = 300;
            // 
            // menuClassList
            // 
            this.menuClassList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtClassFilter,
            this.btnFilter});
            this.menuClassList.Location = new System.Drawing.Point(0, 0);
            this.menuClassList.Name = "menuClassList";
            this.menuClassList.Size = new System.Drawing.Size(275, 27);
            this.menuClassList.TabIndex = 1;
            this.menuClassList.Text = "menuClassFilter";
            // 
            // txtClassFilter
            // 
            this.txtClassFilter.Name = "txtClassFilter";
            this.txtClassFilter.Size = new System.Drawing.Size(147, 23);
            this.txtClassFilter.Stretch = true;
            this.txtClassFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtClassFilter_KeyUp);
            // 
            // btnFilter
            // 
            this.btnFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFilter.Image = global::WMILab.Properties.Resources.Search;
            this.btnFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(28, 23);
            this.btnFilter.Text = "Filter";
            this.btnFilter.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // treeViewNamespaces
            // 
            this.treeViewNamespaces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewNamespaces.ImageKey = "NameSpace";
            this.treeViewNamespaces.ImageList = this.ImageList1;
            this.treeViewNamespaces.Location = new System.Drawing.Point(0, 0);
            this.treeViewNamespaces.Name = "treeViewNamespaces";
            this.treeViewNamespaces.SelectedImageKey = "NameSpaceOpen";
            this.treeViewNamespaces.ShowLines = false;
            this.treeViewNamespaces.ShowRootLines = false;
            this.treeViewNamespaces.Size = new System.Drawing.Size(275, 194);
            this.treeViewNamespaces.TabIndex = 0;
            this.treeViewNamespaces.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewNamespaces_AfterSelect);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabClassMembers);
            this.tabControl1.Controls.Add(this.tabQuery);
            this.tabControl1.Controls.Add(this.tabCode);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.ImageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(706, 515);
            this.tabControl1.TabIndex = 0;
            // 
            // tabClassMembers
            // 
            this.tabClassMembers.Controls.Add(this.splitContainer3);
            this.tabClassMembers.ImageKey = "Object";
            this.tabClassMembers.Location = new System.Drawing.Point(4, 23);
            this.tabClassMembers.Name = "tabClassMembers";
            this.tabClassMembers.Padding = new System.Windows.Forms.Padding(3);
            this.tabClassMembers.Size = new System.Drawing.Size(698, 488);
            this.tabClassMembers.TabIndex = 0;
            this.tabClassMembers.Text = "Members";
            this.tabClassMembers.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.treeViewClassMembers);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.txtClassMemberDetail);
            this.splitContainer3.Size = new System.Drawing.Size(692, 482);
            this.splitContainer3.SplitterDistance = 226;
            this.splitContainer3.TabIndex = 0;
            // 
            // treeViewClassMembers
            // 
            this.treeViewClassMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewClassMembers.ImageIndex = 0;
            this.treeViewClassMembers.ImageList = this.ImageList1;
            this.treeViewClassMembers.Location = new System.Drawing.Point(0, 0);
            this.treeViewClassMembers.Name = "treeViewClassMembers";
            this.treeViewClassMembers.SelectedImageIndex = 0;
            this.treeViewClassMembers.ShowLines = false;
            this.treeViewClassMembers.ShowRootLines = false;
            this.treeViewClassMembers.Size = new System.Drawing.Size(226, 482);
            this.treeViewClassMembers.TabIndex = 0;
            this.treeViewClassMembers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewClassMembers_AfterSelect);
            // 
            // txtClassMemberDetail
            // 
            this.txtClassMemberDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtClassMemberDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtClassMemberDetail.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClassMemberDetail.Location = new System.Drawing.Point(0, 0);
            this.txtClassMemberDetail.Name = "txtClassMemberDetail";
            this.txtClassMemberDetail.Size = new System.Drawing.Size(462, 482);
            this.txtClassMemberDetail.TabIndex = 0;
            this.txtClassMemberDetail.Text = "";
            this.txtClassMemberDetail.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtClassMemberDetail_LinkClicked);
            // 
            // tabQuery
            // 
            this.tabQuery.Controls.Add(this.splitContainer4);
            this.tabQuery.ImageKey = "Query";
            this.tabQuery.Location = new System.Drawing.Point(4, 23);
            this.tabQuery.Name = "tabQuery";
            this.tabQuery.Padding = new System.Windows.Forms.Padding(3);
            this.tabQuery.Size = new System.Drawing.Size(698, 488);
            this.tabQuery.TabIndex = 1;
            this.tabQuery.Text = "Query";
            this.tabQuery.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer4.Location = new System.Drawing.Point(3, 3);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.txtQuery);
            this.splitContainer4.Panel1.Controls.Add(this.menuQuery);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.gridQueryResults);
            this.splitContainer4.Size = new System.Drawing.Size(692, 482);
            this.splitContainer4.SplitterDistance = 80;
            this.splitContainer4.TabIndex = 0;
            // 
            // txtQuery
            // 
            this.txtQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuery.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuery.Location = new System.Drawing.Point(0, 24);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtQuery.Size = new System.Drawing.Size(692, 56);
            this.txtQuery.TabIndex = 0;
            // 
            // menuQuery
            // 
            this.menuQuery.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExecuteQuery,
            this.btnCancelQuery});
            this.menuQuery.Location = new System.Drawing.Point(0, 0);
            this.menuQuery.Name = "menuQuery";
            this.menuQuery.Size = new System.Drawing.Size(692, 24);
            this.menuQuery.TabIndex = 1;
            this.menuQuery.Text = "menuQuery";
            // 
            // btnExecuteQuery
            // 
            this.btnExecuteQuery.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnExecuteQuery.Image = global::WMILab.Properties.Resources.Execute;
            this.btnExecuteQuery.Name = "btnExecuteQuery";
            this.btnExecuteQuery.ShortcutKeyDisplayString = "";
            this.btnExecuteQuery.Size = new System.Drawing.Size(98, 20);
            this.btnExecuteQuery.Text = "Execute (F5)";
            this.btnExecuteQuery.Click += new System.EventHandler(this.executeToolStripMenuItem_Click);
            // 
            // btnCancelQuery
            // 
            this.btnCancelQuery.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnCancelQuery.Image = global::WMILab.Properties.Resources.Delete;
            this.btnCancelQuery.Name = "btnCancelQuery";
            this.btnCancelQuery.Size = new System.Drawing.Size(71, 20);
            this.btnCancelQuery.Text = "&Cancel";
            this.btnCancelQuery.Visible = false;
            this.btnCancelQuery.Click += new System.EventHandler(this.btnCancelQuery_Click);
            // 
            // gridQueryResults
            // 
            this.gridQueryResults.AllowUserToAddRows = false;
            this.gridQueryResults.AllowUserToDeleteRows = false;
            this.gridQueryResults.AllowUserToResizeRows = false;
            this.gridQueryResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridQueryResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridQueryResults.Location = new System.Drawing.Point(0, 0);
            this.gridQueryResults.MultiSelect = false;
            this.gridQueryResults.Name = "gridQueryResults";
            this.gridQueryResults.ReadOnly = true;
            this.gridQueryResults.RowHeadersVisible = false;
            this.gridQueryResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridQueryResults.Size = new System.Drawing.Size(692, 398);
            this.gridQueryResults.TabIndex = 0;
            this.gridQueryResults.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridQueryResults_CellClicked);
            this.gridQueryResults.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridQueryResults_CellContentClick);
            this.gridQueryResults.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridQueryResults_CellDoubleClicked);
            this.gridQueryResults.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridQueryResults_CellMouseEnter);
            this.gridQueryResults.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridQueryResults_CellMouseUp);
            // 
            // tabCode
            // 
            this.tabCode.Controls.Add(this.menuStripCode);
            this.tabCode.ImageKey = "Code";
            this.tabCode.Location = new System.Drawing.Point(4, 23);
            this.tabCode.Name = "tabCode";
            this.tabCode.Padding = new System.Windows.Forms.Padding(3);
            this.tabCode.Size = new System.Drawing.Size(698, 488);
            this.tabCode.TabIndex = 2;
            this.tabCode.Text = "Code";
            this.tabCode.UseVisualStyleBackColor = true;
            // 
            // menuStripCode
            // 
            this.menuStripCode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuScriptTemplates,
            this.btnSaveScript,
            this.mnuCodeOptions});
            this.menuStripCode.Location = new System.Drawing.Point(3, 3);
            this.menuStripCode.Name = "menuStripCode";
            this.menuStripCode.Size = new System.Drawing.Size(692, 24);
            this.menuStripCode.TabIndex = 0;
            // 
            // mnuScriptTemplates
            // 
            this.mnuScriptTemplates.Image = global::WMILab.Properties.Resources.Expanded;
            this.mnuScriptTemplates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuScriptTemplates.Name = "mnuScriptTemplates";
            this.mnuScriptTemplates.Size = new System.Drawing.Size(90, 20);
            this.mnuScriptTemplates.Text = "&Templates";
            // 
            // btnSaveScript
            // 
            this.btnSaveScript.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnSaveScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveScript.Image = global::WMILab.Properties.Resources.Save;
            this.btnSaveScript.Name = "btnSaveScript";
            this.btnSaveScript.Size = new System.Drawing.Size(28, 20);
            this.btnSaveScript.Text = "&Save";
            this.btnSaveScript.Click += new System.EventHandler(this.btnSaveScript_Click);
            // 
            // mnuCodeOptions
            // 
            this.mnuCodeOptions.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnuCodeOptions.Image = global::WMILab.Properties.Resources.Options;
            this.mnuCodeOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuCodeOptions.Name = "mnuCodeOptions";
            this.mnuCodeOptions.Size = new System.Drawing.Size(77, 20);
            this.mnuCodeOptions.Text = "&Options";
            // 
            // listViewLog
            // 
            this.listViewLog.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listViewLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewLog.FullRowSelect = true;
            this.listViewLog.GridLines = true;
            this.listViewLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewLog.HideSelection = false;
            this.listViewLog.Location = new System.Drawing.Point(0, 0);
            this.listViewLog.MultiSelect = false;
            this.listViewLog.Name = "listViewLog";
            this.listViewLog.Size = new System.Drawing.Size(985, 97);
            this.listViewLog.SmallImageList = this.ImageList1;
            this.listViewLog.TabIndex = 1;
            this.listViewLog.UseCompatibleStateImageBehavior = false;
            this.listViewLog.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Log message";
            this.columnHeader2.Width = 981;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "Message";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 640);
            this.Controls.Add(this.splitContainer5);
            this.Controls.Add(this.menuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuMain;
            this.Name = "frmMain";
            this.Text = "WMI Lab";
            this.Shown += new System.EventHandler(this.OnFormShown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.menuQueryRow.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.menuClassList.ResumeLayout(false);
            this.menuClassList.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabClassMembers.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.tabQuery.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.menuQuery.ResumeLayout(false);
            this.menuQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridQueryResults)).EndInit();
            this.tabCode.ResumeLayout(false);
            this.tabCode.PerformLayout();
            this.menuStripCode.ResumeLayout(false);
            this.menuStripCode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView listViewClasses;
        private System.Windows.Forms.TreeView treeViewNamespaces;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabClassMembers;
        private System.Windows.Forms.TabPage tabQuery;
        private System.Windows.Forms.TabPage tabCode;
        internal System.Windows.Forms.ImageList ImageList1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.RichTextBoxEx txtClassMemberDetail;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TreeView treeViewClassMembers;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.MenuStrip menuQuery;
        private System.Windows.Forms.ToolStripMenuItem btnExecuteQuery;
        private System.Windows.Forms.DataGridView gridQueryResults;
        private System.Windows.Forms.ToolStripMenuItem btnCancelQuery;
        private System.Windows.Forms.MenuStrip menuClassList;
        private System.Windows.Forms.ToolStripMenuItem btnFilter;
        private System.Windows.Forms.ContextMenuStrip menuQueryRow;
        private System.Windows.Forms.ToolStripMenuItem btnGetAssociatorsOf;
        private System.Windows.Forms.ToolStripMenuItem btnGetReferencesOf;
        private System.Windows.Forms.ToolStripSeparator btnResultPropertiesSeparater;
        private System.Windows.Forms.ToolStripMenuItem btnResultProperies;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.ListView listViewLog;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.MenuStrip menuStripCode;
        private System.Windows.Forms.ToolStripMenuItem mnuScriptTemplates;
        private System.Windows.Forms.ToolStripMenuItem btnSaveScript;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showSystemClassesToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBoxEx txtClassFilter;
        private System.Windows.Forms.ToolStripMenuItem showmappedValuesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnConnectToServer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuCodeOptions;
    }
}

