namespace Gellybeans.Pathfinder
{
    public class Stat
    {
        public int      Value       { get { return Base + Bonus; } }
        public int      Base        { get; set; } = 0;
        public int      Bonus       { get; set; } = 0;

        public List<Bonus> Bonuses { get; set; } = new List<Bonus>();


        public static implicit operator int(Stat stat)  => stat.Value;
        public static implicit operator Stat(int value) => new Stat { Base = value };

        private void GetTotal()
        {
            Dictionary<BonusType, List<Bonus>> dict = new Dictionary<BonusType, List<Bonus>>();
            foreach(Bonus b in Bonuses)
            {
                if(!dict.ContainsKey(b.Type)) dict[b.Type] = new List<Bonus>();
                dict[b.Type].Add(b);
                
                //this is imporant. sort the highest bonus to the top—so that checking for bonuses that don't stack is easier.
                dict[b.Type].Sort((x, y) => y.Value.CompareTo(x.Value));
            }
            Bonus = GetTotal(dict);
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
            Bonuses.Add(b);
            GetTotal();
            return b;
        }

        public bool RemoveBonus(Bonus b)
        {
            if(Bonuses.Remove(b))
            {
                GetTotal();
                return true;
            }
            GetTotal();
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
            GetTotal();

            return count;
        }
    }
}