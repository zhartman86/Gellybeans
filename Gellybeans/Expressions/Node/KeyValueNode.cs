
using System.Text;

namespace Gellybeans.Expressions
{ 
    public class KeyValueNode : ExpressionNode
    {
        readonly ExpressionNode key;
        readonly ExpressionNode value;
        
        Func<dynamic, dynamic, dynamic> op;

        public KeyValueNode(ExpressionNode key, ExpressionNode value, Func<dynamic, dynamic, dynamic> op)
        {
            this.key = key;
            this.value = value;
            this.op = op;
        }

        

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            var k = key.Eval(depth, caller, sb, ctx);
            var v = value.Eval(depth, caller, sb, ctx);

            return op(k, v);
        }

    }
}
