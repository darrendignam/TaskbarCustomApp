using Microsoft.Win32;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TBCA
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            cbAutostart.Checked = isAutoStartEnabled();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public static bool isAutoStartEnabled()
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            if (!regKey.GetValueNames().Contains("TBCA-Task-Launcher"))
            {
                return false;
            }
            else
            {
                if (regKey.GetValue("TBCA-Task-Launcher").ToString() == "False" )
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private void cbAutostart_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            if (cbAutostart.Checked)
            {
                regKey.SetValue("TBCA-Task-Launcher", Application.ExecutablePath);
            }
            else
            {
                regKey.SetValue("TBCA-Task-Launcher", false);
            }
        }
    }
}
