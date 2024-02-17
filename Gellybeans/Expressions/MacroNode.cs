using System.Text;

namespace Gellybeans.Expressions
{
    public class MacroNode : ExpressionNode
    {
        readonly string expression;
        readonly string modifier;

        public MacroNode (string expression, string modifier)
        {
            this.expression = expression;
            this.modifier = modifier;
        }
            

        public override int Eval(IContext ctx, StringBuilder sb)
        {
            Console.WriteLine("Evaluating macro...");
            var expressions = expression.Split(';');
            for(int i = 0; i < expressions.Length; i++)
            {
                var result = Parser.Parse(expressions[i]+modifier).Eval(ctx, sb);

            }
            
            //ctx?.ResolveMacro(varName, modifier, sb);
            
            return 0;
        }
    }
}
