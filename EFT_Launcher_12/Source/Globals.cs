using System;
using System.IO;

namespace EFT_Launcher_12
{
	public class Globals
	{
		public static string launcherFolder;
		public static string gameFolder;
		public static string serverFolder;
		public static string profilesFolder;
		public static string version;
		public static bool launchServer;
		public static bool useServerPath;
		public static ClientConfig clientConfig;
		public static ServerConfig serverConfig;
		
		static Globals()
		{
			launcherFolder = Environment.CurrentDirectory;
			gameFolder = "";
			serverFolder = "";
			profilesFolder = "";
			version = "EmuTarkov Launcher 0.3.0-beta";
			launchServer = true;
			useServerPath = true;
			clientConfig = null;
			serverConfig = null;
		}
	}
}