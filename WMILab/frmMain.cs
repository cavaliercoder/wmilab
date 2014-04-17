namespace WMILab
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Management;
    using System.Windows.Forms;

    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            // Connection and enumeration options
            classListEnumOpts.EnumerateDeep = true;
            classObjGetOpts.UseAmendedQualifiers = true;

            // Observer event handlers
            nsTreeObserver.ObjectReady += new ObjectReadyEventHandler(OnNamespaceReady);

            classListObserver.ObjectReady += new ObjectReadyEventHandler(OnClassReady);
            classListObserver.Completed += new CompletedEventHandler(OnClassSearchCompleted);

            queryObserver.ObjectReady += new ObjectReadyEventHandler(OnQueryResultReady);
            queryObserver.Completed += new CompletedEventHandler(OnQueryCompleted);

            // Build local namespace tree
            RefreshNamespaceTree();

            // Navigate to default namespace and class
            this.CurrentNamespacePath = @"\\.\ROOT\CIMV2";
            this.CurrentClassPath = @"\\.\ROOT\CIMV2:Win32_ComputerSystem";
        }

        #region Fields

        private ConnectionOptions conOpts = new ConnectionOptions();

        private String currentNamespace;

        private ManagementClass currentClass;

        private ManagementScope nsTreeScope;

        private TreeNode nsTreeRootNode;

        private ManagementObjectSearcher querySearcher;

        private ManagementOperationObserver nsTreeObserver = new ManagementOperationObserver();

        private ManagementOperationObserver classListObserver = new ManagementOperationObserver();

        private ManagementOperationObserver queryObserver = new ManagementOperationObserver();

        private static ObjectGetOptions nsObjGetOpts = new ObjectGetOptions();

        private static ObjectGetOptions classObjGetOpts = new ObjectGetOptions();

        private static EnumerationOptions nsTreeEnumOpts = new EnumerationOptions();

        private static EnumerationOptions classListEnumOpts = new EnumerationOptions();

        private List<ListViewItem> classListItems = new List<ListViewItem>();

        private bool updatingNavigation = false;

        private Boolean queryInProgress = false;

        private int queryResultsReturned = 0;

        private Boolean showSystemClasses = false;

        #endregion

        #region Properties

        private String CurrentNamespacePath
        {
            get
            {
                return currentNamespace;
            }

            set
            {
                if (!String.IsNullOrEmpty(currentNamespace) && currentNamespace.Equals(value))
                    return;

                currentNamespace = value;

                RefreshClassList();
            }
        }

        private String CurrentClassPath
        {
            get { return this.currentClass.Path.Path; }

            set
            {
                var path = new ManagementPath(value);
                var scope = new ManagementScope(path, this.conOpts);
                this.CurrentClass = new ManagementClass(path, classObjGetOpts);
            }
        }

        private ManagementClass CurrentClass
        {
            get { return this.currentClass; }

            set
            {
                this.currentClass = value;
                RefreshClassView();
            }
        }

        private Boolean QueryInProgress
        {
            get { return this.queryInProgress; }

            set
            {
                if (queryInProgress && value)
                    throw new InvalidOperationException("Query already in progress");

                if (queryInProgress == value)
                    return;

                this.queryInProgress = value;

                this.OnQueryInProgressChanged(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Namespace Tree

        private void RefreshNamespaceTree() {
            this.nsTreeObserver.Cancel();

            var path = new ManagementPath(@"\\localhost\ROOT");
            nsTreeScope = new ManagementScope(path, conOpts);

            var searchPath = new ManagementPath(String.Format(@"{0}:__NAMESPACE", path.Path));
            ManagementClass ns = new ManagementClass(nsTreeScope, searchPath, nsObjGetOpts);

            // TODO: Get hostname of system

            // Create classNode node
            var nodePath = String.Format(@"\\{0}\{1}",ns.Path.Server, ns.Path.NamespacePath);
            nsTreeRootNode = new TreeNode(nodePath);
            nsTreeRootNode.Tag = nodePath;
            nsTreeRootNode.ImageKey = "Home";
            nsTreeRootNode.SelectedImageKey = "Home";
            this.treeViewNamespaces.Nodes.Add(nsTreeRootNode);
            nsTreeRootNode.Expand();

            // Search for namespaces asyncronously
            ns.GetInstances(nsTreeObserver, nsTreeEnumOpts);
        }

        void OnNamespaceReady(object sender, ObjectReadyEventArgs e)
        {
            var name = (String)e.NewObject.Properties["Name"].Value;
            var path = String.Format(@"\\{0}\{1}\{2}", e.NewObject.ClassPath.Server, e.NewObject.ClassPath.NamespacePath, name);
            var parts = path.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

            // Get parent node from tree
            var node = nsTreeRootNode;
            for (int i = 2; i < parts.Length - 1; i++)
            {
                bool foundNode = false;
                foreach (TreeNode child in node.Nodes)
                {
                    if (child.Text.Equals(parts[i]))
                    {
                        foundNode = true;
                        node = child;
                        break;
                    }
                }

                if (!foundNode)
                    throw new InvalidOperationException(String.Format("TreeNode not found: \"{0}\"", parts[i]));
            }

            var nextNode = new TreeNode(name);
            nextNode.Tag = path;
            nextNode.ImageKey = "NameSpace";
            nextNode.SelectedImageKey = "NameSpaceOpen";

            appendTreeNode(node, nextNode);

            // Search for child namespaces asyncronously
            var searchPath = new ManagementPath(String.Format(@"{0}:__NAMESPACE", path));
            var searchScope = new ManagementScope(path, conOpts);
            ManagementClass ns = new ManagementClass(searchScope, searchPath, nsObjGetOpts);

            try
            {
                ns.GetInstances(nsTreeObserver, nsTreeEnumOpts);
            }
            catch (ManagementException)
            { }
        }

        private delegate void appendTreeNodeDelegate(TreeNode parent, TreeNode child);

        private void appendTreeNode(TreeNode parent, TreeNode child)
        {
            if(this.InvokeRequired) {
                this.Invoke(new appendTreeNodeDelegate(this.appendTreeNode), parent, child);
                return;
            }

            parent.Nodes.Add(child);
            
            // Expand root
            if(parent == this.nsTreeRootNode)
                parent.Expand();
        }

        #endregion

        #region Class list

        private void RefreshClassList()
        {
            classListItems.Clear();
            this.listViewClasses.Items.Clear();

            var path = new ManagementPath(this.CurrentNamespacePath);
            var scope = new ManagementScope(path, this.conOpts);

            var ns = new ManagementClass(scope, path, nsObjGetOpts);
            try
            {
                ns.GetSubclasses(classListObserver, classListEnumOpts);
            }

            catch (ManagementException e)
            {
                var msg = String.Format("An error occurred listing classes in {0}:\r\n\r\n{1}", this.CurrentNamespacePath, e.Message);
                MessageBox.Show(this, msg, "WMI Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void RefreshClassListFilter()
        {
            this.listViewClasses.Items.Clear();

            var filter = this.txtClassFilter.Text;
            var displayList = new List<ListViewItem>(classListItems.Count);
            for (int i = 0; i < classListItems.Count; i++)
            {
                var className = classListItems[i].Text;

                if (!String.IsNullOrEmpty(filter))
                {
                    if(className.ToLowerInvariant().Contains(filter.ToLowerInvariant()))
                    {
                        displayList.Add(classListItems[i]);
                    }
                }
                
                else if (this.showSystemClasses || !classListItems[i].Text.StartsWith("__"))
                {
                    displayList.Add(classListItems[i]);
                }
            }

            this.listViewClasses.Items.AddRange(displayList.ToArray());

            this.SelectClassListViewItem(this.CurrentClass.ClassPath.ClassName);
        }

        void OnClassReady(object sender, ObjectReadyEventArgs e)
        {
            var c = (ManagementClass)e.NewObject;

            var item = new ListViewItem(c.Path.ClassName, "Class");
            item.Tag = c.Path.Path;

            if (c.IsEvent())
            {
                if (c.IsEvent())
                {
                    item.ImageKey = "EventSystem";
                }
                else
                {
                    item.ImageKey = "Event";
                }
            }
            else if (c.IsPerformanceCounter())
            {
                item.ImageKey = "Performance";
            }

            else if (c.IsSystemClass())
            {
                item.ImageKey = "ClassSystem";
            }

            else if (c.IsAssociation())
            {
                item.ImageKey = "ClassAssoc";
            }

            classListItems.Add(item);
        }

        void OnClassSearchCompleted(object sender, CompletedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CompletedEventHandler(this.OnClassSearchCompleted), sender, e);
                return;
            }

            RefreshClassListFilter();
        }

        #endregion

        #region Class view

        private void SelectClassListViewItem(String className)
        {
            this.updatingNavigation = true;

            // Select this the current class in the listview
            if (this.listViewClasses.SelectedItems.Count != 1
                || !this.listViewClasses.SelectedItems[0].Text.Equals(className))
            {
                for (int i = 0; i < this.listViewClasses.Items.Count; i++)
                {
                    if (this.listViewClasses.Items[i].Text.Equals(className))
                    {
                        this.listViewClasses.Items[i].Selected = true;
                        this.listViewClasses.Items[i].Focused = true;
                        this.listViewClasses.Items[i].EnsureVisible();
                        break;
                    }
                }
            }

            this.updatingNavigation = false;
        }

        private void RefreshClassView()
        {
            this.SelectClassListViewItem(this.CurrentClass.ClassPath.ClassName);

            var c = this.CurrentClass;

            RefreshClassMembersListView(c);
            RefreshClassMembersDetailView(c);
            RefreshQueryView(c);
        }

        private void RefreshClassMembersListView(ManagementClass c)
        {
            this.treeViewClassMembers.Nodes.Clear();
            
            TreeNode classNode = new TreeNode(c.ClassPath.ClassName);
            classNode.ImageKey = classNode.SelectedImageKey = "Class";
            classNode.Tag = c;
            this.treeViewClassMembers.Nodes.Add(classNode);
            this.treeViewClassMembers.SelectedNode = classNode;

            List<TreeNode> nodes = new List<TreeNode>();

            foreach (var m in c.Methods)
            {
                var item = new TreeNode(m.Name);
                item.Tag = m;
                item.ImageKey = item.SelectedImageKey = m.IsStatic() ? "StaticMethod" : "Method";

                nodes.Add(item);
            }

            foreach (PropertyData prop in c.Properties)
            {
                // Update Tree
                TreeNode node = new TreeNode(prop.Name);
                if (prop.IsKey())
                {
                    node.ImageKey = node.SelectedImageKey = "PropertyKey";
                }
                else
                {
                    node.ImageKey = node.SelectedImageKey = "Property";
                }
                node.Tag = prop;
                nodes.Add(node);
            }

            classNode.Nodes.AddRange(nodes.ToArray());
            classNode.Expand();
        }

        private void RefreshClassMembersDetailView(object tag)
        {
            this.txtClassMemberDetail.Clear();

            ManagementClass c = this.CurrentClass;

            if (tag.GetType().IsAssignableFrom(typeof(ManagementClass)))
            {
                Font boldFont = new Font(this.txtClassMemberDetail.Font.FontFamily, this.txtClassMemberDetail.Font.Size, FontStyle.Bold);
                this.txtClassMemberDetail.Clear();
                this.txtClassMemberDetail.AppendText(@"public class ");
                this.txtClassMemberDetail.AppendText(c.Path.ClassName, boldFont);
                this.txtClassMemberDetail.AppendText("\r\n  Member of " + c.Path.NamespacePath + "\r\n\r\n");
                this.txtClassMemberDetail.AppendText("Summary:\r\n", boldFont);
                this.txtClassMemberDetail.AppendText(c.GetDescription() + "\r\n\r\n");
                this.txtClassMemberDetail.AppendText("Base Types:\r\n", boldFont);
                for (int i = 0; i < c.Derivation.Count; i++)
                {
                    this.txtClassMemberDetail.AppendText(new string(' ', i * 2));
                    this.txtClassMemberDetail.InsertLink(c.Derivation[i], c.Path.NamespacePath + ":" + c.Derivation[i]);
                    this.txtClassMemberDetail.AppendText("\r\n");
                }
            }

            else if (tag.GetType().IsAssignableFrom(typeof(PropertyData)))
            {
                // Display PropertyData properties
                PropertyData prop = (PropertyData)tag;

                Font boldFont = new Font(this.txtClassMemberDetail.Font.FontFamily, this.txtClassMemberDetail.Font.Size, FontStyle.Bold);
                string CimType = prop.GetRefType();

                this.txtClassMemberDetail.AppendText("public ");
                if (!String.IsNullOrEmpty(CimType))
                {
                    this.txtClassMemberDetail.InsertLink(CimType, c.Path.NamespacePath + ":" + CimType);
                }
                else
                {
                    this.txtClassMemberDetail.AppendText(prop.Type.ToString());
                }

                this.txtClassMemberDetail.AppendText((prop.IsArray ? "[]" : "") + " ");
                this.txtClassMemberDetail.AppendText(prop.Name + " ", boldFont);
                this.txtClassMemberDetail.AppendText("{ " + (prop.IsWritable() ? "set; " : "") + "get; }");
                this.txtClassMemberDetail.AppendText("\r\n  Member of " + c.Path.NamespacePath + ":" + c.Path.ClassName + "\r\n\r\n");
                if (!String.IsNullOrEmpty(prop.GetDescription()))
                {
                    this.txtClassMemberDetail.AppendText("Summary:\n", boldFont);
                    this.txtClassMemberDetail.AppendText(prop.GetDescription() + "\r\n\r\n");
                }

                /*
                Dictionary<string, string> map = PropertyDataHelper.GetValueMap(prop);
                if (map.Count > 0)
                {
                    this.txtClassMemberDetail.AppendText("Values:\r\n", boldFont);
                    foreach (string mapping in map.Keys)
                    {
                        if (mapping == map[mapping])
                        {
                            this.txtClassMemberDetail.AppendText("    " + mapping + "\r\n");
                        }
                        else
                        {
                            this.txtClassMemberDetail.AppendText("    " + mapping + " - " + map[mapping] + "\r\n");
                        }
                    }
                    this.txtClassMemberDetail.AppendText("\r\n");
                }
                */
                    
                if (prop.Origin != "" && prop.Origin != c.Path.ClassName)
                {
                    this.txtClassMemberDetail.AppendText("Origin:\n", boldFont);
                    this.txtClassMemberDetail.InsertLink(prop.Origin, c.Path.NamespacePath + ":" + prop.Origin);
                    this.txtClassMemberDetail.AppendText("\r\n\r\n");
                }
            }

            else if (tag.GetType().IsAssignableFrom(typeof(MethodData)))
            {
                // Display MethodData properties
                MethodData m = (MethodData)tag;
                PropertyData ret = m.GetReturnValueParameter();
                string mType = (ret != null) ? ret.Type.ToString() : "void";

                Font boldFont = new Font(this.txtClassMemberDetail.Font.FontFamily, this.txtClassMemberDetail.Font.Size, FontStyle.Bold);
                Font emFont = new Font(this.txtClassMemberDetail.Font.FontFamily, this.txtClassMemberDetail.Font.Size, FontStyle.Italic);

                this.txtClassMemberDetail.AppendText("public ");
                if (m.IsStatic())
                    this.txtClassMemberDetail.AppendText("static ");
                this.txtClassMemberDetail.AppendText(mType + " " + m.Name, boldFont);
                this.txtClassMemberDetail.AppendText("(");
                if (m.InParameters != null)
                {
                    int count = 0;
                    foreach (PropertyData prop in m.InParameters.Properties)
                    {
                        count++;
                        this.txtClassMemberDetail.AppendText(prop.Type.ToString(), boldFont);
                        this.txtClassMemberDetail.AppendText(" " + prop.Name);
                        if (count < m.InParameters.Properties.Count)
                            this.txtClassMemberDetail.AppendText(", ");
                    }
                }

                this.txtClassMemberDetail.AppendText(")\r\n  Member of " + c.Path.NamespacePath + ":" + c.Path.ClassName + "\r\n\r\n");

                // Show method origin if inherited
                if (m.Origin != "" && m.Origin != c.Path.ClassName)
                {
                    this.txtClassMemberDetail.AppendText("Origin:\n", boldFont);
                    this.txtClassMemberDetail.InsertLink(m.Origin, c.Path.NamespacePath + ":" + m.Origin);
                    this.txtClassMemberDetail.AppendText("\r\n\r\n");
                }

                // Show summary
                   
                if (!String.IsNullOrEmpty(m.GetDescription()))
                {
                    this.txtClassMemberDetail.AppendText("Summary:\n", boldFont);
                    this.txtClassMemberDetail.AppendText(m.GetDescription() + "\r\n\r\n");
                }

                // Show Parameters
                if (m.InParameters != null || m.OutParameters != null)
                {
                    this.txtClassMemberDetail.AppendText("Parameters:\n", boldFont);
                    if (m.InParameters != null)
                    {
                        foreach (PropertyData prop in m.InParameters.Properties)
                        {
                            this.txtClassMemberDetail.AppendText(prop.Name, emFont);
                            this.txtClassMemberDetail.AppendText(": " + prop.GetDescription() + "\r\n\r\n");
                        }
                    }

                    if (m.OutParameters != null)
                    {
                        foreach (PropertyData prop in m.OutParameters.Properties)
                        {
                            if (prop.Name != "ReturnValue")
                            {
                                this.txtClassMemberDetail.AppendText(prop.Name, emFont);
                                this.txtClassMemberDetail.AppendText(": " + prop.GetDescription() + "\r\n\r\n");
                            }
                        }
                    }
                }
            }
        }

        private void RefreshQueryView(ManagementClass c)
        {
            var query = String.Format("SELECT * FROM {0}", c.ClassPath.ClassName);
            this.txtQuery.Text = query;
        }

        #endregion

        #region Queries

        private void ExecuteQuery(String query)
        {
            // Reset UI
            this.QueryInProgress = true;

            // Execute
            var scope = new ManagementScope(this.CurrentNamespacePath, this.conOpts);
            this.querySearcher = new ManagementObjectSearcher(scope, new ObjectQuery(query));
            this.querySearcher.Get(this.queryObserver);
        }

        private void OnQueryCompleted(object sender, CompletedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CompletedEventHandler(this.OnQueryCompleted), sender, e);
                return;
            }

            this.queryInProgress = false;
        }

        private void OnQueryResultReady(object sender, ObjectReadyEventArgs e)
        {
            if (this.Disposing)
                return;

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ObjectReadyEventHandler(this.OnQueryResultReady), sender, e);
                return;
            }

            // Init the datagrid if required
            if (0 == this.queryResultsReturned)
                this.InitQueryResults(e.NewObject);

            this.queryResultsReturned++;

            // Build an array of values
            var i = 0;
            var values = new String[e.NewObject.Properties.Count + 3];

            values[i++] = this.queryResultsReturned.ToString();
            values[i++] = null;

            foreach (PropertyData p in e.NewObject.Properties)
            {
                values[i++] = p.Value == null ? String.Empty : p.Value.ToString();
            }

            this.dataGridView1.Rows.Add(values);
        }

        private void OnQueryInProgressChanged(object sender, EventArgs e)
        {
            this.btnExecuteQuery.Visible = !this.QueryInProgress;
            this.btnCancelQuery.Visible = this.QueryInProgress;

            this.txtQuery.ReadOnly = this.QueryInProgress;
            
            this.tabQuery.Cursor = this.QueryInProgress ? Cursors.WaitCursor : Cursors.Default;

            if (this.QueryInProgress)
            {
                this.queryResultsReturned = 0;
                this.dataGridView1.Rows.Clear();
                this.dataGridView1.Columns.Clear();
            }

        }

        private void InitQueryResults(ManagementBaseObject result)
        {
            // Create count colIndex
            DataGridViewTextBoxColumn colIndex = new DataGridViewTextBoxColumn();
            colIndex.Name = colIndex.HeaderText = "#";
            colIndex.DefaultCellStyle.BackColor = SystemColors.ControlLight;
            colIndex.DefaultCellStyle.ForeColor = SystemColors.ControlDark;
            colIndex.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // Create result inspector colIndex
            DataGridViewImageColumn colInspector = new DataGridViewImageColumn();
            colInspector.Image = this.ImageList1.Images["Property"];
            colInspector.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            this.dataGridView1.Columns.AddRange(colIndex, colInspector);

            // Add property columns
            foreach (PropertyData p in result.Properties)
            {
                DataGridViewColumn colProperty;

                // Create hyperlink columns for objects
                if (p.Type == CimType.Object || p.Type == CimType.Reference)
                {
                    colProperty = new DataGridViewLinkColumn();
                    DataGridViewLinkColumn link = (DataGridViewLinkColumn)colProperty;
                    link.LinkBehavior = LinkBehavior.HoverUnderline;
                    link.VisitedLinkColor = link.LinkColor;
                    link.ActiveLinkColor = link.LinkColor;
                }

                else
                {
                    colProperty = new DataGridViewTextBoxColumn();
                }

                colProperty.Name = colProperty.HeaderText = p.Name;
                if (p.IsKey())
                {
                    colProperty.DefaultCellStyle.BackColor = SystemColors.Info;
                    colProperty.DefaultCellStyle.ForeColor = SystemColors.InfoText;
                }

                if (this.CurrentClass.IsAssociation())
                    colProperty.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                else
                    colProperty.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                this.dataGridView1.Columns.Add(colProperty);
            }
        }

        #endregion

        #region UI Management

        private void frmMain_Shown(object sender, EventArgs e)
        {
            resetClassColumnHeader();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            resetClassColumnHeader();
        }

        private void resetClassColumnHeader()
        {
            listViewClasses.Columns[0].Width = listViewClasses.ClientRectangle.Width;
        }

        private void treeViewNamespaces_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.updatingNavigation)
                return;

            this.Cursor = Cursors.WaitCursor;
            this.CurrentNamespacePath = (String)e.Node.Tag;
            this.Cursor = Cursors.Default;
        }

        private void listViewClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.updatingNavigation || this.listViewClasses.SelectedItems.Count != 1)
                return;

            this.Cursor = Cursors.WaitCursor;
            this.CurrentClassPath = (String) this.listViewClasses.SelectedItems[0].Tag;
            this.Cursor = Cursors.Default;
        }

        private void treeViewClassMembers_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.updatingNavigation)
                return;

            if (null != treeViewClassMembers.SelectedNode && null != treeViewClassMembers.SelectedNode.Tag)
            {
                this.Cursor = Cursors.WaitCursor;
                this.RefreshClassMembersDetailView(treeViewClassMembers.SelectedNode.Tag);
                this.Cursor = Cursors.Default;
            }
        }

        private void txtClassMemberDetail_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            // RTFex uses # to separate the link text from its href.
            string[] link = e.LinkText.Split('#');
            string target = link[link.Length - 1];
            this.CurrentClassPath = target;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void executeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ExecuteQuery(this.txtQuery.Text);
        }

        private void btnCancelQuery_Click(object sender, EventArgs e)
        {
            this.queryObserver.Cancel();
            this.querySearcher.Dispose();
        }

        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 116)
                this.ExecuteQuery(this.txtQuery.Text);
        }

        private void txtClassFilter_KeyUp(object sender, KeyEventArgs e)
        {
            this.RefreshClassListFilter();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.RefreshClassListFilter();
        }

        private void btnToggleSystemClasses_Click(object sender, EventArgs e)
        {
            this.btnToggleSystemClasses.Checked = !this.btnToggleSystemClasses.Checked;
            this.showSystemClasses = this.btnToggleSystemClasses.Checked;
            this.RefreshClassListFilter();
        }

        #endregion
    }
}
