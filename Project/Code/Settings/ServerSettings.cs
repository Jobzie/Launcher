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

        #region SERVER
        public string GetServerPort()
        {
            return base.config.server.port.ToString();
        }

        public void SetServerPort(string value)
        {
            base.config.server.port = Convert.ToInt32(value);
            base.SaveSettings();
        }
        #endregion

        #region BOTS_PMCWAR
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
        #endregion

        #region BOTS_LIMIT
        public string GetBotsLimitKilla()
        {
            return base.config.bots.limit.bossKilla.ToString();
        }

        public void SetBotsLimitKilla(string value)
        {
            base.config.bots.limit.bossKilla = Convert.ToInt32(value);
            base.SaveSettings();
        }

        public string GetBotsLimitBully()
        {
            return base.config.bots.limit.bossBully.ToString();
        }

        public void SetBotsLimitBully(string value)
        {
            base.config.bots.limit.bossBully = Convert.ToInt32(value);
            base.SaveSettings();
        }

        public string GetBotsLimitBullyFollowers()
        {
            return base.config.bots.limit.bullyFollowers.ToString();
        }

        public void SetBotsLimitBullyFollowers(string value)
        {
            base.config.bots.limit.bullyFollowers = Convert.ToInt32(value);
            base.SaveSettings();
        }

        public string GetBotsLimitMarksman()
        {
            return base.config.bots.limit.marksman.ToString();
        }

        public void SetBotsLimitMarksman(string value)
        {
            base.config.bots.limit.marksman = Convert.ToInt32(value);
            base.SaveSettings();
        }

        public string GetBotsLimitPmcBot()
        {
            return base.config.bots.limit.pmcBot.ToString();
        }

        public void SetBotsLimitPmcBot(string value)
        {
            base.config.bots.limit.pmcBot = Convert.ToInt32(value);
            base.SaveSettings();
        }

        public string GetBotsLimitScav()
        {
            return base.config.bots.limit.scav.ToString();
        }

        public void SetBotsLimitScav(string value)
        {
            base.config.bots.limit.scav = Convert.ToInt32(value);
            base.SaveSettings();
        }
        #endregion
    }
}
