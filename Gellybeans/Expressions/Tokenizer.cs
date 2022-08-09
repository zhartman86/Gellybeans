using System.Text;
using System.Globalization;

namespace Gellybeans.Expressions
{
    public class Tokenizer
    {
        private TextReader reader;

        char currentChar;
        TokenType currentToken;
        int number;


        public TokenType Token { get { return currentToken; } }
        public int Number { get { return number; } }

        public Tokenizer(TextReader textReader)
        {
            reader = textReader;
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

            throw new Exception($"Invalid data: {currentChar}");
        }

      

    }
}
