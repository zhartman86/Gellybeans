using System.Text;

namespace Gellybeans.Expressions
{
    public class FunctionValue
    {
        public int ParamCount { get; set; }
        public string Body { get; set; }

        public FunctionValue(int paramCount, string body)
        {
            ParamCount = paramCount;
            Body = body;
        }

        public override string ToString() =>
            $"```{Body}```\n\nParamCount:**{ParamCount}**";





        public dynamic Invoke(int depth, string[] args, IContext ctx, StringBuilder sb)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            Console.WriteLine("Invoking function");
            if (args.Length != ParamCount)
                return "Arguments don't match parameter count for this function.";

            string s = Body;
            for (int i = 0; i < args.Length; i++)
            {
                s = s.Replace($"«{i}»", args[i]);
            }
            var scope = new ScopedContext(ctx, new());
            Console.WriteLine($"FUNCTION:\n{s}");
            var result = Parser.Parse(s, scope).Eval(depth, scope);
            Console.WriteLine("Function Parsed");
            return result;
        }
    }
}
