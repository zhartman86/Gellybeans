using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using System.Text;

namespace Gellybeans.Expressions
{
    public class BinaryNode : ExpressionNode
    {
        readonly ExpressionNode lhs;
        readonly ExpressionNode rhs;

        Func<dynamic, dynamic, dynamic> op;

        public BinaryNode(ExpressionNode lhs, ExpressionNode rhs, Func<dynamic, dynamic, dynamic> op)
        {
            this.lhs = lhs;
            this.rhs = rhs;
            this.op = op;
        }


        public override dynamic Eval(int depth, IContext ctx, StringBuilder sb)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            Console.WriteLine($"binary: lhs:{lhs.GetType()}, rhs:{rhs.GetType()}");

            var lhValue = lhs.Eval(depth, ctx, sb);
            if (lhValue is IReduce r)
                lhValue = r.Reduce(depth, ctx, sb);

            var rhValue = rhs.Eval(depth, ctx, sb);
            if (rhValue is IReduce rr)
                rhValue = rr.Reduce(depth, ctx, sb);

            Console.WriteLine($"binary: lhValue:{lhValue.GetType()}, rhValue:{rhValue.GetType()}");

            var result = op(lhValue, rhValue);

            Console.WriteLine("returning coimpleted binary operation");

            return result;
        }
    }
}
