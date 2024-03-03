using System.Text;

namespace Gellybeans.Expressions
{
    public class StringNode : ExpressionNode
    {

        public string String { get; }

        public StringNode(string str, IContext ctx, StringBuilder sb)
        {
            String = str;
        }
                       

        public override ValueNode Eval(IContext ctx, StringBuilder sb) =>
            new StringValue(String);

        public override string ToString()
        {
            return String;
        }


    }


}
