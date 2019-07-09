using System.IO;
using Launcher.Code.Helper;

namespace Launcher.Code.Settings
{
    public class SettingsBase<T> where T : new()
    {
        protected T config { private set; get; }
        private string filepath = "";

        protected SettingsBase(string filepath, string file)
        {
            this.filepath = Path.Combine(filepath, file);
            LoadSettings();
        }

        public virtual void LoadSettings()
        {
            if (File.Exists(filepath))
            {
                config = JSON.Load<T>(filepath);
            }
            else
            {
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
