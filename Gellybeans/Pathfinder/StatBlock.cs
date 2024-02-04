using Gellybeans.Expressions;
using System.Text;
using System.Text.RegularExpressions;

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
        
        public List<InvItem>                Inventory   { get; private set; } = new List<InvItem>();
        public Dictionary<string, Stat>     Stats       { get; private set; } = new Dictionary<string, Stat>();
        public Dictionary<string, string>   Expressions { get; private set; } = new Dictionary<string, string>();       
        public Dictionary<string, ExprRow>  ExprRows    { get; private set; } = new Dictionary<string, ExprRow>();
        
        public Dictionary<string, string> Info          { get; private set; } = new Dictionary<string, string>();

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

        //im not sure where else to validate variable names, as expressions don't have the same limitations, which can contain variable names.
        public static readonly Regex validVarName = new Regex(@"^[^0-9][^\[\]<>(){}^@:+*/%=!&|;$#?\-.'""]*$");

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
                if(Stats.ContainsKey(statName) && validVarName.IsMatch(statName)) 
                    Stats[statName].Base = value;
                else 
                    Stats[statName] = value;
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
            if(expr == "")
            {
                OnValueChanged($"edit:{name}");
                return;
            }

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

        public int Resolve(string varName, StringBuilder sb = null!)
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
                return Parser.Parse(Expressions[varName]).Eval(this);
            
            sb?.AppendLine($"*{varName}?*");
            
            return 0;
        }

        public int Assign(string varName, string assignment, TokenType assignType, StringBuilder sb)
        {
            sb ??= new StringBuilder();

            varName = varName.Replace(' ', '_').ToUpper();
            
            if(!validVarName.IsMatch(varName))
            {
                sb.AppendLine($"Invalid variable name {varName}. No leading numbers or any special characters.");
                return -99;
            }

            if(assignType == TokenType.AssignExpr && Stats.ContainsKey(varName))
                RemoveStat(varName);             
            else if(assignType != TokenType.AssignExpr && Expressions.ContainsKey(varName))
                RemoveExpr(varName);
                             
            if(Constants.ContainsKey(varName))
            {
                sb.AppendLine("Cannot change a constant value");
                return -99;
            }

            if(assignType != TokenType.AssignExpr && !Stats.ContainsKey(varName)) Stats[varName] = 0;

            switch(assignType)
            {
                case TokenType.AssignExpr:
                    AddExpr(varName, assignment);
                    break;
                case TokenType.Assign:
                    this[varName] = int.Parse(assignment);
                    break;
                case TokenType.AssignAdd:
                    this[varName] += int.Parse(assignment);
                    break;
                case TokenType.AssignSub:
                    this[varName] -= int.Parse(assignment);
                    break;
                case TokenType.AssignMul:
                    this[varName] *= int.Parse(assignment);
                    break;
                case TokenType.AssignDiv:
                    this[varName] /= int.Parse(assignment);
                    break;
                case TokenType.AssignMod:
                    this[varName] %= int.Parse(assignment);
                    break;
                case TokenType.Flag: //::
                    var val = int.TryParse(assignment, out int outVal) && outVal < 64 && outVal > -64 ? outVal : 0;
                    Console.WriteLine(val);
                    if(Math.Sign(val) > 0)
                        this[varName] |= 1 << val;          
                    else
                        this[varName] &= ~(1 << Math.Abs(val));
                    break;
            }
            sb.AppendLine($"{varName} set to {(assignType != TokenType.AssignExpr ? Stats[varName].Base : Expressions[varName])}");;
            return 1;
        }

        public int Bonus(string statName, string bonusName, int type, int value, TokenType assignType, StringBuilder sb)
        {
            if(assignType == TokenType.Bonus)
            {
                var result = Parser.Parse(bonusName).Eval(this);
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
                    sb.AppendLine($"{bonus} to {statName} (Total:{this[statName]})");
                    break;

                case TokenType.AssignSubBon:
                    Stats[statName].RemoveBonus(bonusName);
                    sb.AppendLine($"{bonusName} removed from {statName}");
                    break;
            }
            OnValueChanged($"stats");      
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


                    ["STR_DAMAGE"] = 0,
                    ["DEX_DAMAGE"] = 0,
                    ["CON_DAMAGE"] = 0,
                    ["INT_DAMAGE"] = 0,
                    ["WIS_DAMAGE"] = 0,
                    ["CHA_DAMAGE"] = 0,

                    ["SAVE_BONUS"] = 0,
                    ["FORT_BONUS"] = 0,
                    ["REF_BONUS"] = 0,
                    ["WILL_BONUS"] = 0,
                   
                    ["SPEED"] = 0,
                    ["SPEED_BURROW"] = 0,
                    ["SPEED_CLIMB"] = 0,
                    ["SPEED_FLY"] = 0,
                    ["SPEED_SWIM"] = 0,

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

					["D_STR"] = "STR - (STR_DAMAGE / 2)",
					["D_DEX"] = "DEX - (DEX_DAMAGE / 2)",
					["D_CON"] = "CON - (CON_DAMAGE / 2)",
					["D_INT"] = "INT - (INT_DAMAGE / 2)",
					["D_WIS"] = "WIS - (WIS_DAMAGE / 2)",
					["D_CHA"] = "CHA - (CHA_DAMAGE / 2)",

					["FORT"] = "FORT_BONUS + SAVE_BONUS + CON",
                    ["REF"] = "REF_BONUS + SAVE_BONUS + DEX",
                    ["WILL"] = "WILL_BONUS + SAVE_BONUS + WIS",

                    ["INIT"] = "INIT_BONUS + DEX",

                    ["MAXDEX"] = "max(0, AC_MAXDEX)",
                    ["AC"] = "10 + AC_BONUS + min(DEX, MAXDEX) + SIZE_MOD",
                    ["TOUCH"] = "AC - ((AC_BONUS $ ARMOR) + (AC_BONUS $ SHIELD) + (AC_BONUS $ NATURAL))",
                    ["FLAT"] = "AC - ((AC_BONUS $ DODGE) + DEX)",

                    ["CMB"] = "BAB + STR - SIZE_MOD",
                    ["CMD"] = "10 + BAB + STR + DEX + CMD_BONUS + ((AC_BONUS $ CIRCUMSTANCE) + (AC_BONUS $ DEFLECTION) + (AC_BONUS $ DODGE) + (AC_BONUS $ INSIGHT) + (AC_BONUS $ LUCK) + (AC_BONUS $ MORALE) + (AC_BONUS $ PROFANE) + (AC_BONUS $ SACRED)) - SIZE_MOD",
					
                    ["ATK"] = "BAB + SIZE_MOD",

					["A_STR"] = "D_STR + ATK",
					["A_DEX"] = "D_DEX + ATK",
					["A_CON"] = "D_CON + ATK",
					["A_INT"] = "D_INT + ATK",
					["A_WIS"] = "D_WIS + ATK",
					["A_CHA"] = "D_CHA + ATK",


					//skills
					["ACR"] = "D_DEX + SK_ALL + SK_ACR + AC_PENALTY",
                    ["APR"] = "D_INT + SK_ALL + SK_APR",
                    ["BLF"] = "D_CHA + SK_ALL + SK_BLF",
                    ["CLM"] = "D_STR + SK_ALL + SK_CLM + AC_PENALTY",
                    ["DIP"] = "D_CHA + SK_ALL + SK_DIP",
                    ["DSA"] = "D_DEX + SK_ALL + SK_DSA + AC_PENALTY",
                    ["DSG"] = "D_CHA + SK_ALL + SK_DSG",
                    ["ESC"] = "D_DEX + SK_ALL + SK_ESC + AC_PENALTY",
                    ["FLY"] = "D_DEX + SK_ALL + SK_FLY + AC_PENALTY + SIZE_SKL",
                    ["HND"] = "D_DEX + SK_ALL + SK_HND",
                    ["HEA"] = "D_WIS + SK_ALL + SK_HEA",
                    ["ITM"] = "D_CHA + SK_ALL + SK_ITM",
                    ["LNG"] = "D_INT + SK_ALL + SK_LNG",
                    ["PRC"] = "D_WIS + SK_ALL + SK_PRC",
                    ["RDE"] = "D_DEX + SK_ALL + SK_RDE + AC_PENALTY",
                    ["SNS"] = "D_WIS + SK_ALL + SK_SNS",
                    ["SLT"] = "D_DEX + SK_ALL + SK_SLT + AC_PENALTY",
                    ["SPL"] = "D_INT + SK_ALL + SK_SPL",
                    ["STL"] = "D_DEX + SK_ALL + SK_STL + AC_PENALTY + (SIZE_SKL * 2)",
                    ["SUR"] = "D_WIS + SK_ALL + SK_SUR",
                    ["SWM"] = "D_STR + SK_ALL + SK_SWM + AC_PENALTY",
                    ["UMD"] = "D_CHA + SK_ALL + SK_UMD",

                    ["ARC"] = "D_INT + SK_ALL + SK_ARC",
                    ["DUN"] = "D_INT + SK_ALL + SK_DUN",
                    ["ENG"] = "D_INT + SK_ALL + SK_ENG",
                    ["GEO"] = "D_INT + SK_ALL + SK_GEO",
                    ["HIS"] = "D_INT + SK_ALL + SK_HIS",
                    ["LCL"] = "D_INT + SK_ALL + SK_LCL",
                    ["NTR"] = "D_INT + SK_ALL + SK_NTR",
                    ["NBL"] = "D_INT + SK_ALL + SK_NBL",
                    ["PLN"] = "D_INT + SK_ALL + SK_PLN",
                    ["RLG"] = "D_INT + SK_ALL + SK_RLG",

                },

                ExprRows = new Dictionary<string, ExprRow>(),
            };

            return stats;
        }
    }
}












