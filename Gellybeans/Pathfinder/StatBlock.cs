using Gellybeans.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace Gellybeans.Pathfinder
{
    public class StatBlock : IContext
    {

        public Guid Id { get; set; }
        public ulong Owner { get; set; }

        public event EventHandler<string> ValueChanged;
        void OnValueChanged(string varChanged) { ValueChanged?.Invoke(this, varChanged); }


        public string CharacterName { get; set; } = "Name me";


        public Dictionary<string, Stat>         Stats       { get; private set; } = new Dictionary<string, Stat>();        
        public Dictionary<string, string>       Expressions { get; private set; } = new Dictionary<string, string>();
        public Dictionary<string, Template>     Templates   { get; private set; } = new Dictionary<string, Template>();
        public Dictionary<string, int>          Constants   { get; private set; } = new Dictionary<string, int>();

        public Dictionary<string, ExprRow>      ExprRows    { get; private set; } = new Dictionary<string, ExprRow>();
        public Dictionary<string, string[]>     Grids       { get; private set; } = new Dictionary<string, string[]>();
        
        public Dictionary<string, string>       Info        { get; private set; } = new Dictionary<string, string>();     

        public Dictionary<string, CraftItem>    Crafts      { get; private set; } = new Dictionary<string, CraftItem>();
        

        public List<Item> Inventory { get; set; } = new List<Item>();



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
                OnValueChanged(statName);
            }
        }

        public string InventoryOut()
        {   
            var sb = new StringBuilder();
    
            sb.AppendLine("```");
            sb.AppendLine($"{CharacterName}'s Inventory");
            sb.AppendLine();

            decimal wTotal = 0;
            decimal vTotal = 0;
            sb.AppendLine($"|{"#",-4}|{"NAME",-17} |{"WEIGHT",-10} |{"VALUE",-8}");
            sb.AppendLine("--------------------------------------------");
            for(int i = 0; i < Inventory.Count; i++)
            {
                sb.AppendLine($"|{i,-4}|{Inventory[i].Name,-17} |{Inventory[i].Weight,-10} |{Inventory[i].Value,-8}");
                wTotal += Inventory[i].Weight;
                vTotal += Inventory[i].Value;
            }

            sb.AppendLine("______________________");
            sb.AppendLine($"{"ITEM COUNT",-15}|{Inventory.Count}\n{"WEIGHT TOTAL",-15}|{wTotal}\n{"VALUE TOTAL",-15}|{vTotal}");
            sb.AppendLine("```");
            
            return sb.ToString();
        }
        
        public void AddBonuses(List<StatModifier> bonuses)
        {
            for(int i = 0; i < bonuses.Count; i++)
                Stats[bonuses[i].StatName].AddBonus(bonuses[i].Bonus);
        }

        public void ClearBonus(string bonusName)
        {
            foreach(var stat in Stats.Values)
                for(int i = 0; i < stat.Bonuses.Count; i++)
                    if(stat.Bonuses[i].Name == bonusName)
                        stat.RemoveBonus(stat.Bonuses[i]);
        }
        
        public string AddTemplate(string templateName, StringBuilder sb)
        {

            var t = Template.Templates[templateName];
            sb.AppendLine($"~ADD TEMPLATE: {t.Name}~");           
            foreach(var stat in t.Stats)
            {
                if(Stats.ContainsKey(stat.Key))
                {
                    Stats[stat.Key].Base += t.Stats[stat.Key].Base;
                    sb.AppendLine($"{stat.Key} changed to {Stats[stat.Key].Base}.");
                }                 
                else
                {
                    Stats[stat.Key] = t.Stats[stat.Key];
                    sb.AppendLine($"{stat.Key} added to stats (value:{stat.Value.Base})");
                }                    
            }          
           
            sb.AppendLine();
            foreach(var expr in t.SetExpressions)
            {
                if(Expressions.ContainsKey(expr.Key))
                    sb.AppendLine($"{expr.Key} was overwritten to {expr.Value}.");                 
                else
                    sb.AppendLine($"Added {expr.Key} to expressions ({expr.Value})");                   
                
                Expressions[expr.Key] = expr.Value;
            }
            sb.AppendLine();

            if(t.ModExpressions.Count > 0) sb.AppendLine("The follow modifications will be made during eval:");
            foreach(var expr in t.ModExpressions)
                sb.AppendLine($"|{expr.Key,-13} -:- {expr.Value,22}|");
            
            sb.AppendLine();

            Templates[t.Name] = t;
            return sb.ToString();
        }

        public string AddTemplate(List<string> templates, StringBuilder sb)
        {
            for(int i = 0; i < templates.Count; i++)
                AddTemplate(templates[i], sb);
            
            return sb.ToString();
        }

        public string RemoveTemplate(string templateName, StringBuilder sb)
        {
            if(!Templates.ContainsKey(templateName))
            {
                sb.AppendLine($"{templateName} not found");
                return sb.ToString(); ;
            }
                
            foreach(var stat in Templates[templateName].Stats)
                if(Stats.Remove(stat.Key)) 
                    sb.AppendLine($"{stat.Key} removed from stats");

            foreach(var expr in Templates[templateName].SetExpressions)
                if(Expressions.Remove(expr.Key))
                    sb.AppendLine($"{expr.Key} removed from expressions");

            Templates.Remove(templateName);
            sb.AppendLine($"{templateName} removed");
            
            return sb.ToString();
        }

        
        //IContext
        public int Call(string methodName, int[] args) => methodName switch
        {
            "abs"       => Math.Abs(args[0]),
            "clamp"     => Math.Clamp(args[0], args[1], args[2]),
            "if"        => args[0] == 1 ? args[1] : 0,
            "max"       => Math.Max(args[0], args[1]),
            "min"       => Math.Min(args[0], args[1]),
            "mod"       => (args[0] - 10) / 2,
            "rand"      => new Random().Next(args[0], args[1]+1),
            "bad"       => args[0] / 3,
            "good"      => 2 + (args[0] / 2),
            "tQ"        => (args[0] + (args[0] / 2)) / 2,
            _           => 0
        };

        public int Resolve(string varName, StringBuilder sb)
        {
            var toUpper = varName.ToUpper();      
            if(Constants.ContainsKey(toUpper)) 
                return Constants[toUpper];           
            
            if(toUpper[0] == '@')
            {
                var replace = toUpper.Replace("@", "");
                if(Stats.ContainsKey(replace)) 
                    return Stats[replace].Base;
            }
            
            if(Stats.ContainsKey(toUpper))
                return this[toUpper];
            if(Expressions.ContainsKey(toUpper))
            {
                string templateExprs = "";
                foreach(var template in Templates.Values)
                    if(template.ModExpressions.ContainsKey(toUpper))
                        templateExprs += $" + {template.ModExpressions[toUpper]}";
                
                var total = Expressions[toUpper];
                if(templateExprs != "") total += templateExprs;
                
                Console.WriteLine(total);
                return Parser.Parse(total).Eval(this, sb);
            }

            sb.AppendLine($"{varName} not found");
            return 0;
        }

        public int Assign(string statName, int assignment, TokenType assignType, StringBuilder sb)
        {          
            if(Stats.Count > 100)
            {
                sb.AppendLine("stat count limited to 100");
                return -99;
            }
                      
            var toUpper = statName.ToUpper();
            if(Expressions.ContainsKey(toUpper))
            {
                sb.AppendLine("Cannot assign value to an expression using /eval. Use /var Set-Expression instead");
                return -99;
            }
            if(Constants.ContainsKey(toUpper))
            {
                sb.AppendLine("Cannot change a constant value");
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
            OnValueChanged(statName);
            return Stats[toUpper];
        }

        public int Bonus(string statName, string bonusName, int type, int value, TokenType assignType, StringBuilder sb)
        {           
            if(assignType == TokenType.GetBon)
            {
                var result = Parser.Parse(bonusName).Eval(this, sb);
                return Stats[statName].GetBonus((BonusType)result);         
            }            
            
            if(Enum.GetName(typeof(BonusType), type) == null)
            {
                sb.AppendLine("Invalid bonus type");
                return -99;
            }
            
            var toUpper = statName.ToUpper();
            if(Expressions.ContainsKey(toUpper))
            {
                sb.AppendLine("Cannot assign bonus to expression");
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
            OnValueChanged(statName);
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

                Constants = new Dictionary<string, int>()
                {
                    ["TRUE"]            = 1,
                    ["FALSE"]           = 0,


                    //bonus types
                    ["TYPELESS"]        = 0,
                    ["ALCHEMICAL"]      = 1,
                    ["ARMOR"]           = 2,
                    ["CIRCUMSTANCE"]    = 3,
                    ["COMPETENCE"]      = 4,
                    ["DEFLECTION"]      = 5,
                    ["DODGE"]           = 6,
                    ["ENHANCEMENT"]     = 7,
                    ["INHERENT"]        = 8,
                    ["INSIGHT"]         = 9,
                    ["LUCK"]            = 10,
                    ["MORALE"]          = 11,
                    ["NATURAL"]         = 12,
                    ["PROFANE"]         = 13,
                    ["RACIAL"]          = 14,
                    ["RESISTANCE"]      = 15,
                    ["SACRED"]          = 16,
                    ["SHIELD"]          = 17,
                    ["SIZE"]            = 18,
                    ["TRAIT"]           = 19,

                    ["ORCUS"]           = 666,
                },
                
                
                Stats = new Dictionary<string, Stat>()
                {
                    ["LEVEL"] = 1,

                    ["SIZE_MOD"] = 0,
                    ["SIZE_FLY"] = 0,
                    ["SIZE_STL"] = 0,

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

                    ["FORT_BONUS"]  = 0,
                    ["REF_BONUS"]   = 0,
                    ["WILL_BONUS"]  = 0,
                    
                    ["MOVE"] = 0,

                    ["BAB"] = 0,

                    ["INIT_BONUS"] = 0,
                    ["CMB_BONUS"] = 0,
                    ["CMD_BONUS"] = 0,

                    ["AC_BONUS"] = 10,
                    ["AC_MAXDEX"] = 99,
                    ["AC_PENALTY"] = 0,
               
                    ["ATK_BONUS"]   = 0,
                    ["DMG_BONUS"]   = 0,


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

                    ["PP"] = 0,
                    ["GP"] = 0,
                    ["SP"] = 0,
                    ["CP"] = 0
                },

                Expressions = new Dictionary<string, string>()
                {
                    ["HP"]      = "HP_BASE + (CON * LEVEL)",

                    ["STR"]     = "mod(STR_SCORE)",
                    ["DEX"]     = "mod(DEX_SCORE)",
                    ["CON"]     = "mod(CON_SCORE)",
                    ["INT"]     = "mod(INT_SCORE)",
                    ["WIS"]     = "mod(WIS_SCORE)",
                    ["CHA"]     = "mod(CHA_SCORE)",                   
                    
                    ["FORT"]    = "1d20 + FORT_BASE + CON",
                    ["REF"]     = "1d20 + REF_BASE + DEX",
                    ["WILL"]    = "1d20 + WILL_BASE + WIS",

                    ["INIT"]    = "1d20 + INIT_BONUS + DEX",

                    ["AC"]      = "AC_BONUS + min(DEX, AC_MAXDEX) + SIZE_MOD",
                    ["TOUCH"]   = "AC - ((AC_BONUS $ ARMOR) + (AC_BONUS $ SHIELD) + (AC_BONUS $ NATURAL))",
                    ["FLAT"]    = "AC - ((AC_BONUS $ DODGE) + DEX)",

                    ["CMB"]     = "1d20 + BAB + STR + CMB_BONUS - SIZE_MOD",
                    ["CMD"]     = "10 + BAB + STR + DEX + CMD_BONUS - SIZE_MOD",
                    
                    ["ATK"]     = "BAB + SIZE_MOD + ATK_BONUS",                  

                    ["ATK_STR"]   = "ATK + STR + (STR_TEMP / 2)",
                    ["ATK_DEX"]   = "ATK + DEX + (DEX_TEMP / 2)",
                    ["ATK_CON"]   = "ATK + CON + (CON_TEMP / 2)",
                    ["ATK_INT"]   = "ATK + INT + (INT_TEMP / 2)",
                    ["ATK_WIS"]   = "ATK + WIS + (WIS_TEMP / 2)",
                    ["ATK_CHA"]   = "ATK + CHA + (CHA_TEMP / 2)",

                    ["DMG"]     = "STR + DMG_BONUS + (STR_TEMP / 2)",
                    ["DMG_TH"]  = "DMG + (DMG / 2)",
                    ["DMG_OH"]  = "DMG / 2",

                    
                    //skills
                    ["ACR"] = "1d20 + DEX + SK_ACR + AC_PENALTY",
                    ["APR"] = "1d20 + INT + SK_APR",
                    ["BLF"] = "1d20 + CHA + SK_BLF",
                    ["CLM"] = "1d20 + STR + SK_CLM + AC_PENALTY",
                    ["DIP"] = "1d20 + CHA + SK_DIP",
                    ["DSA"] = "1d20 + DEX + SK_DSA + AC_PENALTY",
                    ["DSG"] = "1d20 + CHA + SK_DSG",
                    ["ESC"] = "1d20 + DEX + SK_ESC + AC_PENALTY",
                    ["FLY"] = "1d20 + DEX + SK_FLY + AC_PENALTY",
                    ["HND"] = "1d20 + DEX + SK_HND",
                    ["HEA"] = "1d20 + WIS + SK_HEA",
                    ["ITM"] = "1d20 + CHA + SK_ITM",
                    ["LNG"] = "1d20 + INT + SK_LNG",
                    ["PRC"] = "1d20 + WIS + SK_PRC",
                    ["RDE"] = "1d20 + DEX + SK_RDE + AC_PENALTY",
                    ["SNS"] = "1d20 + WIS + SK_SNS",
                    ["SLT"] = "1d20 + DEX + SK_SLT + AC_PENALTY",
                    ["SPL"] = "1d20 + INT + SK_SPL",
                    ["STL"] = "1d20 + DEX + SK_STL + AC_PENALTY",
                    ["SUR"] = "1d20 + WIS + SK_SUR",
                    ["SWM"] = "1d20 + STR + SK_SWM + AC_PENALTY",
                    ["UMD"] = "1d20 + CHA + SK_UMD",

                    ["ARC"] = "1d20 + INT + SK_ARC",
                    ["DUN"] = "1d20 + INT + SK_DUN",
                    ["ENG"] = "1d20 + INT + SK_ENG",
                    ["GEO"] = "1d20 + INT + SK_GEO",
                    ["HIS"] = "1d20 + INT + SK_HIS",
                    ["LCL"] = "1d20 + INT + SK_LCL",
                    ["NTR"] = "1d20 + INT + SK_NTR",
                    ["NBL"] = "1d20 + INT + SK_NBL",
                    ["PLN"] = "1d20 + INT + SK_PLN",
                    ["RLG"] = "1d20 + INT + SK_RLG",
                                                 
                   

                    //["CS_ACR"] = "FALSE",
                    //["CS_APR"] = "FALSE",
                    //["CS_BLF"] = "FALSE",
                    //["CS_CLM"] = "FALSE",
                    //["CS_DIP"] = "FALSE",
                    //["CS_DSA"] = "FALSE",
                    //["CS_DSG"] = "FALSE",
                    //["CS_ESC"] = "FALSE",
                    //["CS_FLY"] = "FALSE",
                    //["CS_HND"] = "FALSE",
                    //["CS_HEA"] = "FALSE",
                    //["CS_ITM"] = "FALSE",
                    
                    //["CS_ARC"] = "FALSE",
                    //["CS_DUN"] = "FALSE",
                    //["CS_ENG"] = "FALSE",
                    //["CS_GEO"] = "FALSE",
                    //["CS_HIS"] = "FALSE",
                    //["CS_LCL"] = "FALSE",
                    //["CS_NTR"] = "FALSE",
                    //["CS_NBL"] = "FALSE",
                    //["CS_PLN"] = "FALSE",
                    //["CS_RLG"] = "FALSE",
                    
                    //["CS_LNG"] = "FALSE",
                    //["CS_PRC"] = "FALSE",
                    //["CS_RDE"] = "FALSE",
                    //["CS_SNS"] = "FALSE",
                    //["CS_SLT"] = "FALSE",
                    //["CS_SPL"] = "FALSE",
                    //["CS_STL"] = "FALSE",
                    //["CS_SUR"] = "FALSE",
                    //["CS_SWM"] = "FALSE",
                    //["CS_UMD"] = "FALSE",
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



                    ["AC_BASE"] = 10,
                    ["AC_MAXDEX"] = 99,

                    ["INITIATIVE"] = 0,

                    ["SK_ACRO"] = 0,
                    ["SK_ANIM"] = 0,
                    ["SK_ARCA"] = 0,
                    ["SK_ATHL"] = 0,
                    ["SK_DECE"] = 0,
                    ["SK_HIST"] = 0,
                    ["SK_INSI"] = 0,
                    ["SK_INTI"] = 0,
                    ["SK_INVE"] = 0,
                    ["SK_MEDI"] = 0,
                    ["SK_NATU"] = 0,
                    ["SK_PERC"] = 0,
                    ["SK_PERF"] = 0,
                    ["SK_PERS"] = 0,
                    ["SK_RELI"] = 0,
                    ["SK_SLEI"] = 0,
                    ["SK_STEA"] = 0,
                    ["SK_SURV"] = 0,

                    ["ATK_BONUS"] = 0,
                    ["DMG_BONUS"] = 0,
                },

                Expressions = new Dictionary<string, string>()
                {
                    ["LEVEL"] = "",
                    
                    ["HP"] = "HP_BASE + (CON * LEVEL)",

                    ["PROF"] = "2 + (LEVEL - 1) / 4",
                    
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
                    ["ACRO"] = "1d20 + DEX + SK_ACR) + if(PROF_ACRO, PROF)",
                    ["ANIM"] = "1d20 + WIS + SK_ANIM + if(PROF_ANIM, PROF)",
                    ["ARCA"] = "1d20 + INT + SK_ACRA + if(PROF_ARCA, PROF)",
                    ["ATHL"] = "1d20 + STR + SK_ATHL + if(PROF_ATHL, PROF)",
                    ["DECE"] = "1d20 + CHA + SK_DECE + if(PROF_DECE, PROF)",
                    ["HIST"] = "1d20 + INT + SK_HIST + if(PROF_HIST, PROF)",
                    ["INSI"] = "1d20 + WIS + SK_INSI + if(PROF_INSI, PROF)",
                    ["INTI"] = "1d20 + CHA + SK_INTI + if(PROF_INTI, PROF)",
                    ["INVE"] = "1d20 + INT + SK_INVE + if(PROF_INVE, PROF)",
                    ["MEDI"] = "1d20 + WIS + SK_MEDI + if(PROF_MEDI, PROF)",
                    ["NATU"] = "1d20 + INT + SK_NATU + if(PROF_NATU, PROF)",
                    ["PERC"] = "1d20 + WIS + SK_PERC + if(PROF_PERC, PROF)",
                    ["PERF"] = "1d20 + CHA + SK_PERF + if(PROF_PERF, PROF)",
                    ["PERS"] = "1d20 + CHA + SK_PERS + if(PROF_PERS, PROF)",
                    ["RELI"] = "1d20 + INT + SK_RELI + if(PROF_RELI, PROF)",
                    ["SLEI"] = "1d20 + DEX + SK_SLEI + if(PROF_SLEI, PROF)",
                    ["STEA"] = "1d20 + DEX + SK_STEA + if(PROF_STEA, PROF)",
                    ["SURV"] = "1d20 + WIS + SK_SURV + if(PROF_SURV, PROF)",

                    ["PASSIVE"] = "10 + WIS + if(PROF_PERC, PROF)",


                    //proficiencies
                    ["PROF_STR"]    = "FALSE",
                    ["PROF_DEX"]    = "FALSE",
                    ["PROF_CON"]    = "FALSE",
                    ["PROF_INT"]    = "FALSE",
                    ["PROF_WIS"]    = "FALSE",
                    ["PROF_CHA"]    = "FALSE",
                    
                    ["PROF_ACRO"]   = "FALSE",
                    ["PROF_ANIM"]   = "FALSE",
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
                        "SK_ONE", new ExprRow()
                        {
                            RowName = "SK_ONE",
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
                        "SK_TWO", new ExprRow()
                        {
                            RowName = "SK_TWO",
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
                        "SK_THREE", new ExprRow()
                        {
                            RowName = "SK_THREE",
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
                        "SK_FOUR", new ExprRow()
                        {
                            RowName = "SK_FOUR",
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
                    { "SK", new string[4] { "SK_ONE","SK_TWO","SK_THREE","SK_FOUR" } }
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












