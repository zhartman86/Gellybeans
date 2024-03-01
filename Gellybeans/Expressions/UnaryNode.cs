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

        public override ValueNode Eval()
        {
            var rhValue = rhs.Eval();           
            var result = op(rhValue);
            return result;
        }
    }
}
