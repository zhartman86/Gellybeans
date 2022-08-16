using System.Text;

namespace Gellybeans.Expressions
{
    public class AssignNode : ExpressionNode
    {
        string          lhs;
        ExpressionNode  rhs;
        TokenType       assignType;

        public AssignNode(string lhs, ExpressionNode rhs, TokenType assignType)
        {
            this.lhs = lhs;
            this.rhs = rhs;
            this.assignType = assignType;
        }

        public override int Eval(IContext ctx, StringBuilder sb)
        {
            var rhValue = rhs.Eval(ctx, sb);
            var result = ctx.Assign(lhs, rhValue, assignType, sb);
            
            return result;
        }
    }
}
