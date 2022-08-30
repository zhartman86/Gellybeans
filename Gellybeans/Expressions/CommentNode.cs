using System.Text;

namespace Gellybeans.Expressions
{
    public class CommentNode : ExpressionNode
    {
        string comment;

        public CommentNode(string comment)
        {
            this.comment = comment;
        }

        public override int Eval(IContext ctx, StringBuilder sb)
        {
            if(sb != null)
                sb.Append(comment);
            return 0;
        }
    }
}
