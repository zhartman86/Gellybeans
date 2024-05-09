using System.Text;

namespace Gellybeans.Expressions
{
    public class ScopedContext : IContext
    {
        readonly IContext parent;
        readonly bool localize;

        public IContext Parent { get { return parent; } }

        public Dictionary<string, dynamic> Vars { get; }

        public ScopedContext(IContext parent, Dictionary<string, dynamic> vars, bool localize = false)
        {
            this.localize = localize;
            this.parent = parent;
            Vars = vars;
        }

        public ScopedContext(IContext parent, string varName, dynamic var, bool localize = false)
        {
            this.localize = localize;
            this.parent = parent;
            Vars = new Dictionary<string, dynamic>() { { varName, var } };
        }

        public dynamic this[string varName]
        {
            get 
            {
                if(TryGetVar(varName, out var value))
                    return value;
                if(parent != null)
                    if(parent.TryGetVar(varName, out var v))
                    {
                        return v;
                    }
                                 
                
                return null!;
            }
            set 
            {
                if(!localize && parent != null && parent.TryGetVar(varName, out var v))
                {
                    parent[varName] = value;
                }                  
                else
                {
                    Vars[varName] = value;
                }
                          
                    
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

        public bool TryElevateVar(string identifier, dynamic value)
        {
            if(parent != null)
            {
                IContext ctx = parent;
                bool found = false;
                while(true)
                {
                    if(ctx.TryGetVar(identifier, out var v))
                    {
                        parent[identifier] = value;
                        found = true;
                        break;
                    }
                        
                    else if(ctx.Parent != null)
                        ctx = ctx.Parent;
                    else
                        break;
                }
                if(!found)
                    parent[identifier] = value;
                return true;
            }
            return false;
        }



    }
}
