using System.Text;

namespace Gellybeans.Expressions
{
    public interface IReduce
    {
        dynamic Reduce(IContext ctx, StringBuilder sb);
    }
}
