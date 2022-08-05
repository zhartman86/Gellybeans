namespace Gellybeans.Pathfinder
{
    [Flags]
    public enum SkillType : long
    {
        Acrobatics          = 1 << 0,
        Appraise            = 1 << 1,
        Bluff               = 1 << 2,
        Climb               = 1 << 3,
        Craft               = 1 << 4,
        Diplomacy           = 1 << 5,
        DisableDevice       = 1 << 6,
        Disguise            = 1 << 7,
        EscapeArtist        = 1 << 8,
        Fly                 = 1 << 9,
        HandleAnimal        = 1 << 10,
        Heal                = 1 << 11,
        Intimidate          = 1 << 12,
        Arcana              = 1 << 13,
        Dungeoneering       = 1 << 14,
        Engineering         = 1 << 15,
        Geography           = 1 << 16,
        History             = 1 << 17,
        Local               = 1 << 18,
        Nature              = 1 << 19,
        Nobility            = 1 << 20,
        Planes              = 1 << 21,
        Religion            = 1 << 22,
        Linguistics         = 1 << 23,
        Perception          = 1 << 24,
        Perform             = 1 << 25,
        Profession          = 1 << 26,
        Ride                = 1 << 27,
        SenseMotive         = 1 << 28,
        SleightOfHand       = 1 << 29,
        Spellcraft          = 1 << 30,
        Stealth             = 1 << 31,
        Survival            = 1 << 32,
        Swim                = 1 << 33,
        UseMagicDevice      = 1 << 34
    }
}
