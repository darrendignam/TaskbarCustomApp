using System;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace TBCA
{
    public class TaskbarCustomApp : ApplicationContext
    {
        NotifyIcon notifyIcon = new NotifyIcon();
        private About abForm;

        public TaskbarCustomApp()
        {
            //Bit of setup
            notifyIcon.Icon = TBCA.Properties.Resources.AppIcon;
            notifyIcon.DoubleClick += new EventHandler(ShowAbout);
            notifyIcon.Text = "Click Commander";
            //notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { configMenuItem, exitMenuItem });

            notifyIcon.ContextMenu = new ContextMenu();

            //see about adding some custom commands
            string app_path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string path = Path.Combine(app_path, "commands.csv");


            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path)) {
                    sw.WriteLine("\"Menu Item\", \"Shell Command\"");
                    sw.WriteLine("\"Hello\", \"echo world\"");
                }
            }
               
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // Skip the row with the column names
                csvParser.ReadLine();

                int _i = 0;
                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();
                    string Title = fields[0];
                    string Command = fields[1];

                    //System.Diagnostics.Debug.WriteLine(Title + " , " + Command);
                    notifyIcon.ContextMenu.MenuItems.Add(new MenuItem(Title, delegate (object sender, EventArgs e) { CommandClick(sender, e, Command); } ));

                    _i++;
                }
            }

            notifyIcon.ContextMenu.MenuItems.Add("-");
            notifyIcon.ContextMenu.MenuItems.Add( new MenuItem("About", new EventHandler(ShowAbout)) );
            notifyIcon.ContextMenu.MenuItems.Add( new MenuItem("Exit", new EventHandler(Exit)) );

            notifyIcon.Visible = true;
        }

        /*
         *  This is the function called when a user clicks the custom commands from the popup context menu
         */
        void CommandClick(object sender, EventArgs e, String _Command)
        {
            //System.Diagnostics.Debug.WriteLine("Command: " + _Command);

            //Visible CMD window:
            /*
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.Arguments = "/c " + _Command;
            proc.StartInfo.UseShellExecute = true;
            //proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            */

            //Hidden CMD Window
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C " + _Command;
            process.StartInfo = startInfo;
            process.Start();

        }

        void ShowAbout(object sender, EventArgs e)
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
