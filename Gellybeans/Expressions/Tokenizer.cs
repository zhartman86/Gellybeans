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
        TokenType       currentToken;

        public List<Token> Tokens = new List<Token>();

        public TokenType    Token       { get { return currentToken; } set { currentToken = value; } }       
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
            while(currentToken != TokenType.EOF && currentToken != TokenType.Error)
            {
                NextToken();
            }
        }

        public void ListTokens()
        {
            for (int i = 0; i < Tokens.Count; i++) 
                Console.WriteLine($"{Tokens[i].TokenType}");      
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

            Console.WriteLine(sb.ToString());
            return sb.ToString();
        }

        public void Peek()
        {

        }

        public void NextToken()
        {          
            while(char.IsWhiteSpace(currentChar)) { NextChar(); }          

            switch(currentChar)
            {
                case '\0':
                    currentToken = TokenType.EOF;
                    Tokens.Add(new Token(TokenType.EOF));
                    return;

                case ';':
                    NextChar();
                    currentToken = TokenType.Semicolon;
                    Tokens.Add(new Token(TokenType.Semicolon, ";"));
                    return;

                case ':':
                    NextChar();
                    if(currentChar == '?')
                    {
                        NextChar();
                        currentToken = TokenType.HasFlag;
                        Tokens.Add(new Token(TokenType.HasFlag, ":?"));
                    }
                    else if(currentChar == ':')
                    {
                        NextChar();
                        currentToken = TokenType.AssignExpr;
                        Tokens.Add(new Token(TokenType.AssignExpr, "::"));
                    }
                    else
                    {
                        currentToken = TokenType.Separator;
                        Tokens.Add(new Token(TokenType.Separator, ":"));
                    }        
                    return;                         
                
                case '=':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.Equals;
                        Tokens.Add(new Token(TokenType.Equals, "=="));
                    }
                    else
                    {
                        currentToken = TokenType.Assign;
                        Tokens.Add(new Token(TokenType.Assign, "="));
                    }                
                    return;

                case '?':
                    NextChar();
                    currentToken = TokenType.Ternary;
                    Tokens.Add(new Token(TokenType.Ternary, "?"));
                    return;

                case '$':
                    NextChar();
                    if(currentChar == '?')
                    {
                        NextChar();
                        currentToken = TokenType.GetBonus;
                        Tokens.Add(new Token(TokenType.GetBonus, "$?"));
                    }
                    currentToken = TokenType.Bonus;
                    Tokens.Add(new Token(TokenType.Bonus, "$"));
                    return;


                case '|':
                    NextChar();
                    if(currentChar == '|')
                    {
                        NextChar();
                        currentToken = TokenType.LogicalOr;
                        Tokens.Add(new Token(TokenType.LogicalOr, "||"));
                    }                  
                    else if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.Assign;
                        Tokens.Add(new Token(TokenType.Assign, "|="));
                    }              
                    else
                    {
                        currentToken = TokenType.BitwiseOr;
                        Tokens.Add(new Token(TokenType.BitwiseOr, "|"));
                    }                    
                    return;
                
                case '&':
                    NextChar();
                    if(currentChar == '&')
                    {
                        NextChar();
                        currentToken = TokenType.LogicalAnd;
                        Tokens.Add(new Token(TokenType.LogicalAnd, "&&"));
                    }
                    else
                    {
                        currentToken = TokenType.BitwiseAnd;
                        Tokens.Add(new Token(TokenType.BitwiseAnd, "&"));
                    }                     
                    return;

                case '!':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.NotEquals;
                        Tokens.Add(new Token(TokenType.NotEquals, "!="));
                    }
                    else
                    {
                        currentToken = TokenType.Not;
                        Tokens.Add(new Token(TokenType.Not, "!"));
                    }                   
                    return;
                             
                case '>':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.GreaterEquals;
                        Tokens.Add(new Token(TokenType.GreaterEquals, ">="));
                    }                       
                    else
                    {
                        currentToken = TokenType.Greater;
                        Tokens.Add(new Token(TokenType.Greater, ">"));
                    }                   
                    return;
                                
                case '<':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.LessEquals;
                        Tokens.Add(new Token(TokenType.LessEquals, "<="));
                    }                  
                    else
                    {
                        currentToken = TokenType.Less;
                        Tokens.Add(new Token(TokenType.Less, "<"));
                    }
                    return;              
                
                
                case '+':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.Assign;
                        Tokens.Add(new Token(TokenType.Assign, "+="));
                    }                   
                    else
                    {
                        currentToken = TokenType.Add;
                        Tokens.Add(new Token(TokenType.Add, "+"));
                    }                       
                    return;

                case '-':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.Assign;
                        Tokens.Add(new Token(TokenType.Assign, "-="));
                    }
                    else
                    {
                        currentToken = TokenType.Sub;
                        Tokens.Add(new Token(TokenType.Sub, "-"));
                    } 
                    return;

                case '*':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.Assign;
                        Tokens.Add(new Token(TokenType.Assign, "*="));
                    }
                    else
                    {
                        currentToken = TokenType.Mul;
                        Tokens.Add(new Token(TokenType.Mul, "*"));
                    }                      
                    return;

                case '/':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.Assign;
                        Tokens.Add(new Token(TokenType.Assign, "/="));
                    }
                    else
                    {
                        currentToken = TokenType.Div;
                        Tokens.Add(new Token(TokenType.Div, "/"));
                    }                                              
                    return;

                case '%':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.Assign;
                        Tokens.Add(new Token(TokenType.Assign, "%="));
                    }
                    else
                    {
                        currentToken = TokenType.Modulo;
                        Tokens.Add(new Token(TokenType.Modulo, "%"));
                    }                       
                    return;

                case '[':
                    NextChar();
                    currentToken = TokenType.BeginMacro;
                    Tokens.Add(new Token(TokenType.BeginMacro, "["));
                    return;

                case ']':
                    NextChar();
                    currentToken = TokenType.EndMacro;
                    Tokens.Add(new Token(TokenType.EndMacro, "]"));
                    return;

                case '(':
                    NextChar();
                    currentToken = TokenType.OpenPar;
                    Tokens.Add(new Token(TokenType.OpenPar, "("));
                    return;

                case ')':
                    NextChar();
                    currentToken = TokenType.ClosePar;
                    Tokens.Add(new Token(TokenType.ClosePar, ")"));
                    return;               
                
                case ',':
                    NextChar();
                    currentToken = TokenType.Comma;
                    Tokens.Add(new Token(TokenType.Comma, ","));
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
                    currentToken = TokenType.Dice;
                    Tokens.Add(new Token(TokenType.Dice, sb.ToString()));
                    return;
                }                            
                else if(sb.ToString().All(x => x >= '0' && x <= '9'))
                {
                    number = int.Parse(sb.ToString(), CultureInfo.InvariantCulture);
                    currentToken = TokenType.Number;
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
                                currentToken = TokenType.Error;
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
                                        currentToken = TokenType.Error;
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
                        currentToken = TokenType.Error;
                        return;
                    }                    

                    sb.Append(currentChar);
                    NextChar();
                }
                NextChar();
                currentToken = TokenType.Expression;
                Tokens.Add(new Token(TokenType.Expression, sb.ToString()));
                return;
            }

            //string
            if(currentChar == '"')
            {
                NextChar();
                while(currentChar != '"')
                {
                    if(currentChar == '{')
                    {
                        sb.Append(currentChar);
                        NextChar();
                        while(currentChar != '}')
                        {
                            if(currentChar == '\0')
                            {
                                Tokens.Add(new Token(TokenType.Error, "Expected }"));
                                currentToken = TokenType.Error;
                                return;
                            }
                            sb.Append(currentChar);
                            NextChar();
                        }
                    }

                    if(currentChar == '\0')
                    {
                        Tokens.Add(new Token(TokenType.Error, "Expected \""));
                        currentToken = TokenType.Error;
                        return;
                    }
                    sb.Append(currentChar);
                    NextChar();
                }
                NextChar();
                currentToken = TokenType.String;
                Tokens.Add(new Token(TokenType.String, sb.ToString()));
                return;
            }

            //substring
            if(currentChar == '\'')
            {
                NextChar();
                while(currentChar != '\'')
                {
                    if(currentChar == '\0')
                    {
                        Tokens.Add(new Token(TokenType.Error, "Expected '"));
                        currentToken = TokenType.Error;
                        return;
                    }

                    sb.Append(currentChar);
                    NextChar();
                }
                NextChar();
                currentToken = TokenType.String;
                Tokens.Add(new Token(TokenType.String, sb.ToString()));
                return;
            }          

            //var
            if(char.IsLetter(currentChar) || !char.IsAscii(currentChar) || currentChar == '_' || currentChar == '@' || currentChar == '^' || currentChar == '.' || char.IsDigit(currentChar))
            {
                sb.Append(currentChar);
                NextChar();
                
                while(IsVar(currentChar))
                {
                    sb.Append(currentChar);
                    NextChar();
                }
                identifier = sb.ToString();
                currentToken = TokenType.Var;
                Tokens.Add(new Token(TokenType.Var, sb.ToString()));
                return;              
            }




            currentToken = TokenType.Error;
            Tokens.Add(new Token(TokenType.Error, $"Invalid data: {currentChar}"));
            Console.WriteLine($"Invalid data: {currentChar}");
            return;
        }
    
        public static bool IsDice(char c) =>
            c == 'd' || c == 'r' || c == 'h' || c == 'l';

        public static bool IsVar(char c) =>
            char.IsLetter(c) || !char.IsAscii(c) || c == '_' || char.IsDigit(c);
    }
}
