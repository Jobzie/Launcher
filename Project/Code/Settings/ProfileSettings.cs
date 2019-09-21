using Launcher.Code.Data;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System;
using System.Dynamic;
using System.Security.Cryptography;
using System.Text;

namespace Launcher.Code.Settings
{
    public class ProfileSettings
    {
        private string FullFilepath = "";
        dynamic data = new ExpandoObject();// = JsonConvert.DeserializeObject("{}");
        #region Iniciator
        public ProfileSettings(string filepath)
        {
            this.FullFilepath = filepath + @"\profiles.json";
            if (File.Exists(this.FullFilepath))
            {
                using (StreamReader sr = new StreamReader(FullFilepath))
                {
                    string json = sr.ReadToEnd();
                    data = JsonConvert.DeserializeObject(json);
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
                    data = JsonConvert.DeserializeObject(json);
                }
            }
        }
        #endregion
        #region Helpers
        public bool ListExists() {
            int counter = 0;
            foreach (var i in data)
                counter++;
            return (counter > 0) ? true : false;
        }

        public int ListCount() {
            int counter = 0;
            foreach (var i in data)
                counter++;
            return counter;
        }

        public bool CheckLoginApprove(string email, string password)
        {
            foreach (dynamic profile in data)
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
            foreach (dynamic profile in data)
            {
                if (profile.email == email && profile.password == password)
                {
                    return profile.id;
                }
            }
            return -1;
        }
        public void Save()
        {
            JsonSerializer serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            if (File.Exists(this.FullFilepath))
            {
                using (StreamWriter sw = new StreamWriter(FullFilepath))
                {
                    serializer.Serialize(sw, data);
                }
            }
            else
            {
                Console.WriteLine("Cannot find file to save");
            }
            Reload();
        }
        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
        #endregion
        #region Main Functions
        public bool AddProfile(string email, string password)
        {
            if (GetProfile(email, password) != -1)
            {
                return false;
            }
            var temp = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(data));
            dynamic newOne = new JObject();
            newOne.email = email;
            newOne.password = password;
            newOne.password_md5 = CalculateMD5Hash(password);
            newOne.id = ListCount();
            newOne.timestamp = 0;
            newOne.online = false;
            temp.Add(newOne);
            data = temp;
            temp = null;
            Save();
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
            data[profileID] = "";
            Save();
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
            data[profileID].email = newEmail;
            Save();
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
            data[profileID].password = newPassword;
            Save();
                        
        }
        #endregion
    }
}
