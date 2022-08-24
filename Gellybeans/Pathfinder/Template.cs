namespace Gellybeans.Pathfinder
{    
    public class Template
    {
        public string Name  { get; set; }
        public string Notes { get; set; } = "";
        
        public Dictionary<string, Stat>     Stats           { get; set; } = new Dictionary<string, Stat>();
        public Dictionary<string, string>   AddExpressions  { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string>   ModExpressions  { get; set; } = new Dictionary<string, string>(); 

        public List<string> Features;

        public static Dictionary<string, Template> Templates = new Dictionary<string, Template>()
        {
            
            //PATHFINDER
            {
                "PF_FIGHTER", new Template()
                {
                    Name = "FIGHTER",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["FIGHTER_LEVEL"] = 1,
                    },
                    AddExpressions = new Dictionary<string, string>()
                    {
                        ["BRAVERY"]         = "FIGHTER_LEVEL >= 2 ? 1 + (FIGHTER_LEVEL - 2) / 4 : 0",
                        ["ARMOR_TRAINING"]  = "FIGHTER_LEVEL >= 3 ? 1 + (FIGHTER_LEVEL - 3) / 4 : 0",
                        ["WEAPON_TRAINING"] = "FIGHTER_LEVEL >= 5 ? 1 + (FIGHTER_LEVEL - 5) / 4 : 0",
                        ["BONUS_FEATS"]     = "1 + (FIGHTER_LEVEL / 2)",

                        ["CS_CLM"] = "TRUE",
                        ["CS_HAN"] = "TRUE",
                        ["CS_INT"] = "TRUE",
                        ["CS_DUN"] = "TRUE",
                        ["CS_ENG"] = "TRUE",
                        ["CS_RDE"] = "TRUE",
                        ["CS_SUR"] = "TRUE",
                        ["CS_SWM"] = "TRUE",
                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "FIGHTER_LEVEL",
                        ["FORT_BASE"]   = "(2 + (FIGHTER_LEVEL / 2))",
                        ["REF_BASE"]    = "(FIGHTER_LEVEL / 3)",
                        ["WILL_BASE"]   = "(FIGHTER_LEVEL / 3)",
                    },
                    Notes = "`FIGHTER_LEVEL` set to 1. Be sure to change this if higher."
                }
            },
            {
                "PF_PSYCHIC", new Template()
                {
                    Name = "PSYCHIC",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["PSYCHIC_LEVEL"] = 1,
                    },
                    AddExpressions = new Dictionary<string, string>()
                    {
                        ["PHRENIC_MOD"]     = "",
                        ["PHRENIC_POOL"]    = "(PSYCHIC_LEVEL / 2) + PHRENIC_MOD",
                        ["PHRENIC_AMP"]     = "1 + PSYCHIC_LEVEL >= 3 ? 1 + (PSYCHIC_LEVEL - 3) / 4 : 0",

                        ["CS_BLF"] = "TRUE",
                        ["CS_DIP"] = "TRUE",
                        ["CS_FLY"] = "TRUE",
                        ["CS_ITM"] = "TRUE",
                        ["CS_ARC"] = "TRUE",
                        ["CS_DUN"] = "TRUE",
                        ["CS_ENG"] = "TRUE",
                        ["CS_GEO"] = "TRUE",
                        ["CS_HIS"] = "TRUE",
                        ["CS_LCL"] = "TRUE",
                        ["CS_NTR"] = "TRUE",
                        ["CS_NBL"] = "TRUE",
                        ["CS_PLN"] = "TRUE",
                        ["CS_RLG"] = "TRUE",
                        ["CS_LNG"] = "TRUE",
                        ["CS_PRC"] = "TRUE",
                        ["CS_SNS"] = "TRUE",
                        ["CS_SPL"] = "TRUE",
                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "(PSYCHIC LEVEL / 2)",
                        ["FORT_BASE"]   = "(PSYCHIC_LEVEL / 3)",
                        ["REF_BASE"]    = "(PSYCHIC_LEVEL / 3)",
                        ["WILL_BASE"]   = "(2 + (PSYCHIC_LEVEL / 2))",
                    },
                    Notes = "Be sure to set `PHRENIC_MOD` (usually CHA or WIS) to the appropriate mod based on your discipline." + "\n" + "PSYCHIC_LEVEL` set to 1. Be sure to change this if higher.",
                }
            },


            //5E
            {
                "FIFTH_BARBARIAN", new Template()
                {
                    Name = "BARBARIAN",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["BARBARIAN_LEVEL"] = 1,
                    },
                    AddExpressions = new Dictionary<string, string>()
                    {
                        ["PROF_STR"] = "TRUE",
                        ["PROF_CON"] = "TRUE",

                    },
                }
            }
        };                              
    }
}
