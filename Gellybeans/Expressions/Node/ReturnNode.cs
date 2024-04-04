using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gellybeans.Expressions
{   
    public class ReturnNode : ExpressionNode
    {
        ExpressionNode value;
        
        public ReturnNode(ExpressionNode value) =>
            this.value = value;

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            return value.Eval(depth, caller, sb, ctx);
        }

    }
}
