using System.Text;

namespace Gellybeans.Expressions
{
    public class VarNode : ExpressionNode
    {
        string varName;

        public VarNode(string varName)
        {
            this.varName = varName;
        }

        public override int Eval(IContext ctx, StringBuilder sb) 
        { 
           
            var result = ctx.Resolve(varName, sb);

            return result;
        }
    }
}
