﻿using System.Text;

namespace Gellybeans.Expressions
{
    public class CallExpressionNode : ExpressionNode
    {
        ExpressionValue Value { get; set; }

        public CallExpressionNode(ExpressionValue value) =>
            Value = value;

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            return Value.Reduce(depth, caller, sb, ctx);
        }
    }
}
