using System.Text;

namespace Gellybeans.Expressions
{
    public class FunctionNode : ExpressionNode
    {
        string functionName;
        ExpressionNode[] args;

        public FunctionNode(string functionName, ExpressionNode[] args)
        {
            this.functionName = functionName;
            this.args = args;
        }

        public override int Eval(IContext ctx, StringBuilder sb)
        {
            var argValues = new int[args.Length];
            for(int i = 0; i < args.Length; i++)
            {
                argValues[i] = args[i].Eval(ctx, sb);
            }

            var result = ctx.Call(functionName, argValues);

            return result;
        }
    }
}
