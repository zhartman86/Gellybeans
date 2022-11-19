using Gellybeans.Expressions;
using System.Text;

namespace Gellybeans.Pathfinder
{
    public class StatBlock : IContext
    {
        public Guid Id          { get; set; }
        public Guid CampaignId  { get; set; }

        public ulong Owner { get; set; }

        public event EventHandler<string>? ValueChanged;
        void OnValueChanged(string varChanged) { ValueChanged?.Invoke(this, varChanged); }

        public string CharacterName { get; set; } = "Name me";


        public Dictionary<string, Stat> Stats { get; private set; } = new Dictionary<string, Stat>();
        public Dictionary<string, string> Expressions { get; private set; } = new Dictionary<string, string>();
        public Dictionary<string, Template> Templates { get; private set; } = new Dictionary<string, Template>();

        public Dictionary<string, ExprRow> ExprRows { get; private set; } = new Dictionary<string, ExprRow>();
        public Dictionary<string, string[]> Grids { get; private set; } = new Dictionary<string, string[]>();

        public Dictionary<string, string> Info { get; private set; } = new Dictionary<string, string>();

        public Dictionary<string, CraftItem> Crafts { get; private set; } = new Dictionary<string, CraftItem>();

        public List<InvItem> Inventory { get; set; } = new List<InvItem>();

        public Dictionary<string, int> Constants { get; private set; } = new Dictionary<string, int>()
        {
            ["TRUE"] = 1,
            ["FALSE"] = 0,


            //bonus types
            ["TYPELESS"] = 0,
            ["ALCHEMICAL"] = 1,
            ["ARMOR"] = 2,
            ["CIRCUMSTANCE"] = 3,
            ["COMPETENCE"] = 4,
            ["DEFLECTION"] = 5,
            ["DODGE"] = 6,
            ["ENHANCEMENT"] = 7,
            ["INHERENT"] = 8,
            ["INSIGHT"] = 9,
            ["LUCK"] = 10,
            ["MORALE"] = 11,
            ["NATURAL"] = 12,
            ["PROFANE"] = 13,
            ["RACIAL"] = 14,
            ["RESISTANCE"] = 15,
            ["SACRED"] = 16,
            ["SHIELD"] = 17,
            ["SIZE"] = 18,
            ["TRAIT"] = 19,
            ["PENALTY"] = 20,
            ["BASE"] = 21,

            ["ORCUS"] = 666,
        };


        public int this[string statName]
        {
            get
            {
                if(Stats.ContainsKey(statName))
                    return Stats[statName].Value;
                return 0;
            }
            set
            {
                if(Stats.ContainsKey(statName)) Stats[statName].Base = value;
                else Stats[statName] = value;
                OnValueChanged($"stat:{statName}");
            }
        }

        public void RemoveStat(string statName)
        {
            if(Stats.Remove(statName))
                OnValueChanged($"stat:{statName}");
        }

        public void AddExpr(string name, string expr)
        {
            Expressions[name] = expr;
            OnValueChanged($"expr:{name}");
        }

        public void RemoveExpr(string name)
        {
            if(Expressions.Remove(name))
                OnValueChanged($"expr:{name}");
        }

        public void AddExprRow(ExprRow row)
        {
            ExprRows[row.RowName] = row;
            OnValueChanged($"row:{row.RowName}");
        }

        public void RemoveExprRow(string row)
        {
            if(ExprRows.Remove(row))
                OnValueChanged($"row:{row}");
        }

        public void AddGrid(string name, List<string> grid)
        {
            Grids.Add(name, grid.ToArray());
            OnValueChanged($"grid:{name}");
        }

        public void RemoveGrid(string name)
        {
            if(Grids.Remove(name))
                OnValueChanged($"grid:{name}");
        }

        public void InventorySet(List<InvItem> inv)
        {
            Inventory = inv;
            Inventory.Sort((x, y) => x.Name.CompareTo(y.Name));
            OnValueChanged("inv");
        }

        public void InventoryAdd(InvItem item)
        {
            Inventory.Add(item);
            Inventory.Sort((x, y) => x.Name.CompareTo(y.Name));
            OnValueChanged("inv");
        }

        public void InventoryAdd(List<InvItem> items)
        {
            for(int i = 0; i < items.Count; i++)
                Inventory.Add(items[i]);
            Inventory.Sort((x, y) => x.Name.CompareTo(y.Name));
            OnValueChanged("inv");
        }

