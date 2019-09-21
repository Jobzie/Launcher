using System;
using Launcher.Code.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Dynamic;

namespace Launcher.Code.Settings
{
    public class ServerSettings
    {
        private string FullFilepath = "";
        dynamic profile_data = new ExpandoObject();// = JsonConvert.DeserializeObject("{}");
        public ServerSettings(string filepath)
        {
            this.FullFilepath = filepath + @"\server.config.json";
            if (File.Exists(this.FullFilepath))
            {
                using (StreamReader sr = new StreamReader(FullFilepath))
                {
                    string json = sr.ReadToEnd();
                    profile_data = JsonConvert.DeserializeObject(json);
                }
            }
            else
            {
                Console.WriteLine("Cannot find file to save");
            }
        }
        private void Reload() {
            if (File.Exists(this.FullFilepath))
            {
                using (StreamReader sr = new StreamReader(FullFilepath))
                {
                    string json = sr.ReadToEnd();
                    profile_data = JsonConvert.DeserializeObject(json);
                }
            }
        }
        public void SetClientLocation(string v) {
            profile_data.game = v;
            Save();
        }
        public string GetClientLocation() {
            //Console.WriteLine(profile_data.game);
            if (profile_data.game == null)
                SetClientLocation(@"C:\EFT");
            return profile_data.game.ToString();
        }
        #region SERVER
        public string GetServerPort()
        {
            return profile_data.server.port.ToString();
        }

        public void SetServerPort(string value)
        {
            profile_data.server.port = Convert.ToInt32(value);
            Save();
        }
        #endregion

        #region BOTS_PMCWAR
        public bool GetBotsPmcWarEnabled()
        {
            return profile_data.bots.pmcWar.enabled;
        }

        public void SetBotsPmcWarEnabled(bool value)
        {
            profile_data.bots.pmcWar.enabled = value;
            Save();
        }

        public string GetBotsPmcWarUsecChance()
        {
            return profile_data.bots.pmcWar.chanceUsec.ToString();
        }

        public void SetBotsPmcWarUsecChance(string value)
        {
            profile_data.bots.pmcWar.chanceUsec = Convert.ToInt32(value);
            Save();
        }
        #endregion

        #region BOTS_LIMIT
        public string GetBotsLimitKilla()
        {
            return profile_data.bots.limit.bossKilla.ToString();
        }

        public void SetBotsLimitKilla(string value)
        {
            profile_data.bots.limit.bossKilla = Convert.ToInt32(value);
            Save();
        }

        public string GetBotsLimitBully()
        {
            return profile_data.bots.limit.bossBully.ToString();
        }

        public void SetBotsLimitBully(string value)
        {
            profile_data.bots.limit.bossBully = Convert.ToInt32(value);
            Save();
        }

        public string GetBotsLimitBullyFollowers()
        {
            return profile_data.bots.limit.bullyFollowers.ToString();
        }

        public void SetBotsLimitBullyFollowers(string value)
        {
            profile_data.bots.limit.bullyFollowers = Convert.ToInt32(value);
            Save();
        }

        public string GetBotsLimitMarksman()
        {
            return profile_data.bots.limit.marksman.ToString();
        }

        public void SetBotsLimitMarksman(string value)
        {
            profile_data.bots.limit.marksman = Convert.ToInt32(value);
            Save();
        }

        public string GetBotsLimitPmcBot()
        {
            return profile_data.bots.limit.pmcBot.ToString();
        }

        public void SetBotsLimitPmcBot(string value)
        {
            profile_data.bots.limit.pmcBot = Convert.ToInt32(value);
            Save();
        }

        public string GetBotsLimitScav()
        {
            return profile_data.bots.limit.scav.ToString();
        }

        public void SetBotsLimitScav(string value)
        {
            profile_data.bots.limit.scav = Convert.ToInt32(value);
            Save();
        }
        #endregion

        #region BOTS_SPAWN
        public string GetBotsSpawnGlasses()
        {
            return profile_data.bots.spawn.glasses.ToString();
        }

        public void SetBotsSpawnGlasses(string value)
        {
            profile_data.bots.spawn.glasses = Convert.ToInt32(value);
            Save();
        }

        public string GetBotsSpawnFaceCover()
        {
            return profile_data.bots.spawn.faceCover.ToString();
        }

        public void SetBotsSpawnFaceCover(string value)
        {
            profile_data.bots.spawn.faceCover = Convert.ToInt32(value);
            Save();
        }

        public string GetBotsSpawnHeadwear()
        {
            return profile_data.bots.spawn.headwear.ToString();
        }

        public void SetBotsSpawnHeadwear(string value)
        {
            profile_data.bots.spawn.headwear = Convert.ToInt32(value);
            Save();
        }

        public string GetBotsSpawnBackpack()
        {
            return profile_data.bots.spawn.backpack.ToString();
        }

        public void SetBotsSpawnBackpack(string value)
        {
            profile_data.bots.spawn.backpack = Convert.ToInt32(value);
            Save();
        }

        public string GetBotsSpawnArmorVest()
        {
            return profile_data.bots.spawn.armorVest.ToString();
        }

        public void SetBotsSpawnArmorVest(string value)
        {
            profile_data.bots.spawn.armorVest = Convert.ToInt32(value);
            Save();
        }

        public string GetBotsSpawnMedPocket()
        {
            return profile_data.bots.spawn.medPocket.ToString();
        }

        public void SetBotsSpawnMedPocket(string value)
        {
            profile_data.bots.spawn.medPocket = Convert.ToInt32(value);
            Save();
        }

        public string GetBotsSpawnItemPocket()
        {
            return profile_data.bots.spawn.itemPocket.ToString();
        }

        public void SetBotsSpawnItemPocket(string value)
        {
            profile_data.bots.spawn.itemPocket = Convert.ToInt32(value);
            Save();
        }
        #endregion

        public void Save()
        {
            JsonSerializer serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            using (StreamWriter sw = new StreamWriter(FullFilepath))
            {
                    serializer.Serialize(sw, profile_data);
            }
            Reload();
        }
    }
}
