namespace Launcher.Code.Data
{
    public class ServerConfig
    {
        public ServerSettings server = new ServerSettings();
        public BotsSettings bots = new BotsSettings();

        public class ServerSettings
        {
            public string backendUrl = "http://127.0.0.1:1337";
            public string IP = "127.0.0.1";
            public int port = 1337;
        }

        public class BotsSettings
        {
            public PMCWarSettings pmcWar = new PMCWarSettings();
            public LimitSettings limit = new LimitSettings();
            public SpawnSettings spawn = new SpawnSettings();

            public class PMCWarSettings
            {
                public bool enabled = false;
                public int chanceUsec = 55;
            }

            public class LimitSettings
            {
                public int bossKilla = 1;
                public int bossBully = 1;
                public int bullyFollowers = 5;
                public int marksman = 10;
                public int pmcBot = 10;
                public int scav = 3;
            }

            public class SpawnSettings
            {
                public int glasses = 30;
                public int faceCover = 40;
                public int headwear = 40;
                public int backpack = 25;
                public int armorVest = 25;
                public int medPocket = 10;
                public int itemPocket = 10;
            }
        }
    }
}
