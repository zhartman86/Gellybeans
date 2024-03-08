using System.Text;

namespace Gellybeans.Expressions
{
    public class TernaryNode : ExpressionNode
    {
        ExpressionNode condition;
        ExpressionNode lhs;
        ExpressionNode rhs;

        Func<dynamic, dynamic, dynamic, dynamic> op;

        public TernaryNode(ExpressionNode condition, ExpressionNode lhs, ExpressionNode rhs, Func<dynamic, dynamic, dynamic, dynamic> op)
        {
            this.condition = condition;
            this.lhs = lhs;
            this.rhs = rhs;
            this.op = op;
        }

        public override dynamic Eval(IContext ctx, StringBuilder sb)
        {
            dynamic lhValue = 0;
            dynamic rhValue = 0;

            var conValue = condition.Eval(ctx,sb);

            if(conValue == 1)
            {
                lhValue = lhs.Eval(ctx, sb);
            }
               
                
            
            if(conValue == 0)
                rhValue = rhs.Eval(ctx, sb);              

            var result = op(conValue, lhValue, rhValue);
            return result;
        }
    }
}
