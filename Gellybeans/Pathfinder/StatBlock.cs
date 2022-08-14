using Gellybeans.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace Gellybeans.Pathfinder
{
    public class StatBlock : IContext
    {

        public Guid Id { get; set; }
        public ulong Owner { get; set; }


        public string CharacterName { get; set; } = "Name me"; 

        public Dictionary<string, Stat>     Stats       { get; private set; } = new Dictionary<string, Stat>();
        public Dictionary<string, string>   Expressions { get; private set; } = new Dictionary<string, string>();
        public Dictionary<string, string>   Info        { get; private set; } = new Dictionary<string, string>();
        
        public List<Item>                   Inventory   { get; set; } = new List<Item>();

        public List<Attack>                 Attacks     { get; set; } = new List<Attack>();
    
     
        public int this[string statName]
        {
            get
            {
                if(Stats.ContainsKey(statName)) return Stats[statName].Value;
                return 0;
            }
            set
            {
                if(Stats.ContainsKey(statName)) Stats[statName].Base = value;
            }
        }

        public void AddBonuses(List<StatModifier> bonuses)
        {
            for(int i = 0; i < bonuses.Count; i++)
            {
                Stats[bonuses[i].StatName].AddBonus(bonuses[i].Bonus);
            }
        }

        public void ClearBonus(string bonusName)
        {
            foreach(var stat in Stats.Values)
            {
                for(int i = 0; i < stat.Bonuses.Count; i++)
                {
                    if(stat.Bonuses[i].Name == bonusName)
                    {
                        stat.RemoveBonus(stat.Bonuses[i]);
                    }
                }
            }
        }

        public int Call(string methodName, int[] args) => methodName switch
        {
            "mod"           => (args[0] - 10) / 2,
            "modulo"        => args[0] % args[1],
            "min"           => Math.Min(args[0], args[1]),
            "max"           => Math.Max(args[0], args[1]),
            "clamp"         => Math.Clamp(args[0], args[1], args[2]),
            "abs"           => Math.Abs(args[0]),
            _               => 0
        };

        public int Resolve(string varName, StringBuilder sb)
        {
            if(Stats.ContainsKey(varName))
                return this[varName];
            else if(Expressions.ContainsKey(varName))
                return Parser.Parse(Expressions[varName]).Eval(this, sb);
            return 0;
        }




        public static StatBlock DefaultPathfinder(string name)
        {

            var statBlock = new StatBlock()
            {
                CharacterName = name,

                Info = new Dictionary<string, string>()
                {
                    ["NAME"] = "NAME ME",
                    ["LEVELS"] = "",
                    ["DEITY"] = "",
                    ["HOME"] = "",
                    ["GENDER"] = "",
                    ["HAIR"] = "",
                    ["EYES"] = "",
                    ["BIO"] = ""
                },

                Stats = new Dictionary<string, Stat>()
                {
                    ["SIZE_MOD"] = 0,
                    ["SIZE_MOD_CM"] = 0,
                    ["SIZE_MOD_FLY"] = 0,
                    ["SIZE_MOD_STEALTH"] = 0,

                    ["HP_BASE"] = 0,

                    ["STR_SCORE"] = 12,
                    ["DEX_SCORE"] = 15,
                    ["CON_SCORE"] = 16,
                    ["INT_SCORE"] = 18,
                    ["WIS_SCORE"] = 8,
                    ["CHA_SCORE"] = 6,

                    //since damage and temporary bonuses apply symmetrical effects, the same field can be used for both. neat. :)
                    ["STR_TEMP"] = 0,
                    ["DEX_TEMP"] = 0,
                    ["CON_TEMP"] = 0,
                    ["INT_TEMP"] = 0,
                    ["WIS_TEMP"] = 0,
                    ["CHA_TEMP"] = 0,

                    ["MOVE"] = 0,

                    ["INIT"] = 0,

                    ["AC_BASE"] = 10,
                    ["AC_MAXDEX"] = 99,
                    ["AC_PENALTY"] = 0,

                    ["FORT_BASE"] = 0,
                    ["REFLEX_BASE"] = 0,
                    ["WILL_BASE"] = 0,

                    ["BAB"] = 0,

                    ["ATK_BONUS"] = 0,
                    ["DMG_BONUS"] = 0,

                    //skills
                    ["SK_ACR"] = 0,
                    ["SK_APR"] = 0,
                    ["SK_BLF"] = 0,
                    ["SK_CLM"] = 0,
                    ["SK_DPL"] = 0,
                    ["SK_DEV"] = 0,
                    ["SK_DSG"] = 0,
                    ["SK_ESC"] = 0,
                    ["SK_FLY"] = 0,
                    ["SK_HAN"] = 0,
                    ["SK_HEA"] = 0,
                    ["SK_INT"] = 0,
                    ["SK_ARC"] = 0,
                    ["SK_DUN"] = 0,
                    ["SK_GEO"] = 0,
                    ["SK_HIS"] = 0,
                    ["SK_LOC"] = 0,
                    ["SK_NAT"] = 0,
                    ["SK_NOB"] = 0,
                    ["SK_PLA"] = 0,
                    ["SK_REL"] = 0,
                    ["SK_LNG"] = 0,
                    ["SK_PER"] = 0,
                    ["SK_RDE"] = 0,
                    ["SK_SMO"] = 0,
                    ["SK_SLE"] = 0,
                    ["SK_SPL"] = 0,
                    ["SK_STL"] = 0,
                    ["SK_SUR"] = 0,
                    ["SK_SWM"] = 0,
                    ["SK_UMD"] = 0,                 
                    
                },

                Expressions = new Dictionary<string, string>()
                {
                    ["ATK_M"]           = "1d20 + BAB + STR + SIZE_MOD + mod(STR_TEMP) + ATK_BONUS",
                    ["ATK_R"]           = "1d20 + BAB + DEX + SIZE_MOD + mod(DEX_TEMP) + ATK_BONUS",

                    ["LEVEL"]           = "0",
                    ["HP"]              = "HP_BASE + (CON * LEVEL)",

                    ["STR"]             = "mod(STR_SCORE)",
                    ["DEX"]             = "mod(DEX_SCORE)",
                    ["CON"]             = "mod(CON_SCORE)",
                    ["INT"]             = "mod(INT_SCORE)",
                    ["WIS"]             = "mod(WIS_SCORE)",
                    ["CHA"]             = "mod(CHA_SCORE)",

                    ["FORT"]            = "FORT_BASE + CON",
                    ["REF"]             = "REFLEX_BASE + DEX",
                    ["WILL"]            = "WILL_BASE + WIS",

                    ["AC"]              = "AC_BASE + min(DEX, AC_MAXDEX)",
                    
                    ["CMB"]             = "BAB + STR + SIZE_MOD",
                    ["CMD"]             = "10 + BAB + STR + DEX + SIZE_MOD",

                    ["ACROBATICS"]      = "DEX + SK_ACR + AC_PENALTY",
                    ["APPRAISE"]        = "INT + SK_APR",
                    ["BLUFF"]           = "CHA + SK_BLF",
                    ["CLIMB"]           = "STR + SK_CLM + AC_PENALTY",
                    ["DIPLOMACY"]       = "CHA + SK_DPL",
                    ["DISABLEDEVICE"]   = "DEX + SK_DEV",
                    ["DISGUISE"]        = "CHA + SK_DSG",
                    ["ESCAPE"]          = "DEX + SK_ESC + AC_PENALTY",
                    ["FLY"]             = "DEX + SK_FLY + AC_PENALTY" + "SIZE_MOD_FLY",
                    ["HANDLEANIMAL"]    = "DEX + SK_HAN",
                    ["HEAL"]            = "WIS + SK_HEA",
                    ["INTIMIDATE"]      = "CHA + SK_INT",
                    ["ARCANA"]          = "INT + SK_ARC",
                    ["DUNGEONEERING"]   = "INT + SK_DUN",
                    ["GEOGRAPHY"]       = "INT + SK_GEO",
                    ["HISTORY"]         = "INT + SK_HIS",
                    ["LOCAL"]           = "INT + SK_LOC",
                    ["NATURE"]          = "INT + SK_NAT",
                    ["NOBILITY"]        = "INT + SK_NOB",
                    ["PLANES"]          = "INT + SK_PLA",
                    ["RELIGION"]        = "INT + SK_REL",
                    ["LINGUISTICS"]     = "INT + SK_LNG",
                    ["PERCEPTION"]      = "WIS + SK_PER",
                    ["RIDE"]            = "DEX + SK_RDE + AC_PENALTY",
                    ["SENSEMOTIVE"]     = "WIS + SK_SMO",
                    ["SLEIGHTOFHAND"]   = "DEX + SK_SLE + AC_PENALTY",
                    ["SPELLCRAFT"]      = "INT + SK_SPL",
                    ["STEALTH"]         = "DEX + SK_STL + AC_PENALTY" + "SIZE_MOD_STEALTH",
                    ["SURVIVAL"]        = "WIS + SK_SUR",
                    ["SWIM"]            = "STR + SK_SWM + AC_PENALTY",
                    ["UMD"]             = "CHA + SK_UMD",
                }
                
            };

            return statBlock;
        }
        
    }
   
}

