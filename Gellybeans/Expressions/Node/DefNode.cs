using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override dynamic Eval(IContext ctx, StringBuilder sb)
        {
            for (int i = 0; i < placeholders.Length; i++)
            {
                for (int j = 0; j < tokens.Count; j++)
                {
                    if (tokens[j].TokenType == TokenType.Var && tokens[j].Value == placeholders[i])
                        tokens[j].Value = $"(«{i}»)";
                }

            }

            return new FunctionValue(placeholders.Length, Tokenizer.Output(tokens));
        }

    }
}
