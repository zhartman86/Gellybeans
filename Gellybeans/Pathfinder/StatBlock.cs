using Gellybeans.Expressions;
using System.Text.RegularExpressions;

namespace Gellybeans.Pathfinder
{
    public class StatBlock : IContext
    {

        
        public Dictionary<string, Stat>     Stats       { get; private set; } = new Dictionary<string, Stat>();
        public Dictionary<string, string>   Expressions { get; private set; } = new Dictionary<string, string>();

        public Dictionary<string, string>   Info        { get; private set; } = new Dictionary<string, string>();
        
        public List<Item>                   Inventory   { get; set; } = new List<Item>();
        public List<StatModifier>           StatMods    { get; set; } = new List<StatModifier>();

        public List<Attack>                 Attacks     { get; set; } = new List<Attack>();

        private static Regex ValidVar = new Regex("^[A-Z_]{1,17}$");
        
        
        public int this[string statName]
        {
            get 
            {
                if(Stats.ContainsKey(statName)) return Stats[statName];
                return 0;
            }
        }

        public int Call(string methodName, int[] args) => methodName switch
        {
            "modifier"      => (args[0] - 10) / 2,
            "mod"           => args[0] % args[1],
            "min"           => Math.Min(args[0], args[1]),
            "max"           => Math.Max(args[0], args[1]),
            "abs"           => Math.Abs(args[0]),
            _               => 0

        };

        public int Resolve(string varName)
        {
            if(Stats.ContainsKey(varName))
                return this[varName];
            else if(Expressions.ContainsKey(varName))
                return Parser.Parse(Expressions[varName]).Eval(this);
            return 0;
        }

        public static StatBlock DefaultPathfinder()
        {
            var statBlock = new StatBlock()
            {

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
                    ["SIZE"] = 0,
                    ["ALIGN"] = 0,
                    ["AGE"] = 0,
                    ["HEIGHT"] = 100,
                    ["WEIGHT"] = 100,

                    ["HP_BASE"] = 0,
                    ["HP_TEMP"] = 0,
                    ["HP_DAMAGE"] = 0,
                    ["HP_NONLETHAL"] = 0,

                    ["STR"] = 12,
                    ["DEX"] = 14,
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

                    ["DR_TYPE"] = 0,  //bitmask                  
                    ["DR_VALUE"] = 0,

                    ["SR"] = 0,

                    ["SAVE_FORT"] = 0,
                    ["SAVE_REFLEX"] = 0,
                    ["SAVE_WILL"] = 0,

                    ["BAB"] = 0,

                    ["CMD"] = 10,
                    ["CMB"] = 0,

                    ["ARCANE_FAIL"] = 0,

                    //skills
                    ["ACROBATICS"] = 0,
                    ["APPRAISE"] = 0,
                    ["BLUFF"] = 0,
                    ["CLIMB"] = 0,
                    ["DIPLOMACY"] = 0,
                    ["DISABLEDEVICE"] = 0,
                    ["DISGUISE"] = 0,
                    ["ESCAPE"] = 0,
                    ["FLY"] = 0,
                    ["HANDLEANIMAL"] = 0,
                    ["HEAL"] = 0,
                    ["INTIMIDATE"] = 0,
                    ["ARCANA"] = 0,
                    ["DUNGEONEERING"] = 0,
                    ["GEOGRAPHY"] = 0,
                    ["HISTORY"] = 0,
                    ["LOCAL"] = 0,
                    ["NATURE"] = 0,
                    ["NOBILITY"] = 0,
                    ["PLANES"] = 0,
                    ["RELIGION"] = 0,
                    ["LINGUISTICS"] = 0,
                    ["PERCEPTION"] = 0,
                    ["RIDE"] = 0,
                    ["SENSEMOTIVE"] = 0,
                    ["SLEIGHTOFHAND"] = 0,
                    ["SPELLCRAFT"] = 0,
                    ["STEALTH"] = 0,
                    ["SURVIVAL"] = 0,
                    ["SWIM"] = 0,
                    ["USEMAGICDEVICE"] = 0,
                },

                Expressions = new Dictionary<string, string>()
                {
                    ["STR_MOD"] = "(STR - 10) / 2",
                    ["DEX_MOD"] = "(DEX - 10) / 2",
                    ["CON_MOD"] = "(CON - 10) / 2",
                    ["INT_MOD"] = "(INT - 10) / 2",
                    ["WIS_MOD"] = "(WIS - 10) / 2",
                    ["CHA_MOD"] = "(CHA - 10) / 2",
                }
                
            };

            return statBlock;
        }
        
    }
   
}

