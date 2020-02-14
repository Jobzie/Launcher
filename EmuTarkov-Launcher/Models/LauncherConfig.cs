using System;

namespace EmuTarkov_Launcher
{
	public class LauncherConfig
	{
		public string Email;
		public string Password;
		public string GamePath;
		public string BackendUrl;
		public bool MinimizeToTray;

		public LauncherConfig()
		{
			Email = "user@jet.com";
			Password = "password";
			GamePath = Environment.CurrentDirectory;
			BackendUrl = "https://127.0.0.1";
			MinimizeToTray = true;
		}
	}
}
