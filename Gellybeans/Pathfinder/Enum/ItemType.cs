namespace Gellybeans.Pathfinder
{
    [Flags]
    public enum ItemType
    {
        None = 0,
        Equipment = 1 << 0,
        Consumable = 1 << 1,
        Weapon = 1 << 2,
        Armor = 1 << 3,
        Magic = 1 << 4,

    }
}
