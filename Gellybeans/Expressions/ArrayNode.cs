using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gellybeans.Expressions
{
    public class ArrayNode : ExpressionNode
    {
        ExpressionNode[] Values { get; set; }

        public ArrayNode(ExpressionNode[] values) =>
            Values = values;

        public override dynamic Eval(IContext ctx, StringBuilder sb)
        {
            Console.WriteLine("array");
            var array = new dynamic[Values.Length];
            for(int i = 0; i < Values.Length; i++)
            {
                array[i] = Values[i].Eval(ctx, sb);
            }
            Console.WriteLine("returning");
            return new ArrayValue(array);
        }
    }
}
