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
    using System.Management;
    using System.Windows.Forms;

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
