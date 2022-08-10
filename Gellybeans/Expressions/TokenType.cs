using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gellybeans.Expressions
{
    public enum TokenType
    {
        Add,
        Sub,
        Mul,
        Div,
        Number, //integer
        OpenPar,
        ClosePar,
        Comma,
        Var,
        Dice,
        EOF,
        
    }
}
