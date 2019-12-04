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
                Close();
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
        }

        private void SetInfo()
        {
            Text += profileToEdit.Info.Nickname;

            nicknameTextBox.Text = profileToEdit.Info.Nickname;
            sideselectorComboBox.SelectedItem = profileToEdit.Info.Side;
            experienceBox.Value = profileToEdit.Info.Experience;
            gameVersionCombo.SelectedItem = profileToEdit.Info.GameVersion;

            #region INIT SKILLS numericBoxes
            enduranceNumericBox.Value = GetSkillValue("Endurance");
            strenghNumericBox.Value = GetSkillValue("Strength");
            vitalityNumericBox.Value = GetSkillValue("Vitality");
            healthNumericBox.Value = GetSkillValue("Health");
            stressNumericBox.Value = GetSkillValue("StressResistance");

            metabolismNumericBox.Value = GetSkillValue("Metabolism");
            immunityNumericBox.Value = GetSkillValue("Immunity");
            perceptionNumericBox.Value = GetSkillValue("Perception");
            intelNumericBox.Value = GetSkillValue("Intellect");
            attentionNumericBox.Value = GetSkillValue("Attention");
            charismaNumericBox.Value = GetSkillValue("Charisma");
            memoryNumericBox.Value = GetSkillValue("Memory");

            covertNumericBox.Value = GetSkillValue("CovertMovement");
            recoilNumericBox.Value = GetSkillValue("RecoilControl");
            searchNumericBox.Value = GetSkillValue("Search");
            magdrillsNumericBox.Value = GetSkillValue("MagDrills");
            #endregion
        }

        private void SaveProfile()
        {
            
            SetSkillValue("Endurance", enduranceNumericBox.Value);
            SetSkillValue("Strength", strenghNumericBox.Value);
            SetSkillValue("Vitality", vitalityNumericBox.Value);
            SetSkillValue("Health", healthNumericBox.Value);
            SetSkillValue("StressResistance", stressNumericBox.Value);
            SetSkillValue("Metabolism", metabolismNumericBox.Value);
            SetSkillValue("Immunity", immunityNumericBox.Value);
            SetSkillValue("Perception", perceptionNumericBox.Value);
            SetSkillValue("Intellect", intelNumericBox.Value);
            SetSkillValue("Attention", attentionNumericBox.Value);
            SetSkillValue("Charisma", charismaNumericBox.Value);
            SetSkillValue("Memory", memoryNumericBox.Value);
            SetSkillValue("CovertMovement", covertNumericBox.Value);
            SetSkillValue("RecoilControl", recoilNumericBox.Value);
            SetSkillValue("Search", searchNumericBox.Value);
            SetSkillValue("MagDrills", magdrillsNumericBox.Value);

            using (StreamWriter file = File.CreateText(profilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, profileToEdit);
            }
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

            public HideoutUpgradesArea(int a, string n, int u)
            {
                areaType = a;
                areaName = n;
                levelMax = u;
            }
        }
    }
}
