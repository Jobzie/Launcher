using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace EFT_Launcher_12
{
    public partial class EditProfileForm : Form
    {
        int id;
        string serverFolder = "Y:/tarkov/EmuTarkov Server dev"; //delete this
        string profilePath;
        ProfileExtended profileToEdit;

        public EditProfileForm(int id)
        {
            this.id = id;
            this.profilePath = Path.Combine(this.serverFolder, "appdata/profiles/character_" + this.id + ".json"); //Program.profileFolder + "character_" id
            InitializeComponent();
        }

        private void EditProfileForm_Load(object sender, EventArgs e)
        {
            try
            {
                using ( StreamReader r = new StreamReader(profilePath) )
                {
                    this.profileToEdit = JsonConvert.DeserializeObject<ProfileExtended>(r.ReadToEnd());
                    SetInfo();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("profile can't be loaded : " + ex.Message );
                this.Close();    
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            profileToEdit.Info.Nickname = nicknameTextBox.Text;
            profileToEdit.Info.Side = sideselectorComboBox.SelectedItem.ToString();
            profileToEdit.Info.Experience = Convert.ToInt32(experienceBox.Value);

            using ( StreamWriter file = File.CreateText(profilePath) )
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, profileToEdit);
            }

        }

        private void SetInfo()
        {
            this.Text += profileToEdit.Info.Nickname;
            this.nicknameTextBox.Text = profileToEdit.Info.Nickname;
            this.sideselectorComboBox.SelectedItem = profileToEdit.Info.Side;
            this.experienceBox.Value = profileToEdit.Info.Experience;
            this.gameVersionCombo.SelectedItem = profileToEdit.Info.GameVersion;

            this.enduranceNumericBox.Value =    GetSkillValue("Endurance");
            this.strenghNumericBox.Value =      GetSkillValue("Strength");
            this.vitalityNumericBox.Value =     GetSkillValue("Vitality");
            this.healthNumericBox.Value =       GetSkillValue("Health");
            this.strenghNumericBox.Value =      GetSkillValue("StressResistance");

            /* lazy to finnish
            this.enduranceNumericBox.Value =    GetSkillValue("Endurance");
            this.enduranceNumericBox.Value =    GetSkillValue("Endurance");
            this.enduranceNumericBox.Value =    GetSkillValue("Endurance");
            this.enduranceNumericBox.Value =    GetSkillValue("Endurance");
            this.enduranceNumericBox.Value =    GetSkillValue("Endurance");
            this.enduranceNumericBox.Value =    GetSkillValue("Endurance");
            this.enduranceNumericBox.Value =    GetSkillValue("Endurance");
            */
        }

        private void SaveProfile()
        {

        }

        private decimal GetSkillValue(string skill)
        {          
            return this.profileToEdit.Skills.Common[profileToEdit.Skills.Common.FindIndex(x => x.Id.Equals(skill))].Progress;
        }

        private void SetSkillValue(string skill, decimal newval)
        {
            this.profileToEdit.Skills.Common[profileToEdit.Skills.Common.FindIndex(x => x.Id.Equals(skill))].Progress = newval;
        }

    }
}
