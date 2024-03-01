using Gellybeans.Pathfinder;

namespace Gellybeans.Expressions
{
    public class StatNode : ExpressionNode
    {
        public Stat Stat { get; } = new Stat();

        public StatNode(int baseValue)
        {
            Console.WriteLine("creating stat");
            Stat.Base = baseValue;
            Console.WriteLine("done");
        }
            

        public override ValueNode Eval()
        {
            Console.WriteLine("stat");
            return Stat.Value;
        }

        public override string ToString()
        {
            return Stat.Value.ToString();
        }
    }   
}
