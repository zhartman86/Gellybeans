using System.Text;

namespace Gellybeans.Expressions
{
    public class FunctionValue
    {
        public string[] VarNames {  get; set; }
        public string Expression { get; set; }

        public FunctionValue(string[] varNames, string expression)
        {
            VarNames = varNames;
            Expression = expression;
        }

        public dynamic Invoke(int depth, dynamic[] args, StringBuilder sb, IContext ctx)
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

            var scope = new ScopedContext(ctx, dict);
            var result = Parser.Parse(Expression, this, sb, scope).Eval(depth: depth, caller: this, sb: sb, ctx : scope);
            if(result is IReduce r)
                result = r.Reduce(depth: depth, caller: this, sb: sb, ctx : scope);

            return result;
        }

        public override string ToString() =>
            $"### **Function**\n>>> ### ({GetParamNames()})\n `{Expression}`";
        
        public string GetParamNames()
        {
            string s = "";
            for(int i = 0; i < VarNames.Length; i++)
                s += $"{VarNames[i]},";
            return s.Trim(',');
        }
    }
}
