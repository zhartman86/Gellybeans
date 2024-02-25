using System.Text;

namespace Gellybeans.Expressions
{
    public class NumberNode : ExpressionNode
    {
        int number;

        public NumberNode(int number) =>
            this.number = number;

        public override int Eval()
        {
            return number;
        }
            
            
    }
}
