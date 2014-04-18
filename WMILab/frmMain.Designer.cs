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
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listViewClasses = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuClassList = new System.Windows.Forms.MenuStrip();
            this.txtClassFilter = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnToggleSystemClasses = new System.Windows.Forms.ToolStripMenuItem();
            this.treeViewNamespaces = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabClassMembers = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.treeViewClassMembers = new System.Windows.Forms.TreeView();
            this.tabQuery = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.menuQuery = new System.Windows.Forms.MenuStrip();
            this.btnExecuteQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancelQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.gridQueryResults = new System.Windows.Forms.DataGridView();
            this.Code = new System.Windows.Forms.TabPage();
            this.menuQueryRow = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnGetAssociatorsOf = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGetReferencesOf = new System.Windows.Forms.ToolStripMenuItem();
            this.btnResultPropertiesSeparater = new System.Windows.Forms.ToolStripSeparator();
            this.btnResultProperies = new System.Windows.Forms.ToolStripMenuItem();
            this.txtClassMemberDetail = new System.Windows.Forms.RichTextBoxEx();
            this.menuMain.SuspendLayout();
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
            this.menuQueryRow.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(985, 24);
            this.menuMain.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(985, 492);
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
            this.splitContainer2.Size = new System.Drawing.Size(275, 492);
            this.splitContainer2.SplitterDistance = 294;
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
            this.listViewClasses.Size = new System.Drawing.Size(275, 267);
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
            // 
            // menuClassList
            // 
            this.menuClassList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtClassFilter,
            this.toolStripMenuItem1,
            this.btnToggleSystemClasses});
            this.menuClassList.Location = new System.Drawing.Point(0, 0);
            this.menuClassList.Name = "menuClassList";
            this.menuClassList.Size = new System.Drawing.Size(275, 27);
            this.menuClassList.TabIndex = 1;
            this.menuClassList.Text = "menuClassFilter";
            // 
            // txtClassFilter
            // 
            this.txtClassFilter.Name = "txtClassFilter";
            this.txtClassFilter.Size = new System.Drawing.Size(200, 23);
            this.txtClassFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtClassFilter_KeyUp);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripMenuItem1.Image = global::WMILab.Properties.Resources.Search;
            this.toolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(28, 23);
            this.toolStripMenuItem1.Text = "Filter";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // btnToggleSystemClasses
            // 
            this.btnToggleSystemClasses.CheckOnClick = true;
            this.btnToggleSystemClasses.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnToggleSystemClasses.Image = global::WMILab.Properties.Resources.ShowHidden;
            this.btnToggleSystemClasses.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnToggleSystemClasses.Name = "btnToggleSystemClasses";
            this.btnToggleSystemClasses.Size = new System.Drawing.Size(28, 23);
            this.btnToggleSystemClasses.Text = "Toggle system classes";
            this.btnToggleSystemClasses.ToolTipText = "Toggle system classes";
            this.btnToggleSystemClasses.Click += new System.EventHandler(this.btnToggleSystemClasses_Click);
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
            this.tabControl1.Controls.Add(this.Code);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.ImageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(706, 492);
            this.tabControl1.TabIndex = 0;
            // 
            // tabClassMembers
            // 
            this.tabClassMembers.Controls.Add(this.splitContainer3);
            this.tabClassMembers.ImageKey = "Object";
            this.tabClassMembers.Location = new System.Drawing.Point(4, 23);
            this.tabClassMembers.Name = "tabClassMembers";
            this.tabClassMembers.Padding = new System.Windows.Forms.Padding(3);
            this.tabClassMembers.Size = new System.Drawing.Size(698, 465);
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
            this.splitContainer3.Size = new System.Drawing.Size(692, 459);
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
            this.treeViewClassMembers.Size = new System.Drawing.Size(226, 459);
            this.treeViewClassMembers.TabIndex = 0;
            this.treeViewClassMembers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewClassMembers_AfterSelect);
            // 
            // tabQuery
            // 
            this.tabQuery.Controls.Add(this.splitContainer4);
            this.tabQuery.ImageKey = "Query";
            this.tabQuery.Location = new System.Drawing.Point(4, 23);
            this.tabQuery.Name = "tabQuery";
            this.tabQuery.Padding = new System.Windows.Forms.Padding(3);
            this.tabQuery.Size = new System.Drawing.Size(698, 465);
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
            this.splitContainer4.Size = new System.Drawing.Size(692, 459);
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
            this.gridQueryResults.Size = new System.Drawing.Size(692, 375);
            this.gridQueryResults.TabIndex = 0;
            this.gridQueryResults.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridQueryResults_CellClicked);
            this.gridQueryResults.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridQueryResults_CellDoubleClicked);
            this.gridQueryResults.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridQueryResults_CellMouseEnter);
            this.gridQueryResults.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridQueryResults_CellMouseUp);
            // 
            // Code
            // 
            this.Code.ImageKey = "Code";
            this.Code.Location = new System.Drawing.Point(4, 23);
            this.Code.Name = "Code";
            this.Code.Padding = new System.Windows.Forms.Padding(3);
            this.Code.Size = new System.Drawing.Size(698, 465);
            this.Code.TabIndex = 2;
            this.Code.Text = "Code";
            this.Code.UseVisualStyleBackColor = true;
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
            // txtClassMemberDetail
            // 
            this.txtClassMemberDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtClassMemberDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtClassMemberDetail.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClassMemberDetail.Location = new System.Drawing.Point(0, 0);
            this.txtClassMemberDetail.Name = "txtClassMemberDetail";
            this.txtClassMemberDetail.Size = new System.Drawing.Size(462, 459);
            this.txtClassMemberDetail.TabIndex = 0;
            this.txtClassMemberDetail.Text = "";
            this.txtClassMemberDetail.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtClassMemberDetail_LinkClicked);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 516);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuMain;
            this.Name = "frmMain";
            this.Text = "WMI Lab";
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
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
            this.menuQueryRow.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage Code;
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
        private System.Windows.Forms.ToolStripTextBox txtClassFilter;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem btnToggleSystemClasses;
        private System.Windows.Forms.ContextMenuStrip menuQueryRow;
        private System.Windows.Forms.ToolStripMenuItem btnGetAssociatorsOf;
        private System.Windows.Forms.ToolStripMenuItem btnGetReferencesOf;
        private System.Windows.Forms.ToolStripSeparator btnResultPropertiesSeparater;
        private System.Windows.Forms.ToolStripMenuItem btnResultProperies;
    }
}

