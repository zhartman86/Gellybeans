using System.Text.RegularExpressions;

namespace Gellybeans.Dice
{
    public class DieSet
    {
        private static readonly Regex pattern = new Regex(@"^ (?<Count> (?:[1-9][0-9]{0,2})?) [dD] 
                                                              (?<Sides> (?:[1-9][0-9]{0,2})) $", RegexOptions.IgnorePatternWhitespace);

        public readonly int Count;
        public readonly int Sides;

        int[] results;

        public DieSet(int diceCount, int sideCount)
        {
            Count = diceCount;
            Sides = sideCount;
            results = new int[diceCount < 1 ? 1 : diceCount];
        }        
        
        public DieSet(string expr)
        {
            var set = Parse(expr);
            Count = set.Count;
            Sides = set.Sides;
            results = new int[set.Count];
        }


        public int[] Roll()
        {
            Random rand = new Random();
            for(int i = 0; i < Count; i++)
            {
                results[i] = rand.Next(1, Sides + 1);
            }
            return results;
        }

        public static DieSet Parse(string expr)
        {
            var match = pattern.Match(expr);
            if(match.Success)
            {
                return new DieSet(int.Parse(match.Groups["Count"].Value), int.Parse(match.Groups["Sides"].Value));
            }
            return Empty();
        }

        public override string ToString() { return Count + "d" + Sides; }
        
        public static DieSet Empty() { return new DieSet(0, 0); }
    
        public static bool operator ==(DieSet ds1, DieSet ds2) { return ds1.Count == ds2.Count && ds1.Sides == ds2.Sides; }
        public static bool operator !=(DieSet ds1, DieSet ds2) { return ds1.Count != ds2.Count || ds1.Sides != ds2.Sides; }

        public bool Equals(DieSet set) { return this == set; }
        public override bool Equals(object obj)
        {
            if(obj.GetType() != typeof(DieSet)) return false;
            else return Equals((DieSet)obj);
        }     
    }

   
        
    
}
