namespace Gellybeans.Pathfinder
{       
    public class StatModifier
    {
        public string   StatName    { get; set; }
        public Bonus    Bonus       { get; set; }

        public StatModifier(string statName)
        {
            StatName = statName;
        }
        
        
        public static Dictionary<string, List<StatModifier>> Buffs = new Dictionary<string, List<StatModifier>>()
        {
            {
                "BULLS", new List<StatModifier>()
                {
                    new StatModifier("STR_TEMP")            { Bonus = new Bonus() { Name = "BULLS", Type = BonusType.Enhancement, Value = 4 } }
                }
            },
            {
                "CATS", new List<StatModifier>()
                {
                    new StatModifier("DEX_TEMP")            { Bonus = new Bonus() { Name = "CATS", Type = BonusType.Enhancement, Value = 4 } }
                }
            },
            {
                "BEARS", new List<StatModifier>()
                {
                    new StatModifier("CON_TEMP")            { Bonus = new Bonus() { Name = "BEARS", Type = BonusType.Enhancement, Value = 4 } }
                }
            },
            {
                "FOXS", new List<StatModifier>()
                {
                    new StatModifier("INT_TEMP")            { Bonus = new Bonus() { Name = "FOXS", Type = BonusType.Enhancement, Value = 4 } }
                }
            },
            {
                "OWLS", new List<StatModifier>()
                {
                    new StatModifier("WIS_TEMP")            { Bonus = new Bonus() { Name = "OWLS", Type = BonusType.Enhancement, Value = 4 } }
                }
            },
            {
                "EAGLES", new List<StatModifier>()
                {
                    new StatModifier("CHA_TEMP")            { Bonus = new Bonus() { Name = "EAGLES", Type = BonusType.Enhancement, Value = 4 } }
                }
            },
            {
                "HASTE", new List<StatModifier>()
                {
                    new StatModifier("ATK_BONUS")           { Bonus = new Bonus() { Name = "HASTE", Type = BonusType.Typeless, Value = 1 } },
                    new StatModifier("AC_BONUS")            { Bonus = new Bonus() { Name = "HASTE", Type = BonusType.Dodge, Value = 1 } },
                    new StatModifier("SAVE_REFLEX")         { Bonus = new Bonus() { Name = "HASTE", Type = BonusType.Dodge, Value = 1 } },
                    new StatModifier("MOVE")                { Bonus = new Bonus() { Name = "HASTE", Type = BonusType.Enhancement, Value = 30 } },
                }
            },
            {
                "ENLARGE_P", new List<StatModifier>()
                {
                    new StatModifier("STR_TEMP")            { Bonus = new Bonus() { Name = "ENLARGE_P", Type = BonusType.Size, Value = 2 } },
                    new StatModifier("DEX_TEMP")            { Bonus = new Bonus() { Name = "ENLARGE_P", Type = BonusType.Typeless, Value = -2 } },
                    new StatModifier("SIZE_ATK")            { Bonus = new Bonus() { Name = "ENLARGE_P", Type = BonusType.Typeless, Value = -1 } },
                    new StatModifier("SIZE_CM")         { Bonus = new Bonus() { Name = "ENLARGE_P", Type = BonusType.Typeless, Value = 1 } },
                    new StatModifier("SIZE_FLY")        { Bonus = new Bonus() { Name = "ENLARGE_P", Type = BonusType.Typeless, Value = -2 } },
                    new StatModifier("SIZE_STEALTH")    { Bonus = new Bonus() { Name = "ENLARGE_P", Type = BonusType.Typeless, Value = -4 } }
                }
            },
            {   
                "REDUCE_P", new List<StatModifier>()
                {
                    new StatModifier("STR_TEMP")            { Bonus = new Bonus() { Name = "REDUCE_P", Type = BonusType.Size, Value = -2 } },
                    new StatModifier("DEX_TEMP")            { Bonus = new Bonus() { Name = "REDUCE_P", Type = BonusType.Typeless, Value = 2 } },
                    new StatModifier("SIZE_ATK")            { Bonus = new Bonus() { Name = "REDUCE_P", Type = BonusType.Typeless, Value = 1 } },
                    new StatModifier("SIZE_CM")         { Bonus = new Bonus() { Name = "REDUCE_P", Type = BonusType.Typeless, Value = -1 } },
                    new StatModifier("SIZE_FLY")        { Bonus = new Bonus() { Name = "REDUCE_P", Type = BonusType.Typeless, Value = 2 } },
                    new StatModifier("SIZE_STEALTH")    { Bonus = new Bonus() { Name = "REDUCE_P", Type = BonusType.Typeless, Value = 4 } }
                }
            },
            {
                "FLAGBEARER", new List<StatModifier>()
                {
                    new StatModifier("ATK_BONUS")           { Bonus = new Bonus() { Name = "FLAGBEARER", Type = BonusType.Morale, Value = 1 } },
                    new StatModifier("DMG_BONUS")             { Bonus = new Bonus() { Name = "FLAGBEARER", Type = BonusType.Morale, Value = 1 } }
                }
            },
            {
                "INSPIRE_COURAGE_1", new List<StatModifier>()
                {
                    new StatModifier("ATK_BONUS")           { Bonus = new Bonus() { Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 1 } },
                    new StatModifier("DMG_BONUS")             { Bonus = new Bonus() { Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 1 } }
                }
            },
            {
                "INSPIRE_COURAGE_2", new List<StatModifier>()
                {
                    new StatModifier("ATK_BONUS")           { Bonus = new Bonus() { Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 2 } },
                    new StatModifier("DMG_BONUS")             { Bonus = new Bonus() { Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 2 } } 
                }
            }

        };
    }
}
