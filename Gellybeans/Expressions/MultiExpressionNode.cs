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

        public override int Eval(IContext ctx = null, StringBuilder sb = null)
        {
            node.Eval(ctx, sb);
            next.Eval(ctx, sb);
            return 0;
        }
    }
}
