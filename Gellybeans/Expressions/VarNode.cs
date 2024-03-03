using System.Text;
using System.Text.RegularExpressions;

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

        public override ValueNode Eval(IContext ctx, StringBuilder sb) 
        {
            var node = ctx.GetVar(varName, sb);
            var value = node.Eval(ctx, sb);
            return value;
        }
    }
}
