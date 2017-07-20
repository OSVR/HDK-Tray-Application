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

using System.Windows.Forms;

namespace HDK_TrayApp
{
    /// <summary>
    /// Useful constants that are commonly used.
    /// </summary>
    class Common
    {
        public static readonly string REGISTRY_SUB_KEY = "SOFTWARE\\OSVR", // TODO: [OSVI-63] check for "SOFTWARE\\WOW6432Node\\OSVR" but prefer 64-bit (non-WOW) version
                                      REGISTRY_INSTALL_DIRECTORY_KEY = "InstallationDirectory",
                                      REGISTRY_VERSION_KEY = "InstalledVersion";

        public static readonly string TRAY_APP_NAME = "HDK_TrayApp.exe",
                                      SERVICE_NAME = "osvr_server.exe",
                                      TRACKER_VIEW_NAME = "OSVRTrackerView.exe",
                                      FW_UTIL_NAME = "HDK_Firmware_Utility.exe",
                                      TEST_APP_NAME = "HDK_SampleScene.exe",
                                      DIRECT_MODE_NAME_NVIDIA = "EnableOSVRDirectMode.exe",
                                      DIRECT_MODE_NAME_AMD = "EnableOSVRDirectModeAMD.exe",
                                      EXTENDED_MODE_NAME_NVIDIA = "DisableOSVRDirectMode.exe",
                                      EXTENDED_MODE_NAME_AMD = "DisableOSVRDirectModeAMD.exe",
                                      RECENTER_NAME = "osvr_reset_yaw_RZ.exe",
                                      IR_CAM_FW_UPDATER_NAME = "OSVR IR Camera Firmware update(5SF006N2_v0007).exe";

        public static readonly string SERVICE_PATH = "OSVR-Core\\bin\\",
                                      TRACKER_VIEW_PATH = "OSVR-TrackerView\\",
                                      FW_UTIL_PATH = "HDK-Firmware-Utility\\",
                                      TEST_APP_PATH = "HDK-SampleScene\\";

        public static readonly string CFG_1X_DM_CAM = "osvr_config_HDK_1X_default.json",
                                      CFG_1X_DM_NOCAM = "osvr_config_HDK_1X_default_no_cam.json",
                                      CFG_1X_EM_CAM = "osvr_config_HDK_1X_extended_mode.json",
                                      CFG_1X_EM_NOCAM = "osvr_config_HDK_1X_extended_mode_no_cam.json",
                                      CFG_2_DM_CAM = "osvr_config_HDK_2_default.json",
                                      CFG_2_DM_NOCAM = "osvr_config_HDK_2_default_no_cam.json",
                                      CFG_2_EM_CAM = "osvr_config_HDK_2_extended_mode.json",
                                      CFG_2_EM_NOCAM = "osvr_config_HDK_2_extended_mode_no_cam.json";

        public static readonly string CUSTOM_CONFIG_DIALOG_FILTER = "OSVR Server Config Files (*.json) | *.json",
                                      CUSTOM_CONFIG_DIALOG_TITLE = "Select OSVR Server Configuration File";

