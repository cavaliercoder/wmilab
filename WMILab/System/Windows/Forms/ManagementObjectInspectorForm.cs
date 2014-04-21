using System.Management;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    public partial class ManagementObjectInspectorForm : Form
    {
        public ManagementObjectInspectorForm()
        {
            InitializeComponent();
        }

        public static void Show(ManagementBaseObject managementObject)
        {
            ManagementObjectInspectorForm form = new ManagementObjectInspectorForm();
            form.ManagementObject = managementObject;
            form.Show();
        }


        public ManagementScope Scope
        {
            get { return this.managementClassInspector1.Scope; }
            set { this.managementClassInspector1.Scope = value; }
        }

        public ManagementBaseObject ManagementObject
        {
            get { return this.managementClassInspector1.ManagementObject; }
            set
            {
                this.managementClassInspector1.ManagementObject = value;
                this.Text = value.GetRelativePath();
            }
        }

        public ManagementClass ManagementClass
        {
            get { return this.managementClassInspector1.ManagementClass; }

            set { this.managementClassInspector1.ManagementClass = value; }
        }

        public Boolean ShowMappedValues
        {
            get { return this.managementClassInspector1.ShowMappedValues; }
            set { this.managementClassInspector1.ShowMappedValues = value; }
        }

        private void ManagementObjectInspectorForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
