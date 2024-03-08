using System.Text;

namespace Gellybeans.Expressions
{
    public class ExpressionValue : IEval
    {        
        public string Expression { get; set; }

        public ExpressionValue(string expr) =>
            Expression = expr;

        public override string ToString() =>
            Expression;

        public dynamic Eval(IContext ctx, StringBuilder sb)
        {
            var result = Parser.Parse(Expression, ctx, sb).Eval(ctx, sb);
            if(!object.ReferenceEquals(null, result))
            {
                if(result is IEval e)
                {
                    result = e.Eval(ctx, sb);
                }
            }
            return result;
        }

        public static implicit operator ExpressionValue(string s) => 
            new(s);


        public static ExpressionValue operator +(ExpressionValue lhs, dynamic rhs) =>
            $"{lhs.Expression} + " + rhs;

        public static ExpressionValue operator +(dynamic lhs, ExpressionValue rhs) =>
             lhs + $" + {rhs.Expression}";
    }
}
