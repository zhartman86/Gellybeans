using System.Text;

namespace Gellybeans.Expressions
{
    public class ErrorNode : ExpressionNode
    {
        public string Message { get; }

        public ErrorNode(string message)
        {
            Message = message;
        }

        public override dynamic Eval(int depth, IContext ctx = null, StringBuilder sb = null) =>
            Message;
    }
}
