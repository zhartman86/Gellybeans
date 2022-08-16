using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gellybeans.Expressions
{
    public enum TokenType
    {
        Ternary,
        Greater,
        GreaterEquals,
        Less,
        LessEquals,
        Add,
        Sub,
        Mul,
        Div,
        Modulo,
        Equals,
        NotEquals,
        Number, //integer
        OpenPar,
        ClosePar,
        Comma,
        Var,
        Dice,
        EOF,       
    }
}
