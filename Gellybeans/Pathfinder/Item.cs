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
                sb.AppendLine($"{(split[0] != "0" ? $"**Armor Bonus** {split[0]}"  : $"**Shield Bonus** {split[1]}")}; **Max Dex** {(string.IsNullOrEmpty(split[2]) ? "—" : split[2])}; **Penalty** {split[3]}");
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
