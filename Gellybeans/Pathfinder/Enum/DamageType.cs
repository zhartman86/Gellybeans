using System;

namespace EsoLib.Pathfinder
{
    [Flags]
    public enum DamageType
    {
        Typeless        = 0,

        Slash           = 1 << 1,
        Pierce          = 1 << 2,
        Bludgeon        = 1 << 3,

        Fire            = 1 << 4,
        Acid            = 1 << 5,
        Electric        = 1 << 6,
        Cold            = 1 << 7,
        Sonic           = 1 << 8,

        Positive        = 1 << 9,
        Negative        = 1 << 10,

        Precision       = 1 << 11,
        
        Force           = 1 << 12,

        //not actual damage types, used mostly for DR
        Law             = 1 << 15,
        Chaos           = 1 << 16,
        Good            = 1 << 17,
        Evil            = 1 << 18,

        Magic           = 1 << 19,
        Silver          = 1 << 20,
        ColdIron        = 1 << 21,
        Adamantine      = 1 << 22,
    
        Epic            = 1 << 24,

        Nonlethal       = 1 << 28,

        //determine if DR is and/or 
        AND             = 1 << 31,
    }
}
