namespace Gellybeans.Pathfinder
{
    public class Attack
    {
        public string   Damage              { get; set; } = "1d1";
        public string   BonusCritDamage     { get; set; } = string.Empty;
        
        public int      CritRange           { get; set; } = 20;
        public int      CritMultiplier      { get; set; } = 2;       
        public int      AttackBonus         { get; set; } = 0;       
    }   
}
