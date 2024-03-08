using System.Text;

namespace Gellybeans.Expressions
{
    /// <summary>
    /// This interface is passed through every expression node. It's primary use is providing context for assigned variables. 
    /// </summary>
    
    public interface IContext
    {
        dynamic GetVar(string varName, StringBuilder sb);
       
        Dictionary<string, dynamic> Vars { get; }
        Dictionary<string, dynamic> Constants { get; }
        
        bool RemoveVar(string varName);

        //use the indexer to retrieve and set values. this will check for constants
        dynamic this[string varName] { get; set; }
    }
}
