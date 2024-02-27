using System.Text;

namespace Gellybeans.Expressions
{
    public class StoredExpressionNode : ExpressionNode
    {
        public string Expression { get; }

        public StoredExpressionNode(string expr, IContext ctx = null!, StringBuilder sb = null!) =>
            Expression = expr;

        public override int Eval() => 0;
    }
}