        public void InventoryRemoveAt(int index)
        {
            Inventory.RemoveAt(index);
            OnValueChanged("inv");
        }

        public string InventoryOut()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{CharacterName}'s Inventory");
            sb.AppendLine();

            decimal? wTotal = 0;
            decimal? vTotal = 0;
            sb.AppendLine($"|{"#",-3}|{"NAME",-25} |{"QTY",-3} |{"VALUE",-7} |{"WT"}");
            sb.AppendLine("-----------------------------------------------------------");
            for(int i = 0; i < Inventory.Count; i++)
            {
                sb.AppendLine($"|{i,-3}|{Inventory[i].Name,-25} |{Inventory[i].Quantity,-3} |{Inventory[i].Value,-7} |{Inventory[i].Weight} {(Inventory[i].Quantity > 1 ? $"[{Inventory[i].Weight * Inventory[i].Quantity}]" : "")}");
                wTotal += Inventory[i].Weight * Inventory[i].Quantity;
                vTotal += Inventory[i].Value;
            }

            sb.AppendLine("______________________");
            sb.AppendLine($"{"ITEM COUNT",-15}|{Inventory.Count}\n{"WEIGHT TOTAL",-15}|{wTotal}\n{"VALUE TOTAL",-15}|{vTotal}");
            return sb.ToString();
        }

        public void AddBonuses(List<StatModifier> bonuses)
        {
            for(int i = 0; i < bonuses.Count; i++)
            {
                Stats[bonuses[i].StatName].AddBonus(bonuses[i].Bonus);
                OnValueChanged($"stat:{bonuses[i].StatName}");
            }      
        }

        public void ClearBonus(string bonusName)
        {
            var bonusToUpper = bonusName.ToUpper();
            foreach(var stat in Stats.Values)
                stat.RemoveBonus(bonusToUpper);
            OnValueChanged("stats");
        }

        public int ClearBonuses()
        {
            foreach(var stat in Stats.Values)
            {
                stat.Override = null!;
                stat.Bonuses.Clear();
            }
            OnValueChanged("stats");
            return 1;
        }


        public int Resolve(string varName, StringBuilder sb)
        {
            varName = varName.Replace(' ', '_').ToUpper();
            if(Constants.ContainsKey(varName))
                return Constants[varName];

            if(varName[0] == '@')
            {
                var replace = varName.Replace("@", "");
                if(Stats.ContainsKey(replace))
                    return Stats[replace].Base;
            }

            if(Stats.ContainsKey(varName))
                return this[varName];
            if(Expressions.ContainsKey(varName))
                return Parser.Parse(Expressions[varName]).Eval(this, sb);
            if(sb != null) sb.AppendLine($"{varName} not found");
            return 0;
        }

        public int Assign(string statName, int assignment, TokenType assignType, StringBuilder sb)
        {
            sb ??= new StringBuilder();
            if(Stats.Count > 100)
            {
                sb.AppendLine("stat count limited to 100");
                return -99;
            }

            statName = statName.Replace(' ', '_').ToUpper();
            if(Expressions.ContainsKey(statName))
            {
                sb.AppendLine("Cannot assign value to an expression using /eval. Use /var Set-Expression instead");
                return -99;
            }
            if(Constants.ContainsKey(statName))
            {
                sb.AppendLine("Cannot change a constant value");
                return -99;
            }

            if(!Stats.ContainsKey(statName)) Stats[statName] = 0;

            switch(assignType)
            {
                case TokenType.AssignEquals:
                    this[statName] = assignment;
                    break;

                case TokenType.AssignAdd:
                    this[statName] += assignment;
                    break;

                case TokenType.AssignSub:
                    this[statName] -= assignment;
                    break;

                case TokenType.AssignMul:
                    this[statName] *= assignment;
                    break;

                case TokenType.AssignDiv:
                    this[statName] /= assignment;
                    break;

                case TokenType.AssignMod:
                    this[statName] %= assignment;
                    break;
            }
            sb.AppendLine($"{statName} set to {Stats[statName].Base}");;
            return Stats[statName];
        }

