namespace Gellybeans.Expressions
{
    public enum TokenType
    {
        None,
        Ternary,        // ?::
        Bonus,          // $
        GetBonus,       // $?
        Assign,         // = += -= *= /= %= |=
        AssignExpr,     // ::
        AssignAddExpr,  // ** 
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
        BeginMacro,     // [
        EndMacro,       // ]
        Error,
        EOF,
    }
}
