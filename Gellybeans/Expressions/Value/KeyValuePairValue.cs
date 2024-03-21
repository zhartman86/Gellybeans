using System.Runtime.CompilerServices;
using System.Text;

namespace Gellybeans.Expressions
{
    public class KeyValuePairValue : IDisplay
    {
        public string Key { get; set; }
        public dynamic Value { get; set; }

        public KeyValuePairValue(StringValue key, dynamic value)
        {
            Key = key.String;
            Value = value;
        }

        public string Display(int depth, object caller, StringBuilder sb, IContext ctx = null!) =>
            $"{Key}: {(Value is IReduce r ? r.Reduce(depth, caller, sb, ctx) : Value)}";

        public override string ToString() =>
            $"{Key}: {Value}";
        

        public static dynamic operator +(KeyValuePairValue lhs, dynamic rhs) =>
            lhs.Value + rhs ;
        public static dynamic operator -(KeyValuePairValue lhs, dynamic rhs) =>
            lhs.Value - rhs;
        public static dynamic operator *(KeyValuePairValue lhs, dynamic rhs) =>
            lhs.Value * rhs;
        public static dynamic operator /(KeyValuePairValue lhs, dynamic rhs) =>
            lhs.Value / rhs;
        public static dynamic operator %(KeyValuePairValue lhs, dynamic rhs) =>
            lhs.Value % rhs;
        public static bool operator ==(KeyValuePairValue lhs, dynamic rhs) =>
            lhs.Value == rhs;
        public static bool operator !=(KeyValuePairValue lhs, dynamic rhs) =>
            lhs.Value != rhs;
        public static bool operator <(KeyValuePairValue lhs, dynamic rhs) =>
            lhs.Value < rhs;
        public static bool operator <=(KeyValuePairValue lhs, dynamic rhs) =>
            lhs.Value <= rhs;
        public static bool operator >(KeyValuePairValue lhs, dynamic rhs) =>
            lhs.Value > rhs;
        public static bool operator >=(KeyValuePairValue lhs, dynamic rhs) =>
            lhs.Value >= rhs;

        public static dynamic operator +(dynamic lhs, KeyValuePairValue rhs) =>
            lhs + rhs.Value;             
        public static dynamic operator -(dynamic lhs, KeyValuePairValue rhs) =>
            lhs - rhs.Value;             
        public static dynamic operator *(dynamic lhs, KeyValuePairValue rhs) =>
            lhs * rhs.Value;             
        public static dynamic operator /(dynamic lhs, KeyValuePairValue rhs) =>
            lhs / rhs.Value;             
        public static dynamic operator %(dynamic lhs, KeyValuePairValue rhs) =>
            lhs % rhs.Value;
        public static bool operator ==(dynamic lhs, KeyValuePairValue rhs) =>
            lhs == rhs.Value;
        public static bool operator !=(dynamic lhs, KeyValuePairValue rhs) =>
            lhs != rhs.Value;
        public static bool operator <(dynamic lhs, KeyValuePairValue rhs) =>
            lhs < rhs.Value;
        public static bool operator <=(dynamic lhs, KeyValuePairValue rhs) =>
            lhs <= rhs.Value;
        public static bool operator >(dynamic lhs, KeyValuePairValue rhs) =>
            lhs > rhs.Value;
        public static bool operator >=(dynamic lhs, KeyValuePairValue rhs) =>
            lhs >= rhs.Value;


        public static dynamic operator +(KeyValuePairValue lhs, KeyValuePairValue rhs) =>
            lhs.Value + rhs.Value;
        public static dynamic operator -(KeyValuePairValue lhs, KeyValuePairValue rhs) =>
            lhs.Value - rhs.Value;
        public static dynamic operator *(KeyValuePairValue lhs, KeyValuePairValue rhs) =>
            lhs.Value * rhs.Value;
        public static dynamic operator /(KeyValuePairValue lhs, KeyValuePairValue rhs) =>
            lhs.Value / rhs.Value;
        public static dynamic operator %(KeyValuePairValue lhs, KeyValuePairValue rhs) =>
            lhs.Value % rhs.Value;
        public static bool operator ==(KeyValuePairValue lhs, KeyValuePairValue rhs) =>
            lhs.Value == rhs.Value;
        public static bool operator !=(KeyValuePairValue lhs, KeyValuePairValue rhs) =>
            lhs.Value != rhs.Value;
        public static bool operator >(KeyValuePairValue lhs, KeyValuePairValue rhs) =>
            lhs.Value > rhs.Value;
        public static bool operator >=(KeyValuePairValue lhs, KeyValuePairValue rhs) =>
            lhs.Value >= rhs.Value;
        public static bool operator <(KeyValuePairValue lhs, KeyValuePairValue rhs) =>
            lhs.Value < rhs.Value;
        public static bool operator <=(KeyValuePairValue lhs, KeyValuePairValue rhs) =>
            lhs.Value <= rhs.Value;







    }
}
