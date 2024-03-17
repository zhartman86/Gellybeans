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

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            return new ExpressionValue(Expression);
        }
        

        public override string ToString()
        {
            return Expression;
        }
    }
}
