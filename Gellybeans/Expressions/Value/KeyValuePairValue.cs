using System.Runtime.CompilerServices;
using System.Text;

namespace Gellybeans.Expressions
{
    public class KeyValuePairValue : IReduce, IDisplay
    {
        public string Key { get; set; }
        public dynamic Value { get; set; }

        public KeyValuePairValue(StringValue key, dynamic value)
        {
            Key = key.String;
            Value = value;
        }

        public dynamic this[int index]
        {
            get
            {
                if(Value is ArrayValue a)
                    return a[index];
                return "Value cannot be indexed."; 
            }
            set
            {
                if(Value is ArrayValue a)
                {
                    if(a[index] is KeyValuePairValue kvp && value is not KeyValuePairValue)
                        a[index] = new KeyValuePairValue(kvp.Key, value);
                    else
                        a[index] = value;
                }
                    
            }
        }

        public string Display(int depth, object caller, StringBuilder sb, IContext ctx = null!) =>
            $"{Key}: {(Value is IReduce r ? r.Reduce(depth, caller, sb, ctx) : Value)}";

        public dynamic Reduce(int depth, object caller, StringBuilder sb, IContext ctx = null!) =>
            new KeyValuePairValue(Key, Value is IReduce r ? r.Reduce(depth, caller, sb, ctx) : Value);

        public override string ToString() =>
            Value.ToString();
        

        public static dynamic operator +(KeyValuePairValue lhs, dynamic rhs) =>
            new KeyValuePairValue(lhs.Key, lhs.Value + rhs);
        public static dynamic operator -(KeyValuePairValue lhs, dynamic rhs) =>
            new KeyValuePairValue(lhs.Key, lhs.Value - rhs);
        public static dynamic operator *(KeyValuePairValue lhs, dynamic rhs) =>
            new KeyValuePairValue(lhs.Key, lhs.Value * rhs);
        public static dynamic operator /(KeyValuePairValue lhs, dynamic rhs) =>
            new KeyValuePairValue(lhs.Key, lhs.Value / rhs);
        public static dynamic operator %(KeyValuePairValue lhs, dynamic rhs) =>
            new KeyValuePairValue(lhs.Key, lhs.Value % rhs);
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
            new KeyValuePairValue(rhs.Key, lhs + rhs.Value);             
        public static dynamic operator -(dynamic lhs, KeyValuePairValue rhs) =>
           new KeyValuePairValue(rhs.Key, lhs - rhs.Value);
        public static dynamic operator *(dynamic lhs, KeyValuePairValue rhs) =>
            new KeyValuePairValue(rhs.Key, lhs * rhs.Value);
        public static dynamic operator /(dynamic lhs, KeyValuePairValue rhs) =>
            new KeyValuePairValue(rhs.Key, lhs / rhs.Value);
        public static dynamic operator %(dynamic lhs, KeyValuePairValue rhs) =>
            new KeyValuePairValue(rhs.Key, lhs % rhs.Value);
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
