using System.Text.RegularExpressions;

namespace Gellybeans.Expressions
{
    /// <summary>
    /// This will take an expression and generate nodes, as to respect typical mathematical equations and their order of operations.
    /// 
    /// Ternary => LogicalAndOr => Equals => GreaterLess => AddSub => MultDivMod => Unary => Leaf (Number => Parens => Dice => Variables => Bonuses, etc.)
    /// 
    /// This parser builds and expands upon ideas expressed in an article written by Brad Robinson. Thanks Brad!
    /// The article can can be found here: https://medium.com/@toptensoftware/writing-a-simple-math-expression-engine-in-c-d414de18d4ce
    /// </summary>
    
    public class Parser
    {
        Tokenizer tokenizer;
        
        //dice expression. 0-3 number(s) => d => 1-5 number(s) => 0-3 instances of ('r' or 'h' or 'l' paired with 1-3 number(s))
        static readonly Regex dRegex = new Regex(@"^([0-9]{0,4})d([0-9]{1,4})((?:r|h|l)(?:[0-9]{1,3})){0,2}$");

        public Parser(Tokenizer tokenizer) => 
            this.tokenizer = tokenizer;
       
        public ExpressionNode ParseExpr()
        {
            var expr = ParseTernary();

            if(tokenizer.Token != TokenType.EOF)       
                return new VarNode($"%Invalid expression.");                  
            
            return expr;
        }               


