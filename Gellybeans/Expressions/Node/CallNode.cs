using System.Text;

namespace Gellybeans.Expressions
{
    class CallNode : ExpressionNode
    {
        string varName;
        List<ExpressionNode> args;

        public CallNode(string varName, List<ExpressionNode> args)
        {
            this.varName = varName;
            this.args = args;
        }

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            dynamic[] argResults;
            if(args != null)
            {
                argResults = new dynamic[args.Count];

                for(int i = 0; i < args.Count; i++)
                    argResults[i] = args[i].Eval(depth: depth, caller: this, sb: sb, ctx : ctx);
            }
            else
                argResults = Array.Empty<dynamic>();

            if (ctx.TryGetVar(varName, out var value))
            {
                if (value is FunctionValue f)
                {
                    var result = f.Invoke(depth, argResults, sb, ctx);
                    return result;
                }

            }
            return "???";
        }
    }
}
