using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using Launcher.Code.Data;
using Newtonsoft.Json;

namespace Launcher.Code.Settings
{
    class ProfileCharacters
    {
        private string FullFilepath = "";
        //public static string data = "{\"err\": 0,\"errmsg\": null,\"data\": [ {\"_id\":\"5c71b934354682353958e983\",\"aid\": 0,\"savage\": null,\"Info\": {\"Nickname\":\"Scav\",\"LowerNickname\":\"scav\",\"Side\":\"savage\",\"Voice\":\"Scav_1\",\"Level\": 1,\"Experience\": 0,\"RegistrationDate\": 0,\"GameVersion\":\"\",\"AccountType\": 0,\"MemberCategory\": 0,\"lockedMoveCommands\": false,\"LastTimePlayedAsSavage\": 0,\"Settings\": {},\"NeedWipe\": false,\"GlobalWipe\": false,\"NicknameChangeDate\": 0 },\"Customization\": {\"Head\": {\"path\":\"assets/content/characters/character/prefabs/wild_head_1.bundle\",\"rcid\": null },\"Body\": {\"path\":\"assets/content/characters/character/prefabs/wild_body_1.bundle\",\"rcid\":\"\" },\"Feet\": {\"path\":\"assets/content/characters/character/prefabs/wild_feet.bundle\",\"rcid\": null },\"Hands\": {\"path\":\"assets/content/hands/wild/wild_body_1_firsthands.bundle\",\"rcid\": null } },\"Health\": {\"Hydration\": {\"Current\": 100,\"Maximum\": 100 },\"Energy\": {\"Current\": 100,\"Maximum\": 100 },\"BodyParts\": {\"Head\": {\"Health\": {\"Current\": 0,\"Maximum\": 0 } },\"Chest\": {\"Health\": {\"Current\": 0,\"Maximum\": 0 } },\"Stomach\": {\"Health\": {\"Current\": 0,\"Maximum\": 0 } },\"LeftArm\": {\"Health\": {\"Current\": 0,\"Maximum\": 0 } },\"RightArm\": {\"Health\": {\"Current\": 0,\"Maximum\": 0 } },\"LeftLeg\": {\"Health\": {\"Current\": 0,\"Maximum\": 0 } },\"RightLeg\": {\"Health\": {\"Current\": 0,\"Maximum\": 0 } } } },\"Inventory\": {\"items\": [ {\"_id\":\"5c6687d65e9d882c8841f0fd\",\"_tpl\":\"55d7217a4bdc2d86028b456d\" }, {\"_id\":\"5c6687d65e9d882c8841f121\",\"_tpl\":\"557ffd194bdc2d28148b457f\",\"parentId\":\"5c6687d65e9d882c8841f0fd\",\"slotId\":\"Pockets\" }, {\"_id\":\"5c6687d65e9d882c8841f0fc\",\"_tpl\":\"566abbc34bdc2d92178b4576\" }, {\"_id\":\"5c6687d65e9d882c8841f0ff\",\"_tpl\":\"5963866b86f7747bfa1c4462\" }, {\"_id\":\"5c6687d65e9d882c8841f0fe\",\"_tpl\":\"5963866286f7747bf429b572\" } ],\"equipment\":\"5c6687d65e9d882c8841f0fd\",\"stash\":\"5c6687d65e9d882c8841f0fc\",\"questRaidItems\":\"5c6687d65e9d882c8841f0fe\",\"questStashItems\":\"5c6687d65e9d882c8841f0ff\",\"fastPanel\": {} },\"Skills\": {\"Common\": [],\"Mastering\": [],\"Points\": 0 },\"Stats\": {\"SessionCounters\": {\"Items\": [] },\"OverallCounters\": {\"Items\": [] } },\"Encyclopedia\": null,\"ConditionCounters\": {\"Counters\": [] },\"BackendCounters\": {},\"InsuredItems\": [] },{\"_id\":\"5c71b934354682353958e983\",\"aid\": 0,\"savage\": null,\"Info\": {\"Nickname\":\"Scav\",\"LowerNickname\":\"scav\",\"Side\":\"savage\",\"Voice\":\"Scav_1\",\"Level\": 1,\"Experience\": 0,\"RegistrationDate\": 0,\"GameVersion\":\"\",\"AccountType\": 0,\"MemberCategory\": 0,\"lockedMoveCommands\": false,\"LastTimePlayedAsSavage\": 0,\"Settings\": {},\"NeedWipe\": false,\"GlobalWipe\": false,\"NicknameChangeDate\": 0 },\"Customization\": {\"Head\": {\"path\":\"assets/content/characters/character/prefabs/wild_head_1.bundle\",\"rcid\": null },\"Body\": {\"path\":\"assets/content/characters/character/prefabs/wild_body_1.bundle\",\"rcid\":\"\" },\"Feet\": {\"path\":\"assets/content/characters/character/prefabs/wild_feet.bundle\",\"rcid\": null },\"Hands\": {\"path\":\"assets/content/hands/wild/wild_body_1_firsthands.bundle\",\"rcid\": null } },\"Health\": {\"Hydration\": {\"Current\": 100,\"Maximum\": 100 },\"Energy\": {\"Current\": 100,\"Maximum\": 100 },\"BodyParts\": {\"Head\": {\"Health\": {\"Current\": 0,\"Maximum\": 0 } },\"Chest\": {\"Health\": {\"Current\": 0,\"Maximum\": 0 } },\"Stomach\": {\"Health\": {\"Current\": 0,\"Maximum\": 0 } },\"LeftArm\": {\"Health\": {\"Current\": 0,\"Maximum\": 0 } },\"RightArm\": {\"Health\": {\"Current\": 0,\"Maximum\": 0 } },\"LeftLeg\": {\"Health\": {\"Current\": 0,\"Maximum\": 0 } },\"RightLeg\": {\"Health\": {\"Current\": 0,\"Maximum\": 0 } } } },\"Inventory\": {\"items\": [ {\"_id\":\"5c6687d65e9d882c8841f0fd\",\"_tpl\":\"55d7217a4bdc2d86028b456d\" }, {\"_id\":\"5c6687d65e9d882c8841f121\",\"_tpl\":\"557ffd194bdc2d28148b457f\",\"parentId\":\"5c6687d65e9d882c8841f0fd\",\"slotId\":\"Pockets\" }, {\"_id\":\"5c6687d65e9d882c8841f0fc\",\"_tpl\":\"566abbc34bdc2d92178b4576\" }, {\"_id\":\"5c6687d65e9d882c8841f0ff\",\"_tpl\":\"5963866b86f7747bfa1c4462\" }, {\"_id\":\"5c6687d65e9d882c8841f0fe\",\"_tpl\":\"5963866286f7747bf429b572\" } ],\"equipment\":\"5c6687d65e9d882c8841f0fd\",\"stash\":\"5c6687d65e9d882c8841f0fc\",\"questRaidItems\":\"5c6687d65e9d882c8841f0fe\",\"questStashItems\":\"5c6687d65e9d882c8841f0ff\",\"fastPanel\": {} },\"Skills\": {\"Common\": [],\"Mastering\": [],\"Points\": 0 },\"Stats\": {\"SessionCounters\": {\"Items\": [] },\"OverallCounters\": {\"Items\": [] } },\"Encyclopedia\": null,\"ConditionCounters\": {\"Counters\": [] },\"BackendCounters\": {},\"InsuredItems\": [] } ] }";
        dynamic profile_content = new ExpandoObject();// = JsonConvert.DeserializeObject(data);
        public ProfileCharacters(string filepath, string filename = "") // last parameter is for object oriented return
        {
            this.FullFilepath = filepath + @"\character.json";
            if (File.Exists(this.FullFilepath))
            {
                using (StreamReader sr = new StreamReader(FullFilepath))
                {
                    string json = sr.ReadToEnd();
                    profile_content = JsonConvert.DeserializeObject(json);
                }
            }
        }
        private void Reload()
        {
            if (File.Exists(this.FullFilepath))
            {
                using (StreamReader sr = new StreamReader(FullFilepath))
                {
                    string json = sr.ReadToEnd();
                    profile_content = JsonConvert.DeserializeObject(json);
                }
            }
        }
        private void Save()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            if (File.Exists(this.FullFilepath))
            {
                using (StreamWriter sw = new StreamWriter(FullFilepath))
                {
                    serializer.Serialize(sw, profile_content);
                }
            }
            else
            {
                Console.WriteLine("Cannot find file to save");
            }
            Reload();
        }
        // Functions below
        public void ReloadProfiles() {
            using (StreamReader sr = new StreamReader(FullFilepath))
            {
                string json = sr.ReadToEnd();
                profile_content = JsonConvert.DeserializeObject(json);
            }
        }
        public string GetCharacterCustomizationPart(string Part) {
            string prepare = "";
            switch (Part)
            {
                case "Head":
                    prepare = profile_content.data[1].Customization.Head.path;
                    break;
                case "Body":
                    prepare = profile_content.data[1].Customization.Body.path;
                    break;
                case "Feet":
                    prepare = profile_content.data[1].Customization.Feet.path;
                    break;
                case "Hands":
                    prepare = profile_content.data[1].Customization.Hands.path;
                    break;
            }
            prepare = prepare.Replace("assets/content/characters/character/prefabs/", "").Replace(".bundle", "");
            return prepare;
        }
        public void SaveCharacterCustomization(string Part, string value) {
            switch (Part) {
                case "Head":
                    profile_content.data[1].Customization.Head.path = value;
                    break;
                case "Body":
                    profile_content.data[1].Customization.Body.path = value;
                    break;
                case "Feet":
                    profile_content.data[1].Customization.Feet.path = value;
                    break;
                case "Hands":
                    profile_content.data[1].Customization.Hands.path = value;
                    break;
            }
            Save();
        }
        public string GetNickname()
        {
            return profile_content.data[1].Info.Nickname;
        }
        public void SetProfileID(int i)
        {
            profile_content.data[1].aid = i;
            Save();
        }
        public void ChangeNickname(string newName, int profType = 1) {
            profile_content.data[profType].Info.Nickname = newName;
            profile_content.data[profType].Info.LowerNickname = newName.Replace(" ","").ToLower();
            Save();
        }

        public void ChangeSide(string newSide, int profType = 1) {
            profile_content.data[profType].Info.Side = newSide;
            Save();
        }
}
}
