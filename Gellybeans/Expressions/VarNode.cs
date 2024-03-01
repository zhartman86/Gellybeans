using System.Text;
using System.Text.RegularExpressions;

namespace Gellybeans.Expressions
{
    public class VarNode : ExpressionNode
    {
        readonly string varName;
        StringBuilder sb;
        IContext ctx;



        public string VarName { get { return varName; } }

        public VarNode(string varName, IContext ctx, StringBuilder sb = null!)
        {
            this.varName = varName;
            this.ctx = ctx;
            this.sb = sb;
        }

        public override ValueNode Eval() 
        {
            var node = ctx.GetVar(varName, sb); 
            var value = node.Eval();
            return value;
        }
    }
}
