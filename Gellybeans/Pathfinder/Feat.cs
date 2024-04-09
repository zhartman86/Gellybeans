using Gellybeans.Expressions;
using Newtonsoft.Json;

namespace Gellybeans.Pathfinder
{
    public class Feat
    {
        public static readonly List<Feat> feats = new List<Feat>();
        public static List<Feat> Feats { get { return feats; }  }
        
        public string? Name             { get; set; }
        public string? Type             { get; set; }
        public string? Prerequisites    { get; set; }
        public string? Description      { get; set; }
        public string? Benefit          { get; set; }
        public string? Normal           { get; set; }
        public string? Special          { get; set; }
        public string? Source           { get; set; }
    
        static Feat()
        {
            Console.Write("Getting feats...");
            var featJson = File.ReadAllText(@"E:\Pathfinder\PFData\Feats.json");
            feats = JsonConvert.DeserializeObject<List<Feat>>(featJson)!;
            Console.WriteLine($"Feats => {feats.Count}");
        }
    
        public ArrayValue ToArrayValue()
        {
            var list = new List<KeyValuePairValue>
            {
                new KeyValuePairValue("NAME", Name),
                new KeyValuePairValue("TYPE", Type),               
            };

            if(Prerequisites != "")
            {
                var split = Prerequisites.Split(',');
                var reqs = new dynamic[split.Length];
                for(int i = 0; i < split.Length; i++)
                    reqs[i] = split[i].Trim();
                list.Add(new KeyValuePairValue("PREREQS", new ArrayValue(reqs)));
            }
            
            list.Add(new KeyValuePairValue("DESC", Description));
            list.Add(new KeyValuePairValue("BENEFIT", Benefit));
            list.Add(new KeyValuePairValue("SOURCE", Source));

            if(Normal != "")
                list.Add(new KeyValuePairValue("NORMAL", Normal));
            if(Special != "")
                list.Add(new KeyValuePairValue("SPECIAL", Normal));

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
