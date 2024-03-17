using System.Text;

namespace Gellybeans.Expressions
{
    public interface IReduce
    {
        dynamic Reduce(int depth, object caller, StringBuilder sb, IContext ctx = null!);
    }
}
