using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace EFT_Launcher_12
{
	public partial class MainWindow : Form
	{
		private LauncherConfig launcherConfig = new LauncherConfig();
		private Profile[] profiles = null;
		private delegate void SetTextCallback(string text);
		private delegate void ResetLauncherCallback();
		private string serverProcessName;

		public MainWindow()
		{
			InitializeComponent();
			this.FormClosing += MainWindow_FormClosing;
			startButton.Enabled = false;
			profileEditButton.Enabled = false;
			profilesListBox.SelectedIndex = 0;

			if (!File.Exists(Path.Combine(Globals.launcherFolder, "launcher.config.json")))
			{
				MessageBox.Show("unable to find launcher.config.json, creating one");
				SaveLauncherSettings();
			}
			else
			{
				string json = File.ReadAllText(Path.Combine(Globals.launcherFolder, "launcher.config.json"));

				launcherConfig = JsonConvert.DeserializeObject<LauncherConfig>(json);
				gamePathTextBox.Text = launcherConfig.gamePath;
				serverPathTextBox.Text = launcherConfig.serverPath;
				backendUrlTextBox.Text = launcherConfig.backendUrl;
				Globals.launchServer = launcherConfig.launchServer;
				Globals.useServerPath = launcherConfig.useServerPath;
			}
		}

		public void SaveLauncherSettings()
		{
			launcherConfig.gamePath = Globals.gameFolder;
			launcherConfig.serverPath = Globals.serverFolder;
			launcherConfig.backendUrl = backendUrlTextBox.Text;
			launcherConfig.launchServer = Globals.launchServer;
			launcherConfig.useServerPath = Globals.useServerPath;

			string json = JsonConvert.SerializeObject(launcherConfig);

			File.WriteAllText(Path.Combine(Globals.launcherFolder, "launcher.config.json"), json);
		}

		public void LoadProfiles()
		{
			if (!File.Exists(Path.Combine(Globals.profilesFolder, "list.json")))
			{
				MessageBox.Show("unable to find profile folder, make sure the server folder is set correctly");
				return;
			}

			using (StreamReader r = new StreamReader(Path.Combine(Globals.profilesFolder, "list.json")))
			{
				profiles = JsonConvert.DeserializeObject<Profile[]>(r.ReadToEnd());

				foreach (Profile someProfile in profiles)
				{
					if (File.Exists(Path.Combine(Globals.profilesFolder, someProfile.id + @"\character.json")) && !profilesListBox.Items.Contains(someProfile.email))
					{
						profilesListBox.Items.Add(someProfile.email);
					}
				}
			}
		}

		// we need to check absolutely everything.
		// if there is one thing we learned from the 0.8.0-alpha,
		// it's that people don't or can't read.
		private void validateValues()
		{
			bool gameExists = false;
			bool serverExists = false;
			bool backendUrlMatch = false;
			bool profileExists = false;

			// game
			if (File.Exists(Path.Combine(gamePathTextBox.Text, "EscapeFromTarkov.exe"))
			&& File.Exists(Path.Combine(gamePathTextBox.Text, "client.config.json")))
			{
				gameExists = true;
				gamePathTextBox.ForeColor = Color.White;
				Globals.gameFolder = gamePathTextBox.Text;

				string json = File.ReadAllText(Path.Combine(Globals.gameFolder, "client.config.json"));

				Globals.clientConfig = JsonConvert.DeserializeObject<ClientConfig>(json);
			}
			else
			{
				gamePathTextBox.ForeColor = Color.Red;
			}

			// server
			if (File.Exists(Path.Combine(serverPathTextBox.Text, "EmuTarkov-Server.exe"))
			&& File.Exists(Path.Combine(serverPathTextBox.Text, @"appdata\server.config.json"))
			)
			{
				serverExists = true;
				serverPathTextBox.ForeColor = Color.White;
				Globals.serverFolder = serverPathTextBox.Text;

				string json = File.ReadAllText(Path.Combine(Globals.serverFolder, @"appdata\server.config.json"));

				Globals.serverConfig = JsonConvert.DeserializeObject<ServerConfig>(json);
				Globals.profilesFolder = Path.Combine(Globals.serverFolder, @"appdata\profiles");
				LoadProfiles();
			}
			else
			{
				if (launcherConfig.launchServer)
				{
					serverPathTextBox.ForeColor = Color.Red;
				}
				else
				{
					serverExists = true;
				}
			}

			if (Globals.useServerPath)
			{
				serverPathTextBox.Text = Globals.serverFolder;
				serverPathTextBox.Enabled = false;
			}

			// backend url
			if (Globals.clientConfig != null
			&& Globals.serverConfig != null
			&& Globals.clientConfig.BackendUrl == Globals.serverConfig.server.backendUrl)
			{
				backendUrlMatch = true;
				backendUrlTextBox.Text = Globals.serverConfig.server.backendUrl;
				backendUrlTextBox.ForeColor = Color.White;
			}
			else
			{
				backendUrlTextBox.ForeColor = Color.Red;
			}

			// profile
			if (profilesListBox.SelectedIndex > 0)
			{
				profileExists = true;
				profileEditButton.Enabled = true;
			}
			else
			{
				profileEditButton.Enabled = false;
			}
			
			// start button
			if (gameExists && serverExists && backendUrlMatch && profileExists)
			{
				startButton.Enabled = true;
				SaveLauncherSettings();
			}
			else
			{
				startButton.Enabled = false;
			}
		}

		private void gamePathTextBox_TextChanged(object sender, EventArgs e)
		{
			validateValues();
		}

		private void serverPathTextBox_TextChanged(object sender, EventArgs e)
		{
			validateValues();
		}

		private void backendUrlTextBox_TextChanged(object sender, EventArgs e)
		{
			validateValues();
		}

		private void profilesListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			validateValues();
		}

		private void profileEditButton_Click(object sender, EventArgs e)
		{
			int select = profiles[profilesListBox.SelectedIndex - 1].id;
			EditProfileForm edit = new EditProfileForm(select);

			edit.Show();
		}

		private string GenerateToken(string email, string password)
		{
			LoginToken token = new LoginToken(email, password);
			string beginKey = "-bC5vLmcuaS5u=";
			string endKey = "=";

			// serialize login token
			string serialized = JsonConvert.SerializeObject(token);

			// encode login token to base64
			string result = Convert.ToBase64String(Encoding.UTF8.GetBytes(serialized));

			// add begin and end part of the token
			return beginKey + result + endKey;
		}


		//**************************************************//
		//					PROCESS FUNCTIONS				//
		//**************************************************//
		private void startButton_Click(object sender, EventArgs e)
		{
			int select = profiles[profilesListBox.SelectedIndex - 1].id;

			if (Globals.launchServer)
			{
				// no need for this.member, we're accessing members inside the class
				Height += 200;
				LaunchServer();
			}

			// start game
			ProcessStartInfo startGame = new ProcessStartInfo(Path.Combine(Globals.gameFolder, "EscapeFromTarkov.exe"));
			startGame.Arguments = GenerateToken(profiles[select].email, profiles[select].password) + " -token=" + select + " -screenmode=fullscreen";
			startGame.UseShellExecute = false;
			startGame.WorkingDirectory = Globals.gameFolder;
			Process.Start(startGame);
		}

		// package-private doesn't benefit from the optimizations that private has
		private void LaunchServer()
		{
			// We know we're creating a new process, don't use var.
			// It's incredibly expensive to use and vague.
			Process proc = new Process();

			// normal properties
			proc.StartInfo.CreateNoWindow = true;
			proc.StartInfo.UseShellExecute = false;
			proc.StartInfo.WorkingDirectory = Globals.serverFolder;
			proc.StartInfo.FileName = Path.Combine(Globals.serverFolder, "EmuTarkov-Server.exe");
			
			// stdout
			proc.StartInfo.RedirectStandardError = true;
			proc.StartInfo.RedirectStandardInput = true;
			proc.StartInfo.RedirectStandardOutput = true;
			proc.StartInfo.StandardOutputEncoding = System.Text.Encoding.UTF8;
			proc.EnableRaisingEvents = true;
			proc.Exited += ServerTerminated;

			proc.Start();

			proc.BeginOutputReadLine();
			proc.OutputDataReceived += proc_OutputDataReceived;
			serverProcessName = proc.ProcessName;
		}

		private void killServer()
		{
			// what is the point of throw-catch if you're not going to use it?
			Process[] procs = Process.GetProcessesByName(serverProcessName);

			if (procs.Length > 0)
			{
				procs[0].Kill();
			}
		}

		private void ServerTerminated(object sender, EventArgs e)
		{
			resetLauncherSize();
		}

		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			killServer();
		}

		void proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			string res = e.Data;

			SetConsoleOutputText(res + "\n");
		}

		private void SetConsoleOutputText(string text)
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (serverOutputRichBox.InvokeRequired)
			{
				SetTextCallback d = new SetTextCallback(SetConsoleOutputText);
				Invoke(d, new object[] { text });
			}
			else
			{
				serverOutputRichBox.Text += text;
				serverOutputRichBox.SelectionStart = serverOutputRichBox.Text.Length;
				serverOutputRichBox.ScrollToCaret();
			}
		}

		private void resetLauncherSize()
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (serverOutputRichBox.InvokeRequired)
			{
				ResetLauncherCallback d = new ResetLauncherCallback(resetLauncherSize);
				Invoke(d, new object[] { });
			}
			else
			{
				Height -= 200;
				serverOutputRichBox.Text = "";
			}
		}
	}

	internal class Profile
	{
		public string email;
		public string password;
		public int id;
	}

	internal class LoginToken
	{
		public string email;
		public string password;
		public bool toggle;
		public long timestamp;

		public LoginToken(string email, string password)
		{
			this.email = email;
			this.password = password;
			this.toggle = true;
			this.timestamp = 132178097635361483;
		}
	}

	internal class LauncherConfig
	{
		public string gamePath;
		public string serverPath;
		public string backendUrl;
		public bool launchServer;
		public bool useServerPath;
	}
}