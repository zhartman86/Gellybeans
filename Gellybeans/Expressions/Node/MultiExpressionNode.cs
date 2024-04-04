using System.Text;

namespace Gellybeans.Expressions
{
    public class MultiExpressionNode : ExpressionNode
    {
        ExpressionNode node;
        ExpressionNode next;

        public MultiExpressionNode(ExpressionNode node, ExpressionNode next)
        {
            this.node = node;
            this.next = next;
        }

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            if(node is ReturnNode ret)
                return ret.Eval(depth, caller, sb, ctx);
            else if(next is ReturnNode nret)
                return nret.Eval(depth, caller, sb, ctx);

            var nodeResult = node.Eval(depth, caller, sb, ctx);
            if(nodeResult is IReduce r)
                nodeResult = r.Reduce(depth, caller, sb, ctx);

            var nextResult = next.Eval(depth, caller, sb, ctx);
            if(nextResult is IReduce rr)
                nextResult = rr.Reduce(depth, caller, sb, ctx);

            return nextResult;
        }
    }
}
