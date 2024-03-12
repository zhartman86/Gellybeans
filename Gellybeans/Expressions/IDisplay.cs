using System.Text;

namespace Gellybeans.Expressions
{
    public interface IDisplay
    {
        string Display(int depth, IContext ctx, StringBuilder sb);
    }
}
