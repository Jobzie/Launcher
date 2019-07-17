using System.Collections.Generic;

namespace Launcher.Code.Data
{
    public class ProfileData
    {
        //public List<Profile> profiles = new List<Profile>();
        public string email = @"user0@jet.com";
        public string password = "password";
        public int id = 0;
        public int timestamp = 0;
        public bool online = false;
    }

    public class FullProfileChars {
        public int err = 0;
        public string errmsg = "";
        public CharacterData[] data = new CharacterData[2];

    }
    public class CharacterData
    {
        public string _id = "";
        public int aid = 0;
        public string savage = "";
        public Info Info = new Info();
        public Customization Customization = new Customization();
        public Health Health = new Health();
        public string Inventory = "";       //save it as string we will not change that
        public string Skills = "";          //save it as string we will not change that
        public string Stats = "";           //save it as string we will not change that
        public string Encyclopedia = "";    //save it as string we will not change that
        public string ConditionCounters = "";//save it as string we will not change that
        public string BackendCounters = ""; //save it as string we will not change that
        public string InsuredItems = "";    //save it as string we will not change that
        public string Notes = "";           //save it as string we will not change that
        public string Quests = "";          //save it as string we will not change that
        public string TraderStandings = ""; //save it as string we will not change that
        public string RagfairInfo = "";     //save it as string we will not change that
        public string WishList = "";        //save it as string we will not change that
    }
    public class Info {
        public string Nickname = "";
        public string LowerNickname = "";
        public string Side = "";
        public string Voice = "";
        public int Level = 1;
        public int Experience = 0;
        public int RegistrationDate = 0;
        public string GameVersion = "";
        public int AccountType = 0;
        public int MemberCategory = 0;
        public bool lockedMoveCommands = false;
        public int LastTimePlayedAsSavage = 0;
        public Info_Settings Settings = new Info_Settings();
        public bool NeedWipe = false;
        public bool GlobalWipe = false;
        public int NicknameChangeDate = 0;

    }
    public class Info_Settings {
        public string Role = "assault";
        public string BotDifficulty = "easy";
        public int Experience = -1;
    }
    public class Customization
    {
        public Customization_Part Head = new Customization_Part();
        public Customization_Part Body = new Customization_Part();
        public Customization_Part Feet = new Customization_Part();
        public Customization_Part Hands = new Customization_Part();
    }
    public class Customization_Part {
        public string path = "";
        public string rcid = "";
    }
    public class Health {
        public HCM Hydration = new HCM();
        public HCM Energy = new HCM();
        public BodyParts BodyParts = new BodyParts();
    }
    public class BodyParts {
        public B_Part Head = new B_Part(35,35);
        public B_Part Chest = new B_Part(80,80);
        public B_Part Stomach = new B_Part(70,70);
        public B_Part LeftArm = new B_Part(60,60);
        public B_Part RightArm = new B_Part(60, 60);
        public B_Part LeftLeg = new B_Part(65, 65);
        public B_Part RightLeg = new B_Part(65, 65);
    }
    public class B_Part {
        private static int Current = 0;
        private static int Maximum = 0;
        public B_Part(int C, int M)
        {
            Current = C;
            Maximum = M;
        }
        public HCM Health = new HCM(Current, Maximum);

    }
    // Health Control Managament
    public class HCM {
        public int Current = 100;
        public int Maximum = 100;
        public HCM(int Current = 100, int Max = 100)
        {
            this.Current = Current;
            this.Maximum = Max;
        }
    }
}
