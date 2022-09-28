namespace Gellybeans.Pathfinder
{    
    public class Template
    {
        public string Name  { get; set; }
        public string Notes { get; set; } = "";
        
        public Dictionary<string, Stat>     Stats           { get; set; } = new Dictionary<string, Stat>();
        public Dictionary<string, string>   Expressions     { get; set; } = new Dictionary<string, string>();
        public List<string> Features;

        public static Dictionary<string, Template> Templates = new Dictionary<string, Template>()
        {
            { "PSYCHIC", new Template() {
                    Stats = new Dictionary<string, Stat>()
                    {
                        ["CL_PSYCHIC"] = 1,    
                    },
                    Expressions = new Dictionary<string, string>()
                    {

                    },
                   
                }
            }
        };
    }
}