        public int Bonus(string statName, string bonusName, int type, int value, TokenType assignType, StringBuilder sb)
        {
            if(assignType == TokenType.GetBon)
            {
                var result = Parser.Parse(bonusName).Eval(this, sb);
                return Stats[statName].GetBonus((BonusType)result);
            }

            if(string.IsNullOrEmpty(statName) && assignType == TokenType.AssignSubBon)
            {
                if(bonusName == "")
                {
                    ClearBonuses();
                    sb.AppendLine("removed all bonuses from all stats");
                }                   
                else
                {
                    ClearBonus(bonusName);
                    sb.AppendLine($"{bonusName} removed from all stats");
                }
            
                return 1;
            }
                

            if(Enum.GetName(typeof(BonusType), type) == null)
            {
                sb.AppendLine("Invalid bonus type");
                return -99;
            }

            statName = statName.Replace(' ', '_').ToUpper();
            if(Expressions.ContainsKey(statName))
            {
                sb.AppendLine("Cannot assign bonus to an expression");
                return -99;
            }

            if(!Stats.ContainsKey(statName)) Stats[statName] = 0;


            switch(assignType)
            {
                case TokenType.AssignAddBon:
                    if(bonusName[0] == '.')
                    {

                    }
                    var bonus = new Bonus { Name = bonusName, Type = (BonusType)type, Value = value };
                    Stats[statName].AddBonus(bonus);
                    sb.AppendLine(bonus.ToString());
                    break;

                case TokenType.AssignSubBon:
                    Stats[statName].RemoveBonus(bonusName);
                    sb.AppendLine($"{bonusName} removed from {statName}");
                    break;
            }
            OnValueChanged($"stats");
            sb.AppendLine($"{statName} set to {this[statName]}");            
            return value;
        }


