using Gellybeans.Expressions;
using Newtonsoft.Json;
using System.Text;

namespace Gellybeans.Pathfinder
{
    public class Item
    {
        static readonly List<Item> items = new List<Item>();        
        public static List<Item> Items { get { return items; } }

        public string?  Name            { get; set; } = "";
        public string?  Weight          { get; set; }
        public string?  Price           { get; set; }
        public string?  Type            { get; set; }
        public string?  Slot            { get; set; }
        
        public string?  MagicStrength   { get; set; }
        public string?  MagicSchool     { get; set; }
        public string?  MagicSubschool  { get; set; }
        public int?     MagicCL         { get; set; }
        
        public string?  Offense         { get; set; }
        public string?  Defense         { get; set; }
        public string?  BaseItem        { get; set; }
        public string?  Description     { get; set; }
        public string?  Requirements    { get; set; }        
        public string?  Destruction     { get; set; }
        public string?  Inscription     { get; set; }
        public string?  Source          { get; set; }

        static Item()
        {
            Console.Write("Getting items...");
            var itemJson = File.ReadAllText(@"E:\Pathfinder\PFData\Items.json");
            items = JsonConvert.DeserializeObject<List<Item>>(itemJson)!;
            Console.WriteLine($"Items => {items.Count}");
        }

        public static Item GetItem(int index) =>
            items[index];

        public static Item GetItem(string itemName) =>
            items.FirstOrDefault(x => x.Name.ToUpper() == itemName.ToUpper())!;


        public ArrayValue ToArrayValue()
        {

            var list = new List<dynamic> {
                
                new KeyValuePairValue("NAME",   new StringValue(Name ?? "")),
                
                new KeyValuePairValue("WEIGHT", new StringValue(Weight ?? "")),
                new KeyValuePairValue("TYPE",   new StringValue(Type ?? "")),
                new KeyValuePairValue("SLOT",   new StringValue(Slot ?? "")),
                new KeyValuePairValue("DESC",   new StringValue(Description ?? "")),
            };


            var split = Price.Split(',');
            if(split.Length == 3)
            {
                var pDict = new Dictionary<string, int>() { { "GP", 0 }, { "SP", 1 }, { "CP", 2 } };
                var p = new dynamic[3];
                for(int i = 0; i < split.Length; i++)
                    p[i] = int.Parse(split[i]);
                
                list.Add(new KeyValuePairValue("PRICE", new ArrayValue(p, pDict)));
                
            }           
            else
                list.Add(new KeyValuePairValue("PRICE",             new StringValue(Price ?? "")));


            if(MagicStrength != "")
                list.Add(new KeyValuePairValue("MAGIC_STR",         new StringValue(MagicStrength)));


            if(MagicSchool != "")
            {
                split = MagicSchool.Split(',');
                list.Add(new KeyValuePairValue("MAGIC_SCHOOL",      new ArrayValue(split)));
            }

            if(MagicSubschool != "")
            {
                split = MagicSubschool.Split(',');
                list.Add(new KeyValuePairValue("MAGIC_SUBSCHOOL",   new ArrayValue(split)));
            }

            if(MagicCL is not null)
                list.Add(new KeyValuePairValue("MAGIC_CL",          MagicCL));


            if(Requirements != "")
            {
                split = Requirements.Split(',');
                list.Add(new KeyValuePairValue("REQUIREMENTS",      new ArrayValue(split)));
            }

            if(Destruction != "")
                list.Add(new KeyValuePairValue("DESTRUCTION",       Destruction));
            
            if(Source != "")
                list.Add(new KeyValuePairValue("SOURCE",            Source));


            if(Offense != "")
            {
                split = Offense!.Split('/');
                list.Add(new KeyValuePairValue("DAMAGE", new StringValue(split[4])));
                list.Add(new KeyValuePairValue("RANGE", new StringValue(split[11])));
                list.Add(new KeyValuePairValue("DAMAGE_TYPE", new StringValue(split[12])));
                list.Add(new KeyValuePairValue("CRIT_RANGE", new StringValue(split[9])));
                list.Add(new KeyValuePairValue("CRIT_MULT", new StringValue(split[10])));
            }


            if(Defense != "")
            {
                split = Defense!.Split('/');
                list.Add(new KeyValuePairValue("ARMOR_BONUS", new StringValue(split[0])));
                list.Add(new KeyValuePairValue("SHIELD_BONUS", new StringValue(split[1])));
                list.Add(new KeyValuePairValue("MAX_DEX", new StringValue(split[2])));
                list.Add(new KeyValuePairValue("ARMOR_PENALTY", new StringValue(split[3])));
                list.Add(new KeyValuePairValue("SPELL_FAILURE", new StringValue(split[4])));
                list.Add(new KeyValuePairValue("THIRTY", new StringValue(split[5])));
                list.Add(new KeyValuePairValue("TWENTY", new StringValue(split[6])));

                //string defEq = "";
                //string defUnEq = "";
                //if(split[0] != "")
                //{
                //    defEq += $"AC_BONUS += $ARMOR:ARMOR:{split[0]};";
                //    defEq += $"AC_MAXDEX += $ARMOR:OVERRIDE:{split[2]};";
                //    defEq += $"AC_PENALTY += $ARMOR:PENALTY:{split[3]};";
                //    list.Add(new KeyValuePairValue("ON_EQUIP", new StringValue(defEq)));

                //    defUnEq += "-$ARMOR;";
                //    list.Add(new KeyValuePairValue("ON_UNEQUIP", new StringValue(defUnEq)));


                //}
                //else if(split[1] != "")
                //{
                //    defEq += $"AC_BONUS += $SHIELD:SHIELD:{split[1]};";
                //    defEq += $"AC_PENALTY += $SHIELD:PENALTY:{split[3]}";
                //    list.Add(new KeyValuePairValue("ON_EQUIP", new StringValue(defEq)));

                //    defUnEq += "-$SHIELD;";
                //    list.Add(new KeyValuePairValue("ON_UNEQUIP", new StringValue(defUnEq)));

                //}
            }

            list.Add(new KeyValuePairValue("NOTE", new StringValue("")));

            Dictionary<string, int> dict = new Dictionary<string, int>();
            dynamic[] array = new dynamic[list.Count];
            for(int i = 0; i < list.Count; i++)
            {
                dict.TryAdd(list[i].Key, i);
                array[i] = list[i].Value;
            }

            return new ArrayValue(array, dict);
        }
    
        public static string Ordinal(int? num)
        {
            if(num == 0) return "0";

            switch(num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }
            
            switch(num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }
        }

    }
}
