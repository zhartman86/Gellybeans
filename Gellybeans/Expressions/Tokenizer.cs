using System.Text;
using System.Globalization;

namespace Gellybeans.Expressions
{
    public class Tokenizer
    {
        private TextReader reader;

        char        currentChar;
        TokenType   currentToken;
        
        int     number;
        string  identifier = "";


        public TokenType    Token       { get { return currentToken; } }       
        public int          Number      { get { return number; } }
        public string       Identifier  { get { return identifier; } }
        
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

        public void NextToken()
        {
            while(char.IsWhiteSpace(currentChar)) { NextChar(); }
        
            switch(currentChar)
            {
                case '\0':
                    currentToken = TokenType.EOF;
                    return;
              
                
                case '=':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.Equals;
                    }
                    else currentToken = TokenType.AssignEquals;
                    return;

                case '!':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.NotEquals;
                    }
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
                    else currentToken = TokenType.Add;
                    return;

                case '-':
                    NextChar();
                    if(currentChar == '=')
                    {
                        NextChar();
                        currentToken = TokenType.AssignSub;
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
            
            if(char.IsDigit(currentChar) || currentChar == 'd' || currentChar == 'D')
            {              
                var builder = new StringBuilder();

                bool hasD = false;
                
                while(char.IsDigit(currentChar) || (!hasD && currentChar == 'd') || (!hasD && currentChar == 'D'))
                {
                    builder.Append(currentChar);
                    hasD = currentChar == 'd' || currentChar == 'D';
                    NextChar();
                    if(currentChar == 'r')
                    {
                        builder.Append(currentChar);
                        NextChar();
                    }                       
                }

                var bts = builder.ToString();
                if(bts.Contains('d') || bts.Contains('D'))
                {
                    currentToken = TokenType.Dice;
                    identifier = builder.ToString();
                    return;
                }
                else
                {
                    number = int.Parse(builder.ToString(), CultureInfo.InvariantCulture);
                    currentToken = TokenType.Number;
                    return;
                }                    
            }

            if(char.IsLetter(currentChar) || currentChar == '_' || currentChar == '$')
            {
                var builder = new StringBuilder();

                while(char.IsLetterOrDigit(currentChar) || currentChar == '_' || currentChar == '$')
                {
                    builder.Append(currentChar);
                    NextChar();
                }

                identifier = builder.ToString();
                currentToken = TokenType.Var;
                return;
            }

            throw new Exception($"Invalid data: {currentChar}");
        }
    }
}
