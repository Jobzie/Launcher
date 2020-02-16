using System;
using System.Windows.Forms;

namespace EmuTarkov_Launcher
{
	public partial class Main : Form
	{
		private static Monitor monitor;

		public Main()
		{
			InitializeComponent();
			InitializeLauncher();
		}

		private void InitializeLauncher()
		{
			// load configs
			Globals.LauncherConfig = Json.Load<LauncherConfig>(Globals.LauncherConfigFile);
			Globals.ClientConfig = Json.Load<ClientConfig>(Globals.ClientConfigFile);

			// set input
			EmailInput.Text = Globals.LauncherConfig.Email;
			PasswordInput.Text = Globals.LauncherConfig.Password;
			UrlInput.Text = Globals.LauncherConfig.BackendUrl;

			// setup monitor
			monitor = new Monitor("EscapeFromTarkov", MonitorCallback);
		}

		private void MonitorCallback(Monitor monitor)
		{
			// stop monitoring
			monitor.Stop();

			// show window
			this.Show();
		}

		private void EmailInput_TextChanged(object sender, EventArgs e)
		{
			// set and save input
			Globals.LauncherConfig.Email = EmailInput.Text;
			Json.Save<LauncherConfig>(Globals.LauncherConfigFile, Globals.LauncherConfig);
		}

		private void PasswordInput_TextChanged(object sender, EventArgs e)
		{
			// set and save input
			Globals.LauncherConfig.Password = PasswordInput.Text;
			Json.Save<LauncherConfig>(Globals.LauncherConfigFile, Globals.LauncherConfig);
		}

		private void UrlInput_TextChanged(object sender, EventArgs e)
		{
			// set and save input
			Globals.LauncherConfig.BackendUrl = UrlInput.Text;
			Json.Save<LauncherConfig>(Globals.LauncherConfigFile, Globals.LauncherConfig);
		}

		private void StartGame_Click(object sender, EventArgs e)
		{
			if (!Starter.StartGame()) {
				return;
			}

			monitor.Start();

			if (Globals.LauncherConfig.MinimizeToTray)
			{
				TrayIcon.Visible = true;
				this.Hide();
			}
		}

		private void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.Show();
			this.WindowState = FormWindowState.Normal;
		}

		private void Main_Resize(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Normal)
			{
				TrayIcon.Visible = false;
			}
		}
	}
}
