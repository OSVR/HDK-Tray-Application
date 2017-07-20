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
    partial class ContextMenuWYSIWYG
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
            this.components = new System.ComponentModel.Container();
            this.OSVRContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OSVRServerToolStripMenuItemLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.startServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showServerConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OSVRServerToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.OSVRTestApplicationsToolStripMenuItemLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.launchTrackerViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.launchSampleSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OSVRTestApplicationsToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.HDKConfigurationToolStripMenuItemLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.launchFWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverConfigurationToolStripMenuItemLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultServerConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customServerConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HDKPresetsToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.useIRCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useIRCameraToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.invertSteamVRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HDKConfigurationToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.displayModeToolStripMenuItemLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.graphicsCardTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NVIDIAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AMDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableDirectModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableExtendedModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayConfigurationToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OSVRContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // OSVRContextMenuStrip
            // 
            this.OSVRContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OSVRServerToolStripMenuItemLabel,
            this.startServerToolStripMenuItem,
            this.restartServerToolStripMenuItem,
            this.stopServerToolStripMenuItem,
            this.showServerConsoleToolStripMenuItem,
            this.OSVRServerToolStripSeparator,
            this.OSVRTestApplicationsToolStripMenuItemLabel,
            this.launchTrackerViewToolStripMenuItem,
            this.launchSampleSceneToolStripMenuItem,
            this.OSVRTestApplicationsToolStripSeparator,
            this.HDKConfigurationToolStripMenuItemLabel,
            this.launchFWToolStripMenuItem,
            this.configureToolStripMenuItem,
            this.HDKConfigurationToolStripSeparator,
            this.displayModeToolStripMenuItemLabel,
            this.graphicsCardTypeToolStripMenuItem,
            this.enableDirectModeToolStripMenuItem,
            this.enableExtendedModeToolStripMenuItem,
            this.displayConfigurationToolStripSeparator,
            this.helpToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.OSVRContextMenuStrip.Name = "OSVRContextMenuStrip";
            this.OSVRContextMenuStrip.ShowImageMargin = false;
            this.OSVRContextMenuStrip.Size = new System.Drawing.Size(175, 424);
            this.OSVRContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.OSVRContextMenuStrip_Opening);
            // 
            // OSVRServerToolStripMenuItemLabel
            // 
            this.OSVRServerToolStripMenuItemLabel.Enabled = false;
            this.OSVRServerToolStripMenuItemLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline);
            this.OSVRServerToolStripMenuItemLabel.Name = "OSVRServerToolStripMenuItemLabel";
            this.OSVRServerToolStripMenuItemLabel.Size = new System.Drawing.Size(174, 22);
            this.OSVRServerToolStripMenuItemLabel.Text = "OSVR Server";
            // 
            // startServerToolStripMenuItem
            // 
            this.startServerToolStripMenuItem.Name = "startServerToolStripMenuItem";
            this.startServerToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.startServerToolStripMenuItem.Text = "Start Server";
            this.startServerToolStripMenuItem.Click += new System.EventHandler(this.startServerToolStripMenuItem_Click);
            // 
            // restartServerToolStripMenuItem
            // 
            this.restartServerToolStripMenuItem.Name = "restartServerToolStripMenuItem";
            this.restartServerToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.restartServerToolStripMenuItem.Text = "Restart Server";
            this.restartServerToolStripMenuItem.Click += new System.EventHandler(this.restartServerToolStripMenuItem_Click);
            // 
            // stopServerToolStripMenuItem
            // 
            this.stopServerToolStripMenuItem.Name = "stopServerToolStripMenuItem";
            this.stopServerToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.stopServerToolStripMenuItem.Text = "Stop Server";
            this.stopServerToolStripMenuItem.Click += new System.EventHandler(this.stopServerToolStripMenuItem_Click);
            // 
            // showServerConsoleToolStripMenuItem
            // 
            this.showServerConsoleToolStripMenuItem.Name = "showServerConsoleToolStripMenuItem";
            this.showServerConsoleToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.showServerConsoleToolStripMenuItem.Text = "Show Server Console";
            this.showServerConsoleToolStripMenuItem.Visible = false;
            this.showServerConsoleToolStripMenuItem.Click += new System.EventHandler(this.showServerConsoleToolStripMenuItem_Click);
            // 
            // OSVRServerToolStripSeparator
            // 
            this.OSVRServerToolStripSeparator.Name = "OSVRServerToolStripSeparator";
            this.OSVRServerToolStripSeparator.Size = new System.Drawing.Size(171, 6);
            // 
            // OSVRTestApplicationsToolStripMenuItemLabel
            // 
            this.OSVRTestApplicationsToolStripMenuItemLabel.Enabled = false;
            this.OSVRTestApplicationsToolStripMenuItemLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline);
            this.OSVRTestApplicationsToolStripMenuItemLabel.Name = "OSVRTestApplicationsToolStripMenuItemLabel";
            this.OSVRTestApplicationsToolStripMenuItemLabel.Size = new System.Drawing.Size(174, 22);
            this.OSVRTestApplicationsToolStripMenuItemLabel.Text = "OSVR Test Applications";
            // 
            // launchTrackerViewToolStripMenuItem
            // 
            this.launchTrackerViewToolStripMenuItem.Name = "launchTrackerViewToolStripMenuItem";
            this.launchTrackerViewToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.launchTrackerViewToolStripMenuItem.Text = "Launch Tracker View";
            this.launchTrackerViewToolStripMenuItem.Click += new System.EventHandler(this.launchTrackerViewToolStripMenuItem_Click);
            // 
            // launchSampleSceneToolStripMenuItem
            // 
            this.launchSampleSceneToolStripMenuItem.Name = "launchSampleSceneToolStripMenuItem";
            this.launchSampleSceneToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.launchSampleSceneToolStripMenuItem.Text = "Launch Sample Scene";
            this.launchSampleSceneToolStripMenuItem.Click += new System.EventHandler(this.launchSampleSceneToolStripMenuItem_Click);
            // 
            // OSVRTestApplicationsToolStripSeparator
            // 
            this.OSVRTestApplicationsToolStripSeparator.Name = "OSVRTestApplicationsToolStripSeparator";
            this.OSVRTestApplicationsToolStripSeparator.Size = new System.Drawing.Size(171, 6);
            // 
            // HDKConfigurationToolStripMenuItemLabel
            // 
            this.HDKConfigurationToolStripMenuItemLabel.Enabled = false;
            this.HDKConfigurationToolStripMenuItemLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline);
            this.HDKConfigurationToolStripMenuItemLabel.Name = "HDKConfigurationToolStripMenuItemLabel";
            this.HDKConfigurationToolStripMenuItemLabel.Size = new System.Drawing.Size(174, 22);
            this.HDKConfigurationToolStripMenuItemLabel.Text = "HDK Configuration";
            // 
            // launchFWToolStripMenuItem
            // 
            this.launchFWToolStripMenuItem.Name = "launchFWToolStripMenuItem";
            this.launchFWToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.launchFWToolStripMenuItem.Text = "Launch Firmware Utility";
            this.launchFWToolStripMenuItem.Click += new System.EventHandler(this.launchFWUToolStripMenuItem_Click);
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.configureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverConfigurationToolStripMenuItemLabel,
            this.defaultServerConfigurationToolStripMenuItem,
            this.customServerConfigurationToolStripMenuItem,
            this.HDKPresetsToolStripSeparator,
            this.useIRCameraToolStripMenuItem,
            this.useIRCameraToolStripSeparator,
            this.invertSteamVRToolStripMenuItem});
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.configureToolStripMenuItem.Text = "Options";
            // 
            // serverConfigurationToolStripMenuItemLabel
            // 
            this.serverConfigurationToolStripMenuItemLabel.Enabled = false;
            this.serverConfigurationToolStripMenuItemLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline);
            this.serverConfigurationToolStripMenuItemLabel.Name = "serverConfigurationToolStripMenuItemLabel";
            this.serverConfigurationToolStripMenuItemLabel.Size = new System.Drawing.Size(154, 22);
            this.serverConfigurationToolStripMenuItemLabel.Text = "OSVR Server";
            // 
            // defaultServerConfigurationToolStripMenuItem
            // 
            this.defaultServerConfigurationToolStripMenuItem.CheckOnClick = true;
            this.defaultServerConfigurationToolStripMenuItem.Name = "defaultServerConfigurationToolStripMenuItem";
            this.defaultServerConfigurationToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.defaultServerConfigurationToolStripMenuItem.Text = "Default";
            this.defaultServerConfigurationToolStripMenuItem.Click += new System.EventHandler(this.defaultToolStripMenuItem_Click);
            // 
            // customServerConfigurationToolStripMenuItem
            // 
            this.customServerConfigurationToolStripMenuItem.CheckOnClick = true;
            this.customServerConfigurationToolStripMenuItem.Name = "customServerConfigurationToolStripMenuItem";
            this.customServerConfigurationToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.customServerConfigurationToolStripMenuItem.Text = "Custom...";
            this.customServerConfigurationToolStripMenuItem.Click += new System.EventHandler(this.customToolStripMenuItem_Click);
            // 
            // HDKPresetsToolStripSeparator
            // 
            this.HDKPresetsToolStripSeparator.Name = "HDKPresetsToolStripSeparator";
            this.HDKPresetsToolStripSeparator.Size = new System.Drawing.Size(151, 6);
            // 
            // useIRCameraToolStripMenuItem
            // 
            this.useIRCameraToolStripMenuItem.Checked = true;
            this.useIRCameraToolStripMenuItem.CheckOnClick = true;
            this.useIRCameraToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useIRCameraToolStripMenuItem.Name = "useIRCameraToolStripMenuItem";
            this.useIRCameraToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.useIRCameraToolStripMenuItem.Text = "Use IR Camera";
            this.useIRCameraToolStripMenuItem.Click += new System.EventHandler(this.useIRCameraToolStripMenuItem_Click);
            // 
            // useIRCameraToolStripSeparator
            // 
            this.useIRCameraToolStripSeparator.Name = "useIRCameraToolStripSeparator";
            this.useIRCameraToolStripSeparator.Size = new System.Drawing.Size(151, 6);
            // 
            // invertSteamVRToolStripMenuItem
            // 
            this.invertSteamVRToolStripMenuItem.Name = "invertSteamVRToolStripMenuItem";
            this.invertSteamVRToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.invertSteamVRToolStripMenuItem.Text = "Invert SteamVR";
            this.invertSteamVRToolStripMenuItem.Click += new System.EventHandler(this.invertSteamVRToolStripMenuItem_Click);
            // 
            // HDKConfigurationToolStripSeparator
            // 
            this.HDKConfigurationToolStripSeparator.Name = "HDKConfigurationToolStripSeparator";
            this.HDKConfigurationToolStripSeparator.Size = new System.Drawing.Size(171, 6);
            // 
            // displayModeToolStripMenuItemLabel
            // 
            this.displayModeToolStripMenuItemLabel.Enabled = false;
            this.displayModeToolStripMenuItemLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline);
            this.displayModeToolStripMenuItemLabel.Name = "displayModeToolStripMenuItemLabel";
            this.displayModeToolStripMenuItemLabel.Size = new System.Drawing.Size(174, 22);
            this.displayModeToolStripMenuItemLabel.Text = "Display Mode";
            // 
            // graphicsCardTypeToolStripMenuItem
            // 
            this.graphicsCardTypeToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.graphicsCardTypeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NVIDIAToolStripMenuItem,
            this.AMDToolStripMenuItem});
            this.graphicsCardTypeToolStripMenuItem.Name = "graphicsCardTypeToolStripMenuItem";
            this.graphicsCardTypeToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.graphicsCardTypeToolStripMenuItem.Text = "Graphics Card Type";
            this.graphicsCardTypeToolStripMenuItem.Visible = false;
            // 
            // NVIDIAToolStripMenuItem
            // 
            this.NVIDIAToolStripMenuItem.CheckOnClick = true;
            this.NVIDIAToolStripMenuItem.Name = "NVIDIAToolStripMenuItem";
            this.NVIDIAToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.NVIDIAToolStripMenuItem.Text = "NVIDIA";
            this.NVIDIAToolStripMenuItem.Click += new System.EventHandler(this.NVIDIAToolStripMenuItem_Click);
            // 
            // AMDToolStripMenuItem
            // 
            this.AMDToolStripMenuItem.CheckOnClick = true;
            this.AMDToolStripMenuItem.Name = "AMDToolStripMenuItem";
            this.AMDToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.AMDToolStripMenuItem.Text = "AMD";
            this.AMDToolStripMenuItem.Click += new System.EventHandler(this.AMDToolStripMenuItem_Click);
            // 
            // enableDirectModeToolStripMenuItem
            // 
            this.enableDirectModeToolStripMenuItem.Name = "enableDirectModeToolStripMenuItem";
            this.enableDirectModeToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.enableDirectModeToolStripMenuItem.Text = "Enable Direct Mode";
            this.enableDirectModeToolStripMenuItem.Click += new System.EventHandler(this.enableDirectModeToolStripMenuItem_Click);
            // 
            // enableExtendedModeToolStripMenuItem
            // 
            this.enableExtendedModeToolStripMenuItem.Name = "enableExtendedModeToolStripMenuItem";
            this.enableExtendedModeToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.enableExtendedModeToolStripMenuItem.Text = "Enable Extended Mode";
            this.enableExtendedModeToolStripMenuItem.Click += new System.EventHandler(this.enableExtendedModeToolStripMenuItem_Click);
            // 
            // displayConfigurationToolStripSeparator
            // 
            this.displayConfigurationToolStripSeparator.Name = "displayConfigurationToolStripSeparator";
            this.displayConfigurationToolStripSeparator.Size = new System.Drawing.Size(171, 6);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // ContextMenuWYSIWYG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Name = "ContextMenuWYSIWYG";
            this.Text = "ContextMenuWYSIWYG";
            this.OSVRContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem OSVRServerToolStripMenuItemLabel;
        private System.Windows.Forms.ToolStripMenuItem startServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator OSVRServerToolStripSeparator;
        public System.Windows.Forms.ContextMenuStrip OSVRContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem launchTrackerViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem launchSampleSceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator OSVRTestApplicationsToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem launchFWToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableDirectModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableExtendedModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator displayConfigurationToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customServerConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OSVRTestApplicationsToolStripMenuItemLabel;
        private System.Windows.Forms.ToolStripMenuItem displayModeToolStripMenuItemLabel;
        private System.Windows.Forms.ToolStripMenuItem graphicsCardTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NVIDIAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AMDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HDKConfigurationToolStripMenuItemLabel;
        private System.Windows.Forms.ToolStripSeparator HDKConfigurationToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator HDKPresetsToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem useIRCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showServerConsoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator useIRCameraToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem defaultServerConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverConfigurationToolStripMenuItemLabel;
        private System.Windows.Forms.ToolStripMenuItem invertSteamVRToolStripMenuItem;
    }
}