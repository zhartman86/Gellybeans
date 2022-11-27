using System.Text;

namespace Gellybeans.Expressions
{
    public class VarNode : ExpressionNode
    {
        readonly string varName;
        
        public string VarName { get { return varName; } }

        public VarNode(string varName) =>
            this.varName = varName;

        public override int Eval(IContext ctx, StringBuilder sb) 
        {                 
            if(ctx != null)
            {
                //if var starts with this symbol, treat it like an error.
                if(varName[0] == '%')
                {
                    sb.AppendLine(varName.Trim('%'));
                    return -99;
                }
                return ctx.Resolve(varName, sb);
            }           
            return 0;
        }
    }
}
