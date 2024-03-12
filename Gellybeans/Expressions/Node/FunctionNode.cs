using System.Text;
using Gellybeans.Expressions;

namespace Gellybeans.Expressions
{
    public class FunctionNode : ExpressionNode
    {
        readonly string functionName;
        readonly ExpressionNode[] args;

        readonly static Random rand = new();

        public FunctionNode(string functionName, ExpressionNode[] args = null!)
        {
            this.functionName = functionName;
            this.args = args;
        }

        public override dynamic Eval(int depth, IContext ctx, StringBuilder sb)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";


            dynamic[] argValues = null!;
            if (args != null)
            {
                argValues = new dynamic[args.Length];
                for (int i = 0; i < args.Length; i++)
                {
                    argValues[i] = args[i].Eval(depth, ctx, sb);
                    if (argValues[i] is IReduce r)
                        argValues[i] = r.Reduce(depth, ctx, sb);
                }

            }

            return Call(functionName, argValues, ctx, sb);
        }

        public dynamic Call(string functionName, dynamic[] args = null!, IContext ctx = null!, StringBuilder sb = null!) => functionName switch
        {
            "abs" => Math.Abs(args[0]),
            "clamp" => Math.Clamp(args[0], args[1], args[2]),
            "if" => args[0] == 1 ? args[1] : "",
            "max" => Math.Max(args[0], args[1]),
            "min" => Math.Min(args[0], args[1]),
            "mod" => Math.Max(-5, args[0] >= 10 ? (args[0] - 10) / 2 : (args[0] - 11) / 2),
            "rand" => rand.Next(args[0], args[1] + 1),
            "bad" => args[0] / 3,
            "good" => 2 + args[0] / 2,
            "tq" => (args[0] + args[0] / 2) / 2,
            "oh" => args[0] / 2,
            "th" => args[0] + args[0] / 2,
            "upper" => args[0].ToString().ToUpper(),
            "lower" => args[0].ToString().ToLower(),
            "print" => Print(args[0], sb),
            "shuffle" => Shuffle(args[0]),
            _ => 0
        };

        static string Print(dynamic s, StringBuilder sb)
        {
            sb?.AppendLine(s.ToString());
            return "";
        }


        static ArrayValue Shuffle(ArrayValue array)
        {

            int n = array.Values.Length;
            dynamic[] a = new dynamic[n];
            array.Values.CopyTo(a, 0);
            while (n > 1)
            {
                int k = rand.Next(n--);
                (a[k], a[n]) = (a[n], a[k]);
            }
            return new ArrayValue(a);
        }


    }
}
