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

#if DEBUG
// #define RUN_TESTRAIL
#endif

#if (RUN_TESTRAIL)
using HDK_TrayApp.Testing;
#endif

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace HDK_TrayApp
{
    static class Program
    {
        private static readonly string APP_GUID = "70419401-3f58-48a7-8ebb-b2afd20338b0";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Visual setup
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine("Unable to set text compatible text rendering default: " + e.Message + "\n" + e.StackTrace);
            }

            // If run with -startserver flag, try to start the server; if the server is already running, ignore this flag
            bool start_server_arg = args.Length == 1 && args[0] == "-startserver";

            // Check whether the OSVR Server is already running
            bool server_running = OSVRProcessManager.ProcessInstanceIsRunning(Common.SERVICE_NAME);

            // Check whether another instance of the TrayApp is already running
            Process existing_trayapp_instance = OSVRProcessManager.ExistingTrayAppProcess();

            bool trayapp_running = existing_trayapp_instance != null;

            // On Win7x64 Pro and potentially other operating systems, two instances of this application are for some reason
            // being launched on startup, despite only one entry existing in msconfig's startups tab.  See OSVI-201 for details.
            //
            // When this situation occurs, this block will detect it and terminate the instance with the higher process ID.
            // This could also be solved with the sort of global system lock that used to be present here, but that had other issues.
            if (trayapp_running &&
                DateTime.Now - existing_trayapp_instance.StartTime < TimeSpan.FromSeconds(1d))
            {
                if (existing_trayapp_instance.Id < Process.GetCurrentProcess().Id)
                {
                    Debug.WriteLine("Two instances launched at nearly the same time; terminating this one because it has a higher process ID.");
                    return;
                }
                else
                {
                    Debug.WriteLine("Two instances launched at nearly the same time; terminating other one because it has a higher process ID.");
                    trayapp_running = false;
                }
            }

            /*
             * trayapp_running, start_server_arg, server_running:
             * no, yes, yes -> show message saying unmanaged server already running
             * no, no, yes -> show message saying unmanaged server already running
             * no, no, no -> vanilla
             * yes, yes, no -> terminate existing instance, launch server
             * no, yes, no -> launch server
             * yes, yes, yes -> terminate this instance, show message saying both server and trayapp are already running
             * yes, no, yes -> terminate this instance, show message saying trayapp already running
             * yes, no, no ->  terminate this instance, show message saying trayapp already running
             */

            bool terminate_this_instance = trayapp_running && (!start_server_arg || server_running);
            bool terminate_existing_instance = trayapp_running && start_server_arg && !server_running;
            bool launch_server = start_server_arg && !server_running;
            bool msg_unmanaged_server_already_running = !trayapp_running && server_running;
            bool msg_trayapp_already_running = trayapp_running && !start_server_arg;
            bool msg_both_already_running = trayapp_running && start_server_arg && server_running;
            
            /// No longer needed warning as server-manager has fault-tolerance
            /*if (msg_unmanaged_server_already_running)
                Common.ShowMessageBox(Common.MSG_SERVER_ALREADY_RUNNING_UMANAGED, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            */

            if (msg_trayapp_already_running)
                Common.ShowMessageBox(Common.MSG_TRAYAPP_ALREADY_RUNNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (msg_both_already_running)
                Common.ShowMessageBox(Common.MSG_TRAYAPP_AND_SERVER_ALREADY_RUNNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (terminate_existing_instance)
            {
                try
                {
                    existing_trayapp_instance.Kill();
                }
                catch
                {
                    Debug.WriteLine("Unable to kill existing TrayApp process with PID " + existing_trayapp_instance.Id + "!");
                }
            }

            if (terminate_this_instance)
                return;

#if (RUN_TESTRAIL)
                CommonTests.Run();
#endif

            using (OSVRIcon osvrIcon = new OSVRIcon())
            {
                osvrIcon.Display(launch_server);
                Application.Run();
            }
        }

    }
}
