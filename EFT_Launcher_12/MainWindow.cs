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
        Profile[] profiles = null;
		delegate void SetTextCallback(string text);
		delegate void ResetLauncherCallback();
		string serverProcessName;

		public MainWindow()
        {
            InitializeComponent();
			this.FormClosing += MainWindow_FormClosing;
            startButton.Enabled = false;
            profileEditButton.Enabled = false;
            profilesListBox.SelectedIndex = 0;
            gamePathTextBox.Text = Properties.Settings.Default.gamePath;
            LoadProfiles();

		}



		public void LoadProfiles()
		{
			if (!File.Exists(Path.Combine(Globals.profilesFolder, "list.json")))
			{
				MessageBox.Show("unable to find profile folder, make sure the launcher is in the server folder");
				return;
			}

			using (StreamReader r = new StreamReader(Path.Combine(Globals.profilesFolder, "list.json")))
			{
				profiles = JsonConvert.DeserializeObject<Profile[]>(r.ReadToEnd());

				foreach (Profile someProfile in profiles)
				{
					if (File.Exists(Path.Combine(Globals.profilesFolder, someProfile.id + "/character.json")))
					{
						profilesListBox.Items.Add(someProfile.email);
					}
				}
			}
		}

		private void readyToLaunch()
		{
			if(File.Exists( Path.Combine(gamePathTextBox.Text, "EscapeFromTarkov.exe") ) && profilesListBox.SelectedIndex > 0)
			{
				startButton.Enabled = true;
			}
            else
            {
                startButton.Enabled = false;
            }
		}

        private void gamePathTextBox_TextChanged(object sender, EventArgs e)
        {
            if( File.Exists(Path.Combine(gamePathTextBox.Text, "EscapeFromTarkov.exe")) )
            {
                gamePathTextBox.ForeColor = Color.White;
                Properties.Settings.Default.gamePath = gamePathTextBox.Text;
                Properties.Settings.Default.Save();
                Globals.gameFolder = gamePathTextBox.Text;
                readyToLaunch();
            }
            else
            {
                gamePathTextBox.ForeColor = Color.Red;
                startButton.Enabled = false;
            }

		}

		private void profilesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
			if (profilesListBox.SelectedIndex != 0)
            {
                profileEditButton.Enabled = true;
                readyToLaunch();
            }
            else
            {
                profileEditButton.Enabled = false;
            }
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
			if (profilesListBox.SelectedIndex == 0)
			{
				MessageBox.Show("select a profile before starting !");
				return; //oh, return cut the function ? that's cool !
			}
			
			int select = profiles[profilesListBox.SelectedIndex - 1].id;
			this.Height += 200;

			LaunchServer();

			//start game
			ProcessStartInfo startGame = new ProcessStartInfo(Path.Combine(Globals.gameFolder, "EscapeFromTarkov.exe"));
			startGame.Arguments = GenerateToken(profiles[select].email, profiles[select].password) + " -token=" + select + " -screenmode=fullscreen";
			startGame.UseShellExecute = false;
			startGame.WorkingDirectory = Globals.gameFolder;
			Process.Start(startGame);

		}

		void LaunchServer()
		{
			var proc = new Process();

			proc.StartInfo.CreateNoWindow = true;
			proc.StartInfo.UseShellExecute = false;
			proc.StartInfo.WorkingDirectory = Globals.serverFolder;	
			proc.StartInfo.RedirectStandardError = true;
			proc.StartInfo.RedirectStandardInput = true;
			proc.StartInfo.RedirectStandardOutput = true;
			proc.StartInfo.FileName = Path.Combine(Globals.serverFolder, "EmuTarkov-Server.exe");
			proc.EnableRaisingEvents = true;
			proc.Exited += Proc_Exited;
			
			proc.Start();
			
			proc.BeginOutputReadLine();
			proc.OutputDataReceived += proc_OutputDataReceived;
			serverProcessName = proc.ProcessName;
		}

		void killServer()
		{
			try
			{
				Process[] procs = Process.GetProcessesByName(serverProcessName);
				procs[0].Kill();
			}
			catch(Exception ex)
			{
				//do nothing whatever
			}
			
		}

		private void Proc_Exited(object sender, EventArgs e)
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
			//string res2 = Encoding.ASCII.GetString(Encoding.Convert(Encoding.UTF8, Encoding.ASCII, Encoding.UTF8.GetBytes(e.Data)));
			SetConsoleOutputText(res + "\n");
		}

		private void SetConsoleOutputText(string text)
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (this.serverOutputRichBox.InvokeRequired)
			{
				SetTextCallback d = new SetTextCallback(SetConsoleOutputText);
				this.Invoke(d, new object[] { text });
			}
			else
			{
				this.serverOutputRichBox.Text += text;
				serverOutputRichBox.SelectionStart = serverOutputRichBox.Text.Length;
				this.serverOutputRichBox.ScrollToCaret();
			}
		}

		private void resetLauncherSize()
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (this.serverOutputRichBox.InvokeRequired)
			{
				ResetLauncherCallback d = new ResetLauncherCallback(resetLauncherSize);
				this.Invoke(d, new object[] { });
			}
			else
			{
				this.Height -= 200;
				this.serverOutputRichBox.Text = "";
			}
		}
	}

	internal class Profile
    {
		public string email;
		public string password;
		public int id;
    }

	// don't change the order of the members
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
}