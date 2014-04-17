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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listViewClasses = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.treeViewNamespaces = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabClassMembers = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.treeViewClassMembers = new System.Windows.Forms.TreeView();
            this.txtClassMemberDetail = new System.Windows.Forms.RichTextBoxEx();
            this.tabQuery = new System.Windows.Forms.TabPage();
            this.Code = new System.Windows.Forms.TabPage();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabClassMembers.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(985, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
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
            this.listViewClasses.Location = new System.Drawing.Point(0, 0);
            this.listViewClasses.MultiSelect = false;
            this.listViewClasses.Name = "listViewClasses";
            this.listViewClasses.Size = new System.Drawing.Size(275, 294);
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
            // tabQuery
            // 
            this.tabQuery.ImageKey = "Query";
            this.tabQuery.Location = new System.Drawing.Point(4, 23);
            this.tabQuery.Name = "tabQuery";
            this.tabQuery.Padding = new System.Windows.Forms.Padding(3);
            this.tabQuery.Size = new System.Drawing.Size(698, 465);
            this.tabQuery.TabIndex = 1;
            this.tabQuery.Text = "Query";
            this.tabQuery.UseVisualStyleBackColor = true;
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
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 516);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "WMI Lab";
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabClassMembers.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
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
    }
}

