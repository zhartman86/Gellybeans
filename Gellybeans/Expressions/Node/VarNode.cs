using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks.Sources;

namespace Gellybeans.Expressions
{
    public class VarNode : ExpressionNode
    {
        readonly string varName;

        public string VarName { get { return varName; } }

        public VarNode(string varName)
        {
            this.varName = varName;
        }



        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            var v = varName.Replace(" ", "_").ToUpper();
            dynamic value = ctx[v];
            if (value is IReduce r)
                value = r.Reduce(depth: depth, caller: this, sb: sb, ctx : ctx);
            if (value is null)
            {
                value = 0;
                sb?.AppendLine($"{v} not found.");
            }
            return value;
        }

        public override string ToString() =>
            varName;
        
    }
}
