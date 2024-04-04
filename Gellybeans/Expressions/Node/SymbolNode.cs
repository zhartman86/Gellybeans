using System.Text;

namespace Gellybeans.Expressions
{
    public class SymbolNode : ExpressionNode
    {
        public string Symbol { get; }

        public SymbolNode(string symbol) => 
            Symbol = symbol;

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";
            return this;
        }

        public override string ToString() =>
            Symbol;
    }
}
