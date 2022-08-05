using Gellybeans.Pathfinder;

namespace Gellybeans.RPG
{
    /// <summary>
    /// Provides a way to keep track of all buffs applied to a particular stat, while accounting for the various rules that apply to identical effects and non-stacking bonus types.
    /// 
    /// Any list of a particular type should keep its highest value at the 0 index. Ties shouldn't matter.
    /// </summary>
    public class Bonuses
    {
        public Dictionary<BonusType, List<Bonus>> bonuses = new Dictionary<BonusType, List<Bonus>>();        
        public int Total { get { return GetTotal(); } }

        private int GetTotal()
        {         
            int total = 0;
            foreach(List<Bonus> bonusList in bonuses.Values)
            {
                if(bonusList[0].Type.HasFlag(BonusType.Typeless)  || bonusList[0].Type.HasFlag(BonusType.Circumstance) || bonusList[0].Type.HasFlag(BonusType.Dodge))
                {                                      
                    for(int i = 0; i < bonusList.Count; i++)
                    {
                        //ignore effects with identical names, else stack.                                             
                        if(bonuses[bonusList[i].Type].FirstOrDefault(x => x.Name == bonusList[i].Name) != null) continue;
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
        
        public Bonus Add(Bonus b)
        {
            if(!bonuses.ContainsKey(b.Type)) bonuses[b.Type] = new List<Bonus>();          
            
            bonuses[b.Type].Add(b);
            bonuses[b.Type].Sort((x, y) => x.Value.CompareTo(y.Value));
            return b;
        }
    
        public bool Remove(Bonus b)
        {
            if(!bonuses.ContainsKey(b.Type) || !bonuses[b.Type].Remove(b))
            {
                //msg
                return false;
            }                              
            
            if(bonuses[b.Type].Count == 0)
            {
                bonuses.Remove(b.Type); 
            }
            
            return true;
        }
    
        
    }
}
