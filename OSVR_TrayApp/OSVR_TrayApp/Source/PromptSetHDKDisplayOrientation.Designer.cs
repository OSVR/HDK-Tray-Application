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
    partial class PromptSetHDKDisplayOrientation
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
            this.promptSetHDKDisplayRotationLabel = new System.Windows.Forms.Label();
            this.promptSetHDKDisplayRotationLabelYes = new System.Windows.Forms.Button();
            this.promptSetHDKDisplayRotationLabelNo = new System.Windows.Forms.Button();
            this.promptHDKDisplayOrientationDontAskAgain = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // promptSetHDKDisplayRotationLabel
            // 
            this.promptSetHDKDisplayRotationLabel.Location = new System.Drawing.Point(12, 9);
            this.promptSetHDKDisplayRotationLabel.Name = "promptSetHDKDisplayRotationLabel";
            this.promptSetHDKDisplayRotationLabel.Size = new System.Drawing.Size(352, 61);
            this.promptSetHDKDisplayRotationLabel.TabIndex = 0;
            this.promptSetHDKDisplayRotationLabel.Text = "Your HDK\'s display orientation is not currently set to Inverted Landscape mode. T" +
    "his may result in an image that appears upside down.\r\n\r\nWould you like to set it" +
    " to Inverted Landscape mode now?";
            // 
            // promptSetHDKDisplayRotationLabelYes
            // 
            this.promptSetHDKDisplayRotationLabelYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.promptSetHDKDisplayRotationLabelYes.Location = new System.Drawing.Point(201, 73);
            this.promptSetHDKDisplayRotationLabelYes.Name = "promptSetHDKDisplayRotationLabelYes";
            this.promptSetHDKDisplayRotationLabelYes.Size = new System.Drawing.Size(75, 23);
            this.promptSetHDKDisplayRotationLabelYes.TabIndex = 1;
            this.promptSetHDKDisplayRotationLabelYes.Text = "Yes";
            this.promptSetHDKDisplayRotationLabelYes.UseVisualStyleBackColor = true;
            this.promptSetHDKDisplayRotationLabelYes.Click += new System.EventHandler(this.promptSetHDKDisplayRotationLabelYes_Click);
            // 
            // promptSetHDKDisplayRotationLabelNo
            // 
            this.promptSetHDKDisplayRotationLabelNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.promptSetHDKDisplayRotationLabelNo.Location = new System.Drawing.Point(282, 73);
            this.promptSetHDKDisplayRotationLabelNo.Name = "promptSetHDKDisplayRotationLabelNo";
            this.promptSetHDKDisplayRotationLabelNo.Size = new System.Drawing.Size(75, 23);
            this.promptSetHDKDisplayRotationLabelNo.TabIndex = 2;
            this.promptSetHDKDisplayRotationLabelNo.Text = "No";
            this.promptSetHDKDisplayRotationLabelNo.UseVisualStyleBackColor = true;
            this.promptSetHDKDisplayRotationLabelNo.Click += new System.EventHandler(this.promptSetHDKDisplayRotationLabelNo_Click);
            // 
            // promptHDKDisplayOrientationDontAskAgain
            // 
            this.promptHDKDisplayOrientationDontAskAgain.AutoSize = true;
            this.promptHDKDisplayOrientationDontAskAgain.Location = new System.Drawing.Point(228, 102);
            this.promptHDKDisplayOrientationDontAskAgain.Name = "promptHDKDisplayOrientationDontAskAgain";
            this.promptHDKDisplayOrientationDontAskAgain.Size = new System.Drawing.Size(100, 17);
            this.promptHDKDisplayOrientationDontAskAgain.TabIndex = 3;
            this.promptHDKDisplayOrientationDontAskAgain.Text = "Don\'t ask again";
            this.promptHDKDisplayOrientationDontAskAgain.UseVisualStyleBackColor = true;
            // 
            // PromptSetHDKDisplayOrientation
            // 
            this.AcceptButton = this.promptSetHDKDisplayRotationLabelYes;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.promptSetHDKDisplayRotationLabelNo;
            this.ClientSize = new System.Drawing.Size(369, 120);
            this.Controls.Add(this.promptHDKDisplayOrientationDontAskAgain);
            this.Controls.Add(this.promptSetHDKDisplayRotationLabelNo);
            this.Controls.Add(this.promptSetHDKDisplayRotationLabelYes);
            this.Controls.Add(this.promptSetHDKDisplayRotationLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PromptSetHDKDisplayOrientation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HDK Display Orientation";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label promptSetHDKDisplayRotationLabel;
        private System.Windows.Forms.Button promptSetHDKDisplayRotationLabelYes;
        private System.Windows.Forms.Button promptSetHDKDisplayRotationLabelNo;
        private System.Windows.Forms.CheckBox promptHDKDisplayOrientationDontAskAgain;
    }
}