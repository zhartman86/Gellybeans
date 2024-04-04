
using System.Text;

namespace Gellybeans.Expressions
{
    public class AssignKeyNode : ExpressionNode
    {
        readonly VarNode varNode;
        readonly KeyNode key;
        readonly ExpressionNode assignment;

        Func<dynamic, List<dynamic>, dynamic, dynamic> op;

        public AssignKeyNode(VarNode varNode, KeyNode key, ExpressionNode assignment, Func<dynamic, List<dynamic>, dynamic, dynamic> op)
        {
            this.varNode = varNode;
            this.key = key;
            this.assignment = assignment;
            this.op = op;
        }

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            if(ctx.TryGetVar(varNode.VarName.ToUpper(), out var variable))
            {
                var assign = assignment.Eval(depth, caller, sb, ctx);
                Console.WriteLine($"ASS: {assign.GetType()}");

                var list = new List<KeyNode>();
                var indexes = new List<dynamic>();

                list.Add(key);
                if(key.Value is KeyNode k)
                {
                    list.Add(k);
                    while(k.Value is KeyNode kk)
                    {
                        list.Add(kk);
                        k = kk;
                    }
                }



                for(int i = list.Count - 1; i >= 0; i--)
                {
                    var index = list[i].Key.Eval(depth: depth, caller: caller, sb: null!, ctx: ctx);
                    var value = list[i].Value.Eval(depth: depth, caller: caller, sb: null!, ctx: ctx);
                    if(value is KeyValuePairValue kv)
                        value = kv.Value;

                    if(value is ArrayValue a)
                    {
                        if(index is StringValue s)
                        {
                            for(int j = 0; j < a.Values.Length; j++)
                            {
                                if(a.Values[j] is KeyValuePairValue kvp && kvp.Key.ToUpper() == s.String.ToUpper())
                                {
                                    indexes.Add(j);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if(index < 0)
                                index = a.Values.Length + index;
                            if(index >= 0 && index < a.Values.Length)
                                indexes.Add(index);
                            else
                                return "Index out of range!";
                        }
                    }
                }
                ctx[varNode.VarName] = op(variable, indexes, assign);
                return assign.ToString();

                //var value = key.
                //var assign = assignment.Eval(depth, caller, sb, ctx);

            }
            return $"{varNode.VarName.ToUpper()} not found.";
        }       
    }
}

