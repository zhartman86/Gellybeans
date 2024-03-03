using System.Text;

namespace Gellybeans.Expressions
{
    public class AssignVarNode : ExpressionNode
    {
        readonly VarNode identifier;
        readonly ExpressionNode assignment;

        readonly Func<VarNode, ValueNode, ValueNode> op;

        public AssignVarNode(VarNode identifier, ExpressionNode assignment, Func<VarNode, ValueNode, ValueNode> op)
        {
            this.identifier = identifier;
            this.assignment = assignment;
            this.op = op;
        }

        public override ValueNode Eval(IContext ctx, StringBuilder sb)
        {
            return op(identifier, assignment.Eval(ctx, sb));
        }

    }
}