        public static StatBlock DefaultPathfinder(string name)
        {
            var stats = new StatBlock()
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
                    ["SIZE_SKL"] = 0,

                    ["HP_BASE"] = 0,

                    ["STR_SCORE"] = 10,
                    ["DEX_SCORE"] = 10,
                    ["CON_SCORE"] = 10,
                    ["INT_SCORE"] = 10,
                    ["WIS_SCORE"] = 10,
                    ["CHA_SCORE"] = 10,

                    //since damage and temporary bonuses apply symmetrical effects, the same field can be used for both.
                    ["STR_TEMP"] = 0,
                    ["DEX_TEMP"] = 0,
                    ["CON_TEMP"] = 0,
                    ["INT_TEMP"] = 0,
                    ["WIS_TEMP"] = 0,
                    ["CHA_TEMP"] = 0,

                    ["SAVE_BONUS"] = 0,
                    ["FORT_BONUS"] = 0,
                    ["REF_BONUS"] = 0,
                    ["WILL_BONUS"] = 0,

                    ["MOVE_BONUS"] = 0,
                    ["MOVE_BASE"] = 0,
                    ["MOVE_BURROW"] = 0,
                    ["MOVE_CLIMB"] = 0,
                    ["MOVE_FLY"] = 0,
                    ["MOVE_SWIM"] = 0,

                    ["BAB"] = 0,

                    ["INIT_BONUS"] = 0,

                    ["CMB_BONUS"] = 0,
                    ["CMB_BULLRUSH"] = 0,
                    ["CMB_DIRTY"] = 0,
                    ["CMB_DISARM"] = 0,
                    ["CMB_OVERRUN"] = 0,
                    ["CMB_REPOSITION"] = 0,
                    ["CMB_STEAL"] = 0,
                    ["CMB_SUNDER"] = 0,
                    ["CMB_TRIP"] = 0,

                    ["CMD_BONUS"] = 0,
                    ["CMD_BULLRUSH"] = 0,
                    ["CMD_DIRTY"] = 0,
                    ["CMD_DISARM"] = 0,
                    ["CMD_OVERRUN"] = 0,
                    ["CMD_REPOSITION"] = 0,
                    ["CMD_STEAL"] = 0,
                    ["CMD_SUNDER"] = 0,
                    ["CMD_TRIP"] = 0,

                    ["AC_BONUS"] = 0,
                    ["AC_MAXDEX"] = 99,
                    ["AC_PENALTY"] = 0,

                    ["ATK_BONUS"] = 0,
                    ["ATK_BONUS_MELEE"] = 0,
                    ["ATK_BONUS_RANGED"] = 0,
                    ["DMG_BONUS"] = 0,
                    ["DMG_BONUS_MELEE"] = 0,
                    ["DMG_BONUS_RANGED"] = 0,

                    ["CL_BONUS"] = 0,

                    //skills
                    ["SK_ACR"] = 0,
                    ["SK_APR"] = 0,
                    ["SK_BLF"] = 0,
                    ["SK_CLM"] = 0,
                    ["SK_DIP"] = 0,
                    ["SK_DSA"] = 0,
                    ["SK_DSG"] = 0,
                    ["SK_ESC"] = 0,
                    ["SK_FLY"] = 0,
                    ["SK_HND"] = 0,
                    ["SK_HEA"] = 0,
                    ["SK_ITM"] = 0,
                    ["SK_LNG"] = 0,
                    ["SK_PRC"] = 0,
                    ["SK_RDE"] = 0,
                    ["SK_SNS"] = 0,
                    ["SK_SLT"] = 0,
                    ["SK_SPL"] = 0,
                    ["SK_STL"] = 0,
                    ["SK_SUR"] = 0,
                    ["SK_SWM"] = 0,
                    ["SK_UMD"] = 0,
                    ["SK_ARC"] = 0,
                    ["SK_DUN"] = 0,
                    ["SK_ENG"] = 0,
                    ["SK_GEO"] = 0,
                    ["SK_HIS"] = 0,
                    ["SK_LCL"] = 0,
                    ["SK_NTR"] = 0,
                    ["SK_NBL"] = 0,
                    ["SK_PLN"] = 0,
                    ["SK_RLG"] = 0,
                    ["SK_ALL"] = 0,

                    ["PP"] = 0,
                    ["GP"] = 0,
                    ["SP"] = 0,
                    ["CP"] = 0
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

                    ["FORT"] = "1d20 + FORT_BONUS + SAVE_BONUS + CON",
                    ["REF"] = "1d20 + REF_BONUS + SAVE_BONUS + DEX",
                    ["WILL"] = "1d20 + WILL_BONUS + SAVE_BONUS + WIS",

                    ["INIT"] = "1d20 + INIT_BONUS + DEX",

                    ["MAXDEX"] = "max(0, AC_MAXDEX)",
                    ["AC"] = "10 + AC_BONUS + min(DEX, MAXDEX) + SIZE_MOD",
                    ["TOUCH"] = "AC - ((AC_BONUS $ ARMOR) + (AC_BONUS $ SHIELD) + (AC_BONUS $ NATURAL))",
                    ["FLAT"] = "AC - ((AC_BONUS $ DODGE) + DEX)",

                    ["CMB"] = "1d20 + BAB + STR + CMB_BONUS - SIZE_MOD",

                    ["BULLRUSH"]    = "CMB + CMB_GRAPPLE",
                    ["DIRTY"]       = "CMB + CMB_DIRTY",
                    ["DISARM"]      = "CMB + CMB_DISARM",
                    ["OVERRUN"]     = "CMB + CMB_OVERRUN",
                    ["REPOSITION"]  = "CMB + CMB_REPOSITION",
                    ["STEAL"]       = "CMB + CMB_STEAL",
                    ["SUNDER"]      = "CMB + CMB_SUNDER",
                    ["TRIP"]        = "CMB + CMB_TRIP",                  

                    ["MOVE"]        = "MOVE_BASE + MOVE_BONUS",
                    ["MOVE_BURROW"] = "MOVE_BURROW  + MOVE_BONUS",
                    ["MOVE_CLIMB"]  = "MOVE_CLIMB + MOVE_BONUS",
                    ["MOVE_FLY"]    = "MOVE_FLY + MOVE_BONUS",
                    ["MOVE_SWIM"]   = "MOVE_SWIM + MOVE_BONUS",

                    ["CMD"] = "10 + BAB + STR + DEX + CMD_BONUS + ((AC_BONUS $ CIRCUMSTANCE) + (AC_BONUS $ DEFLECTION) + (AC_BONUS $ DODGE) + (AC_BONUS $ INSIGHT) + (AC_BONUS $ LUCK) + (AC_BONUS $ MORALE) + (AC_BONUS $ PROFANE) + (AC_BONUS $ SACRED))  - SIZE_MOD",

                    ["ATK"] = "BAB + SIZE_MOD + ATK_BONUS",

                    ["ATK_STR"] = "1d20 + (STR + (STR_TEMP / 2)) - (min(STR_SCORE $ ENHANCEMENT, STR_TEMP $ ENHANCEMENT) / 2) + ATK",
                    ["ATK_DEX"] = "1d20 + (DEX + (DEX_TEMP / 2)) - (min(DEX_SCORE $ ENHANCEMENT, DEX_TEMP $ ENHANCEMENT) / 2) + ATK",
                    ["ATK_CON"] = "1d20 + (CON + (CON_TEMP / 2)) - (min(CON_SCORE $ ENHANCEMENT, CON_TEMP $ ENHANCEMENT) / 2) + ATK",
                    ["ATK_INT"] = "1d20 + (INT + (INT_TEMP / 2)) - (min(INT_SCORE $ ENHANCEMENT, INT_TEMP $ ENHANCEMENT) / 2) + ATK",
                    ["ATK_WIS"] = "1d20 + (WIS + (WIS_TEMP / 2)) - (min(WIS_SCORE $ ENHANCEMENT, WIS_TEMP $ ENHANCEMENT) / 2) + ATK",
                    ["ATK_CHA"] = "1d20 + (CHA + (CHA_TEMP / 2)) - (min(CHA_SCORE $ ENHANCEMENT, CHA_TEMP $ ENHANCEMENT) / 2) + ATK",

                    ["DMG_STR"] = "(STR + (STR_TEMP / 2)) - (min(STR_SCORE $ ENHANCEMENT, STR_TEMP $ ENHANCEMENT) / 2) + DMG_BONUS",
                    ["DMG_DEX"] = "(DEX + (DEX_TEMP / 2)) - (min(DEX_SCORE $ ENHANCEMENT, DEX_TEMP $ ENHANCEMENT) / 2) + DMG_BONUS",
                    ["DMG_CON"] = "(CON + (CON_TEMP / 2)) - (min(CON_SCORE $ ENHANCEMENT, CON_TEMP $ ENHANCEMENT) / 2) + DMG_BONUS",
                    ["DMG_INT"] = "(INT + (INT_TEMP / 2)) - (min(INT_SCORE $ ENHANCEMENT, INT_TEMP $ ENHANCEMENT) / 2) + DMG_BONUS",
                    ["DMG_WIS"] = "(WIS + (WIS_TEMP / 2)) - (min(WIS_SCORE $ ENHANCEMENT, WIS_TEMP $ ENHANCEMENT) / 2) + DMG_BONUS",
                    ["DMG_CHA"] = "(CHA + (CHA_TEMP / 2)) - (min(CHA_SCORE $ ENHANCEMENT, CHA_TEMP $ ENHANCEMENT) / 2) + DMG_BONUS",

                    ["PA_ATK"] = "1 + BAB / 4",
                    ["PA_DMG"] = "2 + (BAB / 4 * 2)",

                    //skills
                    ["ACR"] = "1d20 + DEX + SK_ALL + SK_ACR + AC_PENALTY",
                    ["APR"] = "1d20 + INT + SK_ALL + SK_APR",
                    ["BLF"] = "1d20 + CHA + SK_ALL + SK_BLF",
                    ["CLM"] = "1d20 + STR + SK_ALL + SK_CLM + AC_PENALTY",
                    ["DIP"] = "1d20 + CHA + SK_ALL + SK_DIP",
                    ["DSA"] = "1d20 + DEX + SK_ALL + SK_DSA + AC_PENALTY",
                    ["DSG"] = "1d20 + CHA + SK_ALL + SK_DSG",
                    ["ESC"] = "1d20 + DEX + SK_ALL + SK_ESC + AC_PENALTY",
                    ["FLY"] = "1d20 + DEX + SK_ALL + SK_FLY + AC_PENALTY + SIZE_SKL",
                    ["HND"] = "1d20 + DEX + SK_ALL + SK_HND",
                    ["HEA"] = "1d20 + WIS + SK_ALL + SK_HEA",
                    ["ITM"] = "1d20 + CHA + SK_ALL + SK_ITM",
                    ["LNG"] = "1d20 + INT + SK_ALL + SK_LNG",
                    ["PRC"] = "1d20 + WIS + SK_ALL + SK_PRC",
                    ["RDE"] = "1d20 + DEX + SK_ALL + SK_RDE + AC_PENALTY",
                    ["SNS"] = "1d20 + WIS + SK_ALL + SK_SNS",
                    ["SLT"] = "1d20 + DEX + SK_ALL + SK_SLT + AC_PENALTY",
                    ["SPL"] = "1d20 + INT + SK_ALL + SK_SPL",
                    ["STL"] = "1d20 + DEX + SK_ALL + SK_STL + AC_PENALTY + (SIZE_SKL * 2)",
                    ["SUR"] = "1d20 + WIS + SK_ALL + SK_SUR",
                    ["SWM"] = "1d20 + STR + SK_ALL + SK_SWM + AC_PENALTY",
                    ["UMD"] = "1d20 + CHA + SK_ALL + SK_UMD",

                    ["ARC"] = "1d20 + INT + SK_ALL + SK_ARC",
                    ["DUN"] = "1d20 + INT + SK_ALL + SK_DUN",
                    ["ENG"] = "1d20 + INT + SK_ALL + SK_ENG",
                    ["GEO"] = "1d20 + INT + SK_ALL + SK_GEO",
                    ["HIS"] = "1d20 + INT + SK_ALL + SK_HIS",
                    ["LCL"] = "1d20 + INT + SK_ALL + SK_LCL",
                    ["NTR"] = "1d20 + INT + SK_ALL + SK_NTR",
                    ["NBL"] = "1d20 + INT + SK_ALL + SK_NBL",
                    ["PLN"] = "1d20 + INT + SK_ALL + SK_PLN",
                    ["RLG"] = "1d20 + INT + SK_ALL + SK_RLG",

                },

