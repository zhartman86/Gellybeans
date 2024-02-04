using System.Text;

namespace Gellybeans.Expressions
{
    public class VarNode : ExpressionNode
    {
        readonly string varName;
        
        public string VarName { get { return varName; } }

        public VarNode(string varName) =>
            this.varName = varName;

        public override int Eval(IContext ctx = null, StringBuilder sb = null) 
        {                 
            int result = 0;

            if(ctx != null)
            {
                //if var starts with this symbol, treat it like an error.
                if(varName[0] == '%')
                {
                    sb?.AppendLine(varName.Trim('%'));
                    return -99;
                }
                result = ctx.Resolve(varName, sb);

                return result;
            }           
            return result;
        }
    }
}
