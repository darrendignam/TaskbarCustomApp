using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SynergyServiceManager
{
    public class SynergyServiceManagerContext : ApplicationContext
    {
        NotifyIcon notifyIcon = new NotifyIcon();
        private About abForm;

        public SynergyServiceManagerContext()
        {
            //MenuItem configMenuItem = new MenuItem("Configuration", new EventHandler(ShowConfig));

            MenuItem aboutItem = new MenuItem("About", new EventHandler(ShowMessage));
            MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));

            notifyIcon.Icon = SynergyServiceManager.Properties.Resources.AppIcon;
            notifyIcon.DoubleClick += new EventHandler(ShowMessage);
            notifyIcon.Text = "Synergy Service Manager";
            //notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { configMenuItem, exitMenuItem });
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { aboutItem, exitMenuItem });

            notifyIcon.Visible = true;
        }

        void ShowMessage(object sender, EventArgs e)
        {
            abForm = new About();
            abForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            abForm.MaximizeBox = false;
            abForm.MinimizeBox = false;
            abForm.Show();
        }



        void Exit(object sender, EventArgs e)
        {
            // We must manually tidy up and remove the icon before we exit.
            // Otherwise it will be left behind until the user mouses over.
            notifyIcon.Visible = false;

            Application.Exit();
        }
    }
}
