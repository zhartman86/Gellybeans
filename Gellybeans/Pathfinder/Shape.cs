using System.Text;

namespace Gellybeans.Pathfinder
{
    public class Shape
    {
        public string? Name     { get; set; }
        public string? Type     { get; set; }
        public string? Size     { get; set; }
        public string? Speed    { get; set; }
        public string? Senses   { get; set; }
        public string? Specials { get; set; }
        public string? Bite     { get; set; }
        public string? Claws    { get; set; }
        public string? Gore     { get; set; }
        public string? Hoof     { get; set; }
        public string? Tentacle { get; set; }
        public string? Wing     { get; set; }
        public string? Pincers  { get; set; }
        public string? Tail     { get; set; }
        public string? Slam     { get; set; }
        public string? Sting    { get; set; }
        public string? Talons   { get; set; }
        public string? Other    { get; set; }
        public string? Breath   { get; set; }


        public override string ToString()
        {
            var sb = new StringBuilder();            
            var list = new List<(string,string)>();
            
            if(Bite != "")        list.Add(("bite",     Bite!    ));
            if(Claws != "")       list.Add(("claw",     Claws!   ));
            if(Gore != "")        list.Add(("gore",     Gore!    ));
            if(Slam != "")        list.Add(("slam",     Slam!    ));
            if(Sting != "")       list.Add(("sting",    Sting!   ));
            if(Talons != "")      list.Add(("talon",    Talons!  ));
            if(Hoof != "")        list.Add(("hoof",     Hoof!    ));
            if(Tentacle != "")    list.Add(("tentacle", Tentacle!));
            if(Wing != "")        list.Add(("wing",     Wing!    ));
            if(Pincers != "")     list.Add(("pincer",   Pincers! ));
            if(Tail != "")        list.Add(("tail",     Tail!    ));

            if(Other != "")
            {
                var oSplit = Other!.Split('/');
                for(int i = 0; i < oSplit.Length; i++)
                {
                    var split = oSplit[i].Split(':');
                    if(split.Length > 2)
                        list.Add((split[1], split[2]));
                    else if(split.Length > 1)
                        list.Add((split[0], split[1]));
                    else
                        list.Add(("other", split[0]));
                }
            }

            sb.AppendLine($"__**{Name}**__");
            sb.Append("**Attacks** ");
            for(var i = 0; i < list.Count; i++)
                sb.Append($"{list[i].Item1} ({list[i].Item2}); ");
            
            sb.AppendLine();

            var splits = Speed!.Split('/');
            sb.Append("**Speed** ");
            for(int i = 0; i < splits.Length; i++)
                sb.Append($"{splits[i]}; ");
            sb.AppendLine();
            
            splits = Senses!.Split('/');
            sb.Append("**Senses** ");
            for(int i = 0; i < splits.Length; i++)
                sb.Append($"{splits[i]}; ");
            sb.AppendLine();

            if(Specials != "")
            {
                splits = Specials!.Split('/');
                sb.Append("**Special** ");
                for(int i = 0; i < splits.Length; i++)
                    sb.Append($"{splits[i]}; ");
            }
            return sb.ToString();
        }
    }
}
