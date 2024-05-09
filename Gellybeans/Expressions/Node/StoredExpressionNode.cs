using System.Reflection;
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

        public dynamic Reduce(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";


            var result = Parser.Parse(Expression, caller, sb, ctx).Eval(depth: depth, caller: this, sb: sb, ctx: ctx);
            while(result is IReduce r)
                result = r.Reduce(depth,caller, sb, ctx);
            return result;
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
