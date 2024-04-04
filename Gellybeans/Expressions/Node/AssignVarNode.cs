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

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            //if(caller is not FunctionValue && caller is not TernaryNode)
            //    sb?.AppendLine($"{identifier} set");


            dynamic assign;
            if(assignment is StoredExpressionNode sen)
                assign = sen.Assign();
            else
                assign = assignment.Eval(depth: depth, caller: this, sb: sb, ctx: ctx);

            return op(identifier, assign);
        }
            

    }
}
