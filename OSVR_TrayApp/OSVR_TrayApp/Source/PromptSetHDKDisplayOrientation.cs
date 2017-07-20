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
    public partial class PromptSetHDKDisplayOrientation : Form
    {
        public ContextMenuWYSIWYG m_contextMenu;

        public PromptSetHDKDisplayOrientation()
        {
            InitializeComponent();
        }

        private void promptSetHDKDisplayRotationLabelYes_Click(object sender, EventArgs e)
        {
            NativeHelpers.CorrectDeviceRotation();

            if (promptHDKDisplayOrientationDontAskAgain.Checked)
            {
                Properties.Settings.Default.promptSetLandscapeDisplayOrientation = false;
                Properties.Settings.Default.shouldSetLandscapeDisplayOrientation = true;
                Properties.Settings.Default.Save();
            }

            Hide();

            m_contextMenu.PromptServerStartOrRestartDelegate();
        }

        private void promptSetHDKDisplayRotationLabelNo_Click(object sender, EventArgs e)
        {
            if (promptHDKDisplayOrientationDontAskAgain.Checked)
            {
                Properties.Settings.Default.promptSetLandscapeDisplayOrientation = false;
                Properties.Settings.Default.shouldSetLandscapeDisplayOrientation = false;
                Properties.Settings.Default.Save();
            }

            Hide();

            m_contextMenu.PromptServerStartOrRestartDelegate();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                m_contextMenu.PromptServerStartOrRestartDelegate();

            base.OnFormClosed(e);
        }
    }
}
