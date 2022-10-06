using System.Text;

namespace Gellybeans.Pathfinder
{
    public class Item
    {
        public string?  Name            { get; set; }
        public decimal? Weight          { get; set; }
        public decimal? Value           { get; set; }
        public string?  Type            { get; set; }
        public string?  Aura            { get; set; }
        public int?     CL              { get; set; }
        public string?  Slot            { get; set; }
        public string?  Description     { get; set; }
        public string?  Requirements    { get; set; }        
        public string?  Destruction     { get; set; }
        public string?  Source          { get; set; }


        public override string ToString()
        {
            var sb = new StringBuilder();
            
            sb.AppendLine($"__**{Name.ToUpper()}**__");
            if(Aura != "") sb.AppendLine($"**Aura** {Aura}; **CL** {Ordinal(CL)}");
            sb.AppendLine($"**Slot** {Slot}; **Price** {Value}; **Weight** {Weight}");
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
