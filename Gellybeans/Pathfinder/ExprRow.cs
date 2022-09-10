using System.Text;

namespace Gellybeans.Pathfinder
{ 
    public class ExprRow
    {
        public string       RowName { get; init; } = "NAME_ME";
        public List<Expr>   Set     { get; set; } = new List<Expr>();      

        public override string ToString()
        {   
            var sb = new StringBuilder();

            sb.Append("```");            
            for(int i = 0; i < Set.Count; i++)             
                sb.AppendLine(@$"|{Set[i].Name,-12} |{Set[i].Expression,-30}");               
            sb.Append("```");
            return sb.ToString();
        }
    }
}