using Gellybeans.Pathfinder;
using System.Text;

namespace Gellybeans.Expressions
{

    public class ValueNode : ExpressionNode
    {       
         public dynamic Value{ get; }

        public ValueNode(dynamic value) =>
            Value = value;

        public override string ToString() => 
            Value.ToString();

        public override ValueNode Eval(IContext ctx, StringBuilder sb) => 
            this;

        public static implicit operator int(ValueNode v) =>
            int.TryParse(v.Value, out int i) ? i : 0;

        public static implicit operator Stat(ValueNode v) => 
            new(int.TryParse(v.Value, out int i) ? i : 0);

        public static implicit operator ValueNode(int i) =>
            new(new Stat(i));

        public static implicit operator string(ValueNode v) =>
            v.Value.ToString();

        public static implicit operator ValueNode(string s) =>
            new(s);

        public static implicit operator ValueNode(Stat s) =>
            new(s);

        public static implicit operator ValueNode(Bonus b) =>
            new(b);

        public static ValueNode operator +(ValueNode lhs, ValueNode rhs) =>
            new(lhs.Value + rhs.Value);
        public static ValueNode operator -(ValueNode lhs, ValueNode rhs) =>
            new(lhs.Value - rhs.Value);
        public static ValueNode operator *(ValueNode lhs, ValueNode rhs) =>
            new(lhs.Value * rhs.Value);
        public static ValueNode operator /(ValueNode lhs, ValueNode rhs) =>
            new(lhs.Value / rhs.Value);
        public static ValueNode operator %(ValueNode lhs, ValueNode rhs) =>
            new(lhs.Value % rhs.Value);
        public static ValueNode operator |(ValueNode lhs, ValueNode rhs) =>
            new(lhs.Value | rhs.Value);
        public static ValueNode operator &(ValueNode lhs, ValueNode rhs) =>
            new(lhs.Value & rhs.Value);
        public static bool operator ==(ValueNode lhs, ValueNode rhs) =>
            lhs.Value == rhs.Value;
        public static bool operator !=(ValueNode lhs, ValueNode rhs) =>
            lhs.Value != rhs.Value;
        public static bool operator >(ValueNode lhs, ValueNode rhs) =>
            lhs.Value > rhs.Value;
        public static bool operator <(ValueNode lhs, ValueNode rhs) =>
            lhs.Value < rhs.Value;
        public static bool operator <=(ValueNode lhs, ValueNode rhs) =>
            lhs.Value <= rhs.Value;
        public static bool operator >=(ValueNode lhs, ValueNode rhs) =>
            lhs.Value >= rhs.Value;

        public override bool Equals(object? obj)
        {
            if(obj is not ValueNode v) 
                return false;
            return Value == v.Value;
        }
        
        public override int GetHashCode() => 
            Value.GetHashCode();
    }

}
