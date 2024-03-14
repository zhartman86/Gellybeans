using System.Text;

namespace Gellybeans.Expressions
{
    public class FunctionValue
    {
        public string[] VarNames {  get; set; }
        public List<Token> Tokens { get; set; }

        public FunctionValue(string[] varNames, List<Token> tokens)
        {
            VarNames = varNames;
            Tokens = tokens;
        }


        public dynamic Invoke(int depth, dynamic[] args, IContext ctx, StringBuilder sb)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            
            if (args.Length != VarNames.Length)
                return "Arguments don't match parameter count for this function.";

            var dict = new Dictionary<string, dynamic>();
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"Var: {VarNames[i]}, Value: {args[i]}");
                dict.Add(VarNames[i].ToUpper(), args[i]);
            }

            Console.WriteLine($"Invoking function: {Tokenizer.Output(Tokens)}");
            var scope = new ScopedContext(ctx, dict);
            var result = Parser.Parse(Tokens, scope).Eval(depth, scope);
            return result;
        }

        public override string ToString() =>
            $"**Function**\nParameter Count: {VarNames.Length}\n\n{Tokenizer.Output(Tokens)}";
       
    }
}
