namespace Gellybeans.Pathfinder
{
    public class Attack
    {   
        public string   ToHitExpr   { get; init; } = "ATK_M + 1";
        public string   DamageExpr  { get; init; } = "1d1";
        public string   CritExpr    { get; init; } = "2d1";
        public bool     Confirm     { get; init; } = true;
        public int      CritRange   { get; init; } = 20;
        public int      Sides       { get; init; } = 0;


        public Attack() { }
        public Attack(int sides, string toHitExpr, string damageExpr, string critExpr, int critRange)
        {
            Sides       = sides;
            ToHitExpr   = toHitExpr;
            DamageExpr  = damageExpr;
            CritRange   = critRange;
            CritExpr    = critExpr;
        }
    }
}