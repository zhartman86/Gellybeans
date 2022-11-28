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
        TextReader reader;

        char        currentChar;
        TokenType   currentToken;
        
        int     number;
        string  identifier  = "";

        public TokenType    Token       { get { return currentToken; } }       
        public int          Number      { get { return number; } }
        public string       Identifier  { get { return identifier; } }
        public char         CurrentChar { get { return currentChar; } }
        
        public Tokenizer(TextReader textReader)
        {
            reader = textReader;
            NextChar();
            NextToken();
        }

        void NextChar()
        {
            int chr = reader.Read();
            currentChar = chr < 0 ? '\0' : (char)chr;
        }

        char Peek()
        {
            int chr = reader.Peek();
            return chr < 0 ? '\0' : (char)chr;
        }

        public void NextToken()
        {
            while(char.IsWhiteSpace(currentChar)) { NextChar(); }          

            switch(currentChar)
            {
                case '\0':
                    currentToken = TokenType.EOF;
                    return;
                
                case ':':
                    NextChar();
                    if(currentChar == ':')
                    {

                    }
                    currentToken = TokenType.Separator;
                    return;

                case ';':
                    NextChar();
                    currentToken = TokenType.Semicolon;
                    return;                
                
                case '=':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.Equals;
                    }
                    else currentToken = TokenType.Assign;
                    return;

                case '?':
                    NextChar();
                    currentToken = TokenType.Ternary;
                    return;

                case '$':
                    NextChar();
                    currentToken = TokenType.Bonus;
                    return;


                case '#':
                    NextChar();
                    currentToken = TokenType.AssignExpr;
                    return;
                    
                
                case '|':
                    NextChar();
                    if(currentChar == '|')
                    {
                        NextChar();
                        currentToken = TokenType.LogicalOr;
                    }                  
                    else currentToken = TokenType.BitwiseOr;
                    return;
                
                case '&':
                    NextChar();
                    if(currentChar == '&')
                    {
                        NextChar();
                        currentToken = TokenType.LogicalAnd;
                    }
                    else currentToken = TokenType.BitwiseAnd;
                    return;

                case '!':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.NotEquals;
                    }
                    else currentToken = TokenType.Not;
                    return;
                             
                case '>':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.GreaterEquals;
                    }                       
                    else currentToken = TokenType.Greater;
                    return;
                                
                case '<':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.LessEquals;
                    }                  
                    else currentToken = TokenType.Less;
                    return;              
                
                
                case '+':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.AssignAdd;
                    }
                    else if(currentChar == '$')
                    {
                        NextChar();
                        currentToken = TokenType.AssignAddBon;
                    }                     
                    else currentToken = TokenType.Add;
                    return;

                case '-':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.AssignSub;
                    }
                    else if(currentChar == '$')
                    {
                        NextChar();
                        currentToken = TokenType.AssignSubBon;
                    }
                    else currentToken = TokenType.Sub;
                    return;

                case '*':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.AssignMul;
                    }
                    else currentToken = TokenType.Mul;
                    return;

                case '/':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.AssignDiv;
                    }
                    else currentToken = TokenType.Div;
                    return;

                case '%':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.AssignMod;
                    }
                    else currentToken = TokenType.Modulo;
                    return;

                case '(':
                    NextChar();
                    currentToken = TokenType.OpenPar;
                    return;

                case ')':
                    NextChar();
                    currentToken = TokenType.ClosePar;
                    return;               
                
                case ',':
                    NextChar();
                    currentToken = TokenType.Comma;
                    return;
            }       
            
            
            if(char.IsDigit(currentChar) || currentChar == 'd')
            {
                var sb = new StringBuilder();
                sb.Append(currentChar);
                
                //if the first two characters are a combination of 'd' and a number, treat as a dice expression.
                //this allows for the omission of the first number, in which case it will be assumed to be 1.
                if((currentChar == 'd' || Peek() == 'd') && (char.IsDigit(currentChar) || char.IsDigit(Peek())))
                {                   
                    NextChar();
                    while(char.IsLetterOrDigit(currentChar))
                    {
                        sb.Append(currentChar);
                        NextChar();
                    }
                    
                    identifier = sb.ToString();
                    currentToken = TokenType.Dice;
                    return;                   
                }               
                else
                {
                    if(char.IsDigit(currentChar))
                    {
                        NextChar();
                        while(char.IsDigit(currentChar))
                        {
                            sb.Append(currentChar);
                            NextChar();
                        }
                    }
                    number = int.Parse(sb.ToString(), CultureInfo.InvariantCulture);
                    currentToken = TokenType.Number;
                    return;
                }           
            }

            if(char.IsLetter(currentChar) || !char.IsAscii(currentChar) || currentChar == '_' || currentChar == '"' || currentChar == '@')
            {
                var sb = new StringBuilder();

                if(currentChar == '"')
                {
                    NextChar();
                    while(currentChar != '"')
                    {
                        sb.Append(currentChar);
                        NextChar();                       
                    }
                    NextChar();
                }
                else
                    while(char.IsLetterOrDigit(currentChar) || !char.IsAscii(currentChar) || currentChar == '_' || currentChar == '@')
                    {
                        sb.Append(currentChar);
                        NextChar();
                    }
                
                identifier = sb.ToString();
                currentToken = TokenType.Var;
                return;
            }

            //ignore square brackets and any enclosed characters.
            if(currentChar == '[')
            {
                while(currentChar != ']')
                    NextChar();
                NextChar();
                NextToken();
            }

            currentToken = TokenType.Error;
            Console.WriteLine($"Invalid data: {currentChar}");
            return;
        }
    }
}
