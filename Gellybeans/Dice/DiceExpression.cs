using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Gellybeans.Dice
{ 
    public class DiceExpression
    {
        private static readonly Regex pattern = new Regex(@"^ ( (?<Sets> \d{0,3} [dD] \d{1,3}) \+? )+  
                                                              ( (?<ModOp>[\+\-]) (?<ModVal>\d{1,4}))? $", RegexOptions.IgnorePatternWhitespace);

        public List<DieSet> Sets    { get; set; } = new List<DieSet>();
        public DiceModifier? Mod    { get; set; }

        public RollResult Results   { get; set; } = new RollResult();
        
        public int TotalResult
        {
            get { return Results.DiceTotal + (Mod != null ? Mod.Value : 0); }
        }


        public DiceExpression(List<DieSet> sets, DiceModifier? mod)
        {
            Sets = sets;
            Mod = mod;
        }
        public DiceExpression(string expr)
        {
            var match = pattern.Match(expr);
            if(match.Success)
            {
                var sets = match.Groups["Sets"];              
                for(int i = 0; i < sets.Captures.Count; i++)
                {
                    var dieSet = new DieSet(sets.Captures[i].Value);
                    Sets.Add(dieSet);
                    Results.Add(dieSet.Roll());
                }

                var modop   = match.Groups["ModOp"];
                var modval  = match.Groups["ModVal"].Value;
                if(modop.Success) Mod = new DiceModifier(modop.Value[0], int.Parse(modval));
            }
            
            else
            {
                Results = null;
            }
        }       

        public override string ToString()
        {
            string s = "";
            for(int i = 0; i < Sets.Count; i++)
            {
                s += Sets[i].ToString();
                if(i != Sets.Count - 1) s += "+";
                
            }
            if(Mod != null) s += Mod.ToString();
            return s;
        }
    
    
    }


}
