﻿using System.Text;

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
            var result = 0;
            if(ctx != null)
                result = ctx.Resolve(varName, sb);
            return result;
        }
    }
}
