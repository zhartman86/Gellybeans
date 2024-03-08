namespace Gellybeans.Expressions
{
    public enum TokenType
    {
        None,
        Ternary,        // ?::
        Bonus,          // $
        GetBonus,       // $?
        Base,           // @
        Assign,         // = += -= *= /= %= |=
        AssignExpr,     // ::
        Remove,         // \
        AssignFlag,     // |=
        LogicalOr,      // ||
        LogicalAnd,     // &&
        BitwiseOr,      //
        BitwiseXor,     //
        BitwiseAnd,     //
        Equals,         // ==
        NotEquals,      // !=
        Greater,        // >
        GreaterEquals,  // >=
        Less,           // <
        LessEquals,     // <=
        Not,            // !
        Add,            // +
        Sub,            // -
        Mul,            // *
        Div,            // /
        Modulo,         // %
        Number,         // integer
        OpenPar,        // (
        ClosePar,       // )
        OpenSquig,      // {
        CloseSquig,     // }
        OpenSquare,     // [
        CloseSquare,    // ]
        Comma,          // ,
        BeginString,    // "
        SingleQuote,    // '
        Semicolon,      // ;
        Expression,     // `
        Var,            //
        String,         // "abc" or 'abc'
        Dice,           // 1d20
        Separator,      // :
        HasFlag,        // :?       
        Error,
        EOF,
    }
}
