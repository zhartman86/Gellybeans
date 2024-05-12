using System.Text;

namespace Gellybeans.Expressions
{
    public class ValueNode : ExpressionNode
    {
        readonly dynamic value;
        readonly bool returnValue = false;

        public bool ReturnValue {  get { return returnValue; } }

        public ValueNode(dynamic value, bool returnValue = false)
        {
            this.value = value;
            this.returnValue = returnValue;
        }         

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            return value;
        }

    }
}
