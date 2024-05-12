using System.Text;


namespace Gellybeans.Expressions
{
    public class IfNode : ExpressionNode
    {
        readonly ExpressionNode condition;
        readonly List<Token> statement;
        readonly List<Token> elseStatement;

        readonly List<ConditionalNode> conditionals;

        public IfNode(ExpressionNode condition, List<Token> statement, List<Token> elseStatement = null!)
        {
            this.condition = condition;
            this.statement = statement;
            this.elseStatement = elseStatement;
        }
        
        public IfNode(List<ConditionalNode> conditionals)
        {
            this.conditionals = conditionals;
        }

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";           

            for(int i = 0; i < conditionals.Count; i++)
            {
                var result = conditionals[i].Eval(depth, caller, sb, ctx);
                if(result) 
                    break;
            }

            return 0;
        }

    }
}
