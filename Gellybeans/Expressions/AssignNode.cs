using System.Text;

namespace Gellybeans.Expressions
{
    public class AssignNode : ExpressionNode
    {
        readonly string          lhs;
        readonly ExpressionNode  rhs;
        readonly string          assignType;
        readonly IContext        ctx;
        StringBuilder   sb;     

        public AssignNode(string lhs, ExpressionNode rhs, string assignType, IContext ctx, StringBuilder sb = null!)
        {
            this.lhs        = lhs;
            this.rhs        = rhs;
            this.assignType = assignType;
            this.ctx        = ctx;
            this.sb         = sb;
        }

        public override int Eval() =>                
            ctx.AssignValue(lhs, rhs, assignType, sb);

        
    }
}
