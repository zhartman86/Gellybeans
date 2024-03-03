using System.Text;

namespace Gellybeans.Expressions
{
    public class StoredExpressionNode : ExpressionNode
    {
        public string Expression { get; }

        public StoredExpressionNode(string expr)
        {
            Expression = expr;
        }
            
        public override ValueNode Eval(IContext ctx, StringBuilder sb)
        {
            return new ExpressionValue(Expression);
        }

        public override string ToString()
        {
            return Expression;
        }
    }
}
