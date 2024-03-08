using System.Text;

namespace Gellybeans.Expressions
{
    public class GroupNode : ExpressionNode
    {
        readonly List<ExpressionNode> expressions;

        public List<ExpressionNode> Expressions { get { return expressions; } }

        public GroupNode(List<ExpressionNode> expressions)
        {
            this.expressions = expressions;
        }

        public override dynamic Eval(IContext ctx, StringBuilder sb)
        {
            for (int i = 0; i < expressions.Count; i++)
            {
                var result = expressions[i].Eval(ctx, sb);
                sb?.AppendLine($"**Total:** {result}\r\n");
            }
            
            return 0;
        }
    }
}
