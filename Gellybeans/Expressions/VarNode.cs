using System.Text;
using System.Threading.Tasks.Sources;

namespace Gellybeans.Expressions
{
    public class VarNode : ExpressionNode
    {
        readonly string varName;

        public string VarName { get { return varName; } }

        public VarNode(string varName)
        {
            this.varName = varName;
        }

        public override dynamic Eval(IContext ctx, StringBuilder sb) 
        {
            var variable = varName.Replace(" ", "_").ToUpper();
            dynamic value = "";
            if(ctx.Vars.TryGetValue(variable, out var node))
            {
                if(node is IEval e)
                {
                    Console.WriteLine("found eval");
                    value = e.Eval(ctx, sb);
                }
            }
            else
                sb?.AppendLine($"{variable} not found.");

            
            return value;
        }
    }
}
