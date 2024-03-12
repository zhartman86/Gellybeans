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

        public override dynamic Eval(int depth, IContext ctx, StringBuilder sb)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";


            dynamic lhValue = 0;
            dynamic rhValue = 0;

            var conValue = condition.Eval(depth, ctx, sb);
            if (conValue is IReduce r)
                conValue = r.Reduce(depth, ctx, sb);

            if (conValue is ArrayValue a)
            {
                for (int i = 0; i < a.Values.Length; i++)
                {
                    if (a.Values[i])
                    {
                        lhValue = lhs.Eval(depth, ctx, sb);
                        if (lhValue is IReduce rr)
                            lhValue = rr.Reduce(depth, ctx, sb);
                    }
                    if (!a.Values[i])
                    {
                        rhValue = rhs.Eval(depth, ctx, sb);
                        if (rhValue is IReduce rrr)
                            rhValue = rrr.Reduce(depth, ctx, sb);
                    }
                    a.Values[i] = op(a.Values[i], lhValue, rhValue);
                }
                return a;
            }
            else
            {
                if (conValue)
                {
                    lhValue = lhs.Eval(depth, ctx, sb);
                    if (lhValue is IReduce rr)
                        lhValue = rr.Reduce(depth, ctx, sb);
                }
                if (!conValue)
                {
                    rhValue = rhs.Eval(depth, ctx, sb);
                    if (rhValue is IReduce rrr)
                        rhValue = rrr.Reduce(depth, ctx, sb);
                }
            }

            var result = op(conValue, lhValue, rhValue);
            return result;
        }
    }
}
