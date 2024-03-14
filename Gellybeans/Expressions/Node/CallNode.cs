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

        public override dynamic Eval(int depth, IContext ctx, StringBuilder sb = null)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            var argResults = new dynamic[args.Count];

            for (int i = 0; i < args.Count; i++)
                argResults[i] = args[i].Eval(depth, ctx, sb);

            if (ctx.TryGetVar(varName, out var value))
            {
                if (value is FunctionValue f)
                {
                    var result = f.Invoke(depth, argResults, ctx, sb);
                    return result;
                }

            }
            return "???";
        }
    }
}
