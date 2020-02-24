using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace EmuTarkov_Launcher
{
	public static class Starter
	{
		public static bool StartGame()
		{
			// detect if executable is found
			if (!System.IO.File.Exists(Globals.ClientExecutable))
			{
				MessageBox.Show("The launcher is not running from the Escape From Tarkov directory");
				return false;
			}

			// get profile ID
			string token = GenerateToken(Globals.LauncherConfig.Email, Globals.LauncherConfig.Password);
			string playerId = LauncherRequest.Send(Globals.LauncherConfig.BackendUrl + "/launcher/profile/login", token);

			if (playerId == null)
			{
				MessageBox.Show("Server is not started");
				return false;
			}

			if (playerId == "0")
			{
				MessageBox.Show("Wrong email and/or password");
				return false;
			}

			// set backend url
			Globals.ClientConfig.BackendUrl = Globals.LauncherConfig.BackendUrl;
			Json.Save<ClientConfig>(Globals.ClientConfigFile, Globals.ClientConfig);

			ProcessStartInfo clientProcess = new ProcessStartInfo(Globals.ClientExecutable);
			clientProcess.Arguments = "-bC5vLmcuaS5u=" + token + " -token=" + playerId + " -screenmode=fullscreen";
			clientProcess.UseShellExecute = false;
			clientProcess.WorkingDirectory = Environment.CurrentDirectory;
			Process.Start(clientProcess);

			return true;
		}

		private static string GenerateToken(string email, string password)
		{
			LoginToken token = new LoginToken(email, password);
			string serialized = Json.Serialize(token);
			string result = Convert.ToBase64String(Encoding.UTF8.GetBytes(serialized));

			// add begin and end part of the token
			return result + "=";
		}
	}
}
