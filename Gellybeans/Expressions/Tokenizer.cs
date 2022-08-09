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
        string  identifier;


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

                case '+':
                    NextChar();
                    currentToken = TokenType.Add;
                    return;

                case '-':
                    NextChar();
                    currentToken = TokenType.Sub;
                    return;

                case '*':
                    NextChar();
                    currentToken = TokenType.Mul;
                    return;

                case '/':
                    NextChar();
                    currentToken = TokenType.Div;
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
        
            if(char.IsDigit(currentChar))
            {
                var builder = new StringBuilder();
                while(char.IsDigit((char)currentChar))
                {
                    builder.Append(currentChar);
                    NextChar();
                }

                number = int.Parse(builder.ToString(), CultureInfo.InvariantCulture);
                currentToken = TokenType.Number;
                return;
            }

            if(char.IsLetter(currentChar) || currentChar == '_')
            {
                var builder = new StringBuilder();

                while(char.IsLetterOrDigit(currentChar) || currentChar == '_')
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
