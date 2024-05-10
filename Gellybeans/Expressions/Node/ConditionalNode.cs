using System.Text;

namespace Gellybeans.Expressions
{
    public class ConditionalNode : ExpressionNode
    {
        ExpressionNode condition;
        List<Token> statement;

        public ConditionalNode(ExpressionNode condition, List<Token> statement)
        {
            this.condition = condition;
            this.statement = statement;
        }

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null)
        {
            var conValue = condition == null ? true : condition.Eval(depth, caller, sb, ctx);

            if(conValue)
            {
                var scope = new ScopedContext(ctx, new Dictionary<string, dynamic>());
                Parser.Parse(statement, caller, sb, scope)
                    .Eval(depth, caller, sb, scope);

                return true;
            }
            return false;
        }
    }
}
