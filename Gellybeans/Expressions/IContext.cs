using System.Text;

namespace Gellybeans.Expressions
{
    /// <summary>
    /// This interface is passed through every expression node. It's primary use is providing context for assigned variables. 
    /// </summary>
    
    public interface IContext
    {
        IContext Parent { get; }
        bool TryGetVar(string varName, out dynamic value);
        bool RemoveVar(string varName);
        bool TryElevateVar(string varName, dynamic assignment);
        Dictionary<string, dynamic> Vars { get; }
        dynamic this[string varName] { get; set; }
    }
}
