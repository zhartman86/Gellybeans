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

        public override dynamic Eval(IContext ctx, StringBuilder sb) 
        {
            var variable = varName.Replace(" ", "_").ToUpper();
            dynamic value = ctx[variable];
            if(value is null)
            {
                value = 0;
                sb?.AppendLine($"{variable} not found.");
            }
            return value;
        }
    }
}
