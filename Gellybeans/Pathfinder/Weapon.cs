using System.Text;

namespace Gellybeans.Pathfinder
{
    public class Weapon
    {
        public string?  Name        { get; set; }
        public string?  Description { get; set; }
        public string?  Fine        { get; set; }
        public string?  Diminutive  { get; set; }
        public string?  Tiny        { get; set; }
        public string?  Small       { get; set; }
        public string?  Medium      { get; set; }
        public string?  Large       { get; set; }
        public string?  Huge        { get; set; }
        public string?  Gargantuan  { get; set; }
        public string?  Colossal    { get; set; }
        public string?  CritMul     { get; set; }        
        public string?  DmgType     { get; set; }
        public string?  Special     { get; set; }
        public int?     CritRng     { get; set; }
        public int?     Range       { get; set; }

        public string ToString(SizeType size)
        {
            var sb = new StringBuilder();
            var damage = "";
            switch(size)
            {
                case SizeType.Fine:
                    damage = Fine;
                    break;
                case SizeType.Diminutive:
                    damage = Diminutive;
                    break;
                case SizeType.Tiny:
                    damage = Tiny;
                    break;
                case SizeType.Small:
                    damage = Small;
                    break;
                case SizeType.Medium:
                    damage = Medium;
                    break;
                case SizeType.Large:
                    damage = Large;
                    break;
                case SizeType.Huge:
                    damage = Huge;
                    break;
                case SizeType.Gargantuan:
                    damage = Gargantuan;
                    break;
                case SizeType.Colossal:
                    damage = Colossal;
                    break;
            }
            sb.AppendLine($"__**{Name.ToUpper()}**__");
                sb.AppendLine($"**Damage** {damage}");
            if(Range > 0)
                sb.AppendLine($" **Range** {Range} **Type** {DmgType}");
            else
                sb.AppendLine($" **Type** {DmgType}");
            sb.AppendLine($" **Critical** {CritRng}/x{CritMul} ");
            if(Special != "") sb.AppendLine($"**Special** {Special}");
            sb.AppendLine();
            if(Description != "") sb.AppendLine(Description);
            return sb.ToString();
            
        }
        
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"__**{Name.ToUpper()}**__");           
            sb.AppendLine($"**Small** {Small}");
            sb.AppendLine($"**Medium** {Medium}");
            sb.AppendLine($"**Large** {Large}");          
            if(Range > 0)
                sb.AppendLine($" **Range** {Range} **Type** {DmgType}");
            else
                sb.AppendLine($" **Type** {DmgType}");
            sb.AppendLine($" **Critical** {CritRng}/x{CritMul} ");
            if(Special != "") sb.AppendLine($"**Special** {Special}");
            sb.AppendLine();
            if(Description != "") sb.AppendLine(Description);
            return sb.ToString();
        }
    }
}
