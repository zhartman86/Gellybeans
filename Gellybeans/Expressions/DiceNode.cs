using System.Text;

namespace Gellybeans.Expressions
{
    public class DiceNode : ExpressionNode
    {
        int count;
        int sides;

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
            for(int i = 0; i < count; i++)
            {
                var r = random.Next(1, sides + 1);
                total += r;
                results.Add(r);
            }

            if(sb != null)
            {
                sb.AppendLine();
                sb.Append(ToString() + ": ");
                for(int i = 0; i < results.Count; i++)
                {
                    sb.Append($"[{results[i]}]");
                }
            }


            return total;
        }

        public override string ToString()
        {
            return count + "d" + sides;
        }
    }
}
