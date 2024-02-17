using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gellybeans.Expressions
{
    public class ExpressionListNode : ExpressionNode
    {        
        List<ExpressionNode> expressions = new List<ExpressionNode>();

        public ExpressionListNode(List<ExpressionNode> expressions) =>
            this.expressions = expressions;


        public override int Eval(IContext ctx = null, StringBuilder sb = null)
        {
            int result = 0;
            for (int i = 0; i < expressions.Count; i++)
                result += expressions[i].Eval(ctx, sb);                       
            return result;
        }
    }
}
