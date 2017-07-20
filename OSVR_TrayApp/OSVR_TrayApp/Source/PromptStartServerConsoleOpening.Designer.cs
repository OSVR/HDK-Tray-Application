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

namespace HDK_TrayApp
{
    partial class PromptStartServerConsoleOpening
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PromptStartServerConsoleOpening));
            this.serverNotRunningLabel = new System.Windows.Forms.Label();
            this.serverNotRunningStartButton = new System.Windows.Forms.Button();
            this.serverNotRunningDontStartButton = new System.Windows.Forms.Button();
            this.serverNotRunningDontAskAgainCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // serverNotRunningLabel
            // 
            this.serverNotRunningLabel.Location = new System.Drawing.Point(12, 9);
            this.serverNotRunningLabel.Name = "serverNotRunningLabel";
            this.serverNotRunningLabel.Size = new System.Drawing.Size(357, 23);
            this.serverNotRunningLabel.TabIndex = 0;
            this.serverNotRunningLabel.Text = "The OSVR Server is not currently running.  Would you like to start it?";
            // 
            // serverNotRunningStartButton
            // 
            this.serverNotRunningStartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.serverNotRunningStartButton.Location = new System.Drawing.Point(213, 35);
            this.serverNotRunningStartButton.Name = "serverNotRunningStartButton";
            this.serverNotRunningStartButton.Size = new System.Drawing.Size(75, 23);
            this.serverNotRunningStartButton.TabIndex = 1;
            this.serverNotRunningStartButton.Text = "Yes";
            this.serverNotRunningStartButton.UseVisualStyleBackColor = true;
            this.serverNotRunningStartButton.Click += new System.EventHandler(this.serverNotRunningStartButton_Click);
            // 
            // serverNotRunningDontStartButton
            // 
            this.serverNotRunningDontStartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.serverNotRunningDontStartButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.serverNotRunningDontStartButton.Location = new System.Drawing.Point(294, 35);
            this.serverNotRunningDontStartButton.Name = "serverNotRunningDontStartButton";
            this.serverNotRunningDontStartButton.Size = new System.Drawing.Size(75, 23);
            this.serverNotRunningDontStartButton.TabIndex = 2;
            this.serverNotRunningDontStartButton.Text = "No";
            this.serverNotRunningDontStartButton.UseVisualStyleBackColor = true;
            this.serverNotRunningDontStartButton.Click += new System.EventHandler(this.serverNotRunningDontStartButton_Click);
            // 
            // serverNotRunningDontAskAgainCheckbox
            // 
            this.serverNotRunningDontAskAgainCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.serverNotRunningDontAskAgainCheckbox.AutoSize = true;
            this.serverNotRunningDontAskAgainCheckbox.Location = new System.Drawing.Point(241, 64);
            this.serverNotRunningDontAskAgainCheckbox.Name = "serverNotRunningDontAskAgainCheckbox";
            this.serverNotRunningDontAskAgainCheckbox.Size = new System.Drawing.Size(100, 17);
            this.serverNotRunningDontAskAgainCheckbox.TabIndex = 3;
            this.serverNotRunningDontAskAgainCheckbox.Text = "Don\'t ask again";
            this.serverNotRunningDontAskAgainCheckbox.UseVisualStyleBackColor = true;
            // 
            // PromptStartServerConsoleOpening
            // 
            this.AcceptButton = this.serverNotRunningStartButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.serverNotRunningDontStartButton;
            this.ClientSize = new System.Drawing.Size(374, 84);
            this.Controls.Add(this.serverNotRunningDontAskAgainCheckbox);
            this.Controls.Add(this.serverNotRunningDontStartButton);
            this.Controls.Add(this.serverNotRunningStartButton);
            this.Controls.Add(this.serverNotRunningLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PromptStartServerConsoleOpening";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OSVR Server Not Running";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label serverNotRunningLabel;
        private System.Windows.Forms.Button serverNotRunningStartButton;
        private System.Windows.Forms.Button serverNotRunningDontStartButton;
        private System.Windows.Forms.CheckBox serverNotRunningDontAskAgainCheckbox;
    }
}