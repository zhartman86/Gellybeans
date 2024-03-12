using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gellybeans.Expressions;

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
            if (ctx.TryGetVar(v, out var var))
            {
                if (var is ArrayValue a)
                    return a[result];
            }
            sb?.Append($"{v} not found");
            return result;
        }
    }
}
