using System.Runtime.CompilerServices;
using System.Text;

namespace Gellybeans.Expressions
{
    public class KeyValuePairValue : IReduce, IContainer, IMember, IString
    {
        public string Key { get; set; }
        public dynamic Value { get; set; }

        public dynamic[] Values
        {

            get
            {
                if(Value is ArrayValue a)
                    return a.Values;
                return Array.Empty<dynamic>();
            }
            set
            {
                if(Value is ArrayValue a)
                    a.Values = value;
            }
        }

        public KeyValuePairValue(StringValue key, dynamic value)
        {
            Key = key.String.ToUpper();
            Value = value is string s ? new StringValue(s) : value;
        }

        public dynamic this[int index]
        {
            get
            {
                if(Value is ArrayValue a)
                    return a[index];
                return "%"; 
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


        public dynamic Reduce(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            return new KeyValuePairValue(Key, Value is IReduce r ? r.Reduce(depth, caller, sb, ctx) : Value);
        }

        public bool TryGetMember(string name, out dynamic value, dynamic[] args)
        {
            if(name == "KEY")
            {
                value = Key; 
                return true;
            }              
            if(name == "VALUE")
            {
                value = Value; 
                return true;
            }
            if(Value is IMember m)
            {
                m.TryGetMember(name, out value, args);
                return true;
            }

            value = "%";
            return false;
        }

        public override string ToString() =>
            Value.ToString();

        public string ToStr() =>
            $"{Key}: {Value}";

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
