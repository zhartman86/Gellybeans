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

        public override int Eval()
        {
            next.Eval();
            return 0;
        }
    }
}
