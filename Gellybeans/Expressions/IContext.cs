using System.Text;

namespace Gellybeans.Expressions
{
    /// <summary>
    /// This interface is passed through every expression node. It's primary use is providing context for assigned variables. 
    /// </summary>
    
    public interface IContext
    {
        ExpressionNode GetVar(string varName, StringBuilder sb);

        abstract Dictionary<string, ValueNode> Vars { get; }
    }
}
