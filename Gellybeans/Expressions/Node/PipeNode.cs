using System.Text;

namespace Gellybeans.Expressions
{
    public class PipeNode : ExpressionNode
    {
        readonly ExpressionNode value;
        readonly ExpressionNode next;

        public PipeNode(ExpressionNode value, ExpressionNode next)
        {
            this.value = value;
            this.next = next;
        }

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            var result = value.Eval(depth, caller, sb, ctx);
            if(result is IReduce r)
                result = r.Reduce(depth, caller, sb, ctx);

            var scope = new ScopedContext(ctx, new Dictionary<string, dynamic> { { "_", result } });      
            return next.Eval(depth: depth, caller: this, sb: sb, ctx : scope);
        }

    }
}
