using System.Text;
using Gellybeans.Expressions;

namespace Gellybeans.Expressions
{
    public class TernaryNode : ExpressionNode
    {
        ExpressionNode condition;
        ExpressionNode lhs;
        ExpressionNode rhs;

        Func<dynamic, dynamic, dynamic, dynamic> op;

        public TernaryNode(ExpressionNode condition, ExpressionNode lhs, ExpressionNode rhs, Func<dynamic, dynamic, dynamic, dynamic> op)
        {
            this.condition = condition;
            this.lhs = lhs;
            this.rhs = rhs;
            this.op = op;
        }

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            dynamic lhValue = 0;
            dynamic rhValue = 0;

            var conValue = condition.Eval(depth: depth, caller: this, sb: sb, ctx : ctx);
            if(conValue is IReduce r)
                conValue = r.Reduce(depth: depth, caller: this, sb: sb, ctx: ctx);


            if(conValue is ArrayValue a)
            {                   
                var na = new dynamic[a.Values.Length];
                for(int i = 0; i < a.Values.Length; i++)
                {
                    var value = a[i] ? lhs.Eval(depth: depth, caller: this, sb: sb, ctx: ctx) : rhs.Eval(depth: depth, caller: this, sb: sb, ctx: ctx);

                    if(value is SymbolNode s)
                    {
                        if(s.Symbol == "#")
                            if(condition is BinaryNode b)                     
                                if(b.LResult is ArrayValue av)
                                {
                                    value = av[i];
                                }                       
                    }              
                    na[i] = op(a[i], a[i] ? value : lhValue, !a[i] ? value : rhValue);
                }
                return new ArrayValue(na);                         
            }     
            else
            {
                var value = conValue ? lhs.Eval(depth: depth, caller: this, sb: sb, ctx: ctx) : rhs.Eval(depth: depth, caller: this, sb: sb, ctx: ctx);
                if(value is IReduce rr)
                    value = rr.Reduce(depth: depth, caller: this, sb: sb, ctx: ctx);
                var result = op(conValue, conValue ? value : lhValue, !conValue ? value : rhValue);
                return result;
            }                         
        }
    }
}
