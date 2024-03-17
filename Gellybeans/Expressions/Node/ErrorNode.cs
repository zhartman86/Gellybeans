﻿using System.Text;

namespace Gellybeans.Expressions
{
    public class ErrorNode : ExpressionNode
    {
        public string Message { get; }

        public ErrorNode(string message)
        {
            Message = message;
        }

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            return Message;
        }
           
    }
}
