namespace Gellybeans.Expressions
{
    public class Parser
    {
        Tokenizer tokenizer;

        public Parser(Tokenizer tokenizer) => this.tokenizer = tokenizer;

        public ExpressionNode Parse()
        {
            var expr = ParseAddSub();
            if(tokenizer.Token != TokenType.EOF) throw new Exception("Unexpected character at end of expression.");
            return expr;
        }               
        
        ExpressionNode ParseAddSub()
        {
            var lhs = ParseMulDiv();

            while(true)
            {
                Func<int, int, int> op = null;
                
                if(tokenizer.Token == TokenType.Add)        { op = (a, b) => a + b; }
                else if(tokenizer.Token == TokenType.Sub)   { op = (a, b) => a - b; }

                if(op == null) return lhs;

                tokenizer.NextToken();

                var rhs = ParseMulDiv();
                
                lhs = new BinaryNode(lhs, rhs, op);
            }
        }

        ExpressionNode ParseMulDiv()
        {
            var lhs = ParseUnary();

            while(true)
            {
                Func<int, int, int> op = null;

                if(tokenizer.Token == TokenType.Mul) { op = (a, b) => a * b; }
                else if(tokenizer.Token == TokenType.Div) { op = (a, b) => a / b; }

                if(op == null) return lhs;

                tokenizer.NextToken();

                var rhs = ParseUnary();

                lhs = new BinaryNode(lhs, rhs, op);
            }
        }

        ExpressionNode ParseUnary()
        {
            while(true)
            {
                if(tokenizer.Token == TokenType.Add)
                {
                    tokenizer.NextToken();
                    continue;
                }

                if(tokenizer.Token == TokenType.Sub)
                {
                    tokenizer.NextToken();
                    
                    var rhs = ParseUnary();                  
                    return new UnaryNode(rhs, (a) => -a);
                }
            }
            
            return ParseLeaf();
        }
        
        ExpressionNode ParseLeaf()
        {
            if(tokenizer.Token == TokenType.Number)
            {
                var node = new NumberNode(tokenizer.Number);
                tokenizer.NextToken();
                return node;
            }

            if(tokenizer.Token == TokenType.OpenPar)
            {
                tokenizer.NextToken();
                var node = ParseAddSub();
                
                if(tokenizer.Token != TokenType.ClosePar) 
                    throw new Exception("Missing closed parens.");
                
                tokenizer.NextToken();
                return node;
            }
            
            if(tokenizer.Token == TokenType.Var)
            {
                var name = tokenizer.Identifier;
                tokenizer.NextToken();
                
                if(tokenizer.Token != TokenType.OpenPar)
                {
                    return new VarNode(name);
                }
            
                else
                {
                    tokenizer.NextToken();

                    var args = new List<ExpressionNode>();
                    while(true)
                    {
                        args.Add(ParseAddSub());

                        if(tokenizer.Token == TokenType.Comma)
                        {
                            tokenizer.NextToken();
                            continue;
                        }

                        break;
                    }

                    if(tokenizer.Token != TokenType.ClosePar)
                        throw new Exception("Missing close parens.");
                    
                    tokenizer.NextToken();

                    return new FunctionNode(name, args.ToArray());
                }
            }

            throw new Exception($"Unexpected symbol: {tokenizer.Token}");
        }
    }
}
