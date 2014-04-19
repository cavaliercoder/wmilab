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

        public ManagementBaseObject ManagementObject
        {
            get { return this.managementClassInspector1.ManagementObject; }
            set 
            {
                this.managementClassInspector1.ManagementObject = value;
                this.Text = value.GetRelativePath();
            }
        }

        public ManagementScope Scope
        {
            get { return this.managementClassInspector1.Scope; }
            set { this.managementClassInspector1.Scope = value; }
        }

        public PropertyDataValueMapCollection ValueMaps
        {
            get { return this.managementClassInspector1.ValueMaps; }

            set { this.managementClassInspector1.ValueMaps = value; }
        }

        private void ManagementObjectInspectorForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
