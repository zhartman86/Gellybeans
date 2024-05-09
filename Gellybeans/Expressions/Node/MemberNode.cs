using System.Text;

namespace Gellybeans.Expressions
{
    public class MemberNode : ExpressionNode
    {
        readonly ExpressionNode parent;
        readonly string member;
        readonly ExpressionNode[] args;
        

        public MemberNode(ExpressionNode parent, string member, ExpressionNode[] args = null!)
        {
            this.parent = parent;
            this.member = member.ToUpper();
            this.args = args;
        }       
        
        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null)
        {
            var value = parent.Eval(depth, caller, sb, ctx);

            dynamic[] values;
            if(args == null)
            {
                values = Array.Empty<dynamic>();
            }
            else
            {
                values = new dynamic[args.Length];
                for(int i = 0; i < args.Length; i++)
                {
                    values[i] = args[i].Eval(depth, caller, sb, ctx);
                }
            }

            if(value is string s)
                value = new StringValue(s);

            if(value is IMember m && m.TryGetMember(member, out var outVal, values))
                return outVal;


            else return new StringValue("%");
        }
    }
}
