using System.Text;


namespace Gellybeans.Expressions
{
    public class IfNode : ExpressionNode
    {
        readonly ExpressionNode condition;
        readonly List<Token> statement;
        readonly List<Token> elseStatement;

        public IfNode(ExpressionNode condition, List<Token> statement, List<Token> elseStatement = null!)
        {
            this.condition = condition;
            this.statement = statement;
            this.elseStatement = elseStatement;
        }

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";           

            var conValue = condition.Eval(depth: depth, caller: caller, sb: sb, ctx: ctx);
            if(conValue is IReduce r)
                conValue = r.Reduce(depth: depth, caller: caller, sb: sb, ctx: ctx);

            if(conValue)
            {
                var scope = new ScopedContext(ctx, new Dictionary<string, dynamic>());
                var result = Parser.Parse(statement, caller, sb, scope)
                    .Eval(depth, caller, sb, scope);
            }
            else if(elseStatement != null)
            {
                var scope = new ScopedContext(ctx, new Dictionary<string, dynamic>());
                var result = Parser.Parse(elseStatement, caller, sb, scope)
                    .Eval(depth, caller, sb, scope);
            }

            return 0;
        }

    }
}
