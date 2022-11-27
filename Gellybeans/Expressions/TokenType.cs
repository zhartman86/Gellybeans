namespace Gellybeans.Expressions
{
    public enum TokenType
    {
        None,
        Ternary,        // ?
        GetBon,         // $
        AssignBon,      //
        AssignAddBon,   // +$ 
        AssignSubBon,   // -$ 
        AssignMod,      // %=
        AssignDiv,      // /=
        AssignMul,      // *=
        AssignSub,      // -=
        AssignAdd,      // +=
        AssignEquals,   // =
        AssignExpr,     // +#
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
        Quotes,         // "
        Semicolon,      // ;
        Var,            //
        Dice,           // 1d20
        Separator,      // :
        Preset,         // ::
        EOF,
    }
}
