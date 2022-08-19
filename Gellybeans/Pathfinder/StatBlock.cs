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

        public Dictionary<string, Stat>     Stats       { get; private set; }   = new Dictionary<string, Stat>();    
        public Dictionary<string, string>   Expressions { get; private set; }   = new Dictionary<string, string>();
        public Dictionary<string, Attack>   Attacks     { get; private set; }   = new Dictionary<string, Attack>();
        public Dictionary<string, string>   Info        { get; private set; }   = new Dictionary<string, string>();
        
        public List<Item>                   Inventory   { get; set; } = new List<Item>();

        
     
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
            "mod"           => (args[0] - 10) / 2,
            "min"           => Math.Min(args[0], args[1]),
            "max"           => Math.Max(args[0], args[1]),
            "clamp"         => Math.Clamp(args[0], args[1], args[2]),
            "abs"           => Math.Abs(args[0]),
            "if"            => args[0] == 1 ? args[1] : 0,
            _               => 0
        };

        public int Resolve(string varName, StringBuilder sb)
        {
            if(Attacks.ContainsKey(varName)) return ResolveAttack(varName, sb);
            
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
            sb.AppendLine($"{statName} set to {Stats[toUpper].Base}");
            OnValueAssigned(statName);
            return Stats[toUpper].Base;          
        }

        int ResolveAttack(string attackName, StringBuilder sb)
        {
            if(Attacks.ContainsKey(attackName))
            {
                var attack = Attacks[attackName];
                var random = new Random();

                var roll = random.Next(1, attack.Sides + 1);
                var atkBonus = Parser.Parse(attack.ToHitExpr).Eval(this, sb);
                sb.AppendLine   ($"*Hit:* [{roll}] + {atkBonus} = {roll + atkBonus}");

                var holder = new StringBuilder();
                var damageExpr = Parser.Parse(attack.DamageExpr).Eval(this, holder);


                sb.AppendLine($"*Dmg Total:* {damageExpr} {holder}");
                sb.AppendLine();


                if(roll >= attack.CritRange)
                {
                    int conf = random.Next(1, attack.Sides + 1);
                    if(attack.Confirm) sb.AppendLine($"**CONFIRM:** [{conf}] + {atkBonus} = {conf + atkBonus}");

                    holder.Clear();
                    var critExpr = Parser.Parse(attack.CritExpr).Eval(this, holder);


                    sb.Append($"**CRIT:** {critExpr} {holder}");
                    sb.AppendLine();
                }
                
                return roll;
            }
            return 0;
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

                Attacks = new Dictionary<string, Attack>()
                {
                    { "$ATK_TEST", new Attack("ATK_TEST", 20,"ATK_M","2d6","4d6",17) }
                },

                Stats = new Dictionary<string, Stat>()
                {
                    ["LEVEL"] = 1,
                    
                    ["SIZE_MOD"] = 0,
                    ["SIZE_MOD_CM"] = 0,
                    ["SIZE_MOD_FLY"] = 0,
                    ["SIZE_MOD_STEALTH"] = 0,

                    ["HP_BASE"] = 0,
                    ["HP_TEMP"] = 0,
                    ["HP_DMG"] = 0,


                    ["STR_SCORE"] = 12,
                    ["DEX_SCORE"] = 15,
                    ["CON_SCORE"] = 16,
                    ["INT_SCORE"] = 18,
                    ["WIS_SCORE"] = 8,
                    ["CHA_SCORE"] = 6,

                    //since damage and temporary bonuses apply symmetrical effects, the same field can be used for both. neat. :)
                    ["STR_TEMP"] = 0,
                    ["DEX_TEMP"] = 0,
                    ["CON_TEMP"] = 0,
                    ["INT_TEMP"] = 0,
                    ["WIS_TEMP"] = 0,
                    ["CHA_TEMP"] = 0,

                    ["MOVE"]        = 0,

                    ["INITIATIVE"]  = 0,

                    ["AC_BONUS"] = 0,
                   

                    ["SAVE_FORT"]   = 0,
                    ["SAVE_REFLEX"] = 0,
                    ["SAVE_WILL"]   = 0,

                    ["BAB"] = 0,

                    ["ATK_BONUS"] = 0,
                    ["DMG_BONUS"] = 0,

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
                    ["ATK_M"]            = "BAB + STR + SIZE_MOD + (STR_TEMP / 2) + ATK_BONUS",
                    ["ATK_R"]            = "BAB + DEX + SIZE_MOD + (DEX_TEMP / 2) + ATK_BONUS",

                    
                    ["HP"]                  = "HP_BASE + (CON * LEVEL)",

                    ["STR"]                 = "mod(STR_SCORE)",
                    ["DEX"]                 = "mod(DEX_SCORE)",
                    ["CON"]                 = "mod(CON_SCORE)",
                    ["INT"]                 = "mod(INT_SCORE)",
                    ["WIS"]                 = "mod(WIS_SCORE)",
                    ["CHA"]                 = "mod(CHA_SCORE)",

                    ["FORT"]                = "1d20 + FORT_BASE + CON",
                    ["REFLEX"]              = "1d20 + REFLEX_BASE + DEX",
                    ["WILL"]                = "1d20 + WILL_BASE + WIS",

                    ["INIT"]                = "1d20 + INITIATIVE + DEX",

                    ["AC"]                  = "ARMOR_BONUS + min(DEX, AC_MAXDEX)",
                    ["AC_MAXDEX"]           = "99",
                    ["AC_PENALTY"]          = "0",


                    ["CMB"]                 = "1d20 + BAB + STR + SIZE_MOD",
                    ["CMD"]                 = "10 + BAB + STR + DEX + SIZE_MOD",

                    ["ACROBATICS"]          = "1d20 + DEX + SK_ACR + AC_PENALTY",
                    ["APPRAISE"]            = "1d20 + INT + SK_APR",
                    ["BLUFF"]               = "1d20 + CHA + SK_BLF",
                    ["CLIMB"]               = "1d20 + STR + SK_CLM + AC_PENALTY",
                    ["DIPLOMACY"]           = "1d20 + CHA + SK_DPL",
                    ["DISABLE"]             = "1d20 + DEX + SK_DEV",
                    ["DISGUISE"]            = "1d20 + CHA + SK_DSG",
                    ["ESCAPE"]              = "1d20 + DEX + SK_ESC + AC_PENALTY",
                    ["FLY"]                 = "1d20 + DEX + SK_FLY + AC_PENALTY" + "SIZE_MOD_FLY",
                    ["HANDLE"]              = "1d20 + DEX + SK_HAN",
                    ["HEAL"]                = "1d20 + WIS + SK_HEA",
                    ["INTIMIDATE"]          = "1d20 + CHA + SK_INT",
                    ["ARCANA"]              = "1d20 + INT + SK_ARC",
                    ["DUNGEONEERING"]       = "1d20 + INT + SK_DUN",
                    ["GEOGRAPHY"]           = "1d20 + INT + SK_GEO",
                    ["HISTORY"]             = "1d20 + INT + SK_HIS",
                    ["LOCAL"]               = "1d20 + INT + SK_LOC",
                    ["NATURE"]              = "1d20 + INT + SK_NAT",
                    ["NOBILITY"]            = "1d20 + INT + SK_NOB",
                    ["PLANES"]              = "1d20 + INT + SK_PLA",
                    ["RELIGION"]            = "1d20 + INT + SK_REL",
                    ["LINGUISTICS"]         = "1d20 + INT + SK_LNG",
                    ["PERCEPTION"]          = "1d20 + WIS + SK_PER",
                    ["RIDE"]                = "1d20 + DEX + SK_RDE + AC_PENALTY",
                    ["MOTIVE"]              = "1d20 + WIS + SK_SMO",
                    ["SLEIGHT"]             = "1d20 + DEX + SK_SLE + AC_PENALTY",
                    ["SPELLCRAFT"]          = "1d20 + INT + SK_SPL",
                    ["STEALTH"]             = "1d20 + DEX + SK_STL + AC_PENALTY" + "SIZE_MOD_STEALTH",
                    ["SURVIVAL"]            = "1d20 + WIS + SK_SUR",
                    ["SWIM"]                = "1d20 + STR + SK_SWM + AC_PENALTY",
                    ["UMD"]                 = "1d20 + CHA + SK_UMD",
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

                    ["STR_SCORE"] = 10,
                    ["DEX_SCORE"] = 10,
                    ["CON_SCORE"] = 10,
                    ["INT_SCORE"] = 10,
                    ["WIS_SCORE"] = 10,
                    ["CHA_SCORE"] = 10,

                    ["PROF"] = 0,

                    ["AC_BASE"] = 10,
                    ["AC_MAXDEX"] = 99,

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
                },

                Expressions = new Dictionary<string, string>()
                {
                    //skills
                    ["ACROBATICS"]      = "1d20 + DEX + if(PROF_ACROBATICS, PROF_BONUS)",
                    ["HANDLEANIMAL"]    = "1d20 + WIS + if(PROF_HANDLEANIMAL, PROF_BONUS)",
                    ["ARCANA"]          = "1d20 + INT",
                    ["ATHLETICS"]       = "1d20 + STR",
                    ["DECEPTION"]       = "1d20 + CHA",
                    ["HISTORY"]         = "1d20 + INT",
                    ["INSIGHT"]         = "1d20 + WIS",
                    ["INTIMIDATION"]    = "1d20 + CHA",
                    ["INVESTIGATION"]   = "1d20 + INT",
                    ["MEDICINE"]        = "1d20 + WIS",
                    ["NATURE"]          = "1d20 + INT",
                    ["PERCEPTION"]      = "1d20 + WIS",
                    ["PERFORM"]         = "1d20 + CHA",
                    ["PERSUASION"]      = "1d20 + CHA",
                    ["RELIGION"]        = "1d20 + INT",
                    ["SLEIGHT"]         = "1d20 + DEX",
                    ["STEALTH"]         = "1d20 + DEX",
                    ["SURVIVIAL"]       = "1d20 + WIS",

                    ["PASSIVE"] = "10 + WIS + if(PROF_PERCEPTION, PROF_BONUS)",

                    ["ATK_S"] = "1d20 + STR + PROF_BONUS + ATK_BONUS",
                    ["ATK_D"] = "1d20 + DEX + PROF_BONUS + ATK_BONUS",
                    ["ATK_I"] = "1d20 + INT + PROF_BONUS + ATK_BONUS",
                    ["ATK_W"] = "1d20 + WIS + PROF_BONUS + ATK_BONUS",
                    ["ATK_C"] = "1d20 + CHA + PROF_BONUS + ATK_BONUS",                  

                    ["STR"] = "mod(STR_SCORE)",
                    ["DEX"] = "mod(DEX_SCORE)",
                    ["CON"] = "mod(CON_SCORE)",
                    ["INT"] = "mod(INT_SCORE)",
                    ["WIS"] = "mod(WIS_SCORE)",
                    ["CHA"] = "mod(CHA_SCORE)",

                    ["SAVE_STR"] = "1d20 + STR + if(PROF_SAVE_STR, PROF_BONUS)", 
                    ["SAVE_DEX"] = "1d20 + DEX + if(PROF_SAVE_DEX, PROF_BONUS)",
                    ["SAVE_CON"] = "1d20 + CON + if(PROF_SAVE_CON, PROF_BONUS)",
                    ["SAVE_INT"] = "1d20 + INT + if(PROF_SAVE_INT, PROF_BONUS)",
                    ["SAVE_WIS"] = "1d20 + WIS + if(PROF_SAVE_WIS, PROF_BONUS)",
                    ["SAVE_CHA"] = "1d20 + CHA + if(PROF_SAVE_CHA, PROF_BONUS)",

                    ["INIT"]    = "1d20 + DEX",
                    ["AC"]      = "AC_BASE + min(DEX, AC_MAXDEX)",

                    
                    ["PROF_STR"] = "FALSE",
                    ["PROF_DEX"] = "FALSE",
                    ["PROF_CON"] = "FALSE",
                    ["PROF_INT"] = "FALSE",
                    ["PROF_WIS"] = "FALSE",
                    ["PROF_CHA"] = "FALSE",
                    ["PROF_ACROBATICS"] = "FALSE",
                    ["PROF_HANDLEANIMAL"] = "FALSE",
                    ["PROF_ARCANA"] = "FALSE",
                    ["PROF_ATHLETICS"] = "FALSE",
                    ["PROF_DECEPTION"] = "FALSE",
                    ["PROF_HISTORY"] = "FALSE",
                    ["PROF_INSIGHT"] = "FALSE",
                    ["PROF_INTIMIDATION"] = "FALSE",
                    ["PROF_INVESTIGATION"] = "FALSE",
                    ["PROF_MEDICINE"] = "FALSE",
                    ["PROF_NATURE"] = "FALSE",
                    ["PROF_PERCEPTION"] = "FALSE",
                    ["PROF_PERFORM"] = "FALSE",
                    ["PROF_PERSUASION"] = "FALSE",
                    ["PROF_RELIGION"] = "FALSE",
                    ["PROF_SLEIGHT"] = "FALSE",
                    ["PROF_STEALTH"] = "FALSE",
                    ["PROF_SURVIVIAL"] = "FALSE",
                }
            };

            return statBlock;
        }
    
    }
   
}

