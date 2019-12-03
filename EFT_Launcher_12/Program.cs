using System;
using System.Windows.Forms;

namespace EFT_Launcher_12
{
    static class Program
    {
        public static string profileFolder = System.IO.Path.Combine(Environment.CurrentDirectory, "appdata/profiles/");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
