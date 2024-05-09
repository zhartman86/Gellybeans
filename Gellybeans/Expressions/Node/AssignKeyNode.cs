
using System.Text;

namespace Gellybeans.Expressions
{
    public class AssignKeyNode : ExpressionNode
    {
        readonly KeyNode key;
        readonly ExpressionNode assignment;
        readonly string opValue;

        public AssignKeyNode(KeyNode key, ExpressionNode assignment, string opValue)
        {
            this.key = key;
            this.assignment = assignment;
            this.opValue = opValue;
        }

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            var assign = assignment.Eval(depth, caller, sb, ctx);
            var k = key.Key.Eval(depth, caller, sb, ctx);
            var v = key.Value.Eval(depth, caller, sb, ctx);

            if(opValue == "=")
                v[k] = assign;
            else if (opValue == "+=")
                v[k] += assign;
            else if(opValue == "-=")
                v[k] -= assign;
            else if(opValue == "*=")
                v[k] *= assign;
            else if(opValue == "/=")
                v[k] /= assign;
            else if(opValue == "%=")
                v[k] %= assign;

            return v[k];
        }       
    }
}

