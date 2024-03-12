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

        public override dynamic Eval(IContext ctx, StringBuilder sb) =>
            new ExpressionValue(Expression);

        public override string ToString()
        {
            return Expression;
        }
    }
}
