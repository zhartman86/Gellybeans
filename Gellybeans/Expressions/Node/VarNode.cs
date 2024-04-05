using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks.Sources;

namespace Gellybeans.Expressions
{
    public class VarNode : ExpressionNode, IReduce
    {
        readonly string varName;

        public string VarName { get { return varName; } }

        public VarNode(string varName)
        {
            this.varName = varName.ToUpper();
        }

        public dynamic Reduce(int depth, object caller, StringBuilder sb, IContext ctx)
        {
            if(ctx.TryGetVar(varName, out var value))
                return value;

            sb?.AppendLine($"{varName} not found.");
            return 0;
        }


        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            if(ctx.TryGetVar(varName, out var value))
            {
                if(value is ExpressionNode e)
                    value = e.Eval(depth, caller, sb, ctx);
                return value;
            }               
            else
            {
                sb?.AppendLine($"{varName} not found.");
                return 0;
            }
        }

        public override string ToString() =>
            varName;
        
    }
}
