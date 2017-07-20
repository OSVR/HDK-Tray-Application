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
using System.IO;
using System.Windows.Forms;

namespace HDK_TrayApp
{
    public class ServerManager : IDisposable
    {
        private ServerConsole m_console;
        private ContextMenuWYSIWYG m_contextMenu;

        private PromptStartServerConsoleOpening m_startServerOnConsoleOpenPrompt;

        public bool ConsoleVisible { get { return m_console.Visible; } }

        public bool Running { get { return OSVRProcessManager.ProcessInstanceIsRunning(Common.SERVICE_NAME); } }

        private object m_lock = new object();
        private bool m_showingPrompt = false;

        public ServerManager(ContextMenuWYSIWYG contextMenu)
        {
            m_contextMenu = contextMenu;
            m_console = new ServerConsole(this);
            m_startServerOnConsoleOpenPrompt = new PromptStartServerConsoleOpening(this);
        }

        /// <summary>
        /// Start OSVR server
        /// </summary>
        public void StartServer(bool restart = false)
        {
            string configArgs = string.Empty;

            // is the "Use Custom Config" option checked
            if (m_contextMenu.CustomServerConfigChecked)
            {
                string targetCustomConfig = Properties.Settings.Default.targetCustomConfig;
                // has the user specified a custom config?
                if (!string.IsNullOrEmpty(targetCustomConfig) && File.Exists(targetCustomConfig))
                    configArgs = targetCustomConfig;
                else
                {
                    Common.ShowMessageBox(Common.MSG_MISSING_CUSTOM_CONFIG, MessageBoxButtons.OK, MessageBoxIcon.Error, true);
                    return;
                }
            }
            else
            {
                switch (m_contextMenu.DetectHDKType())
                {
                    case ContextMenuWYSIWYG.HDKType.HDK1:
                        if (NativeHelpers.IsExtendedModeEnabled())
                        {
                            if (m_contextMenu.UseIRCameraChecked)
                                configArgs = Common.CFG_1X_EM_CAM;
                            else
                                configArgs = Common.CFG_1X_EM_NOCAM;
                        }
                        else
                        {
                            if (m_contextMenu.UseIRCameraChecked)
                                configArgs = Common.CFG_1X_DM_CAM;
                            else
                                configArgs = Common.CFG_1X_DM_NOCAM;
                        }
                        break;

                    case ContextMenuWYSIWYG.HDKType.HDK2:
                        if (NativeHelpers.IsExtendedModeEnabled())
                        {
                            if (m_contextMenu.UseIRCameraChecked)
                                configArgs = Common.CFG_2_EM_CAM;
                            else
                                configArgs = Common.CFG_2_EM_NOCAM;
                        }
                        else
                        {
                            if (m_contextMenu.UseIRCameraChecked)
                                configArgs = Common.CFG_2_DM_CAM;
                            else
                                configArgs = Common.CFG_2_DM_NOCAM;
                        }
                        break;

                    case ContextMenuWYSIWYG.HDKType.UNKNOWN:
                        Common.ShowMessageBox(Common.MSG_UNABLE_TO_DETECT_HDK_TYPE, MessageBoxButtons.OK, MessageBoxIcon.Error, true);
                        return;

                    default:
                        // error case already shows error message
                        return;
                }
            }

            string osvrPath = OSVRRegistry.GetInstallDirectoryFromRegistry();
            string completeFilePath = osvrPath + Common.SERVICE_PATH + Common.SERVICE_NAME;
            string workingDirectory = osvrPath + Common.SERVICE_PATH;
            Process server = OSVRProcessManager.LaunchExecutable(completeFilePath,
                                                                 workingDirectory,
                                                                 ProcessWindowStyle.Minimized,  // Note: this is ignored
                                                                 configArgs,
                                                                 false);

            if (server == null)
            {
                Common.ShowMessageBox(Common.MSG_UNABLE_TO_START_SERVER, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!m_console.Visible)
                m_console.Show();

            m_console.ServerStateChanged(restart ? ServerConsole.ServerStateChange.Restart : ServerConsole.ServerStateChange.Start);

            server.EnableRaisingEvents = true;
            server.OutputDataReceived += Server_OutputDataReceived;
            server.ErrorDataReceived += Server_ErrorDataReceived;
            server.BeginOutputReadLine();
            server.BeginErrorReadLine();
        }

        /// <summary>
        /// Stop OSVR server
        /// </summary>
        public bool StopServer()
        {
            if (Running)
            {
                if (OSVRProcessManager.KillProcessByName(Common.SERVICE_NAME) > 0)
                {
                    m_console.ServerStateChanged(ServerConsole.ServerStateChange.Stop);
                    return true;
                }
                else
                    Common.ShowMessageBox(Common.MSG_UNABLE_TO_STOP_SERVER, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        /// <summary>
        /// Restart OSVR server
        /// </summary>
        public void RestartServer()
        {
            StopServer();
            StartServer(true);
        }

        /// <summary>
        /// Prompt the user to restart the OSVR server if it's running or start it if it's not
        /// </summary>
        public void PromptServerStartOrRestart()
        {
            lock (m_lock)
            {
                if (m_showingPrompt)
                    return;

                m_showingPrompt = true;
            }

            if (Running)
            {
                DialogResult askToResetServer = Common.ShowMessageBox(Common.MSG_RESET_SERVER, MessageBoxButtons.YesNo, MessageBoxIcon.Question, true);

                if (askToResetServer == DialogResult.Yes)
                    RestartServer();
            }
            else
            {
                DialogResult askToStartServer = Common.ShowMessageBox(Common.MSG_START_SERVER, MessageBoxButtons.YesNo, MessageBoxIcon.Question, true);

                if (askToStartServer == DialogResult.Yes)
                    StartServer();
            }

            lock (m_lock)
            {
                m_showingPrompt = false;
            }
        }

        /// <summary>
        /// Prompt the user to restart the OSVR server if user clicked start and it's already running
        /// </summary>
        public void PromptServerRestartAlreadyRunning()
        {
            DialogResult askToStartServer = Common.ShowMessageBox(Common.MSG_RESET_SERVER_ALREADY_RUNNING, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (askToStartServer == DialogResult.Yes)
                RestartServer();
        }

        private void Server_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            m_console.DataReceived(e.Data, false);
        }

        private void Server_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            m_console.DataReceived(e.Data, true);

            if (e.Data != null && e.Data.Contains("WARNING - Your HDK infrared tracking camera was detected to have outdated firmware in need of updating, and may not function properly."))
            {
                string osvrPath = OSVRRegistry.GetInstallDirectoryFromRegistry();
                string workingDirectory = osvrPath + Common.FW_UTIL_PATH;
                string completeFilePath = workingDirectory + Common.IR_CAM_FW_UPDATER_NAME;
                Process ir_cam_fw_updater = OSVRProcessManager.LaunchExecutable(completeFilePath,
                                                                      workingDirectory,
                                                                      ProcessWindowStyle.Normal,
                                                                      string.Empty);

                if (ir_cam_fw_updater == null)
                    Common.ShowMessageBox(Common.MSG_MISSING_OR_CANCELLED_IR_CAM_FW_UPDATER, MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    m_console.DataReceived(
                        "\n---\n" +
                        "Because your HDK IR Camera has outdated firmware, the firmware update tool will be launched.\n" +
                        "Please restart the server after updating your camera's firmware.\n\n" +
                        "NOTE: If the update utility reports that the update \"is not suitable for your device\"\n" +
                        "please plug your HDK IR Camera directly into a USB port on your computer rather than a hub\n" +
                        "such as those found on monitors, then restart the server to try again." +
                        "\n---\n", false);
            }
        }

        public void Recenter()
        {
            string osvrPath = OSVRRegistry.GetInstallDirectoryFromRegistry();
            string workingDirectory = osvrPath + Common.FW_UTIL_PATH;
            string completeFilePath = workingDirectory + Common.RECENTER_NAME;
            OSVRProcessManager.LaunchExecutable(completeFilePath,
                                                workingDirectory,
                                                ProcessWindowStyle.Minimized,
                                                string.Empty);
        }

        public void NotifyIsRunning(bool running)
        {
            m_contextMenu.NotifyServerRunning(running);
        }

        public void Show(bool prompt_to_start = true)
        {
            m_console.Show();

            if (!Running && prompt_to_start)
            {
                if (Properties.Settings.Default.promptServerConsoleOpening)
                    m_startServerOnConsoleOpenPrompt.ShowDialog();
                else if (Properties.Settings.Default.shouldStartServerConsoleOpening)
                    StartServer();
            }
        }

        public void Dispose()
        {
            if (m_console != null)
            {
                m_console.Dispose();
                m_console = null;
            }
        }
    }
}
