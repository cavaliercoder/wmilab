namespace System.Windows.Forms
{
    partial class ManagementObjectInspectorForm
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
            this.managementClassInspector1 = new System.Windows.Forms.ManagementObjectInspector();
            this.SuspendLayout();
            // 
            // managementClassInspector1
            // 
            this.managementClassInspector1.AutoScroll = true;
            this.managementClassInspector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.managementClassInspector1.Location = new System.Drawing.Point(0, 0);
            this.managementClassInspector1.ManagementObject = null;
            this.managementClassInspector1.Name = "managementClassInspector1";
            this.managementClassInspector1.ShowQualifiers = true;
            this.managementClassInspector1.ShowSystemProperties = true;
            this.managementClassInspector1.Size = new System.Drawing.Size(344, 387);
            this.managementClassInspector1.TabIndex = 0;
            // 
            // ManagementObjectInspectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 387);
            this.Controls.Add(this.managementClassInspector1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "ManagementObjectInspectorForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "WMI Query Result";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ManagementObjectInspectorForm_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ManagementObjectInspector managementClassInspector1;


    }
}