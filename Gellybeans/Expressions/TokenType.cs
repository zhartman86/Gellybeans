namespace Gellybeans.Expressions
{
    public enum TokenType
    {
        None,
        Ternary,        // ?::
        If,             // ??
        For,            // **
        Dollar,         // $
        GetBonus,       // $?
        Base,           // @
        Assign,         // = += -= *= /= %= |= ^=
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
        Insert,         // >>*
        Arrange,        // <>
        Less,           // <
        LessEquals,     // <=
        Not,            // !
        Add,            // +
        Sub,            // -
        Mul,            // *
        Div,            // /
        Percent,        // %
        ToExpr,         // %%
        Number,         // integer
        OpenPar,        // (
        ClosePar,       // )
        OpenSquig,      // {
        CloseSquig,     // }
        OpenSquare,     // [
        CloseSquare,    // ]
        Dot,            // .
        Comma,          // ,
        BeginString,    // "
        SingleQuote,    // '
        Semicolon,      // ;
        Expression,     // `
        Self,           // #
        Var,            //
        String,         // "abc"
        Event,          // 'abc'
        Dice,           // 1d20
        Separator,      // :
        HasFlag,        // :?
        Range,          // ..              
        Break,          // ^
        Return,         // ^^
        Random,         // .^
        Pair,           // ::
        Error,
        EOF,
    }
}
