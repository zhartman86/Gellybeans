using System.Text;

namespace Gellybeans.Expressions
{
    public class ExpressionValue : IReduce
    {
        public string Expression { get; set; }

        public ExpressionValue(string expr)
        {
            Expression = expr;
        }


        public override string ToString() =>
            Expression;

        public dynamic Reduce(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            var result = Parser.Parse(Expression, this, sb, ctx).Eval(depth: depth, caller: this, sb: sb, ctx : ctx);
            while (result is IReduce r)
                result = r.Reduce(depth: depth, caller: this, sb: sb, ctx : ctx);
            return result;
        }



        public static implicit operator ExpressionValue(string s) =>
            new(s);

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(obj, null))
                return false;

            if (obj is IReduce rhs)
                return Equals(Expression.Equals(rhs));
            return false;
        }

        public override int GetHashCode() =>
            Expression.GetHashCode();

    }
}
