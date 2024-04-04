using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gellybeans.Expressions
{
    public class ShellNode : ExpressionNode
    {
        readonly ExpressionNode expression;

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null)
        {
            throw new NotImplementedException();
        }
    }
}
