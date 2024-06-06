using System.Text;

namespace Gellybeans.Expressions
{
    public abstract class ExpressionNode
    {
        public abstract dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!);
    }
}


