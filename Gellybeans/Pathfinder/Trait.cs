using Gellybeans.Expressions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gellybeans.Pathfinder
{
    class Trait
    {
        public readonly static List<Trait> traits = new List<Trait>();
        public static List<Trait> Traits { get { return traits; } }
        
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Category { get; set; }
        public string? Requirements { get; set; }
        public string? Description { get; set; }
        public string? Source { get; set; }

        
        static Trait()
        {
            Console.Write("Getting traits...");
            var traitJson = File.ReadAllText(@"E:\Pathfinder\PFData\Traits.json");
            traits = JsonConvert.DeserializeObject<List<Trait>>(traitJson)!;
            Console.WriteLine($"Items => {traits.Count}");
        }


        public ArrayValue ToArrayValue()
        {

            var list = new List<KeyValuePairValue>()
            {
                new KeyValuePairValue("NAME",   new StringValue(Name ?? "")),
                new KeyValuePairValue("TYPE",   new StringValue(Type ?? "")),
                new KeyValuePairValue("CAT",    new StringValue(Category ?? "")),               
                new KeyValuePairValue("DESC",   new StringValue(Description ?? "")),
            };

            if(Requirements != "")
            {
                var split = Requirements.Split(',');
                var reqs = new dynamic[split.Length];
                for(int i = 0; i < split.Length; i++)
                    reqs[i] = split[i].Trim();
                list.Add(new KeyValuePairValue("REQS", new ArrayValue(reqs)));
            }


            Dictionary<string, int> dict = new Dictionary<string, int>();
            dynamic[] array = new dynamic[list.Count];
            for(int i = 0; i < list.Count; i++)
            {
                dict.TryAdd(list[i].Key, i);
                array[i] = list[i].Value;
            }

            return new ArrayValue(array, dict);
        }

    }
}
