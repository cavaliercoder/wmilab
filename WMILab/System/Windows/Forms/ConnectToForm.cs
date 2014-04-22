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
    using System.Runtime.InteropServices;
    using System.Security.Principal;
    
    public partial class ConnectToForm : Form
    {
        #region Constructors

        private ConnectToForm()
        {
            InitializeComponent();

            //this.keychain = new KeyChain();

            // Set default values
            this.SetIdentityLocal();

            // Get recent Servers
            /*
            foreach (string server in Settings.Default.RecentServers)
            {
                string[] parts = server.Split('|');
                this.txtHost.Items.Add(parts[0]);
            }

            // Add remembered users
            foreach (AuthKey key in this.keychain.GetKeys())
            {
                this.txtUserName.Items.Add(key.UserName);
            }
            */

            // Select most recent
            if(this.txtHost.Items.Count >0)
                this.txtHost.SelectedIndex = 0;
        }

        public static ManagementScope ShowConnectToServerDialog()
        {
            ConnectToForm dialog = new ConnectToForm();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.newScope;
            }

            return null;
        }

        private void ConnectToForm_Load(object sender, EventArgs e)
        {
            this.txtHost.Focus();
        }

        #endregion

        #region Fields

        // private KeyChain keychain;

        private bool rememberedPassword;

        #endregion

        #region Properties

        private ManagementScope newScope;

        #endregion

        #region Methods

        private void SetIdentityLocal()
        {
            string[] id = WindowsIdentity.GetCurrent().Name.Split('\\');
            this.txtUserName.Text = id[1];
            this.txtDomain.Text = id[0];
            this.txtPassword.Text = txtPassword.Text = String.Empty;
        }

        private bool isLocalHost(string host)
        {
            string[] localHosts = new string[] { "localhost", ".", "127.0.0.1" };
            return (-1 != Array.IndexOf(localHosts, host.ToLower()));
        }

        private void HostChanged(object sender, EventArgs e)
        {
            bool isLocal = this.isLocalHost(this.txtHost.Text);

            this.txtUserName.Enabled = 
                this.txtPassword.Enabled =
                this.txtDomain.Enabled = 
                this.chkPacketPrivacy.Enabled = 
                this.chkRemember.Enabled = 
                !isLocal;

            if (isLocal)
            {
                this.SetIdentityLocal();
            }

            else
            {
                // Apply last used username and domain
                /*
                for (int i = 0; i < Settings.Default.RecentServers.Count; i++)
                {
                    string[] parts = Settings.Default.RecentServers[i].Split('|');
                    if (parts[0] == this.txtHost.Text)
                    {
                        this.txtDomain.Text = parts[2];
                        this.txtUserName.Text = parts[1];
                        this.ApplyRememberedPassword();
                        return;
                    }
                }
                 */
            }
        }

        private void UsernameChanged(object sender, EventArgs e)
        {
            if (this.rememberedPassword)
                this.txtPassword.Text = String.Empty;
        }

        private void PasswordChanged(object sender, EventArgs e)
        {
            this.rememberedPassword = false;
        }

        private void ApplyRememberedPassword()
        {
            if (this.txtPassword.Text != String.Empty) return;
            /*
            foreach (AuthKey key in this.keychain.GetKeys())
            {
                if (key.Domain == this.txtDomain.Text && 
                    key.UserName == this.txtUserName.Text)
                {
                    this.txtPassword.Text = key.GetPassword();
                    this.rememberedPassword = true;
                    return;
                }
            }
             */
        }

        private void ApplyRememberedPassword(object sender, EventArgs e)
        {
            this.ApplyRememberedPassword();
        }

        private void CancelDialog(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ConfirmDialog(object sender, EventArgs e)
        {
            // Disable UI
            this.Cursor = Cursors.AppStarting;
            this.txtUserName.Enabled =
                this.txtHost.Enabled =
                this.txtPassword.Enabled =
                this.txtDomain.Enabled =
                this.chkPacketPrivacy.Enabled =
                this.chkRemember.Enabled =
                this.OK_Button.Enabled =
                false;

            // Build connection options
            ConnectionOptions opts = new ConnectionOptions();
            if (!this.isLocalHost(this.txtHost.Text))
            {
                opts.Username = String.IsNullOrEmpty(this.txtDomain.Text) ? 
                    this.txtUserName.Text : 
                    String.Format("{0}\\{1}", this.txtDomain.Text, this.txtUserName.Text);
                opts.Password = this.txtPassword.Text;
                opts.Timeout = new TimeSpan(0, 0, 3);

                if (this.chkPacketPrivacy.Checked)
                    opts.Authentication = AuthenticationLevel.PacketPrivacy;
            }

            // Connect to remote server and test connection
            try
            {
                this.newScope = new ManagementScope(String.Format("\\\\{0}\\ROOT", this.txtHost.Text), opts);
                this.newScope.Connect();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Access Denied.\r\n\r\nPlease check your username and password.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (COMException x)
            {
                MessageBox.Show(x.Message, String.Format("Error 0x{0:X}", x.ErrorCode), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ManagementException x)
            {
                MessageBox.Show(x.Message, String.Format("Error 0x{0:X}", x.ErrorCode), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (this.newScope != null && this.newScope.IsConnected)
            {
                string newHost = String.Format("{0}|{1}|{2}", this.txtHost.Text, this.txtUserName.Text, this.txtDomain.Text);
                
                /*
                // Remove existin entries for current server in recent servers.
                for(int i = 0; i< Settings.Default.RecentServers.Count; i++)
                {
                    string[] parts = Settings.Default.RecentServers[i].Split('|');
                    if (parts[0] == this.txtHost.Text)
                    {
                        Settings.Default.RecentServers.RemoveAt(i);
                        continue;
                    }
                }

                // Add recent server
                Settings.Default.RecentServers.Insert(0, newHost);

                // Save password
                if (this.chkRemember.Checked)
                {
                    this.keychain.SetKey(
                        this.txtUserName.Text,
                        this.txtDomain.Text,
                        this.txtPassword.Text,
                        null
                        );
                }

                Settings.Default.Save();
                */

                // Send back success
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            // Re-enable UI
            this.Cursor = Cursors.Default;
            this.txtUserName.Enabled =
                this.txtHost.Enabled =
                this.txtPassword.Enabled =
                this.txtDomain.Enabled =
                this.chkPacketPrivacy.Enabled =
                this.chkRemember.Enabled =
                this.OK_Button.Enabled =
                true;
        }

        #endregion
    }
}
