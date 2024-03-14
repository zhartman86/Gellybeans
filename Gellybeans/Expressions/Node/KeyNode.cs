using System.Collections.Generic;
using System.Text;

namespace Gellybeans.Expressions
{
    public class KeyNode : ExpressionNode
    {
        public string VarName { get; }
        public ExpressionNode Key { get; }

        public KeyNode(string VarName, ExpressionNode Key)
        {
            this.VarName = VarName;
            this.Key = Key;
        }

        public override dynamic Eval(int depth, IContext ctx = null, StringBuilder sb = null)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            var v = VarName.ToUpper();
            var result = Key.Eval(depth, ctx, sb);
            if (ctx.TryGetVar(v, out var var))
            {
                if (var is ArrayValue a)
                {
                    if(result is RangeValue r)
                    {
                        if(r.OneRandom)
                            return a[Random.Shared.Next(0, a.Values.Length)];
                        

                        var start = r.Lower;
                        if(start < 0)
                            start = a.Values.Length + start;
                        var end = r.Upper;
                        if (end < 0)
                            end = a.Values.Length + end;

                        if(start < 0 || start >= a.Values.Length || end < 0 || end >= a.Values.Length)
                            return "Invalid range for this array.";

                        
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
                        return "Index out of range";

                    return a[result];
                }
                    
            }
            sb?.Append($"{v} not found");
            return result;
        }
    }
}
