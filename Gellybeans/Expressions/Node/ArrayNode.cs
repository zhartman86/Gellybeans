using System.Security.Cryptography;
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
            Dictionary<string, int> keys = null!;

            if(Values == null || Values.Length == 0)
                array = Array.Empty<dynamic>();
            else
            {
                array = new dynamic[Values.Length];
                
                for(int i = 0; i < Values.Length; i++)
                {
                    var result = Values[i].Eval(depth: depth, caller: this, sb: sb, ctx: ctx);
                    if(result is KeyValuePairValue k)
                    {
                        keys ??= new Dictionary<string, int>();

                        keys.Add(k.Key, i);
                        array[i] = k.Value;
                    }
                    else
                        array[i] = result;
                }
                                                  
            }
            return new ArrayValue(array, keys);
        }
    }
}
