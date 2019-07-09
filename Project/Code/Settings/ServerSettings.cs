using System;
using Launcher.Code.Data;

namespace Launcher.Code.Settings
{
    public class ServerSettings : SettingsBase<ServerConfig>
    {
        public ServerSettings(string filepath) : base(filepath, "config.json")
        {
            // for calling base constructor
        }

        public string GetServerPort()
        {
            return base.config.server.port.ToString();
        }

        public void SetServerPort(string value)
        {
            base.config.server.port = Convert.ToInt32(value);
            base.SaveSettings();
        }
    }
}
