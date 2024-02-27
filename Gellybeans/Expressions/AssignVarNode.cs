using System.Text;

namespace Gellybeans.Expressions
{
    public class AssignVarNode : ExpressionNode
    {
        readonly string identifier;
        readonly ExpressionNode assignment;

        readonly IContext ctx;
        readonly StringBuilder sb;

        public AssignVarNode(string identifier, ExpressionNode assignment, IContext ctx, StringBuilder sb)
        {
            this.identifier = identifier;
            this.assignment = assignment;
            this.ctx = ctx;
            this.sb = sb;
        }

        public override int Eval()
        {
            return ctx.Assign(identifier, assignment, sb);
        }

    }
}
