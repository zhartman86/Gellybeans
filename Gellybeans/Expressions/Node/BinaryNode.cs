using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using System.Text;

namespace Gellybeans.Expressions
{
    public class BinaryNode : ExpressionNode
    {
        readonly ExpressionNode lhs;
        readonly ExpressionNode rhs;

        public dynamic LResult { get; private set; } = null!;
        public dynamic RResult { get; private set; } = null!;

        Func<dynamic, dynamic, dynamic> op;

        public BinaryNode(ExpressionNode lhs, ExpressionNode rhs, Func<dynamic, dynamic, dynamic> op)
        {
            this.lhs = lhs;
            this.rhs = rhs;
            this.op = op;
        }

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            Console.WriteLine($"binary: lhs:{lhs.GetType()}, rhs:{rhs.GetType()}");

            var lhValue = lhs.Eval(depth: depth, caller: this, sb: sb, ctx : ctx);
            if (lhValue is IReduce r)
                lhValue = r.Reduce(depth: depth, caller: this, sb: sb, ctx : ctx);

            var rhValue = rhs.Eval(depth: depth, caller: this, sb: sb, ctx : ctx);
            if (rhValue is IReduce rr)
                rhValue = rr.Reduce(depth: depth, caller: this, sb: sb, ctx : ctx);

            Console.WriteLine($"binary: lhValue:{lhValue.GetType()}, rhValue:{rhValue.GetType()}");

            LResult = lhValue;
            RResult = rhValue;

            var result = op(lhValue, rhValue);

            Console.WriteLine("returning completed binary operation");

            return result;
        }
    }
}
