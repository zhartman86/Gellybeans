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

        public override dynamic Eval(IContext ctx, StringBuilder sb = null!)
        {
            ScopedContext scope;
            scope = new(ctx ?? null!, new() { { "P", value } });
            return next.Eval(scope, sb);
        }

    }
}
