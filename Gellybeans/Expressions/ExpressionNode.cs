using System.Text;

namespace Gellybeans.Expressions
{
    public abstract class ExpressionNode 
    {
        public string Comment { get; set; } = "";
        public abstract int Eval(IContext ctx, StringBuilder sb); 
    }    
}


