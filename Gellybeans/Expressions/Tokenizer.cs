using System.Text;
using System.Globalization;

namespace Gellybeans.Expressions
{
    /// <summary>
    /// The Tokenizer class walks through a string from beginning to end, stopping with each call of `NextToken()`. 
    /// 
    /// Tokenizers are meant to be consumed by a Parser in order to determine upcoming values in a given expression.
    /// </summary>
    
    public class Tokenizer
    {
        
        readonly TextReader? reader;
        
        int number;
        string identifier = "";

        char            currentChar;

        public List<Token> Tokens = new List<Token>();
   
        public int          Number      { get { return number; } }
        public string       Identifier  { get { return identifier; } }
        public char         CurrentChar { get { return currentChar; } }
       

        public Tokenizer(TextReader textReader)
        {
            reader = textReader;
            NextChar();
            NextToken();
            Tokenize();
        }

        public Tokenizer(List<Token> tokens) =>
            Tokens = tokens;

        public TokenType this[int index] { get { return Tokens[index].TokenType; } }


        void NextChar()
        {
            int chr = reader.Read();
            currentChar = chr < 0 ? '\0' : (char)chr;
        }

        void Tokenize()
        {
            while(Tokens[^1].TokenType != TokenType.EOF && Tokens[^1].TokenType != TokenType.Error)
            {
                NextToken();
            }
        }

        public void ListTokens()
        {
            for (int i = 0; i < Tokens.Count; i++) { }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for(int i = 0; i < Tokens.Count; i++)
                sb.Append(Tokens[i].ToString());
            return sb.ToString();
        }

        public string ToString(int index)
        {
            var sb = new StringBuilder();
            for(int i = index; i < Tokens.Count; i++)
                sb.Append(Tokens[i].ToString());
            return sb.ToString();
        }

        public static string Output(List<Token> tokens, int index = 0)
        {
            var sb = new StringBuilder();
            for(int i = index; i < tokens.Count; i++)
                sb.Append(tokens[i].ToString());

            return sb.ToString();
        }

        public char Peek()
        {
            int chr = reader.Peek();
            char c = chr < 0 ? '\0' : (char)chr;
            return c;
        }

        public void NextToken()
        {          
            while(char.IsWhiteSpace(currentChar)) { NextChar(); }          

            switch(currentChar)
            {
                case '\0':
                    Tokens.Add(new Token(TokenType.EOF));
                    return;

                case ';':
                    NextChar();
                    Tokens.Add(new Token(TokenType.Semicolon, ";"));
                    return;

                case ':':
                    NextChar();
                    if(currentChar == '?')
                    {
                        NextChar();
                        Tokens.Add(new Token(TokenType.HasFlag, ":?"));
                    }
                    else if(currentChar == ':')
                    {
                        NextChar();
                        Tokens.Add(new Token(TokenType.Pair, "::"));
                    }
                    else
                    {
                        Tokens.Add(new Token(TokenType.Separator, ":"));
                    }        
                    return;                         
                
                case '=':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        Tokens.Add(new Token(TokenType.Equals, "=="));
                    }                    
                    else
                    {
                        Tokens.Add(new Token(TokenType.Assign, "="));
                    }                
                    return;

                case '.':
                    NextChar();
                    if(currentChar == '.')
                    {
                        NextChar();
                        Tokens.Add(new Token(TokenType.Range, ".."));
                    }
                    if(currentChar == '^')
                    {
                        NextChar();
                        Tokens.Add(new Token(TokenType.Random, ".^"));
                    }
                    return;

                case '?':
                    NextChar();
                    Tokens.Add(new Token(TokenType.Ternary, "?"));
                    return;

                case '$':
                    NextChar();
                    if(currentChar == '?')
                    {
                        NextChar();
                        Tokens.Add(new Token(TokenType.GetBonus, "$?"));
                    }
                    else
                    {
                        Tokens.Add(new Token(TokenType.Dollar, "$"));
                    }                 
                    return;


                case '|':
                    NextChar();
                    if(currentChar == '|')
                    {
                        NextChar();
                        Tokens.Add(new Token(TokenType.LogicalOr, "||"));
                    }                  
                    else if(currentChar == '=')
                    {
                        NextChar();
                        Tokens.Add(new Token(TokenType.Assign, "|="));
                    }              
                    else
                    {
                        Tokens.Add(new Token(TokenType.Pipe, "|"));
                    }                    
                    return;
                
                case '&':
                    NextChar();
                    if(currentChar == '&')
                    {
                        NextChar();
                        Tokens.Add(new Token(TokenType.LogicalAnd, "&&"));
                    }
                    else
                    {
                        Tokens.Add(new Token(TokenType.And, "&"));
                    }                     
                    return;

                case '!':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        Tokens.Add(new Token(TokenType.NotEquals, "!="));
                    }
                    else
                    {
                        Tokens.Add(new Token(TokenType.Not, "!"));
                    }                   
                    return;
                             
