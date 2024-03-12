using System.Text;

namespace Gellybeans.Expressions
{
    public class ArrayNode : ExpressionNode
    {
        ExpressionNode[] Values { get; set; }

        public ArrayNode(ExpressionNode[] values) =>
            Values = values;

        public override dynamic Eval(IContext ctx, StringBuilder sb)
        {
            var array = new dynamic[Values.Length];
            for (int i = 0; i < Values.Length; i++)
                array[i] = Values[i].Eval(ctx, sb);

            return new ArrayValue(array);
        }
    }
}
