using Gellybeans.Pathfinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gellybeans.Expressions.Node
{
    public class AssignNode : ExpressionNode
    {
        readonly string symbol;
        readonly string identifier;
        readonly ExpressionNode value;
        readonly ExpressionNode assignment;

        public AssignNode(string symbol, string identifier, ExpressionNode value, ExpressionNode assignment)
        {
            this.symbol = symbol;
            this.identifier = identifier;
            this.value = value;
            this.assignment = assignment;            
        }

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx)
        {
            ctx.TryGetVar(identifier, out var variable);          
            var val = value.Eval(depth, caller, sb, ctx);
            var assign = assignment.Eval(depth, caller, sb, ctx);          
            
            if(symbol == "=")
            {                           
                if(assign is int i)
                {
                    if(variable is Stat s && s.Bonuses != null)
                    {
                        s.Base = i;
                        val = s;
                    }
                    else
                        val = new Stat(i);
                }
                else
                    val = assign;
            }
            
            
            if(variable == null)
                return new StringValue($"{identifier} not found.");

            switch(symbol)
            {
                case "+=":
                    val += assign;                  
                    break;
                case "-=":
                    val -= assign; 
                    break;
                case "*=":
                    val *= assign;
                    break;
                case "/=":
                    val /= assign;
                    break;
                case "%=":
                    val %= assign;
                    break;
            }

            if(value is KeyNode k)
            {
                dynamic v;
                while(true)
                {
                    v = variable[k.Eval(depth, caller, sb, ctx)];
                    if(k.Value is KeyNode kk)
                        k = kk;
                    else
                    {
                        v[k.Eval(depth, caller, sb, ctx)] = val;
                        break;
                    }                      
                }
            }
            else
                variable = val;

            ctx[identifier] = variable;
            sb?.AppendLine($"{identifier} set.");
            return assignment;
        }

    }
}
