using System.Text;

namespace Gellybeans.Expressions
{
    public class FunctionNode : ExpressionNode
    {
        readonly string functionName;
        readonly ExpressionNode[] args;

        public FunctionNode(string functionName, ExpressionNode[] args = null!)
        {
            this.functionName = functionName;
            this.args = args;
        }

        public override dynamic Eval(IContext ctx, StringBuilder sb)
        {
            dynamic[] argValues = null!;
            if(args != null)
            {
                argValues = new dynamic[args.Length];
                for(int i = 0; i < args.Length; i++)
                    argValues[i] = args[i].Eval(ctx, sb);
            }
           
            return Call(functionName, argValues, ctx);
        }

        public dynamic Call(string functionName, dynamic[] args = null!, IContext ctx = null!) => functionName switch
        {
            "abs"       => Math.Abs(args[0]),
            "clamp"     => Math.Clamp(args[0], args[1], args[2]),
            "if"        => args[0] == 1 ? args[1] : 0,
            "max"       => Math.Max(args[0], args[1]),
            "min"       => Math.Min(args[0], args[1]),
            "mod"       => Math.Max(-5, args[0] >= 10 ? (args[0] - 10) / 2 : (args[0] - 11) / 2),
            "rand"      => new Random().Next(args[0], args[1] + 1),
            "bad"       => args[0] / 3,
            "good"      => 2 + (args[0] / 2),
            "tq"        => (args[0] + (args[0] / 2)) / 2,
            "oh"        => (args[0] / 2),
            "th"        => (args[0] + (args[0] / 2)),
            "listvar"   => ListVars(args == null ? "" : args[0].ToString(), ctx),
            _           => 0
        };


        string ListVars(string varName, IContext ctx)
        {
            
            var sb = new StringBuilder();

            if(varName != "%?")
            {
                varName = varName.ToUpper();
                if(ctx.Vars.TryGetValue(varName, out dynamic var))
                    sb.AppendLine($"|{varName,-15} |{var.GetType().Name.Replace("Value", ""),-15} |{var,-50}");
            }
            else
            {
                var ordered = ctx.Vars.OrderBy(x => x.GetType().Name);
                sb.AppendLine($"|{"VAR",-15} |{"TYPE",-15} |{"VALUE",-50}");
                foreach(var var in ordered)
                {
                    sb.AppendLine($"|{var.Key,-15} |{var.Value.GetType().Name.Replace("Value", ""),-15} |{var.Value,-50}");
                }

                using var stream = new MemoryStream(Encoding.Default.GetBytes(sb.ToString()));
                
            }
            return $"```{sb}```";
        }
    }
}
