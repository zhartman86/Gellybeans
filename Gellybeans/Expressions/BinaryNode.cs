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
            this.lhs    = lhs;
            this.rhs    = rhs;
            this.op     = op;
        }


        public override dynamic Eval(IContext ctx, StringBuilder sb)
        {
            Console.WriteLine($"binary: lhs:{lhs.GetType()}, rhs:{rhs.GetType()}");           

            var lhValue = lhs.Eval(ctx, sb);
            if( lhValue is IReduce r) 
                lhValue = r.Reduce(ctx, sb);
           
            var rhValue = rhs.Eval(ctx, sb);
            if(rhValue is IReduce rr)
                rhValue = rr.Reduce(ctx, sb);

            Console.WriteLine($"binary: lhValue:{lhValue.GetType()}, rhValue:{rhValue.GetType()}");

            var result = op(lhValue, rhValue);

            Console.WriteLine("returning coimpleted binary operation");

            return result;
        }
    }
}
