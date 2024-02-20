namespace Gellybeans.Expressions
{
    public enum TokenType
    {
        None,
        Ternary,        // ?::
        Bonus,          // $        
        Assign,         // =
        AssignBon,      //
        AssignAddBon,   // +$ 
        AssignSubBon,   // -$ 
        AssignMod,      // %=
        AssignDiv,      // /=
        AssignMul,      // *=
        AssignSub,      // -=
        AssignAdd,      // +=       
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
        Quotes,         // " or '
        Semicolon,      // ;
        Var,            //
        String,         // "abc" or 'abc'
        Dice,           // 1d20
        Separator,      // :
        HasFlag,        // :?
        Macro,          // [abc;abc] + x
        Error,
        EOF,
    }
}
