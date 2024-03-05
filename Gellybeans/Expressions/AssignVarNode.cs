using System.Text;

namespace Gellybeans.Expressions
{
    public class AssignVarNode : ExpressionNode
    {
        readonly string identifier;
        readonly ExpressionNode assignment;

        readonly Func<string, dynamic, dynamic> op;

        public AssignVarNode(string identifier, dynamic assignment, Func<string, dynamic, dynamic> op)
        {
            this.identifier = identifier;
            this.assignment = assignment;
            this.op = op;
        }

        public override dynamic Eval(IContext ctx, StringBuilder sb)
        {
            return op(identifier, assignment.Eval(ctx, sb));
        }

    }
}
