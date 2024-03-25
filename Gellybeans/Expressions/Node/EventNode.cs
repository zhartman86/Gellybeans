using System.Text;

namespace Gellybeans.Expressions
{
    public class EventNode : ExpressionNode
    {
        readonly ExpressionNode args;

        public EventNode(ExpressionNode args) =>
            this.args = args;


        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";                           
                       
            var result = args.Eval(depth, caller, sb, ctx);
            if(result is ArrayValue a)
                return new EventValue(a.Values);
                   
            return $"Expected array value. got {result.GetType()}";
        }


    }
}
