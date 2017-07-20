// #define DBG_PRINT_DISPLAY_ENUM

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
using System.Runtime.InteropServices;

namespace HDK_TrayApp
{
    class NativeHelpers
    {
        [DllImport("user32.dll")]
        static extern int ChangeDisplaySettingsEx(string lpszDeviceName, ref DEVMODE lpDevMode, IntPtr hwnd, uint dwflags, IntPtr lParam);

        //http://stackoverflow.com/questions/23407024/enumdisplaydevices-return-value-is-generic-pnp-monitor-c-sharp
        [DllImport("user32.dll")]
        public static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

        [Flags()]
        public enum DisplayDeviceStateFlags : int
        {
            /// <summary>The device is part of the desktop.</summary>
            AttachedToDesktop = 0x1,
            MultiDriver = 0x2,
            /// <summary>The device is part of the desktop.</summary>
            PrimaryDevice = 0x4,
            /// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes.</summary>
            MirroringDriver = 0x8,
            /// <summary>The device is VGA compatible.</summary>
            VGACompatible = 0x16,
            /// <summary>The device is removable; it cannot be the primary display.</summary>
            Removable = 0x20,
            /// <summary>The device has more display modes than its output devices support.</summary>
            ModesPruned = 0x8000000,
            Remote = 0x4000000,
            Disconnect = 0x2000000
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DISPLAY_DEVICE
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cb;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DeviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceString;
            [MarshalAs(UnmanagedType.U4)]
            public DisplayDeviceStateFlags StateFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceKey;
        }

        //https://msdn.microsoft.com/en-us/library/ms812499.aspx
        // PInvoke declaration for EnumDisplaySettings Win32 API
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern int EnumDisplaySettings( string lpszDeviceName,
                                                      int iModeNum,
                                                      ref DEVMODE lpDevMode);

        // PInvoke declaration for ChangeDisplaySettings Win32 API
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern int ChangeDisplaySettings(ref DEVMODE lpDevMode,
                                                       int dwFlags);

        // add more functions as needed …

        // constants
        public const int ENUM_CURRENT_SETTINGS = -1;
        public const int ENUM_REGISTRY_SETTINGS = -2;
        public const int ENUM_DISPLAY_SETTINGS_FAILURE = 0;
        public const int DMDO_DEFAULT = 0;
        public const int DMDO_90 = 1;
        public const int DMDO_180 = 2;
        public const int DMDO_270 = 3;
        public const int DISP_CHANGE_SUCCESSFUL = 0;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DEVMODE
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmDeviceName;

            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmFormName;

            public short dmLogPixels;
            public short dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        };

        private static DEVMODE CreateDevmode()
        {
            DEVMODE dm = new DEVMODE();
            dm.dmDeviceName = new String(new char[32]);
            dm.dmFormName = new String(new char[32]);
            dm.dmSize = (short)Marshal.SizeOf(dm);
            return dm;
        }

        public static bool IsDeviceRotationCorrect()
        {
            DEVMODE devMode = CreateDevmode();
            string deviceName = GetOSVRDisplayName();

            if (!string.IsNullOrEmpty(deviceName) && EnumDisplaySettings(deviceName, ENUM_CURRENT_SETTINGS, ref devMode) != ENUM_DISPLAY_SETTINGS_FAILURE)
            {
                int orientation = devMode.dmDisplayOrientation;
                int width = devMode.dmPelsWidth;
                int height = devMode.dmPelsHeight;

                // HDK 2
                if (orientation == DMDO_180 && width == 2160 && height == 1200)
                    return true;

                // HDK 1
                if (orientation == DMDO_90 && width == 1920 && height == 1080)
                    return true;

                return false;
            }

            throw new Exception("Unable to detect device rotation!");
        }

