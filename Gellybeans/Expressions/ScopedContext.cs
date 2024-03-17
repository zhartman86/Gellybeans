using System.Text;

namespace Gellybeans.Expressions
{
    public class ScopedContext : IContext
    {
        readonly IContext parent;

        public Dictionary<string, dynamic> Vars { get; }

        public ScopedContext(IContext parent, Dictionary<string, dynamic> vars)
        {
            this.parent = parent;
            Vars = vars;
        }

        public ScopedContext(IContext parent, string varName, dynamic var)
        {
            this.parent = parent;
            Vars = new Dictionary<string, dynamic>() { { varName, var } };
        }

        public dynamic this[string varName]
        {
            get 
            {
                Console.WriteLine("CHECKING VARS");
                if(Vars.ContainsKey(varName))
                    return Vars[varName];
                if(parent != null)
                    if(parent.TryGetVar(varName, out var v))
                    {
                        Console.WriteLine("FOUND PARENT VAR");
                        return v;
                    }
                                 
                
                return null!;
            }
            set 
            {
                if(parent != null && parent.TryGetVar(varName, out var v))
                    parent[varName] = value;
                else
                    Vars[varName] = value;          
                    
            }
        }
        
        public bool TryGetVar(string varName, out dynamic value)
        {               
            if(Vars.ContainsKey(varName))
            {
                value = Vars[varName];
                return true;
            }
            else if(parent != null && parent.TryGetVar(varName, out var v))
            {
                value = v;
                return true;
            }

            value = null!;
            return false;
        }


        public dynamic GetVar(string varName, StringBuilder sb)
        {
            varName = varName.Replace(" ", "_").ToUpper();
            if(Vars.TryGetValue(varName, out var node))
                return node;

            sb?.AppendLine($"{varName} not found.");

            return null!;            
        }


        public bool RemoveVar(string varName) 
        {
            if(Vars.Remove(varName))
                return true;
          
            return false;
        }


    }
}
