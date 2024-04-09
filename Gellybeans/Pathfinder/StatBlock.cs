using Gellybeans.Expressions;
using System.Text;

namespace Gellybeans.Pathfinder
{
    public class StatBlock : IContext
    {
        IContext parent = null!;
        public IContext Parent {  get { return parent; } }
        
        public IContext Global { get {  return parent; } set { parent = value; } }

        public Guid Id          { get; set; }
        public Guid CampaignId  { get; set; }     
        
        public ulong Owner { get; set; }

        public event EventHandler<string>? ValueChanged;
        public void OnValueChanged(string varChanged) { ValueChanged?.Invoke(this, varChanged); }

        public string CharacterName { get; set; } = "Name me";
        
        public Dictionary<string, ExprRow>  ExprRows    { get; private set; } = new Dictionary<string, ExprRow>();

        public Dictionary<string, dynamic> Vars { get; private set; } = new Dictionary<string, dynamic>();
        public Dictionary<string, dynamic> Constants { get; private set; } = new Dictionary<string, dynamic>()
        {
            ["TRUE"] = true,
            ["FALSE"] = false,


            //bonus types
            ["EMPTY"] = -1,
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
            ["OVERRIDE"] = 21,

            ["ORCUS"] = 666,
        };
          
        public dynamic this[string varName]
        {
            get
            {
                if(TryGetVar(varName, out var v))
                    return v;
                
                return null!;
            }
            set
            {
                if(Constants.ContainsKey(varName))
                    return;
                
                Vars[varName] = value;
                OnValueChanged("var");
            }
        }

        public void SetVars(Dictionary<string, dynamic> vars)
        {
            Vars = vars;
            OnValueChanged("var");
        }
            

        public bool TryGetVar(string varName, out dynamic value)
        {
            if(Constants.ContainsKey(varName))
            {
                value = Constants[varName];
                return true;
            }
            else if(Vars.ContainsKey(varName))
            {
                value = Vars[varName];
                return true;
            }
            else if(parent != null && parent.TryGetVar(varName, out var v))
            {
                value = v;
                return true;
            }

            else value = null!;
            return false;
        }


        public bool RemoveVar(string statName)
        {
            if(Vars.Remove(statName))
            {
                OnValueChanged($"var");
                return true;
            }
            return false;
        }
    
