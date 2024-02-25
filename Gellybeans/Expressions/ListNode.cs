using System.Text;

namespace Gellybeans.Expressions
{
    public class ListNode : ExpressionNode
    {
        readonly List<ExpressionNode> expressions;
        StringBuilder sb;

        public List<ExpressionNode> Expressions { get { return expressions; } }

        public ListNode(List<ExpressionNode> expressions, StringBuilder sb = null!)
        {
            this.expressions = expressions;
            this.sb = sb;
        }

        public override int Eval()
        {
            for (int i = 0; i < expressions.Count; i++)
            {
                var result = expressions[i].Eval();
                sb?.AppendLine($"**Total:** {result}\r\n");
            }
            
            return 0;
        }
    }
}