        public static bool CorrectDeviceRotation()
        {
            DEVMODE devMode = CreateDevmode();
            string deviceName = GetOSVRDisplayName();

            if (!string.IsNullOrEmpty(deviceName) && EnumDisplaySettings(deviceName, ENUM_CURRENT_SETTINGS, ref devMode) != ENUM_DISPLAY_SETTINGS_FAILURE)
            {
                int orientation = devMode.dmDisplayOrientation;
                int width = devMode.dmPelsWidth;
                int height = devMode.dmPelsHeight;

                if (width == 2160 || height == 2160)
                {
                    devMode.dmDisplayOrientation = DMDO_180;
                    devMode.dmPelsWidth = 2160;
                    devMode.dmPelsHeight = 1200;
                }
                else if (width == 1920 || height == 1920)
                {
                    devMode.dmDisplayOrientation = DMDO_90;
                    devMode.dmPelsWidth = 1920;
                    devMode.dmPelsHeight = 1080;
                }
                else
                    return false;

                int settingsChangeStatus = ChangeDisplaySettingsEx(deviceName, ref devMode, IntPtr.Zero, 1, IntPtr.Zero);

                return DISP_CHANGE_SUCCESSFUL == settingsChangeStatus;
            }

            return false;
        }

        private static DISPLAY_DEVICE GetOSVRDisplay()
        {
            var displayAdapter = new DISPLAY_DEVICE();
            displayAdapter.cb = Marshal.SizeOf(displayAdapter);

            for (uint i = 0; ; ++i)
            {
                if (EnumDisplayDevices(null, i, ref displayAdapter, 0))
                {
#if (DBG_PRINT_DISPLAY_ENUM)
                    Debug.WriteLine("Display adapter name: " + displayAdapter.DeviceName);
                    Debug.WriteLine("Display adapter string: " + displayAdapter.DeviceString);
#endif

                    string displayAdapterName = displayAdapter.DeviceName;

                    for (uint j = 0; ; ++j)
                    {
                        var currentMonitor = new DISPLAY_DEVICE();
                        currentMonitor.cb = Marshal.SizeOf(currentMonitor);

                        if (EnumDisplayDevices(displayAdapterName, j, ref currentMonitor, 0))
                        {
#if (DBG_PRINT_DISPLAY_ENUM)
                            Debug.WriteLine("Display device name: " + currentMonitor.DeviceName);
                            Debug.WriteLine("Display device string: " + currentMonitor.DeviceString);
                            Debug.WriteLine("Display device ID: " + currentMonitor.DeviceID);
                            Debug.WriteLine("Display device key: " + currentMonitor.DeviceKey);
                            Debug.WriteLine("\tState Flags for : " + currentMonitor.DeviceString);
                            foreach (DisplayDeviceStateFlags flag in Enum.GetValues(typeof(DisplayDeviceStateFlags)))
                            {
                                string isEnabled = ((currentMonitor.StateFlags & flag) != 0) ? "true" : "false";
                                Debug.WriteLine("\t\t" + flag.ToString() + " is " + isEnabled);
                            }
#endif

                            // Note: this method only works if the drivers are installed to properly show the HDK's display name.
                            // There are a few cases:
                            // * HDK 2 with 1.01 firmware, and drivers installed, recognized as "OSVR HDK 2 Head-Mounted Display"
                            // * HDK 2 with other firmware, and drivers installed, recognized as "OSVR HDK Head-Mounted Display"
                            // * HDK 1, and drivers installed, recognized as "OSVR HDK Head-Mounted Display"
                            // * Without drivers installed, both are recognized as "Generic PnP Monitor"
                            /*if (currentMonitor.DeviceString.Contains("OSVR"))
                            {
                                Debug.WriteLine("HDK found by DeviceString!");
                                return currentMonitor.DeviceName;
                            }*/

                            if (currentMonitor.DeviceID.StartsWith(@"MONITOR\AUO1111") || currentMonitor.DeviceID.StartsWith(@"MONITOR\SVR1019"))
                            {
#if (DBG_PRINT_DISPLAY_ENUM)
                                Debug.WriteLine("HDK found by DeviceID!");
#endif
                                return currentMonitor;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            throw new Exception("Unable to locate HDK!");
        }

        private static string GetOSVRDisplayName()
        {
            try
            {
                DISPLAY_DEVICE currentMonitor = GetOSVRDisplay();
                return currentMonitor.DeviceName.Replace("\\Monitor0", "");
            }
            catch
            {
                return null;
            }
        }

        public static bool IsExtendedModeEnabled()
        {
            try
            {
                GetOSVRDisplay();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
