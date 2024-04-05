using System.Text;

namespace Gellybeans.Expressions
{
    public class UnaryNode : ExpressionNode
    {
        readonly ExpressionNode node;

        Func<dynamic, dynamic> op;

        public UnaryNode(ExpressionNode node, Func<dynamic, dynamic> op)
        {
            this.node = node;
            this.op = op;
        }

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";
            
            var rhValue = node.Eval(depth: depth, caller: caller, sb: sb, ctx : ctx);
            
            var result = op(rhValue);
            return result;
        }
    }
}
