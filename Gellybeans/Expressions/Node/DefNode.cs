using System.Text;

namespace Gellybeans.Expressions
{
    public class DefNode : ExpressionNode
    {
        readonly string[] placeholders;
        readonly List<Token> tokens;

        public DefNode(List<Token> tokens, string[] placeholders)
        {
            this.tokens = tokens;
            this.placeholders = placeholders;
        }

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            return new FunctionValue(placeholders, Tokenizer.Output(tokens));
        }

    }
}
