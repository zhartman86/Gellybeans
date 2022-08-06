namespace Gellybeans.Pathfinder
{
    public class Item
    {
        public string   Name            { get; set; } = "Name Me";
        public string   Description     { get; set; } = "";
        public float    Value           { get; set; } = 0;
        public float    Weight          { get; set; } = 0;

        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

        public static bool operator ==(Item a, Item b) { return a.Name == b.Name; }
        public static bool operator !=(Item a, Item b) { return !(a == b); }

        public bool Equals(Item item) { return this == item; }
        public override bool Equals(object? obj) { if(obj != null && obj.GetType() == typeof(Item)) return Equals((Item)obj); else return false; }

        public override int GetHashCode() { unchecked { return Name.GetHashCode() + Properties.GetHashCode(); } }
    }
}
