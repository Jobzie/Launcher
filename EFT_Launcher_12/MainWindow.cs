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

        public MainWindow()
        {
            InitializeComponent();
            
            startButton.Enabled = false;
            profileEditButton.Enabled = false;
            profilesListBox.SelectedIndex = 0;
            gamePathTextBox.Text = Properties.Settings.Default.gamePath;
			serverPathTextBox.Text = Properties.Settings.Default.serverPath;
		}

		public void LoadProfiles()
		{
			try
			{
				using (StreamReader r = new StreamReader(Path.Combine(Globals.serverFolder, "appdata/profiles/list.json")))
				{
					profiles = JsonConvert.DeserializeObject<Profile[]>(r.ReadToEnd());

					foreach (Profile someProfile in profiles)
					{
						if (File.Exists(Path.Combine(Globals.serverFolder, "appdata/profiles/" + someProfile.id + "/character.json")) == true)
						{
							profilesListBox.Items.Add(someProfile.email);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("unable to find profile folder, please set the server path");
			}
		}

        private void startButton_Click(object sender, EventArgs e)
        {
            if (profilesListBox.SelectedIndex == 0)
            {
                MessageBox.Show("select a profile before starting !");
            }
            else
            {
                int select = profiles[profilesListBox.SelectedIndex - 1].id;
                string filePath = Path.Combine(Globals.gameFolder, "EscapeFromTarkov.exe");
                string launchArgs = GenerateToken(profiles[select].email, profiles[select].password) + " -token="+select+" -screenmode=fullscreen";

                Process.Start(filePath, launchArgs);
            }
        }

        private void gamePathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(gamePathTextBox.Text, "EscapeFromTarkov.exe")) == true)
            {
                startButton.Enabled = true;
                gamePathTextBox.ForeColor = Color.White;

                Properties.Settings.Default.gamePath = gamePathTextBox.Text;
                Properties.Settings.Default.Save();

				Globals.gameFolder = gamePathTextBox.Text;
            }
            else
            {
                startButton.Enabled = false;
                gamePathTextBox.ForeColor = Color.Red;
            }
        }

		private void serverPathTextBox_TextChanged(object sender, EventArgs e)
		{
			serverPathTextBox.ForeColor = Color.White;

			Properties.Settings.Default.serverPath = serverPathTextBox.Text;
			Properties.Settings.Default.Save();

			Globals.serverFolder = serverPathTextBox.Text;
			LoadProfiles();
		}

		private void profilesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (profilesListBox.SelectedIndex != 0)
            {
                profileEditButton.Enabled = true;
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