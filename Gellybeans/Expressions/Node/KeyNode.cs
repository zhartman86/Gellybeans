using System;
using System.Collections.Generic;
using System.Text;

namespace Gellybeans.Expressions
{
    public class KeyNode : ExpressionNode
    {
        
        public ExpressionNode Key { get; }
        public ExpressionNode Value { get; }

        Func<dynamic, dynamic, dynamic> op;

        public KeyNode(ExpressionNode key, ExpressionNode value, Func<dynamic, dynamic, dynamic> op)
        {         
            Key = key;
            Value = value;
            this.op = op;
        }

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            var k = Key.Eval(depth, caller, sb, ctx);     
            var v = Value.Eval(depth, caller, sb, ctx);
            return op(k, v);
        }
              
    }    
}
