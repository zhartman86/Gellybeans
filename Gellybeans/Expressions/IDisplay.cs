using System.Text;

namespace Gellybeans.Expressions
{
    public interface IDisplay
    {
        string Display(int depth, object caller, StringBuilder sb, IContext ctx);
    }
}
