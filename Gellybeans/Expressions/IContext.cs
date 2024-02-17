using System.Text;

namespace Gellybeans.Expressions
{
    /// <summary>
    /// This interface is passed through every expression node. It's primary use is providing context for assigned variables. 
    /// </summary>
    
    public interface IContext
    {
        int Resolve      (string identifier, StringBuilder sb);
        int ResolveMacro (string identifier, string modifier, StringBuilder sb);
        int Assign       (string identifier, string assignment, TokenType assignType, StringBuilder sb);
        int Bonus        (string identifier, string bonusName, int type, int value, TokenType assignType, StringBuilder sb);
    }
}
