using System.Text;

namespace Gellybeans.Expressions
{
    public class TernaryNode : ExpressionNode
    {
        ExpressionNode condition;
        ExpressionNode lhs;
        ExpressionNode rhs;

        Func<int, int, int, int> op;

        public TernaryNode(ExpressionNode condition, ExpressionNode lhs, ExpressionNode rhs, Func<int, int, int, int> op)
        {
            this.condition = condition;
            this.lhs = lhs;
            this.rhs = rhs;
            this.op = op;
        }

        public override int Eval(IContext ctx, StringBuilder sb)
        {
            var conValue = condition.Eval(ctx, sb);
            var lhValue = lhs.Eval(ctx, sb);
            var rhValue = rhs.Eval(ctx, sb);

            var result = op(conValue, lhValue, rhValue);
            return result;
        }
    }
}
