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
    using System.Management;
    using System.Net;
    using System.Net.NetworkInformation;
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
            this.SuspendControlUpdates();

            // Select default hostname
            if (this.txtHost.Items.Count > 0)
                this.txtHost.SelectedIndex = 0;
            else
                this.txtHost.Text = "localhost";
            this.txtHost.SelectAll();

            // Set default user identity
            this.SetIdentityToLocal();

            this.RefreshHostnameControl();
            this.RefreshDomainNameControl();

            // Done
            this.ResumeControlUpdates();

            // Get recent Servers
            /*
            foreach (string server in Settings.Default.RecentServers)
            {
                string[] usernameParts = server.Split('|');
                this.txtHost.Items.Add(usernameParts[0]);
            }

            // Add remembered users
            foreach (AuthKey key in this.keychain.GetKeys())
            {
                this.txtUserName.Items.Add(key.UserName);
            }
            */
        }

        public static ManagementScope ShowConnectToServerDialog()
        {
            ConnectToForm dialog = new ConnectToForm();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.NewScope;
            }

            return null;
        }

        private void connectToForm_OnShown(object sender, EventArgs e)
        {
            this.txtHost.Focus();
        }

        #endregion

        #region Fields

        private Int32 uiLockLevel = 0;

        // private KeyChain keychain;

        private Boolean rememberedPassword;

        private String[] localAddresses;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the ManagementScope created by this ConnectToForm.
        /// </summary>
        public ManagementScope NewScope
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the username (excluding domain) currently selected by the user
        /// </summary>
        public String Username
        {
            get
            {
                var components = GetUsernameComponents(this.txtUserName.Text);
                return components[0];
            }
        }

        /// <summary>
        /// Gets the user domain currently selected by the user
        /// </summary>
        public String Domain
        {
            get
            {
                var components = GetUsernameComponents(this.txtUserName.Text);
                if (!String.IsNullOrEmpty(components[1]))
                    return components[1];

                return this.txtDomain.Text;
            }
        }

        /// <summary>
        /// Gets a list of all hostnames and IPs that resolve to the local system.
        /// </summary>
        public String[] LocalAddresses
        {
            get
            {
                if (this.localAddresses == null)
                {
                    var localAddressList = new List<String>();

                    // Add default addresses
                    localAddressList.AddRange(new String[] { 
                        ".",
                        "localhost",
                        "loopback",
                        "::1",
                        Environment.MachineName ,
                        Dns.GetHostName()
                    });

                    // Get FQDN from global IP config
                    var ipProperties = IPGlobalProperties.GetIPGlobalProperties();
                    localAddressList.Add(ipProperties.HostName);
                    if (!ipProperties.HostName.EndsWith(ipProperties.DomainName, StringComparison.InvariantCultureIgnoreCase))
                        localAddressList.Add(String.Format("{0}.{1}", ipProperties.HostName, ipProperties.DomainName));

                    // Get configured IPs from WMI
                    var searcher = new ManagementObjectSearcher("SELECT IpAddress from Win32_NetworkAdapterConfiguration");
                    var results = searcher.Get();
                    foreach (ManagementObject result in results)
                    {
                        var ips = (String[])result.Properties["IpAddress"].Value;
                        if (ips != null)
                            localAddressList.AddRange(ips);
                    }

                    // Convert all to lowercase
                    for (int i = 0; i < localAddressList.Count; i++)
                        localAddressList[i] = localAddressList[i].ToLowerInvariant();

                    // Update instance variable
                    this.localAddresses = localAddressList.ToArray();

                }

                return this.localAddresses;
            }
        }
        
        #endregion

        #region Control management

        /// <summary>
        /// Suspends all event handlers for window controls.
        /// </summary>
        private void SuspendControlUpdates()
        {
            this.uiLockLevel++;
        }

        /// <summary>
        /// Releases the suspension of all event handlers for window controls.
        /// </summary>
        private void ResumeControlUpdates()
        {
            this.uiLockLevel--;
        }

        /// <summary>
        /// Returns whether window control event handlers are currently suspended.
        /// </summary>
        private Boolean IsControlUpdatesSuspended()
        {
            return this.uiLockLevel > 0;
        }

        #endregion

        #region Host selection

        private bool IsLocalHost(string host)
        {
            // Is host a 127.0.0.0/8 address?
            IPAddress dummy;
            if (IPAddress.TryParse(host, out dummy) && host.StartsWith("127."))
                return true;

            // Is host in the list of local addresses?
            return (-1 != Array.IndexOf(this.LocalAddresses, host.ToLower()));
        }

        private void RefreshHostnameControl()
        {
            this.SuspendControlUpdates();

            bool isLocal = this.IsLocalHost(this.txtHost.Text);

            this.txtUserName.Enabled =
                this.txtPassword.Enabled =
                this.txtDomain.Enabled =
                this.chkPacketPrivacy.Enabled =
                this.chkRemember.Enabled =
                this.cmbAuthority.Enabled =
                !isLocal;

            if (isLocal)
            {
                this.SetIdentityToLocal();
            }

            else
            {
                // Apply last used username and domain
                /*
                for (int i = 0; i < Settings.Default.RecentServers.Count; i++)
                {
                    string[] usernameParts = Settings.Default.RecentServers[i].Split('|');
                    if (usernameParts[0] == this.txtHost.Text)
                    {
                        this.txtDomain.Text = usernameParts[2];
                        this.txtUserName.Text = usernameParts[1];
                        this.ApplyRememberedPassword();
                        return;
                    }
                }
                 */
            }

            this.ResumeControlUpdates();
        }

        private void RefreshHostnameControl(object sender, EventArgs e)
        {
            if (this.IsControlUpdatesSuspended())
                return;

            RefreshHostnameControl();
        }

        #endregion

        #region Identity selection

        /// <summary>
        /// Returns a String[] with format { username, domain } for the users currently selected username.
        /// </summary>
        /// <returns>A String[] with format { username, domain }.</returns>
        private String[] GetUsernameComponents(String username)
        {
            String domain = String.Empty;
            String[] usernameParts;

            // Auto-populate domain field if domain specified in username as DOMAIN\user
            usernameParts = username.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            if (usernameParts.Length == 2)
            {
                username = usernameParts[1];
                domain = usernameParts[0];
            }

            // Auto-populate domain field if domain specified in username as user@domain
            usernameParts = this.txtUserName.Text.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            if (usernameParts.Length == 2)
            {
                username = usernameParts[0];
                domain = usernameParts[1];
            }

            return new String[] { username, domain };
        }

        private void SetIdentityToLocal()
        {
            this.SuspendControlUpdates();

            string[] id = WindowsIdentity.GetCurrent().Name.Split('\\');
            this.txtUserName.Text = id[1];
            this.txtDomain.Text = id[0];
            this.txtPassword.Text = txtPassword.Text = String.Empty;
            this.cmbAuthority.SelectedIndex = 0;

            this.ResumeControlUpdates();
        }

        private void RefreshUsernameControl()
        {
            this.SuspendControlUpdates();

            String[] components = this.GetUsernameComponents(this.txtUserName.Text);

            if (String.IsNullOrEmpty(components[1]))
            {
                this.txtDomain.Enabled = true;
            }

            else
            {
                this.txtDomain.Enabled = false;
                this.txtDomain.Text = components[1];
            }

            if (this.rememberedPassword)
                this.txtPassword.Text = String.Empty;

            this.ResumeControlUpdates();
        }

        private void RefreshUsernameControl(object sender, EventArgs e)
        {
            if (this.IsControlUpdatesSuspended())
                return;

            RefreshUsernameControl();
        }

        private void RefreshDomainNameControl()
        {
            this.SuspendControlUpdates();

            this.cmbAuthority.Items[1] = String.Format("NTLMDomain:{0}", this.txtDomain.Text);
            this.cmbAuthority.Items[2] = String.Format("Kerberos:{0}", this.txtDomain.Text);

            this.ResumeControlUpdates();
        }

        private void RefreshDomainNameControl(object sender, EventArgs e)
        {
            if (this.IsControlUpdatesSuspended())
                return;

            this.RefreshDomainNameControl();
        }

        #endregion

        #region Password selection

        private void RefreshPasswordControl(object sender, EventArgs e)
        {
            if (this.IsControlUpdatesSuspended())
                return;

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
            if (this.IsControlUpdatesSuspended())
                return;

            this.ApplyRememberedPassword();
        }

        #endregion

        #region Connection management

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
                this.cmbAuthority.Enabled =
                this.chkPacketPrivacy.Enabled =
                this.chkRemember.Enabled =
                this.OK_Button.Enabled =
                false;

            // Build connection options
            ConnectionOptions opts = new ConnectionOptions();
            if (!this.IsLocalHost(this.txtHost.Text))
            {
                if (this.cmbAuthority.SelectedIndex > 0)
                {                    
                    // User selected an Authority type. 
                    // Omit domain name from connection options as it should only be specified in the Authority string.
                    opts.Username = this.Username;
                    opts.Authority = this.cmbAuthority.Text;
                }

                else
                {
                    if(this.txtUserName.Text.Contains("@"))
                    {
                        // User specified auto-negotiate authority and user@domain credentials
                        opts.Username = String.Format("{0}@{1}", this.Username, this.Domain);
                    }

                    else
                    {
                        // User specified auto-negotiate authority and DOMAIN\user credentials
                        opts.Username = String.Format("{0}\\{1}", this.Domain, this.Username);
                    }
                }

                opts.Password = this.txtPassword.Text;
                opts.Timeout = new TimeSpan(0, 0, 5);

                if (this.chkPacketPrivacy.Checked)
                    opts.Authentication = AuthenticationLevel.PacketPrivacy;

            }

            // Connect to remote server and test connection
            try
            {
                this.NewScope = new ManagementScope(String.Format("\\\\{0}\\ROOT", this.txtHost.Text), opts);
                this.NewScope.Connect();
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

            if (this.NewScope != null && this.NewScope.IsConnected)
            {
                string newHost = String.Format("{0}|{1}|{2}", this.txtHost.Text, this.txtUserName.Text, this.txtDomain.Text);
                
                /*
                // Remove existin entries for current server in recent servers.
                for(int i = 0; i< Settings.Default.RecentServers.Count; i++)
                {
                    string[] usernameParts = Settings.Default.RecentServers[i].Split('|');
                    if (usernameParts[0] == this.txtHost.Text)
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
                this.cmbAuthority.Enabled =
                this.chkPacketPrivacy.Enabled =
                this.chkRemember.Enabled =
                this.OK_Button.Enabled =
                true;
        }

        #endregion
    }
}
