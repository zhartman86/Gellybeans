using System.Text;

namespace Gellybeans.Expressions
{
    public class ArrayNode : ExpressionNode
    {
        ExpressionNode[] Values { get; set; }

        public ArrayNode(ExpressionNode[] values) =>
            Values = values;

        public override dynamic Eval(int depth, IContext ctx, StringBuilder sb)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";
            
            var array = new dynamic[Values.Length];
            for (int i = 0; i < Values.Length; i++)
                array[i] = Values[i].Eval(depth, ctx, sb);

            return new ArrayValue(array);
        }
    }
}
