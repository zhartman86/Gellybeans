using System.Text;

namespace Gellybeans.Expressions
{
    public interface IReduce
    {
        dynamic Reduce(int depth, IContext ctx, StringBuilder sb);
    }
}
