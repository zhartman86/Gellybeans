using System.Text;

namespace Gellybeans.Expressions
{
    public class UnaryNode : ExpressionNode
    {
        readonly ExpressionNode node;

        Func<dynamic, dynamic> op;

        public UnaryNode(ExpressionNode node, Func<dynamic, dynamic> op)
        {
            this.node = node;
            this.op = op;
        }

        public override dynamic Eval(int depth, IContext ctx, StringBuilder sb)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            var rhValue = node.Eval(depth, ctx, sb);
            if (rhValue is IReduce r)
                rhValue = r.Reduce(depth, ctx, sb);
            var result = op(rhValue);
            return result;
        }
    }
}
