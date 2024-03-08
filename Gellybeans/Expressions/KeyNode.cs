using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gellybeans.Expressions
{
    public class KeyNode : ExpressionNode
    {
        readonly string varName;
        readonly ExpressionNode key;

        public KeyNode(string varName, ExpressionNode key)
        {
            this.varName = varName;
            this.key = key;
        }

        public override dynamic Eval(IContext ctx = null, StringBuilder sb = null)
        {
            var result = key.Eval(ctx, sb);         
            if(ctx.Vars.TryGetValue(varName, out var var))
            {
                if(var is ArrayValue a)
                    return a[result];
            }
            return $"{varName} not found";
        }
    }
}
