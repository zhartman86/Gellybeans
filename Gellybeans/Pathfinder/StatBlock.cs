using Gellybeans.ECS;

namespace Gellybeans.Pathfinder
{
    public class StatBlock : Component
    {

        public List<Modifier> Modifiers { get; set; } = new List<Modifier>();
        public List<Item> Inventory { get; set; } = new List<Item>();
        public Dictionary<string, Item> Equip { get; private set; } = new Dictionary<string, Item>();
        public Dictionary<string, Stat> Stats { get; private set; } = new Dictionary<string, Stat>();
        public Dictionary<string, string> Info { get; private set; } = new Dictionary<string, string>();


        public int this[string statName]
        {
            get { return Stats.ContainsKey(statName) ? Stats[statName].Value : 0; }
            set { Stats[statName] = value; }
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
                    ["MOVE_BURROW"] = 0,
                    ["MOVE_FLY"] = 0,
                    ["MOVE_SWIM"] = 0,
                    ["MOVE_CLIMB"] = 0,

                    ["INIT"] = 0,

                    ["AC"] = 10,
                    ["AC_TOUCH"] = 10,
                    ["AC_FLAT"] = 10,

                    ["ARMOR_PENALTY"] = 0,

                    ["DR_TYPE"] = 0,  //bitmask                  
                    ["DR_VALUE"] = 0,

                    ["SR"] = 0,

                    ["SAVE_FORT"] = 0,
                    ["SAVE_REFLEX"] = 0,
                    ["SAVE_WILL"] = 0,

                    ["BAB"] = 0,

                    ["CMD"] = 10,
                    ["CMB"] = 0,

                    ["SPELL_FAIL"] = 0,
                    ["CONCENTRATION"] = 0,

                    ["SKILL_ACROBATICS"] = 0,
                    ["SKILL_APPRAISE"] = 0,
                    ["SKILL_BLUFF"] = 0,
                    ["SKILL_CLIMB"] = 0,
                    ["SKILL_DIPLOMACY"] = 0,
                    ["SKILL_DISABLEDEVICE"] = 0,
                    ["SKILL_DISGUISE"] = 0,
                    ["SKILL_ESCAPE"] = 0,
                    ["SKILL_FLY"] = 0,
                    ["SKILL_HANDLEANIMAL"] = 0,
                    ["SKILL_HEAL"] = 0,
                    ["SKILL_INTIMIDATE"] = 0,
                    ["SKILL_ARCANA"] = 0,
                    ["SKILL_DUNGEONEERING"] = 0,
                    ["SKILL_GEOGRAPHY"] = 0,
                    ["SKILL_HISTORY"] = 0,
                    ["SKILL_LOCAL"] = 0,
                    ["SKILL_NATURE"] = 0,
                    ["SKILL_NOBILITY"] = 0,
                    ["SKILL_PLANES"] = 0,
                    ["SKILL_RELIGION"] = 0,
                    ["SKILL_LINGUISTICS"] = 0,
                    ["SKILL_PERCEPTION"] = 0,
                    ["SKILL_RIDE"] = 0,
                    ["SKILL_SENSEMOTIVE"] = 0,
                    ["SKILL_SLEIGHTOFHAND"] = 0,
                    ["SKILL_SPELLCRAFT"] = 0,
                    ["SKILL_STEALTH"] = 0,
                    ["SKILL_SURVIVAL"] = 0,
                    ["SKILL_SWIM"] = 0,
                    ["SKILL_USEMAGICDEVICE"] = 0,
                }
            };
            return statBlock;
        }

    }
}
