using Gellybeans.Expressions;

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

        
        
        public string this[string varName]
        {
            get 
            {
                if(Stats.ContainsKey(varName))              return Stats[varName].ToString();
                else if(Expressions.ContainsKey(varName))   return Parser.Parse(Expressions[varName]).Eval(this).ToString();
                return "0";
            }
        }

        public bool AddStat(string name, int baseValue)
        {
            if(Stats.ContainsKey(name) || Expressions.ContainsKey(name)) return false;

            Stats[name] = baseValue;
            return true;
        }
        
        public bool AddExpression(string name, string expr)
        {
            if(Stats.ContainsKey(name) || Expressions.ContainsKey(name)) return false;
            
            Expressions[name] = expr;
            return true;
        }



        public int Call(string methodName, int[] args) => methodName switch
        {
            "mod"   => (args[0] - 10) / 2,
            _       => 0

        };

        public int Resolve(string statName)
        {
            return Stats[statName];
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

                    ["STR"] = 10,
                    ["DEX"] = 10,
                    ["CON"] = 10,
                    ["INT"] = 10,
                    ["WIS"] = 10,
                    ["CHA"] = 10,

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
                }
            
            };

            return statBlock;
        }
        
    }
   
}

