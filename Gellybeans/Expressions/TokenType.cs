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
        Remove,         // \
        AssignFlag,     // |=
        LogicalOr,      // ||
        LogicalAnd,     // &&
        Pipe,           // |
        And,            // &
        Equals,         // ==
        NotEquals,      // !=
        Greater,        // >
        GreaterEquals,  // >=
        Lambda,         // ->
        Push,           // <<
        Pull,           // >>
        Append,         // >>>
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
        Self,           // #
        Var,            //
        String,         // "abc" or 'abc'
        Dice,           // 1d20
        Separator,      // :
        HasFlag,        // :?
        Range,          // ..              
        Caret,          // ^
        DoubleCaret,    // ^^
        Random,         // .^ 
        Error,
        EOF,
    }
}
