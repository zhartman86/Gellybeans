using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Gellybeans.Expressions
{
    public class NumberNode : ExpressionNode
    {
        readonly int number;

        public NumberNode(int number) =>
            this.number = number;

        public override int Eval()
        {
            return number;
        }
            
        public override string ToString() => 
            number.ToString();

        public static NumberNode operator +(NumberNode lhs, NumberNode rhs) =>
            new(lhs.number + rhs.number);

        public static NumberNode operator -(NumberNode lhs, NumberNode rhs) =>
            new(lhs.number - rhs.number);     

        public static NumberNode operator *(NumberNode lhs, NumberNode rhs) =>
            new(lhs.number * rhs.number);      

        public static NumberNode operator /(NumberNode lhs, NumberNode rhs) =>
            new(lhs.number / rhs.number);
        
        public static NumberNode operator %(NumberNode lhs, NumberNode rhs) =>
            new(lhs.number % rhs.number);

        public static NumberNode operator |(NumberNode lhs, NumberNode rhs) =>
            new(lhs.number | rhs.number);

        public static NumberNode operator &(NumberNode lhs, NumberNode rhs) =>
            new(lhs.number & rhs.number);

        public static bool operator ==(NumberNode lhs, NumberNode rhs) =>
            lhs.number == rhs.number;

        public static bool operator !=(NumberNode lhs, NumberNode rhs) =>
            lhs.number != rhs.number;

        public static bool operator >(NumberNode lhs, NumberNode rhs) =>
            lhs.number > rhs.number;

        public static bool operator <(NumberNode lhs, NumberNode rhs) =>
            lhs.number < rhs.number;

        public static bool operator <=(NumberNode lhs, NumberNode rhs) =>
            lhs.number <= rhs.number;

        public static bool operator >=(NumberNode lhs, NumberNode rhs) =>
            lhs.number >= rhs.number;
    }
}