        private static readonly int MSG_TITLE = 0, MSG_BODY = 1;
        public static readonly string[] MSG_RESET_SERVER = new string[]
                                        {
                                            "Restart OSVR Server?",
                                            "The OSVR server must be restarted to apply the new configuration. Would you like to restart it now?"
                                        },
                                        MSG_RESET_SERVER_ALREADY_RUNNING = new string[]
                                        {
                                            "Restart OSVR Server?",
                                            "The OSVR server is already running. Would you like to restart it?"
                                        },
                                        MSG_START_SERVER = new string[]
                                        {
                                            "Start OSVR Server?",
                                            "The OSVR server is not currently running. The new configuration will be applied the next time the server is started. Would you like to start it now?"
                                        },
                                        MSG_TRAYAPP_ALREADY_RUNNING = new string[]
                                        {
                                            "HDK Tray Application Already Running",
                                            "The HDK Tray Application is already running in the notification area (system tray)."
                                        },
                                        MSG_SERVER_ALREADY_RUNNING_UMANAGED = new string[]
                                        {
                                            "OSVR Server Already Running",
                                            "A standalone instance of the OSVR Server is already running. To run an instance of the OSVR Server which is managed by the HDK Tray Application, right click on the OSVR tray icon and select Restart Server."
                                        },
                                        MSG_TRAYAPP_AND_SERVER_ALREADY_RUNNING = new string[]
                                        {
                                            "HDK Tray Application And Server Already Running",
                                            "The HDK Tray Application is already running in the notification area (system tray), and the OSVR Server is already running as well."
                                        },
                                        MSG_SELECT_GPU_TYPE = new string[]
                                        {
                                            "Select Graphics Card",
                                            "Please select your graphics card type through the menu before setting your HDK's display mode."
                                        },
                                        MSG_GPU_TYPE_DIFFERS = new string[]
                                        {
                                            "Graphics Card Type Updated",
                                            "The currently selected graphics card type does not match the graphics card type detected on your system. The setting has been updated."
                                        },
                                        MSG_SAMPLE_CRASH = new string[]
                                        {
                                            "Unable To Launch Sample Scene",
                                            "The VR sample scene has crashed. Please ensure the OSVR Server is running and that your HDK is connected as described in the manual. If the problem persists with this or other OSVR applications, we strongly recommend updating firmware to version 2.00 or newer, as this issue is common with older firmware releases."
                                        },
                                        MSG_UNABLE_TO_DETECT_HDK_TYPE = new string[]
                                        {
                                            "Unable To Detect HDK",
                                            "No HDK USB connection is detected. Please ensure that your HDK is connected as described in the manual.\n\nTo bypass USB connectivity verification, select a custom OSVR Server configuration file. If you do so, applications may not be able to run on your HDK."
                                        },
                                        MSG_MISSING_CUSTOM_CONFIG = new string[]
                                        {
                                            "Unable To Start OSVR Server",
                                            "A custom configuration is selected but the file cannot be loaded. Please select a new configuration under the options menu."
                                        },
                                        MSG_UNABLE_TO_STOP_SERVER = new string[]
                                        {
                                            "Unable To Stop OSVR Server",
                                            "An error has occurred while attempting to stop the OSVR Server. Please terminate it from the Task Manager before starting it again."
                                        },
                                        MSG_ERROR_LOCATING_STEAMVR_CONFIG = new string[]
                                        {
                                            "Unable To Update SteamVR Configuration",
                                            "An error has occurred while locating the SteamVR configuration file. Ensure that Steam is installed correctly, run SteamVR once with your HDK plugged in, and try again."
                                            // TODO: [OSVI-159] will this always be here? or do we need to instruct the user to plug in hmd or something first
                                        },
                                        MSG_ERROR_READING_STEAMVR_CONFIG = new string[]
                                        {
                                            "Unable To Update SteamVR Configuration",
                                            "An error has occurred while reading your existing SteamVR configuration file. Please ensure that your existing file is not corrupted and that it is valid JSON."
                                            // TODO: [OSVI-159] provide an example or link to steamvr wiki for more information? Point them to our docs? Tell them to just delete file if there's an issue?
                                        },
                                        MSG_ERROR_UPDATING_STEAMVR_CONFIG = new string[]
                                        {
                                            "Unable To Update SteamVR Settings",
                                            "Unable to update SteamVR's settings file (steamvr.vrsettings). Please exit Steam and try again."
                                        },
                                        MSG_STEAMVR_CONFIG_UPDATED = new string[]
                                        {
                                            "SteamVR Configuration Updated",
                                            "Your SteamVR configuration has been updated. Please restart SteamVR if it is currently running."
                                        },
                                        MSG_STEAMVR_OSVR_LEGACY_DRIVER_DETECTED = new string[]
                                        {
                                            "Legacy SteamVR-OSVR Driver Detected",
                                            @"An old version of the SteamVR-OSVR driver has been detected in the SteamVR drivers directory (<Steam root>\steamapps\common\SteamVR\drivers).\n\nIt may override the driver installed with the HDK Software Suite. Please move or remove that directory."
                                        },
                                        MSG_MISSING_EM_EXE = new string[]
                                        {
                                            "Unable To Set Extended Mode",
                                            "The executable used to set extended mode cannot be located. Please reinstall."
                                        },
                                        MSG_MISSING_DM_EXE = new string[]
                                        {
                                            "Unable To Set Direct Mode",
                                            "The executable used to set direct mode cannot be located. Please reinstall."
                                        },
                                        MSG_MISSING_FWU = new string[]
                                        {
                                            "Unable To Launch HDK Firmware Utility",
                                            "The HDK Firmware Utility cannot be found. Please reinstall."
                                        },
                                        MSG_MISSING_TRACKER_VIEW = new string[]
                                        {
                                            "Unable To Launch Tracker View",
                                            "The OSVR Tracker View utility cannot be found. Please reinstall."
                                        },
                                        MSG_MISSING_SAMPLE_SCENE = new string[]
                                        {
                                            "Unable To Launch HDK Sample Scene",
                                            "The HDK Sample Scene cannot be found. Please reinstall."
                                        },
                                        MSG_MISSING_OR_CANCELLED_IR_CAM_FW_UPDATER = new string[]
                                        {
                                            "Unable To Launch IR Camera Firmware Updater",
                                            "The IR Camera Firmware Updater cannot be found or was cancelled.\n\n" +                                            
                                            "You can manually download and run the updater from https://osvr.github.io/using/.\n\n" +
                                            "If you're having trouble updating your IR camera's firmware, try connecting it to a USB port " +
                                            "on your computer itself rather than a hub such as those found on monitors or keyboards."
                                        },
                                        MSG_UNABLE_TO_START_SERVER = new string[]
                                        {
                                            "Unable To Launch OSVR Server",
                                            "The OSVR Server cannot be launched. If you've modified your installation directory, please reinstall, otherwise please refer to the online documentation."
                                        };

        public static readonly string OSVR_HELP_URL = "http://www.osvr.org/hdk2help";

        public static DialogResult ShowMessageBox(string[] title_and_message, MessageBoxButtons buttons, MessageBoxIcon icon, bool modal = false)
        {
            if (modal)
                return MessageBox.Show(title_and_message[MSG_BODY], title_and_message[MSG_TITLE], buttons, icon, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            else
                return MessageBox.Show(title_and_message[MSG_BODY], title_and_message[MSG_TITLE], buttons, icon);
        }
    }
}