                ExprRows = new Dictionary<string, ExprRow>()
                {
                    {
                        "SV", new ExprRow()
                        {
                            RowName = "SAVES",

                            Set = new List<Expr>()
                            {
                                new Expr("FORT",    "FORT"),
                                new Expr("REF",     "REF"),
                                new Expr("WILL",    "WILL"),
                            },
                        }
                    },
                    {
                        "SK_ONE", new ExprRow()
                        {
                            RowName = "SKILLS ONE",

                            Set = new List<Expr>()
                            {
                                new Expr("ACRO",    "ACR"),
                                new Expr("APPR",    "APR"),
                                new Expr("BLFF",    "BLF"),
                                new Expr("CLMB",    "CLM"),
                                new Expr("DIPL",    "DIP"),
                            },
                        }
                    },
                    {
                        "SK_TWO", new ExprRow()
                        {
                            RowName = "SKILLS TWO",

                            Set = new List<Expr>()
                            {
                                new Expr("DSBL",    "DSA"),
                                new Expr("ESC",     "ESC"),
                                new Expr("ANIM",    "HND"),
                                new Expr("HEAL",    "HEA"),
                                new Expr("INTI",    "ITM"),
                            },
                        }
                    },
                    {
                        "SK_THREE", new ExprRow()
                        {
                            RowName = "SKILLS THREE",

                            Set = new List<Expr>()
                            {
                                new Expr("ARC",     "ARC"),
                                new Expr("DNG",     "DUN"),
                                new Expr("ENG",     "ENG"),
                                new Expr("GEO",     "GEO"),
                                new Expr("HIS",     "HIS"),
                            },
                        }
                    },
                    {
                        "SK_FOUR", new ExprRow()
                        {
                            RowName = "SKILLS FOUR",

                            Set = new List<Expr>()
                            {
                                new Expr("LOCL",    "LCL"),
                                new Expr("NAT",     "NTR"),
                                new Expr("NOBL",    "NBL"),
                                new Expr("PLNS",    "PLN"),
                                new Expr("RLGN",    "RLG"),
                            },
                        }
                    },
                    {
                        "SK_FIVE", new ExprRow()
                        {
                            RowName = "SKILLS FIVE",

                            Set = new List<Expr>()
                            {
                                new Expr("PERC",    "PRC"),
                                new Expr("SENS",    "SNS"),
                                new Expr("SPEL",    "SPL"),
                                new Expr("STEL",    "STL"),
                                new Expr("SURV",    "SUR"),
                            },
                        }
                    },
                    {
                        "SK_SIX", new ExprRow()
                        {
                            RowName = "SKILLS SIX",

                            Set = new List<Expr>()
                            {
                                new Expr("DISG",    "DSG"),
                                new Expr("LING",    "LNG"),
                                new Expr("RIDE",    "RDE"),
                                new Expr("SLGT",    "SLT"),
                                new Expr("SWIM",    "SWM"),
                            },
                        }
                    },
                },

                Grids = new Dictionary<string, string[]>()
                {
                    { "SK", new string[5] { "SK_ONE","SK_TWO","SK_THREE","SK_FOUR","SK_FIVE" } }
                },
            };

            return stats;
        }
    }
}












