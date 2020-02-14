using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace EmuTarkov_Launcher
{
	public static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolveEvent);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Main());
		}

		private static Assembly AssemblyResolveEvent(object sender, ResolveEventArgs args)
		{
			string assembly = new AssemblyName(args.Name).Name;
			string filename = Path.Combine(Environment.CurrentDirectory, "EscapeFromTarkov_Data/Managed/" + assembly + ".dll");

            // resources are embedded inside assembly
            if (filename.Contains("resources"))
            {
                return null;
            }

			return Assembly.LoadFrom(filename);
		}
	}
}
