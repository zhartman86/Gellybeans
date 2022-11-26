namespace Gellybeans.Pathfinder
{
    public class Bonus : IComparer<Bonus>
    {
        public string       Name    { get; set; } = "";     
        public int          Value   { get; set; }
        public BonusType    Type    { get; set; }
  

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
            $"{Name}: {Value} {Enum.GetName(typeof(BonusType), Type)}";
    }
}
