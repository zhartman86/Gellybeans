using System.Text;

namespace Gellybeans.Expressions
{
    public class DiceMultiplierNode : ExpressionNode
    {
        DiceNode lhs;
        ExpressionNode rhs;
        TokenType token;

        public DiceMultiplierNode(DiceNode lhs, ExpressionNode rhs, TokenType token)
        {
            this.lhs = lhs;
            this.rhs = rhs;
            this.token = token;
        }

        public override dynamic Eval(int depth, IContext ctx, StringBuilder sb)
        {
            var rhValue = rhs.Eval(depth, ctx, sb);
            var lhValue = 0;
            if (lhs.Highest > 0 || lhs.Lowest > 0)
                for (int i = 0; i < rhValue; i++)
                    lhValue += lhs.Eval(depth, ctx, sb);
            else
            {
                if (token == TokenType.Mul)
                    lhValue = (lhs * rhValue).Eval(depth, ctx, sb);
                else if (token == TokenType.Div)
                    lhValue = (lhs / rhValue).Eval(depth, ctx, sb);
            }

            return lhValue;
        }
    }
}
