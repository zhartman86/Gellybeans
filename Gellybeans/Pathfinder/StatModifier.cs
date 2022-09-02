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
            //spells
            { "BULLS_STRENGTH", new List<StatModifier>()    { new StatModifier("STR_TEMP") { Bonus = new Bonus() { Name = "BULLS_STRENGTH",     Type = BonusType.Enhancement, Value = 4 } } } },
            { "CATS_GRACE", new List<StatModifier>()        { new StatModifier("DEX_TEMP") { Bonus = new Bonus() { Name = "CATS_GRACE",         Type = BonusType.Enhancement, Value = 4 } } } },
            { "BEARS_ENDURANCE", new List<StatModifier>()   { new StatModifier("CON_TEMP") { Bonus = new Bonus() { Name = "BEARS_ENDURANCE",    Type = BonusType.Enhancement, Value = 4 } } } },
            { "FOXS_CUNNING", new List<StatModifier>()      { new StatModifier("INT_TEMP") { Bonus = new Bonus() { Name = "FOXS_CUNNING",       Type = BonusType.Enhancement, Value = 4 } } } },
            { "OWLS_WISDOM", new List<StatModifier>()       { new StatModifier("WIS_TEMP") { Bonus = new Bonus() { Name = "OWLS_WISDOM",        Type = BonusType.Enhancement, Value = 4 } } } },
            { "EAGLES_SPLENDOR", new List<StatModifier>()   { new StatModifier("CHA_TEMP") { Bonus = new Bonus() { Name = "EAGLES_SPLENDOR",    Type = BonusType.Enhancement, Value = 4 } } } },
            {
                "HASTE", new List<StatModifier>()
                {
                    new StatModifier("ATK_BONUS")           { Bonus = new Bonus() { Name = "HASTE", Type = BonusType.Typeless, Value = 1 } },
                    new StatModifier("AC_BONUS")            { Bonus = new Bonus() { Name = "HASTE", Type = BonusType.Dodge, Value = 1 } },
                    new StatModifier("REF_BONUS")           { Bonus = new Bonus() { Name = "HASTE", Type = BonusType.Dodge, Value = 1 } },
                    new StatModifier("MOVE")                { Bonus = new Bonus() { Name = "HASTE", Type = BonusType.Enhancement, Value = 30 } },
                }
            },
            {
                "ENLARGE_PERSON", new List<StatModifier>()
                {
                    new StatModifier("STR_TEMP")            { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Size, Value = 2 } },
                    new StatModifier("DEX_TEMP")            { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Typeless, Value = -2 } },
                    new StatModifier("SIZE_MOD")            { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Typeless, Value = -1 } },
                    new StatModifier("SIZE_FLY")            { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Typeless, Value = -2 } },
                    new StatModifier("SIZE_STL")            { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Typeless, Value = -4 } }
                }
            },
            {
                "REDUCE_PERSON", new List<StatModifier>()
                {
                    new StatModifier("STR_TEMP")            { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Size, Value = -2 } },
                    new StatModifier("DEX_TEMP")            { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 2 } },
                    new StatModifier("SIZE_MOD")            { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 1 } },
                    new StatModifier("SIZE_FLY")            { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 2 } },
                    new StatModifier("SIZE_STL")            { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 4 } }
                }
            },
            {
                "FLAGBEARER", new List<StatModifier>()
                {
                    new StatModifier("ATK_BONUS")           { Bonus = new Bonus() { Name = "FLAGBEARER", Type = BonusType.Morale, Value = 1 } },
                    new StatModifier("DMG_BONUS")           { Bonus = new Bonus() { Name = "FLAGBEARER", Type = BonusType.Morale, Value = 1 } }
                }
            },
            {
                "INSPIRE_COURAGE_1", new List<StatModifier>()
                {
                    new StatModifier("ATK_BONUS")           { Bonus = new Bonus() { Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 1 } },
                    new StatModifier("DMG_BONUS")           { Bonus = new Bonus() { Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 1 } }
                }
            },
            {
                "INSPIRE_COURAGE_2", new List<StatModifier>()
                {
                    new StatModifier("ATK_BONUS")           { Bonus = new Bonus() { Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 2 } },
                    new StatModifier("DMG_BONUS")           { Bonus = new Bonus() { Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 2 } }
                }
            },


            //conditions
            {
                "BLINDED", new List<StatModifier>()
                {
                    new StatModifier("AC_MAXDEX")       { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -99  } },
                    new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -2 } },                    
                    new StatModifier("SK_ACR")          { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -4 } },
                    new StatModifier("SK_CLM")          { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -4 } },
                    new StatModifier("SK_DSA")          { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -4 } },
                    new StatModifier("SK_ESC")          { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -4 } },
                    new StatModifier("SK_ACR")          { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -4 } },
                    new StatModifier("SK_FLY")          { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -4 } },
                    new StatModifier("SK_RDE")          { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -4 } },
                    new StatModifier("SK_SLT")          { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -4 } },
                    new StatModifier("SK_SWM")          { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -4 } },
                }
            },
            {
                "COWERING", new List<StatModifier>()
                {
                    new StatModifier("AC_MAXDEX")       { Bonus = new Bonus() { Name = "COWERING", Type = BonusType.Penalty, Value = -99 } },
                    new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "COWERING", Type = BonusType.Penalty, Value = -2 } },
                }
            },
            {
                "DAZZLED", new List<StatModifier>()
                {
                    new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "DAZZLED", Type = BonusType.Penalty, Value = -1  } },                    
                }
            },
            {
                "DEAFENED", new List<StatModifier>()
                {
                    new StatModifier("INIT_BONUS")      { Bonus = new Bonus() { Name = "DEAFENED", Type = BonusType.Penalty, Value = -4  } },
                    new StatModifier("SK_PRC")          { Bonus = new Bonus() { Name = "DEAFENED", Type = BonusType.Penalty, Value = -4  } },
                }
            },
            {
                "ENTANGLED", new List<StatModifier>()
                {
                    new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "ENTANGLED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ENTANGLED", Type = BonusType.Penalty, Value = -4  } },
                }
            },
            {
                "EXHAUSTED", new List<StatModifier>()
                {
                    new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "EXHAUSTED", Type = BonusType.Penalty, Value = -6  } },
                    new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "EXHAUSTED", Type = BonusType.Penalty, Value = -6  } },
                }
            },
            {
                "FASCINATED", new List<StatModifier>()
                {
                    new StatModifier("SK_PRC")          { Bonus = new Bonus() { Name = "FASCINATED", Type = BonusType.Penalty, Value = -4  } },
                }
            },
            {
                "FATIGUED", new List<StatModifier>()
                {
                    new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "FATIGUED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "FATIGUED", Type = BonusType.Penalty, Value = -2  } },
                }
            },
            {
                "FRIGHTENED", new List<StatModifier>()
                {
                    new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("FORT_BONUS")      { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("REF_BONUS")       { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("WILL_BONUS")      { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },                    
                    new StatModifier("SK_ACR")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_APR")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_BLF")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_CLM")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_DIP")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_DSA")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_DSG")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_ESC")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_FLY")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_HND")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_HEA")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_ITM")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_LNG")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_PRC")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_RDE")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_SNS")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_SLT")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_SPL")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_STL")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_SUR")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_SWM")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_UMD")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_ARC")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_DUN")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_ENG")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_GEO")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_HIS")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_LCL")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_NTR")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_NBL")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_PLN")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_RLG")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  } },
                }
            },
            {
                "GRAPPLED", new List<StatModifier>()
                {
                    new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "GRAPPLED", Type = BonusType.Penalty, Value = -4  } },
                    new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "GRAPPLED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("CMB_BONUS")       { Bonus = new Bonus() { Name = "GRAPPLED", Type = BonusType.Penalty, Value = -2  } },
                }
            },
            {
                "HELPLESS", new List<StatModifier>()
                {
                    new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "HELPLESS", Type = BonusType.Penalty, Value = -100  } },
                }
            },
            {
                "PANICKED", new List<StatModifier>()
                {
                    new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("FORT_BONUS")      { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("REF_BONUS")       { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("WILL_BONUS")      { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_ACR")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_APR")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_BLF")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_CLM")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_DIP")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_DSA")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_DSG")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_ESC")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_FLY")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_HND")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_HEA")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_ITM")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_LNG")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_PRC")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_RDE")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_SNS")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_SLT")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_SPL")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_STL")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_SUR")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_SWM")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_UMD")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_ARC")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_DUN")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_ENG")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_GEO")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_HIS")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_LCL")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_NTR")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_NBL")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_PLN")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_RLG")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  } },
                }
            },
            {
                "PARALYZED", new List<StatModifier>()
                {
                    new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "PARALYZED", Type = BonusType.Penalty, Value = -100  } },
                    new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "PARALYZED", Type = BonusType.Penalty, Value = -100  } },
                }
            },
            {
                "PINNED", new List<StatModifier>()
                {
                    new StatModifier("AC_MAXDEX")       { Bonus = new Bonus() { Name = "PINNED", Type = BonusType.Penalty, Value = -99  } },
                    new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "PINNED", Type = BonusType.Penalty, Value = -4  } },
                }
            },
            {
                "SHAKEN", new List<StatModifier>()
                {
                    new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("FORT_BONUS")      { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("REF_BONUS")       { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("WILL_BONUS")      { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_ACR")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_APR")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_BLF")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_CLM")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_DIP")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_DSA")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_DSG")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_ESC")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_FLY")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_HND")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_HEA")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_ITM")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_LNG")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_PRC")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_RDE")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_SNS")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_SLT")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_SPL")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_STL")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_SUR")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_SWM")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_UMD")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_ARC")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_DUN")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_ENG")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_GEO")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_HIS")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_LCL")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_NTR")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_NBL")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_PLN")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_RLG")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  } },
                }
            },
            {
                "SICKENED", new List<StatModifier>()
                {
                    new StatModifier("DMG_BONUS")       { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("FORT_BONUS")      { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("REF_BONUS")       { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("WILL_BONUS")      { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_ACR")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_APR")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_BLF")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_CLM")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_DIP")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_DSA")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_DSG")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_ESC")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_FLY")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_HND")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_HEA")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_ITM")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_LNG")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_PRC")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_RDE")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_SNS")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_SLT")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_SPL")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_STL")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_SUR")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_SWM")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_UMD")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_ARC")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_DUN")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_ENG")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_GEO")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_HIS")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_LCL")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_NTR")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_NBL")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_PLN")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("SK_RLG")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  } },
                }
            },
            {
                "STUNNED", new List<StatModifier>()
                {
                    new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "STUNNED", Type = BonusType.Penalty, Value = -2  } },
                    new StatModifier("AC_MAXDEX")       { Bonus = new Bonus() { Name = "STUNNED", Type = BonusType.Penalty, Value = -99  } },
                }
            },
        };
    }
}
