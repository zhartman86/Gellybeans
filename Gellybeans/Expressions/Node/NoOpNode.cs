using System.Text;

namespace Gellybeans.Expressions
{
    public class NoOpNode : ExpressionNode, IReduce
    {
        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null)
        {
            return null!;
        }

        public dynamic Reduce(int depth, object caller, StringBuilder sb, IContext ctx) => 0;

        public override string ToString() => 
            string.Empty;
    }
}
