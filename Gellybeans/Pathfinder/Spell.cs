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

        
        //these regex are used for formulae placed inside squiggle-brackets in a data-set created specifically for these purposes.
        //anything inside these brackets are meant to be evaluated (and the brackets removed) before being displayed to the end-user.
        static readonly Regex intensifiedDesc       = new Regex(@"(\{min\(.CL,)([0-9]*)(\)\}d)");
        static readonly Regex intensifiedFormulae   = new Regex(@"([0-9]{1,2}d[0-9]{1,2}.*min\(.CL,)([0-9]*)");
        static readonly Regex empoweredDesc         = new Regex(@"(\{.*\}?d[0-9]*.*?[ +]*[0-9]+?|[0-9]+d[0-9]+[ +]*[0-9]*)");
        static readonly Regex empoweredFormulae     = new Regex(@"#([0-9]d.*)");
        static readonly Regex doubled = new Regex(@"\{(.*)\}");
        static readonly Regex brackets = new Regex(@"\{.*?\}");

        
        public Spell Empowered()
        {
            Spell s = Copy();
            s.DescriptionVar = empoweredDesc.Replace(s.DescriptionVar!, "($1)**+50%**");
            s.Formulae = empoweredFormulae.Replace(s.Formulae!, "#th($1)");
            return s;
        }
        
        public Spell Enlarged()
        {
            Spell s = Copy();
            s.RangeVar = doubled.Replace(s.RangeVar!, "{($1)*2}");
            return s;
        }
        
        public Spell Extended()
        {
            Spell s = Copy();
            s.DurationVar = doubled.Replace(s.DurationVar!, "{($1)*2}");
            return s;
        }

        public Spell Intensified()
        {
            Spell s = Copy();
            s.DescriptionVar = intensifiedDesc.Replace(s.DescriptionVar!, "$1$2+5$3");
            s.Formulae = intensifiedFormulae.Replace(s.Formulae!, "$1$2+5");
            return s;
        }


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
    
        public string ToCasterLevel(uint cl)
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
                var str = m.Value.Trim(new char[] { '{', '}' }).Replace(".CL", cl.ToString());
                return $"{Parser.Parse(str).Eval(null!, new StringBuilder())}";
            }));

            if(Targets != "")
            {
                sb.Append("**Target** ");
                sb.AppendLine(brackets.Replace(TargetsVar!, m =>
                {
                    var str = m.Value.Trim(new char[] { '{', '}' }).Replace(".CL", cl.ToString());
                    return $"{Parser.Parse(str).Eval(null!, new StringBuilder())}";
                }));
            }
            
            if(AreaVar != "")
            {
                sb.Append("**Area** ");
                sb.AppendLine(brackets.Replace(AreaVar!, m =>
                {
                    var str = m.Value.Trim(new char[] { '{', '}' }).Replace(".CL", cl.ToString());
                    return $"{Parser.Parse(str).Eval(null!, new StringBuilder())}";
                }));
            }       

            sb.Append("**Duration** ");
            sb.AppendLine(brackets.Replace(DurationVar!, m =>
            {
                var str = m.Value.Trim(new char[] { '{', '}' }).Replace(".CL", cl.ToString());
                return $"{Parser.Parse(str).Eval(null!, new StringBuilder())}";
            }));
        

            sb.AppendLine($"**Saving Throw** {SavingThrow}; **Spell Resistance** {SpellResistance}");
            sb.AppendLine();
             
            

            sb.AppendLine(brackets.Replace(DescriptionVar!, m =>{
                var str = m.Value.Trim(new char[] { '{', '}' }).Replace(".CL", cl.ToString());
                return $"**{Parser.Parse(str).Eval(null!, new StringBuilder())}**";
            }));
                       
            return sb.ToString();
        }

        Spell Copy()
        {
            return new Spell()
            {
                Name = Name,
                Properties = Properties,
                Levels = Levels,
                School = School,
                Subschool = Subschool,
                Descriptor = Descriptor,
                CastingTime = CastingTime,
                Components = Components,
                Range = Range,
                RangeVar = RangeVar,
                Area = Area,
                AreaVar = AreaVar,
                Targets = Targets,
                TargetsVar = TargetsVar,
                Duration = Duration,
                DurationVar = DurationVar,
                SavingThrow = SavingThrow,
                SpellResistance = SpellResistance,
                Domain = Domain,
                Deity = Deity,
                Description = Description,
                DescriptionVar = DescriptionVar,
                Formulae = Formulae,
                Source = Source
            };
        }
    }
}
