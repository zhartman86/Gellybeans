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

        public override dynamic Eval(IContext ctx, StringBuilder sb)
        {        
            var argValues = new ValueNode[args.Length];
            for(int i = 0; i < args.Length; i++)
                argValues[i] = args[i].Eval(ctx, sb);

            for(int i = 0; i < argValues.Length; i++)
            {
                Console.WriteLine($"{argValues.GetType()} : {argValues[i].Value}");
            }


            return Call(functionName, argValues);
        }

        public ValueNode Call(string functionName, ValueNode[] args) => functionName switch
        {
            "abs"   => Math.Abs(int.Parse(args[0])),
            "clamp" => Math.Clamp(args[0], args[1], args[2]),
            "if"    => args[0] == 1 ? args[1] : 0,
            "max"   => Math.Max(args[0], args[1]),
            "min"   => Math.Min(args[0], args[1]),
            "mod"   => Math.Max(-5, args[0] >= 10 ? (args[0] - 10) / 2 : (args[0] - 11) / 2),
            "rand"  => new Random().Next(args[0], args[1] + 1),
            "bad"   => args[0] / 3,
            "good"  => 2 + (args[0] / 2),
            "tq"    => (args[0] + (args[0] / 2)) / 2,
            "oh"    => (args[0] / 2),
            "th"    => (args[0] + (args[0] / 2)),
            _       => 0
        };

    }
}
