using System.Collections.Generic;
using System.Text;

namespace Gellybeans.Expressions
{
    public class KeyNode : ExpressionNode
    {
        public ExpressionNode Value { get; }
        public ExpressionNode Key { get; }

        public KeyNode(ExpressionNode value, ExpressionNode key)
        {
            Value = value;
            Key = key;
        }

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            var value = Value.Eval(depth, caller, sb, ctx);
            var result = Key.Eval(depth, caller, sb, ctx);

            if (value is ArrayValue a)
            {
                if(result is SymbolNode symbol)
                {
                    if(symbol.Symbol == ".^")
                        return a[Random.Shared.Next(0, a.Values.Length)];
                    if(symbol.Symbol == "^^")
                        return a.Values.Length;
                }

                if(result is RangeValue r)
                {

                    var start = r.Lower;
                    if(start < 0)
                        start = a.Values.Length + start;
                    var end = r.Upper;
                    if (end < 0)
                        end = a.Values.Length + end;

                    if(start < 0 || start >= a.Values.Length || end < 0 || end >= a.Values.Length)
                        return new StringValue($"Invalid range `[{r}]` for this array.");                   
                    else
                    {
                        var list = new List<dynamic>();
                        if(start > end)
                        {
                            for(int i = start; i >= end; i--)
                                list.Add(a[i]);
                        }
                        else
                        {
                            for(int i = start; i <= end; i++)
                                list.Add(a[i]);
                        }
                        return new ArrayValue(list.ToArray());
                    }                                        
                }

                if(result < 0)
                    result = a.Values.Length + result;                   
                    
                if(result < 0 || result >= a.Values.Length)
                    return new StringValue($"Index `[{result}]` out of range");

                return a[result];                                 
            }           
            return new StringValue($"No key found for this value.");
        }
    }
}
