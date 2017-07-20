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
    partial class PromptConsoleClosing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PromptConsoleClosing));
            this.consoleClosingLabel = new System.Windows.Forms.Label();
            this.consoleClosingOKButton = new System.Windows.Forms.Button();
            this.consoleClosingDontAskAgainCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // consoleClosingLabel
            // 
            this.consoleClosingLabel.Location = new System.Drawing.Point(13, 13);
            this.consoleClosingLabel.Name = "consoleClosingLabel";
            this.consoleClosingLabel.Size = new System.Drawing.Size(419, 56);
            this.consoleClosingLabel.TabIndex = 0;
            this.consoleClosingLabel.Text = resources.GetString("consoleClosingLabel.Text");
            // 
            // consoleClosingOKButton
            // 
            this.consoleClosingOKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.consoleClosingOKButton.Location = new System.Drawing.Point(354, 72);
            this.consoleClosingOKButton.Name = "consoleClosingOKButton";
            this.consoleClosingOKButton.Size = new System.Drawing.Size(75, 23);
            this.consoleClosingOKButton.TabIndex = 1;
            this.consoleClosingOKButton.Text = "OK";
            this.consoleClosingOKButton.UseVisualStyleBackColor = true;
            this.consoleClosingOKButton.Click += new System.EventHandler(this.consoleClosingOKButton_Click);
            // 
            // consoleClosingDontAskAgainCheckbox
            // 
            this.consoleClosingDontAskAgainCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.consoleClosingDontAskAgainCheckbox.AutoSize = true;
            this.consoleClosingDontAskAgainCheckbox.Location = new System.Drawing.Point(257, 101);
            this.consoleClosingDontAskAgainCheckbox.Name = "consoleClosingDontAskAgainCheckbox";
            this.consoleClosingDontAskAgainCheckbox.Size = new System.Drawing.Size(172, 17);
            this.consoleClosingDontAskAgainCheckbox.TabIndex = 2;
            this.consoleClosingDontAskAgainCheckbox.Text = "Don\'t show this message again";
            this.consoleClosingDontAskAgainCheckbox.UseVisualStyleBackColor = true;
            // 
            // PromptConsoleClosing
            // 
            this.AcceptButton = this.consoleClosingOKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 123);
            this.Controls.Add(this.consoleClosingDontAskAgainCheckbox);
            this.Controls.Add(this.consoleClosingOKButton);
            this.Controls.Add(this.consoleClosingLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PromptConsoleClosing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OSVR Server Still Running";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label consoleClosingLabel;
        private System.Windows.Forms.Button consoleClosingOKButton;
        private System.Windows.Forms.CheckBox consoleClosingDontAskAgainCheckbox;
    }
}