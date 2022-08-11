namespace Gellybeans.Pathfinder
{
    public class Buff
    {
        public string               Name { get; set; }
        public List<StatModifier> Mods { get; set; }


        public static Dictionary<string, Buff> Buffs = new Dictionary<string, Buff>()
        {
            {
                "BULLS_STR", new Buff()
                {
                    Name = "BULLS_STR",
                    Mods = new List<StatModifier>
                    {
                        { 
                            new StatModifier()
                            {
                                StatName = "STR_TEMP",
                                Bonus = new Bonus()
                                {
                                    Name = "BULLS_STR", Type = BonusType.Enhancement, Value = 4
                                } 
                            } 
                        } 
                    } 
            
                } 
                
            } 
        };
    }

}