        ExpressionNode ParseTernary()
        {
            if(tokenizer.Token == TokenType.Error)
                return new VarNode($"%Invalid expression.");

            if(tokenizer.Token == TokenType.Separator)
                tokenizer.NextToken();
            
            var conditional = ParseLogicalAndOr();

            while(true)
            {
                Func<int, int, int, int> op = null!;

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
                Func<int, int, int> op = null!;

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
                Func<int, int, int> op = null!;
                
                if(tokenizer.Token == TokenType.Equals)         { op = (a, b) => a == b ? 1 : 0; }
                else if(tokenizer.Token == TokenType.NotEquals) { op = (a, b) => a != b ? 1 : 0; }
                else if(tokenizer.Token == TokenType.HasFlag)   { op = (a, b) => (a & (1<<b)) != 0 ? 1 : 0; }

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
                Func<int, int, int> op = null!;

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
                Func<int, int, int> op = null!;
                
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
                Func<int, int, int> op = null!;

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
                    return new VarNode("%Missing closed parenthesis `)`.");
                
                tokenizer.NextToken();
                return node;
            }

            if(tokenizer.Token == TokenType.Dice)
            {
                //^([0-9]{0,3})d([0-9]{1,3})((?:r|h|l)(?:[0-9]{1,3})){0,3}$
                var match   = dRegex.Match(tokenizer.Identifier);

                if(match.Success)
                {
                    var count = match.Groups[1].Captures.Count > 0 ? int.TryParse(match.Groups[1].Captures[0].Value, out int outVal) ? outVal : 1 : 1;
                    var sides = int.Parse(match.Groups[2].Captures[0].Value);

                    var lhs = new DiceNode(count, sides);

                    for(int i = 0; i < match.Groups[3].Captures.Count; i++)
                    {
                        //store the letter in index 0, remove it and parse the number that comes after
                        var letter = match.Groups[3].Captures[i].Value[0];
                        var number = int.Parse(match.Groups[3].Captures[i].Value.Remove(0, 1));

                        if(letter == 'r')
                            lhs.Reroll = number;
                        if(letter == 'h')
                            lhs.Highest = number;
                        if(letter == 'l')
                            lhs.Lowest = number;
                    }
                                                         
                    tokenizer.NextToken();
                    if(tokenizer.Token == TokenType.Mul || tokenizer.Token == TokenType.Div)
                    {
                        var op = tokenizer.Token;
                        tokenizer.NextToken();
                        var rhs = ParseTernary();
                        return new DiceMultiplierNode(lhs, rhs, op);
                    }
                    return lhs;
                }
                return new VarNode(tokenizer.Identifier);
            }

            if(tokenizer.Token == TokenType.Var)
            {                             
                var name = tokenizer.Identifier;
                tokenizer.NextToken();

                if(tokenizer.Token == TokenType.Assign || tokenizer.Token == TokenType.AssignAdd || tokenizer.Token == TokenType.Flag || tokenizer.Token == TokenType.AssignSub || tokenizer.Token == TokenType.AssignDiv || tokenizer.Token == TokenType.AssignMul || tokenizer.Token == TokenType.AssignMod)
                {
                    var type = tokenizer.Token;
                    tokenizer.NextToken();

                    if(tokenizer.Token == TokenType.String)
                    {
                        var str = tokenizer.Identifier;
                        tokenizer.NextToken();
                        if(type == TokenType.Assign)
                        {
                            return new AssignNode(name, new StringNode(str), TokenType.AssignExpr);
                        }
                        else if(type == TokenType.AssignAdd)
                        {
                            return new AssignNode(name, new StringNode(str), TokenType.AssignAddExpr);
                        }
                        
                    }
                    else
                    {
                        var rhs = ParseTernary();
                        var lh = new AssignNode(name, rhs, type);
                        return lh;
                    }
                   
                }

                if(tokenizer.Token == TokenType.Bonus)
                {
                    var type = tokenizer.Token;
                    tokenizer.NextToken();

                    var bName = tokenizer.Token == TokenType.Var ? 
                                    tokenizer.Identifier : 
                                tokenizer.Token == TokenType.Number ? 
                                    tokenizer.Number.ToString() : 
                                    "";

                    tokenizer.NextToken();

                    if(bName != "")
                        return new BonusNode(name, bName, null, null, type);
                }

                if(tokenizer.Token == TokenType.Bonus || tokenizer.Token == TokenType.AssignAddBon || tokenizer.Token == TokenType.AssignSubBon)
                {
                    var type = tokenizer.Token;
                    tokenizer.NextToken();
                    if(tokenizer.Token == TokenType.Var)
                    {
                        var bName = tokenizer.Identifier;
                        tokenizer.NextToken();

                        BonusNode lh;
                        if(type == TokenType.Bonus)
                            lh = new BonusNode(name, bName, null!, null!, type);                       
                        else if(type == TokenType.AssignSubBon)
                            lh = new BonusNode(name, bName, null!, null!, type);
                        else if(tokenizer.Token != TokenType.Separator)
                            lh = new BonusNode(name, bName, null!, null!, type);
                        else
                        {
                            var bType = ParseTernary();
                            var bVal = ParseTernary();
                            lh = new BonusNode(name, bName, bType, bVal, type);
                        }                  
                        return lh;
                    }                                                                  
                }

                if(tokenizer.Token != TokenType.OpenPar)
                    return new VarNode(name);        
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
                        return new VarNode("%Missing closed parenthesis `)`");
                    
                    tokenizer.NextToken();

                    return new FunctionNode(name, args.ToArray());
                }             
            }

            if(tokenizer.Token == TokenType.AssignSubBon)
            {
                var type = tokenizer.Token;
                tokenizer.NextToken();
                if(tokenizer.Token == TokenType.EOF)
                    return new BonusNode(null!, "", null!, null!, type);
                if(tokenizer.Token == TokenType.Var)
                {
                    tokenizer.NextToken();
                    return new BonusNode(null!, tokenizer.Identifier, null!, null!, type);
                }
            }
            tokenizer.Token = TokenType.Error;
            return new StringNode("");
        }
     
        public static ExpressionNode Parse(string expr) => 
            Parse(new Tokenizer(new StringReader(expr)));
    
        public static ExpressionNode Parse(Tokenizer tokenizer)
        {
            var parser = new Parser(tokenizer);
            return parser.ParseExpr();
        }
    }
}
