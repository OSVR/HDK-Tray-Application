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

using Microsoft.Win32;
using System;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace HDK_TrayApp
{
    public class SteamVRConfig
    {
        private static readonly string DRIVER_OSVR_JSON_BLOCK_KEY = "driver_osvr",
                                       ORIGIN_KEY = "scanoutOrigin",
                                       INVERTED_VALUE = "lower-right";
        private static readonly bool DRIVER_OSVR_VERBOSE = false;

        public enum ConfigChange { Invert }
        public enum SteamVRInversion { Unknown, NotSet, Standard, Inverted }

        #region Steam Path
        /// <summary>
        /// Locate the Steam installation root.
        /// The default location is C:\Program Files (x86)\Steam.
        /// Steam's root directory can be found under the registry key HKEY_CURRENT_USER\Software\Valve\Steam\SteamPath.
        /// </summary>
        /// <returns>Path to Steam installation root.</returns>
        private static string SteamRootPath()
        {
            // Open Steam registry keys
            RegistryKey k = Registry.CurrentUser;
            k = k.OpenSubKey(@"Software\Valve\Steam");
            if (k == null)
                return null;

            // Load Steam install path
            object result = k.GetValue("SteamPath");
            if (result == null)
                return null;

            // Convert path separators
            return result.ToString().Replace("/", @"\");
        }

        /// <summary>
        /// Locate the SteamVR settings file from the Steam directory.
        /// The default location is C:\Program Files (x86)\Steam\config\steamvr.vrsettings.
        /// </summary>
        /// <returns>Path to the SteamVR settings file</returns>
        private static string SteamVRSettingsFilePath()
        {
            // Start at Steam installation root
            string steam_path = SteamRootPath();
            if (steam_path == null)
                return null;

            // Append remainder of path to get to vrsettings file
            string vr_settings_path = steam_path + @"\config\steamvr.vrsettings";
            if (!File.Exists(vr_settings_path))
                return null;

            return vr_settings_path;
        }

        private static string SteamVRDriverPath()
        {
            // Start at Steam installation root
            string steam_path = SteamRootPath();
            if (steam_path == null)
                return null;

            // Append remainder of path to get to driver directory path
            string driver_path = steam_path + @"\steamapps\common\SteamVR\drivers";
            if (!Directory.Exists(driver_path))
                return null;

            return driver_path;
        }
        #endregion

        #region JSON Update
        /// <summary>
        /// Update SteamVR.vrsettings file as requested
        /// </summary>
        /// <param name="request">Requested settings change</param>
        /// <returns>Whether update was successful</returns>
        public static bool UpdateSteamVRConfigFile(ConfigChange request)
        {
            string vr_settings_path = SteamVRSettingsFilePath();
            if (vr_settings_path == null)
            {
                Common.ShowMessageBox(Common.MSG_ERROR_LOCATING_STEAMVR_CONFIG, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }

            string existing_configuration_json = File.ReadAllText(vr_settings_path);
            if (existing_configuration_json == null)
            {
                Common.ShowMessageBox(Common.MSG_ERROR_READING_STEAMVR_CONFIG, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }

            string updated_configuration_json = UpdateSteamVRConfigJSON(existing_configuration_json, request);
            File.WriteAllText(vr_settings_path, updated_configuration_json);

            if (File.ReadAllText(vr_settings_path) != updated_configuration_json)
            {
                Common.ShowMessageBox(Common.MSG_ERROR_UPDATING_STEAMVR_CONFIG, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                Common.ShowMessageBox(Common.MSG_STEAMVR_CONFIG_UPDATED, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
        }

        /// <summary>
        /// Update the JSON from SteamVR's configuration file (steamvr.vrsettings).
        /// </summary>
        /// <param name="json_str">Current configuration JSON string</param>
        /// <param name="request">Requested configuration change</param>
        /// <returns></returns>
        private static string UpdateSteamVRConfigJSON(string json_str, ConfigChange request)
        {
            // Parse settings file
            JObject json;
            try
            {
                json = JObject.Parse(json_str);
            }
            catch (Exception)
            {
                return null;
            }

            switch (request)
            {
                case ConfigChange.Invert:
                    {
                        // Create or overwrite driver_osvr block
                        JToken token;
                        if (json.TryGetValue(DRIVER_OSVR_JSON_BLOCK_KEY, out token))
                        {
                            JObject driver_osvr_json = (JObject)token;
                            ToggleDriverOSVRDisplayInverted(driver_osvr_json);
                        }
                        else
                        {
                            JObject driver_osvr_json = new JObject();
                            ToggleDriverOSVRDisplayInverted(driver_osvr_json);
                            json.Add(DRIVER_OSVR_JSON_BLOCK_KEY, driver_osvr_json);
                        }
                    }
                    break;
            }
            
            return json.ToString();
        }

        /// <summary>
        /// Update driver_osvr block (inversion, verbose mode)
        /// </summary>
        /// <param name="driver_osvr_json">JSON block to modify</param>
        private static void ToggleDriverOSVRDisplayInverted(JObject driver_osvr_json)
        {
            bool invert = true;
            var scanout_origin = driver_osvr_json.Property(ORIGIN_KEY);
            if (scanout_origin != null)
                invert = (string)scanout_origin.Value != INVERTED_VALUE;

            if (invert)
                driver_osvr_json[ORIGIN_KEY] = INVERTED_VALUE;
            else
                driver_osvr_json.Remove(ORIGIN_KEY);

            driver_osvr_json["verbose"] = DRIVER_OSVR_VERBOSE;
        }

        /// <summary>
        /// Check whether OSVR rendering through SteamVR is inverted
        /// </summary>
        /// <returns>Whether it's inverted</returns>
        public static SteamVRInversion InversionState()
        {
            string vr_settings_path = SteamVRSettingsFilePath();
            if (vr_settings_path == null)
                return SteamVRInversion.Unknown;

            string json_str = File.ReadAllText(vr_settings_path);
            if (json_str == null)
                return SteamVRInversion.Unknown;

            JObject json;
            try
            {
                json = JObject.Parse(json_str);
            }
            catch (Exception)
            {
                return SteamVRInversion.Unknown;
            }
            
            JToken token;
            if (json.TryGetValue(DRIVER_OSVR_JSON_BLOCK_KEY, out token))
            {
                JObject driver_osvr_json = (JObject)token;

                var scanout_origin = driver_osvr_json.Property(ORIGIN_KEY);
                if (scanout_origin == null)
                    return SteamVRInversion.NotSet;
                else if ((string)scanout_origin.Value == INVERTED_VALUE)
                    return SteamVRInversion.Inverted;
                else
                    return SteamVRInversion.Standard;
            }
            else
                return SteamVRInversion.NotSet;
        }
        #endregion

        public static bool IsLegacyOSVRDriverInstalled()
        {
            return Directory.Exists(SteamVRDriverPath() + @"\osvr");
        }
    }
}
