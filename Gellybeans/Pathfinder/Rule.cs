namespace Gellybeans.Pathfinder
{
    public class Rule
    {
        public string? Name         { get; set; }
        public string? Description  { get; set; }
        public string? Formulae     { get; set; }


        public override string ToString()
        {
            return
                $@"__{Name}__

{Description}";
        }
    }
}
