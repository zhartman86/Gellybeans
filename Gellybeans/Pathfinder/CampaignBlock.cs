using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gellybeans.Pathfinder
{
    public class CampaignBlock
    {
        public string CampaignName { get; set; } = "";
        
        public Guid         Id          { get; set; }
        public ulong        Owner       { get; set; }
        public List<ulong>  Players     { get; set; } = new List<ulong>();

        public int          CurrentPage { get; set; }
      

        public Dictionary<string, Dictionary<string, string>>   Articles    = new Dictionary<string, Dictionary<string, string>>();
        public List<string>                                     Journal     = new List<string>();


        public async Task AddEntryAsync(string entry) => await Task.Run(() => Journal.Add(entry)).ConfigureAwait(false);

        public void AddArticle(string category, string subject, string article)
        {
            if(!Articles.ContainsKey(category))
                Articles[category] = new Dictionary<string, string>();
            Articles[category][subject] = article;
        }
    
        public async Task<string> GetEntriesAsync()
        {
            var sb = new StringBuilder();          
               
            for(int i = 0; i < Journal.Count; i++)
                sb.AppendLine(Journal[i]);
            return await Task.FromResult(sb.ToString()).ConfigureAwait(false);
        }       
    }
}
