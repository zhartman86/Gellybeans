using System.Text;

namespace Gellybeans.Expressions
{
    public class UnaryNode : ExpressionNode
    {
        readonly ExpressionNode rhs;
        
        Func<dynamic, dynamic> op;

        public UnaryNode(ExpressionNode rhs, Func<dynamic, dynamic> op)
        {
            this.rhs = rhs;
            this.op = op;
        }

        public override dynamic Eval(IContext ctx, StringBuilder sb)
        {
            var rhValue = rhs.Eval(ctx, sb);           
            var result = op(rhValue);
            return result;
        }
    }
}
