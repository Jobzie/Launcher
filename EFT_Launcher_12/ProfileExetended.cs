using System.Collections.Generic;

namespace EFT_Launcher_12
{
    public struct Settings
    {
        public string Role;
        public string BotDifficulty;
        public int Experience;
    }

    public struct Info
    {
        public string Nickname;
        public string LowerNickname;
        public string Side;
        public string Voice;
        public int Level;
        public int Experience;
        public int RegistrationDate;
        public string GameVersion;
        public int AccountType;
        public string MemberCategory;
        public bool lockedMoveCommands;
        public int SavageLockTime;
        public int LastTimePlayedAsSavage;
        public Settings Settings;
        public bool NeedWipe;
        public bool GlobalWipe;
        public int NicknameChangeDate;
        public List<object> Bans;
    }

    public struct Customization
    {
        public string Head;
        public string Body;
        public string Feet;
        public string Hands;
    }

    public struct Hydration
    {
        public int Current;
        public int Maximum;
    }

    public struct Energy
    {
        public int Current;
        public int Maximum;
    }

    public struct Health2
    {
        public int Current;
        public int Maximum;
    }

    public struct Head
    {
        public Health2 Health;
    }

    public struct Health3
    {
        public int Current;
        public int Maximum;
    }

    public struct Chest
    {
        public Health3 Health;
    }

    public struct Health4
    {
        public int Current;
        public int Maximum;
    }

    public struct Stomach
    {
        public Health4 Health;
    }

    public struct Health5
    {
        public int Current;
        public int Maximum;
    }

    public struct LeftArm
    {
        public Health5 Health;
    }

    public struct Health6
    {
        public int Current;
        public int Maximum;
    }

    public struct RightArm
    {
        public Health6 Health;
    }

    public struct Health7
    {
        public int Current;
        public int Maximum;
    }

    public struct LeftLeg
    {
        public Health7 Health;
    }

    public struct Health8
    {
        public int Current;
        public int Maximum;
    }

    public struct RightLeg
    {
        public Health8 Health;
    }

    public struct BodyParts
    {
        public Head Head;
        public Chest Chest;
        public Stomach Stomach;
        public LeftArm LeftArm;
        public RightArm RightArm;
        public LeftLeg LeftLeg;
        public RightLeg RightLeg;
    }

    public struct Health
    {
        public Hydration Hydration;
        public Energy Energy;
        public BodyParts BodyParts;
        public int UpdateTime;
    }

    public struct Foldable
    {
        public bool Folded;
    }

    public struct Tag
    {
        public int Color;
        public string Name;
    }

    public struct Upd
    {
        public int StackObjectsCount;
        public Foldable Foldable;
        public Tag Tag;
    }

    public struct Item
    {
        public string _id;
        public string _tpl;
        public string parentId;
        public string slotId;
        public Upd upd;
        public object location;
    }

    public struct FastPanel
    {
		// intentionally empty
	}

	public struct Inventory
    {
        public List<Item> items;
        public string equipment;
        public string stash;
        public string questRaidItems;
        public string questStashItems;
        public FastPanel fastPanel;
    }

    public struct Common
    {
        public string Id;
        public decimal Progress;
        public decimal MaxAchieved;
        public int LastAccess;
    }

    public struct Skills
    {
        public List<Common> Common;
        public List<object> Mastering;
        public int Points;
    }

    public struct OverallCounters
    {
       public List<object> Items;
    }

    public struct Stats
    {
        public object SessionCounters;
        public OverallCounters OverallCounters;
        public double SessionExperienceMult;
        public double TotalSessionExperience;
        public int LastSessionDate;
        public object Aggressor;
        public List<object> DroppedItems;
        public List<object> FoundInRaidItems;
        public List<object> Victims;
        public List<object> CarriedQuestItems;
        public int TotalInGameTime;
        public string Survivorstruct;
    }

    public struct Encyclopedia
    {
		// intentionally empty
	}

	public struct ConditionCounters
    {
        public List<object> Counters;
    }

    public struct BackendCounters
    {
		// intentionally empty
    }

    public struct Production
    {
        public int Progress;
        public bool inProgress;
        public string RecipeId;
        public List<object> Products;
        public int StartTime;
    }

    public struct Area
    {
        public int type;
        public int level;
        public bool active;
        public bool passiveBonusesEnabled;
        public int completeTime;
        public bool constructing;
        public List<object> slots;
    }

    public struct Hideout
    {
        public Production Production;
        public List<Area> Areas;
    }

    public struct Bonus
    {
        public string type;
        public string templateId;
    }

    public struct Notes
    {
        private List<object> notes;

        public List<object> GetNotes()
        {
            return notes;
        }

        public void SetNotes(List<object> value)
        {
            notes = value;
        }
    }

    public struct TraderStandings
    {
		// intentionally empty
    }

    public struct RagfairInfo
    {
        public double rating;
        public bool isRatingGrowing;
        public List<object> offers;
    }

    public struct ProfileExtended
    {
        public string _id;
        public int aid;
        public string savage;
        public Info Info;
        public Customization Customization;
        public Health Health;
        public Inventory Inventory;
        public Skills Skills;
        public Stats Stats;
        public Encyclopedia Encyclopedia;
        public ConditionCounters ConditionCounters;
        public BackendCounters BackendCounters;
        public List<object> InsuredItems;
        public Hideout Hideout;
        public List<Bonus> Bonuses;
        public Notes Notes;
        public List<object> Quests;
        public TraderStandings TraderStandings;
        public RagfairInfo RagfairInfo;
        public List<object> WishList;
    }
}
