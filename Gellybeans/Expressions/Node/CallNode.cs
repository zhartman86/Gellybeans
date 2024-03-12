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

        public override dynamic Eval(IContext ctx, StringBuilder sb = null)
        {
            var argResults = new string[args.Count];

            for (int i = 0; i < args.Count; i++)
                argResults[i] = args[i] is VarNode v ? v.VarName : args[i].Eval(ctx, sb).ToString();


            if (ctx.TryGetVar(varName, out var value))
            {
                if (value is FunctionValue f)
                {
                    var result = f.Invoke(argResults, ctx, sb);
                    return result;
                }

            }
            return "???";
        }
    }
}
