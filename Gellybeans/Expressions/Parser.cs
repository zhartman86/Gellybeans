using System.Text;
using System.Text.RegularExpressions;

namespace Gellybeans.Expressions
{
    public class Parser
    {
        Tokenizer tokenizer;
        static readonly Regex dRegex = new Regex(@"([0-9]{1,3})d([0-9]{1,3})r?([0-9]{1,2})?((?:h|l)[0-9]{1,2})?");


        public Parser(Tokenizer tokenizer) => this.tokenizer = tokenizer;

        public ExpressionNode ParseExpr()
        {
            var expr = ParseTernary();
            if(tokenizer.Token != TokenType.EOF)
            {
                Console.WriteLine($"Unexpected character {tokenizer.CurrentChar} at end of expression. TOKEN:{tokenizer.Token}");
                throw new Exception("Unexpected character at end of expression.");
            }                      
            return expr;
        }               

        ExpressionNode ParseTernary()
        {                    
            var conditional = ParseLogicalAndOr();

            while(true)
            {
                Func<int, int, int, int> op = null;

                if(tokenizer.Token == TokenType.Ternary) { op = (a, b, c) => a == 1 ? b : c; }

                if(op == null) return conditional;

                tokenizer.NextToken();

                var lhs = ParseLogicalAndOr();
                var rhs = ParseTernary();

                conditional = new TernaryNode(conditional, lhs, rhs, op);
            }          
        }                 
             
        
        ExpressionNode ParseLogicalAndOr()
        {
            var lhs = ParseEquals();

            while(true)
            {
                Func<int, int, int> op = null;

                if(tokenizer.Token == TokenType.LogicalOr)          { op = (a, b) => (a == 1 || b == 1) ? 1 : 0; }
                else if(tokenizer.Token == TokenType.LogicalAnd)    { op = (a, b) => (a == 1 && b == 1) ? 1 : 0; }

                if(op == null) return lhs;

                tokenizer.NextToken();

                var rhs = ParseEquals();

                lhs = new BinaryNode(lhs, rhs, op);
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
                var node = ParseTernary();

                if(tokenizer.Token != TokenType.ClosePar) 
                    throw new Exception("Missing closed parens.");
                
                tokenizer.NextToken();
                return node;
            }

            if(tokenizer.Token == TokenType.Dice)
            {
                var match   = dRegex.Match(tokenizer.Identifier);

                if(match.Success)
                {
                    var count = int.Parse(match.Groups[1].Captures[0].Value);
                    var sides = int.Parse(match.Groups[2].Captures[0].Value);
                    var reroll = match.Groups[3].Captures.Count > 0 ? int.Parse(match.Groups[3].Captures[0].Value) : 0;
                    var highOrLow = match.Groups[4].Captures.Count > 0 ? match.Groups[4].Captures[0].Value : "";

                    DiceNode lhs = new DiceNode(count, sides);
                    if(highOrLow != "")
                    {
                        if(highOrLow[0] == 'h')
                        {
                            lhs.Highest = int.Parse(highOrLow.Remove(0, 1));
                        }                                          
                        else
                        {
                            lhs.Lowest = int.Parse(highOrLow.Remove(0, 1));
                        }
                                              
                    }
                    Console.WriteLine("TEST2");
                    tokenizer.NextToken();
                    if(tokenizer.Token == TokenType.Mul)
                    {
                        tokenizer.NextToken();
                        var rhs = ParseTernary();
                        return new DiceMultiplierNode(lhs, rhs);
                    }
                    return lhs;
                }                
            }

            if(tokenizer.Token == TokenType.Var)
            {                             
                var name = tokenizer.Identifier;             
                tokenizer.NextToken();                            
                
                if(tokenizer.Token == TokenType.AssignEquals || tokenizer.Token == TokenType.AssignAdd || tokenizer.Token == TokenType.AssignSub || tokenizer.Token == TokenType.AssignDiv || tokenizer.Token == TokenType.AssignMul || tokenizer.Token == TokenType.AssignMod)
                {
                    var type = tokenizer.Token;
                    tokenizer.NextToken();
                    var rhs = ParseTernary();
                    var lh = new AssignNode(name, rhs, type);
                    return lh;
                }

                if(tokenizer.Token == TokenType.GetBon)
                {
                    var type = tokenizer.Token;
                    tokenizer.NextToken();

                    var bName = tokenizer.Token == TokenType.Var ? 
                        tokenizer.Identifier : tokenizer.Token == TokenType.Number ? 
                        tokenizer.Number.ToString() : "";

                    tokenizer.NextToken();

                    if(bName != "")
                        return new BonusNode(name, bName, null, null, type);

                }

                if(tokenizer.Token == TokenType.GetBon || tokenizer.Token == TokenType.AssignAddBon || tokenizer.Token == TokenType.AssignSubBon)
                {
                    var type = tokenizer.Token;
                    tokenizer.NextToken();
                    if(tokenizer.Token == TokenType.Var)
                    {
                        var bName = tokenizer.Identifier;
                        tokenizer.NextToken();

                        BonusNode lh;
                        if(type == TokenType.GetBon)
                        {
                            lh = new BonusNode(name, bName, null, null, type);
                            return lh;
                        }
                        
                        if(type == TokenType.AssignSubBon)
                        {
                            lh = new BonusNode(name, bName, null, null, type);
                            return lh;
                        }

                        var bType = ParseTernary();
                        var bVal = ParseTernary();
 
                        lh = new BonusNode(name, bName, bType, bVal, type);
                        return lh;
                    }                                 
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
                        args.Add(ParseTernary());

                        if(tokenizer.Token == TokenType.Comma)
                        {
                            tokenizer.NextToken();
                            continue;
                        }
                        break;
                    }

                    if(tokenizer.Token != TokenType.ClosePar)
                        Console.WriteLine("Missing close parens.");
                    
                    tokenizer.NextToken();

                    return new FunctionNode(name, args.ToArray());
                }             
            }

            if(tokenizer.Token == TokenType.AssignSubBon)
            {
                var type = tokenizer.Token;
                tokenizer.NextToken();
                if(tokenizer.Token == TokenType.Var)
                {
                    tokenizer.NextToken();
                    return new BonusNode(null!, tokenizer.Identifier, null!, null!, type);
                }                    
            }


            Console.WriteLine($"Unexpected symbol: {tokenizer.Token}");
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
