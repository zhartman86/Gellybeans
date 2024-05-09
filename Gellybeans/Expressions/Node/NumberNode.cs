using System.Text;
using Gellybeans.Pathfinder;

namespace Gellybeans.Expressions
{
    internal class NumberNode : ExpressionNode
    {
        public int Number { get; }

        public NumberNode(int number) =>
            Number = number;

        public override string ToString() =>
            Number.ToString();

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";
            
            return new Stat(Number);
        }
    }
}
