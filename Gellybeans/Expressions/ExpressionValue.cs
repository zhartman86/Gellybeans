using System.Text;

namespace Gellybeans.Expressions
{
    public class ExpressionValue : IReduce
    {        
        public string Expression { get; set; }

        public ExpressionValue(string expr)
        {         
            Expression = expr;
        }
            

        public override string ToString() =>
            Expression;

        public dynamic Reduce(IContext ctx, StringBuilder sb)
        {
            var result = Parser.Parse(Expression, ctx, sb).Eval(ctx, sb);           
            while(result is IReduce r)
                result = r.Reduce(ctx, sb);       
            return result;
        }
            
                     
    
        public static implicit operator ExpressionValue(string s) => 
            new(s);

        public override bool Equals(object? obj)
        {
            if(ReferenceEquals(obj, null))
                return false;

            if(obj is IReduce rhs)
                return Equals(Expression.Equals(rhs));
            return false;
        }

        public override int GetHashCode() =>
            Expression.GetHashCode();
       
    }
}
