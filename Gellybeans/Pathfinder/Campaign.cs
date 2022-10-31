using System.Text;

namespace Gellybeans.Pathfinder
{
    public class Campaign
    {              
        public Guid Id      { get; set; }
        public ulong Owner  { get; set; }

        public string Name                          { get; set; }
        public string Description                   { get; set; } = "This campaign has no description.";
        public Dictionary<ulong, string> players    { get; set; } = new Dictionary<ulong, string>();

        public List<Creature>                                Bestiary    { get; set; } = new List<Creature>();       
        public List<StatBlock>                               StatBlocks  { get; set; } = new List<StatBlock>();
        public List<Item>                                    Items       { get; set; } = new List<Item>();
        public List<Shape>                                   Shapes      { get; set; } = new List<Shape>();        
        public List<Spell>                                   Spells      { get; set; } = new List<Spell>();     
        public Dictionary<string, List<(string, string)>>    Modifiers   { get; set; } = new Dictionary<string, List<(string, string)>>();

        public Dictionary<string, Stat>     Stats;
        public Dictionary<string, string>   Expressions;

        public string ListBestiary()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{"#",-2} |{"NAME",-40} |{"CR",-3} |{"TYPE"}");
            for(int i = 0; i < Bestiary.Count; i++)
                sb.AppendLine($"{i,-2} |{Bestiary[i].Name,-40} |{Bestiary[i].CR,-3} |{Bestiary[i].Type}");
            return sb.ToString();
        }
        public string ListItems()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{"#",-2} |{"NAME",-40} |{"TYPE"}");
            for(int i = 0; i < Items.Count; i++)
                sb.AppendLine($"{i,-2} |{Items[i].Name,-40} |{Items[i].Type}");
            return sb.ToString();
        }
        public string ListMods()
        {
            var sb = new StringBuilder();
            foreach(var mod in Modifiers)
                sb.AppendLine(mod.Key);
            return sb.ToString();
        }
        public string ListShapes()
        {
            var sb = new StringBuilder();
            for(int i = 0; i < Shapes.Count; i++)
                sb.AppendLine($"{i,-4} |{Shapes[i].Name,-25} |{Shapes[i].Type,-14}");
            return sb.ToString();
        }
        public string ListSpells()
        {
            var sb = new StringBuilder();
            for(int i = 0; i < Spells.Count; i++)
                sb.AppendLine($"{i,-4} |{Spells[i].Name,-25}");
            return sb.ToString();
        }
    }
}
