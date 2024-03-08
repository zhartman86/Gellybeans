using Gellybeans.Expressions;
using System.Text;

namespace Gellybeans.Pathfinder
{
    public class Stat : IEval
    {

        public int Base { get; set; } = 0;
        public int Bonus { get { return GetTotal(); } }
        public List<Bonus> Bonuses { get; set; } = null;
        public Bonus Override { get; set; } = null;
        public int Value
        {
            get
            {
                if(Override is null) return Base + Bonus;
                return Override.Value;
            }
        }

        public Stat() { }

        public Stat(int baseValue) => 
            Base = baseValue;

        public string Display()
        {
            var str = $"## {Value}";
            if(Bonuses != null || Override != null)
            {
                var stb = new StringBuilder();
                stb.AppendLine(str);
                if(Override != null)
                {
                    stb.AppendLine($"~~**Base:**~~ {Base}");
                    stb.AppendLine($"**Override:** {Override.Value}");
                }
                else
                    stb.AppendLine($"**Base:** {Base}");

                if(Bonuses != null)
                    for(int i = 0; i < Bonuses.Count; i++)
                        stb.AppendLine($"- {Bonuses[i]}");

                return stb.ToString();
            }
            return str;
        }

        public dynamic Eval(IContext ctx, StringBuilder sb) =>
            Value;



        public int GetBonus(BonusType type)
        {
            if(Bonuses == null) 
                return 0;

            if(Bonuses.Count > 1)
                Bonuses.Sort((x, y) => y.Value.CompareTo(x.Value));

            if(Bonuses.Any(x => x.Type == type))
                return Bonuses.First(x => x.Type == type).Value;
            return 0;
        }

        private int GetTotal()
        {
            if(Bonuses != null)
            {
                Dictionary<BonusType, List<Bonus>> dict = new Dictionary<BonusType, List<Bonus>>();
                foreach(Bonus b in Bonuses)
                {
                    if(!dict.ContainsKey(b.Type)) dict[b.Type] = new List<Bonus>();
                    dict[b.Type].Add(b);

                    //this is imporant. sort the highest bonus to the top so that checking for bonuses that don't stack is easier.
                    dict[b.Type].Sort((x, y) => y.Value.CompareTo(x.Value));
                }
                return GetTotal(dict);
            }
            return 0;
        }


        private static int GetTotal(Dictionary<BonusType, List<Bonus>> bonuses)
        {
            int total = 0;
            foreach(var bonusList in bonuses.Values)
            {
                if(bonusList[0].Type == BonusType.Typeless || bonusList[0].Type == BonusType.Circumstance || bonusList[0].Type == BonusType.Dodge)
                {
                    var list = new List<string>();
                    for(int i = 0; i < bonusList.Count; i++)
                    {
                        //ignore effects with identical names, else stack.   
                        if(list.Contains(bonusList[i].Name)) continue;

                        list.Add(bonusList[i].Name);
                        total += bonusList[i].Value;
                    }
                }
                else if(bonusList[0].Type == BonusType.Penalty)
                {
                    for(int i = 0; i < bonusList.Count; i++)
                        total += bonusList[i].Value;
                }
                else
                {
                    total += bonusList[0].Value;
                }
            }
            return total;
        }

        public bool AddBonus(Bonus b)
        {
            if(b.Type == BonusType.Empty || b.Value == 0) 
                return false;

            Bonuses ??= new List<Bonus>();
            
            if(b.Type == BonusType.Override)
                Override = b;
            else
                Bonuses.Add(b);
            
            return true;
        }

        public bool RemoveBonus(Bonus b)
        {
            if(Bonuses.Remove(b))
            {
                if(Bonuses.Count == 0)
                    Bonuses = null!;
                return true;
            }


            return false;
        }

        public bool RemoveBonus(string bonusName)
        {
            if(Bonuses == null) 
                return false;

            var bonusToUpper = bonusName.ToUpper();

            if(Override is not null)
            {
                if(Override.Name == bonusToUpper)
                    Override = null!;
            }

            var count = Bonuses.RemoveAll(x => x.Name == bonusToUpper);

            if(Bonuses.Count == 0)
                Bonuses = null!;

            return count > 0;
        }

        public override string ToString() =>
            Value.ToString();

        public static implicit operator int(Stat stat) => stat.Value;
        public static implicit operator Stat(int value) => new(value);

        public static int operator +(Stat lhs, Stat rhs) =>
            lhs.Value + rhs.Value;
        public static int operator -(Stat lhs, Stat rhs) =>
            lhs.Value - rhs.Value;
        public static int operator *(Stat lhs, Stat rhs) =>
            lhs.Value * rhs.Value;
        public static int operator /(Stat lhs, Stat rhs) =>
            lhs.Value / rhs.Value;
        public static int operator %(Stat lhs, Stat rhs) =>
            lhs.Value % rhs.Value;

        public static int operator +(Stat lhs, int rhs) =>
            lhs.Value + rhs;
        public static int operator -(Stat lhs, int rhs) =>
            lhs.Value - rhs;
        public static int operator *(Stat lhs, int rhs) =>
            lhs.Value * rhs;
        public static int operator /(Stat lhs, int rhs) =>
            lhs.Value / rhs;
        public static int operator %(Stat lhs, int rhs) =>
            lhs.Value % rhs;
        
        public static int operator +(int lhs, Stat rhs) =>
            lhs + rhs.Value;
        public static int operator -(int lhs, Stat rhs) =>
            lhs - rhs.Value;
        public static int operator *(int lhs, Stat rhs) =>
            lhs * rhs.Value;
        public static int operator /(int lhs, Stat rhs) =>
            lhs / rhs.Value;
        public static int operator %(int lhs, Stat rhs) =>
            lhs % rhs.Value;

        public static bool operator ==(Stat lhs, Stat rhs) =>
            lhs.Value == rhs.Value;
        public static bool operator !=(Stat lhs, Stat rhs) =>
            lhs.Value != rhs.Value;

        public static Stat operator +(Stat lhs, Bonus rhs)
        {
            lhs.AddBonus(rhs);
            return lhs;
        }

        public static Stat operator -(Stat lhs, Bonus rhs)
        {
            lhs.RemoveBonus(rhs.Name);
            return lhs;
        }

        //public static string operator +(Stat lhs)

        public override bool Equals(object? obj)
        {
            if(obj != null && obj is Stat s)
                return Value == s.Value;

            return false;
        }
        public override int GetHashCode() =>
            Value;

       


        //public static Stat operator +(Stat lhs, Stat rhs)
        //{
        //    if( rhs.Bonuses != null)
        //        lhs.Bonuses?.AddRange( rhs.Bonuses);

        //    var stat = new Stat() { Base = lhs.Base + rhs.Base, Bonuses = lhs.Bonuses ?? ( rhs.Bonuses ?? null!) };
        //    return stat;
        //}
    }
}