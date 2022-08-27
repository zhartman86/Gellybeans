using System.Text;

namespace Gellybeans.Expressions
{
    public interface IContext
    {
        int Resolve     (string identifier, StringBuilder sb);
        int Call        (string methodName, int[] args);
        int Assign      (string identifier, int assignment, TokenType assignType, StringBuilder sb);
        int Bonus       (string identifier, string bonusName, int type, int value, TokenType assignType, StringBuilder sb);
    }
}
