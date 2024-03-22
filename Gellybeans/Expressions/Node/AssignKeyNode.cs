using System.Diagnostics;
using System.Text;

namespace Gellybeans.Expressions.Node
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
            var variable = varNode.Eval(depth, caller, sb, ctx);
            var assign = assignment.Eval(depth, caller, sb, ctx);

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
                var index = list[i].Key.Eval(depth: depth, caller: caller, sb: sb, ctx: ctx);
                var value = list[i].Value.Eval(depth: depth, caller: caller, sb: sb, ctx: ctx);
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
            var result = op(variable, indexes, assign);
            var varName = varNode.VarName.ToUpper();
            ctx[varName] = result;
            return result;
        }       
    }
}

