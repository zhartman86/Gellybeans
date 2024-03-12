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

        public override dynamic Eval(int depth, IContext ctx, StringBuilder sb)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            next.Eval(depth, ctx, sb);
            return 0;
        }
    }
}
