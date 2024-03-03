using System.Text;

namespace Gellybeans.Expressions
{
    public class ExpressionValue : ValueNode
    {
        public string Expression { get { return Value; } }

        public ExpressionValue(string expr) : base(value : expr) { }

        public override ValueNode Eval(IContext ctx, StringBuilder sb) =>
            Parser.Parse(Value, ctx, sb).Eval();

        public override string ToString() => 
            Value.ToString();

    }
}
