using System;
using System.IO;

namespace EmuTarkov_Launcher
{
	public static class Globals
	{
		public static LauncherConfig LauncherConfig;
		public static ClientConfig ClientConfig;
		public static string LauncherConfigFile;
		public static string ClientConfigFile;
		public static string ClientExecutable;

		static Globals()
		{
			LauncherConfig = null;
			ClientConfig = null;
			SetPaths(Environment.CurrentDirectory);
		}

		public static void SetPaths(string dir)
		{
			LauncherConfigFile = Path.Combine(dir, "launcher.config.json");
			ClientConfigFile = Path.Combine(dir, "client.config.json");
			ClientExecutable = Path.Combine(dir, "EscapeFromTarkov.exe");
		}
	}
}
