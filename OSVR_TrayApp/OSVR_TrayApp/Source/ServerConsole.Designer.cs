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
    partial class ServerConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerConsole));
            this.serverConsoleTextBox = new System.Windows.Forms.RichTextBox();
            this.autoScrollCheckBox = new System.Windows.Forms.CheckBox();
            this.copyToClipboardButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.saveAsButton = new System.Windows.Forms.Button();
            this.startServerButton = new System.Windows.Forms.Button();
            this.restartServerButton = new System.Windows.Forms.Button();
            this.stopServerButton = new System.Windows.Forms.Button();
            this.errorHighlightCheckbox = new System.Windows.Forms.CheckBox();
            this.recenterButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serverConsoleTextBox
            // 
            this.serverConsoleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverConsoleTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.serverConsoleTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverConsoleTextBox.HideSelection = false;
            this.serverConsoleTextBox.Location = new System.Drawing.Point(13, 41);
            this.serverConsoleTextBox.Name = "serverConsoleTextBox";
            this.serverConsoleTextBox.ReadOnly = true;
            this.serverConsoleTextBox.Size = new System.Drawing.Size(759, 479);
            this.serverConsoleTextBox.TabIndex = 0;
            this.serverConsoleTextBox.Text = "OSVR Server Log\n---";
            this.serverConsoleTextBox.WordWrap = false;
            // 
            // autoScrollCheckBox
            // 
            this.autoScrollCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.autoScrollCheckBox.AutoSize = true;
            this.autoScrollCheckBox.Checked = true;
            this.autoScrollCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoScrollCheckBox.Location = new System.Drawing.Point(655, 530);
            this.autoScrollCheckBox.Name = "autoScrollCheckBox";
            this.autoScrollCheckBox.Size = new System.Drawing.Size(117, 17);
            this.autoScrollCheckBox.TabIndex = 1;
            this.autoScrollCheckBox.Text = "Automatically Scroll";
            this.autoScrollCheckBox.UseVisualStyleBackColor = true;
            this.autoScrollCheckBox.CheckedChanged += new System.EventHandler(this.autoScrollCheckBox_CheckedChanged);
            // 
            // copyToClipboardButton
            // 
            this.copyToClipboardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.copyToClipboardButton.Location = new System.Drawing.Point(93, 526);
            this.copyToClipboardButton.Name = "copyToClipboardButton";
            this.copyToClipboardButton.Size = new System.Drawing.Size(108, 23);
            this.copyToClipboardButton.TabIndex = 2;
            this.copyToClipboardButton.Text = "Copy to Clipboard";
            this.copyToClipboardButton.UseVisualStyleBackColor = true;
            this.copyToClipboardButton.Click += new System.EventHandler(this.copyToClipboardButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clearButton.Location = new System.Drawing.Point(207, 526);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(91, 23);
            this.clearButton.TabIndex = 3;
            this.clearButton.Text = "Clear Console";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // saveAsButton
            // 
            this.saveAsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveAsButton.Location = new System.Drawing.Point(12, 526);
            this.saveAsButton.Name = "saveAsButton";
            this.saveAsButton.Size = new System.Drawing.Size(75, 23);
            this.saveAsButton.TabIndex = 4;
            this.saveAsButton.Text = "Save As...";
            this.saveAsButton.UseVisualStyleBackColor = true;
            this.saveAsButton.Click += new System.EventHandler(this.saveAsButton_Click);
            // 
            // startServerButton
            // 
            this.startServerButton.Location = new System.Drawing.Point(13, 12);
            this.startServerButton.Name = "startServerButton";
            this.startServerButton.Size = new System.Drawing.Size(74, 23);
            this.startServerButton.TabIndex = 5;
            this.startServerButton.Text = "Start Server";
            this.startServerButton.UseVisualStyleBackColor = true;
            this.startServerButton.Click += new System.EventHandler(this.startServerButton_Click);
            // 
            // restartServerButton
            // 
            this.restartServerButton.Location = new System.Drawing.Point(94, 12);
            this.restartServerButton.Name = "restartServerButton";
            this.restartServerButton.Size = new System.Drawing.Size(88, 23);
            this.restartServerButton.TabIndex = 6;
            this.restartServerButton.Text = "Restart Server";
            this.restartServerButton.UseVisualStyleBackColor = true;
            this.restartServerButton.Click += new System.EventHandler(this.restartServerButton_Click);
            // 
            // stopServerButton
            // 
            this.stopServerButton.Location = new System.Drawing.Point(188, 12);
            this.stopServerButton.Name = "stopServerButton";
            this.stopServerButton.Size = new System.Drawing.Size(78, 23);
            this.stopServerButton.TabIndex = 7;
            this.stopServerButton.Text = "Stop Server";
            this.stopServerButton.UseVisualStyleBackColor = true;
            this.stopServerButton.Click += new System.EventHandler(this.stopServerButton_Click);
            // 
            // errorHighlightCheckbox
            // 
            this.errorHighlightCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.errorHighlightCheckbox.AutoSize = true;
            this.errorHighlightCheckbox.Location = new System.Drawing.Point(552, 530);
            this.errorHighlightCheckbox.Name = "errorHighlightCheckbox";
            this.errorHighlightCheckbox.Size = new System.Drawing.Size(97, 17);
            this.errorHighlightCheckbox.TabIndex = 8;
            this.errorHighlightCheckbox.Text = "Highlight Errors";
            this.errorHighlightCheckbox.UseVisualStyleBackColor = true;
            this.errorHighlightCheckbox.Visible = false;
            this.errorHighlightCheckbox.CheckedChanged += new System.EventHandler(this.highlightErrorsCheckBox_CheckedChanged);
            // 
            // recenterButton
            // 
            this.recenterButton.Location = new System.Drawing.Point(696, 12);
            this.recenterButton.Name = "recenterButton";
            this.recenterButton.Size = new System.Drawing.Size(75, 23);
            this.recenterButton.TabIndex = 9;
            this.recenterButton.Text = "Recenter";
            this.recenterButton.UseVisualStyleBackColor = true;
            this.recenterButton.Click += new System.EventHandler(this.recenterButton_Click);
            // 
            // ServerConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.recenterButton);
            this.Controls.Add(this.errorHighlightCheckbox);
            this.Controls.Add(this.stopServerButton);
            this.Controls.Add(this.restartServerButton);
            this.Controls.Add(this.startServerButton);
            this.Controls.Add(this.saveAsButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.copyToClipboardButton);
            this.Controls.Add(this.autoScrollCheckBox);
            this.Controls.Add(this.serverConsoleTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ServerConsole";
            this.Text = "OSVR Server Console";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox serverConsoleTextBox;
        private System.Windows.Forms.CheckBox autoScrollCheckBox;
        private System.Windows.Forms.Button copyToClipboardButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button saveAsButton;
        private System.Windows.Forms.Button startServerButton;
        private System.Windows.Forms.Button restartServerButton;
        private System.Windows.Forms.Button stopServerButton;
        private System.Windows.Forms.CheckBox errorHighlightCheckbox;
        private System.Windows.Forms.Button recenterButton;
    }
}