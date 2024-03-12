using System.Text;

namespace Gellybeans.Expressions
{
    public interface IDisplay
    {
        string Display(IContext ctx, StringBuilder sb);
    }
}
