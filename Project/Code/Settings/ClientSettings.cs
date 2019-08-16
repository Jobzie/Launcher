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
    public class ClientSettings
    {
        public string FullFilepath = "";
        dynamic data = new ExpandoObject();// = JsonConvert.DeserializeObject("{}");
        public ClientSettings(string filepath)
        {
            this.FullFilepath = filepath + @"\client.config.json";
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
        public string GetBackendURL()
        {
            return data.BackendUrl.ToString();
        }

        public void SetBackendURL(string value)
        {
            data.BackendUrl = value;
            Save();
        }
        private void Save()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
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
    }
}
