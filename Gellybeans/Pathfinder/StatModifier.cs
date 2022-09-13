namespace Gellybeans.Pathfinder
{
    public class StatModifier
    {
        public string           StatName    { get; set; }
        public Bonus            Bonus       { get; set; } = null;        
        
        public StatModifier(string statName) { StatName = statName; }

        public static Dictionary<string, List<StatModifier>> Mods = new Dictionary<string, List<StatModifier>>()
        {     
            ///animal growth
            { "ANIMAL_GROWTH_DIMINUTIVE", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Base, Value = 4        }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Base, Value = 6        }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Natural, Value = 2     }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Penalty, Value = -2    }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Size, Value = 4        }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Size, Value = 8        }}}},
            { "ANIMAL_GROWTH_TINY", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Base, Value = 2        }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Base, Value = 4        }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Natural, Value = 2     }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Penalty, Value = -2    }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Size, Value = 4        }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Size, Value = 8        }}}},
            { "ANIMAL_GROWTH_SMALL", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Base, Value = 1        }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Base, Value = 2        }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Natural, Value = 2     }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Penalty, Value = -2    }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Size, Value = 4        }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Size, Value = 8        }}}},
            { "ANIMAL_GROWTH_MEDIUM", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Base, Value = 0        }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Base, Value = 0        }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Natural, Value = 2     }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Penalty, Value = -2    }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Size, Value = 4        }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Size, Value = 8        }}}},
            { "ANIMAL_GROWTH_LARGE", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Base, Value = -1       }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Base, Value = -2       }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Natural, Value = 2     }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Penalty, Value = -2    }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Size, Value = 4        }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Size, Value = 8        }}}},
            { "ANIMAL_GROWTH_HUGE", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Base, Value = -2       }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Base, Value = -4       }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Natural, Value = 2     }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Penalty, Value = -2    }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Size, Value = 4        }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Size, Value = 8        }}}},
            { "ANIMAL_GROWTH_GARGANTUAN", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Base, Value = -4       }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Base, Value = -6       }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Natural, Value = 2     }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Penalty, Value = -2    }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Size, Value = 4        }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Size, Value = 8        }}}},
            { "ANIMAL_GROWTH_COLOSSAL", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Base, Value = -8       }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Base, Value = 8        }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Natural, Value = 2     }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Penalty, Value = -2    }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Size, Value = 4        }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ANIMAL_GROWTH", Type = BonusType.Size, Value = 8        }}}},

            { "BANE", new List<StatModifier>()                  { new StatModifier("ATK_BONUS") { Bonus = new Bonus()   { Name = "BANE", Type = BonusType.Penalty, Value = -1                                   }}}},
            { "BEARS_ENDURANCE", new List<StatModifier>()       { new StatModifier("CON_TEMP")  { Bonus = new Bonus()   { Name = "BEARS_ENDURANCE",     Type = BonusType.Enhancement, Value = 4     }}}},
            { "BLEND", new List<StatModifier>()                 { new StatModifier("SK_STL")    { Bonus = new Bonus()   { Name = "BLEND",Type = BonusType.Circumstance, Value = 4                               }}}},
            { "BLESS", new List<StatModifier>()                 { new StatModifier("ATK_BONUS") { Bonus = new Bonus()   { Name = "BLESS",Type = BonusType.Morale, Value = 1                                     }}}},
            
            
            { "BEAST_SHAPE_DIMINUTIVE", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Base, Value = 4      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Base, Value = 6      }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Natural, Value = 1   }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Size, Value = 6      }},
                new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Penalty, Value = -4  }}}},
            { "BEAST_SHAPE_TINY", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Base, Value = 2      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Base, Value = 4      }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Natural, Value = 1   }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Size, Value = 4      }},
                new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Penalty, Value = -2  }}}},
            { "BEAST_SHAPE_SMALL", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Base, Value = 1      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Base, Value = 2      }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Natural, Value = 1   }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Size, Value = 2      }}}},
            { "BEAST_SHAPE_MEDIUM", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Base, Value = 0      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Base, Value = 0      }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Natural, Value = 2   }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Size, Value = 2      }}}},
            { "BEAST_SHAPE_LARGE", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Base, Value = -1     }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Base, Value = -2     }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Natural, Value = 4   }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Size, Value = 4      }}}},
            { "BEAST_SHAPE_HUGE", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Base, Value = -2     }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Base, Value = -4     }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Natural, Value = 6   }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Penalty, Value = -4  }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "BEAST_SHAPE", Type = BonusType.Size, Value = 6      }}}},

            { "BULLS_STRENGTH", new List<StatModifier>()    { new StatModifier("STR_TEMP")  { Bonus = new Bonus()   { Name = "BULLS_STRENGTH",      Type = BonusType.Enhancement, Value = 4  }}}},
            { "CATS_GRACE", new List<StatModifier>()        { new StatModifier("DEX_TEMP")  { Bonus = new Bonus()   { Name = "CATS_GRACE",          Type = BonusType.Enhancement, Value = 4  }}}},
            { "DEADEYES_LORE", new List<StatModifier>()     { new StatModifier("SK_SUR")    { Bonus = new Bonus()   { Name = "DEADEYES_LORE",       Type = BonusType.Sacred, Value = 4       }}}},
            
            ///dragon
            { "DRAGON_FORM_MEDIUM", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "DRAGON_FORM", Type = BonusType.Base, Value = 0      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "DRAGON_FORM", Type = BonusType.Base, Value = 0      }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "DRAGON_FORM", Type = BonusType.Natural, Value = 4   }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "DRAGON_FORM", Type = BonusType.Size, Value = 2      }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "DRAGON_FORM", Type = BonusType.Size, Value = 4      }}}},
            { "DRAGON_FORM_LARGE", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "DRAGON_FORM", Type = BonusType.Base, Value = -1     }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "DRAGON_FORM", Type = BonusType.Base, Value = -2     }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "DRAGON_FORM", Type = BonusType.Natural, Value = 6   }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "DRAGON_FORM", Type = BonusType.Size, Value = 4      }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "DRAGON_FORM", Type = BonusType.Size, Value = 6      }}}},
            { "DRAGON_FORM_HUGE", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "DRAGON_FORM", Type = BonusType.Base, Value = -2     }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "DRAGON_FORM", Type = BonusType.Base, Value = -4     }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "DRAGON_FORM", Type = BonusType.Natural, Value = 8   }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "DRAGON_FORM", Type = BonusType.Size, Value = 8      }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "DRAGON_FORM", Type = BonusType.Size, Value = 10     }}}},

            { "EAGLES_SPLENDOR", new List<StatModifier>()       { new StatModifier("CHA_TEMP")  { Bonus = new Bonus()   { Name = "EAGLES_SPLENDOR",     Type = BonusType.Enhancement, Value = 4  }}}},

            ///elemental
            { "ELEMENTAL_BODY_SMALL_AIR", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = 1       }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = 2       }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 2       }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Natural, Value = 2    }}}},
            { "ELEMENTAL_BODY_SMALL_EARTH", new List<StatModifier>(){          
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = 1       }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = 2       }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 2       }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Natural, Value = 4    }}}},
            { "ELEMENTAL_BODY_SMALL_FIRE", new List<StatModifier>(){      
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = 1       }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = 2       }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 2       }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Natural, Value = 2    }}}},
            { "ELEMENTAL_BODY_SMALL_WATER", new List<StatModifier>(){  
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = 1       }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = 2       }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 2       }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Natural, Value = 4    }}}},
            { "ELEMENTAL_BODY_MEDIUM_AIR", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = 0       }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = 0       }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 4       }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 3       }}}},
            { "ELEMENTAL_BODY_MEDIUM_EARTH", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = 0       }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = 0       }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 4       }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 5       }}}},
            { "ELEMENTAL_BODY_MEDIUM_FIRE", new List<StatModifier>(){        
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = 0       }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = 0       }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 4       }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 3       }}}},
            { "ELEMENTAL_BODY_MEDIUM_WATER", new List<StatModifier>(){            
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = 0       }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = 0       }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 4       }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 5       }}}},
            { "ELEMENTAL_BODY_LARGE_AIR", new List<StatModifier>(){                
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = -1      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = -2      }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 2       }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 4       }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Natural, Value = 4    }}}},
            { "ELEMENTAL_BODY_LARGE_EARTH", new List<StatModifier>(){             
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = -1      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = -2      }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 6       }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Penalty, Value = -2   }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 2       }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Natural, Value = 6    }}}},
            { "ELEMENTAL_BODY_LARGE_FIRE", new List<StatModifier>(){               
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = -1      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = -2      }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 4       }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 2       }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Natural, Value = 4    }}}},
            { "ELEMENTAL_BODY_LARGE_WATER", new List<StatModifier>(){               
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = -1      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = -2      }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 2       }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Penalty, Value = -2   }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 6       }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Natural, Value = 6    }}}},
            { "ELEMENTAL_BODY_HUGE_AIR", new List<StatModifier>(){                  
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = -2      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = -4      }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 4       }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 6       }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Natural, Value = 4    }}}},
            { "ELEMENTAL_BODY_HUGE_EARTH", new List<StatModifier>(){               
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = -2      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = -4      }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 8       }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Penalty, Value = -2   }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Penalty, Value = 4    }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Natural, Value = 6    }}}},
            { "ELEMENTAL_BODY_HUGE_FIRE", new List<StatModifier>(){                
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = -2      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = -4      }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 6       }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 4       }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Natural, Value = 4    }}}},
            { "ELEMENTAL_BODY_HUGE_WATER", new List<StatModifier>(){                
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = -2      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Base, Value = -4      }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 4       }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Penalty, Value = -2   }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Size, Value = 8       }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "ELEMENTAL_BODY", Type = BonusType.Natural, Value = 8    }}}},        
            
            ///fey
            { "FEY_FORM_DIMINUTIVE", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Base, Value = 4      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Base, Value = 6      }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Size, Value = 8         }},
                new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Penalty, Value = -4     }}}},
            { "FEY_FORM_TINY", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Base, Value = 2      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Base, Value = 4      }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Size, Value = 6         }},
                new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Penalty, Value = -2     }}}},
            { "FEY_FORM_SMALL", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Base, Value = 1      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Base, Value = 2      }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Size, Value = 2         }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Size, Value = 2         }}}},
            { "FEY_FORM_MEDIUM", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Base, Value = 0      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Base, Value = 0      }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Size, Value = 2         }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Size, Value = 2         }}}},
            { "FEY_FORM_LARGE", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Base, Value = -1     }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Base, Value = -2     }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Size, Value = 4         }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Size, Value = 4         }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Penalty, Value = -2     }}}},
            { "FEY_FORM_HUGE", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Base, Value = -2     }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Base, Value = -4     }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Size, Value = 6         }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Size, Value = 6         }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "FEY_FORM", Type = BonusType.Penalty, Value = -4     }}}},

            { "FOXS_CUNNING", new List<StatModifier>()          { new StatModifier("INT_TEMP")  { Bonus = new Bonus()   { Name = "FOXS_CUNNING",        Type = BonusType.Enhancement, Value = 4  }}}},

            ///giant
            { "GIANT_FORM_LARGE", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "GIANT_FORM", Type = BonusType.Base, Value = -1     }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "GIANT_FORM", Type = BonusType.Base, Value = -2     }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "GIANT_FORM", Type = BonusType.Size, Value = 8       }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "GIANT_FORM", Type = BonusType.Penalty, Value = -2   }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "GIANT_FORM", Type = BonusType.Size, Value = 6       }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "GIANT_FORM", Type = BonusType.Natural, Value = 6    }}}},
            { "GIANT_FORM_HUGE", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "GIANT_FORM", Type = BonusType.Base, Value = -2     }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "GIANT_FORM", Type = BonusType.Base, Value = -4     }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "GIANT_FORM", Type = BonusType.Size, Value = 6       }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "GIANT_FORM", Type = BonusType.Penalty, Value = -2   }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "GIANT_FORM", Type = BonusType.Size, Value = 4       }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "GIANT_FORM", Type = BonusType.Natural, Value = 4    }}}},
            
            { "MAGE_ARMOR", new List<StatModifier>()                { new StatModifier("AC_BONUS")  { Bonus = new Bonus()   { Name = "MAGE_ARMOR",          Type = BonusType.Armor, Value = 4        }}}},
            
            ///magical beast
            { "MAGICAL_BEAST_SHAPE_DIMINUTIVE", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Base, Value = 4      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Base, Value = 6      }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Size, Value = 10     }},
                new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Penalty, Value = -4  }}}},
            { "MAGICAL_BEAST_SHAPE_TINY", new List<StatModifier>(){                
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Base, Value = 2      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Base, Value = 4      }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Natural, Value = 3   }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Size, Value = 8      }},
                new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Penalty, Value = -2  }}}},
            { "MAGICAL_BEAST_SHAPE_SMALL", new List<StatModifier>(){            
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Base, Value = 1      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Base, Value = 2      }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Natural, Value = 2   }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Size, Value = 4      }}}},
            { "MAGICAL_BEAST_SHAPE_MEDIUM", new List<StatModifier>(){       
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Base, Value = 0      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Base, Value = 0      }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Natural, Value = 4   }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Size, Value = 4      }}}},
            { "MAGICAL_BEAST_SHAPE_LARGE", new List<StatModifier>(){        
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Base, Value = -1     }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Base, Value = -2     }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Natural, Value = 6   }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Size, Value = -2     }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Penalty, Value = 6   }}}},
            { "MAGICAL_BEAST_SHAPE_HUGE", new List<StatModifier>(){              
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Base, Value = -2     }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Base, Value = -4     }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Natural, Value = 7   }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Penalty, Value = -4  }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Size, Value = 2      }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "MAGICAL_BEAST_SHAPE", Type = BonusType.Size, Value = 8      }}}},
            
            ///naga
            { "NAGA_SHAPE", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "NAGA_SHAPE", Type = BonusType.Base, Value = -1     }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "NAGA_SHAPE", Type = BonusType.Base, Value = -2     }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "NAGA_SHAPE", Type = BonusType.Size, Value = 4       }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "NAGA_SHAPE", Type = BonusType.Penalty, Value = -2   }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "NAGA_SHAPE", Type = BonusType.Natural, Value = 4    }}}},
            
            ///ooze
            { "OOZE_FORM_SMALL", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "OOZE_FORM", Type = BonusType.Base, Value = 1      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "OOZE_FORM", Type = BonusType.Base, Value = 2      }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "OOZE_FORM", Type = BonusType.Size, Value = 4        }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "OOZE_FORM", Type = BonusType.Penalty, Value = -4    }}}},
            { "OOZE_FORM_MEDIUM", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "OOZE_FORM", Type = BonusType.Base, Value = 0      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "OOZE_FORM", Type = BonusType.Base, Value = 0      }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "OOZE_FORM", Type = BonusType.Size, Value = 6        }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "OOZE_FORM", Type = BonusType.Penalty, Value = -6    }}}},
            { "OOZE_FORM_LARGE", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "OOZE_FORM", Type = BonusType.Base, Value = -1     }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "OOZE_FORM", Type = BonusType.Base, Value = -2     }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "OOZE_FORM", Type = BonusType.Size, Value = 2        }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "OOZE_FORM", Type = BonusType.Size, Value = 8        }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "OOZE_FORM", Type = BonusType.Penalty, Value = -8    }}}},
            { "OOZE_FORM_HUGE", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "OOZE_FORM", Type = BonusType.Base, Value = -2     }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "OOZE_FORM", Type = BonusType.Base, Value = -4     }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "OOZE_FORM", Type = BonusType.Size, Value = 4        }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "OOZE_FORM", Type = BonusType.Size, Value = 10       }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "OOZE_FORM", Type = BonusType.Penalty, Value = -10   }}}},

            { "OWLS_WISDOM", new List<StatModifier>()           { new StatModifier("WIS_TEMP")  { Bonus = new Bonus()   { Name = "OWLS_WISDOM",         Type = BonusType.Enhancement, Value = 4  }}}},

            ///plant shape
            { "PLANT_SHAPE_SMALL", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "PLANT_SHAPE", Type = BonusType.Base, Value = 1      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "PLANT_SHAPE", Type = BonusType.Base, Value = 2      }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "PLANT_SHAPE", Type = BonusType.Size, Value = 2      }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "PLANT_SHAPE", Type = BonusType.Natural, Value = 2   }}}},
            { "PLANT_SHAPE_MEDIUM", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "PLANT_SHAPE", Type = BonusType.Base, Value = 0      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "PLANT_SHAPE", Type = BonusType.Base, Value = 0      }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "PLANT_SHAPE", Type = BonusType.Size, Value = 2      }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "PLANT_SHAPE", Type = BonusType.Size, Value = 2      }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "PLANT_SHAPE", Type = BonusType.Natural, Value = 2   }}}},
            { "PLANT_SHAPE_LARGE", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "PLANT_SHAPE", Type = BonusType.Base, Value = -1     }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "PLANT_SHAPE", Type = BonusType.Base, Value = -2     }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "PLANT_SHAPE", Type = BonusType.Size, Value = 4      }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "PLANT_SHAPE", Type = BonusType.Size, Value = 2      }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "PLANT_SHAPE", Type = BonusType.Natural, Value = 4   }}}},
            { "PLANT_SHAPE_HUGE", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "PLANT_SHAPE", Type = BonusType.Base, Value = -2     }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "PLANT_SHAPE", Type = BonusType.Base, Value = -4     }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "PLANT_SHAPE", Type = BonusType.Size, Value = 8      }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "PLANT_SHAPE", Type = BonusType.Size, Value = -2     }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "PLANT_SHAPE", Type = BonusType.Size, Value = 4      }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "PLANT_SHAPE", Type = BonusType.Natural, Value = 6   }}}},         
            
            ///pupper
            { "PUP_SHAPE", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "PUP_SHAPE", Type = BonusType.Base, Value = 1      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "PUP_SHAPE", Type = BonusType.Base, Value = 2      }},
                new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "PUP_SHAPE", Type = BonusType.Penalty, Value = -4    }},
                new StatModifier("CON_SCORE")       { Bonus = new Bonus() { Name = "PUP_SHAPE", Type = BonusType.Penalty, Value = -4    }},
                new StatModifier("INT_SCORE")       { Bonus = new Bonus() { Name = "PUP_SHAPE", Type = BonusType.Penalty, Value = -4    }},
                new StatModifier("WIS_SCORE")       { Bonus = new Bonus() { Name = "PUP_SHAPE", Type = BonusType.Penalty, Value = -4    }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "PUP_SHAPE", Type = BonusType.Size, Value = 4        }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "PUP_SHAPE", Type = BonusType.Natural, Value = 2     }}}},
            
            ///transformation
            { "TRANSFORMATION", new List<StatModifier>(){
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "TRANSFORMATION", Type = BonusType.Enhancement, Value = 4 }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "TRANSFORMATION", Type = BonusType.Enhancement, Value = 4 }},
                new StatModifier("CON_TEMP")        { Bonus = new Bonus() { Name = "TRANSFORMATION", Type = BonusType.Enhancement, Value = 4 }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "TRANSFORMATION", Type = BonusType.Natural, Value = 4     }},
                new StatModifier("FORT_BONUS")      { Bonus = new Bonus() { Name = "TRANSFORMATION", Type = BonusType.Competence, Value = 5  }}}},          
            
            ///vermin
            { "VERMIN_SHAPE_TINY", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "VERMIN_SHAPE", Type = BonusType.Base, Value = 2      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "VERMIN_SHAPE", Type = BonusType.Base, Value = 4      }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "VERMIN_SHAPE", Type = BonusType.Size, Value = 4     }},
                new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "VERMIN_SHAPE", Type = BonusType.Penalty, Value = -2 }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "VERMIN_SHAPE", Type = BonusType.Natural, Value = 1  }}}},
            { "VERMIN_SHAPE_SMALL", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "VERMIN_SHAPE", Type = BonusType.Base, Value = 1      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "VERMIN_SHAPE", Type = BonusType.Base, Value = 2      }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "VERMIN_SHAPE", Type = BonusType.Size, Value = 2     }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "VERMIN_SHAPE", Type = BonusType.Natural, Value = 2  }}}},
            { "VERMIN_SHAPE_MEDIUM", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "VERMIN_SHAPE", Type = BonusType.Base, Value = 0      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "VERMIN_SHAPE", Type = BonusType.Base, Value = 0      }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "VERMIN_SHAPE", Type = BonusType.Size, Value = 2     }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "VERMIN_SHAPE", Type = BonusType.Natural, Value = 3  }}}},
            { "VERMIN_SHAPE_LARGE", new List<StatModifier>(){
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "VERMIN_SHAPE", Type = BonusType.Base, Value = -1     }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "VERMIN_SHAPE", Type = BonusType.Base, Value = -2     }},
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "VERMIN_SHAPE", Type = BonusType.Size, Value = 4     }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "VERMIN_SHAPE", Type = BonusType.Penalty, Value = -2 }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "VERMIN_SHAPE", Type = BonusType.Natural, Value = 5  }}}},


            ///other
            


            { "DISCOVERY_TORCH", new List<StatModifier>(){
                new StatModifier("SK_PRC")          { Bonus = new Bonus() { Name = "DISCOVERY_TORCH", Type = BonusType.Enhancement, Value = 2 }},
                new StatModifier("SK_SNS")          { Bonus = new Bonus() { Name = "DISCOVERY_TORCH", Type = BonusType.Enhancement, Value = 2 }}}},

            ///divine favor
            { "DIVINE_FAVOR_1", new List<StatModifier>(){
                new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "DIVINE_FAVOR", Type = BonusType.Luck, Value = 1 }},
                new StatModifier("DMG_BONUS")       { Bonus = new Bonus() { Name = "DIVINE_FAVOR", Type = BonusType.Luck, Value = 1 }}}},
            { "DIVINE_FAVOR_2", new List<StatModifier>(){
                new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "DIVINE_FAVOR", Type = BonusType.Luck, Value = 2 }},
                new StatModifier("DMG_BONUS")       { Bonus = new Bonus() { Name = "DIVINE_FAVOR", Type = BonusType.Luck, Value = 2 }}}},
            { "DIVINE_FAVOR_3", new List<StatModifier>(){
                new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "DIVINE_FAVOR", Type = BonusType.Luck, Value = 3 }},
                new StatModifier("DMG_BONUS")       { Bonus = new Bonus() { Name = "DIVINE_FAVOR", Type = BonusType.Luck, Value = 3 }}}},

            ///enlarge person
            { "ENLARGE_PERSON_DIMINUTIVE", new List<StatModifier>(){
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Size, Value = 2       }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Typeless, Value = -2  }},
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Base, Value = 4       }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Base, Value = 6       }}}},
            { "ENLARGE_PERSON_TINY", new List<StatModifier>(){
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Size, Value = 2       }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Typeless, Value = -2  }},
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Base, Value = 2       }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Base, Value = 4       }}}},
            { "ENLARGE_PERSON_SMALL", new List<StatModifier>(){
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Size, Value = 2       }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Typeless, Value = -2  }},
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Base, Value = 1       }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Base, Value = 2       }}}},
            { "ENLARGE_PERSON_MEDIUM", new List<StatModifier>(){
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Size, Value = 2       }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Typeless, Value = -2  }},
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Base, Value = 0       }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Base, Value = 0       }}}},
            { "ENLARGE_PERSON_LARGE", new List<StatModifier>(){
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Size, Value = 2       }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Typeless, Value = -2  }},
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Base, Value = -1      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Base, Value = -2      }}}},
            { "ENLARGE_PERSON_HUGE", new List<StatModifier>(){
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Size, Value = 2       }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Typeless, Value = -2  }},
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Base, Value = -2      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Base, Value = -4      }}}},
            { "ENLARGE_PERSON_GARANTUAN", new List<StatModifier>(){
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Size, Value = 2       }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Typeless, Value = -2  }},
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Base, Value = -4      }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Base, Value = -6      }}}},
            { "ENLARGE_PERSON_COLOSSAL", new List<StatModifier>(){
                new StatModifier("STR_TEMP")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Size, Value = 2       }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Typeless, Value = -2  }},
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Typeless, Value = -8  }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "ENLARGE_PERSON", Type = BonusType.Typeless, Value = -8  }}}},

            { "FLAGBEARER", new List<StatModifier>(){
                new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "FLAGBEARER", Type = BonusType.Morale, Value = 1 }},
                new StatModifier("DMG_BONUS")       { Bonus = new Bonus() { Name = "FLAGBEARER", Type = BonusType.Morale, Value = 1 }}}},
            { "HASTE", new List<StatModifier>(){
                new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "HASTE", Type = BonusType.Typeless, Value = 1     }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "HASTE", Type = BonusType.Dodge, Value = 1        }},
                new StatModifier("REF_BONUS")       { Bonus = new Bonus() { Name = "HASTE", Type = BonusType.Dodge, Value = 1        }},
                new StatModifier("MOVE")            { Bonus = new Bonus() { Name = "HASTE", Type = BonusType.Enhancement, Value = 30 }}}},

            ///inspire courage
            { "INSPIRE_COURAGE_1", new List<StatModifier>(){
                new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 1 }},
                new StatModifier("DMG_BONUS")       { Bonus = new Bonus() { Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 1 }}}},
            { "INSPIRE_COURAGE_2", new List<StatModifier>(){
                new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 2 }},
                new StatModifier("DMG_BONUS")       { Bonus = new Bonus() { Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 2 }}}},
            { "INSPIRE_COURAGE_3", new List<StatModifier>(){
                new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 3 }},
                new StatModifier("DMG_BONUS")       { Bonus = new Bonus() { Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 3 }}}},
            { "INSPIRE_COURAGE_4", new List<StatModifier>(){
                new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 4 }},
                new StatModifier("DMG_BONUS")       { Bonus = new Bonus() { Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 4 }}}},
            { "INSPIRE_COURAGE_5", new List<StatModifier>(){
                new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 5 }},
                new StatModifier("DMG_BONUS")       { Bonus = new Bonus() { Name = "INSPIRE_COURAGE", Type = BonusType.Competence, Value = 5 }}}},

            ///reduce person
            { "REDUCE_PERSON_FINE", new List<StatModifier>(){
                new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Size, Value = -2    }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 2 }},
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 4 }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 2 }}}},
            { "REDUCE_PERSON_DIMINUTIVE", new List<StatModifier>(){
                new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Size, Value = -2    }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 2 }},
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 2 }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 2 }}}},
            { "REDUCE_PERSON_TINY", new List<StatModifier>(){
                new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Size, Value = -2    }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 2 }},
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 1}},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 2 }}}},
            { "REDUCE_PERSON_SMALL", new List<StatModifier>(){
                new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Size, Value = -2    }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 2 }},
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 1 }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 2 }}}},
            { "REDUCE_PERSON_MEDIUM", new List<StatModifier>(){
                new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Size, Value = -2    }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 2 }},
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 1 }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 2 }}}},
            { "REDUCE_PERSON_LARGE", new List<StatModifier>(){
                new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Size, Value = -2    }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 2 }},
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 1 }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 2 }}}},
            { "REDUCE_PERSON_HUGE", new List<StatModifier>(){
                new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Size, Value = -2    }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 2 }},
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 2 }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 2 }}}},
            { "REDUCE_PERSON_GARGANTUAN", new List<StatModifier>(){
                new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Size, Value = -2    }},
                new StatModifier("DEX_TEMP")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 2 }},
                new StatModifier("SIZE_MOD")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 4 }},
                new StatModifier("SIZE_SKL")        { Bonus = new Bonus() { Name = "REDUCE_PERSON", Type = BonusType.Typeless, Value = 2 }}}},

            { "RESISTANCE", new List<StatModifier>(){
                new StatModifier("FORT_BONUS")      { Bonus = new Bonus() { Name = "RESISTANCE", Type = BonusType.Resistance, Value = 1 }},
                new StatModifier("REF_BONUS")       { Bonus = new Bonus() { Name = "RESISTANCE", Type = BonusType.Resistance, Value = 1 }},
                new StatModifier("WILL_BONUS")      { Bonus = new Bonus() { Name = "RESISTANCE", Type = BonusType.Resistance, Value = 1 }}}},
            { "SHIELD", new List<StatModifier>()                    { new StatModifier("AC_BONUS")  { Bonus = new Bonus()   { Name = "SHIELD",              Type = BonusType.Shield, Value = 4       }}}},
            
            { "SHIELD_OF_FAITH_1", new List<StatModifier>()         { new StatModifier("AC_BONUS")  { Bonus = new Bonus()   { Name = "SHIELD_OF_FAITH",     Type = BonusType.Deflection, Value = 2   }}}},
            { "SHIELD_OF_FAITH_2", new List<StatModifier>()         { new StatModifier("AC_BONUS")  { Bonus = new Bonus()   { Name = "SHIELD_OF_FAITH",     Type = BonusType.Deflection, Value = 3   }}}},
            { "SHIELD_OF_FAITH_3", new List<StatModifier>()         { new StatModifier("AC_BONUS")  { Bonus = new Bonus()   { Name = "SHIELD_OF_FAITH",     Type = BonusType.Deflection, Value = 4   }}}},
            { "SHIELD_OF_FAITH_4", new List<StatModifier>()         { new StatModifier("AC_BONUS")  { Bonus = new Bonus()   { Name = "SHIELD_OF_FAITH",     Type = BonusType.Deflection, Value = 5   }}}},
            { "SHOCK_SHIELD", new List<StatModifier>()              { new StatModifier("AC_BONUS")  { Bonus = new Bonus()   { Name = "SHOCK_SHIELD",        Type = BonusType.Shield, Value = 2       }}}},

            { "STUNNING_BARRIER", new List<StatModifier>(){
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "STUNNING_BARRIER", Type = BonusType.Deflection, Value = 1 }},
                new StatModifier("FORT_BONUS")      { Bonus = new Bonus() { Name = "STUNNING_BARRIER", Type = BonusType.Resistance, Value = 1 }},
                new StatModifier("REF_BONUS")       { Bonus = new Bonus() { Name = "STUNNING_BARRIER", Type = BonusType.Resistance, Value = 1 }},
                new StatModifier("WILL_BONUS")      { Bonus = new Bonus() { Name = "STUNNING_BARRIER", Type = BonusType.Resistance, Value = 1 }}}},
            { "STUNNING_BARRIER_GREATER", new List<StatModifier>(){
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "STUNNING_BARRIER", Type = BonusType.Deflection, Value = 2 }},
                new StatModifier("FORT_BONUS")      { Bonus = new Bonus() { Name = "STUNNING_BARRIER", Type = BonusType.Resistance, Value = 2 }},
                new StatModifier("REF_BONUS")       { Bonus = new Bonus() { Name = "STUNNING_BARRIER", Type = BonusType.Resistance, Value = 2 }},
                new StatModifier("WILL_BONUS")      { Bonus = new Bonus() { Name = "STUNNING_BARRIER", Type = BonusType.Resistance, Value = 2 }}}},
            { "TAP_INNER_BEAUTY", new List<StatModifier>(){
                new StatModifier("SK_BLF")          { Bonus = new Bonus() { Name = "TAP_INNER_BEAUTY", Type = BonusType.Insight, Value = 2 }},
                new StatModifier("SK_DIP")          { Bonus = new Bonus() { Name = "TAP_INNER_BEAUTY", Type = BonusType.Insight, Value = 2 }},
                new StatModifier("SK_DSG")          { Bonus = new Bonus() { Name = "TAP_INNER_BEAUTY", Type = BonusType.Insight, Value = 2 }},
                new StatModifier("SK_ITM")          { Bonus = new Bonus() { Name = "TAP_INNER_BEAUTY", Type = BonusType.Insight, Value = 2 }},
                new StatModifier("SK_UMD")          { Bonus = new Bonus() { Name = "TAP_INNER_BEAUTY", Type = BonusType.Insight, Value = 2 }}}},
            { "UNPREPARED_COMBATANT", new List<StatModifier>(){
                new StatModifier("INIT_BONUS")      { Bonus = new Bonus() { Name = "UNPREPARED_COMBATANT", Type = BonusType.Penalty, Value = -4 }},
                new StatModifier("REF_BONUS")       { Bonus = new Bonus() { Name = "UNPREPARED_COMBATANT", Type = BonusType.Penalty, Value = -4 }}}},
           
            
            //conditions
            { "BLINDED", new List<StatModifier>(){
                new StatModifier("AC_MAXDEX")       { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -99 }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_ACR")          { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -4  }},
                new StatModifier("SK_CLM")          { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -4  }},
                new StatModifier("SK_DSA")          { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -4  }},
                new StatModifier("SK_ESC")          { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -4  }},
                new StatModifier("SK_ACR")          { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -4  }},
                new StatModifier("SK_FLY")          { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -4  }},
                new StatModifier("SK_RDE")          { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -4  }},
                new StatModifier("SK_SLT")          { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -4  }},
                new StatModifier("SK_SWM")          { Bonus = new Bonus() { Name = "BLINDED", Type = BonusType.Penalty, Value = -4  }}}},
            { "COWERING", new List<StatModifier>(){
                new StatModifier("AC_MAXDEX")       { Bonus = new Bonus() { Name = "COWERING", Type = BonusType.Penalty, Value = -99 }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "COWERING", Type = BonusType.Penalty, Value = -2  }}}},
            { "DAZZLED", new List<StatModifier>(){
                new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "DAZZLED", Type = BonusType.Penalty, Value = -1  }}}},
            { "DEAFENED", new List<StatModifier>(){
                new StatModifier("INIT_BONUS")      { Bonus = new Bonus() { Name = "DEAFENED", Type = BonusType.Penalty, Value = -4 }},
                new StatModifier("SK_PRC")          { Bonus = new Bonus() { Name = "DEAFENED", Type = BonusType.Penalty, Value = -4 }}}},
            { "ENTANGLED", new List<StatModifier>(){
                new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "ENTANGLED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "ENTANGLED", Type = BonusType.Penalty, Value = -4  }}}},
            { "EXHAUSTED", new List<StatModifier>(){
                new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "EXHAUSTED", Type = BonusType.Penalty, Value = -6  }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "EXHAUSTED", Type = BonusType.Penalty, Value = -6  }}}},
            { "FASCINATED", new List<StatModifier>(){
                new StatModifier("SK_PRC")          { Bonus = new Bonus() { Name = "FASCINATED", Type = BonusType.Penalty, Value = -4  }}}},
            { "FATIGUED", new List<StatModifier>(){
                new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "FATIGUED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "FATIGUED", Type = BonusType.Penalty, Value = -2  }}}},
            { "FRIGHTENED", new List<StatModifier>(){
                new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("FORT_BONUS")      { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("REF_BONUS")       { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("WILL_BONUS")      { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_ACR")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_APR")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_BLF")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_CLM")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_DIP")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_DSA")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_DSG")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_ESC")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_FLY")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_HND")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_HEA")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_ITM")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_LNG")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_PRC")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_RDE")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_SNS")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_SLT")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_SPL")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_STL")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_SUR")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_SWM")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_UMD")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_ARC")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_DUN")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_ENG")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_GEO")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_HIS")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_LCL")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_NTR")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_NBL")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_PLN")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_RLG")          { Bonus = new Bonus() { Name = "FRIGHTENED", Type = BonusType.Penalty, Value = -2  }}}},
            { "GRAPPLED", new List<StatModifier>(){
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "GRAPPLED", Type = BonusType.Penalty, Value = -4  }},
                new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "GRAPPLED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("CMB_BONUS")       { Bonus = new Bonus() { Name = "GRAPPLED", Type = BonusType.Penalty, Value = -2  }}}},
            { "HELPLESS", new List<StatModifier>(){
                    new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "HELPLESS", Type = BonusType.Penalty, Value = -100 }}}},
            { "PANICKED", new List<StatModifier>(){
                new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("FORT_BONUS")      { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("REF_BONUS")       { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("WILL_BONUS")      { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_ACR")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_APR")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_BLF")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_CLM")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_DIP")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_DSA")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_DSG")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_ESC")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_FLY")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_HND")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_HEA")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_ITM")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_LNG")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_PRC")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_RDE")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_SNS")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_SLT")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_SPL")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_STL")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_SUR")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_SWM")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_UMD")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_ARC")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_DUN")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_ENG")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_GEO")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_HIS")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_LCL")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_NTR")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_NBL")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_PLN")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_RLG")          { Bonus = new Bonus() { Name = "PANICKED", Type = BonusType.Penalty, Value = -2  }}}},
            { "PARALYZED", new List<StatModifier>(){
                new StatModifier("DEX_SCORE")       { Bonus = new Bonus() { Name = "PARALYZED", Type = BonusType.Penalty, Value = -100 }},
                new StatModifier("STR_SCORE")       { Bonus = new Bonus() { Name = "PARALYZED", Type = BonusType.Penalty, Value = -100 }}}},
            { "PINNED", new List<StatModifier>(){
                new StatModifier("AC_MAXDEX")       { Bonus = new Bonus() { Name = "PINNED", Type = BonusType.Penalty, Value = -99 }},
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "PINNED", Type = BonusType.Penalty, Value = -4  }}}},
            { "SHAKEN", new List<StatModifier>(){
                new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("FORT_BONUS")      { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("REF_BONUS")       { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("WILL_BONUS")      { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_ACR")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_APR")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_BLF")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_CLM")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_DIP")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_DSA")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_DSG")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_ESC")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_FLY")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_HND")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_HEA")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_ITM")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_LNG")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_PRC")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_RDE")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_SNS")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_SLT")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_SPL")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_STL")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_SUR")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_SWM")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_UMD")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_ARC")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_DUN")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_ENG")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_GEO")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_HIS")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_LCL")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_NTR")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_NBL")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_PLN")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_RLG")          { Bonus = new Bonus() { Name = "SHAKEN", Type = BonusType.Penalty, Value = -2  }}}},
            { "SICKENED", new List<StatModifier>(){
                new StatModifier("DMG_BONUS")       { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("ATK_BONUS")       { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("FORT_BONUS")      { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("REF_BONUS")       { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("WILL_BONUS")      { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_ACR")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_APR")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_BLF")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_CLM")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_DIP")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_DSA")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_DSG")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_ESC")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_FLY")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_HND")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_HEA")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_ITM")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_LNG")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_PRC")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_RDE")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_SNS")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_SLT")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_SPL")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_STL")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_SUR")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_SWM")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_UMD")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_ARC")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_DUN")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_ENG")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_GEO")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_HIS")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_LCL")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_NTR")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_NBL")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_PLN")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("SK_RLG")          { Bonus = new Bonus() { Name = "SICKENED", Type = BonusType.Penalty, Value = -2  }}}},
            { "STUNNED", new List<StatModifier>(){
                new StatModifier("AC_BONUS")        { Bonus = new Bonus() { Name = "STUNNED", Type = BonusType.Penalty, Value = -2  }},
                new StatModifier("AC_MAXDEX")       { Bonus = new Bonus() { Name = "STUNNED", Type = BonusType.Penalty, Value = -99 }}}},
        };
    }

    
}
