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
using System.Windows.Forms;

namespace HDK_TrayApp
{
    public partial class PromptStartServerConsoleOpening : Form
    {
        private ServerManager m_server;

        public PromptStartServerConsoleOpening(ServerManager serverManager)
        {
            InitializeComponent();

            m_server = serverManager;
        }

        private void serverNotRunningStartButton_Click(object sender, EventArgs e)
        {
            m_server.StartServer();

            if (serverNotRunningDontAskAgainCheckbox.Checked)
            {
                Properties.Settings.Default.promptServerConsoleOpening = false;
                Properties.Settings.Default.shouldStartServerConsoleOpening = true;
                Properties.Settings.Default.Save();
            }

            Hide();
        }

        private void serverNotRunningDontStartButton_Click(object sender, EventArgs e)
        {
            if (serverNotRunningDontAskAgainCheckbox.Checked)
            {
                Properties.Settings.Default.promptServerConsoleOpening = false;
                Properties.Settings.Default.shouldStartServerConsoleOpening = false;
                Properties.Settings.Default.Save();
            }

            Hide();
        }
    }
}
