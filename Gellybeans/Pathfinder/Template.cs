namespace Gellybeans.Pathfinder
{    
    public class Template
    {
        public string Name  { get; set; }
        public string Notes { get; set; } = "";
        
        public Dictionary<string, Stat>     Stats           { get; set; } = new Dictionary<string, Stat>();
        public Dictionary<string, string>   SetExpressions  { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string>   ModExpressions  { get; set; } = new Dictionary<string, string>(); 

        public List<string> Features;

        public static Dictionary<string, Template> Templates = new Dictionary<string, Template>()
        {

            //PATHFINDER
            ///MAIN
            { 
                "PF_BARBARIAN,", new Template()
                {
                    Name = "BARBARIAN",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["BARBARIAN_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {
                        ["RAGE_RNDS"] = "(4 + CON) + ((BARBARIAN_LEVEL - 1) * 2)",
                        ["RAGE_PWRS"] = "(BARBARIAN / 2)",

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "BARBARIAN_LEVEL",
                        ["FORT_BASE"]   = "good(BARBARIAN_LEVEL)",
                        ["REF_BASE"]    = "bad(BARBARIAN_LEVE)",
                        ["WILL_BASE"]   = "bad(BARBARIAN_LEVEL)",
                    }                   
                }
            },           
            {
                "PF_BARD", new Template()
                {
                    Name = "BARD",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["BARD_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {
                        ["BARDIC_PERF"] = "(4 + CHA) + ((BARD_LEVEL - 1) * 2)"
                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(BARD_LEVEL)",
                        ["FORT_BASE"]   = "bad(BARD_LEVEL)",
                        ["REF_BASE"]    = "good(BARD_LEVEL)",
                        ["WILL_BASE"]   = "good(BARD_LEVEL)",
                    }
                }
            },
            {
                "PF_CLERIC", new Template()
                {
                    Name = "CLERIC",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["CLERIC_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {
                        ["CHANNELS"] = "(3 + CHA)",
                        ["CHANNEL_DMG"] = "(1d6 * (CLERIC_LEVEL / 2))"
                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(CLERIC_LEVEL)",
                        ["FORT_BASE"]   = "good(CLERIC_LEVEL)",
                        ["REF_BASE"]    = "bad(CLERIC_LEVEL)",
                        ["WILL_BASE"]   = "good(CLERIC_LEVEL)",
                    }
                }
            },
            {
                "PF_DRUID", new Template()
                {
                    Name = "DRUID",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["DRUID_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {
                        ["WILD_SHAPES"] = "(DRUID_LEVEL >= 4 ? 1 + ((DRUID_LEVEL - 4) / 2) : 0)"
                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(DRUID_LEVEL)",
                        ["FORT_BASE"]   = "good(DRUID_LEVEL)",
                        ["REF_BASE"]    = "bad(DRUID_LEVEL)",
                        ["WILL_BASE"]   = "good(DRUID_LEVEL)",
                    }
                }
            },
            {
                "PF_FIGHTER", new Template()
                {
                    Name = "FIGHTER",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["FIGHTER_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {
                        ["BRAVERY"]         = "FIGHTER_LEVEL >= 2 ? 1 + (FIGHTER_LEVEL - 2) / 4 : 0",
                        ["ARMOR_TRAINING"]  = "FIGHTER_LEVEL >= 3 ? 1 + (FIGHTER_LEVEL - 3) / 4 : 0",
                        ["WEAPON_TRAINING"] = "FIGHTER_LEVEL >= 5 ? 1 + (FIGHTER_LEVEL - 5) / 4 : 0",
                        ["BONUS_FEATS"]     = "1 + (FIGHTER_LEVEL / 2)",                   
                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "FIGHTER_LEVEL",
                        ["FORT_BASE"]   = "good(FIGHTER_LEVEL)",
                        ["REF_BASE"]    = "bad(FIGHTER_LEVEL)",
                        ["WILL_BASE"]   = "bad(FIGHTER_LEVEL)",
                    },
                    Notes = "`FIGHTER_LEVEL` set to 1."
                }
            },
            {
                "PF_MONK", new Template()
                {
                    Name = "MONK",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["MONK_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {
                        ["DMG_UNARMED"] = "MONK_LEVEL < 4 ? 1d4 : MONK_LEVEL < 8 ? 1d6 : MONK_LEVEL < 12 ? 1d8 : MONK_LEVEL < 16 ? 1d10 : MONK_LEVEL < 20 ? 2d6 : 2d8"
                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(MONK_LEVEL)",
                        ["FORT_BASE"]   = "good(MONK_LEVEL)",
                        ["REF_BASE"]    = "good(MONK_LEVEL)",
                        ["WILL_BASE"]   = "good(MONK_LEVEL)",
                        ["AC"]          = "(WIS + (MONK_LEVEL / 4) : 0)",
                        ["CMD"]         = "(WIS + (MONK_LEVEL / 4) : 0)",
                    }
                }
            },
            {
                "PF_PALADIN", new Template()
                {
                    Name = "PALADIN",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["PALADIN_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {
                        ["SMITES"]      = "(1 + PALADIN_LEVEL >= 4 ? 1 + (PALADIN_LEVEL - 4) / 3 : 0)",
                        ["DMG_SMITE"]   = "(PALADIN_LEVEL * 2)",
                        ["LOH"]         = "(PALADIN_LEVEL > 1 ? ((PALADIN_LEVEL - 1) / 2) + CHA : 0)",
                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "PALADIN_LEVEL",
                        ["FORT_BASE"]   = "(good(PALADIN_LEVEL) + (PALADIN_LEVEL > 1 ? CHA : 0))",
                        ["REF_BASE"]    = "(bad(PALADIN_LEVEL) + (PALADIN_LEVEL > 1 ? CHA : 0))",
                        ["WILL_BASE"]   = "(good(PALADIN_LEVEL)  + (PALADIN_LEVEL > 1 ? CHA : 0))",
                    }
                }
            },
            {
                "PF_RANGER", new Template()
                {
                    Name = "RANGER",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["RANGER_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "RANGER_LEVEL",
                        ["FORT_BASE"]   = "good(RANGER_LEVEL)",
                        ["REF_BASE"]    = "good(RANGER_LEVEL)",
                        ["WILL_BASE"]   = "bad(RANGER_LEVEL)",
                    }
                }
            },
            {
                "PF_ROGUE", new Template()
                {
                    Name = "ROGUE",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["ROGUE_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {
                        ["SNEAK_DICE"] = "(1d6 * (ROGUE_LEVEL / 2))"
                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(ROGUE_LEVEL)",
                        ["FORT_BASE"]   = "bad(ROGUE_LEVEL)",
                        ["REF_BASE"]    = "good(ROGUE_LEVEL)",
                        ["WILL_BASE"]   = "bad(ROGUE_LEVEL)",
                    }
                }
            },
            {
                "PF_SORCERER", new Template()
                {
                    Name = "SORCERER",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["SORCERER_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "(SORCERER_LEVEL / 2)",
                        ["FORT_BASE"]   = "bad(SORCERER_LEVEL)",
                        ["REF_BASE"]    = "bad(SORCERER_LEVEL)",
                        ["WILL_BASE"]   = "good(SORCERER_LEVEL)",
                    }
                }
            },
            {
                "PF_WIZARD", new Template()
                {
                    Name = "WIZARD",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["WIZARD_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "(WIZARD_LEVEL / 2)",
                        ["FORT_BASE"]   = "bad(WIZARD_LEVEL)",
                        ["REF_BASE"]    = "bad(WIZARD_LEVEL)",
                        ["WILL_BASE"]   = "good(WIZARD_LEVEL)",
                    }
                }
            },
            {
                "PF_ALCHEMIST", new Template()
                {
                    Name = "ALCHEMIST",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["ALCHEMIST_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {
                        ["BOMB_DMG"] = "1d6 * (ALCHEMIST_LEVEL / 2)",
                        ["BOMB_COUNT"] = "ALCHEMIST_LEVEL + INT",
                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(ALCHEMIST_LEVEL)",
                        ["FORT_BASE"]   = "good(ALCHEMIST_LEVEL)",
                        ["REF_BASE"]    = "good(ALCHEMIST_LEVEL)",
                        ["WILL_BASE"]   = "bad(ALCHEMIST_LEVEL)",
                    }
                }
            },
            {
                "PF_CAVALIER", new Template()
                {
                    Name = "CAVALIER",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["CAVALIER_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {
                        ["CHALLENGES"] = "1 + CAVALIER_LEVEL >= 4 ? 1 + (CAVALIER - 4) / 3 : 0" 
                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "CAVALIER_LEVEL",
                        ["FORT_BASE"]   = "good(CAVALIER_LEVEL)",
                        ["REF_BASE"]    = "bad(CAVALIER_LEVEL)",
                        ["WILL_BASE"]   = "bad(CAVALIER_LEVEL)",
                    }
                }
            },
            {
                "PF_GUNSLINGER", new Template()
                {
                    Name = "GUNSLINGER",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["GUNSLINGER_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {
                        ["GRIT"] = "min(1, WIS)"
                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "GUNSLINGER_LEVEL",
                        ["FORT_BASE"]   = "good(GUNSLINGER_LEVEL)",
                        ["REF_BASE"]    = "good(GUNSLINGER_LEVEL)",
                        ["WILL_BASE"]   = "bad(GUNSLINGER_LEVEL)",
                    }
                }
            },
            {
                "PF_INQUISITOR", new Template()
                {
                    Name = "INQUISITOR",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["INQUISITOR_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {
                        ["JUDGEMENT"] = "1 + INQUISITOR_LEVEL >= 4 ? (INQUISITOR_LEVEL - 4) / 3 : 0"
                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(INQUISITOR_LEVEL)",
                        ["FORT_BASE"]   = "good(INQUISITOR_LEVEL)",
                        ["REF_BASE"]    = "bad(INQUISITOR_LEVEL)",
                        ["WILL_BASE"]   = "good(INQUISITOR_LEVEL)",
                    }
                }
            },
            {
                "PF_MAGUS", new Template()
                {
                    Name = "MAGUS",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["MAGUS_LEVEL"] = 1,
                        
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {
                        ["ARCANA_POOL"] = "(MAGUS_LEVEL / 2) + INT"
                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "MAGUS_LEVEL",
                        ["FORT_BASE"]   = "good(MAGUS_LEVEL)",
                        ["REF_BASE"]    = "bad(MAGUS_LEVEL)",
                        ["WILL_BASE"]   = "good(MAGUS_LEVEL)",
                    }
                }
            },
            {
                "PF_ORACLE", new Template()
                {
                    Name = "ORACLE",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["ORACLE_LEVEL"] = 1,

                    },
                    SetExpressions = new Dictionary<string, string>()
                    {
                        
                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(ORACLE_LEVEL)",
                        ["FORT_BASE"]   = "bad(ORACLE_LEVEL)",
                        ["REF_BASE"]    = "bad(ORACLE_LEVEL)",
                        ["WILL_BASE"]   = "good(ORACLE_LEVEL)",
                    }
                }
            },
            {
                "PF_OMDURA", new Template()
                {
                    Name = "OMDURA",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["OMDURA_LEVEL"] = 1,

                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(OMDURA_LEVEL)",
                        ["FORT_BASE"]   = "good(OMDURA_LEVEL)",
                        ["REF_BASE"]    = "bad(OMDURA_LEVEL)",
                        ["WILL_BASE"]   = "good(OMDURA_LEVEL)",
                    }
                }
            },          
            {
                "PF_SHIFTER", new Template()
                {
                    Name = "SHIFTER",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["SHIFTER_LEVEL"] = 1,
                        ["INSTINCT"] = 0,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["AC"]          = "(SHIFTER_LEVEL >= 2 && INSTINCT > 1 ? WIS : INSTINCT > 0 ? (WIS/2) : 0) + (SHIFTER_LEVEL / 4) : 0)",
                        ["BAB"]         = "SHIFTER_LEVEL",
                        ["FORT_BASE"]   = "good(SHIFTER_LEVEL)",
                        ["REF_BASE"]    = "good(SHIFTER_LEVEL)",
                        ["WILL_BASE"]   = "bad(SHIFTER_LEVEL)",
                    }
                }
            },
            {
                "PF_SUMMONER", new Template()
                {
                    Name = "SUMMONER",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["SUMMONER_LEVEL"] = 1,

                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(SUMMONER_LEVEL)",
                        ["FORT_BASE"]   = "bad(SUMMONER_LEVEL)",
                        ["REF_BASE"]    = "bad(SUMMONER_LEVEL)",
                        ["WILL_BASE"]   = "good(SUMMONER_LEVEL)",
                    }
                }
            },
            {
                "PF_VAMPIREHUNTER", new Template()
                {
                    Name = "VAMPIREHUNTER",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["VAMPIREHUNTER_LEVEL"] = 1,

                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "VAMPIREHUNTER_LEVEL",
                        ["FORT_BASE"]   = "bad(VAMPIREHUNTER_LEVEL)",
                        ["REF_BASE"]    = "good(VAMPIREHUNTER_LEVEL)",
                        ["WILL_BASE"]   = "good(VAMPIREHUNTER_LEVEL)",
                    }
                }
            },
            {
                "PF_VIGILANTE", new Template()
                {
                    Name = "VIGILANTE",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["VIGILANTE_LEVEL"] = 1,

                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(VIGILANTE_LEVEL)",
                        ["FORT_BASE"]   = "bad(VIGILANTE_LEVEL)",
                        ["REF_BASE"]    = "good(VIGILANTE_LEVEL)",
                        ["WILL_BASE"]   = "good(VIGILANTE_LEVEL)",
                    }
                }
            },
            {
                "PF_WITCH", new Template()
                {
                    Name = "WITCH",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["WITCH_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "(WITCH_LEVEL / 2)",
                        ["FORT_BASE"]   = "bad(WITCH_LEVEL)",
                        ["REF_BASE"]    = "bad(WITCH_LEVEL)",
                        ["WILL_BASE"]   = "good(WITCH_LEVEL)",
                    }
                }
            },
            {
                "PF_ANTIPALADIN", new Template()
                {
                    Name = "ANTIPALADIN",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["ANTIPALADIN_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "ANTIPALADIN_LEVEL",
                        ["FORT_BASE"]   = "good(ANTIPALADIN_LEVEL)",
                        ["REF_BASE"]    = "bad(ANTIPALADIN_LEVEL)",
                        ["WILL_BASE"]   = "good(ANTIPALADIN_LEVEL)",
                    }
                }
            },
            {
                "PF_NINJA", new Template()
                {
                    Name = "NINJA",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["NINJA_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(NINJA_LEVEL)",
                        ["FORT_BASE"]   = "bad(NINJA_LEVEL)",
                        ["REF_BASE"]    = "good(NINJA_LEVEL)",
                        ["WILL_BASE"]   = "bad(NINJA_LEVEL)",
                    }
                }
            },
            {
                "PF_SAMURAI", new Template()
                {
                    Name = "SAMURAI",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["SAMURAI_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "SAMURAI_LEVEL",
                        ["FORT_BASE"]   = "good(SAMURAI_LEVEL)",
                        ["REF_BASE"]    = "bad(SAMURAI_LEVEL)",
                        ["WILL_BASE"]   = "bad(SAMURAI_LEVEL)",
                    }
                }
            },
            {
                "PF_BLOODRAGER", new Template()
                {
                    Name = "BLOODRAGER",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["BLOODRAGER_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "BLOODRAGER_LEVEL",
                        ["FORT_BASE"]   = "good(BLOODRAGER_LEVEL)",
                        ["REF_BASE"]    = "bad(BLOODRAGER_LEVEL)",
                        ["WILL_BASE"]   = "bad(BLOODRAGER_LEVEL)",
                    }
                }
            },
            {
                "PF_ARCANIST", new Template()
                {
                    Name = "ARCANIST",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["ARCANIST_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {
                        
                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "(ARCANIST_LEVEL / 2)",
                        ["FORT_BASE"]   = "bad(ARCANIST_LEVEL)",
                        ["REF_BASE"]    = "bad(ARCANIST_LEVEL)",
                        ["WILL_BASE"]   = "good(ARCANIST_LEVEL)",
                    },
                }
            },
            {
                "PF_BRAWLER", new Template()
                {
                    Name = "BRAWLER",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["BRAWLER_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "BRAWLER_LEVEL",
                        ["FORT_BASE"]   = "good(BRAWLER_LEVEL)",
                        ["REF_BASE"]    = "good(BRAWLER_LEVEL)",
                        ["WILL_BASE"]   = "bad(BRAWLER_LEVEL)",
                    },
                }
            },
            {
                "PF_HUNTER", new Template()
                {
                    Name = "HUNTER",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["HUNTER_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(HUNTER_LEVEL)",
                        ["FORT_BASE"]   = "good(HUNTER_LEVEL)",
                        ["REF_BASE"]    = "good(HUNTER_LEVEL)",
                        ["WILL_BASE"]   = "bad(HUNTER_LEVEL)",
                    },
                }
            },
            {
                "PF_INVESTIGATOR", new Template()
                {
                    Name = "INVESTIGATOR",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["INVESTIGATOR_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(INVESTIGATOR_LEVEL)",
                        ["FORT_BASE"]   = "bad(INVESTIGATOR_LEVEL)",
                        ["REF_BASE"]    = "good(INVESTIGATOR_LEVEL)",
                        ["WILL_BASE"]   = "good(INVESTIGATOR_LEVEL)",
                    },
                }
            },
            {
                "PF_SLAYER", new Template()
                {
                    Name = "SLAYER",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["SLAYER_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "SLAYER_LEVEL",
                        ["FORT_BASE"]   = "good(SLAYER_LEVEL)",
                        ["REF_BASE"]    = "good(SLAYER_LEVEL)",
                        ["WILL_BASE"]   = "bad(SLAYER_LEVEL)",
                    },
                }
            },
            {
                "PF_WARPRIEST", new Template()
                {
                    Name = "WARPRIEST",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["WARPRIEST_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(WARPRIEST_LEVEL)",
                        ["FORT_BASE"]   = "good(SLAYER_LEVEL)",
                        ["REF_BASE"]    = "bad(SLAYER_LEVEL)",
                        ["WILL_BASE"]   = "good(SLAYER_LEVEL)",
                    },
                }
            },
            {
                "PF_SWASHBUCKLER", new Template()
                {
                    Name = "SWASHBUCKLER",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["SWASHBUCKLER_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "SWASHBUCKLER_LEVEL",
                        ["FORT_BASE"]   = "bad(SWASHBUCKLER_LEVEL)",
                        ["REF_BASE"]    = "good(SWASHBUCKLER_LEVEL)",
                        ["WILL_BASE"]   = "bad(SWASHBUCKLER_LEVEL)",
                    },
                }
            },
            {
                "PF_SHAMAN", new Template()
                {
                    Name = "SHAMAN",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["SHAMAN_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(SHAMAN_LEVEL)",
                        ["FORT_BASE"]   = "bad(SHAMAN_LEVEL)",
                        ["REF_BASE"]    = "bad(SHAMAN_LEVEL)",
                        ["WILL_BASE"]   = "good(SHAMAN_LEVEL)",
                    },
                }
            },
            {
                "PF_SKALD", new Template()
                {
                    Name = "SKALD",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["SKALD_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(SKALD_LEVEL)",
                        ["FORT_BASE"]   = "good(SKALD_LEVEL)",
                        ["REF_BASE"]    = "bad(SKALD_LEVEL)",
                        ["WILL_BASE"]   = "good(SKALD_LEVEL)",
                    },
                }
            },
            {
                "PF_UC_BARBARIAN", new Template()
                {
                    Name = "UC_BARBARIAN",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["BARBARIAN_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "BARBARIAN_LEVEL",
                        ["FORT_BASE"]   = "good(BARBARIAN_LEVEL)",
                        ["REF_BASE"]    = "bad(BARBARIAN_LEVEL)",
                        ["WILL_BASE"]   = "bad(BARBARIAN_LEVEL)",
                    },
                }
            },
            {
                "PF_UC_MONK", new Template()
                {
                    Name = "UC_MONK",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["MONK_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "MONK_LEVEL",
                        ["FORT_BASE"]   = "good(MONK_LEVEL)",
                        ["REF_BASE"]    = "good(MONK_LEVEL)",
                        ["WILL_BASE"]   = "bad(MONK_LEVEL)",
                    },
                }
            },
            {
                "PF_UC_ROGUE", new Template()
                {
                    Name = "UC_ROGUE",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["ROGUE_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {
                        ["DMG_SNEAK"] = "(1d6 * (ROGUE_LEVEL / 2))"
                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(ROGUE_LEVEL)",
                        ["FORT_BASE"]   = "bad(ROGUE_LEVEL)",
                        ["REF_BASE"]    = "good(ROGUE_LEVEL)",
                        ["WILL_BASE"]   = "bad(ROGUE_LEVEL)",
                    },
                }
            },
            {
                "PF_UC_SUMMONER", new Template()
                {
                    Name = "UC_SUMMONER",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["SUMMONER_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {
                        
                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(SUMMONER_LEVEL)",
                        ["FORT_BASE"]   = "bad(SUMMONER_LEVEL)",
                        ["REF_BASE"]    = "bad(SUMMONER_LEVEL)",
                        ["WILL_BASE"]   = "good(SUMMONER_LEVEL)",
                    },
                }
            },
            {
                "PF_KINETICIST", new Template()
                {
                    Name = "KINETICIST",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["KINETICIST_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(KINETICIST_LEVEL)",
                        ["FORT_BASE"]   = "good(KINETICIST_LEVEL)",
                        ["REF_BASE"]    = "good(KINETICIST_LEVEL)",
                        ["WILL_BASE"]   = "bad(KINETICIST_LEVEL)",
                    },
                }
            },
            {
                "PF_MEDIUM", new Template()
                {
                    Name = "MEDIUM",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["MEDIUM_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(MEDIUM_LEVEL)",
                        ["FORT_BASE"]   = "bad(MEDIUM_LEVEL)",
                        ["REF_BASE"]    = "bad(MEDIUM_LEVEL)",
                        ["WILL_BASE"]   = "good(MEDIUM_LEVEL)",
                    },
                }
            },
            {
                "PF_MEMERIST", new Template()
                {
                    Name = "MEMERIST",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["MEMERIST_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(MEMERIST_LEVEL)",
                        ["FORT_BASE"]   = "bad(MEMERIST_LEVEL)",
                        ["REF_BASE"]    = "good(MEMERIST_LEVEL)",
                        ["WILL_BASE"]   = "good(MEMERIST_LEVEL)",
                    },
                }
            },
            {
                "PF_OCCULTIST", new Template()
                {
                    Name = "OCCULTIST",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["OCCULTIST_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(OCCULTIST_LEVEL)",
                        ["FORT_BASE"]   = "good(OCCULTIST_LEVEL)",
                        ["REF_BASE"]    = "bad(OCCULTIST_LEVEL)",
                        ["WILL_BASE"]   = "good(OCCULTIST_LEVEL)",
                    },
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
                    SetExpressions = new Dictionary<string, string>()
                    {
                        ["PHRENIC_MOD"]     = "",
                        ["PHRENIC_POOL"]    = "(PSYCHIC_LEVEL / 2) + PHRENIC_MOD",
                        ["PHRENIC_AMP"]     = "1 + PSYCHIC_LEVEL >= 3 ? 1 + (PSYCHIC_LEVEL - 3) / 4 : 0",                   
                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "(PSYCHIC_LEVEL / 2)",
                        ["FORT_BASE"]   = "bad(PSYCHIC_LEVEL)",
                        ["REF_BASE"]    = "bad(PSYCHIC_LEVEL)",
                        ["WILL_BASE"]   = "good(PSYCHIC_LEVEL)",
                    },
                    Notes = "Set `PHRENIC_MOD` (usually CHA or WIS) to the appropriate mod based on your discipline." + "\n" + "PSYCHIC_LEVEL` set to 1.",
                }
            },
            {
                "PF_SPIRITUALIST", new Template()
                {
                    Name = "SPIRITUALIST",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["SPIRITUALIST_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {

                    },
                    ModExpressions = new Dictionary<string, string>()
                    {
                        ["BAB"]         = "tQ(SPIRITUALIST_LEVEL)",
                        ["FORT_BASE"]   = "good(SPIRITUALIST_LEVEL)",
                        ["REF_BASE"]    = "bad(SPIRITUALIST_LEVEL)",
                        ["WILL_BASE"]   = "good(SPIRITUALIST_LEVEL)",
                    },
                }
            },
           

            ///ARCHETYPES




            //5E
            {
                "FIFTH_BARBARIAN", new Template()
                {
                    Name = "BARBARIAN",
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["BARBARIAN_LEVEL"] = 1,
                    },
                    SetExpressions = new Dictionary<string, string>()
                    {
                        ["PROF_STR"] = "TRUE",
                        ["PROF_CON"] = "TRUE",

                    },
                }
            }
        };                              
    }
}
