using System.Text;

namespace Gellybeans.Expressions
{
    public class StringNode : ExpressionNode
    {

        public string String { get; }

        public StringNode(string str)
        {
            String = str;
        }


        public override dynamic Eval(int depth, IContext ctx, StringBuilder sb)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";
            
            return new StringValue(String);
        }
           

        public override string ToString()
        {
            return String;
        }


    }


}
