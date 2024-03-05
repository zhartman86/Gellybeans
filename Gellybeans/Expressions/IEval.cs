using System.Text;

namespace Gellybeans.Expressions
{
    public interface IEval
    {
        dynamic Eval(IContext ctx, StringBuilder sb);
    }
}
