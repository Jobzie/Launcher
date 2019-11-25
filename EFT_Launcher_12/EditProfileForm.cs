using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EFT_Launcher_12
{
    public partial class EditProfileForm : Form
    {
        int id;
        string serverFolder = "Y:/tarkov/EmuTarkov Server dev";
        ProfileExtended profileToEdit = null;

        public EditProfileForm(int id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void EditProfileForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (StreamReader r = new StreamReader(Path.Combine(this.serverFolder, "appdata/profiles/character_" + this.id + ".json")))
                {
                    this.profileToEdit = JsonConvert.DeserializeObject<ProfileExtended>(r.ReadToEnd());
                    this.Text += profileToEdit.Info.Nickname;
                    this.nicknameTextBox.Text = profileToEdit.Info.Nickname;
                    this.sideselectorComboBox.SelectedItem = profileToEdit.Info.Side;
                    this.experienceBox.Value = profileToEdit.Info.Experience;
                }
            }
            catch(Exception ex)
            {
                
                MessageBox.Show("profile can't be loaded");
                this.Close();
                
            }
        }
    }
}
