using Gellybeans.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace Gellybeans.Pathfinder
{
    public class StatBlock : IContext
    {

        public Guid Id { get; set; }
        public ulong Owner { get; set; }

        public event EventHandler<string> ValueAssigned;
        void OnValueAssigned(string statChanged) { ValueAssigned?.Invoke(this, statChanged); }

        public string CharacterName { get; set; } = "Name me";

        public Dictionary<string, Stat>         Stats       { get; private set; } = new Dictionary<string, Stat>();
        public Dictionary<string, string>       Expressions { get; private set; } = new Dictionary<string, string>();
        public Dictionary<string, ExprRow>      ExprRows    { get; private set; } = new Dictionary<string, ExprRow>();

        public Dictionary<string, string>       Info        { get; private set; } = new Dictionary<string, string>();

        public Dictionary<string, string[]>     Grids       { get; private set; } = new Dictionary<string, string[]>();
        
        

        public List<Item> Inventory { get; set; } = new List<Item>();



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
            "mod"       => (args[0] - 10) / 2,
            "min"       => Math.Min(args[0], args[1]),
            "max"       => Math.Max(args[0], args[1]),
            "clamp"     => Math.Clamp(args[0], args[1], args[2]),
            "abs"       => Math.Abs(args[0]),
            "rand"      => new Random().Next(args[0], args[1]+1),
            "if"        => args[0] == 1 ? args[1] : 0,
            _           => 0
        };

        public int Resolve(string varName, StringBuilder sb)
        {
            var toUpper = varName.ToUpper();

            if(toUpper == "TRUE")   return 1;
            if(toUpper == "FALSE")  return 0;

            if(Stats.ContainsKey(toUpper))
                return this[toUpper];
            else if(Expressions.ContainsKey(toUpper))
                return Parser.Parse(Expressions[toUpper]).Eval(this, sb);
            return 0;
        }

        public int Assign(string statName, int assignment, TokenType assignType, StringBuilder sb)
        {
            var toUpper = statName.ToUpper();
            if(Expressions.ContainsKey(toUpper))
            {
                sb.AppendLine("Cannot assign value to expression. Use /var Set-Expression instead.");
                return -99;
            }

            if(!Stats.ContainsKey(toUpper)) Stats[toUpper] = 0;

            switch(assignType)
            {
                case TokenType.AssignEquals:
                    this[toUpper] = assignment;
                    break;

                case TokenType.AssignAdd:
                    this[toUpper] += assignment;
                    break;

                case TokenType.AssignSub:
                    this[toUpper] -= assignment;
                    break;

                case TokenType.AssignMul:
                    this[toUpper] *= assignment;
                    break;

                case TokenType.AssignDiv:
                    this[toUpper] /= assignment;
                    break;

                case TokenType.AssignMod:
                    this[toUpper] %= assignment;
                    break;
            }
            sb.AppendLine($"{toUpper} set to {Stats[toUpper].Base}");
            OnValueAssigned(statName);
            return Stats[toUpper].Base;
        }

        public int AssignBonus(string statName, string bonusName, int type, int value, TokenType assignType, StringBuilder sb)
        {
            if(Enum.GetName(typeof(BonusType), type) == null)
            {
                sb.AppendLine("Invalid bonus type.");
                return -99;
            }
            var toUpper = statName.ToUpper();
            if(Expressions.ContainsKey(toUpper))
            {
                sb.AppendLine("Cannot assign value to expression. Use /var Set-Expression instead.");
                return -99;
            }

            if(!Stats.ContainsKey(toUpper)) Stats[toUpper] = 0;

            
            switch(assignType)
            {
                case TokenType.AssignAddBon:
                    var bonus = new Bonus { Name = bonusName, Type = (BonusType)type, Value = value };            
                    Stats[toUpper].AddBonus(bonus);
                    sb.AppendLine(bonus.ToString());
                    break;

                case TokenType.AssignSubBon:
                    Stats[toUpper].RemoveBonus(bonusName);
                    sb.AppendLine($"{bonusName} removed from {statName}");
                    break;
            }
            
            sb.AppendLine($"{toUpper} set to {this[toUpper]}");
            OnValueAssigned(statName);
            return value;    
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
                    ["LEVEL"] = 1,

                    ["SIZE_MOD"] = 0,
                    ["SIZE_MAN"] = 0,
                    ["SIZE_FLY"] = 0,
                    ["SIZE_STEALTH"] = 0,

                    ["HP_BASE"] = 0,
                    ["HP_TEMP"] = 0,
                    ["HP_DMG"] = 0,

                    ["STR_SCORE"] = 12,
                    ["DEX_SCORE"] = 15,
                    ["CON_SCORE"] = 16,
                    ["INT_SCORE"] = 18,
                    ["WIS_SCORE"] = 8,
                    ["CHA_SCORE"] = 6,

                    //since damage and temporary bonuses apply symmetrical effects, the same field can be used for both.
                    ["STR_TEMP"] = 0,
                    ["DEX_TEMP"] = 0,
                    ["CON_TEMP"] = 0,
                    ["INT_TEMP"] = 0,
                    ["WIS_TEMP"] = 0,
                    ["CHA_TEMP"] = 0,

                    ["MOVE"] = 0,

                    ["INITIATIVE"] = 0,

                    ["AC_BONUS"] = 0,
                    ["AC_MAXDEX"] = 99,
                    ["AC_PENALTY"] = 0,

                    ["SAVE_FORT"] = 0,
                    ["SAVE_REFLEX"] = 0,
                    ["SAVE_WILL"] = 0,

                    ["BAB"]         = 0,
                    ["ATK_BONUS"]   = 0,
                    ["TW_PEN"]      = -2,

                    ["DMG_BONUS"]   = 0,


                    //magic
                    ["CL"] = 0,


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
                    ["HP"] = "HP_BASE + (CON * LEVEL)",

                    ["STR"] = "mod(STR_SCORE)",
                    ["DEX"] = "mod(DEX_SCORE)",
                    ["CON"] = "mod(CON_SCORE)",
                    ["INT"] = "mod(INT_SCORE)",
                    ["WIS"] = "mod(WIS_SCORE)",
                    ["CHA"] = "mod(CHA_SCORE)",

                    ["FORT"]    = "1d20 + FORT_BASE + CON",
                    ["REFLEX"]  = "1d20 + REFLEX_BASE + DEX",
                    ["WILL"]    = "1d20 + WILL_BASE + WIS",

                    ["INIT"]    = "1d20 + INITIATIVE + DEX",

                    ["AC"]      = "ARMOR_BONUS + min(DEX, AC_MAXDEX) + SIZE_MOD",

                    ["CMB"]     = "1d20 + BAB + STR + SIZE_MOD",
                    ["CMD"]     = "10 + BAB + STR + DEX + SIZE_MOD",             

                    ["ATK"]     = "BAB + SIZE_MOD + ATK_BONUS + if(TW, TW_PEN)",
                    ["TW"]      = "FALSE",

                    ["ATK_S"]   = "ATK + STR + (STR_TEMP / 2)",
                    ["ATK_D"]   = "ATK + DEX + (DEX_TEMP / 2)",
                    ["ATK_C"]   = "ATK + CON + (CON_TEMP / 2)",
                    ["ATK_I"]   = "ATK + INT + (INT_TEMP / 2)",
                    ["ATK_W"]   = "ATK + WIS + (WIS_TEMP / 2)",
                    ["ATK_C"]   = "ATK + CHA + (CHA_TEMP / 2)",

                    ["DMG"]     = "STR + DMG_BONUS",
                    ["DMG_TH"]  = "DMG + (DMG / 2)",
                    ["DMG_OH"]  = "DMG / 2",


                    //magic
                    ["SPELL_MOD"]   = "INT",
                    ["CONCENTRATE"] = "1d20 + SPELL_MOD + CL",


                    
                    //skills
                    ["ACROBATICS"]      = "1d20 + DEX + SK_ACR + AC_PENALTY",
                    ["APPRAISE"]        = "1d20 + INT + SK_APR",
                    ["BLUFF"]           = "1d20 + CHA + SK_BLF",
                    ["CLIMB"]           = "1d20 + STR + SK_CLM + AC_PENALTY",
                    ["DIPLOMACY"]       = "1d20 + CHA + SK_DPL",
                    ["DISABLE"]         = "1d20 + DEX + SK_DEV",
                    ["DISGUISE"]        = "1d20 + CHA + SK_DSG",
                    ["ESCAPE"]          = "1d20 + DEX + SK_ESC + AC_PENALTY",
                    ["FLY"]             = "1d20 + DEX + SK_FLY + AC_PENALTY" + "SIZE_FLY",
                    ["HANDLE"]          = "1d20 + DEX + SK_HAN",
                    ["HEAL"]            = "1d20 + WIS + SK_HEA",
                    ["INTIMIDATE"]      = "1d20 + CHA + SK_INT",
                    ["ARCANA"]          = "1d20 + INT + SK_ARC",
                    ["DUNGEONEERING"]   = "1d20 + INT + SK_DUN",
                    ["GEOGRAPHY"]       = "1d20 + INT + SK_GEO",
                    ["HISTORY"]         = "1d20 + INT + SK_HIS",
                    ["LOCAL"]           = "1d20 + INT + SK_LOC",
                    ["NATURE"]          = "1d20 + INT + SK_NAT",
                    ["NOBILITY"]        = "1d20 + INT + SK_NOB",
                    ["PLANES"]          = "1d20 + INT + SK_PLA",
                    ["RELIGION"]        = "1d20 + INT + SK_REL",
                    ["LINGUISTICS"]     = "1d20 + INT + SK_LNG",
                    ["PERCEPTION"]      = "1d20 + WIS + SK_PER",
                    ["RIDE"]            = "1d20 + DEX + SK_RDE + AC_PENALTY",
                    ["MOTIVE"]          = "1d20 + WIS + SK_SMO",
                    ["SLEIGHT"]         = "1d20 + DEX + SK_SLE + AC_PENALTY",
                    ["SPELLCRAFT"]      = "1d20 + INT + SK_SPL",
                    ["STEALTH"]         = "1d20 + DEX + SK_STL + AC_PENALTY" + "SIZE_STEALTH",
                    ["SURVIVAL"]        = "1d20 + WIS + SK_SUR",
                    ["SWIM"]            = "1d20 + STR + SK_SWM + AC_PENALTY",
                    ["UMD"]             = "1d20 + CHA + SK_UMD",

                },

                ExprRows = new Dictionary<string, ExprRow>()
                {
                    {
                        "$SAVES", new ExprRow()
                        {                                                   
                            RowName = "$SAVES",

                            Set = new List<Expr>()
                            { 
                                new Expr("FORT",    "FORT"),
                                new Expr("REF",     "REFLEX"),
                                new Expr("WILL",    "WILL"),
                                new Expr("INIT",    "INIT"),
                                new Expr("CMB",     "CMB"),                                
                            },                        
                        }
                    },
                    {
                        "$SK_ONE", new ExprRow()
                        {
                            RowName = "$SK_ONE",

                            Set = new List<Expr>()
                            {
                                new Expr("ACRO",    "ACROBATICS"),
                                new Expr("APPR",    "APPRAISE"),
                                new Expr("BLFF",    "BLUFF"),
                                new Expr("CLMB",    "CLIMB"),
                                new Expr("DIPL",    "DIPLOMACY"),
                            },            
                        }
                    },
                    {
                        "$SK_TWO", new ExprRow()
                        {
                            RowName = "$SK_TWO",

                            Set = new List<Expr>()
                            {
                                new Expr("DISBL",    "DISABLE"),
                                new Expr("ESC",     "ESCAPE"),
                                new Expr("HND",    "HANDLE"),
                                new Expr("HEAL",    "HEAL"),
                                new Expr("INTI",    "INTIMIDATE"),
                            }, 
                        }
                    },
                    {
                        "$SK_THREE", new ExprRow()
                        {
                            RowName = "$SK_THREE",

                            Set = new List<Expr>()
                            {
                                new Expr("ARCA",    "ARCANA"),
                                new Expr("DNG",     "DUNGEONEERING"),
                                new Expr("ENG",     "ENGINEERING"),
                                new Expr("GEO",     "GEOGRAPHY"),
                                new Expr("HIST",    "HISTORY"),
                            },                   
                        }
                    },
                    {
                        "$SK_FOUR", new ExprRow()
                        {
                            RowName = "$SK_FOUR",

                            Set = new List<Expr>()
                            {
                                new Expr("LOCL",    "LOCAL"),
                                new Expr("NAT",     "NATURE"),
                                new Expr("NOBL",    "NOBILITY"),
                                new Expr("PLNS",    "PLANES"),
                                new Expr("RLGN",    "RELIGION"),
                            },                    
                        }
                    },
                    {
                        "$SK_FIVE", new ExprRow()
                        {
                            RowName = "$SK_FIVE",

                            Set = new List<Expr>()
                            {
                                new Expr("PERC",    "PERCEPTION"),
                                new Expr("SENS",    "MOTIVE"),
                                new Expr("SPEL",    "SPELLCRAFT"),
                                new Expr("STL",     "STEALTH"),
                                new Expr("SURV",    "SURVIVAL"),
                            },
                        }
                    },
                    {
                        "$SK_SIX", new ExprRow()
                        {
                            RowName = "$SK_MISC",

                            Set = new List<Expr>()
                            {
                                new Expr("DISG",    "DISGUISE"),
                                new Expr("LING",    "LINGUISTICS"),
                                new Expr("RIDE",    "RIDE"),
                                new Expr("SLGT",    "SLEIGHT"),
                                new Expr("SWIM",    "SWIM"),
                            },
                        }
                    },
                    {
                        "$LONGSWORD", new ExprRow()
                        {
                            RowName = "$LONGSWORD",

                            Set = new List<Expr>()
                            {
                                new Expr("HIT",     "1d20 + ATK_S"),
                                new Expr("DMG_M",   "1d8 + DMG"),
                                new Expr("DMG_TH",  "1d8 + DMG_TH"),
                                new Expr("CRIT_M",  "(1d8*2) + DMG*2"),
                                new Expr("CRIT_TH", "(1d8*2) + DMG_TH*2"),
                            },
                        }                       
                    },
                    {
                        "$SHIELD_HEAVY", new ExprRow()
                        {
                            RowName = "$SHIELD_HEAVY",

                            Set = new List<Expr>()
                            {
                                new Expr("HIT",     "1d20 + ATK_S"),
                                new Expr("DMG_M",   "1d4 + DMG"),
                                new Expr("DMG_OH",  "1d4 + DMG_OH"),
                                new Expr("CRIT_M",  "(1d4*2) + DMG*2"),
                                new Expr("CRIT_OH", "(1d4*2) + DMG_OH*2"),
                            },
                        }
                    },
                    {
                        "$LONGBOW", new ExprRow()
                        {
                            RowName = "$LONGBOW",

                            Set = new List<Expr>()
                            {
                                new Expr("HIT",         "1d20 + ATK_D"),
                                new Expr("DMG",         "1d8"),
                                new Expr("CRIT",        "1d8*3"),
                                new Expr("DMG_COMP",    "1d8 + DMG"),
                                new Expr("CRT_COMP",    "(1d8*3) + DMG*3"),
                            },
                        }

                    },
                },
                
                Grids = new Dictionary<string, string[]>()
                {
                    { "#SK", new string[5] { "$SK_ONE","$SK_TWO","$SK_THREE","$SK_FOUR","$SK_FIVE" } }
                }
            };
           
            return statBlock;
        }
            
        public static StatBlock DefaultFifthEd(string name)
        {
            var statBlock = new StatBlock()
            {
                CharacterName = name,
                Stats = new Dictionary<string, Stat>()
                {
                    ["HP_BASE"] = 0,
                    ["HP_TEMP"] = 0,
                    ["DMG"] = 0,

                    ["STR_SCORE"] = 10,
                    ["DEX_SCORE"] = 10,
                    ["CON_SCORE"] = 10,
                    ["INT_SCORE"] = 10,
                    ["WIS_SCORE"] = 10,
                    ["CHA_SCORE"] = 10,

                    ["PROF"] = 0,

                    ["AC_BASE"] = 10,
                    ["AC_MAXDEX"] = 99,

                    ["INITIATIVE"] = 0,

                    ["SK_ACR"] = 0,
                    ["SK_ANI"] = 0,
                    ["SK_ARC"] = 0,
                    ["SK_ATH"] = 0,
                    ["SK_DEC"] = 0,
                    ["SK_HIS"] = 0,
                    ["SK_INS"] = 0,
                    ["SK_INT"] = 0,
                    ["SK_INV"] = 0,
                    ["SK_MED"] = 0,
                    ["SK_NAT"] = 0,
                    ["SK_PERC"] = 0,
                    ["SK_PERF"] = 0,
                    ["SK_PERS"] = 0,
                    ["SK_REL"] = 0,
                    ["SK_SLT"] = 0,
                    ["SK_STL"] = 0,
                    ["SK_SUR"] = 0,

                    ["ATK_BONUS"] = 0,
                    ["DMG_BONUS"] = 0,
                },

                Expressions = new Dictionary<string, string>()
                {               
                    ["HP"] = "HP_BASE + (CON * LEVEL)",                   

                    ["ATK_S"] = "1d20 + STR + PROF + ATK_BONUS",
                    ["ATK_D"] = "1d20 + DEX + PROF + ATK_BONUS",
                    ["ATK_I"] = "1d20 + INT + PROF + ATK_BONUS",
                    ["ATK_W"] = "1d20 + WIS + PROF + ATK_BONUS",
                    ["ATK_C"] = "1d20 + CHA + PROF + ATK_BONUS",

                    
                    ["STR"] = "mod(STR_SCORE)",
                    ["DEX"] = "mod(DEX_SCORE)",
                    ["CON"] = "mod(CON_SCORE)",
                    ["INT"] = "mod(INT_SCORE)",
                    ["WIS"] = "mod(WIS_SCORE)",
                    ["CHA"] = "mod(CHA_SCORE)",

                    ["SAVE_STR"] = "1d20 + STR + if(PROF_STR, PROF)",
                    ["SAVE_DEX"] = "1d20 + DEX + if(PROF_DEX, PROF)",
                    ["SAVE_CON"] = "1d20 + CON + if(PROF_CON, PROF)",
                    ["SAVE_INT"] = "1d20 + INT + if(PROF_INT, PROF)",
                    ["SAVE_WIS"] = "1d20 + WIS + if(PROF_WIS, PROF)",
                    ["SAVE_CHA"] = "1d20 + CHA + if(PROF_CHA, PROF)",

                    ["INIT"]    = "1d20 + DEX + INITIATIVE",
                    ["AC"]      = "AC_BASE + min(DEX, AC_MAXDEX)",


                    //skills
                    ["ACRO"] = "1d20 + DEX + if(PROF_ACRO, PROF)",
                    ["HAND"] = "1d20 + WIS + if(PROF_HAND, PROF)",
                    ["ARCA"] = "1d20 + INT + if(PROF_ARCA, PROF)",
                    ["ATHL"] = "1d20 + STR + if(PROF_ATHL, PROF)",
                    ["DECE"] = "1d20 + CHA + if(PROF_DECE, PROF)",
                    ["HIST"] = "1d20 + INT + if(PROF_HIST, PROF)",
                    ["INSI"] = "1d20 + WIS + if(PROF_INSI, PROF)",
                    ["INTI"] = "1d20 + CHA + if(PROF_INTI, PROF)",
                    ["INVE"] = "1d20 + INT + if(PROF_INVE, PROF)",
                    ["MEDI"] = "1d20 + WIS + if(PROF_MEDI, PROF)",
                    ["NATU"] = "1d20 + INT + if(PROF_NATU, PROF)",
                    ["PERC"] = "1d20 + WIS + if(PROF_PERC, PROF)",
                    ["PERF"] = "1d20 + CHA + if(PROF_PERF, PROF)",
                    ["PERS"] = "1d20 + CHA + if(PROF_PERS, PROF)",
                    ["RELI"] = "1d20 + INT + if(PROF_RELI, PROF)",
                    ["SLEI"] = "1d20 + DEX + if(PROF_SLEI, PROF)",
                    ["STEA"] = "1d20 + DEX + if(PROF_STEA, PROF)",
                    ["SURV"] = "1d20 + WIS + if(PROF_SURV, PROF)",

                    ["PASSIVE"] = "10 + WIS + if(PROF_PERC, PROF)",


                    //proficiencies
                    ["PROF_STR"]    = "FALSE",
                    ["PROF_DEX"]    = "FALSE",
                    ["PROF_CON"]    = "FALSE",
                    ["PROF_INT"]    = "FALSE",
                    ["PROF_WIS"]    = "FALSE",
                    ["PROF_CHA"]    = "FALSE",
                    
                    ["PROF_ACRO"]   = "FALSE",
                    ["PROF_HAND"]   = "FALSE",
                    ["PROF_ARCA"]   = "FALSE",
                    ["PROF_ATHL"]   = "FALSE",
                    ["PROF_DECE"]   = "FALSE",
                    ["PROF_HIST"]   = "FALSE",
                    ["PROF_INSI"]   = "FALSE",
                    ["PROF_INTI"]   = "FALSE",
                    ["PROF_INVE"]   = "FALSE",
                    ["PROF_MEDI"]   = "FALSE",
                    ["PROF_NATU"]   = "FALSE",
                    ["PROF_PERC"]   = "FALSE",
                    ["PROF_PERF"]   = "FALSE",
                    ["PROF_PERS"]   = "FALSE",
                    ["PROF_RELI"]   = "FALSE",
                    ["PROF_SLEI"]   = "FALSE",
                    ["PROF_STEA"]   = "FALSE",
                    ["PROF_SURV"]   = "FALSE",
                },

                ExprRows = new Dictionary<string, ExprRow>()
                {
                    {
                        "$SK_ONE", new ExprRow()
                        {
                            RowName = "$SK_ONE",
                            Set = new List<Expr>()
                            {
                                new Expr("ACRO", "ACRO"),
                                new Expr("HAND", "HAND"),
                                new Expr("ACRA", "ARCA"),
                                new Expr("ATHL", "ATHL"),
                                new Expr("DECE", "DECE"),
                            }
                        }
                    },
                    {
                        "$SK_TWO", new ExprRow()
                        {
                            RowName = "$SK_TWO",
                            Set = new List<Expr>()
                            {
                                new Expr("HIST", "HIST"),
                                new Expr("INSI", "INSI"),
                                new Expr("INTI", "INTI"),
                                new Expr("INVE", "INVE"),
                                new Expr("MEDI", "MEDI"),
                            }
                        }
                    },
                    {
                        "$SK_THREE", new ExprRow()
                        {
                            RowName = "$SK_THREE",
                            Set = new List<Expr>()
                            {
                                new Expr("NATU", "NATU"),
                                new Expr("PERC", "PERC"),
                                new Expr("PERF", "PERF"),
                                new Expr("PERS", "PERS"),
                                new Expr("RELI", "RELI"),
                            }
                        }
                    },
                    {
                        "$SK_FOUR", new ExprRow()
                        {
                            RowName = "$SK_FOUR",
                            Set = new List<Expr>()
                            {
                                new Expr("SLEI", "SLEI"),
                                new Expr("STEA", "STEA"),
                                new Expr("SURV", "SURV"),
                            }
                        }

                    },           
                },

                Grids = new Dictionary<string, string[]>()
                {
                    { "#SK", new string[4] { "$SK_ONE","$SK_TWO","$SK_THREE","$SK_FOUR" } }
                }
            };

            return statBlock;
            }

        public static StatBlock DefaultStarfinder(string name)
        {
            var statblock = new StatBlock()
            {
                Stats = new Dictionary<string, Stat>()
                {
                    ["LEVEL"] = 1,

                    ["CREDITS"] = 0,

                    ["HP"] = 0,
                    ["STAM_BASE"] = 0,
                    ["RESOLVE"] = 0,
                    
                    ["DMG_HP"] = 0,
                    ["DMG_STAM"] = 0,

                    ["STR_SCORE"] = 10,
                    ["DEX_SCORE"] = 10,
                    ["CON_SCORE"] = 10,
                    ["INT_SCORE"] = 10,
                    ["WIS_SCORE"] = 10,
                    ["CHA_SCORE"] = 10,

                    ["EAC_BONUS"] = 0,
                    ["KAC_BONUS"] = 0,
                    ["AC_MAXDEX"] = 99,
                    ["AC_PENALTY"] = 0,

                    ["INITIATIVE"] = 0,

                    ["SAVE_FORT"] = 0,
                    ["SAVE_REFL"] = 0,
                    ["SAVE_WILL"] = 0,

                    ["BAB"] = 0,

                    ["ATK_M_BONUS"]     = 0,
                    ["ATK_R_BONUS"]     = 0,
                    ["ATK_T_BONUS"]     = 0,
                    ["ATK_CM_BONUS"]    = 0,

                    //skills
                    ["SK_ACR"] = 0,
                    ["SK_ATH"] = 0,
                    ["SK_BLF"] = 0,
                    ["SK_COM"] = 0,
                    ["SK_CUL"] = 0,
                    ["SK_DPL"] = 0,                    
                    ["SK_DSG"] = 0,
                    ["SK_ENG"] = 0,
                    ["SK_INT"] = 0,
                    ["SK_LIF"] = 0,
                    ["SK_MED"] = 0,
                    ["SK_MYS"] = 0,
                    ["SK_PER"] = 0,
                    ["SK_PHY"] = 0,
                    ["SK_PLT"] = 0,
                    ["SK_MOT"] = 0,
                    ["SK_SLE"] = 0,
                    ["SK_STL"] = 0,
                    ["SK_SUR"] = 0,
                },
                
                Expressions = new Dictionary<string, string>()
                {
                    ["STR"]     = "mod(STR_SCORE)",
                    ["DEX"]     = "mod(DEX_SCORE)",
                    ["CON"]     = "mod(CON_SCORE)",
                    ["INT"]     = "mod(INT_SCORE)",
                    ["WIS"]     = "mod(WIS_SCORE)",
                    ["CHA"]     = "mod(CHA_SCORE)",

                    ["FORT"]    = "1d20 + FORT_BASE + CON",
                    ["REFL"]    = "1d20 + REFL_BASE + DEX",
                    ["WILL"]    = "1d20 + WILL_BASE + WIS",

                    ["INIT"]    = "1d20 + INITIATIVE + DEX",

                    ["EAC"]     = "EAC_BONUS + min(DEX, AC_MAXDEX)",
                    ["KAC"]     = "KAC_BONUS + min(DEX, AC_MAXDEX)",
                    ["CMD"]     = "KAC + 8",

                    
                    ["ATK_M"]   = "1d20 + BAB + STR + ATK_M_BONUS",
                    ["ATK_R"]   = "1d20 + BAB + DEX + ATK_R_BONUS",
                    ["ATK_T"]   = "1d20 + BAB + STR + ATK_T_BONUS",
                    ["ATK_CM"]  = "1d20 + BAB + STR + ATK_CM_BONUS",

                    //skills
                    ["ACRO"]        = "1d20 + DEX + SK_ACR + AC_PENALTY",
                    ["ATHL"]        = "1d20 + DEX + SK_ATH + AC_PENALTY",              
                    ["BLUFF"]       = "1d20 + CHA + SK_BLF",
                    ["COMP"]        = "1d20 + INT + SK_COM",
                    ["CULT"]        = "1d20 + INT + SK_CUL",
                    ["DIPL"]        = "1d20 + CHA + SK_DPL",
                    ["DISG"]        = "1d20 + CHA + SK_DSG",
                    ["ENG"]         = "1d20 + INT + SK_ENG",
                    ["INTI"]        = "1d20 + CHA + SK_INT",
                    ["LIFE"]        = "1d20 + INT + SK_LIF",
                    ["MED"]         = "1d20 + INT + SK_MED",
                    ["MYST"]        = "1d20 + WIS + SK_MYS",
                    ["PERC"]        = "1d20 + WIS + SK_PER",
                    ["PHYS"]        = "1d20 + INT + SK_PHY",
                    ["PILOT"]       = "1d20 + DEX + SK_PIL",
                    ["MOTIVE"]      = "1d20 + WIS + SK_MOT",
                    ["SLEIGHT"]     = "1d20 + DEX + SK_SLE + AC_PENALTY",
                    ["STEALTH"]     = "1d20 + DEX + SK_STL + AC_PENALTY",
                    ["SURV"]        = "1d20 + WIS + SK_SUR",
                }           
            };
            
            return statblock;
        }       
    }
}












