using Gellybeans.Expressions;
using System.Linq.Expressions;
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
        public void OnValueChanged(string varChanged) { ValueChanged?.Invoke(this, varChanged); }

        public string CharacterName { get; set; } = "Name me";
        
        public List<InvItem>                Inventory   { get; private set; } = new List<InvItem>();     
        public Dictionary<string, ExprRow>  ExprRows    { get; private set; } = new Dictionary<string, ExprRow>();

        public Dictionary<string, ValueNode> Vars { get; private set; } = new Dictionary<string, ValueNode>();

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



        public ValueNode this[string statName]
        {
            get
            {
                if(Vars.ContainsKey(statName))
                    return Vars[statName];
                return "Var not found.";
            }
            set
            {
                Vars[statName] = value;
                OnValueChanged($"stat:{statName}");
            }
        }




        public void RemoveVar(string statName)
        {
            if(Vars.Remove(statName))
                OnValueChanged($"stat:{statName}");
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
            foreach(var var in Vars.Where(x => x.Value.Value is Stat))
                ((Stat)var.Value).RemoveBonus(bonusToUpper);
            OnValueChanged("stats");
        }

        public int ClearBonuses()
        {
            foreach(var var in Vars.Where(x => x.Value.Value is Stat))
            {
                ((Stat)var.Value).Override = null!;
                ((Stat)var.Value).Bonuses.Clear();
            }
               
            OnValueChanged("stats");
            return 1;
        }
     
        public ExpressionNode GetVar(string identifier, StringBuilder sb)
        {
            identifier = identifier.Replace(" ", "_").ToUpper();
            if(Vars.TryGetValue(identifier, out var node)) 
                return node;

            return new StringNode($"{identifier} not found.", this, sb);
        }

        public static StatBlock DefaultPathfinder(string name)
        {
            var stats = new StatBlock()
            {
                CharacterName = name,

                Vars = new Dictionary<string, ValueNode>()
                {
                    
                    //strings
                    ["NAME"] = new ExpressionValue("NAME ME"),
                    ["LEVELS"] = new ExpressionValue(""),
                    ["DEITY"] = new ExpressionValue(""),
                    ["HOME"] = new ExpressionValue(""),
                    ["GENDER"] = new ExpressionValue(""),
                    ["HAIR"] = new ExpressionValue(""),
                    ["EYES"] = new ExpressionValue(""),
                    ["BIO"] = new ExpressionValue(""),


                    //stats
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
                    ["CP"] = 0,

                    //expressions
                    ["HP"] = new ExpressionValue("HP_BASE + (CON * LEVEL)"),

                    ["STR"] = new ExpressionValue("mod(STR_SCORE)"),
                    ["DEX"] = new ExpressionValue("mod(DEX_SCORE)"),
                    ["CON"] = new ExpressionValue("mod(CON_SCORE)"),
                    ["INT"] = new ExpressionValue("mod(INT_SCORE)"),
                    ["WIS"] = new ExpressionValue("mod(WIS_SCORE)"),
                    ["CHA"] = new ExpressionValue("mod(CHA_SCORE)"),

                    ["D_STR"] = new ExpressionValue("STR - (STR_DAMAGE / 2)"),
                    ["D_DEX"] = new ExpressionValue("DEX - (DEX_DAMAGE / 2)"),
                    ["D_CON"] = new ExpressionValue("CON - (CON_DAMAGE / 2)"),
                    ["D_INT"] = new ExpressionValue("INT - (INT_DAMAGE / 2)"),
                    ["D_WIS"] = new ExpressionValue("WIS - (WIS_DAMAGE / 2)"),
                    ["D_CHA"] = new ExpressionValue("CHA - (CHA_DAMAGE / 2)"),

                    ["FORT"] = new ExpressionValue("FORT_BONUS + SAVE_BONUS + D_CON"),
                    ["REF"] = new ExpressionValue("REF_BONUS + SAVE_BONUS + D_DEX"),
                    ["WILL"] = new ExpressionValue("WILL_BONUS + SAVE_BONUS + D_WIS"),

                    ["INIT"] = new ExpressionValue("INIT_BONUS + DEX"),

                    ["MAXDEX"] = new ExpressionValue("max(0, AC_MAXDEX)"),
                    ["AC"] = new ExpressionValue("10 + AC_BONUS + min(D_DEX, max(0, AC_MAXDEX)) + SIZE_MOD"),
                    ["TOUCH"] = new ExpressionValue("AC - ((AC_BONUS $? ARMOR) + (AC_BONUS $? SHIELD) + (AC_BONUS $? NATURAL))"),
                    ["FLAT"] = new ExpressionValue("AC - ((AC_BONUS $? DODGE) + D_DEX)"),

                    ["CMB"] = new ExpressionValue("BAB + STR - SIZE_MOD"),
                    ["CMD"] = new ExpressionValue("10 + BAB + STR + D_DEX + CMD_BONUS + ((AC_BONUS $? CIRCUMSTANCE) + (AC_BONUS $? DEFLECTION) + (AC_BONUS $? DODGE) + (AC_BONUS $? INSIGHT) + (AC_BONUS $? LUCK) + (AC_BONUS $? MORALE) + (AC_BONUS $? PROFANE) + (AC_BONUS $? SACRED)) - SIZE_MOD"),

                    ["ATK"] = new ExpressionValue("BAB + SIZE_MOD"),

                    ["A_STR"] = new ExpressionValue("D_STR + ATK"),
                    ["A_DEX"] = new ExpressionValue("D_DEX + ATK"),
                    ["A_CON"] = new ExpressionValue("D_CON + ATK"),
                    ["A_INT"] = new ExpressionValue("D_INT + ATK"),
                    ["A_WIS"] = new ExpressionValue("D_WIS + ATK"),
                    ["A_CHA"] = new ExpressionValue("D_CHA + ATK"),


                    //skills
                    ["ACR"] = new ExpressionValue("D_DEX + SK_ALL + SK_ACR + AC_PENALTY"),
                    ["APR"] = new ExpressionValue("D_INT + SK_ALL + SK_APR"),
                    ["BLF"] = new ExpressionValue("D_CHA + SK_ALL + SK_BLF"),
                    ["CLM"] = new ExpressionValue("D_STR + SK_ALL + SK_CLM + AC_PENALTY"),
                    ["DIP"] = new ExpressionValue("D_CHA + SK_ALL + SK_DIP"),
                    ["DSA"] = new ExpressionValue("D_DEX + SK_ALL + SK_DSA + AC_PENALTY"),
                    ["DSG"] = new ExpressionValue("D_CHA + SK_ALL + SK_DSG"),
                    ["ESC"] = new ExpressionValue("D_DEX + SK_ALL + SK_ESC + AC_PENALTY"),
                    ["FLY"] = new ExpressionValue("D_DEX + SK_ALL + SK_FLY + AC_PENALTY + SIZE_SKL"),
                    ["HND"] = new ExpressionValue("D_DEX + SK_ALL + SK_HND"),
                    ["HEA"] = new ExpressionValue("D_WIS + SK_ALL + SK_HEA"),
                    ["ITM"] = new ExpressionValue("D_CHA + SK_ALL + SK_ITM"),
                    ["LNG"] = new ExpressionValue("D_INT + SK_ALL + SK_LNG"),
                    ["PRC"] = new ExpressionValue("D_WIS + SK_ALL + SK_PRC"),
                    ["RDE"] = new ExpressionValue("D_DEX + SK_ALL + SK_RDE + AC_PENALTY"),
                    ["SNS"] = new ExpressionValue("D_WIS + SK_ALL + SK_SNS"),
                    ["SLT"] = new ExpressionValue("D_DEX + SK_ALL + SK_SLT + AC_PENALTY"),
                    ["SPL"] = new ExpressionValue("D_INT + SK_ALL + SK_SPL"),
                    ["STL"] = new ExpressionValue("D_DEX + SK_ALL + SK_STL + AC_PENALTY + (SIZE_SKL * 2)"),
                    ["SUR"] = new ExpressionValue("D_WIS + SK_ALL + SK_SUR"),
                    ["SWM"] = new ExpressionValue("D_STR + SK_ALL + SK_SWM + AC_PENALTY"),
                    ["UMD"] = new ExpressionValue("D_CHA + SK_ALL + SK_UMD"),

                    ["ARC"] = new ExpressionValue("D_INT + SK_ALL + SK_ARC"),
                    ["DUN"] = new ExpressionValue("D_INT + SK_ALL + SK_DUN"),
                    ["ENG"] = new ExpressionValue("D_INT + SK_ALL + SK_ENG"),
                    ["GEO"] = new ExpressionValue("D_INT + SK_ALL + SK_GEO"),
                    ["HIS"] = new ExpressionValue("D_INT + SK_ALL + SK_HIS"),
                    ["LCL"] = new ExpressionValue("D_INT + SK_ALL + SK_LCL"),
                    ["NTR"] = new ExpressionValue("D_INT + SK_ALL + SK_NTR"),
                    ["NBL"] = new ExpressionValue("D_INT + SK_ALL + SK_NBL"),
                    ["PLN"] = new ExpressionValue("D_INT + SK_ALL + SK_PLN"),
                    ["RLG"] = new ExpressionValue("D_INT + SK_ALL + SK_RLG"),

                },

                ExprRows = new Dictionary<string, ExprRow>(),
            };

            return stats;
        }
    }
}












