namespace Gellybeans.Pathfinder
{
    public class Modifier
    {
        public string Source { get; set; }
    }

    public class StatModifier : Modifier
    {
        public string StatName { get; set; }
        public Bonus Bonus { get; set; }
    }
}
