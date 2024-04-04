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

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            var rhValue = rhs.Eval(depth: depth, caller: this, sb: sb, ctx : ctx);
            var lhValue = 0;
            if (lhs.Highest > 0 || lhs.Lowest > 0)
                for (int i = 0; i < rhValue; i++)
                    lhValue += lhs.Eval(depth: depth, caller: this, sb: sb, ctx : ctx);
            else
            {
                if (token == TokenType.Mul)
                    lhValue = (lhs * rhValue).Eval(depth: depth, caller: this, sb: sb, ctx : ctx);
                else if (token == TokenType.Div)
                    lhValue = (lhs / rhValue).Eval(depth: depth, caller: this, sb: sb, ctx : ctx);
            }

            return lhValue;
        }
    }
}
