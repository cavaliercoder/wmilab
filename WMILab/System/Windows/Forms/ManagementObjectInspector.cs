/*
 * Copyright (c) 2014 Ryan Armstrong (www.cavaliercoder.com)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to
 * deal in the Software without restriction, including without limitation the
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
 * sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * The Software shall be used for Good, not Evil.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */
namespace System.Windows.Forms
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Management;
    using System.Media;

    public partial class ManagementObjectInspector : UserControl
    {
        private const int MAX_ARRAY_MEMBERS = 16;

        #region Constructors

        public ManagementObjectInspector()
        {
            InitializeComponent();
        }

        #endregion

        #region Fields

        private TreeGridNode SystemPropertiesGroup;

        private ManagementBaseObject managementObject;

        private ManagementBaseObject selectedObject;

        private ManagementClass managementClass;

        private PropertyDataValueMapCollection valueMaps;

        private PropertyData selectedProperty;

        private Dictionary<string, PropertyData> propertyMap = new Dictionary<string, PropertyData>();

        private Dictionary<string, ManagementBaseObject> objectMap = new Dictionary<string, ManagementBaseObject>();

        private TreeGridNode PropertiesGroup;

        private TreeGridNode QualifiersGroup;

        private bool readOnly = true;

        private bool showProperties = true;

        private bool showSystemProperties;

        private bool showQualifiers = true;

        private bool showMappedValues = true;

        private bool autoExpandProperties = true;

        private bool autoExpandSystemProperties = true;

        private bool autoExpandQualifiers = true;

        private string propertiesHeaderText;

        private string systemPropertiesHeaderText;

        private string qualifiersHeaderText;

        private object preEditValue;

        #endregion

        #region Properties

        [Browsable(false)]
        public ManagementScope Scope
        {
            get;
            set;
        }

        [Browsable(false)]
        public ManagementBaseObject ManagementObject
        {
            get
            {
                return this.managementObject;
            }

            set
            {
                this.managementObject = value;
                this.RefreshView();
            }
        }

        [Browsable(false)]
        public ManagementClass ManagementClass
        {
            get { return this.managementClass; }
            set
            {
                this.managementClass = value;
                this.valueMaps = value.GetValueMaps();
                this.RefreshView();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagementBaseObject SelectedObject
        {
            get { return this.selectedObject; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PropertyData SelectedProperty
        {
            get { return this.selectedProperty; }
        }

        [Browsable(true), Category("Behavior"), DefaultValue(true), Description("Prevents changes to applied to the specified ManagementObject.")]
        public bool ReadOnly
        {
            get { return this.readOnly; }
            set
            {
                this.readOnly = value;
                this.RefreshView();
            }
        }

        [Browsable(true), Category("Appearance"), DefaultValue(true), Description("List object properties.")]
        public bool ShowProperties
        {
            get { return this.showProperties; }
            set
            {
                this.showProperties = value;
                this.RefreshView();
            }
        }

        [Browsable(true), Category("Appearance"), DefaultValue(false), Description("List object System Properties.")]
        public bool ShowSystemProperties
        {
            get { return this.showSystemProperties; }
            set
            {
                this.showSystemProperties = value;
                this.RefreshView();
            }
        }

        [Browsable(true), Category("Appearance"), DefaultValue(false), Description("Show object qualifiers.")]
        public bool ShowQualifiers
        {
            get { return this.showQualifiers; }
            set
            {
                this.showQualifiers = value;
                this.RefreshView();
            }
        }

        [Browsable(true), Category("Behavior"), DefaultValue(true), Description("Show mapped values instead of raw values.")]
        public bool ShowMappedValues
        {
            get { return this.showMappedValues; }
            set
            {
                this.showMappedValues = value;
                RefreshView();
            }
        }

        [Browsable(true), Category("Behavior"), DefaultValue(true), Description("Automatically expand all Property rows when the view is refreshed.")]
        public bool AutoExpandProperties
        {
            get { return this.autoExpandProperties; }
            set { this.autoExpandProperties = value; }
        }

        [Browsable(true), Category("Behavior"), DefaultValue(true), Description("Automatically expand all System Property rows when the view is refreshed.")]
        public bool AutoExpandSystemProperties
        {
            get { return this.autoExpandSystemProperties; }
            set { this.autoExpandSystemProperties = value; }
        }

        [Browsable(true), Category("Behavior"), DefaultValue(true), Description("Automatically expand all Qualifier rows when the view is refreshed.")]
        public bool AutoExpandQualifiers
        {
            get { return this.autoExpandQualifiers; }
            set { this.autoExpandQualifiers = value; }
        }

        [Browsable(true), Category("Appearance"), DefaultValue(""), Description("Header text for the Properties group.")]
        public string PropertiesHeaderText
        {
            get { return String.IsNullOrEmpty(this.propertiesHeaderText) ? "Class Properties" : this.propertiesHeaderText; }
            set
            {
                this.propertiesHeaderText = value;
                this.RefreshView();
            }
        }

        [Browsable(true), Category("Appearance"), DefaultValue(""), Description("Header text for the System Properties group.")]
        public string SystemPropertiesHeaderText
        {
            get { return String.IsNullOrEmpty(this.systemPropertiesHeaderText) ? "System Properties" : this.systemPropertiesHeaderText; }
            set
            {
                this.systemPropertiesHeaderText = value;
                this.RefreshView();
            }
        }

        [Browsable(true), Category("Appearance"), DefaultValue(""), Description("Header text for the Qualifiers group.")]
        public string QualifiersHeaderText
        {
            get { return String.IsNullOrEmpty(this.qualifiersHeaderText) ? "Qualifiers" : this.qualifiersHeaderText; }
            set
            {
                this.qualifiersHeaderText = value;
                this.RefreshView();
            }
        }

        #endregion

        #region Methods

        protected virtual void RefreshView()
        {
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.ReadOnly = this.ReadOnly;

            if (this.managementObject == null) return;

            // Create group headers styling
            DataGridViewCellStyle groupCellStyle = new DataGridViewCellStyle();
            groupCellStyle.BackColor =
                groupCellStyle.SelectionBackColor =
                SystemColors.ControlLight;

            groupCellStyle.ForeColor =
                 groupCellStyle.SelectionForeColor =
                 SystemColors.ControlDark;

            Font f = this.dataGridView1.DefaultCellStyle.Font;
            groupCellStyle.Font = new Font(f.FontFamily, f.Size, FontStyle.Bold);

            // Create group header nodes
            if (this.ShowProperties)
            {
                this.PropertiesGroup = this.dataGridView1.Nodes.Add(this.PropertiesHeaderText);
                this.PropertiesGroup.DefaultCellStyle = groupCellStyle;
                this.PropertiesGroup.Tag = this.ManagementObject;

                foreach (PropertyData p in this.ManagementObject.Properties)
                {
                    this.AddNode(this.ManagementObject, p, this.PropertiesGroup.Nodes);
                }

                if (this.AutoExpandProperties)
                    this.dataGridView1.ExpandNode(this.PropertiesGroup);
            }

            if (this.ShowSystemProperties)
            {
                this.SystemPropertiesGroup = this.dataGridView1.Nodes.Add(this.SystemPropertiesHeaderText);
                this.SystemPropertiesGroup.DefaultCellStyle = groupCellStyle;
                this.PropertiesGroup.Tag = this.ManagementObject;
                
                foreach (PropertyData p in this.ManagementObject.SystemProperties)
                {
                    this.AddNode(this.ManagementObject, p, this.SystemPropertiesGroup.Nodes);
                }

                if (this.AutoExpandSystemProperties)
                    this.dataGridView1.ExpandNode(this.SystemPropertiesGroup);
            }

            if (this.ShowQualifiers)
            {
                this.QualifiersGroup = this.dataGridView1.Nodes.Add(this.QualifiersHeaderText);
                this.QualifiersGroup.DefaultCellStyle = groupCellStyle;
                this.PropertiesGroup.Tag = this.ManagementObject;

                foreach (QualifierData q in this.ManagementObject.Qualifiers)
                {
                    this.QualifiersGroup.Nodes.Add(q.Name, q.Value.ToString());
                }

                if (this.AutoExpandQualifiers)
                    this.dataGridView1.ExpandNode(this.QualifiersGroup);
            }
        }

        private TreeGridNode AddNode(ManagementBaseObject o, PropertyData p, TreeGridNodeCollection nodes)
        {
            TreeGridNode node = nodes.Add(p.Name, p.GetValueAsString(this.valueMaps));
            string guid = GetGUID();
            node.Tag = guid;
            this.objectMap.Add(guid, o);
            this.propertyMap.Add(guid, p);

            // Get property description
            var description = String.Empty;
            if (this.ManagementClass != null && this.managementClass.HasProperty(p.Name))
            {
                description = this.ManagementClass.Properties[p.Name].GetDescription();
                if (!String.IsNullOrEmpty(description))
                    description = "\r\n" + description;
            }

            // Set tooltip
            node.Cells[0].ToolTipText =
                String.Format(
                "{0} {1}.{2}{3}",
                p.Type.ToString() + (p.IsArray ? "[]" : String.Empty),
                this.ManagementObject.ClassPath.ClassName,
                p.Name,
                description
                );
            
            // Highlight key columns
            if (p.IsKey())
            {
                // Apply styles
                Font f = node._grid.DefaultCellStyle.Font;
                node.Cells[0].Style.Font = new Font(f.FontFamily, f.Size, FontStyle.Bold);
            }

            // Expand arrays
            if (p.Value != null && p.IsArray)
            {
                var values = p.GetValueAsStringArray(this.ShowMappedValues ? this.valueMaps : null);

                int i = 0;
                bool addValues = true;
                foreach (object value in values)
                {
                    if (i >= MAX_ARRAY_MEMBERS)
                    {
                        addValues = false;
                    }
                    else
                    {
                        // Keep add values or just count them?
                        if (addValues)
                        {
                            TreeGridNode child = node.Nodes.Add(String.Format("[{0}]", i), value.ToString());
                            child.Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                            child.Cells[0].Style.ForeColor = SystemColors.GrayText;
                        }

                        i++;
                    }
                }

                // Add note if results were truncated
                if (!addValues)
                {
                    TreeGridNode truncNode = node.Nodes.Add(String.Format("[...{0}]", i), "Results were truncated.");
                    truncNode.Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    truncNode.Cells[0].Style.ForeColor = SystemColors.GrayText;
                }

                node.Cells[1].Value = String.Format("{0} [{1}]", p.Type, i);
            }

            // Expand Objects
            if (p.Type == CimType.Reference || p.Type == CimType.Object)
            {
                if (p.Value != null)
                {
                    // TODO: What about object arrays?
                    ManagementBaseObject refObject;
                    if (p.Type == CimType.Reference && null != this.Scope)
                    {
                        refObject = new ManagementObject(this.Scope, new ManagementPath((String)p.Value), new ObjectGetOptions());
                    }

                    else
                    {
                        refObject = (ManagementBaseObject) p.Value;
                    }

                    node.Cells[1].Value = refObject.GetRelativePath();

                    foreach (PropertyData subProperty in refObject.Properties)
                    {
                        TreeGridNode subNode = this.AddNode(refObject, subProperty, node.Nodes);
                        string subGuid = GetGUID();
                        subNode.Tag = subGuid;
                        this.objectMap.Add(subGuid, refObject);
                        this.propertyMap.Add(subGuid, subProperty);
                    }
                }

                else
                {
                    node.Cells[1].Value = "NULL";
                }
            }

            return node;
        }

        public void EndEdit()
        {
            this.Validate(false);
            this.dataGridView1.EndEdit();
        }

        private static string GetGUID()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        #endregion

        #region Events

        public event EventHandler SelectedPropertyChanged;

        public event EventHandler<ValidationExceptionEventArgs> ValidationFailed;

        public event EventHandler ValidationSucceeded;

        #endregion

        #region Event Handlers

        protected void OnSelectedPropertyChanged()
        {
            if (this.SelectedPropertyChanged != null)
                this.SelectedPropertyChanged(this, EventArgs.Empty);
        }

        protected void OnValidationFailed(Exception e)
        {
            if (this.ValidationFailed == null)
                throw e;

            else
                this.ValidationFailed(this, new ValidationExceptionEventArgs(e.Message));
        }

        protected void OnValidationSucceeded()
        {
            if (this.ValidationSucceeded != null)
                this.ValidationSucceeded(this, EventArgs.Empty);
        }

        /// <summary>
        /// Caches the current cell value for the cell about to be edited.
        /// </summary>
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Cache current value to prevent unnecessary updates
            this.preEditValue =
                this.dataGridView1.Rows[e.RowIndex].Cells[1].Value != null ?
                this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() :
                String.Empty;
        }

        /// <summary>
        /// Updates the current object pointers based on the selected cells.
        /// </summary>
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentNode == null || this.dataGridView1.CurrentNode.Tag == null)
            {
                this.selectedObject = null;
                this.selectedProperty = null;
                return;
            }

            else
            {
                // Update selected property and object
                string guid = this.dataGridView1.CurrentNode.Tag.ToString();
                this.selectedObject = this.objectMap.ContainsKey(guid) ? this.objectMap[guid] : null;
                this.selectedProperty = this.propertyMap.ContainsKey(guid) ? this.propertyMap[guid] : null;

                // Move to column index 1 (the editable field)
                if (this.dataGridView1.CurrentCell.ColumnIndex != 1)
                {
                    this.dataGridView1.CurrentCell =
                        this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[1];
                }

                // Edit contents
                if (this.dataGridView1.CurrentNode.Nodes.Count == 0)
                    this.dataGridView1.BeginEdit(true);
            }

            this.OnSelectedPropertyChanged();
        }

        /// <summary>
        /// Validates the value entered when a cell is editted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!this.dataGridView1.CurrentCell.IsInEditMode ||
                this.selectedObject == null ||
                this.selectedProperty == null)
                return;

            string guid = this.dataGridView1.CurrentNode.Tag.ToString();

            // Ignore unchanged values
            if (!this.SelectedProperty.GetValueAsString().Equals(e.FormattedValue.ToString()))
            {
                // Update object
                try
                {
                    TreeGridNode parent = this.dataGridView1.CurrentNode.Parent;
                    ManagementBaseObject target = this.objectMap[guid];
                    string property = this.propertyMap[guid].Name;
                    object value = e.FormattedValue;

                    // Apply new value
                    target.SetPropertyValue(property, value);

                    // Move up a level
                    while (parent != null && this.objectMap.ContainsKey(parent.Tag.ToString()))
                    {
                        value = target;
                        target = this.objectMap[parent.Tag.ToString()];

                        if (this.propertyMap.ContainsKey(parent.Tag.ToString()))
                        {
                            property = this.propertyMap[parent.Tag.ToString()].Name;
                            target.SetPropertyValue(property, value);
                        }

                        parent = parent.Parent;
                    }

                    this.OnValidationSucceeded();
                }

                catch (Exception x)
                {
                    SystemSounds.Exclamation.Play();

                    e.Cancel = true;
                    this.dataGridView1.Rows[e.RowIndex].ErrorText = x.Message;

                    this.OnValidationFailed(x);
                }

            }
        }

        #endregion
    }

    public class ValidationExceptionEventArgs : EventArgs
    {
        public ValidationExceptionEventArgs(string message)
            : base()
        {
            this.message = message;
        }

        private string message;

        public string Message
        {
            get { return this.message; }
        }
    }
}
