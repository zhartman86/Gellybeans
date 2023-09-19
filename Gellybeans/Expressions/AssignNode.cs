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
            this.lhs        = lhs;
            this.rhs        = rhs;
            this.assignType = assignType;
        }

        public override int Eval(IContext ctx, StringBuilder sb)
        {
            if(assignType == TokenType.AssignExpr || assignType == TokenType.AssignAddExpr)
                return ctx.Assign(lhs, rhs != null ? ((StringNode)rhs).String : null!, assignType, sb);
            
            var rhValue = rhs.Eval(ctx, sb);
            var result  = ctx.Assign(lhs, rhValue.ToString(), assignType, sb);
            
            return result;
        }
    }
}
