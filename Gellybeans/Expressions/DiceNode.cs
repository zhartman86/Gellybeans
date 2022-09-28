using System.Text;

namespace Gellybeans.Expressions
{
    public class DiceNode : ExpressionNode
    {       
        int count;
        int sides;

        public int Reroll       { get; init; }  = 0;
        public int Keep         { get; init; }  = 0;

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
                var r = random.Next(1, sides + 1);
                
                if(Reroll > 0 && r <= Reroll)
                {
                    if(!rerolled)
                    {
                        rerolled = true;
                        rerolledResults += $"Rerolled:[{r}]";
                    }
                    else rerolledResults += $"[{r}]";
                    r = random.Next(1, sides + 1);                 
                }

                total += r;
                results.Add(r);            
            }             

            if(sb != null)
            {
                sb.Append(ToString() + ": ");
                for(int i = 0; i < results.Count; i++)
                    sb.Append($"[{results[i]}]");
                if(rerolled) sb.Append($" ({rerolledResults})");
                sb.AppendLine();
            }

            return total;
        }

        public override string ToString()
        {
            return count + "d" + sides;
        }
        
        public static DiceNode operator *(DiceNode node, int multiplier)
        {
            return new DiceNode(node.count * multiplier, node.sides) { Keep = node.Keep, Reroll = node.Reroll};
        }
    }
}
