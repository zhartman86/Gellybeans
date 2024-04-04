using System.Text;

namespace Gellybeans.Expressions
{
    public class ArrayNode : ExpressionNode
    {
        ExpressionNode[] Values { get; set; }

        public ArrayNode(ExpressionNode[] values) =>
            Values = values;

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            dynamic[] array;
            
            if(Values == null || Values.Length == 0)
                array = Array.Empty<dynamic>();
            else
            {
                array = new dynamic[Values.Length];
                for(int i = 0; i < Values.Length; i++)
                {
                    dynamic assign;
                    if(Values[i] is StoredExpressionNode sen)
                        assign = sen.Assign();
                    else
                        assign = Values[i].Eval(depth: depth, caller: caller, sb: sb, ctx: ctx);

                    array[i] = assign;
                }                   
            }
            return new ArrayValue(array);
        }
    }
}
