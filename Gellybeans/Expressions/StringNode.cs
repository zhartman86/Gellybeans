using System.Text;

namespace Gellybeans.Expressions
{
    public class StringNode : ExpressionNode
    {
        readonly string str;

        public string String { get { return str; } }

        public StringNode(string str) =>
            this.str = str;

        public override int Eval(IContext ctx, StringBuilder sb) => 1;
    }
}
