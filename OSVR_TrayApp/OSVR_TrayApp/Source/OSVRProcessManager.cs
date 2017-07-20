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

namespace HDK_TrayApp
{
    class OSVRProcessManager
    {
        public static bool ProcessInstanceIsRunning(string processName)
        {
            Process[] processesByName = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(processName));
            return processesByName.Length > 0;
        }

        public static Process ExistingTrayAppProcess()
        {
            Process[] processesByName = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Common.TRAY_APP_NAME));

            if (processesByName.Length == 1)
                return null;

            foreach (Process p in processesByName)
            {
                if (p.Id == Process.GetCurrentProcess().Id)
                    continue;

                return p;
            }

            return null;
        }

        public static int KillProcessByName(string processName)
        {
            int killed = 0;

            foreach (Process p in Process.GetProcessesByName(Path.GetFileNameWithoutExtension(processName)))
            {
                try
                {
                    p.Kill();
                    killed++;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Unable to kill process (PID: " + p.Id + ", Name: " + processName + ")!\n" + e.Message + "\n" + e.StackTrace);
                }
            }

            return killed;
        }

        public static Process LaunchExecutable(string completeFilePath, string workingDirectory, ProcessWindowStyle windowStyle, string arguments, bool shell_execute = true)
        {
            if (File.Exists(completeFilePath))
            {
                try
                {
                    ProcessStartInfo exeStart = new ProcessStartInfo(completeFilePath);
                    exeStart.WorkingDirectory = workingDirectory;
                    exeStart.WindowStyle = windowStyle;

                    if (!shell_execute)
                    {
                        exeStart.RedirectStandardOutput = true;
                        exeStart.RedirectStandardError = true;
                        exeStart.UseShellExecute = false;
                        exeStart.CreateNoWindow = true;
                    }

                    if (!string.IsNullOrEmpty(arguments))
                        exeStart.Arguments = "\"" + arguments + "\"";

                    return Process.Start(exeStart);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine("Exception occured while attempting to launch executable at '" + completeFilePath + "':\n" + exception.ToString());
                }
            }
            else
                Debug.WriteLine("Executable not found!");

            return null;
        }
    }
}
