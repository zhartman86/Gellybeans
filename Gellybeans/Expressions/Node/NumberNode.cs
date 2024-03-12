using System.Text;

namespace Gellybeans.Expressions
{
    internal class NumberNode : ExpressionNode
    {
        public int Number { get; }

        public NumberNode(int number) =>
            Number = number;

        public override string ToString() =>
            Number.ToString();

        public override dynamic Eval(IContext ctx = null!, StringBuilder sb = null!) =>
            Number;
    }
}
