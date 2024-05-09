namespace Gellybeans.Pathfinder
{
    public class Bonus : IComparer<Bonus>
    {
        public string       Name    { get; set; } = "";     
        public int          Value   { get; set; } = 0;
        public BonusType    Type    { get; set; } = (BonusType)(-1);
  

        public bool Equals(Bonus b) { return this == b; }
        public override bool Equals(object? obj)
        {
            if(obj != null && obj.GetType() == typeof(Bonus)) 
                return Equals((Bonus)obj); 
            else return false;
        }
        public override int GetHashCode() { unchecked { return Name.GetHashCode() + Type.GetHashCode(); } }

        public int Compare(Bonus? a, Bonus? b) =>
            a.Value.CompareTo(b); 

        public override string ToString() =>
            $"{(Math.Sign(Value) > 0 ? "+" : "")}{Value} {Enum.GetName(typeof(BonusType), Type)!.ToLower()} [{Name}]";


        public static Bonus Empty { get; } = new Bonus();


        public static Stat operator +(int lhs, Bonus rhs) 
        { 
            var stat = new Stat(lhs);
            stat.AddBonus(rhs);
            return stat;
        }
        

    }
}
