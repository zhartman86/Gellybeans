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
        IContext ctx;
        int index;
        StringBuilder sb;

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

            Console.WriteLine("found node");

            if(Current.TokenType == TokenType.EOF)
                return expr;

            return new StringNode($"Invalid expression. Error on token `{Current.Value}`");
        }

        ExpressionNode ParseTermination()
        {
            var expr = ParseAssignment();

            if(Current.TokenType == TokenType.Semicolon)
            {
                Next();
                expr.Eval(ctx, sb);
                return ParseTermination();
            }
            return expr;
        }

        ExpressionNode ParseAssignment()
        {
            var node = ParseTernary();

            if(node is VarNode varNode && Current.TokenType == TokenType.Assign)
            {
                Func<string, dynamic, dynamic> op = null!;

                var varName = varNode.VarName.ToUpper();
                
                switch(Current.Value)
                {
                    case "=":
                        op = (identifier, assignment) =>
                        {
                            if(assignment is int i)
                                assignment = new Stat(i);
                            
                            ctx[varName] = assignment;
                            sb?.AppendLine($"{varName} set to {assignment}");

                            return $"{assignment}";
                        };
                        break;
                    case "+=":
                        if(ctx.Vars.TryGetValue(varName, out var var))
                        {
                            op = (identifier, assignment) =>
                            {
                                if(var is Stat stat)
                                {                                                                                                      
                                    if(assignment is int i)
                                    {
                                        stat.Base += i;
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
                        if(ctx.Vars.TryGetValue(varName, out var))
                        {
                            op = (identifier, assignment) =>
                            {
                                if(var is Stat stat)
                                {
                                    if(assignment is int i)
                                    {
                                        stat.Base -= i;
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
                        if(ctx.Vars.TryGetValue(varName, out var))
                        {
                            op = (identifier, assignment) =>
                            {
                                if(var is Stat stat)
                                {
                                    if(assignment is int i)
                                    {
                                        stat.Base *= i;
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
                        if(ctx.Vars.TryGetValue(varName, out var))
                        {
                            op = (identifier, assignment) =>
                            {
                                if(var is Stat stat)
                                {
                                    if(assignment is int i)
                                    {
                                        stat.Base /= i;
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
                        if(ctx.Vars.TryGetValue(varName, out var))
                        {
                            op = (identifier, assignment) =>
                            {
                                if(var is Stat stat)
                                {
                                    if(assignment is int i)
                                    {
                                        stat.Base %= i;
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
                return new AssignVarNode(varNode.VarName, rhs, op);
            }
            return node;
        }

        ExpressionNode ParseTernary()
        {           
            var conditional = ParseLogicalAndOr();

            while(true)
            {
                Func<dynamic, dynamic, dynamic, dynamic> op = null!;

                if(Current.TokenType == TokenType.Ternary) { op = (a, b, c) => a == 1 ? b : c; }

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

                if(Current.TokenType == TokenType.LogicalOr) { op = (a, b) => (a == 1 || b == 1) ? 1 : 0; }
                else if(Current.TokenType == TokenType.LogicalAnd) { op = (a, b) => (a == 1 && b == 1) ? 1 : 0; }

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

                if(Current.TokenType == TokenType.BitwiseOr) { op = (a, b) => a | b; }
                else if(Current.TokenType == TokenType.BitwiseAnd) { op = (a, b) => a & b; }
                else if(Current.TokenType == TokenType.GetBonus) 
                { 
                    op = (identifier, type) =>
                    { 
                        if(ctx.Vars.TryGetValue(identifier.ToString(), out dynamic var))
                        {
                            if(var is StatValue s)
                                return s.Stat.GetBonus((BonusType)(int)type);
                            else
                                sb?.AppendLine($"{identifier} is not a stat value.");
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

                if(Current.TokenType == TokenType.Equals) { op = (a, b) => a == b ? 1 : 0; }
                else if(Current.TokenType == TokenType.NotEquals) { op = (a, b) => a != b ? 1 : 0; }
                else if(Current.TokenType == TokenType.HasFlag) { op = (a, b) => (a & b) != 0 ? 1 : 0; }

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

                if(Current.TokenType == TokenType.Greater) { op = (a, b) => a > b ? 1 : 0; }
                else if(Current.TokenType == TokenType.GreaterEquals) { op = (a, b) => a >= b ? 1 : 0; }
                else if(Current.TokenType == TokenType.Less) { op = (a, b) => a < b ? 1 : 0; }
                else if(Current.TokenType == TokenType.LessEquals) { op = (a, b) => a <= b ? 1 : 0; }

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
            if(Current.TokenType == TokenType.Add)
                    Next();

            if(Current.TokenType == TokenType.Sub)
            {
                Next();
                var rhs = ParseUnary();

                Func<dynamic, dynamic> op = null!;

                if(rhs is BonusNode b)
                {
                    op = (bonus) =>
                    {
                        var count = 0;
                        var stats = ctx.Vars.Values.OfType<Stat>();
                        Console.WriteLine($"stat count: {stats.Count()}. VAR TOTAL: {ctx.Vars.Count}");
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
                    return new StringNode("%Expected`)`");

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

            if(Current.TokenType == TokenType.BeginMacro)
            {
                Next();

                List<List<Token>> macro = new List<List<Token>>();


                var sub = new List<Token>();
                while(Current.TokenType != TokenType.EndMacro)
                {
                    if(Current.TokenType == TokenType.EOF)
                        return new StringNode("Expected `]`.");

                    if(Current.TokenType == TokenType.Semicolon)
                    {
                        Next();
                        macro.Add(sub);
                        sub = new List<Token>();
                    }
                    sub.Add(Current);
                    Next();
                }
                macro.Add(sub);
                Next();
                
                var exprs = new List<ExpressionNode>();

                for(int i = 0; i < macro.Count;i++)
                {
                    if(Current.TokenType != TokenType.EOF)
                    {
                        var tks = tokens.GetRange(index, tokens.Count - index);
                        macro[i].AddRange(tks);
                    }
                    
                    //tag end with EOF
                    if(macro[i][^1].TokenType != TokenType.EOF)
                        macro[i].Add(new Token(TokenType.EOF, "EOF"));
                    
                    var node = Parse(macro[i], ctx, sb);
                    exprs.Add(node);
                }
                index = tokens.Count - 1;
                return new ListNode(exprs);
            }


            //VarNode
            if(Current.TokenType == TokenType.Var)
            {
                var identifier = Current.Value;
               
                if(Look().TokenType == TokenType.OpenPar)
                {
                    Move(2);
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

                    if(Current.TokenType != TokenType.ClosePar)
                        return new StringNode("Expected `)`.");

                    Next();
                    return new FunctionNode(identifier, args.ToArray());
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
                    var bName = Current.Value;
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
                return new StringNode("%?");
            }
            
            //StringNode
            if(Current.TokenType == TokenType.String)
            {
                Next();              
                return new StringNode(Look(-1).Value);
            }
            
            //StoredExpressionNode
            if(Current.TokenType == TokenType.Expression)
            {
                Next();
                return new StoredExpressionNode(Look(-1).Value.Trim(new char[] {'{', '}' }));
            }

            return new StringNode("%?");
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

