using Gellybeans.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace Gellybeans.Pathfinder
{
   public class Creature
    {
        public string? Visual               { get; set; } = "";
        public string? Name                 { get; set; } = "";
        public string? CR                   { get; set; } = "";
        public string? XP                   { get; set; } = "";
        public string? Alignment            { get; set; } = "";
        public string? Size                 { get; set; } = "";
        public string? Race                 { get; set; } = "";              
        public string? Type                 { get; set; } = "";
        public string? Init                 { get; set; } = "";
        public string? Senses               { get; set; } = "";
        public string? Aura                 { get; set; } = "";
        public string? AC                   { get; set; } = "";
        public string? ACArmor              { get; set; } = "";
        public string? ACDeflection         { get; set; } = "";
        public string? ACDodge              { get; set; } = "";
        public string? ACInsight            { get; set; } = "";
        public string? ACNatural            { get; set; } = "";
        public string? ACProfane            { get; set; } = "";
        public string? ACSacred             { get; set; } = "";
        public string? ACShield             { get; set; } = "";
        public string? HP                   { get; set; } = "";
        public string? Regen                { get; set; } = "";
        public string? Fort                 { get; set; } = "";
        public string? Ref                  { get; set; } = "";
        public string? Will                 { get; set; } = "";
        public string? SaveMods             { get; set; } = "";       
        public string? Defensive            { get; set; } = "";
        public string? Immune               { get; set; } = "";
        public string? DR                   { get; set; } = "";       
        public string? Resist               { get; set; } = "";
        public string? SR                   { get; set; } = "";
        public string? Weaknesses           { get; set; } = "";
        public string? Speed                { get; set; } = "";
        public string? MeleeOne             { get; set; } = "";
        public string? MeleeTwo             { get; set; } = "";
        public string? MeleeThree           { get; set; } = "";
        public string? MeleeFour            { get; set; } = "";
        public string? MeleeFive            { get; set; } = "";
        public string? RangedOne            { get; set; } = "";
        public string? RangedTwo            { get; set; } = "";
        public string? Space                { get; set; } = "";
        public string? Reach                { get; set; } = "";
        public string? SpecialAttacks       { get; set; } = "";
        public string? SpellLikeAbilities   { get; set; } = "";
        public string? SpellsKnown          { get; set; } = "";
        public string? SpellsPrepared       { get; set; } = "";
        public string? Domains              { get; set; } = "";
        public string? Str                  { get; set; } = "";
        public string? Dex                  { get; set; } = "";
        public string? Con                  { get; set; } = "";
        public string? Int                  { get; set; } = "";
        public string? Wis                  { get; set; } = "";
        public string? Cha                  { get; set; } = "";
        public string? BAB                  { get; set; } = "";
        public string? CMB                  { get; set; } = "";
        public string? CMD                  { get; set; } = "";
        public string? Feats                { get; set; } = "";
        public string? Skills               { get; set; } = "";
        public string? SkillsRacial         { get; set; } = "";
        public string? Languages            { get; set; } = "";
        public string? SQ                   { get; set; } = "";
        public string? SpecialAbilities     { get; set; } = "";
        public string? Source               { get; set; } = "";

        public ArrayValue ToArray()
        {
            var list = new List<dynamic>();

            list.Add(new KeyValuePairValue("DESCRIPTION",           new StringValue(Visual)));
            list.Add(new KeyValuePairValue("NAME",                  new StringValue(Name)));
            list.Add(new KeyValuePairValue("CR",                    new StringValue(CR)));
            list.Add(new KeyValuePairValue("XP",                    new StringValue(XP.Replace(",", ""))));
            if(Race != "") 
                list.Add(new KeyValuePairValue("RACE",              new StringValue(Race)));
            list.Add(new KeyValuePairValue("ALIGNMENT",             new StringValue(Alignment)));
            list.Add(new KeyValuePairValue("SIZE",                  new StringValue(Size)));

            switch(Size)
            {
                case "Fine":
                    break;
            }

            list.Add(new KeyValuePairValue("TYPE",                  new StringValue(Type)));
            list.Add(new KeyValuePairValue("SENSES",                new StringValue(Senses)));                           
            if(Aura != "") 
                list.Add(new KeyValuePairValue("AURA",              new StringValue(Aura)));

            var split = AC.Split(',');
            if(int.TryParse(split[0], out var outVar))    
                list.Add(new KeyValuePairValue("AC",                new Stat(outVar)));
            if(int.TryParse(split[1].Replace("touch", "").Replace(" ", ""), out outVar))
                list.Add(new KeyValuePairValue("TOUCH",             new Stat(outVar)));
            if(int.TryParse(split[2].Replace("flat-footed", "").Replace(" ", ""), out outVar))
                list.Add(new KeyValuePairValue("FLAT",              new Stat(outVar)));

            split = HP.Split(' ');
            if(int.TryParse(split[0], out outVar))
                list.Add(new KeyValuePairValue("HP",                new Stat(outVar)));

            if(Regen != "")
                list.Add(new KeyValuePairValue("REGEN", new StringValue(Regen)));

            return new ArrayValue(list.ToArray());

        }


        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"*{Visual}*");
            sb.AppendLine();
            sb.AppendLine($"**{Name,-30}** — **CR** {CR,30}");
            sb.AppendLine($"**XP** {XP}");
            if(Race != "") sb.AppendLine($"{Race}");
            sb.AppendLine($"{Alignment} {Size} {Type}");
            sb.AppendLine($"**Init** {Init} **Senses** {Senses}");
            if(Aura != $"**Aura** {Aura}");
            sb.AppendLine();
            sb.AppendLine($"**AC** {AC}");
            sb.Append($"**hp** {HP}");
            if(Regen != "") sb.Append($"; {Regen}");
            sb.AppendLine();
            sb.AppendLine($"**Fort** +{Fort} **Ref** +{Ref} **Will** +{Will}");      
            if(Defensive != "") sb.Append($"**Defensive** {Defensive}");
            if(DR != "") sb.Append($" **DR** {DR}");
            if(Immune != "") sb.Append($" **Immune** {Immune}");
            if(Resist != "") sb.Append($" **Resist** {Resist}");         
            sb.AppendLine();
            if(Weaknesses != "") sb.AppendLine($"**Weaknesses** {Weaknesses}");           
            sb.AppendLine($"**Speed** {Speed}");
            if(MeleeOne != "") sb.Append($"**Melee** {MeleeOne}");
            if(MeleeTwo != "") sb.Append($", {MeleeTwo}");
            if(MeleeThree != "") sb.Append($", {MeleeThree}");
            if(MeleeFour != "") sb.Append($", {MeleeFour}");
            if(MeleeFive != "") sb.Append($", {MeleeFive}");
            sb.AppendLine();
            if(RangedOne != "") sb.Append($"**Ranged** {RangedOne}");
            if(RangedTwo != "") sb.Append($", {RangedTwo}");
            sb.AppendLine();
            if(SpecialAttacks != "") sb.AppendLine($"**Special Attacks** {SpecialAttacks}");

            
            if(SpellLikeAbilities != "")
            {
                var regex = new Regex(@"([^ ]*[—])");
                var split = SpellLikeAbilities.Split('&', StringSplitOptions.RemoveEmptyEntries);

                for(int i = 0; i < split.Length; i++)
                {
                    var regSplit = regex.Split(split[i]);
                    sb.AppendLine($"**Spell-Like Abilities—{regSplit[0]}**");
                    for(int j = 1; j < regSplit.Length; j++)
                    {
                        sb.AppendLine($"    *{regSplit[j]}*{regSplit[j + 1]}");
                        j++;
                    }
                }              
            }
            
            if(SpellsPrepared != "")
            {
                var regex = new Regex(@"([^ ]*[—])");
                var split = regex.Split(SpellsPrepared);

                sb.AppendLine($"**{split[0]}**");
                for(int i = 1; i < split.Length; i++)
                {
                    sb.AppendLine($"    *{split[i]}*{split[i + 1]}");
                    i++;
                }
            }

            if(SpellsKnown != "")
            {
                var regex = new Regex(@"\[(.*?)\]");
                var split = regex.Split(SpellsKnown);

                sb.AppendLine($"**{split[0]}**");
                for(int i = 1; i < split.Length; i++)
                {
                    sb.AppendLine($"    *{split[i]}*{split[i + 1]}");
                    i++;
                }
            }

            sb.AppendLine();
            sb.AppendLine($"**Str** {(Str == null ? "—" : Str)} **Dex** {(Dex == null ? "—" : Dex)} **Con** {(Con == null ? "—" : Con)} **Int** {(Int == null ? "—" : Int)} **Wis** {(Wis == null ? "—" : Wis)} **Cha** {(Cha == null ? "—" : Cha)}");
            sb.AppendLine($"**BAB** +{BAB} **CMB** +{CMB} **CMD** +{CMD}");
            sb.AppendLine($"**Feats** {Feats}");
            sb.Append($"**Skills** {Skills}");
            if(SkillsRacial != "") sb.Append($"; **Racial Modifiers** {SkillsRacial}");
            sb.AppendLine();
            sb.AppendLine($"**Languages** {Languages}");

            return sb.ToString();
        }
        
        public string GetSmallBlock()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"**{Name,-30}** — **CR** {CR,30}");
            sb.AppendLine($"**Init** {Init} **Senses** {Senses}");
            if(Aura != $"**Aura** {Aura}") ;
            sb.AppendLine($"**AC** {AC}");
            sb.Append($"**hp** {HP}");
            if(Regen != "") sb.Append($"; {Regen}");
            sb.AppendLine();           
            sb.AppendLine($"**Fort** +{Fort} **Ref** +{Ref} **Will** +{Will}");
            if(Defensive != "") sb.Append($"**Defensive** {Defensive}");
            if(DR != "") sb.Append($" **DR** {DR}");
            if(Immune != "") sb.Append($" **Immune** {Immune}");
            if(Resist != "") sb.Append($" **Resist** {Resist}");
            if (Defensive != "" || DR != "" || Immune != "" || Resist != "") sb.AppendLine();
            if(Weaknesses != "") sb.AppendLine($"**Weaknesses** {Weaknesses}");
            sb.AppendLine($"**Speed** {Speed}");
            sb.AppendLine($"**Str** {(Str == null ? "—" : Str)} **Dex** {(Dex == null ? "—" : Dex)} **Con** {(Con == null ? "—" : Con)} **Int** {(Int == null ? "—" : Int)} **Wis** {(Wis == null ? "—" : Wis)} **Cha** {(Cha == null ? "—" : Cha)}");
            sb.AppendLine($"**BAB** +{BAB} **CMB** +{CMB} **CMD** +{CMD}");            
            if(SpecialAttacks != "") sb.AppendLine($"**Special Attacks** {SpecialAttacks}");
            if(SpellLikeAbilities != "")
            {
                var regex = new Regex(@"([^ ]*[—])");
                var split = SpellLikeAbilities.Split('&', StringSplitOptions.RemoveEmptyEntries);

                for(int i = 0; i < split.Length; i++)
                {
                    var regSplit = regex.Split(split[i]);
                    sb.AppendLine($"**Spell-Like Abilities—{regSplit[0]}**");
                    for(int j = 1; j < regSplit.Length; j++)
                    {
                        sb.AppendLine($"    *{regSplit[j]}*{regSplit[j + 1]}");
                        j++;
                    }
                }
            }

            if(SpellsPrepared != "")
            {
                var regex = new Regex(@"([^ ]*[—])");
                var split = regex.Split(SpellsPrepared);

                sb.AppendLine($"**{split[0]}**");
                for(int i = 1; i < split.Length; i++)
                {
                    sb.AppendLine($"    *{split[i]}*{split[i + 1]}");
                    i++;
                }
            }

            if(SpellsKnown != "")
            {
                var regex = new Regex(@"\[(.*?)\]");
                var split = regex.Split(SpellsKnown);

                sb.AppendLine($"**{split[0]}**");
                for(int i = 1; i < split.Length; i++)
                {
                    sb.AppendLine($"    *{split[i]}*{split[i + 1]}");
                    i++;
                }
            }
            sb.AppendLine($"**Feats** {Feats}");
            sb.Append($"**Skills** {Skills}");
            return sb.ToString();         
        }

        public string GetSpecialAbilities()
        {
            StringBuilder sb = null;
            if(SpecialAbilities != "")
            {               
                sb = new StringBuilder();
                var split = Regex.Split(SpecialAbilities!, @"([^.]*[(][ExSuSp]{1,2}[)])");                      
                sb.AppendLine("__**Special Abilities**__");
                
                for(int i = 0; i < split.Length; i++)
                    if(!string.IsNullOrWhiteSpace(split[i]))
                    {
                        sb.AppendLine($"**{split[i]}** {split[i + 1]}");
                        i++;
                    }                
                return sb.ToString();
            }
            else 
                return null!;
            
            
        }
    }
}
