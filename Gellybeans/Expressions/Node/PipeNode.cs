using System.Text;

namespace Gellybeans.Expressions
{
    public class PipeNode : ExpressionNode
    {

        readonly dynamic value;
        ExpressionNode next;
        IContext context;

        public PipeNode(dynamic value, ExpressionNode next, IContext context)
        {
            this.value = value;
            this.next = next;
            this.context = context;
        }

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            
            return next.Eval(depth: depth, caller: this, sb: sb, ctx : ctx);
        }

    }
}
