using System.Text;

namespace Gellybeans.Expressions
{
    public class AssignVarNode : ExpressionNode
    {
        readonly VarNode identifier;
        readonly ExpressionNode assignment;

        readonly Func<VarNode, ExpressionNode, ValueNode> op;

        public AssignVarNode(VarNode identifier, ExpressionNode assignment, Func<VarNode, ExpressionNode, ValueNode> op)
        {
            this.identifier = identifier;
            this.assignment = assignment;
            this.op = op;
        }

        public override ValueNode Eval()
        {
            Console.WriteLine("found assignment");
            return op(identifier, assignment);
        }

    }
}
