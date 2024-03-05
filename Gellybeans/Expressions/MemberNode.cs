using System.Text;

namespace Gellybeans.Expressions
{
    public class MemberNode : ExpressionNode
    {


        public MemberNode(string identifier, ExpressionNode member, IContext ctx, StringBuilder sb = null!)
        {
        }

        public override dynamic Eval(IContext ctx, StringBuilder sb)
        {
            return 0;
        }
    }
}
