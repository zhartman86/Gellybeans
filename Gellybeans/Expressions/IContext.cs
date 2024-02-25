using System.Text;

namespace Gellybeans.Expressions
{
    /// <summary>
    /// This interface is passed through every expression node. It's primary use is providing context for assigned variables. 
    /// </summary>
    
    public interface IContext
    {
        ExpressionNode Resolve(string varName, StringBuilder sb);
        int AssignValue         (string identifier, ExpressionNode node, string assignType, StringBuilder sb);
        int Bonus               (string identifier, string bonusName, int type, int value, string assignType, StringBuilder sb);
        string? GetValue        (string identifier);
    }
}
