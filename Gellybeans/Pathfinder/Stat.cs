using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Gellybeans.Pathfinder
{
    public class Stat
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





        public int GetBonus(BonusType type)
        {
            if(Bonuses is null) return 0;

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

        public Bonus AddBonus(Bonus b)
        {
            Bonuses ??= new List<Bonus>();
            if(b.Type == BonusType.Base)
            {
                Override = b;
                return b;
            }

            if(b.Value == 0)
                return null!;

            Bonuses.Add(b);
            return b;
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

        public int RemoveBonus(string bonusName)
        {
            if(Bonuses == null) return 0;

            var bonusToUpper = bonusName.ToUpper();
            int count = 0;
            var bonuses = new List<Bonus>();

            if(Override is not null)
            {
                if(Override.Name == bonusToUpper)
                {
                    Override = null;
                }
            }

            for(int i = 0; i < Bonuses.Count; i++)
            {
                if(Bonuses[i].Name == bonusToUpper)
                {
                    count++;
                    bonuses.Add(Bonuses[i]);
                }
            }
            foreach(Bonus b in bonuses) RemoveBonus(b);
            return count;
        }

        public override string ToString()
        {
            StringBuilder sb = null;

            if(Override != null)
                return $"{Override.Value} (override)";

            if(Bonuses.Count > 0)
            {
                sb = new StringBuilder();
                for(int i = 0; i < Bonuses.Count; i++)
                    sb.Append($"{Bonuses[i].Value} {Enum.GetName(typeof(BonusType), Bonuses[i].Type)},");
            }

            return $"{Value} {(Bonus > 0 ? $"({sb.ToString().TrimEnd(',').ToLower()})" : "")}";
        }

        public static implicit operator int(Stat stat) => stat.Value;
        public static implicit operator Stat(int value) => new Stat { Base = value };

        public static Stat operator +(Stat a, Stat b)
        {
            if(b.Bonuses != null)
                a.Bonuses?.AddRange(b.Bonuses);

            var stat = new Stat() { Base = a.Base + b.Base, Bonuses = a.Bonuses ?? (b.Bonuses ?? null!) };
            return stat;
        }

        public static Stat operator -(Stat a, Stat b)
        {
            if(b.Bonuses != null)
                a.Bonuses?.AddRange(b.Bonuses);

            var stat = new Stat() { Base = a.Base - b.Base, Bonuses = a.Bonuses ?? (b.Bonuses ?? null!) };
            return stat;
        }

        public static Stat operator *(Stat a, Stat b)
        {
            if(b.Bonuses != null)
                a.Bonuses?.AddRange(b.Bonuses);

            var stat = new Stat() { Base = a.Base * b.Base, Bonuses = a.Bonuses ?? (b.Bonuses ?? null!) };
            return stat;
        }

        public static Stat operator /(Stat a, Stat b)
        {
            if(b.Bonuses != null)
                a.Bonuses?.AddRange(b.Bonuses);

            var stat = new Stat() { Base = a.Base / b.Base, Bonuses = a.Bonuses ?? (b.Bonuses ?? null!) };
            return stat;
        }

        public static Stat operator %(Stat a, Stat b)
        {
            if(b.Bonuses != null)
                a.Bonuses?.AddRange(b.Bonuses);

            var stat = new Stat() { Base = a.Base % b.Base, Bonuses = a.Bonuses ?? (b.Bonuses ?? null!) };
            return stat;
        }
    }
}