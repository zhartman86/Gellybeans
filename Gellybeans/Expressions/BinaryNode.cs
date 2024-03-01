using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using System.Text;

namespace Gellybeans.Expressions
{
    public class BinaryNode : ExpressionNode
    {
        ExpressionNode lhs;
        ExpressionNode rhs;

        Func<ValueNode, ValueNode, ValueNode> op;

        public BinaryNode(ExpressionNode lhs, ExpressionNode rhs, Func<ValueNode, ValueNode, ValueNode> op)
        {
            this.lhs    = lhs;
            this.rhs    = rhs;
            this.op     = op;
        }


        public override ValueNode Eval()
        {           
            var lhValue = lhs.Eval();
            var rhValue = rhs.Eval();

            var result = op(lhValue, rhValue);           
                                
            return result;
        }
    }
}
