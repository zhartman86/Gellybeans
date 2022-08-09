namespace Gellybeans.Expressions
{
    public abstract class ExpressionNode { public abstract int Eval(); }


    public class NumberNode : ExpressionNode
    {
        public int Number { get; init; }
                
        public NumberNode(int number) => Number = number; 
        
        public override int Eval() { return Number; }
    }

    public class UnaryNode : ExpressionNode
    {
        ExpressionNode rhs;
        Func<int, int> op;

        public UnaryNode(ExpressionNode rhs, Func<int, int> op)
        {
            this.rhs = rhs;
            this.op = op;
        }
    
        public override int Eval() 
        {
            var value = rhs.Eval();
            return op(value);
        }
    }
    
    public class BinaryNode : ExpressionNode
    {
        ExpressionNode lhs;
        ExpressionNode rhs;
        Func<int, int, int> op;

        public BinaryNode(ExpressionNode lhs, ExpressionNode rhs, Func<int, int, int> op)
        {
            this.lhs = lhs;
            this.rhs = rhs;
            this.op = op;
        }

        public override int Eval()
        {
            var lhValue = lhs.Eval();
            var rhValue = rhs.Eval();

            return op(lhValue, rhValue);
        }
    }
}


