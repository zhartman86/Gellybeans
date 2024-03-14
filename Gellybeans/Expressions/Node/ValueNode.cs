using System.Text;

namespace Gellybeans.Expressions
{
    public class ValueNode : ExpressionNode
    {
        readonly dynamic value;

        public ValueNode(dynamic value) =>
            this.value = value;

        public override dynamic Eval(int depth, IContext ctx = null!, StringBuilder sb = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            return value;
        }

    }
}
