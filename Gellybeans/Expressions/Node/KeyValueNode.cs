
using System.Text;

namespace Gellybeans.Expressions
{ 
    public class KeyValueNode : ExpressionNode
    {
        readonly string key;
        readonly ExpressionNode value;

        public KeyValueNode(string key, ExpressionNode value)
        {
            this.key = key;
            this.value = value;
        }

        

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            var result = value is VarNode v ? v : value.Eval(depth, caller, sb, ctx);

            return new KeyValuePairValue(key, result);
        }

    }
}
