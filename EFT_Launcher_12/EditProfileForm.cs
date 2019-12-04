using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EFT_Launcher_12
{
    public partial class EditProfileForm : Form
    {
        string profilePath;
        ProfileExtended profileToEdit;
        List<HideoutUpgradesArea> hideoutLevels;

        public EditProfileForm(int id)
        {
            profilePath = Path.Combine(Globals.serverFolder, "appdata/profiles/" + id + "/character.json");
            hideoutLevels = new List<HideoutUpgradesArea>();

            #region hideoutlevel init

            hideoutLevels.Add(new HideoutUpgradesArea(0, "", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(1, "", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(2, "", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(3, "Stash", 4));
            hideoutLevels.Add(new HideoutUpgradesArea(4, "", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(5, "", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(6, "", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(7, "", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(8, "", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(9, "", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(10, "Workbench", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(11, "", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(12, "", 1));
            hideoutLevels.Add(new HideoutUpgradesArea(13, "", 1));
            hideoutLevels.Add(new HideoutUpgradesArea(14, "", 1));
            hideoutLevels.Add(new HideoutUpgradesArea(15, "", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(16, "", 1));
            hideoutLevels.Add(new HideoutUpgradesArea(17, "", 1));
            hideoutLevels.Add(new HideoutUpgradesArea(18, "", 1));
            hideoutLevels.Add(new HideoutUpgradesArea(19, "", 1));
            hideoutLevels.Add(new HideoutUpgradesArea(20, "Bitcoin Farm", 3));

            #endregion
            InitializeComponent();
        }

        private void EditProfileForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (StreamReader r = new StreamReader(profilePath))
                {
                    profileToEdit = JsonConvert.DeserializeObject<ProfileExtended>(r.ReadToEnd());
                    SetInfo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("profile can't be loaded : " + ex.Message);
                this.Close();
            }

            foreach (HideoutUpgradesArea h in hideoutLevels)
            {
                hideoutAreaComboBox.Items.Add(h.areaName);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            profileToEdit.Info.Nickname = nicknameTextBox.Text;
            profileToEdit.Info.Side = sideselectorComboBox.SelectedItem.ToString();
            profileToEdit.Info.Experience = Convert.ToInt32(experienceBox.Value);

            using (StreamWriter file = File.CreateText(profilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, profileToEdit);
            }
        }

        private void SetInfo()
        {
            Text += profileToEdit.Info.Nickname;

            nicknameTextBox.Text = profileToEdit.Info.Nickname;
            sideselectorComboBox.SelectedItem = profileToEdit.Info.Side;
            experienceBox.Value = profileToEdit.Info.Experience;
            gameVersionCombo.SelectedItem = profileToEdit.Info.GameVersion;

            #region INIT SKILLS numericBoxes
            this.enduranceNumericBox.Value = GetSkillValue("Endurance");
            this.strenghNumericBox.Value = GetSkillValue("Strength");
            this.vitalityNumericBox.Value = GetSkillValue("Vitality");
            this.healthNumericBox.Value = GetSkillValue("Health");
            this.stressNumericBox.Value = GetSkillValue("StressResistance");


            this.metabolismNumericBox.Value = GetSkillValue("Metabolism");
            this.immunityNumericBox.Value = GetSkillValue("Immunity");
            this.perceptionNumericBox.Value = GetSkillValue("Perception");
            this.intelNumericBox.Value = GetSkillValue("Intellect");
            this.attentionNumericBox.Value = GetSkillValue("Attention");
            this.charismaNumericBox.Value = GetSkillValue("Charisma");
            this.memoryNumericBox.Value = GetSkillValue("Memory");

            this.covertNumericBox.Value = GetSkillValue("CovertMovement");
            this.recoilNumericBox.Value = GetSkillValue("RecoilControl");
            this.searchNumericBox.Value = GetSkillValue("Search");
            this.magdrillsNumericBox.Value = GetSkillValue("MagDrills");
            #endregion
        }

        private void SaveProfile()
        {


        }

        private decimal GetSkillValue(string skill)
        {
            return profileToEdit.Skills.Common[profileToEdit.Skills.Common.FindIndex(x => x.Id.Equals(skill))].Progress;
        }

        private void SetSkillValue(string skill, decimal newval)
        {
            profileToEdit.Skills.Common[profileToEdit.Skills.Common.FindIndex(x => x.Id.Equals(skill))].Progress = newval;
        }

        private void hideoutAreaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            hideoutLevelNumeric.Maximum = hideoutLevels[this.hideoutAreaComboBox.SelectedIndex].levelMax;
            hideoutLevelNumeric.Value = profileToEdit.Hideout.Areas.Find(x => x.type.Equals(this.hideoutAreaComboBox.SelectedIndex)).level;
        }

        private void hideoutLevelNumeric_ValueChanged(object sender, EventArgs e)
        {
            profileToEdit.Hideout.Areas.Find(x => x.type.Equals(this.hideoutAreaComboBox.SelectedIndex)).level = Convert.ToInt32(hideoutLevelNumeric.Value);
        }

        /// <summary>
        /// hideout upgrades level object
        /// </summary>
        internal class HideoutUpgradesArea
        {
            public int areaType;
            public string areaName;
            public int levelMax;
			public int areaType;
			public string areaName;
			public int levelMax;


            public HideoutUpgradesArea(int a, string n, int u)
            {
                areaType = a;
                areaName = n;
                levelMax = u;
            }
        }
    }
}