                case '>':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        Tokens.Add(new Token(TokenType.GreaterEquals, ">="));
                    }
                    else if(Peek() == '>')
                    {
                        NextChar();
                        NextChar();
                        Tokens.Add(new Token(TokenType.Append, ">>>"));
                    }
                    else if(currentChar == '>')
                    {
                        NextChar();
                        Tokens.Add(new Token(TokenType.Pull, ">>"));
                    }                 
                    else
                    {
                        Tokens.Add(new Token(TokenType.Greater, ">"));
                    }                   
                    return;
                                
                case '<':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        Tokens.Add(new Token(TokenType.LessEquals, "<="));
                    }
                    else if(currentChar == '<')
                    {
                        NextChar();
                        Tokens.Add(new Token(TokenType.Push, "<<"));
                    }
                    else
                    {
                        Tokens.Add(new Token(TokenType.Less, "<"));
                    }
                    return;              
                
                
                case '+':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        Tokens.Add(new Token(TokenType.Assign, "+="));
                    }                   
                    else
                    {
                        Tokens.Add(new Token(TokenType.Add, "+"));
                    }                       
                    return;

                case '-':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        Tokens.Add(new Token(TokenType.Assign, "-="));
                    }
                    else if(currentChar == '>')
                    {
                        NextChar();
                        Tokens.Add(new(TokenType.Lambda, "->"));
                    }
                    else
                    {
                        Tokens.Add(new Token(TokenType.Sub, "-"));
                    } 
                    return;

                case '*':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        Tokens.Add(new Token(TokenType.Assign, "*="));
                    }
                    else
                    {
                        Tokens.Add(new Token(TokenType.Mul, "*"));
                    }                      
                    return;

                case '/':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        Tokens.Add(new Token(TokenType.Assign, "/="));
                    }
                    else
                    {
                        Tokens.Add(new Token(TokenType.Div, "/"));
                    }                                              
                    return;

                case '%':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        Tokens.Add(new Token(TokenType.Assign, "%="));
                    }
                    else if(currentChar == '%')
                    {
                        NextChar();
                        Tokens.Add(new Token(TokenType.ToExpr));
                    }
                    else
                    {
                        Tokens.Add(new Token(TokenType.Percent, "%"));
                    }                       
                    return;

                case '[':
                    NextChar();
                    Tokens.Add(new Token(TokenType.OpenSquare, "["));
                    return;

                case ']':
                    NextChar();
                    Tokens.Add(new Token(TokenType.CloseSquare, "]"));
                    return;

                case '(':
                    NextChar();
                    Tokens.Add(new Token(TokenType.OpenPar, "("));
                    return;

                case ')':
                    NextChar();
                    Tokens.Add(new Token(TokenType.ClosePar, ")"));
                    return;

                case '{':
                    NextChar();
                    Tokens.Add(new Token(TokenType.OpenSquig, "{"));
                    return;

                case '}':
                    NextChar();
                    Tokens.Add(new Token(TokenType.CloseSquig, "}"));
                    return;

                case ',':
                    NextChar();
                    Tokens.Add(new Token(TokenType.Comma, ","));
                    return;

                case '@':
                    NextChar();
                    Tokens.Add(new Token(TokenType.Base, "@"));
                    return;

                case '^':
                    NextChar();
                    if(currentChar == '^')
                    {
                        NextChar();
                        Tokens.Add(new Token(TokenType.DoubleCaret, "^^"));
                    }
                    else
                    {
                        Tokens.Add(new Token(TokenType.Caret, "^"));
                    }
                    return;
                
                case '#':
                    NextChar();
                    Tokens.Add(new Token(TokenType.Self, "#"));
                    return;

                case '\\':
                    NextChar();
                    Tokens.Add(new Token(TokenType.Remove, @"\"));
                    return;

                case '\'':
                    NextChar();
                    Tokens.Add(new Token(TokenType.Event, "'"));
                    return;
            }

            var sb = new StringBuilder();
            
            //dice expr
            if((currentChar >= '0' && currentChar <= '9')  || currentChar == 'd')
            {                
                sb.Append(currentChar);
                NextChar();

                while(char.IsDigit(currentChar) || IsDice(currentChar))
                {
                    sb.Append(currentChar);
                    NextChar();                                   
                }

                if(sb.ToString().Where(x => x == 'd').Count() == 1 && sb.ToString().Any(x => x>= '0' && x<= '9'))
                {
                    identifier = sb.ToString();
                    Tokens.Add(new Token(TokenType.Dice, sb.ToString()));
                    return;
                }                            
                else if(sb.ToString().All(x => x >= '0' && x <= '9'))
                {
                    number = int.Parse(sb.ToString(), CultureInfo.InvariantCulture);
                    Tokens.Add(new Token(TokenType.Number, sb.ToString()));
                    return;
                }           
            }

            //expression encapsulation for var assignment
            if(currentChar == '`')
            {
                NextChar();
                while(currentChar != '`')
                {
                    if(currentChar == '{')
                    {
                        while(currentChar != '}')
                        {
                            
                        
                            if(currentChar == '\0')
                            {
                                Tokens.Add(new Token(TokenType.Error, "Expected }"));
                                return;
                            }
                            sb.Append(currentChar);
                            NextChar();

                            if(currentChar == '{')
                            {
                                while(currentChar != '}')
                                {
                                    if(currentChar == '\0')
                                    {
                                        Tokens.Add(new Token(TokenType.Error, "Expected }"));
                                        return;
                                    }
                                    sb.Append(currentChar);
                                    NextChar();
                                }
                            }
                        }
                    }
                    
                    if(currentChar == '\0')
                    {
                        Tokens.Add(new Token(TokenType.Error, "Expected `"));
                        return;
                    }                    

                    sb.Append(currentChar);
                    NextChar();
                }
                NextChar();
                Tokens.Add(new Token(TokenType.Expression, $"`{sb}`"));
                return;
            }

            //string
            if(currentChar == '"')
            {
                NextChar();
                while(currentChar != '"')
                {
                    if(currentChar == '{')
                        sb.Append(ParseDepth('{', '}'));
                                   
                    if(currentChar == '\0')
                    {
                        Tokens.Add(new Token(TokenType.Error, "Expected \""));
                        return;
                    }
                    if(currentChar != '"')
                    {
                        sb.Append(currentChar);
                        NextChar();
                    }
                }
                NextChar();
                Tokens.Add(new Token(TokenType.String, $"\"{sb}\""));
                return;
            }
            
            //var
            if(char.IsLetter(currentChar) || !char.IsAscii(currentChar) || currentChar == '_' || currentChar == '^' || char.IsDigit(currentChar))
            {
                sb.Append(currentChar);
                NextChar();
                
                while(IsVar(currentChar))
                {
                    sb.Append(currentChar);
                    NextChar();
                }
                identifier = sb.ToString();
                Tokens.Add(new Token(TokenType.Var, sb.ToString()));
                return;              
            }

            Tokens.Add(new Token(TokenType.Error, $"Invalid data: {currentChar}"));
            return;
        }

        string ParseDepth(char open, char close)
        {
            var sb = new StringBuilder();
            
            while(currentChar != close)
            {
                sb.Append(currentChar);
                NextChar();
                if(currentChar == open)
                    sb.Append(ParseDepth(open, close));
                if(currentChar == '\0')
                {
                    Tokens.Add(new Token(TokenType.Error, $"Expected `{close}`"));
                    break;
                }                 
            }
            sb.Append(currentChar);
            NextChar();
            return sb.ToString();
        }

        public static bool IsDice(char c) =>
            c == 'd' || c == 'r' || c == 'h' || c == 'l';

        public static bool IsVar(char c) =>
            char.IsLetter(c) || !char.IsAscii(c) || c == '_' || char.IsDigit(c);
    }
}
