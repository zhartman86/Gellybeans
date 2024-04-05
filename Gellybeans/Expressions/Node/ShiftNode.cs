using System.Text;

namespace Gellybeans.Expressions
{
    public class ShiftNode : ExpressionNode
    {
        readonly ExpressionNode lhs;
        readonly ExpressionNode rhs;

        Func<dynamic, ExpressionNode, dynamic> op;

        public ShiftNode(ExpressionNode lhs, ExpressionNode rhs, Func<dynamic, ExpressionNode, dynamic> op)
        {
            this.lhs = lhs;
            this.rhs = rhs;
            this.op = op;
        }

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            var lhValue = lhs.Eval(depth: depth, caller: caller, sb: sb, ctx: ctx);
                     
            var result = op(lhValue, rhs);
            
            
            //if(lhs is VarNode v)
            //    ctx[v.VarName] = result;


            return result;
        }
    }
}
