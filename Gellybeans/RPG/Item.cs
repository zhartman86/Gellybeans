using System;
using System.Collections.Generic;
using System.Text;

namespace EsoLib.Pathfinder
{
    public class Item
    {        
        protected static int counter;


        public int      Id              { get; set; }
        public string   Name            { get; set; } = "Name Me";
        public string   Description     { get; set; } = "";
        public float    Weight          { get; set; } = 0.1f;

        public Dictionary<string, Stat> Properties = null;      

        
        
        public Item()
        {
            Id = counter++;
        }

        public static bool operator ==(Item a, Item b) { return a.Id == b.Id; }
        public static bool operator !=(Item a, Item b) { return !(a == b); }

        public bool Equals(Item item) { return this == item; }
        public override bool Equals(object obj) { if(obj != null && obj.GetType() == typeof(Item)) return Equals((Item)obj); else return false; }

        public override int GetHashCode() { unchecked { return Id.GetHashCode() + Name.GetHashCode(); } }
    
    
        public Item Weapon(string name, string damage, int critRange, int critMultiplier, float weight, string description = "")
        {
            var item = new Item()
            {
                Name = name,
                Weight = weight,
                Description = description,

                Properties = new Dictionary<string, Stat>()
                
            };
            return item;
        }
    }
}
