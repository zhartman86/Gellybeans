using Gellybeans.Pathfinder;
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
        List<Token> tokens;

        //dice expression. 0-3 number(s) => d => 1-5 number(s) => 0-3 instances of ('r' or 'h' or 'l' paired with 1-3 number(s))
        static readonly Regex dRegex = new Regex(@"^([0-9]{0,4})d([0-9]{1,4})((?:r|h|l)(?:[0-9]{1,3})){0,2}$");



        public Parser(List<Token> tokens, int index = 0)
        {
            this.tokens = tokens;
            this.index = index;
        }
            

        int index;


        public ExpressionNode ParseExpr()
        {
            var expr = ParseTernary();

            switch(tokens[index].TokenType)
            {
                case TokenType.Error:
                    return new VarNode($"%ERROR: {tokens[index].Value}");
                case TokenType.EOF:
                    return expr;
                default:
                    return new VarNode($"%Invalid expression.");
            }       

            
        }               

        ExpressionNode ParseTermination()
        {
            var expr = ParseTernary();
            
            if(tokens[index].TokenType == TokenType.Semicolon)
                return new MultiExpressionNode(expr, ParseTermination());

            return expr;
        }


        ExpressionNode ParseTernary()
        {
            if(tokens[index].TokenType == TokenType.Separator)
                index++;
            
            var conditional = ParseLogicalAndOr();

            while(true)
            {
                Func<int, int, int, int> op = null!;

                if(tokens[index].TokenType == TokenType.Ternary) { op = (a, b, c) => a == 1 ? b : c; }

                if(op == null) return conditional;

                index++;
                var lhs = ParseLogicalAndOr();
                var rhs = ParseTernary();

                conditional = new TernaryNode(conditional, lhs, rhs, op);
            }          
        }                 
             
        
        ExpressionNode ParseLogicalAndOr()
        {
            var lhs = ParseBitwiseAndOr();

            while(true)
            {
                Func<int, int, int> op = null!;

                if(tokens[index].TokenType == TokenType.LogicalOr)          { op = (a, b) => (a == 1 || b == 1) ? 1 : 0; }
                else if(tokens[index].TokenType == TokenType.LogicalAnd)    { op = (a, b) => (a == 1 && b == 1) ? 1 : 0; }

                if(op == null) return lhs;

                index++;
                var rhs = ParseBitwiseAndOr();

                lhs = new BinaryNode(lhs, rhs, op);
            }
        }
        
        ExpressionNode ParseBitwiseAndOr()
        {
            var lhs = ParseEquals();

            while(true)
            {
                Func<int, int, int> op = null!;

                if(tokens[index].TokenType == TokenType.BitwiseOr) { op = (a, b) => a | b; }
                else if(tokens[index].TokenType == TokenType.BitwiseAnd) { op = (a, b) => a & b; }

                if(op == null) return lhs;
                
                index++;
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
                
                if(tokens[index].TokenType == TokenType.Equals)         { op = (a, b) => a == b ? 1 : 0; }
                else if(tokens[index].TokenType == TokenType.NotEquals) { op = (a, b) => a != b ? 1 : 0; }
                else if(tokens[index].TokenType == TokenType.HasFlag)   { op = (a, b) => (a & b) != 0 ? 1 : 0; }

                if(op == null) return lhs;

                index++;
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

                if(tokens[index].TokenType == TokenType.Greater)            { op = (a, b) => a > b ? 1 : 0; }
                else if(tokens[index].TokenType == TokenType.GreaterEquals) { op = (a, b) => a >= b ? 1 : 0; }
                else if(tokens[index].TokenType == TokenType.Less)          { op = (a, b) => a < b ? 1 : 0; }
                else if(tokens[index].TokenType == TokenType.LessEquals)    { op = (a, b) => a <= b ? 1 : 0; }

                if(op == null) return lhs;

                index++;
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
                
                if(tokens[index].TokenType == TokenType.Add)        { op = (a, b) => a + b; }
                else if(tokens[index].TokenType == TokenType.Sub)   { op = (a, b) => a - b; }

                if(op == null) return lhs;

                index++;
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

                if(tokens[index].TokenType == TokenType.Mul)            { op = (a, b) => a * b; }
                else if(tokens[index].TokenType == TokenType.Div)       { op = (a, b) => a / b; }
                else if(tokens[index].TokenType == TokenType.Modulo)    { op = (a, b) => a % b; }

                if(op == null) return lhs;

                index++;
                var rhs = ParseUnary();

                lhs = new BinaryNode(lhs, rhs, op);
            }
        }

        ExpressionNode ParseUnary()
        {
            while(true)
            {                   
                if(tokens[index].TokenType == TokenType.Add)
                {
                    index++;
                    continue;
                }

                if(tokens[index].TokenType == TokenType.Sub)
                {
                    index++;                   
                    var rhs = ParseUnary();                  
                    return new UnaryNode(rhs, (a) => -a);
                }
                return ParseLeaf();
            }                      
        }
        
        ExpressionNode ParseLeaf()
        {
            if(tokens[index].TokenType == TokenType.Number)
            {
                var node = new NumberNode(int.Parse(tokens[index].Value));
                index++;
                return node;
            }      
            
            if(tokens[index].TokenType == TokenType.OpenPar)
            {
                index++;
                var node = ParseTernary();

                if(tokens[index].TokenType != TokenType.ClosePar) 
                    return new VarNode("%Missing closed parenthesis `)`.");
                
                index++;
                return node;
            }

            if(tokens[index].TokenType == TokenType.Dice)
            {
                //^([0-9]{0,3})d([0-9]{1,3})((?:r|h|l)(?:[0-9]{1,3})){0,3}$
                var match = dRegex.Match(tokens[index].Value);

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
                                                         
                    index++;
                    if(tokens[index].TokenType == TokenType.Mul || tokens[index].TokenType == TokenType.Div)
                    {
                        var op = tokens[index].TokenType;
                        index++;
                        var rhs = ParseTernary();
                        return new DiceMultiplierNode(lhs, rhs, op);
                    }
                    return lhs;
                }
                return new VarNode(tokens[index].Value);
            }


            if(tokens[index].TokenType == TokenType.Macro)
            {              
                var macro = new MacroNode(tokens[index].Value, Tokenizer.Output(tokens, index+1));
                index = tokens.Count - 1;
                return macro;
            }

            if(tokens[index].TokenType == TokenType.Var)
            {                             
                var name = tokens[index].Value;
                

                index++;
                if(tokens[index].TokenType == TokenType.Assign || tokens[index].TokenType == TokenType.AssignAdd || tokens[index].TokenType == TokenType.AssignFlag || tokens[index].TokenType == TokenType.AssignSub || tokens[index].TokenType == TokenType.AssignDiv || tokens[index].TokenType == TokenType.AssignMul || tokens[index].TokenType == TokenType.AssignMod)
                {
                    var type = tokens[index].TokenType;
                    index++;

                    if(tokens[index].TokenType == TokenType.String)
                    {
                        var str = tokens[index].Value;
                        index++;
                        if(type == TokenType.Assign)
                        {
                            return new AssignNode(name, new StringNode(str), TokenType.AssignExpr);
                        }                       
                    }
                    else
                    {
                        var rhs = ParseTernary();
                        var lh = new AssignNode(name, rhs, type);
                        return lh;
                    }
                   
                }
             
                if(tokens[index].TokenType == TokenType.Bonus)
                {
                    var type = tokens[index].TokenType;
                    index++;

                    var bName = tokens[index].Value != string.Empty ? 
                                    tokens[index].Value : "";

                    index++;

                    if(bName != "")
                        return new BonusNode(name, bName, null, null, type);
                }

                if(tokens[index].TokenType == TokenType.Bonus || tokens[index].TokenType == TokenType.AssignAddBon || tokens[index].TokenType == TokenType.AssignSubBon)
                {
                    var type = tokens[index].TokenType;
                    index++;
                    if(tokens[index].TokenType == TokenType.Var)
                    {
                        var bName = tokens[index].Value;
                        index++;

                        BonusNode lh;
                        if(type == TokenType.Bonus)
                            lh = new BonusNode(name, bName, null!, null!, type);                       
                        else if(type == TokenType.AssignSubBon)
                            lh = new BonusNode(name, bName, null!, null!, type);
                        else if(tokens[index].TokenType != TokenType.Separator)
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

                if(tokens[index].TokenType != TokenType.OpenPar)
                {
                    return new VarNode(name);
                }                 
                else
                {
                    index++;

                    var args = new List<ExpressionNode>();
                    while(true)
                    {
                        args.Add(ParseTernary());

                        if(tokens[index].TokenType == TokenType.Comma)
                        {
                            index++;
                            continue;
                        }
                        break;
                    }

                    if(tokens[index].TokenType != TokenType.ClosePar)
                        return new VarNode("%Missing closed parenthesis `)`");                 
                    
                    index++;
                    return new FunctionNode(name, args.ToArray());
                }             
            }


            if(tokens[index].TokenType == TokenType.AssignSubBon)
            {
                var type = tokens[index].TokenType;
                index++;
                if(tokens[index].TokenType == TokenType.EOF)
                    return new BonusNode(null!, "", null!, null!, type);
                if(tokens[index].TokenType == TokenType.Var)
                {
                    index++;
                    return new BonusNode(null!, tokens[index-1].Value, null!, null!, type);
                }
            }


            tokens[index].TokenType = TokenType.Error;
            return new StringNode("");
        }
     
        public static ExpressionNode Parse(string expr) => 
            Parse(new Tokenizer(new StringReader(expr)).Tokens);
    
        public static ExpressionNode Parse(List<Token> tokens)
        {
            var parser = new Parser(tokens);
            return parser.ParseExpr();
        }
    }
}
