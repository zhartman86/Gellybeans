using System.Text;

namespace Gellybeans.Expressions
{
    public class StoredExpressionNode : ExpressionNode
    {
        public string Expression { get; }
        IContext ctx;

        public StoredExpressionNode(string expr, IContext ctx = null!)
        {
            Expression = expr;
            this.ctx = ctx;
        }
            

        public override ValueNode Eval()
        {
            return Parser.Parse(Expression, ctx).Eval();
        }
    }
}
