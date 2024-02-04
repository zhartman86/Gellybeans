using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using System.Text;

namespace Gellybeans.Expressions
{
    public class BinaryNode : ExpressionNode
    {
        ExpressionNode lhs;
        ExpressionNode rhs;

        Func<int, int, int> op;

        TokenType? tokenType = TokenType.None;

        public BinaryNode(ExpressionNode lhs, ExpressionNode rhs, Func<int, int, int> op)
        {
            this.lhs    = lhs;
            this.rhs    = rhs;
            this.op     = op;
        }

        public BinaryNode(ExpressionNode lhs, ExpressionNode rhs, Func<int, int, int> op, TokenType tokenType)
        {
            this.lhs = lhs;
            this.rhs = rhs;
            this.op = op;
            this.tokenType = tokenType;
        }


        public override int Eval(IContext ctx, StringBuilder sb)
        {
            
            var lhValue = lhs.Eval(ctx, sb);         
            
            //if(sb != null)
            //{               
            //    switch(tokenType)
            //    {
            //        case TokenType.Add:
            //            sb.Append('+');
            //            break;
            //        case TokenType.Sub:
            //            sb.Append('-');
            //            break;
            //        case TokenType.Mul:
            //            sb.Append('*');
            //            break;
            //        case TokenType.Div:
            //            sb.Append('/');
            //            break;
            //    }
            //}          

            var rhValue = rhs.Eval(ctx, sb);

            var result = op(lhValue, rhValue);           
                                
            return result;
        }
    }
}
