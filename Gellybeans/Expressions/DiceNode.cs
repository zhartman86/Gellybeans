using System.Text;

namespace Gellybeans.Expressions
{
    public class DiceNode : ExpressionNode
    {       
        int count;
        int sides;

        public int Reroll       { get; set; } = 0;
        public int Highest      { get; set; } = 0;
        public int Lowest       { get; set; } = 0;

        public DiceNode(int count, int sides)
        {
            this.count = count;
            this.sides = sides;
        }

        public override int Eval(IContext ctx, StringBuilder sb)
        {
            var random = new Random();
            var results = new List<int>();
            int total = 0;

            bool rerolled = false;
            string rerolledResults = "";
            for(int i = 0; i < count; i++)
            {
                var r = sides == 0 ? 0 : random.Next(1, sides + 1);
                
                if(Reroll > 0 && r <= Reroll)
                {
                    if(!rerolled)
                    {
                        rerolled = true;
                        rerolledResults += $"Rerolled:[{r}]";
                    }
                    else if (i < 10) rerolledResults += $"[{r}]";
                    r = random.Next(1, sides + 1);                 
                }

                total += r;
                results.Add(r);            
            }
            

            List<int> dropped = new List<int>();
            if(Highest > 0)
            {
                var diff = results.Count - Highest;
                for(int i = 0; i < diff; i++)
                {
                    var drp = results.Min();
                    dropped.Add(drp);
                    total -= drp;
                    results.Remove(drp);
                }                  
            }
            else if(Lowest > 0)
            {
                var diff = results.Count - Lowest;
                for(int i = 0; i < diff; i++)
                {
                    var drp = results.Max();
                    dropped.Add(drp);
                    total -= drp;
                    results.Remove(drp);
                }
            }

            if(sb != null)
            {
                var resultsCap = Math.Min(results.Count, 30);
                var droppedCap = Math.Min(dropped.Count, 30);
                sb.Append(ToString() + ": ");
                for(int i = 0; i < resultsCap; i++)
                {
                    sb.Append($"[{results[i]}]");
                    if(i == 29)
                        sb.Append("...");
                }
                    
                for(int i = 0; i < droppedCap; i++)
                {
                    sb.Append($"~~[{dropped[i]}]~~");
                    if(i == 29)
                        sb.Append("...");
                }
                    
                sb.Append($" = {total}");
                if(rerolled) sb.Append($" <{rerolledResults}>");
                sb.AppendLine();
            }

            return total;
        }

        public override string ToString()
        {
            return count + "d" + sides;
        }
        
        public static DiceNode operator *(DiceNode node, int multiplier) =>
            new DiceNode(node.count * multiplier, node.sides) { Highest = node.Highest, Reroll = node.Reroll};
        
        public static DiceNode operator /(DiceNode node, int divisor) =>
            new DiceNode(node.count / divisor, node.sides) { Highest = node.Highest, Reroll = node.Reroll };
    }
}
