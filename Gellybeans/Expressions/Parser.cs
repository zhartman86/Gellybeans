using Gellybeans.Expressions;
using Gellybeans.Pathfinder;
using Microsoft.VisualBasic;
using System.ComponentModel.Design;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Gellybeans.Expressions
{
    /// <summary>
    /// This parser builds and expands upon ideas expressed in an article written by Brad Robinson. Thanks Brad!
    /// The article can can be found here: https://medium.com/@toptensoftware/writing-a-simple-math-expression-engine-in-c-d414de18d4ce
    /// </summary>

    public class Parser
    {
        public const int MAX_DEPTH = 500;

        readonly List<Token> tokens;
        readonly IContext ctx;       
        readonly StringBuilder sb;

        int index;
        int depth = 0;

        //dice expression. 0-3 number(s) => d => 1-5 number(s) => 0-3 instances of ('r' or 'h' or 'l' paired with 1-3 number(s))
        static readonly Regex dRegex = new Regex(@"^([0-9]{0,4})d([0-9]{1,4})((?:r|h|l)(?:[0-9]{1,3})){0,2}$", RegexOptions.Compiled);
   
        public static readonly Regex validVarName = new Regex(@"^[^0-9][^\[\]<>(){}^@:+*/%=!&|;$#?\-.'""]*$", RegexOptions.Compiled);

        Token Current { get { return tokens[index]; } }

        public Parser(List<Token> tokens, IContext ctx, StringBuilder sb, int index = 0)
        {
            this.tokens = tokens;
            this.ctx = ctx;
            this.index = index;
            this.sb = sb;
        }

        Token Look(int i = 1)
        {
            if((index + i) < tokens.Count - 1 && (index + 1) > 0)
                return tokens[index + i];
            return Current;
        }

        void Move(int count) =>
            index = Math.Clamp(index + count, 0, tokens.Count - 1);

        Token Next()
        {
            if(index < tokens.Count - 1)
                index++;
            return Current;
        }



        public ExpressionNode ParseExpr()
        {
            var expr = ParseTermination();

            if(Current.TokenType == TokenType.EOF || expr is ErrorNode)
                return expr;

            return new ErrorNode($"Invalid expression. Error on token `{Current.Value}`");
        }

        ExpressionNode ParseTermination()
        {
            var expr = ParseAssignment();

            if(Current.TokenType == TokenType.Pipe)
            {
                Console.WriteLine("PIPING");
                Next();
                var result = expr.Eval(depth, ctx, sb);
                return new PipeNode(result, ParseTermination());
            }
            
            if(Current.TokenType == TokenType.Semicolon)
            {
                Next();
                expr.Eval(depth, ctx, sb);
                return ParseTermination();
            }
            return expr;
        }

        ExpressionNode ParseAssignment()
        {
            var node = ParseTernary();
            if(Current.TokenType == TokenType.Assign)
            {
                Func<string, dynamic, dynamic> op = null!;

                if(node is VarNode varNode)
                {                
                    var varName = varNode.VarName.ToUpper();

                    switch(Current.Value)
                    {
                        case "=":
                            op = (identifier, assignment) =>
                            {
                                var setMessage = $"{varName} set";
                                if(assignment is int i)
                                {
                                    if(ctx[varName] is Stat s && s.Bonuses != null)
                                    {
                                        s.Base = i;
                                        assignment = s;
                                        setMessage = $"{varName} has bonuses. Base value set";
                                    }
                                    else
                                        assignment = new Stat(i);
                                }
                                ctx[varName] = assignment;
                                sb?.AppendLine(setMessage);

                                return $"{assignment}";
                            };
                            break;
                        case "+=":
                            if(ctx.TryGetVar(varName, out var var))
                            {
                                op = (identifier, assignment) =>
                                {
                                    if(var is Stat stat)
                                    {
                                        if(assignment is int i)
                                        {
                                            stat.Base += i;
                                            ctx[varName] = stat;
                                            sb?.AppendLine($"{varName} base value set to {stat.Base}");
                                            return stat.Value;
                                        }
                                    }
                                    ctx[varName] = var + assignment;
                                    sb?.AppendLine($"{varName} updated");
                                    return $"{assignment}";
                                };

                            }
                            else
                                sb?.Append($"{varNode.VarName} not found");
                            break;
                        case "-=":
                            if(ctx.TryGetVar(varName, out var))
                            {
                                op = (identifier, assignment) =>
                                {
                                    if(var is Stat stat)
                                    {
                                        if(assignment is int i)
                                        {
                                            stat.Base -= i;
                                            ctx[varName] = stat;
                                            sb?.AppendLine($"{varName} base value set to {stat.Base}");
                                            return stat.Value;
                                        }
                                    }
                                    ctx[varName] = var - assignment;
                                    sb?.AppendLine($"{varName} updated");
                                    return $"{assignment}";
                                };

                            }
                            else
                                sb?.Append($"{varNode.VarName} not found");
                            break;
                        case "*=":
                            if(ctx.TryGetVar(varName, out var))
                            {
                                op = (identifier, assignment) =>
                                {
                                    if(var is Stat stat)
                                    {
                                        if(assignment is int i)
                                        {
                                            stat.Base *= i;
                                            ctx[varName] = stat;
                                            sb?.AppendLine($"{varName} base value set to {stat.Base}");
                                            return stat.Value;
                                        }
                                    }
                                    ctx[varName] = var * assignment;
                                    sb?.AppendLine($"{varName} updated");
                                    return $"{assignment}";
                                };
                            }
                            else
                                sb?.Append($"{varNode.VarName} not found");
                            break;
                        case "/=":
                            if(ctx.TryGetVar(varName, out var))
                            {
                                op = (identifier, assignment) =>
                                {
                                    if(var is Stat stat)
                                    {
                                        if(assignment is int i)
                                        {
                                            stat.Base /= i;
                                            ctx[varName] = stat;
                                            sb?.AppendLine($"{varName} base value set to {stat.Base}");
                                            return stat.Value;
                                        }
                                    }
                                    ctx[varName] = var / assignment;
                                    sb?.AppendLine($"{varName} updated");
                                    return $"{assignment}";
                                };
                            }
                            else
                                sb?.Append($"{varNode.VarName} not found");
                            break;
                        case "%=":
                            if(ctx.TryGetVar(varName, out var))
                            {
                                op = (identifier, assignment) =>
                                {
                                    if(var is Stat stat)
                                    {
                                        if(assignment is int i)
                                        {
                                            stat.Base %= i;
                                            ctx[varName] = stat;
                                            sb?.AppendLine($"{varName} base value set to {stat.Base}");
                                            return stat.Value;
                                        }
                                    }
                                    ctx[varName] = var % assignment;
                                    sb?.AppendLine($"{varName} updated");
                                    return $"{assignment}";
                                };
                            }
                            else
                                sb?.Append($"{varNode.VarName} not found");
                            break;
                    }

                    if(op == null) return node;

                    Next();
                    var rhs = ParseTernary();
                    return new AssignVarNode(varName, rhs, op);
                }
                
                if(node is KeyNode keyNode)
                {
                    var varName = keyNode.VarName.ToUpper();
                    switch(Current.Value)
                    {
                        case "=":
                            if(ctx.TryGetVar(varName, out var var) && var is ArrayValue a)
                            {
                                op = (identifier, assignment) =>
                                {                                   
                                    var index = keyNode.Key.Eval(depth);
                                    var setMessage = $"{varName}[{index}] set";
                                    if(index >= 0 && index < a.Values.Length)
                                        a[index] = assignment;
                                    else
                                        return "Index out of range";

                                    sb?.AppendLine(setMessage);
                                    return $"{assignment}";
                                };
                            }                            
                            break;
                    }

                    if(op == null) return node;

                    Next();
                    var rhs = ParseTernary();
                    return new AssignVarNode(varName, rhs, op);
                }
            }
            return node;
        }

        ExpressionNode ParseTernary()
        {           
            var conditional = ParseLogicalAndOr();

            while(true)
            {
                Func<dynamic, dynamic, dynamic, dynamic> op = null!;

                if(Current.TokenType == TokenType.Ternary) { op = (a, b, c) => a ? b : c; }

                if(op == null) return conditional;

                Next();
                var lhs = ParseAssignment();
                if(Current.TokenType == TokenType.Separator)
                {
                    Next();
                    var rhs = ParseAssignment();
                    conditional = new TernaryNode(conditional, lhs, rhs, op);
                }                          
            }
        }


        ExpressionNode ParseLogicalAndOr()
        {
            var lhs = ParseBitwiseAndOr();

            while(true)
            {
                Func<dynamic, dynamic, dynamic> op = null!;

                if(Current.TokenType == TokenType.LogicalOr) { op = (a, b) => (a || b ); }
                else if(Current.TokenType == TokenType.LogicalAnd) { op = (a, b) => (a && b); }

                if(op == null) return lhs;

                Next();
                var rhs = ParseBitwiseAndOr();

                lhs = new BinaryNode(lhs, rhs, op);
            }
        }

        ExpressionNode ParseBitwiseAndOr()
        {
            var lhs = ParseEquals();

            while(true)
            {
                Func<dynamic, dynamic, dynamic> op = null!;
                
                if(Current.TokenType == TokenType.GetBonus) 
                { 
                    op = (identifier, type) =>
                    { 
                        if(ctx.TryGetVar(identifier.ToString(), out dynamic var))
                        {
                            if(var is Stat s)
                                return s.GetBonus((BonusType)(int)type);
                            else
                                sb?.AppendLine($"$? cannot be applied to {identifier}");
                        }
                        else
                            sb?.AppendLine($"{identifier} not found.");
                        return 0;
                    };                
                }

                if(op == null) return lhs;

                Next();
                var rhs = ParseEquals();

                lhs = new BinaryNode(lhs, rhs, op);
            }
        }

        ExpressionNode ParseEquals()
        {
            var lhs = ParseGreaterLess();

            while(true)
            {
                Func<dynamic, dynamic, dynamic> op = null!;

                if(Current.TokenType == TokenType.Equals) { op = (a, b) => a == b; }
                else if(Current.TokenType == TokenType.NotEquals) { op = (a, b) => a != b; }
                else if(Current.TokenType == TokenType.HasFlag) { op = (a, b) => (a & b) != 0; }
                else if(Current.TokenType == TokenType.GetBonus)
                    op = (lhs, rhs) =>
                    {
                        var value = 0;
                        if(lhs is Stat s && rhs is int i)
                        {
                            value = s.GetBonus((BonusType)i);
                        }
                            
                       
                        return value;
                    };

                if(op == null) return lhs;

                Next();
                var rhs = ParseGreaterLess();

                lhs = new BinaryNode(lhs, rhs, op);
            }
        }

        ExpressionNode ParseGreaterLess()
        {
            var lhs = ParseAddSub();

            while(true)
            {
                Func<dynamic, dynamic, dynamic> op = null!;

                if(Current.TokenType == TokenType.Greater)              { op = (a, b) => a > b; }
                else if(Current.TokenType == TokenType.GreaterEquals)   { op = (a, b) => a >= b; }
                else if(Current.TokenType == TokenType.Less)            { op = (a, b) => a < b; }
                else if(Current.TokenType == TokenType.LessEquals)      { op = (a, b) => a <= b; }

                if(op == null) return lhs;

                Next();
                var rhs = ParseAddSub();

                lhs = new BinaryNode(lhs, rhs, op);
            }
        }

        ExpressionNode ParseAddSub()
        {
            var lhs = ParseMulDivMod();

            while(true)
            {
                Func<dynamic, dynamic, dynamic> op = null!;

                if(Current.TokenType == TokenType.Add) { op = (a, b) => a + b; }
                else if(Current.TokenType == TokenType.Sub) { op = (a, b) => a - b; }

                if(op == null) return lhs;

                Next();
                var rhs = ParseMulDivMod();

                lhs = new BinaryNode(lhs, rhs, op);
            }
        }

        ExpressionNode ParseMulDivMod()
        {
            var lhs = ParseUnary();

            while(true)
            {
                Func<dynamic, dynamic, dynamic> op = null!;

                if(Current.TokenType == TokenType.Mul) { op = (a, b) => a * b; }
                else if(Current.TokenType == TokenType.Div) { op = (a, b) => a / b; }
                else if(Current.TokenType == TokenType.Modulo) { op = (a, b) => a % b; }

                if(op == null) return lhs;

                Next();
                var rhs = ParseUnary();

                lhs = new BinaryNode(lhs, rhs, op);
            }
        }

        ExpressionNode ParseUnary()
        {
            Func<dynamic, dynamic> op = null!;

            if(Current.TokenType == TokenType.Add)
                    Next();

            if(Current.TokenType == TokenType.Sub)
            {
                Next();
                var rhs = ParseLeaf();

                if(rhs is BonusNode b)
                {
                    op = (bonus) =>
                    {
                        var count = 0;
                        var stats = ctx.Vars.Values.OfType<Stat>();
                        foreach(Stat s in stats)
                        {
                            if(s.RemoveBonus(b.BonusName))
                                count++;
                        }
                        sb?.AppendLine($"{b.BonusName} removed from {count} stats.");

                        return count;
                    };
                }
                else
                    op = (a) => -a;


                return new UnaryNode(rhs, op);
            }
            
            if(Current.TokenType == TokenType.Not)
            {
                op = (value) => !value;
                
                Next();
                var rhs = ParseLeaf();
                
                return new UnaryNode(rhs, op);
            }

            if(Current.TokenType == TokenType.Base)
            {
                Next();
                var rhs = ParseLeaf();

                if(rhs is VarNode v)
                {
                    op = (var) =>
                    {
                        if(var is Stat s)
                            return s.Base;
                        else
                        {
                            sb.AppendLine($"@ cannot be applied to {v.VarName}");
                            return 0;
                        }                           
                    };
                    return new UnaryNode(rhs, op);
                }
                
            }

            if(Current.TokenType == TokenType.Remove)
            {
                Next();
                var rhs = ParseLeaf();
                if(rhs is VarNode v)
                {
                    op = (value) =>
                    {
                        var varName = v.VarName.ToUpper();
                        if(ctx.RemoveVar(varName))
                            return new StringValue($"{varName} removed.");
                        return new StringValue($"{varName} not found.");
                    };
                    return new UnaryNode(new NumberNode(0), op);
                }
                
                
            }
            
            if(Current.TokenType == TokenType.And)
            {
                Next();
                var rhs = ParseLeaf();
                op = (value) =>
                {
                    var d = new DiceNode(1, 20).Eval(depth, ctx, sb);
                    return d + value;
                };
                return new UnaryNode(rhs, op);
            }

            return ParseLeaf();
        }

        ExpressionNode ParseLeaf()
        {
            if(Current.TokenType == TokenType.Number)
            {                               
                var i = int.Parse(Current.Value);

                Next();
                return new NumberNode(i);
            }

            if(Current.TokenType == TokenType.OpenPar)
            {
                Next();
                var node = ParseTernary();

                if(Current.TokenType != TokenType.ClosePar)
                    return new ErrorNode("%Expected`)`");

                Next();
                return node;
            }

            
            if(Current.TokenType == TokenType.Dice)
            {
                //^([0-9]{0,3})d([0-9]{1,3})((?:r|h|l)(?:[0-9]{1,3})){0,3}$
                var match = dRegex.Match(Current.Value);

                if(match.Success)
                {
                    var count = match.Groups[1].Captures.Count > 0 ? int.TryParse(match.Groups[1].Captures[0].Value, out int outVal) ? outVal : 1 : 1;
                    var sides = int.Parse(match.Groups[2].Captures[0].Value);

                    var lhs = new DiceNode(count, sides, sb);

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

                    Next();
                    if(Current.TokenType == TokenType.Mul || Current.TokenType == TokenType.Div)
                    {
                        var op = Current.TokenType;
                        Next();
                        var rhs = ParseTernary();
                        return new DiceMultiplierNode(lhs, rhs, op);
                    }
                    return lhs;
                }
                Console.WriteLine("found var in dice");
                return new VarNode(Current.Value);
            }

            //array
            if(Current.TokenType == TokenType.OpenSquig)
            {
                var list = new List<ExpressionNode>();

                Next();

                while(true)
                {
                    list.Add(ParseTernary());

                    if(Current.TokenType == TokenType.Comma)
                    {
                        Next();
                        continue;
                    }
                    break;
                }

                if(Current.TokenType != TokenType.CloseSquig)
                    return new ErrorNode("Expected `}`");

                Next();
                return new ArrayNode(list.ToArray());
            }
       


            //VarNode
            if(Current.TokenType == TokenType.Var)
            {
                var identifier = Current.Value;
               
                if(Look().TokenType == TokenType.OpenPar)
                {
                    Move(2);

                    
                    if(ctx.TryGetVar(identifier.ToUpper(), out var value))
                    {
                        if(value is FunctionValue function)
                        {
                            var args = new List<ExpressionNode>();
                            CallNode c;
                            if(Current.TokenType == TokenType.ClosePar)
                            {
                                c = new CallNode(identifier.ToUpper(), args);
                            }
                            else
                                while(true)
                                {
                                    args.Add(ParseTernary());
                                    if(Current.TokenType == TokenType.Comma)
                                    {
                                        Next();
                                        continue;
                                    }
                                    break;
                                }                 

                            if(Current.TokenType != TokenType.ClosePar)
                                return new ErrorNode("Expected `)`");

                            Console.WriteLine($"CALLING WITH {args.Count} PARAMS");

                            Next();
                            return new CallNode(identifier.ToUpper(), args);

                        }
                    }




                    FunctionNode f;

                    //if no args
                    if(Current.TokenType == TokenType.ClosePar)
                        f = new FunctionNode(identifier.ToLower());
                    
                    else
                    {
                        var args = new List<ExpressionNode>();
                        while(true)
                        {
                            args.Add(ParseTernary());

                            if(Current.TokenType == TokenType.Comma)
                            {
                                Next();
                                continue;
                            }
                            break;
                        }
                        f = new FunctionNode(identifier.ToLower(), args.ToArray());
                    }

                    if(Current.TokenType != TokenType.ClosePar)
                        return new ErrorNode("Expected `)`");

                    Next();
                    return f;
                }

                if(Look().TokenType == TokenType.OpenSquare)
                {
                    Console.WriteLine("found indexer");
                    Move(2);
                    var value = ParseTernary();

                    Console.WriteLine(value);

                    if(Current.TokenType != TokenType.CloseSquare)
                        return new ErrorNode("Expected `]`");

                    Next();
                    return new KeyNode(identifier, value);
                }
                            
                Next();
                return new VarNode(identifier);
            }

            //BonusNode
            if(Current.TokenType == TokenType.Bonus)
            {
                Next();
                if(Current.TokenType == TokenType.Var)
                {
                    var bName = Current.Value.ToUpper();
                    Next();
                    if(Current.TokenType == TokenType.Separator)
                    {
                        Next();
                        var bType = ParseTernary();
                        if(Current.TokenType == TokenType.Separator)
                        {
                            Next();
                            var bValue = ParseTernary();
                            return new BonusNode(bName, bType, bValue);
                        }
                    }
                    else
                        return new BonusNode(bName);
                }
                return new ErrorNode("%?");
            }
            
            //StringNode
            if(Current.TokenType == TokenType.String)
            {
                Next();              
                return new StringNode(Look(-1).Value.Trim('"'));
            }
            
            //StoredExpressionNode
            if(Current.TokenType == TokenType.Expression)
            {
                Next();
                return new StoredExpressionNode(Look(-1).Value.Trim('`').Trim(new char[] {'{', '}' }));
            }

            //Defined Function
            if(Current.TokenType == TokenType.Lambda)
            {
                Next();
                if(Current.TokenType == TokenType.OpenPar)
                {                    
                    Next();
                    var parameters = new List<string>();
                    if(Current.TokenType == TokenType.ClosePar)
                    {

                    }
                    else
                    {
                        while(true)
                        {
                            var result = ParseTernary();
                            if(result is VarNode v)
                                parameters.Add(v.VarName);
                            else
                                return new ErrorNode("Expected valid variable name for function parameter");

                            if(Current.TokenType == TokenType.Comma)
                            {
                                Next();
                                continue;
                            }
                            break;
                        }
                    }
                    Console.WriteLine($"got function params");

                    if(Current.TokenType != TokenType.ClosePar)
                        return new ErrorNode("Expected `)`");
                    
                    Next();
                    if(Current.TokenType == TokenType.OpenSquig)
                    {
                        Console.WriteLine("function: open squig");
                        var list = new List<Token>();

                        Next();
                        while(Current.TokenType != TokenType.CloseSquig)
                        {
                            if(Current.TokenType == TokenType.OpenSquig)
                                list.AddRange(ParseDepth(TokenType.OpenSquig, TokenType.CloseSquig));
                                           
                            list.Add(Current);
                            Next();
                        }
                        Next();
                        Console.WriteLine($"parsed function:\n{Tokenizer.Output(list)}");
                        return new DefNode(list, parameters.ToArray());
                    }
                    

                   

                }    
            }

            return new ErrorNode("%?");
        }

        List<Token> ParseDepth(TokenType open, TokenType close)
        {
            var list = new List<Token>();            
            while(Current.TokenType != close)
            {
                list.Add(Current);
                Next();
                if( Current.TokenType == open)
                    list.AddRange(ParseDepth(open, close));
                if(Current.TokenType == TokenType.EOF)
                    break;          
            }
            return list;
        }

        public static ExpressionNode Parse(string expr, IContext ctx = null!, StringBuilder sb = null!) =>
            Parse(new Tokenizer(new StringReader(expr)).Tokens, ctx, sb);
       
        public static ExpressionNode Parse(List<Token> tokens, IContext ctx = null!, StringBuilder sb = null!)
        {
            var parser = new Parser(tokens, ctx, sb);
            return parser.ParseExpr();
        }       
    }                 
}   

