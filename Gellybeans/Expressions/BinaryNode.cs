using System.Text;

namespace Gellybeans.Expressions
{
    public class BinaryNode : ExpressionNode
    {
        ExpressionNode lhs;
        ExpressionNode rhs;

        Func<int, int, int> op;

        public BinaryNode(ExpressionNode lhs, ExpressionNode rhs, Func<int, int, int> op)
        {
            this.lhs = lhs;
            this.rhs = rhs;
            this.op = op;
        }

        public override int Eval(IContext ctx, StringBuilder sb)
        {
            var lhValue = lhs.Eval(ctx, sb);
            var rhValue = rhs.Eval(ctx, sb);
                       
            var result = op(lhValue, rhValue);
                                
            return result;
        }
    }
}
