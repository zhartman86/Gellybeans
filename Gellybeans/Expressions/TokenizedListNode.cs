using System.Text;

namespace Gellybeans.Expressions
{
    public class TokenizedListNode : ExpressionNode
    {
        readonly List<List<Token>> tokenList = new List<List<Token>>();

        public List<List<Token>> TokenList { get { return tokenList; } }

        public TokenizedListNode(List<List<Token>> tokenList)
        {
            this.tokenList = tokenList;
        }

        public void Append(Token token)
        {
            for(int i = 0; i < tokenList.Count; i++)
                tokenList[i].Add(token);
        }

        public void Append(List<Token> tokens)
        {
            for (int i = 0; i < tokenList.Count; i++)
            {
                    tokenList[i].AddRange(tokens);

                //tag end with EOF
                if(tokenList[i][^1].TokenType != TokenType.EOF)
                    tokenList[i].Add(new Token(TokenType.EOF));
            }
        }

        public override dynamic Eval(IContext ctx, StringBuilder sb)
        {
            for(int i = 0; i < tokenList.Count; i++)
            {              
                var node = Parser.Parse(tokenList[i], ctx, sb!);
                var result = node.Eval(ctx, sb);
                sb?.AppendLine($"**Total:** {result}\r\n");
            }
            
            return 0;
        }

    }
}
