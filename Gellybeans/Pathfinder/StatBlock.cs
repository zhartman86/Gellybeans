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
        public Dictionary<string, Buff>     Buffs       { get; set; } = new Dictionary<string, Buff>();

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

        public void AddBuff(Buff buff)
        {
            if(Buffs.ContainsKey(buff.Name)) return;

            Buffs[buff.Name] = buff;

            foreach(var mod in buff.Mods)
            {
                Stats[mod.StatName].AddBonus(mod.Bonus);
            }
        }

        public void RemoveBuff(Buff buff)
        {
            if(!Buffs.ContainsKey(buff.Name)) return;

            foreach(var mod in buff.Mods)
            {
                Stats[mod.StatName].RemoveBonus(mod.Bonus);
            }

            Buffs.Remove(buff.Name);
        }

        public void ClearBuffs()
        {
            foreach(var buff in Buffs.Values)
            {
                RemoveBuff(buff);              
            }
            Buffs.Clear();
        }

        public int Call(string methodName, int[] args) => methodName switch
        {
            "mod"           => (args[0] - 10) / 2,
            "modulo"        => args[0] % args[1],
            "min"           => Math.Min(args[0], args[1]),
            "max"           => Math.Max(args[0], args[1]),
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
                    ["HP_TEMP"] = 0,
                    ["DAMAGE"] = 0,
                    ["NONLETHAL"] = 0,

                    ["STR"] = 12,
                    ["DEX"] = 15,
                    ["CON"] = 16,
                    ["INT"] = 18,
                    ["WIS"] = 8,
                    ["CHA"] = 6,

                    //since damage and temporary bonuses apply symmetrical effects, the same field can be used for both. neat. :)
                    ["STR_TEMP"] = 0,
                    ["DEX_TEMP"] = 0,
                    ["CON_TEMP"] = 0,
                    ["INT_TEMP"] = 0,
                    ["WIS_TEMP"] = 0,
                    ["CHA_TEMP"] = 0,

                    ["MOVE"] = 0,

                    ["INIT"] = 0,

                    ["AC"] = 10,
                    ["AC_TOUCH"] = 10,
                    ["AC_FLAT"] = 10,

                    ["AC_MAXDEX"] = 99,
                    ["CHECK_PENALTY"] = 0,

                    ["FORT_BASE"] = 0,
                    ["REFLEX_BASE"] = 0,
                    ["WILL_BASE"] = 0,

                    ["BAB"] = 0,


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
                    ["ATK_M"]           = "1d20 + BAB + STR_MOD + SIZE_MOD + mod(STR_TEMP)",
                    ["ATK_R"]           = "1d20 + BAB + DEX_MOD + SIZE_MOD + mod(DEX_TEMP)",
                    
                    ["STR_MOD"]         = "mod(STR)",
                    ["DEX_MOD"]         = "mod(DEX)",
                    ["CON_MOD"]         = "mod(CON)",
                    ["INT_MOD"]         = "mod(INT)",
                    ["WIS_MOD"]         = "mod(WIS)",
                    ["CHA_MOD"]         = "mod(CHA)",

                    ["FORT"]            = "FORT_BASE    + CON_MOD",
                    ["REF"]             = "REFLEX_BASE  + DEX_MOD",
                    ["WILL"]            = "WILL_BASE    + WIS_MOD",

                    ["CMB"]             = "     BAB + STR_MOD +           SIZE_MOD",
                    ["CMD"]             = "10 + BAB + STR_MOD + DEX_MOD + SIZE_MOD",

                    ["ACROBATICS"]      = "DEX_MOD + SK_ACR + CHECK_PENALTY",
                    ["APPRAISE"]        = "INT_MOD + SK_APR",
                    ["BLUFF"]           = "CHA_MOD + SK_BLF",
                    ["CLIMB"]           = "STR_MOD + SK_CLM + CHECK_PENALTY",
                    ["DIPLOMACY"]       = "CHA_MOD + SK_DPL",
                    ["DISABLEDEVICE"]   = "DEX_MOD + SK_DEV",
                    ["DISGUISE"]        = "CHA_MOD + SK_DSG",
                    ["ESCAPE"]          = "DEX_MOD + SK_ESC + CHECK_PENALTY",
                    ["FLY"]             = "DEX_MOD + SK_FLY + CHECK_PENALTY" + "SIZE_MOD_FLY",
                    ["HANDLEANIMAL"]    = "DEX_MOD + SK_HAN",
                    ["HEAL"]            = "WIS_MOD + SK_HEA",
                    ["INTIMIDATE"]      = "CHA_MOD + SK_INT",
                    ["ARCANA"]          = "INT_MOD + SK_ARC",
                    ["DUNGEONEERING"]   = "INT_MOD + SK_DUN",
                    ["GEOGRAPHY"]       = "INT_MOD + SK_GEO",
                    ["HISTORY"]         = "INT_MOD + SK_HIS",
                    ["LOCAL"]           = "INT_MOD + SK_LOC",
                    ["NATURE"]          = "INT_MOD + SK_NAT",
                    ["NOBILITY"]        = "INT_MOD + SK_NOB",
                    ["PLANES"]          = "INT_MOD + SK_PLA",
                    ["RELIGION"]        = "INT_MOD + SK_REL",
                    ["LINGUISTICS"]     = "INT_MOD + SK_LNG",
                    ["PERCEPTION"]      = "WIS_MOD + SK_PER",
                    ["RIDE"]            = "DEX_MOD + SK_RDE + CHECK_PENALTY",
                    ["SENSEMOTIVE"]     = "WIS_MOD + SK_SMO",
                    ["SLEIGHTOFHAND"]   = "DEX_MOD + SK_SLE + CHECK_PENALTY",
                    ["SPELLCRAFT"]      = "INT_MOD + SK_SPL",
                    ["STEALTH"]         = "DEX_MOD + SK_STL + CHECK_PENALTY" + "SIZE_MOD_STEALTH",
                    ["SURVIVAL"]        = "WIS_MOD + SK_SUR",
                    ["SWIM"]            = "STR_MOD + SK_SWM + CHECK_PENALTY",
                    ["USEMAGICDEVICE"]  = "CHA_MOD + SK_UMD",
                }
                
            };

            return statBlock;
        }
        
    }
   
}

