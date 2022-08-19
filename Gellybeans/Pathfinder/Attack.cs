namespace Gellybeans.Pathfinder
{
    public class Attack
    {   
        public string   AttackName  { get; init; }
        public string   ToHitExpr   { get; init; } = "ATK_M + 1";
        public string   DamageExpr  { get; init; } = "1d1";
        public string   CritExpr    { get; init; } = "2d1";
        public bool     Confirm     { get; init; } = true;
        public int      CritRange   { get; init; } = 20;
        public int      Sides       { get; init; } = 0;


        public Attack() { }
        public Attack(string atkName, int sides, string toHitExpr, string damageExpr, string critExpr, int critRange)
        {
            AttackName  = atkName;
            Sides       = sides;
            ToHitExpr   = toHitExpr;
            DamageExpr  = damageExpr;     
            CritExpr    = critExpr;
            CritRange   = critRange;
        }

        public override string ToString()
        {
            var confirm = Confirm ? "YES" : "NO";
            return
@$"```NAME:       {AttackName}
HIT:        1d{Sides} + {ToHitExpr}
DMG:        {DamageExpr}
CRIT.DMG:   {CritExpr}
CRIT.RNG:   {CritRange}
CONFIRM:    {confirm}```";
        }
    }
}