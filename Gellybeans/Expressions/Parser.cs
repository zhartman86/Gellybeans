namespace Gellybeans.Expressions
{
    public class Parser
    {
        Tokenizer tokenizer;

        public Parser(Tokenizer tokenizer)
        {
            this.tokenizer = tokenizer;
        }

        public ExpressionNode ParseExpr()
        {
            var expr = ParseEquals();
            if(tokenizer.Token != TokenType.EOF) throw new Exception("Unexpected character at end of expression.");        
            return expr;
        }               
        
        ExpressionNode ParseTernary()
        {
            var conditional = ParseEquals();

            while(true)
            {
                Func<int, int, int, int> op = null;

                if(tokenizer.Token == TokenType.Ternary) { op = (a, b, c) => a == 1 ? b : c; }

                if(op == null) return conditional;

                tokenizer.NextToken();

                var lhs = ParseEquals();
                var rhs = ParseEquals();

                conditional = new TernaryNode(conditional, lhs, rhs, op);
            }
        }                     
        
        
        
        ExpressionNode ParseEquals()
        {
            var lhs = ParseGreaterLess();

            while(true)
            {
                Func<int, int, int> op = null;
                
                if(tokenizer.Token == TokenType.Equals)         { op = (a, b) => a == b ? 1 : 0; }
                else if(tokenizer.Token == TokenType.NotEquals) { op = (a, b) => a != b ? 1 : 0; }

                if(op == null) return lhs;

                tokenizer.NextToken();

                var rhs = ParseGreaterLess();

                lhs = new BinaryNode(lhs, rhs, op);
            }
        }
        
        ExpressionNode ParseGreaterLess()
        {
            var lhs = ParseAddSub();

            while(true)
            {
                Func<int, int, int> op = null;

                if(tokenizer.Token == TokenType.Greater)            { op = (a, b) => a > b ? 1 : 0; }
                else if(tokenizer.Token == TokenType.GreaterEquals) { op = (a, b) => a >= b ? 1 : 0; }
                else if(tokenizer.Token == TokenType.Less)          { op = (a, b) => a < b ? 1 : 0; }
                else if(tokenizer.Token == TokenType.LessEquals)    { op = (a, b) => a <= b ? 1 : 0; }

                if(op == null) return lhs;

                tokenizer.NextToken();

                var rhs = ParseAddSub();

                lhs = new BinaryNode(lhs, rhs, op);
            }
        }
        
        ExpressionNode ParseAddSub()
        {
            var lhs = ParseMulDivMod();

            while(true)
            {
                Func<int, int, int> op = null;
                
                if(tokenizer.Token == TokenType.Add)        { op = (a, b) => a + b; }
                else if(tokenizer.Token == TokenType.Sub)   { op = (a, b) => a - b; }

                if(op == null) return lhs;

                tokenizer.NextToken();

                var rhs = ParseMulDivMod();
                
                lhs = new BinaryNode(lhs, rhs, op);
            }
        }

        ExpressionNode ParseMulDivMod()
        {
            var lhs = ParseUnary();

            while(true)
            {
                Func<int, int, int> op = null;

                if(tokenizer.Token == TokenType.Mul)            { op = (a, b) => a * b; }
                else if(tokenizer.Token == TokenType.Div)       { op = (a, b) => a / b; }
                else if(tokenizer.Token == TokenType.Modulo)    { op = (a, b) => a % b; }

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

                return ParseLeaf();
            }                      
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
                var node = ParseEquals();
                
                if(tokenizer.Token != TokenType.ClosePar) 
                    throw new Exception("Missing closed parens.");
                
                tokenizer.NextToken();
                return node;
            }

            if(tokenizer.Token == TokenType.Dice)
            {
                var rr = 0;
                var splitRR = tokenizer.Identifier.Split('r');
                string[] split;
                if(splitRR.Length > 1)
                {
                    rr = int.Parse(splitRR[1]);
                    split = splitRR[0].Split(new char[] { 'd', 'D' }, StringSplitOptions.RemoveEmptyEntries);
                }
                else split = tokenizer.Identifier.Split(new char[] { 'd', 'D' }, StringSplitOptions.RemoveEmptyEntries);
                

                int count = split.Length > 1 ? int.Parse(split[0]) : 1; 
                int sides = split.Length > 1 ? int.Parse(split[1]) : int.Parse(split[0]);                    
                
                tokenizer.NextToken();

                var lhs = new DiceNode(count, sides) { Reroll = rr };

                if(tokenizer.Token == TokenType.Mul)
                {
                    tokenizer.NextToken();
                    var rhs = ParseEquals();                
                    return new DiceMultiplierNode(lhs, rhs);
                }
                return lhs;
            }


            if(tokenizer.Token == TokenType.Var)
            {                             
                var name = tokenizer.Identifier;
                
                tokenizer.NextToken();                            
                if(tokenizer.Token == TokenType.AssignEquals || tokenizer.Token == TokenType.AssignAdd || tokenizer.Token == TokenType.AssignSub || tokenizer.Token == TokenType.AssignDiv || tokenizer.Token == TokenType.AssignMul || tokenizer.Token == TokenType.AssignMod)
                {
                    var type = tokenizer.Token;
                    tokenizer.NextToken();
                    var rhs = ParseEquals();
                    var lh = new AssignNode(name, rhs, type);
                    return lh;
                }


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
                        args.Add(ParseEquals());

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
     
        public static ExpressionNode Parse(string expr)
        {
            return Parse(new Tokenizer(new StringReader(expr)));
        }
    
        public static ExpressionNode Parse(Tokenizer tokenizer)
        {
            var parser = new Parser(tokenizer);
            return parser.ParseExpr();
        }
    
    
        
    }
}
