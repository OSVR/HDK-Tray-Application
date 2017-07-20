// Copyright 2017 Razer, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace HDK_TrayApp
{
    public partial class ServerConsole : Form
    {
        #region Native Helpers
        private const int SB_VERT = 0x1,
                          WM_VSCROLL = 0x115,
                          SB_THUMBPOSITION = 0x4,
                          SB_BOTTOM = 0x7;
        [DllImport("user32")]
        private static extern bool HideCaret(IntPtr hWnd);
        [DllImport("user32")]
        private static extern int GetScrollPos(IntPtr hWnd, int nBar);
        [DllImport("user32")]
        private static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);
        [DllImport("user32")]
        private static extern bool PostMessageA(IntPtr hWnd, int nBar, int wParam, int lParam);
        //[DllImport("user32.dll")]
        //private static extern bool GetScrollRange(IntPtr hWnd, int nBar, out int lpMinPos, out int lpMaxPos);
        #endregion
        
        private object m_lock = new object();   // Note: can't lock(this) because https://social.msdn.microsoft.com/Forums/vstudio/en-us/aa70d091-2a54-4ff8-a012-4e198ff1e01c/controlbegininvoke-will-also-be-blocked?forum=netfxbcl
        private ServerManager m_server;
        private PromptConsoleClosing m_closingPrompt;
        private string m_defaultTitle;
        public enum ServerStateChange { Start, Restart, Stop }
        private static readonly string NL = Environment.NewLine;

        #region Window Lifecycle
        public ServerConsole(ServerManager serverManager)
        {
            InitializeComponent();

            // Note: This was needed when Invoke resulted in deadlocks.  See OSVI-65 for context
            // (or https://blogs.msdn.microsoft.com/dsui_team/2012/10/31/debugging-windows-forms-application-hangs-during-systemevents-userpreferencechanged/
            // and http://stackoverflow.com/questions/287142/invoke-is-blocking).
            if (!IsHandleCreated)
                CreateHandle();

            m_server = serverManager;

            m_closingPrompt = new PromptConsoleClosing();

            m_defaultTitle = Text;
            Text = m_defaultTitle + " - Stopped";

            UpdateServerLifecycleButtonsAndIcon(serverManager.Running);

            FormClosing += ServerConsole_FormClosing;
        }

        private void ServerConsole_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;

                try
                {
                    Hide();

                    if (m_server.Running && Properties.Settings.Default.promptServerConsoleClosing)
                        m_closingPrompt.Show();
                }
                catch (ObjectDisposedException) { }
            }
        }
        #endregion

        #region Logging
        public void DataReceived(string data, bool err)
        {
            if (data == null || data.Trim().Length == 0)
                return;

            lock (m_lock)
            {
                try
                {
                    BeginInvoke(new MethodInvoker
                    (
                        delegate
                        {
                            Log(data, err);
                        }
                    ));
                }
                catch (Exception)
                {
                    Debug.WriteLine("Unable to log message to console!");
                }
            }
        }

        public void ServerStateChanged(ServerStateChange change)
        {
            lock (m_lock)
            {
                try
                {
                    BeginInvoke(
                        new MethodInvoker
                        (
                            delegate
                            {
                                string change_message;

                                switch (change)
                                {
                                    case ServerStateChange.Start:
                                        change_message = "started";
                                        UpdateServerLifecycleButtonsAndIcon(true);
                                        Text = m_defaultTitle + " - Running";
                                        break;

                                    case ServerStateChange.Restart:
                                        change_message = "restarted";
                                        UpdateServerLifecycleButtonsAndIcon(true);
                                        Text = m_defaultTitle + " - Running";
                                        break;

                                    case ServerStateChange.Stop:
                                        change_message = "stopped";
                                        UpdateServerLifecycleButtonsAndIcon(false);
                                        Text = m_defaultTitle + " - Stopped";
                                        break;

                                    default:
                                        throw new Exception("Invalid server state change!");
                                }

                                Log(NL + NL + "[Server " + change_message + " at " + DateTime.Now + "]" + NL + "---", false);
                            }
                        )
                    );
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Unable to log message to console:\n" + e.Message + "\n" + e.StackTrace);
                }
            }
        }

        private void Log(string line, bool err)
        {
            int previous_select_start = serverConsoleTextBox.SelectionStart;
            int previous_selection_length = serverConsoleTextBox.SelectionLength;
            int savedVpos = GetScrollPos(serverConsoleTextBox.Handle, SB_VERT);

            serverConsoleTextBox.AppendText(line + NL);

            if (autoScrollCheckBox.Checked)
            {
                int last_newline = serverConsoleTextBox.Text.LastIndexOf("\n", serverConsoleTextBox.TextLength - 2) + 1;

                if (errorHighlightCheckbox.Checked)
                {
                    serverConsoleTextBox.Select(last_newline, line.Length);
                    serverConsoleTextBox.SelectionBackColor = err ? Color.Red : Color.White;
                }

                if (last_newline > 0)
                    serverConsoleTextBox.Select(last_newline, 0);

                PostMessageA(serverConsoleTextBox.Handle, WM_VSCROLL, SB_BOTTOM, 0);
            }
            else
            {
                if (errorHighlightCheckbox.Checked)
                {
                    serverConsoleTextBox.Select(serverConsoleTextBox.Text.LastIndexOf("\n", serverConsoleTextBox.TextLength - 2) + 1, line.Length);
                    serverConsoleTextBox.SelectionBackColor = err ? Color.Red : Color.White;
                    serverConsoleTextBox.Select(previous_select_start, previous_selection_length);
                }

                SetScrollPos(serverConsoleTextBox.Handle, SB_VERT, savedVpos, true);
                PostMessageA(serverConsoleTextBox.Handle, WM_VSCROLL, SB_THUMBPOSITION + 0x10000 * savedVpos, 0);
            }

            HideCaret(serverConsoleTextBox.Handle);
        }
        #endregion

        #region UI Event Handlers
        private void startServerButton_Click(object sender, EventArgs e)
        {
            if (!m_server.Running)
                m_server.StartServer();
            else
                m_server.RestartServer();
        }

        private void restartServerButton_Click(object sender, EventArgs e)
        {
            if (m_server.Running)
                m_server.RestartServer();
        }

        private void stopServerButton_Click(object sender, EventArgs e)
        {
            if (m_server.Running)
                m_server.StopServer();
        }

        private void copyToClipboardButton_Click(object sender, EventArgs e)
        {
            lock (m_lock)
            {
                if (serverConsoleTextBox.SelectionLength == 0)
                    Clipboard.SetText(serverConsoleTextBox.Text);
                else
                    Clipboard.SetText(serverConsoleTextBox.SelectedText);
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            lock (m_lock)
            {
                serverConsoleTextBox.Clear();
            }
        }

        private void saveAsButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "OSVR Server Log|*.log|Plain Text|*.txt";
            sfd.Title = "Save OSVR Server Log";

            switch (sfd.ShowDialog())
            {
                case DialogResult.OK:
                    if (sfd.FileName != "")
                    {
                        lock (m_lock) { serverConsoleTextBox.SaveFile(sfd.FileName, RichTextBoxStreamType.PlainText); }
                    }
                    break;

                default:
                    break;
            }
        }

        private void recenterButton_Click(object sender, EventArgs e)
        {
            m_server.Recenter();
        }

        private void autoScrollCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                PostMessageA(serverConsoleTextBox.Handle, WM_VSCROLL, SB_BOTTOM, 0);

            HideCaret(serverConsoleTextBox.Handle);
        }

        private void highlightErrorsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked)
            {
                int previous_select_start = serverConsoleTextBox.SelectionStart;
                int previous_selection_length = serverConsoleTextBox.SelectionLength;
                int savedVpos = GetScrollPos(serverConsoleTextBox.Handle, SB_VERT);

                serverConsoleTextBox.Select(0, serverConsoleTextBox.TextLength);
                serverConsoleTextBox.SelectionBackColor = Color.White;

                serverConsoleTextBox.Select(previous_select_start, previous_selection_length);

                SetScrollPos(serverConsoleTextBox.Handle, SB_VERT, savedVpos, true);
                PostMessageA(serverConsoleTextBox.Handle, WM_VSCROLL, SB_THUMBPOSITION + 0x10000 * savedVpos, 0);

                HideCaret(serverConsoleTextBox.Handle);
            }
        }
        #endregion

        #region Misc
        private void UpdateServerLifecycleButtonsAndIcon(bool server_running)
        {
            if (server_running)
            {
                startServerButton.Enabled = false;
                restartServerButton.Enabled = true;
                stopServerButton.Enabled = true;
                recenterButton.Enabled = true;
            }
            else
            {
                startServerButton.Enabled = true;
                restartServerButton.Enabled = false;
                stopServerButton.Enabled = false;
                recenterButton.Enabled = false;
            }

            m_server.NotifyIsRunning(server_running);
        }
        #endregion
    }
}
