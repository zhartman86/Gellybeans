using System.Text;

namespace Gellybeans.Expressions
{
    class CallFunctionNode : ExpressionNode
    {
        ExpressionNode function;
        List<ExpressionNode> args;

        public CallFunctionNode(ExpressionNode function, List<ExpressionNode> args)
        {
            this.function = function;
            this.args = args;
        }

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            var func = function.Eval(depth, caller, sb, ctx);
            if(func is KeyValuePairValue kvp)
                func = kvp.Value;
            
            if(func is not FunctionValue f)
                return "%";

            dynamic[] argResults;
            if(args != null)
            {                
                argResults = new dynamic[args.Count];
                for(int i = 0; i < args.Count; i++)
                {
                    if(args[i] is VarNode v)
                        argResults[i] = v.Reduce(depth, caller, sb, ctx);
                    else
                        argResults[i] = args[i].Eval(depth, caller, sb, ctx);
                }                   
            }
            else
                argResults = Array.Empty<dynamic>();


            var result = f.Invoke(depth, caller, argResults, sb, ctx);
            return result;
        }
    }
}
