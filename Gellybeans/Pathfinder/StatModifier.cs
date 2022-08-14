namespace Gellybeans.Pathfinder
{       
    public class StatModifier
    {
        public string   StatName    { get; set; }
        public Bonus    Bonus       { get; set; }


        public static Dictionary<string, List<StatModifier>> Buffs = new Dictionary<string, List<StatModifier>>()
        {
            {
                "BULLS_STR", new List<StatModifier>()
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
            },
            {
                "INSPIRE_COURAGE_1", new List<StatModifier>()
                {
                    new StatModifier()
                    {
                        StatName = "ATK_BONUS",
                        Bonus = new Bonus()
                        {
                            Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 1 
                        }
                    },
                    new StatModifier()
                    {
                        StatName = "WPN_DMG",
                        Bonus = new Bonus()
                        {
                            Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 1
                        }
                    }
                }
            },
            {
                "INSPIRE_COURAGE_2", new List<StatModifier>()
                {
                    new StatModifier()
                    {
                        StatName = "ATK_BONUS",
                        Bonus = new Bonus()
                        {
                            Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 2
                        }
                    },
                    new StatModifier()
                    {
                        StatName = "WPN_DMG",
                        Bonus = new Bonus()
                        {
                            Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 2
                        }
                    }
                }
            }
              
        };
    }
}
