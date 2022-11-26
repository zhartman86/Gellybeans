using System.Text;

namespace Gellybeans.Expressions
{
    /// <summary>
    /// This interface is the optional context that can be provided to an evaluation, allowing for custom variables. 
    /// </summary>
    
    public interface IContext
    {
        int Resolve     (string identifier, StringBuilder sb);
        int Assign      (string identifier, string assignment, TokenType assignType, StringBuilder sb);
        int Bonus       (string identifier, string bonusName, int type, int value, TokenType assignType, StringBuilder sb);
    }
}
