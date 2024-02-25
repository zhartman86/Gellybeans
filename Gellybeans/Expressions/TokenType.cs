namespace Gellybeans.Expressions
{
    public enum TokenType
    {
        None,
        Ternary,        // ?::
        GetBonus,       // $?        
        Assign,         // = += -= *= /= %= |= +$ -$ 
        AssignBon,      // $ +$ -$ 
        AssignExpr,     // used by the Parser
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
        DoubleQuote,    // "
        SingleQuote,    // '
        Semicolon,      // ;
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
