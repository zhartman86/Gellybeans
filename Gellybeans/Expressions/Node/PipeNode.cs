using System.Text;

namespace Gellybeans.Expressions
{
    public class PipeNode : ExpressionNode
    {

        readonly dynamic value;
        ExpressionNode next;

        public PipeNode(dynamic value, ExpressionNode next)
        {
            this.value = value;
            this.next = next;
        }

        public override dynamic Eval(int depth, IContext ctx, StringBuilder sb = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            ScopedContext scope;
            scope = new(ctx ?? null!, new() { { "P", value } });
            return next.Eval(depth, scope, sb);
        }

    }
}
