using System.Collections.Generic;
using System.IO;
using Launcher.Code.Helper;

namespace Launcher.Code.Settings
{
    public class SettingsBase<T> where T : new()
    {
        protected T config { private set; get; }
        protected List<T> configObject { private set; get; }
        private string filepath = "";
        private bool isArraySwitch = false;

        protected SettingsBase(string filepath, string file, bool isArray = false)
        {
            this.isArraySwitch = isArray;
            this.filepath = Path.Combine(filepath, file);
            LoadSettings();
        }

        public virtual void LoadSettings()
        {
            if (File.Exists(filepath))
            {
                if (isArraySwitch)
                    configObject = JSON.LoadObject<T>(filepath);
                else
                    config = JSON.Load<T>(filepath);
            }
            else
            {
                if (isArraySwitch)
                    configObject = new List<T>();
                else
                    config = new T();
            }
        }

        public virtual void SaveSettings()
        {
            if (File.Exists(filepath))
            {
                JSON.Save<T>(filepath, config);
            }
        }
    }
}
