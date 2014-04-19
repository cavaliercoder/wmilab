namespace WMILab
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Management;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using WMILab.Localization;
    using System.Diagnostics;

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

            // Build local namespace tree
            RefreshNamespaceTree();

            // Navigate to default namespace and class
            this.CurrentNamespacePath = @"\\.\ROOT\CIMV2";
            this.CurrentClassPath = @"\\.\ROOT\CIMV2:Win32_ComputerSystem";
        }

        #region Fields

        private ConnectionOptions conOpts = new ConnectionOptions();

        private String currentNamespacePath;

        private ManagementScope currentNamespaceScope;

        private ManagementClass currentClass;

        private ManagementScope nsTreeScope;

        private TreeNode nsTreeRootNode;

        private ManagementOperationObserver nsTreeObserver = new ManagementOperationObserver();

        private ManagementOperationObserver classListObserver = new ManagementOperationObserver();

        private static ObjectGetOptions nsObjGetOpts = new ObjectGetOptions();

        private static ObjectGetOptions classObjGetOpts = new ObjectGetOptions();

        private static EnumerationOptions nsTreeEnumOpts = new EnumerationOptions();

        private static EnumerationOptions classListEnumOpts = new EnumerationOptions();

        private List<ListViewItem> classListItems = new List<ListViewItem>();

        private bool updatingNavigation = false;

        private Boolean showSystemClasses = false;

        private ManagementQueryBroker queryBroker;

        #endregion

        #region Properties

        private String CurrentNamespacePath
        {
            get
            {
                return currentNamespacePath;
            }

            set
            {
                if (!String.IsNullOrEmpty(currentNamespacePath) && currentNamespacePath.Equals(value))
                    return;

                try
                {
                    this.currentNamespaceScope = new ManagementScope(value, this.conOpts);
                    this.CurrentNamespaceScope.Connect();

                    currentNamespacePath = value;

                    RefreshClassList();
                }

                catch (ManagementException e)
                {
                    this.Log(LogLevel.Critical, String.Format("Error connecting to \"{0}\": {1}", value, e.Message));
                }
            }
        }

        private ManagementScope CurrentNamespaceScope
        {
            get { return this.currentNamespaceScope; }
        }

        private String CurrentClassPath
        {
            get { return this.currentClass.Path.Path; }

            set
            {
                var path = new ManagementPath(value);

                try
                {
                    this.CurrentClass = new ManagementClass(this.CurrentNamespaceScope, path, classObjGetOpts);
                    this.Log(LogLevel.Information, String.Format("Loaded class: {0}", value));
                }
                catch (ManagementException e)
                {
                    this.Log(LogLevel.Critical, String.Format("Unable to load class: {0}", value));
                }
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

            this.Log(LogLevel.Information, String.Format("Loading class for path: {0}", this.currentNamespacePath));

            var path = new ManagementPath(this.CurrentNamespacePath);
            var ns = new ManagementClass(this.CurrentNamespaceScope, path, nsObjGetOpts);
            try
            {
                ns.GetSubclasses(classListObserver, classListEnumOpts);
                this.Log(LogLevel.Information, "Finished loading classes.");
            }

            catch (ManagementException e)
            {
                var msg = String.Format("An error occurred listing classes in {0}:\r\n\r\n{1}", this.CurrentNamespacePath, e.Message);
                this.Log(LogLevel.Critical, msg);
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

                var map = prop.GetValueMap();
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
            String query;
            if (c.IsEvent())
            {
                query = String.Format("SELECT * FROM {0} WITHIN 30", c.ClassPath.ClassName);
            }

            else
            {
                query = String.Format("SELECT * FROM {0}", c.ClassPath.ClassName);
            }

            this.txtQuery.Text = query;
        }

        #endregion

        #region Queries

        /// <summary>
        /// Prepares the Query UI and executes a WQL query via a new ManagementQueryBroker.
        /// </summary>
        /// <param name="query">The WQL query to execute.</param>
        private void ExecuteQuery(String query)
        {
            // Reset UI
            this.ResetQueryResults();
            this.ToggleQueryUI(true);

            this.Log(LogLevel.Information, String.Format("Executing query: {0}", query));

            try
            {
                // Execute
                var scope = new ManagementScope(this.CurrentNamespacePath, this.conOpts);
                this.queryBroker = new ManagementQueryBroker(this.txtQuery.Text, this.CurrentNamespaceScope);
                this.queryBroker.ObjectReady += new BrokerObjectReadyEventHandler(this.OnQueryResultReady);
                this.queryBroker.Completed += new BrokerCompletedEventHandler(this.OnQueryCompleted);

                this.queryBroker.ExecuteAsync();
            }

            catch (Exception e)
            {
                bool hasCode = false;
                UInt32 code = 0;

                if (e.GetType() == typeof(COMException))
                {
                    hasCode = true;
                    code = (UInt32) ((COMException)e).ErrorCode;
                }

                else if(e.GetType() == typeof(ManagementException))
                {
                    hasCode = true;
                    code = (UInt32) ((ManagementException)e).ErrorCode;
                }

                if (hasCode && Enum.IsDefined(typeof(ManagementError), code))
                {
                    var constant = ((ManagementError)code).ToString();
                    this.Log(LogLevel.Critical, String.Format("Query execution failed with {0:G} (0x{0:X}) {1}", code, constant));
                    
                    var description = ErrorCodes.ResourceManager.GetString(constant);
                    if (!String.IsNullOrEmpty(description))
                        this.Log(LogLevel.Information, description);
                }

                else
                {
                    this.Log(LogLevel.Critical, String.Format("Query execution failed with: {0}", e.Message));
                }

                // Generic exceptions may not trigger a 'Completed' event
                this.ToggleQueryUI(false);
            }
        }

        private void OnQueryCompleted(object sender, BrokerCompletedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new BrokerCompletedEventHandler(this.OnQueryCompleted), sender, e);
                return;
            }

            this.ToggleQueryUI(false);

            if (e.Success)
            {
                this.Log(LogLevel.Information, String.Format("Query completed in {0} with {1} results.",this.queryBroker.ExecutionTime, this.queryBroker.ResultCount));
            }

            else
            {
                var code = (UInt32)e.Status;
                if(Enum.IsDefined(typeof(ManagementError), code))
                {
                    var constant = ((ManagementError)code).ToString();
                    this.Log(LogLevel.Critical, String.Format("Query failed after {0} with {1:G} (0x{1:X}) {2}", this.queryBroker.ExecutionTime, code, constant));

                    var description = ErrorCodes.ResourceManager.GetString(constant);
                    if (!String.IsNullOrEmpty(description))
                        this.Log(LogLevel.Information, description);
                }

                else
                {
                    this.Log(LogLevel.Critical, String.Format("Query failed after {0} with {1:G} (0x{1:X}).", this.queryBroker.ExecutionTime, code));
                }
            }
        }

        private void OnQueryResultReady(object sender, BrokerObjectReadyEventArgs e)
        {
            if (this.Disposing)
                return;

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new BrokerObjectReadyEventHandler(this.OnQueryResultReady), sender, e);
                return;
            }

            // Init the datagrid if required
            if (1 == this.queryBroker.ResultCount)
                this.InitQueryResults(e.NewObject);

            // Build an array of values
            var i = 0;
            var values = new String[e.NewObject.Properties.Count + 3];

            values[i++] = e.Index.ToString();
            values[i++] = null;

            if (WqlQueryType.Select == this.queryBroker.QueryType)
            {
                foreach (PropertyData p in e.NewObject.Properties)
                {
                    // Should we translate a value map?
                    if (this.queryBroker.ResultClassValueMaps.ContainsKey(p.Name))
                    {
                        var map = this.queryBroker.ResultClassValueMaps[p.Name];
                        values[i++] = p.GetValueAsString(map);
                    }

                    else
                    {

                        values[i++] = p.GetValueAsString();
                    }
                }
            }

            else
            {
                values[i++] = e.NewObject.GetRelativePath();
            }

            this.gridQueryResults.Rows.Add(values);
        }

        /// <summary>
        /// Reset the query UI by removing previous results and resetting counters.
        /// </summary>
        private void ResetQueryResults()
        {
            this.gridQueryResults.Rows.Clear();
            this.gridQueryResults.Columns.Clear();
        }

        /// <summary>
        /// Initializes the Query UI to receive new results for the specified ManagementObject type.
        /// </summary>
        /// <param name="result">The first result returned by a query.</param>
        private void InitQueryResults(ManagementBaseObject result)
        {
            var c = this.queryBroker.ResultClass;

            // Configure result context menu
            this.btnGetAssociatorsOf.Visible =
                this.btnGetReferencesOf.Visible =
                this.btnResultPropertiesSeparater.Visible =
                this.queryBroker.QueryType == WqlQueryType.Select;

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

            this.gridQueryResults.Columns.AddRange(colIndex, colInspector);

            if (WqlQueryType.Select == this.queryBroker.QueryType)
            {
                // Add property columns
                foreach (PropertyData p in c.Properties)
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

                    if (c.IsAssociation())
                        colProperty.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    else
                        colProperty.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;

                    this.gridQueryResults.Columns.Add(colProperty);
                }
            }

            else
            {
                DataGridViewLinkColumn col = new DataGridViewLinkColumn();
                col.LinkBehavior = LinkBehavior.HoverUnderline;
                col.VisitedLinkColor = col.LinkColor;
                col.ActiveLinkColor = col.LinkColor;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                switch (this.queryBroker.QueryType)
                {
                    case WqlQueryType.AssociatorsOf:
                        col.Name = "Association";
                        break;

                    case WqlQueryType.ReferencesOf:
                        col.Name = "Reference";
                        break;
                }

                this.gridQueryResults.Columns.Add(col);
            }
        }

        /// <summary>
        /// Toggles the query UI to feedback query status to the user.
        /// </summary>
        /// <param name="inProgress">Determines the state of UI components.</param>
        private void ToggleQueryUI(Boolean inProgress)
        {
            this.btnExecuteQuery.Visible = !inProgress;
            this.btnCancelQuery.Visible = inProgress;

            this.txtQuery.ReadOnly = inProgress;

            this.tabQuery.Cursor = inProgress ? Cursors.WaitCursor : Cursors.Default;
        }

        private void ShowInspector(ManagementBaseObject managementObject, Point location)
        {
            this.gridQueryResults.Cursor = Cursors.WaitCursor;

            // Create form
            ManagementObjectInspectorForm popup = new ManagementObjectInspectorForm();
            popup.Scope = this.CurrentNamespaceScope;
            popup.ValueMaps = this.queryBroker.ResultClassValueMaps;
            popup.ManagementObject = managementObject;

            // Offset location to screen bounds
            Rectangle bounds = Screen.FromControl(this).WorkingArea;
            location.X = Math.Min(location.X, bounds.Width - popup.Width);
            location.Y = Math.Min(location.Y, bounds.Height - popup.Height);
            popup.Location = location;

            // Show
            this.gridQueryResults.Cursor = Cursors.Default;
            popup.Show(this);
        }

        private void ShowInspectorForLinkedObject(DataGridViewCellEventArgs e)
        {
            Boolean selectMode = this.queryBroker.QueryType == WqlQueryType.Select;

            // A hyperlinked object was click. Get target object
            ManagementBaseObject mObject = null;
            if (selectMode)
            {
                string propertyName = this.gridQueryResults.Columns[e.ColumnIndex].HeaderText;
                if (this.queryBroker.ResultClass.Properties[propertyName].Type == CimType.Reference)
                {
                    // Link was a reference to an instance. Fetch the instance
                    string objectPath = this.queryBroker.Results[e.RowIndex].Properties[propertyName].Value.ToString();

                    var scope = new ManagementScope(this.CurrentNamespacePath, this.conOpts);
                    mObject = new ManagementObject(scope, new ManagementPath(objectPath), new ObjectGetOptions());
                }

                else
                {
                    // Link was to an object instance
                    mObject = (ManagementBaseObject) this.queryBroker.Results[e.RowIndex].Properties[propertyName].Value;
                }
            }

            else
            {
                // Select the whole object for 'Assoc' and 'Ref' queries
                mObject = this.queryBroker.Results[e.RowIndex];
            }

            // Get cell location
            Rectangle cell = this.gridQueryResults.GetCellDisplayRectangle(selectMode ? e.ColumnIndex : 1, e.RowIndex, true);
            Point location = this.gridQueryResults.PointToScreen(new Point(cell.Right, cell.Bottom));

            // Show object inspector
            this.ShowInspector(mObject, location);
        }

        #endregion

        #region Logging

        private void LogManagementException(ManagementException e)
        {
            UInt32 code = (UInt32) e.ErrorCode;
            this.Log(LogLevel.Critical, String.Format("WMI Exception {0:G} (0x{0:X})", code));
        }

        private void LogComException(COMException e)
        {
            UInt32 code = (UInt32) e.ErrorCode;

            if (Enum.IsDefined(typeof(ManagementError), code))
            {
                // Log an constant with known constant info
                var constant = ((ManagementError)code).ToString();
                this.Log(LogLevel.Critical, String.Format("COM Exception {0:G} (0x{0:X}) {1}", code, constant));

                // Attempt to get an constant description
                var description = ErrorCodes.ResourceManager.GetString(constant);
                if (!String.IsNullOrEmpty(description))
                    this.Log(LogLevel.Information, description);
            }

            else
            {
                this.Log(LogLevel.Critical, String.Format("COM Exception {0:G} (0x{0:X})", code));
            }
        }

        private void LogException(Exception e)
        {
            this.Log(LogLevel.Critical, e.Message);
        }

        private void Log(LogLevel level, String message)
        {
            var item = new ListViewItem(message);
            item.ImageKey = level.ToString();

            this.listViewLog.Items.Add(item);
            item.EnsureVisible();
        }

        #endregion

        #region UI Event Handlers

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
            this.queryBroker.Cancel();
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

        private void gridQueryResults_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1) return;

            if (this.gridQueryResults.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell))
                this.gridQueryResults.Cursor = Cursors.Hand;
            else
                this.gridQueryResults.Cursor = Cursors.Default;
        }

        private void gridQueryResults_CellClicked(object sender, DataGridViewCellEventArgs e)
        {
            Type cellType = this.gridQueryResults.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType();

            // Handle 'properties' button clicks
            if (e.ColumnIndex == 1)
            {
                this.gridQueryResults_CellDoubleClicked(sender, e);
            }
        }

        private void gridQueryResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Type cellType = this.gridQueryResults.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType();

            // Handle hyperlink clicks
            if (cellType == typeof(DataGridViewLinkCell))
            {
                ShowInspectorForLinkedObject(e);
            }
        }

        private void gridQueryResults_CellDoubleClicked(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            // Highlight row
            this.gridQueryResults.ClearSelection();
            this.gridQueryResults.Rows[e.RowIndex].Selected = true;

            // Show inspector in place
            Rectangle cell = this.gridQueryResults.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
            Point location = this.gridQueryResults.PointToScreen(new Point(cell.Right, cell.Bottom));

            this.ShowInspector(this.queryBroker.Results[e.RowIndex], location);
        }

        private void gridQueryResults_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.RowIndex >= 0)
                {
                    // Select a row on right click
                    this.gridQueryResults.Rows[e.RowIndex].Selected = true;

                    // Get bounds for result context menu
                    var loc = this.gridQueryResults.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    loc.X += e.X;
                    loc.Y += e.Y;

                    // Show context menu
                    this.menuQueryRow.Show(this.gridQueryResults.PointToScreen(loc.Location));
                }
            }
        }

        private void btnResultProperies_Click(object sender, EventArgs e)
        {
            if (this.gridQueryResults.SelectedRows.Count == 1)
            {
                // Find location of top right of the 'property' button for the selected row
                var index = this.gridQueryResults.SelectedRows[0].Index;
                var rect = this.gridQueryResults.GetCellDisplayRectangle(1, index, true);
                var loc = this.gridQueryResults.PointToScreen(new Point(rect.Right, rect.Top));

                // Display object inspector
                this.ShowInspector(this.queryBroker.Results[index], loc);
            }
        }

        private void btnGetAssociatorsOf_Click(object sender, EventArgs e)
        {
            if (this.gridQueryResults.SelectedRows.Count == 1)
            {
                // Execute 'Associators Of' query for selected result
                var obj = this.queryBroker.Results[this.gridQueryResults.SelectedRows[0].Index];
                var query = String.Format("ASSOCIATORS OF {{{0}}}", obj.GetRelativePath());
                this.txtQuery.Text = query;
                this.ExecuteQuery(query);
            }
        }

        private void btnGetReferencesOf_Click(object sender, EventArgs e)
        {
            if (this.gridQueryResults.SelectedRows.Count == 1)
            {
                // Execute 'Associators Of' query for selected result
                var obj = this.queryBroker.Results[this.gridQueryResults.SelectedRows[0].Index];
                var query = String.Format("REFERENCES OF {{{0}}}", obj.GetRelativePath());
                this.txtQuery.Text = query;
                this.ExecuteQuery(query);
            }
        }

        #endregion
    }

    internal enum LogLevel
    {
        Information,
        Warning,
        Critical
    }
}
