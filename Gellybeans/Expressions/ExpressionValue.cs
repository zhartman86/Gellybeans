using System.Text;

namespace Gellybeans.Expressions
{
    public class ExpressionValue : ValueNode
    {
        public string Expression { get; set; }

        public ExpressionValue(string expr) : base(expr) 
        {
            Expression = Value;
        }

        public override ValueNode Eval(IContext ctx, StringBuilder sb) =>
            Parser.Parse(Expression, ctx, sb).Eval(ctx, sb);

        public override string ToString() => 
            Expression.ToString();

    }
}
