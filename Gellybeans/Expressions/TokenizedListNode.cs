using System.Text;

namespace Gellybeans.Expressions
{
    public class TokenizedListNode : ExpressionNode
    {
        readonly ExpressionNode[] args;
        readonly List<Token> tokens;

        public TokenizedListNode(List<Token> tokens, ExpressionNode[] args)
        {
            this.args = args;
            this.tokens = tokens;
        }
            



        public override dynamic Eval(IContext ctx, StringBuilder sb)
        {
            
            
            return 0;
        }

    }
}
