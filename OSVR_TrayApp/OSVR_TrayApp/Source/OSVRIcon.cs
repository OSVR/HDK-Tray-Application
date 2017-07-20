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

using HDK_TrayApp.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HDK_TrayApp
{
    public class OSVRIcon : IDisposable
    {
        private NotifyIcon m_osvrIcon;
        private ContextMenuWYSIWYG m_contextMenu;

        private static readonly Color
            ORANGE = Color.FromArgb(255, 128, 0),
            DARK = Color.FromArgb(22, 22, 22),
            GREY = Color.FromArgb(153, 153, 153),
            WHITE = Color.FromArgb(255, 255, 255),
            TRANSPARENT = Color.Transparent;

        private static readonly Color
            BG_COLOR = WHITE,
            BG_HOVER_COLOR = ORANGE,
            FG_COLOR = DARK,
            FG_HOVER_COLOR = DARK,
            CHECKBOX_BG_COLOR_CHECKED = WHITE,
            BORDER = DARK;

        public OSVRIcon()
        {
            m_osvrIcon = new NotifyIcon();
        }

        public void Dispose()
        {
            if (m_osvrIcon != null)
            {
                m_osvrIcon.Dispose();
                m_osvrIcon = null;
            }
        }

        public void Display(bool start_server = false)
        {
            m_osvrIcon.Icon = Resources.logo;
            m_osvrIcon.Text = "OSVR Tray App";
            m_osvrIcon.Visible = true;

            // Note: this is a workaround; see OSVI-65 for context.
            PromptSetHDKDisplayOrientation p = new PromptSetHDKDisplayOrientation();
            p.Opacity = 0d;
            p.Show();
            p.Hide();
            p.Opacity = 100d;

            m_contextMenu = new ContextMenuWYSIWYG(m_osvrIcon, p, start_server);
            p.m_contextMenu = m_contextMenu;
            m_osvrIcon.ContextMenuStrip = m_contextMenu.OSVRContextMenuStrip;
            m_osvrIcon.MouseDoubleClick += OSVRIcon_MouseDoubleClick;

            SetupLabels(m_osvrIcon.ContextMenuStrip.Items);

            m_osvrIcon.ContextMenuStrip.BackColor = BG_COLOR;

            foreach (ToolStripItem tsi in m_osvrIcon.ContextMenuStrip.Items)
                    SetupTSIColors(tsi);

            m_osvrIcon.ContextMenuStrip.Renderer = new CustomColorRenderer();
        }

        /// <summary>
        /// Recursively configure appearance of label menu items
        /// </summary>
        /// <param name="items">Collection of menu items to process</param>
        private void SetupLabels(ToolStripItemCollection items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                ToolStripItem tsi = items[i];
                if (tsi.Name.EndsWith("Label"))
                {
                    ToolStripLabel label = new ToolStripLabel(tsi.Text);
                    label.Font = new Font(tsi.Font, FontStyle.Underline | FontStyle.Bold);

                    items.RemoveAt(i);
                    items.Insert(i, label);
                }
                else if (tsi is ToolStripMenuItem && ((ToolStripMenuItem)tsi).HasDropDownItems)
                    SetupLabels(((ToolStripMenuItem)tsi).DropDownItems);
            }
        }

        private void SetupTSIColors(ToolStripItem tsi)
        {
            tsi.ForeColor = FG_COLOR;
            tsi.BackColor = BG_COLOR;

            if (tsi is ToolStripMenuItem)
            {
                foreach (ToolStripItem child in ((ToolStripMenuItem)tsi).DropDownItems)
                    SetupTSIColors(child);
            }
        }

        private void OSVRIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                m_contextMenu.ShowServerConsole();
        }

        /// <summary>
        /// Custom color scheme with optional menu arrow coloration
        /// </summary>
        private class CustomColorRenderer : ToolStripProfessionalRenderer
        {
            public CustomColorRenderer() : base(new MyColors()) { }

            // Note: use this to edit arrow color
            /*protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                var tsMenuItem = e.Item as ToolStripMenuItem;
                if (tsMenuItem != null)
                    e.ArrowColor = WHITE;
                base.OnRenderArrow(e);
            }*/
        }

        /// <summary>
        /// Custom color scheme
        /// </summary>
        private class MyColors : ProfessionalColorTable
        {
            // General
            public override Color MenuItemSelected { get { return BG_HOVER_COLOR; } }               // On-hover background color
            public override Color MenuItemBorder { get { return TRANSPARENT; } }                    // No border color on select
            public override Color MenuBorder { get { return BORDER; } }                             // Menu border
            public override Color ToolStripDropDownBackground { get { return BG_COLOR; } }          // Background behind toolstrip separators in submenus

            // Background behind checkboxes
            public override Color ImageMarginGradientBegin { get { return BG_COLOR; } }
            public override Color ImageMarginGradientMiddle { get { return BG_COLOR; } }
            public override Color ImageMarginGradientEnd { get { return BG_COLOR; }}

            // Checkboxes
            public override Color CheckBackground { get { return CHECKBOX_BG_COLOR_CHECKED; } }     // Background color around checks
            public override Color CheckSelectedBackground { get { return WHITE; } }                 // Background color of checkbox when hovering over it
        }
    }
}
