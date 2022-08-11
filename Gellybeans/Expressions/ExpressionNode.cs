using System.Text;

namespace Gellybeans.Expressions
{
    public abstract class ExpressionNode { public abstract int Eval(IContext ctx, StringBuilder sb); }    
}


