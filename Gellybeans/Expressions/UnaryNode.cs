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

        public override dynamic Eval(IContext ctx, StringBuilder sb)
        {
            var rhValue = node.Eval(ctx, sb);           
            var result = op(rhValue);
            return result;
        }
    }
}
