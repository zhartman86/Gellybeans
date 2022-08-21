using System.Text;

namespace Gellybeans.Expressions
{
    public class AssignBonusNode : ExpressionNode
    {
        string          lhs;
        string          bonusName;
        ExpressionNode  bonusType;
        ExpressionNode  bonusValue;
        TokenType       assignType;

        public AssignBonusNode(string lhs, string bonusName, ExpressionNode bonusType, ExpressionNode bonusValue, TokenType assignType)
        {
            this.lhs        = lhs;
            this.bonusName  = bonusName;
            this.bonusType  = bonusType;
            this.bonusValue = bonusValue;
            this.assignType = assignType;
        }

        public override int Eval(IContext ctx, StringBuilder sb)
        {
            
            var bType   = bonusType.Eval(ctx, sb);
            var bVal    = bonusValue.Eval(ctx, sb);
            var result  = ctx.AssignBonus(lhs, bonusName, bType, bVal, assignType, sb);

            return result;
        }
    }
}