        public bool TryElevateVar(string identifier, dynamic value)
        {
            if(parent  != null) 
            {
                parent[identifier] = value;
                return true;
            }
            return false;
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
    

        public void ClearBonus(string bonusName)
        {
            var bonusToUpper = bonusName.ToUpper();
            foreach(var var in Vars.Where(x => x.Value is Stat))
                ((Stat)var.Value).RemoveBonus(bonusToUpper);
            OnValueChanged("var");
        }

        public int ClearBonuses()
        {
            foreach(var var in Vars.OfType<Stat>())
            {
                ((Stat)var.Value).Override = null!;
                ((Stat)var.Value).Bonuses.Clear();
            }
               
            OnValueChanged("var");
            return 1;
        }
     
        public dynamic GetVar(string identifier, StringBuilder sb)
        {
            identifier = identifier.Replace(" ", "_").ToUpper();
            if(Vars.TryGetValue(identifier, out var node)) 
                return node;

            sb.AppendLine($"{identifier} not found.");

            return null;
        }

        public static StatBlock DefaultPathfinder(string name)
        {
            var stats = new StatBlock()
            {
                CharacterName = name,

                Vars = new Dictionary<string, dynamic>()
                {
                    
                    //strings
                    ["NAME"]    = new StringValue("NAME ME"),
                    ["LEVELS"]  = new StringValue(""),
                    ["DEITY"]   = new StringValue(""),
                    ["HOME"]    = new StringValue(""),
                    ["GENDER"]  = new StringValue(""),
                    ["HAIR"]    = new StringValue(""),
                    ["EYES"]    = new StringValue(""),
                    ["BIO"]     = new StringValue(""),


                    //stats
                    ["LEVEL"]           = new Stat(1),

                    ["SIZE_MOD"]        = new Stat(0),
                    ["SIZE_SKL"]        = new Stat(0),

                    ["HP_BASE"]         = new Stat(0),

                    ["STR_SCORE"]       = new Stat(10),
                    ["DEX_SCORE"]       = new Stat(10),
                    ["CON_SCORE"]       = new Stat(10),
                    ["INT_SCORE"]       = new Stat(10),
                    ["WIS_SCORE"]       = new Stat(10),
                    ["CHA_SCORE"]       = new Stat(10),


                    ["STR_DAMAGE"]      = new Stat(0),
                    ["DEX_DAMAGE"]      = new Stat(0),
                    ["CON_DAMAGE"]      = new Stat(0),
                    ["INT_DAMAGE"]      = new Stat(0),
                    ["WIS_DAMAGE"]      = new Stat(0),
                    ["CHA_DAMAGE"]      = new Stat(0),

                    ["SAVE_BONUS"]      = new Stat(0),
                    ["FORT_BONUS"]      = new Stat(0),
                    ["REF_BONUS"]       = new Stat(0),
                    ["WILL_BONUS"]      = new Stat(0),

                    ["SPEED"]           = new Stat(0),
                    ["SPEED_BURROW"]    = new Stat(0),
                    ["SPEED_CLIMB"]     = new Stat(0),
                    ["SPEED_FLY"]       = new Stat(0),
                    ["SPEED_SWIM"]      = new Stat(0),

                    ["BAB"]             = new Stat(0),

                    ["INIT_BONUS"]      = new Stat(0),

                    ["CMB_BONUS"]       = new Stat(0),
                    ["CMB_BULLRUSH"]    = new Stat(0),
                    ["CMB_DIRTY"]       = new Stat(0),
                    ["CMB_DISARM"]      = new Stat(0),
                    ["CMB_OVERRUN"]     = new Stat(0),
                    ["CMB_REPOSITION"]  = new Stat(0),
                    ["CMB_STEAL"]       = new Stat(0),
                    ["CMB_SUNDER"]      = new Stat(0),
                    ["CMB_TRIP"]        = new Stat(0),

                    ["CMD_BONUS"]       = new Stat(0),
                    ["CMD_BULLRUSH"]    = new Stat(0),
                    ["CMD_DIRTY"]       = new Stat(0),
                    ["CMD_DISARM"]      = new Stat(0),
                    ["CMD_OVERRUN"]     = new Stat(0),
                    ["CMD_REPOSITION"]  = new Stat(0),
                    ["CMD_STEAL"]       = new Stat(0),
                    ["CMD_SUNDER"]      = new Stat(0),
                    ["CMD_TRIP"]        = new Stat(0),

                    ["AC_BONUS"]        = new Stat(0),
                    ["AC_MAXDEX"]       = new Stat(99),
                    ["AC_PENALTY"]      = new Stat(0),

                    

                    ["CL_BONUS"]        = new Stat(0),
                  
                    ["SK_ACR"]          = new Stat(0),
                    ["SK_APR"]          = new Stat(0),
                    ["SK_BLF"]          = new Stat(0),
                    ["SK_CLM"]          = new Stat(0),
                    ["SK_DIP"]          = new Stat(0),
                    ["SK_DSA"]          = new Stat(0),
                    ["SK_DSG"]          = new Stat(0),
                    ["SK_ESC"]          = new Stat(0),
                    ["SK_FLY"]          = new Stat(0),
                    ["SK_HND"]          = new Stat(0),
                    ["SK_HEA"]          = new Stat(0),
                    ["SK_ITM"]          = new Stat(0),
                    ["SK_LNG"]          = new Stat(0),
                    ["SK_PRC"]          = new Stat(0),
                    ["SK_RDE"]          = new Stat(0),
                    ["SK_SNS"]          = new Stat(0),
                    ["SK_SLT"]          = new Stat(0),
                    ["SK_SPL"]          = new Stat(0),
                    ["SK_STL"]          = new Stat(0),
                    ["SK_SUR"]          = new Stat(0),
                    ["SK_SWM"]          = new Stat(0),
                    ["SK_UMD"]          = new Stat(0),
                    ["SK_ARC"]          = new Stat(0),
                    ["SK_DUN"]          = new Stat(0),
                    ["SK_ENG"]          = new Stat(0),
                    ["SK_GEO"]          = new Stat(0),
                    ["SK_HIS"]          = new Stat(0),
                    ["SK_LCL"]          = new Stat(0),
                    ["SK_NTR"]          = new Stat(0),
                    ["SK_NBL"]          = new Stat(0),
                    ["SK_PLN"]          = new Stat(0),
                    ["SK_RLG"]          = new Stat(0),
                    ["SK_ALL"]          = new Stat(0),

                    ["PP"]              = new Stat(0),
                    ["GP"]              = new Stat(0),
                    ["SP"]              = new Stat(0),
                    ["CP"]              = new Stat(0),

                    //expressions
                    ["HP"]              = new ExpressionValue("HP_BASE + (CON * LEVEL)"),

                    ["STR"]             = new ExpressionValue("mod(STR_SCORE)"),
                    ["DEX"]             = new ExpressionValue("mod(DEX_SCORE)"),
                    ["CON"]             = new ExpressionValue("mod(CON_SCORE)"),
                    ["INT"]             = new ExpressionValue("mod(INT_SCORE)"),
                    ["WIS"]             = new ExpressionValue("mod(WIS_SCORE)"),
                    ["CHA"]             = new ExpressionValue("mod(CHA_SCORE)"),

                    ["D_STR"]           = new ExpressionValue("STR - (STR_DAMAGE / 2)"),
                    ["D_DEX"]           = new ExpressionValue("DEX - (DEX_DAMAGE / 2)"),
                    ["D_CON"]           = new ExpressionValue("CON - (CON_DAMAGE / 2)"),
                    ["D_INT"]           = new ExpressionValue("INT - (INT_DAMAGE / 2)"),
                    ["D_WIS"]           = new ExpressionValue("WIS - (WIS_DAMAGE / 2)"),
                    ["D_CHA"]           = new ExpressionValue("CHA - (CHA_DAMAGE / 2)"),

                    ["FORT"]            = new ExpressionValue("FORT_BONUS + SAVE_BONUS + D_CON"),
                    ["REF"]             = new ExpressionValue("REF_BONUS + SAVE_BONUS + D_DEX"),
                    ["WILL"]            = new ExpressionValue("WILL_BONUS + SAVE_BONUS + D_WIS"),

                    ["INIT"]            = new ExpressionValue("INIT_BONUS + DEX"),

                    ["MAXDEX"]          = new ExpressionValue("max(0, AC_MAXDEX)"),
                    ["AC"]              = new ExpressionValue("10 + AC_BONUS + min(D_DEX, max(0, AC_MAXDEX)) + SIZE_MOD"),
                    ["TOUCH"]           = new ExpressionValue("AC - ((AC_BONUS $? ARMOR) + (AC_BONUS $? SHIELD) + (AC_BONUS $? NATURAL))"),
                    ["FLAT"]            = new ExpressionValue("AC - ((AC_BONUS $? DODGE) + D_DEX)"),

                    ["CMB"]             = new ExpressionValue("BAB + STR - SIZE_MOD"),
                    ["CMD"]             = new ExpressionValue("10 + BAB + D_STR + D_DEX + CMD_BONUS + ((AC_BONUS $? CIRCUMSTANCE) + (AC_BONUS $? DEFLECTION) + (AC_BONUS $? DODGE) + (AC_BONUS $? INSIGHT) + (AC_BONUS $? LUCK) + (AC_BONUS $? MORALE) + (AC_BONUS $? PROFANE) + (AC_BONUS $? SACRED)) - SIZE_MOD"),

                    ["ATK"]             = new ExpressionValue("BAB + SIZE_MOD"),

                    ["A_STR"]           = new ExpressionValue("D_STR + ATK"),
                    ["A_DEX"]           = new ExpressionValue("D_DEX + ATK"),
                    ["A_CON"]           = new ExpressionValue("D_CON + ATK"),
                    ["A_INT"]           = new ExpressionValue("D_INT + ATK"),
                    ["A_WIS"]           = new ExpressionValue("D_WIS + ATK"),
                    ["A_CHA"]           = new ExpressionValue("D_CHA + ATK"),


                    //skills
                    ["ACR"]             = new ExpressionValue("D_DEX + SK_ALL + SK_ACR + AC_PENALTY"),
                    ["APR"]             = new ExpressionValue("D_INT + SK_ALL + SK_APR"),
                    ["BLF"]             = new ExpressionValue("D_CHA + SK_ALL + SK_BLF"),
                    ["CLM"]             = new ExpressionValue("D_STR + SK_ALL + SK_CLM + AC_PENALTY"),
                    ["DIP"]             = new ExpressionValue("D_CHA + SK_ALL + SK_DIP"),
                    ["DSA"]             = new ExpressionValue("D_DEX + SK_ALL + SK_DSA + AC_PENALTY"),
                    ["DSG"]             = new ExpressionValue("D_CHA + SK_ALL + SK_DSG"),
                    ["ESC"]             = new ExpressionValue("D_DEX + SK_ALL + SK_ESC + AC_PENALTY"),
                    ["FLY"]             = new ExpressionValue("D_DEX + SK_ALL + SK_FLY + AC_PENALTY + SIZE_SKL"),
                    ["HND"]             = new ExpressionValue("D_DEX + SK_ALL + SK_HND"),
                    ["HEA"]             = new ExpressionValue("D_WIS + SK_ALL + SK_HEA"),
                    ["ITM"]             = new ExpressionValue("D_CHA + SK_ALL + SK_ITM"),
                    ["LNG"]             = new ExpressionValue("D_INT + SK_ALL + SK_LNG"),
                    ["PRC"]             = new ExpressionValue("D_WIS + SK_ALL + SK_PRC"),
                    ["RDE"]             = new ExpressionValue("D_DEX + SK_ALL + SK_RDE + AC_PENALTY"),
                    ["SNS"]             = new ExpressionValue("D_WIS + SK_ALL + SK_SNS"),
                    ["SLT"]             = new ExpressionValue("D_DEX + SK_ALL + SK_SLT + AC_PENALTY"),
                    ["SPL"]             = new ExpressionValue("D_INT + SK_ALL + SK_SPL"),
                    ["STL"]             = new ExpressionValue("D_DEX + SK_ALL + SK_STL + AC_PENALTY + (SIZE_SKL * 2)"),
                    ["SUR"]             = new ExpressionValue("D_WIS + SK_ALL + SK_SUR"),
                    ["SWM"]             = new ExpressionValue("D_STR + SK_ALL + SK_SWM + AC_PENALTY"),
                    ["UMD"]             = new ExpressionValue("D_CHA + SK_ALL + SK_UMD"),

                    ["ARC"]             = new ExpressionValue("D_INT + SK_ALL + SK_ARC"),
                    ["DUN"]             = new ExpressionValue("D_INT + SK_ALL + SK_DUN"),
                    ["ENG"]             = new ExpressionValue("D_INT + SK_ALL + SK_ENG"),
                    ["GEO"]             = new ExpressionValue("D_INT + SK_ALL + SK_GEO"),
                    ["HIS"]             = new ExpressionValue("D_INT + SK_ALL + SK_HIS"),
                    ["LCL"]             = new ExpressionValue("D_INT + SK_ALL + SK_LCL"),
                    ["NTR"]             = new ExpressionValue("D_INT + SK_ALL + SK_NTR"),
                    ["NBL"]             = new ExpressionValue("D_INT + SK_ALL + SK_NBL"),
                    ["PLN"]             = new ExpressionValue("D_INT + SK_ALL + SK_PLN"),
                    ["RLG"]             = new ExpressionValue("D_INT + SK_ALL + SK_RLG"),

                },

                ExprRows = new Dictionary<string, ExprRow>(),
            };

            return stats;
        }
    }
}












