using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gellybeans.Expressions
{
    public class DiceMultiplierNode : ExpressionNode
    {
        DiceNode        lhs;
        ExpressionNode  rhs;

        public DiceMultiplierNode(DiceNode lhs, ExpressionNode rhs)
        {
            this.lhs = lhs;
            this.rhs = rhs;
        }

        public override int Eval(IContext ctx, StringBuilder sb)
        {
            var rhValue = rhs.Eval(ctx, sb);
            var lhValue = 0;
            if(lhs.Highest > 0 || lhs.Lowest > 0)
                for(int i = 0; i < rhValue; i++)
                    lhValue += lhs.Eval(ctx, sb);
            else
                lhValue = (lhs * rhValue).Eval(ctx, sb);
            
            return lhValue;
        }
    }
}
