using System.Text;

namespace Gellybeans.Pathfinder
{
    public class Armor
    {
        public string?  Name        { get; set; }
        public int?     Cost        { get; set; }
        public int?     ArmorBonus  { get; set; }
        public int?     ShieldBonus { get; set; }
        public int?     MaxDex      { get; set; }
        public int?     Penalty     { get; set; }
        public int?     Failure     { get; set; }
        public int?     Weight      { get; set; }
        public string?  Type        { get; set; }
        public string?  Description { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"__**{Name}**__");
            sb.AppendLine($"**Cost** {Cost}; **Weight** {Weight}");
            sb.AppendLine($"{(ArmorBonus > 0 ? "**Armor Bonus**" : "**Shield Bonus**")} {(ArmorBonus > 0 ? ArmorBonus : ShieldBonus)}; **Max Dex** {(MaxDex != null ? MaxDex.ToString() : "—")}; **Penalty** {Penalty}");
            sb.AppendLine();
            sb.AppendLine($"{Description}");
            return sb.ToString();
        }
    }
}
