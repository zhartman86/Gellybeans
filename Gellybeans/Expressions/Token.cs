namespace Gellybeans.Expressions
{
    public class Token
    {
        public TokenType TokenType { get; set; }
        public string Value { get; set; } = "";

        public override string ToString() =>
             Value;

        public Token(TokenType tokenType, string value = "") 
        { 
            this.TokenType = tokenType;
            this.Value = value;
        }
    }
}
