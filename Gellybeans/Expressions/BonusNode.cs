using System.Text;

namespace Gellybeans.Expressions
{
    public class BonusNode : ExpressionNode
    {
        string          lhs;
        string          bonusName;
        ExpressionNode  bonusType;
        ExpressionNode  bonusValue;
        TokenType       assignType;

        public BonusNode(string lhs, string bonusName, ExpressionNode bonusType, ExpressionNode bonusValue, TokenType assignType)
        {
            this.lhs        = lhs;
            this.bonusName  = bonusName;
            this.bonusType  = bonusType;
            this.bonusValue = bonusValue;
            this.assignType = assignType;
        }

        public override int Eval(IContext ctx, StringBuilder sb)
        {          
            var bType  = bonusType != null ? bonusType.Eval(ctx, sb) : 0;
            var bVal   = bonusValue != null ? bonusValue.Eval(ctx, sb) : 0;
            var result = ctx.Bonus(lhs, bonusName, bType, bVal, assignType, sb);
            return result;
        }
    }
}
