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

namespace HDK_TrayApp
{
    /// <summary>
    /// Provides functionality for reading important OSVR
    /// info from the Windows registry.
    /// </summary>
    class OSVRRegistry
    {
        /// <summary>
        /// Get the root install directory for OSVR.
        /// </summary>
        /// <returns> Absolute path to the root OSVR install directory. </returns>
        public static string GetInstallDirectoryFromRegistry()
        {
            string installDirectory = string.Empty;

            /// First try to get the directory from the environment variable so as to keep
            /// consistent with OSVR-Server. If null, then check registry.
            installDirectory = Environment.GetEnvironmentVariable(Common.ENVIRONMENT_VAR_INSTALL_DIRECTORY);

            if (installDirectory == null)
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(Common.REGISTRY_SUB_KEY, false);

                if (registryKey != null)
                {
                    object registryValue = registryKey.GetValue(Common.REGISTRY_INSTALL_DIRECTORY_KEY);

                    if (registryKey.GetValueKind(Common.REGISTRY_INSTALL_DIRECTORY_KEY) == RegistryValueKind.String)
                    {
                        installDirectory = registryValue as string;
                    }
                }
            }

            return installDirectory;
        }

        public static string GetInstalledVersion()
        {
            string installedVersion = string.Empty;
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(Common.REGISTRY_SUB_KEY, false);

            if (registryKey != null)
            {
                object registryValue = registryKey.GetValue(Common.REGISTRY_VERSION_KEY);

                if (registryKey.GetValueKind(Common.REGISTRY_VERSION_KEY) == RegistryValueKind.String)
                {
                    installedVersion = registryValue as string;
                }
            }
            return installedVersion;
        }
    }
}
