using Gellybeans.Expressions;
using System.Text;
using System.Text.RegularExpressions;

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
        public string? RangeVar         { get; set; }
        public string? Area             { get; set; }
        public string? AreaVar          { get; set; }
        public string? Targets          { get; set; }
        public string? TargetsVar       { get; set; }
        public string? Duration         { get; set; }
        public string? DurationVar      { get; set; }
        public string? SavingThrow      { get; set; }
        public string? SpellResistance  { get; set; }
        public string? Domain           { get; set; }
        public string? Deity            { get; set; }
        public string? Description      { get; set; }
        public string? DescriptionVar   { get; set; }
        public string? Formulae         { get; set; }
        public string? Source           { get; set; }

        static readonly Regex duration = new Regex(@"([0-9]{1,3})(.*)/level(.*)");
        static readonly Regex brackets = new Regex(@"\{.*?\}");

        public override string ToString()
        {
            var sb = new StringBuilder();            
            sb.AppendLine($"__**{Name}**__");
            sb.AppendLine($"*{Source}*");
            sb.AppendLine($"**School** {School} {Subschool} {Descriptor}");
            sb.AppendLine($"**Level** {Levels}");
            sb.AppendLine($"**Casting Time** {CastingTime}");
            sb.AppendLine($"**Components** {Components}");
            sb.AppendLine($"**Range** {Range}");
            if(Targets != "")   sb.AppendLine($"**Target** {Targets}");
            if(Area != "")      sb.AppendLine($"**Area** {Area}");
            sb.AppendLine($"**Duration** {Duration}");
            sb.AppendLine($"**Saving Throw** {SavingThrow}; **Spell Resistance** {SpellResistance}");
            sb.AppendLine();
            sb.AppendLine(Description);
            return sb.ToString();
        }
    
        public string ToCasterLevel(int cl)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"__**{Name}**__");
            sb.AppendLine($"*{Source}*");
            sb.AppendLine($"**Caster Level** {cl}");
            sb.AppendLine($"**School** {School} {Subschool} {Descriptor}");
            sb.AppendLine($"**Level** {Levels}");
            sb.AppendLine($"**Casting Time** {CastingTime}");
            sb.AppendLine($"**Components** {Components}");

            sb.Append("**Range** ");
            sb.AppendLine(brackets.Replace(RangeVar!, m =>
            {
                var str = m.Value.Trim(new char[] { '{', '}' }).Replace("CL", cl.ToString());
                return $"{Parser.Parse(str).Eval(null!, new StringBuilder())}";
            }));

            if(Targets != "")
            {
                sb.Append("**Target** ");
                sb.AppendLine(brackets.Replace(TargetsVar!, m =>
                {
                    var str = m.Value.Trim(new char[] { '{', '}' }).Replace("CL", cl.ToString());
                    return $"{Parser.Parse(str).Eval(null!, new StringBuilder())}";
                }));
            }
            
            if(AreaVar != "")
            {
                sb.Append("**Area** ");
                sb.AppendLine(brackets.Replace(AreaVar!, m =>
                {
                    var str = m.Value.Trim(new char[] { '{', '}' }).Replace("CL", cl.ToString());
                    return $"{Parser.Parse(str).Eval(null!, new StringBuilder())}";
                }));
            }       

            sb.Append("**Duration** ");
            sb.AppendLine(brackets.Replace(DurationVar!, m =>
            {
                var str = m.Value.Trim(new char[] { '{', '}' }).Replace("CL", cl.ToString());
                return $"{Parser.Parse(str).Eval(null!, new StringBuilder())}";
            }));
        

            sb.AppendLine($"**Saving Throw** {SavingThrow}; **Spell Resistance** {SpellResistance}");
            sb.AppendLine();
            
            //replace all bracketed statements
            sb.AppendLine(brackets.Replace(DescriptionVar!, m =>{
                var str = m.Value.Trim(new char[] { '{', '}' }).Replace("CL", cl.ToString());
                return $"**{Parser.Parse(str).Eval(null!, new StringBuilder())}**";
            }));
                       
            return sb.ToString();
        }
    }
}
