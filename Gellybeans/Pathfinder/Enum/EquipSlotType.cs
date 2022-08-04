using System;

namespace EsoLib.Pathfinder
{
    [Flags]
    public enum EquipSlotType
    {
        Slotless    = 0,
        Head        = 1 << 1,
        Headband    = 1 << 2,
        Eye         = 1 << 3,
        Shoulder    = 1 << 4,
        Neck        = 1 << 5,
        Chest       = 1 << 6,
        Body        = 1 << 7,
        Armor       = 1 << 8,
        Belt        = 1 << 9,
        Wrist       = 1 << 10,
        Hand        = 1 << 11,
        Finger      = 1 << 12,
        Legs        = 1 << 13,
        Feet        = 1 << 14,
        Held        = 1 << 15,
        Clothing    = 1 << 16,
        Secondary   = 1 << 17,
    }
}
