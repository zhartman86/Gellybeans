using System.Text;

namespace Gellybeans.Expressions
{
    public class MemberNode : ExpressionNode
    {
        readonly string identifier;
        readonly ExpressionNode member;
        readonly IContext ctx;
        readonly StringBuilder sb;

        public MemberNode(string identifier, ExpressionNode member, IContext ctx, StringBuilder sb = null!)
        {
            this.identifier = identifier;
            this.member = member;
            this.ctx = ctx;
            this.sb = sb;
        }

        public override int Eval()
        {
            var mbr = member.Eval();
            return ctx.Bonus(identifier, "", mbr, 0, "$?", sb);
        }
    }
}
