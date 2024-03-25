using Gellybeans.Expressions;
using System.Text;

namespace Gellybeans.Pathfinder
{
    public class Item
    {
        public string?  Name            { get; set; }
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


        public ArrayValue ToArray()
        {

            string magic = "";
            if(Magic != "")
            {
                var split = Magic!.Split('/');
                magic = $"**Aura** {split[0]}; **CL** {Ordinal(int.Parse(split[1]!))}";
            }


            var list = new List<dynamic> {

                new KeyValuePairValue("NAME",           new StringValue(Name ?? "")),
                new KeyValuePairValue("WEIGHT",         new StringValue(Weight != null ? Weight.ToString() : "")),
                new KeyValuePairValue("PRICE",          new StringValue(Price ?? "")),
                new KeyValuePairValue("TYPE",           new StringValue(Type ?? "")),
                new KeyValuePairValue("SLOT",           new StringValue(Slot ?? "")),
                new KeyValuePairValue("MAGIC",          new StringValue(magic)),
                new KeyValuePairValue("REQUIREMENTS",   new StringValue(Requirements ?? "")),
                new KeyValuePairValue("DESCRIPTION",    new StringValue(Description ?? "")),
                new KeyValuePairValue("SOURCE",         new StringValue(Source ?? "")),
            };

            if(Offense != "")
            {
                var split = Offense!.Split('/');
                list.Add(new KeyValuePairValue("W_TINY",        new StringValue(split[2])));
                list.Add(new KeyValuePairValue("W_SMALL",       new StringValue(split[3])));
                list.Add(new KeyValuePairValue("W_MEDIUM",      new StringValue(split[4])));
                list.Add(new KeyValuePairValue("W_LARGE",       new StringValue(split[5])));
                list.Add(new KeyValuePairValue("W_HUGE",        new StringValue(split[6])));
                list.Add(new KeyValuePairValue("W_RANGE",       new StringValue(split[11])));
                list.Add(new KeyValuePairValue("W_TYPE",        new StringValue(split[12])));
                list.Add(new KeyValuePairValue("W_CRIT_R",      new StringValue(split[9])));
                list.Add(new KeyValuePairValue("W_CRIT_M",      new StringValue(split[10])));
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
            }


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
