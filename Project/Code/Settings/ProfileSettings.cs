using Launcher.Code.Data;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Launcher.Code.Settings
{
    public class ProfileSettings
    {
        private string FullFilepath = "";
        dynamic profile_data = JsonConvert.DeserializeObject("{}");
        #region Iniciator
        public ProfileSettings(string filepath, string filename = "profiles.json") // last parameter is for object oriented return
        {
            this.FullFilepath = filepath + "\\" + filename;
            // for calling base constructor
            using (StreamReader sr = new StreamReader(FullFilepath))
            {
                string json = sr.ReadToEnd();
                profile_data = JsonConvert.DeserializeObject(json);
            }
        }
        #endregion
        #region Helpers
        public bool ListExists() {
            int counter = 0;
            foreach (var i in profile_data)
                counter++;
            return (counter > 0) ? true : false;
        }

        public int ListCount() {
            int counter = 0;
            foreach (var i in profile_data)
                counter++;
            return counter;
        }

        public bool CheckLoginApprove(string email, string password)
        {
            foreach (dynamic profile in profile_data)
            {
                if (profile.email == email && profile.password == password)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetProfile(string email, string password)
        {
            foreach (dynamic profile in profile_data)
            {
                if (profile.email == email && profile.password == password)
                {
                    return profile.id;
                }
            }
            return -1;
        }
        public void saveData()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(FullFilepath))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(sw, profile_data);
                }
            }
        }
        #endregion
        #region Main Functions
        public bool AddProfile(string email, string password)
        {
            if (GetProfile(email, password) != -1)
            {
                return false;
            }
            var temp = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(profile_data));
            dynamic newOne = new JObject();
            newOne.email = email;
            newOne.password = password;
            newOne.id = ListCount();
            newOne.timestamp = 0;
            newOne.online = false;
            temp.Add(newOne);
            profile_data = temp;
            temp = null;
            saveData();
            return true;
        }

        public void RemoveProfile(string email, string password)
        {
            int profileID = GetProfile(email, password);

            // profile doesn't exist
            if (profileID == -1)
            {
                return;
            }
            profile_data[profileID] = "";
            saveData();
        }

        public void ChangeProfileEmail(string email, string password, string newEmail)
        {
      
            int profileID = GetProfile(email, password);

            // profile doesn't exist
            if (profileID >= -1)
            {
                return;
            }

            // change the profile email
            profile_data[profileID].email = newEmail;
            saveData();
        }

        public void ChangeProfilePassword(string email, string password, string newPassword)
        {
                   
            int profileID = GetProfile(email, password);
            // profile doesn't exist
            if (profileID != -1)
            {
                return;
            }
            // change the profile password and save
            profile_data[profileID].password = newPassword;
            saveData();
                        
        }
        #endregion
    }
}
