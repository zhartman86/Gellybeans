using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gellybeans.Expressions
{
    public class KeyNode : ExpressionNode
    {
        public string VarName { get; }
        public ExpressionNode Key { get; }

        public KeyNode(string VarName, ExpressionNode Key)
        {
            this.VarName = VarName;
            this.Key = Key;
        }

        public override dynamic Eval(IContext ctx = null, StringBuilder sb = null)
        {
            var v = VarName.ToUpper();
            var result = Key.Eval(ctx, sb);         
            if(ctx.Vars.TryGetValue(v, out var var))
            {
                if(var is ArrayValue a)
                    return a[result];
            }
            return $"{v} not found";
        }
    }
}
