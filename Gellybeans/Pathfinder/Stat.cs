namespace Gellybeans.Pathfinder
{
    public class Stat
    {
        
        public int      Base        { get; set; } = 0;
        public int      Bonus       { get { return GetTotal(); } }
        public int      Value       { get { return Base + Bonus; } }

        public List<Bonus> Bonuses { get; set; } = new List<Bonus>();


        public static implicit operator int(Stat stat)  => stat.Value;
        public static implicit operator Stat(int value) => new Stat { Base = value };

        public static Stat operator +(Stat a, Stat b)
        {
            var stat = new Stat() { Base = a.Base + b.Base };
            a.Bonuses.AddRange(b.Bonuses);
            return stat;
        }

        public int GetBonus(BonusType type)
        {
            Bonuses.Sort((x, y) => y.Value.CompareTo(x.Value));
            
            if(Bonuses.Any(x => x.Type == type))
                return Bonuses.First(x => x.Type == type).Value;
            return 0;
        }

        private int GetTotal()
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
        
        
        private int GetTotal(Dictionary<BonusType, List<Bonus>> bonuses)
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
                else
                {
                    total += bonusList[0].Value;
                }
            }
            return total;
        }

        public Bonus AddBonus(Bonus b)
        {
            if(b.Value == 0)
                return null;

            Bonuses.Add(b);
            return b;
        }

        public bool RemoveBonus(Bonus b)
        {
            if(Bonuses.Remove(b))
            {
                return true;
            }
            return false;
        }
    
        public int RemoveBonus(string bonusName)
        {
            int count = 0;
            var bonuses = new List<Bonus>();
            
            for(int i = 0; i < Bonuses.Count; i++)
            {            
                if(Bonuses[i].Name == bonusName)
                {
                    count++;
                    bonuses.Add(Bonuses[i]);
                }                         
            }
            foreach(Bonus b in bonuses) RemoveBonus(b);
            return count;
        }
    }
}