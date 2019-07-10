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

        public bool GetBotsPmcWarEnabled()
        {
            return base.config.bots.pmcWar.enabled;
        }

        public void SetBotsPmcWarEnabled(bool value)
        {
            base.config.bots.pmcWar.enabled = value;
            base.SaveSettings();
        }

        public string GetBotsPmcWarUsecChance()
        {
            return base.config.bots.pmcWar.chanceUsec.ToString();
        }

        public void SetBotsPmcWarUsecChance(string value)
        {
            base.config.bots.pmcWar.chanceUsec = Convert.ToInt32(value);
            base.SaveSettings();
        }
    }
}
