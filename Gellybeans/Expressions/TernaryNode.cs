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

        public override int Eval()
        {
            int lhValue = 0;
            int rhValue = 0;
            var conValue = condition.Eval();
            if(conValue == 1)
                lhValue = lhs.Eval();
            
            if(conValue == 0)
                rhValue = rhs.Eval();

            var result = op(conValue, lhValue, rhValue);
            return result;
        }
    }
}
