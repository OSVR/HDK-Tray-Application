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
    public partial class ContextMenuWYSIWYG : Form
    {
        private NotifyIcon m_icon;
        private ServerManager m_server;
        private PromptSetHDKDisplayOrientation m_displayOrientationPrompt;

        public enum HDKType { ERROR = -2, NOT_REQUESTED = -1, UNKNOWN = 0, HDK1 = 1, HDK2 = 2 }

        #region Public UI State
        public bool CustomServerConfigChecked { get { return customServerConfigurationToolStripMenuItem.Checked; } }
        public bool UseIRCameraChecked { get { return useIRCameraToolStripMenuItem.Checked;  } }
        #endregion

        #region Lifecycle
        public ContextMenuWYSIWYG(NotifyIcon icon, PromptSetHDKDisplayOrientation prompt, bool start_server)
        {
            InitializeComponent();

            m_icon = icon;
            m_displayOrientationPrompt = prompt;
            m_server = new ServerManager(this);

            // Note: This was needed when Invoke resulted in deadlocks.  See OSVI-65 for context
            // (or https://blogs.msdn.microsoft.com/dsui_team/2012/10/31/debugging-windows-forms-application-hangs-during-systemevents-userpreferencechanged/
            // and http://stackoverflow.com/questions/287142/invoke-is-blocking).
            if (!IsHandleCreated)
                CreateHandle();

            SetGPUType();

            // Set server config GUI to match settings
            defaultServerConfigurationToolStripMenuItem.Checked = Properties.Settings.Default.useDefaultConfig;
            customServerConfigurationToolStripMenuItem.Checked = Properties.Settings.Default.useCustomConfig;
            useIRCameraToolStripMenuItem.Checked = Properties.Settings.Default.useIRCamera;
            useIRCameraToolStripMenuItem.Enabled = !customServerConfigurationToolStripMenuItem.Checked;

            if (start_server)
                m_server.StartServer();

            if (SteamVRConfig.IsLegacyOSVRDriverInstalled())
                Common.ShowMessageBox(Common.MSG_STEAMVR_OSVR_LEGACY_DRIVER_DETECTED, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public HDKType DetectHDKType()
        {
            return StartFWUtil(true);
        }

        /// <summary>
        /// Detect GPU type and update corresponding settings and GUI state
        /// </summary>
        private void SetGPUType()
        {
            // Detect GPU type
            GPUDetection.GraphicsCardType detected = GPUDetection.GraphicsCardType.UNKNOWN;
            try
            {
                detected = GPUDetection.Detect();
            }
            catch
            {
                // Handle error detecting GPU type
                Debug.WriteLine("Error detecting GPU type; leaving default set");
                return;
            }

            // Handle inability to detect GPU type
            if (detected == GPUDetection.GraphicsCardType.UNKNOWN)
            {
                Debug.WriteLine("Unable to detect GPU type; leaving default set");
                return;
            }

            // If detected GPU type does not match saved GPU type
            if (Properties.Settings.Default.graphicsCardType != "" && Properties.Settings.Default.graphicsCardType != detected.ToString())
            {
                // Note: this is disabled because the GPU selection menu has been hidden (automatic detection has been 100% reliable)
                // Common.ShowMessageBox(Common.MSG_GPU_TYPE_DIFFERS, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Update menu state
            switch (detected)
            {
                case GPUDetection.GraphicsCardType.NVIDIA:
                    NVIDIAToolStripMenuItem.Checked = true;
                    AMDToolStripMenuItem.Checked = false;
                    Properties.Settings.Default.graphicsCardType = GPUDetection.GraphicsCardType.NVIDIA.ToString();
                    break;

                case GPUDetection.GraphicsCardType.AMD:
                    AMDToolStripMenuItem.Checked = true;
                    NVIDIAToolStripMenuItem.Checked = false;
                    Properties.Settings.Default.graphicsCardType = GPUDetection.GraphicsCardType.AMD.ToString();
                    break;
            }

            // Save updated setting
            Properties.Settings.Default.Save();
        }

        public void ShowServerConsole()
        {
            m_server.Show(false);
        }

        public void NotifyServerRunning(bool running)
        {
            m_icon.Text = "HDK Tray Application" + (running ? " - Server Running" : "");
        }
        #endregion

        #region UI Event Handlers
        /// <summary>
        /// Update enabled/disabled items based on server state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OSVRContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (m_server.Running)
            {
                startServerToolStripMenuItem.Enabled = false;
                restartServerToolStripMenuItem.Enabled = true;
                stopServerToolStripMenuItem.Enabled = true;

                launchSampleSceneToolStripMenuItem.Enabled = true;
                launchTrackerViewToolStripMenuItem.Enabled = true;
            }
            else
            {
                startServerToolStripMenuItem.Enabled = true;
                restartServerToolStripMenuItem.Enabled = false;
                stopServerToolStripMenuItem.Enabled = false;

                launchSampleSceneToolStripMenuItem.Enabled = false;
                launchTrackerViewToolStripMenuItem.Enabled = false;
            }
            
            showServerConsoleToolStripMenuItem.Enabled = !m_server.ConsoleVisible;
            enableExtendedModeToolStripMenuItem.Enabled = !NativeHelpers.IsExtendedModeEnabled();

            switch (SteamVRConfig.InversionState())
            {
                case SteamVRConfig.SteamVRInversion.Unknown:
                    invertSteamVRToolStripMenuItem.Enabled = false;
                    invertSteamVRToolStripMenuItem.Checked = false;
                    break;

                case SteamVRConfig.SteamVRInversion.Inverted:
                    invertSteamVRToolStripMenuItem.Enabled = true;
                    invertSteamVRToolStripMenuItem.Checked = true;
                    break;

                case SteamVRConfig.SteamVRInversion.NotSet:
                case SteamVRConfig.SteamVRInversion.Standard:
                    invertSteamVRToolStripMenuItem.Enabled = true;
                    invertSteamVRToolStripMenuItem.Checked = false;
                    break;
            }
        }

        /// <summary>
        /// Handler for starting server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!m_server.Running)
                m_server.StartServer();
            else
                m_server.PromptServerRestartAlreadyRunning();
        }

        /// <summary>
        /// Handler for restarting server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void restartServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_server.RestartServer();
        }

        /// <summary>
        /// Handler for stopping server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_server.StopServer();
        }

        /// <summary>
        /// Show server console window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showServerConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowServerConsole();
        }

        /// <summary>
        /// Handler for launching tracker viewer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void launchTrackerViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!OSVRProcessManager.ProcessInstanceIsRunning(Common.TRACKER_VIEW_NAME))
                StartTrackerView();
        }

        /// <summary>
        /// Handler for launching Unity campfire sample app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void launchSampleSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!OSVRProcessManager.ProcessInstanceIsRunning(Common.TEST_APP_NAME))
                StartTestApp();
        }

        /// <summary>
        /// Handler for launching HDK Firmware Utility
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void launchFWUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!OSVRProcessManager.ProcessInstanceIsRunning(Common.FW_UTIL_NAME))
                StartFWUtil();
        }

        /// <summary>
        /// Handler for setting direct mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enableDirectModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetDirectMode();
        }

        /// <summary>
        /// Handler for setting extended mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enableExtendedModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetExtendedMode();
        }

        /// <summary>
        /// Event fired when the exit option is clicked. Perform any
        /// shutdown logic here.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_server.Running)
                m_server.StopServer();

            OSVRProcessManager.KillProcessByName(Common.TRACKER_VIEW_NAME);
            OSVRProcessManager.KillProcessByName(Common.FW_UTIL_NAME);
            OSVRProcessManager.KillProcessByName(Common.TEST_APP_NAME);

            Application.Exit();
        }

        /// <summary>
        /// Use default configuration rather than custom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            bool wasChecked = Properties.Settings.Default.useDefaultConfig;

            UpdateServerConfigCheckboxStates(tsmi);

            if (!wasChecked)
                m_server.PromptServerStartOrRestart();
        }

        /// <summary>
        /// Prompt user to select custom server config JSON, set it as the current config, and prompt to restart server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;

            bool isChecked = tsmi.Checked;
            bool wasChecked = Properties.Settings.Default.useCustomConfig;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = Common.CUSTOM_CONFIG_DIALOG_FILTER;
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = OSVRRegistry.GetInstallDirectoryFromRegistry() + Common.SERVICE_PATH;
            openFileDialog.Title = Common.CUSTOM_CONFIG_DIALOG_TITLE;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Debug.WriteLine(openFileDialog.FileName);
                Properties.Settings.Default.targetCustomConfig = openFileDialog.FileName;
                UpdateServerConfigCheckboxStates(tsmi);
                m_server.PromptServerStartOrRestart();
            }
            else // if config wasn't actually changed, restore previous state
                tsmi.Checked = wasChecked;

            openFileDialog.Dispose();
        }

        /// <summary>
        /// Handler for toggling IR camera
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void useIRCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!customServerConfigurationToolStripMenuItem.Checked)
            {
                UpdateAndSaveServerConfigCheckboxStates();
                m_server.PromptServerStartOrRestart();
            }
            else
                Debug.WriteLine("Toggling IR camera does not apply to custom configuration!");
        }

        /// <summary>
        /// Handler for setting NVIDIA graphics card type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NVIDIAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NVIDIAToolStripMenuItem.Checked = true;
            AMDToolStripMenuItem.Checked = false;

            Properties.Settings.Default.graphicsCardType = GPUDetection.GraphicsCardType.NVIDIA.ToString();
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Handler for setting AMD graphics card type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AMDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AMDToolStripMenuItem.Checked = true;
            NVIDIAToolStripMenuItem.Checked = false;

            Properties.Settings.Default.graphicsCardType = GPUDetection.GraphicsCardType.AMD.ToString();
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Handler for help button (currently invisible)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Common.OSVR_HELP_URL);
        }

        private void invertSteamVRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SteamVRConfig.UpdateSteamVRConfigFile(SteamVRConfig.ConfigChange.Invert);
        }
        #endregion

        #region Supplementary App Utility
        /// <summary>
        /// Launch tracker viewer
        /// </summary>
        private void StartTrackerView()
        {
            string osvrPath = OSVRRegistry.GetInstallDirectoryFromRegistry();
            string workingDirectory = osvrPath + Common.TRACKER_VIEW_PATH;
            string completeFilePath = workingDirectory + Common.TRACKER_VIEW_NAME;
            Process tv  = OSVRProcessManager.LaunchExecutable(completeFilePath, workingDirectory, ProcessWindowStyle.Normal, string.Empty);
            if (tv == null)
                Common.ShowMessageBox(Common.MSG_MISSING_TRACKER_VIEW, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Launch Unity campfire sample application
        /// </summary>
        private void StartTestApp()
        {
            string osvrPath = OSVRRegistry.GetInstallDirectoryFromRegistry();
            string workingDirectory = osvrPath + Common.TEST_APP_PATH;
            string completeFilePath = workingDirectory + Common.TEST_APP_NAME;

            Process proc = OSVRProcessManager.LaunchExecutable(completeFilePath,
                                                               workingDirectory,
                                                               ProcessWindowStyle.Normal,
                                                               ""); // "-show-screen-selector"); // NativeHelpers.IsExtendedModeEnabled() ? "-show-screen-selector" : "");

            if (proc == null)
            {
                Common.ShowMessageBox(Common.MSG_MISSING_SAMPLE_SCENE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            proc.EnableRaisingEvents = true;
            proc.Exited += Testapp_Exited;
        }

        /// <summary>
        /// Handle TestApp crashes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Testapp_Exited(object sender, EventArgs e)
        {
            Process proc = (Process)sender;
            string crash_file_path = proc.StartInfo.WorkingDirectory + "/.crash";
            if (proc.ExitCode != 0 || File.Exists(crash_file_path))
            {
                File.Delete(crash_file_path);
                Debug.WriteLine("Unity sample scene application crashed!");
                Common.ShowMessageBox(Common.MSG_SAMPLE_CRASH, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Launch CPI
        /// </summary>
        public HDKType StartFWUtil(bool detect_hw_type = false)
        {
            string osvrPath = OSVRRegistry.GetInstallDirectoryFromRegistry();
            string workingDirectory = osvrPath + Common.FW_UTIL_PATH;
            string completeFilePath = workingDirectory + Common.FW_UTIL_NAME;
            Process fw_util = OSVRProcessManager.LaunchExecutable(completeFilePath,
                                                                  workingDirectory,
                                                                  ProcessWindowStyle.Normal,
                                                                  detect_hw_type ? "-detecthw" : string.Empty);

            if (fw_util == null)
            {
                Common.ShowMessageBox(Common.MSG_MISSING_FWU, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return HDKType.ERROR;
            }

            if (detect_hw_type)
            {
                fw_util.WaitForExit();
                return (HDKType)fw_util.ExitCode;
            }
            return HDKType.NOT_REQUESTED;
        }
        #endregion

        #region Display/GPU Management
        /// <summary>
        /// Show warning when no GPU type is selected in menu
        /// </summary>
        private void ShowSelectGPUTypeDialog()
        {
            Common.ShowMessageBox(Common.MSG_SELECT_GPU_TYPE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        /// <summary>
        /// Set direct display mode
        /// </summary>
        private void SetDirectMode()
        {
            string direct_mode_name = null;

            GPUDetection.GraphicsCardType gct = GPUDetection.GraphicsCardType.UNKNOWN;
            try
            {
                gct = (GPUDetection.GraphicsCardType)Enum.Parse(typeof(GPUDetection.GraphicsCardType), Properties.Settings.Default.graphicsCardType);
            }
            catch (ArgumentException) { }

            switch (gct)
            {
                case GPUDetection.GraphicsCardType.NVIDIA:
                    direct_mode_name = Common.DIRECT_MODE_NAME_NVIDIA;
                    break;

                case GPUDetection.GraphicsCardType.AMD:
                    direct_mode_name = Common.DIRECT_MODE_NAME_AMD;
                    break;

                case GPUDetection.GraphicsCardType.UNKNOWN:
                    ShowSelectGPUTypeDialog();
                    return;
            }

            string osvrPath = OSVRRegistry.GetInstallDirectoryFromRegistry();
            string workingDirectory = osvrPath + Common.SERVICE_PATH;
            string completeFilePath = workingDirectory + direct_mode_name;
            Process dm_exe = OSVRProcessManager.LaunchExecutable(completeFilePath,
                                                                 workingDirectory,
                                                                 ProcessWindowStyle.Normal,
                                                                 string.Empty);

            if (dm_exe == null)
            {
                Common.ShowMessageBox(Common.MSG_MISSING_DM_EXE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dm_exe.EnableRaisingEvents = true;
            dm_exe.Exited += setDirectModeProcess_Exited;
        }

        private void setDirectModeProcess_Exited(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(PromptServerStartOrRestartDelegate));
        }

        /// <summary>
        /// Set extended display mode
        /// </summary>
        private void SetExtendedMode()
        {
            string extended_mode_name = null;

            GPUDetection.GraphicsCardType gct = GPUDetection.GraphicsCardType.UNKNOWN;
            try
            {
                gct = (GPUDetection.GraphicsCardType)Enum.Parse(typeof(GPUDetection.GraphicsCardType), Properties.Settings.Default.graphicsCardType);
            }
            catch (ArgumentException) { }

            switch (gct)
            {
                case GPUDetection.GraphicsCardType.NVIDIA:
                    extended_mode_name = Common.EXTENDED_MODE_NAME_NVIDIA;
                    break;

                case GPUDetection.GraphicsCardType.AMD:
                    extended_mode_name = Common.EXTENDED_MODE_NAME_AMD;
                    break;

                case GPUDetection.GraphicsCardType.UNKNOWN:
                    ShowSelectGPUTypeDialog();
                    return;
            }

            string osvrPath = OSVRRegistry.GetInstallDirectoryFromRegistry();
            string workingDirectory = osvrPath + Common.SERVICE_PATH;
            string completeFilePath = workingDirectory + extended_mode_name;
            Process em_exe = OSVRProcessManager.LaunchExecutable(completeFilePath,
                                                                 workingDirectory,
                                                                 ProcessWindowStyle.Normal,
                                                                 string.Empty);

            if (em_exe == null)
            {
                Common.ShowMessageBox(Common.MSG_MISSING_EM_EXE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            em_exe.EnableRaisingEvents = true;
            em_exe.Exited += setExtendedModeProcess_Exited;
        }

        private void setExtendedModeProcess_Exited(object sender, EventArgs e)
        {
            if (NativeHelpers.IsExtendedModeEnabled())
            {
                bool server_restart_prompt = true;

                try
                {
                    if (!NativeHelpers.IsDeviceRotationCorrect())
                    {
                        server_restart_prompt = false;  // device rotation will prompt restart
                        BeginInvoke(new MethodInvoker(PromptDeviceRotation));
                    }
                }
                catch
                {
                    server_restart_prompt = false;
                }

                if (server_restart_prompt)
                    BeginInvoke(new MethodInvoker(PromptServerStartOrRestartDelegate));
            }
        }

        private void PromptDeviceRotation()
        {
            if (Properties.Settings.Default.promptSetLandscapeDisplayOrientation)
                m_displayOrientationPrompt.Show();
            else if (Properties.Settings.Default.shouldSetLandscapeDisplayOrientation)
                NativeHelpers.CorrectDeviceRotation();
        }

        public void PromptServerStartOrRestartDelegate()
        {
            m_server.PromptServerStartOrRestart();
        }
        #endregion

        #region Checkbox State Management
        /// <summary>
        /// Process config checkbox state change (default vs. custom)
        /// 
        /// Ensures that only one is checked and that you can't uncheck the currently selected item
        /// </summary>
        /// <param name="lastChangedCheckbox">Checkbox to initiate state change</param>
        private void UpdateServerConfigCheckboxStates(ToolStripMenuItem lastChangedCheckbox)
        {
            // only disable other checkboxes if the current checkbox was toggled ON
            if (lastChangedCheckbox.Checked)
            {
                if (lastChangedCheckbox == defaultServerConfigurationToolStripMenuItem)
                    customServerConfigurationToolStripMenuItem.Checked = false;
                else if (lastChangedCheckbox == customServerConfigurationToolStripMenuItem)
                    defaultServerConfigurationToolStripMenuItem.Checked = false;
            }
            else
                lastChangedCheckbox.Checked = true; // disallow unchecking a menu item

            // IR camera setting only applies to non-custom configuration
            useIRCameraToolStripMenuItem.Enabled = !customServerConfigurationToolStripMenuItem.Checked;

            UpdateAndSaveServerConfigCheckboxStates();
        }

        /// <summary>
        /// Updates the properties related to configuration
        /// checkboxes and saves the user settings.
        /// </summary>
        private void UpdateAndSaveServerConfigCheckboxStates()
        {
            Properties.Settings.Default.useDefaultConfig = defaultServerConfigurationToolStripMenuItem.Checked;
            Properties.Settings.Default.useCustomConfig = customServerConfigurationToolStripMenuItem.Checked;
            Properties.Settings.Default.useIRCamera = useIRCameraToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }
        #endregion
    }
}
