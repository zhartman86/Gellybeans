using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using System.Text;

namespace Gellybeans.Expressions
{
    public class BinaryNode : ExpressionNode
    {
        ExpressionNode lhs;
        ExpressionNode rhs;

        Func<dynamic, dynamic, dynamic> op;

        public BinaryNode(ExpressionNode lhs, ExpressionNode rhs, Func<dynamic, dynamic, dynamic> op)
        {
            this.lhs    = lhs;
            this.rhs    = rhs;
            this.op     = op;
        }


        public override dynamic Eval(IContext ctx, StringBuilder sb)
        {
            Console.WriteLine($"eval'ing binary node. {lhs}, {rhs}");
            var lhValue = lhs.Eval(ctx, sb);
            var rhValue = rhs.Eval(ctx, sb);
            Console.WriteLine($"eval'd. {lhValue}, {rhValue}");

            var result = op(lhValue, rhValue);

            Console.WriteLine($"result {result}");

            return result;
        }
    }
}
