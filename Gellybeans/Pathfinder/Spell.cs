using System.Text;

namespace Gellybeans.Pathfinder
{
    public class Spell
    {
        public string? Name             { get; set; }
        public string? Properties       { get; set; }
        public string? Levels           { get; set; }
        public string? School           { get; set; }
        public string? Subschool        { get; set; }
        public string? Descriptor       { get; set; }
        public string? CastingTime      { get; set; }
        public string? Components       { get; set; }
        public string? Range            { get; set; }
        public string? Area             { get; set; }
        public string? Effect           { get; set; }
        public string? Targets          { get; set; }
        public string? Duration         { get; set; }
        public string? SavingThrow      { get; set; }
        public string? SpellResistance  { get; set; }
        public string? Domain           { get; set; }
        public string? Deity            { get; set; }
        public string? Description      { get; set; }


        public override string ToString()
        {
            var sb = new StringBuilder();            
            sb.AppendLine($"__**{Name}**__");
            sb.AppendLine($"**School** {School} {Subschool} {Descriptor}");
            sb.AppendLine($"**Level** {Levels}");
            sb.AppendLine($"**Casting Time** {CastingTime}");
            sb.AppendLine($"**Components** {Components}");
            sb.AppendLine($"**Range** {Range}");
            sb.AppendLine($"**Target** {Targets}");
            sb.AppendLine($"**Duration** {Duration}");
            sb.AppendLine($"**Saving Throw** {SavingThrow}; **Spell Resistance** {SpellResistance}");
            sb.AppendLine();
            sb.AppendLine(Description);
            return sb.ToString();
        }
    }
}
