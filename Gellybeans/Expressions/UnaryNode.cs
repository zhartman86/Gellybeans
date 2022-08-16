using System.Text;

namespace Gellybeans.Expressions
{
    public class UnaryNode : ExpressionNode
    {
        ExpressionNode rhs;
        
        Func<int, int> op;

        public UnaryNode(ExpressionNode rhs, Func<int, int> op)
        {
            this.rhs = rhs;
            this.op = op;
        }

        public override int Eval(IContext ctx, StringBuilder sb)
        {
            var rhValue = rhs.Eval(ctx, sb);
            
            var result = op(rhValue);

            return result;
        }
    }
}
