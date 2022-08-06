namespace Gellybeans.Pathfinder
{
    public class Bonus : IComparer<Bonus>
    {
        public string       Name    { get; set; }        
        public int          Value   { get; set; }
        public BonusType    Type    { get; set; }

        public static bool operator ==(Bonus a, Bonus b) { return a.Value == b.Value && a.Name == b.Name && a.Type == b.Type; }
        public static bool operator !=(Bonus a, Bonus b) { return !(a == b); }

        public bool Equals(Bonus b) { return this == b; }
        public override bool Equals(object obj)
        {
            if(obj != null && obj.GetType() == typeof(Bonus)) 
                return Equals((Bonus)obj); 
            else return false;
        }
        public override int GetHashCode() { unchecked { return Name.GetHashCode() + Type.GetHashCode(); } }

        public int Compare(Bonus a, Bonus b) { return a.Value.CompareTo(b); }
    }
}
