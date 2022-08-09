namespace Gellybeans.Expressions
{
    public abstract class ExpressionNode { public abstract int Eval(IContext ctx); }


    public class NumberNode : ExpressionNode
    {
        int number;

        public NumberNode(int number)
        {
            this.number = number;
        }
        
        public override int Eval(IContext ctx) { return number; }
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
    
        public override int Eval(IContext ctx) 
        {
            var rhValue = rhs.Eval(ctx);
            var result = op(rhValue);
            return result;
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

        public override int Eval(IContext ctx)
        {
            var lhValue = lhs.Eval(ctx);
            var rhValue = rhs.Eval(ctx);

            var result = op(lhValue, rhValue);
            return result;
        }
    }

    public class VarNode : ExpressionNode
    {
        string varName;

        public VarNode(string varName)
        {
            this.varName = varName;
        }

        public override int Eval(IContext ctx) { return ctx.Resolve(varName); }
    }

    public class FunctionNode : ExpressionNode
    {
        string              functionName;
        ExpressionNode[]    args;

        public FunctionNode(string functionName, ExpressionNode[] args)
        {
            this.functionName = functionName;
            this.args = args;
        }

        public override int Eval(IContext ctx)
        {
            var argValues = new int[args.Length];
            for(int i = 0; i < args.Length; i++)
            {
                argValues[i] = args[i].Eval(ctx);
            }

            return ctx.Call(functionName, argValues);
        }
    }
}


