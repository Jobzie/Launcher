using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text;
using Launcher.Code.Data;
using Newtonsoft.Json;
using System.Dynamic;

namespace Launcher.Code.Settings
{
    public class LauncherSettings
    {
        public string FullFilepath = "";
        dynamic data = new ExpandoObject();// = JsonConvert.DeserializeObject("{\"email\": \"user1@jet.com\",\"password\": \"password\",\"clientLocation\": \"M:/game/ET\",\"serverLocation\": \"M:/game/ETS\",\"clientFilename\": \"EscapeFromTarkov\",\"serverFilename\": \"EmuTarkov-Server\",\"screenMode\": 0,\"port\": 1337,\"ip\": \"localhost\",\"backendURL\": \"http://localhost:1337\"}");
        public LauncherSettings()
        {
            this.FullFilepath = @".\data\launcher.config.json";
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
        private void Save()
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
        // functions below
        #region EMAIL
        public string GetEmail()
        {
            return data.email.ToString();
        }

        public void SetEmail(string value)
        {
            data.email = value;
            Save();
        }
        #endregion
        #region Password
        public string GetPassword()
        {
            return data.password.ToString();
        }

        public void SetPassword(string value)
        {
            data.password = value;
            Save();
        }
        #endregion
        #region CLIENT DATA
        public string GetClientLocation()
        {
            return data.clientLocation.ToString();
        }
        public string GetClientFilename()
        {
            return data.clientFilename.ToString();
        }
        public int GetScreenMode()
        {
            return data.screenMode;
        }
        public void SetClientLocation(string value)
        {
            data.clientLocation = value;
            Save();
        }
        public void SetClientFilename(string value)
        {
            data.clientFilename = value;
            Save();
        }
        public void SetScreenMode(int value) {
            data.screenMode = value;
            Save();
        }

        #endregion
        #region SERVER DATA
        public string GetServerLocation()
        {
            return data.serverLocation.ToString();
        }
        public string GetServerFilename()
        {
            return data.serverFilename.ToString();
        }

        public void SetServerLocation(string value)
        {
            data.serverLocation = value;
            Save();
        }

        public void SetServerFilename(string value)
        {
            data.serverFilename = value;
            Save();
        }
        #endregion
        #region BACKEND URL
        public void SavePort(int value) {
            data.port = value;
            Save();
        }
        public string LoadPort() {
            return data.port.ToString();
        }
        public void SaveIP(string value) {
            data.ip = value;
            Save();
        }
        public string LoadIP() {
            return data.ip.ToString();
        }
        public string PrepareBackendURL(bool secured = false)
        {
            if (secured)
                return "https://" + LoadIP() + ":" + LoadPort();
            else
                return "http://" + LoadIP() + ":" + LoadPort();
        }
        #endregion
    }
}
