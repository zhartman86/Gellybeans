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
        public decimal? Weight          { get; set; }
        public string?  Price           { get; set; }
        public string?  Type            { get; set; }
        public string?  Magic           { get; set; }
        public string?  Offense         { get; set; }
        public string?  Defense         { get; set; }
        public string?  Slot            { get; set; }
        public string?  BaseItem        { get; set; }
        public string?  Formulae        { get; set; }
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

                new KeyValuePairValue("NAME",           new StringValue(Name ?? "")),
                new KeyValuePairValue("VALUE",          new StringValue(Price ?? "")),
                new KeyValuePairValue("WEIGHT",         new StringValue(Weight.ToString())),                             
                new KeyValuePairValue("DESC",           new StringValue(Description ?? "")),               
                new KeyValuePairValue("SLOT",           new StringValue(Slot ?? "")),               
            };

            if(Magic != "")
            {
                var split = Magic!.Split('/');
                list.Add(new KeyValuePairValue("MAGIC", $"**Aura** {split[0]}; **CL** {Ordinal(int.Parse(split[1]!))}"));
            }

            if(Offense != "")
            {
                var split = Offense!.Split('/');
                list.Add(new KeyValuePairValue("DAMAGE",        new StringValue(split[4])));              
                list.Add(new KeyValuePairValue("RANGE",         new StringValue(split[11])));
                list.Add(new KeyValuePairValue("DAMAGE_TYPE",   new StringValue(split[12])));
                list.Add(new KeyValuePairValue("CRIT_RANGE",      new StringValue(split[9])));
                list.Add(new KeyValuePairValue("CRIT_MULT",      new StringValue(split[10])));
            }
            if(Defense != "")
            {
                var split = Defense!.Split('/');
                list.Add(new KeyValuePairValue("ARMOR_BONUS",   new StringValue(split[0])));
                list.Add(new KeyValuePairValue("SHIELD_BONUS",  new StringValue(split[1])));
                list.Add(new KeyValuePairValue("MAX_DEX",       new StringValue(split[2])));
                list.Add(new KeyValuePairValue("ARMOR_PENALTY", new StringValue(split[3])));
                list.Add(new KeyValuePairValue("SPELL_FAILURE", new StringValue(split[4])));
                list.Add(new KeyValuePairValue("THIRTY",        new StringValue(split[5])));
                list.Add(new KeyValuePairValue("TWENTY",        new StringValue(split[6])));

                string defEq = "";
                string defUnEq = "";
                if(split[0] != "")
                {
                    defEq += $"AC_BONUS += $ARMOR:ARMOR:{split[0]};";
                    defEq += $"AC_MAXDEX += $ARMOR:OVERRIDE:{split[2]};";
                    defEq += $"AC_PENALTY += $ARMOR:PENALTY:{split[3]};";
                    list.Add(new KeyValuePairValue("ON_EQUIP", new StringValue(defEq)));                

                    defUnEq += "-$ARMOR;";
                    list.Add(new KeyValuePairValue("ON_UNEQUIP", new StringValue(defUnEq)));


                }                 
                else if(split[1] != "")
                {
                    defEq += $"AC_BONUS += $SHIELD:SHIELD:{split[1]};";
                    defEq += $"AC_PENALTY += $SHIELD:PENALTY:{split[3]}";
                    list.Add(new KeyValuePairValue("ON_EQUIP", new StringValue(defEq)));

                    defUnEq += "-$SHIELD;";
                    list.Add(new KeyValuePairValue("ON_UNEQUIP", new StringValue(defUnEq)));

                }    
            }

            list.Add(new KeyValuePairValue("NOTE", new StringValue("")));

            return new ArrayValue(list.ToArray());
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            
            sb.AppendLine($"__**{Name.ToUpper()}**__");
            if(Magic != "")
            {
                var split = Magic!.Split('/');
                sb.AppendLine($"**Aura** {split[0]}; **CL** {Ordinal(int.Parse(split[1]!))}");
            }
                
            sb.AppendLine($"**Slot** {Slot}; **Price** {Price}; **Weight** {Weight}");
            if(Offense != "")
            {
                var split = Offense!.Split('/');
                sb.AppendLine($"**Tiny** {split[2]}; **Small** {split[3]}; **Medium** {split[4]}; **Large** {split[5]}; **Huge** {split[6]}");
                sb.AppendLine($" **Range** {split[11]}; **Type** {split[12]}; **Critical** {split[9]}/x{split[10]}");
                if(split[13] != "—") sb.AppendLine($"**Special** {split[13].Replace('&',',')}");
                sb.AppendLine($"**Category** {split[14].Replace('&', ',')}; **Proficiency** {split[15]}");
            }

            if(Defense != "")
            {
                var split = Defense!.Split('/');
                sb.AppendLine($"{(split[0] != "0" ? $"**Armor Bonus** {split[0]}" : $"**Shield Bonus** {split[1]}")}; **Max Dex** {(string.IsNullOrEmpty(split[2]) ? "—" : split[2])}; **Penalty** {split[3]}");
                sb.AppendLine($"**Failure** {split[4]}; **Thirty** {split[5]}; **Twenty** {split[6]}");                
            }

            sb.AppendLine();
            if(Description != "") sb.AppendLine(Description);
            sb.AppendLine();
            if(Requirements != "")
            {
                sb.AppendLine("__Requirements__");
                sb.AppendLine(Requirements);
            }
            if(Destruction != "")
            {
                sb.AppendLine("__Destruction__");
                sb.AppendLine(Destruction);
            }
            sb.AppendLine();
            if(Source != "") sb.AppendLine($"*{Source}*");
            return sb.ToString();
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
