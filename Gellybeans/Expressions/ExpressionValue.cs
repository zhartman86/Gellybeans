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

        public dynamic Eval(IContext ctx, StringBuilder sb) =>
            Parser.Parse(Expression, ctx, sb).Eval(ctx, sb);
        

    }
}
