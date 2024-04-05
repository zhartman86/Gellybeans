using System.Runtime.ExceptionServices;
using System.Text;

namespace Gellybeans.Expressions
{
    public class ArrayValue : IReduce, IString, IContainer, IMember
    {
        public dynamic[] Values { get; set; }
        public Dictionary<string, int> Keys { get; set; } 

        public dynamic this[int index]
        {
            get
            {
                if(index >= 0 && index < Values.Length)
                    return Values[index];
                Console.WriteLine($"INDEX: {index}, COUNT: {Values.Length}");
                return "%";
            }
            set
            {
                if(index >= 0 && index < Values.Length)
                {
                    if(value is KeyValuePairValue kvp)
                    {
                        AddKey(kvp.Key, index);
                        Values[index] = kvp.Value;
                    }
                    else
                        Values[index] = value;                       
                }                   
            }
        }

        public dynamic this[string key] 
        { 
            get 
            {
                if(Keys.TryGetValue(key.ToUpper(), out var value))
                    return Values[value];                             
                return "%"; 
            }
            
        }

        public ArrayValue(dynamic[] values, Dictionary<string, int> keys = null!)
        {
            Values = values;
            Keys = keys;
        }
   
        public bool AddKey(string key, int index) 
        { 
            if(index >= 0 && index < Values.Length)
            {
                Keys[key] = index;
                return true;
            }
            return false;
        }

        public ref dynamic GetValueByRef(int index) => 
            ref Values[index];

        public bool TryGetMember(string name, out dynamic value)
        {
            if(name == "LEN")
            {
                value = Values.Length;
                return true;
            }
            else if(name == "IDX")
            {
                value = Values.Length - 1;
                return true;
            }
            value = "%";
            return false;
        }

        public string ToStr()
        {
            var sb = new StringBuilder();
            for(int i = 0; i < Values.Length; i++)
            {
                bool gotKey = false;

                Console.WriteLine("KEYS NULL?");
                if(Keys != null)
                {
                    Console.WriteLine("KEYS NOT NULL");
                    foreach(var index in Keys)
                    {
                        Console.WriteLine($"checking {index.Value}");
                        if(i == index.Value)
                        {
                            Console.WriteLine("Got key");
                            gotKey = true;
                            if(Values[i] is ArrayValue ka)
                            {
                                sb.AppendLine($"[{i}] {index.Key}:");
                                ParseDepth(ka, sb);
                            }
                            else
                                sb.AppendLine($"[{i}] {index.Key}: {Values[i]}");
                            break;
                        }

                    }
                }
                
                if(!gotKey)
                {
                    if(Values[i] is ArrayValue a)
                    {
                        sb.AppendLine($"[{i}]:");
                        ParseDepth(a, sb);
                    }
                    else
                    {
                        Console.WriteLine(Values[i].GetType());
                        sb.AppendLine($"[{i}] {Values[i]}");
                    }
                }                                  
            }
            return $"```{sb}```";
        }

        void ParseDepth(ArrayValue a, StringBuilder sb, string indent = " | ")
        {            
            for(int i = 0; i < a.Values.Length ;i++) 
            {
                bool gotKey = false;
                if(Keys != null)
                {
                    foreach(var index in Keys)
                    {
                        if(i == index.Value)
                        {
                            gotKey = true;
                            if(Values[i] is ArrayValue ka)
                            {
                                sb.AppendLine($"{indent}[{i}] {index.Key}:");
                                ParseDepth(ka, sb, indent + " | ");
                            }
                            else
                                sb.AppendLine($"{indent}[{i}] {index.Key}: {index.Value}");
                        }
                    }
                }
                if(!gotKey)
                {
                    if(a.Values[i] is ArrayValue aa)
                    {
                        sb.AppendLine($"{indent}[{i}]:");
                        ParseDepth(aa, sb, indent + " | ");
                    }
                    else
                        sb.AppendLine($"{indent}[{i}] {a.Values[i]}");
                }                             
            }
        }

        public dynamic Reduce(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            var a = new dynamic[Values.Length];
            for (int i = 0; i < Values.Length; i++)
            {
                var result = Values[i];
                if (result is IReduce r)
                    result = r.Reduce(depth: depth, caller: this, sb: sb, ctx : ctx);             
                a[i] = result;
            }
            return new ArrayValue(a);
        }

        public override string ToString()
        {
            var s = new StringBuilder();

            s.Append("[ ");
            for(int i = 0; i < Values.Length; i++)
            {
                s.Append(Values[i]);
                if(i < Values.Length - 1)
                    s.Append(", ");
            }
            s.Append(" ]");
            return s.ToString();
        }

        public static implicit operator ArrayValue(dynamic[] values) =>
            new(values);

        public static ArrayValue operator +(ArrayValue lhs, dynamic rhs)
        {
            if(lhs.Values == null || lhs.Values.Length == 0)
            {
                var array = new dynamic[rhs];
                Array.Fill(array, 0);
                return new ArrayValue(array);
            }              

            var a = new ArrayValue(new dynamic[lhs.Values.Length]);
            for (int i = 0; i < lhs.Values.Length; i++)
            {
                a[i] = lhs[i] + rhs;
            }
            return a;
        }

        public static ArrayValue operator -(ArrayValue lhs, dynamic rhs)
        {
            var a = new ArrayValue(new dynamic[lhs.Values.Length]);
            for (int i = 0; i < lhs.Values.Length; i++)
            {
                a[i] = lhs[i] - rhs;
            }
            return a;
        }

        public static ArrayValue operator *(ArrayValue lhs, dynamic rhs)
        {
            var a = new ArrayValue(new dynamic[lhs.Values.Length]);
            for (int i = 0; i < lhs.Values.Length; i++)
            {
                a[i] = lhs[i] * rhs;
            }
            return a;
        }

        public static ArrayValue operator /(ArrayValue lhs, dynamic rhs)
        {
            var a = new ArrayValue(new dynamic[lhs.Values.Length]);
            for (int i = 0; i < lhs.Values.Length; i++)
            {
                a[i] = lhs[i] / rhs;
            }
            return a;
        }

        public static ArrayValue operator %(ArrayValue lhs, dynamic rhs)
        {
            var a = new ArrayValue(new dynamic[lhs.Values.Length]);
            for (int i = 0; i < lhs.Values.Length; i++)
            {
                a[i] = lhs[i] % rhs;
            }
            return a;
        }

        public static ArrayValue operator ==(ArrayValue lhs, dynamic rhs)
        {
            var a = new ArrayValue(new dynamic[lhs.Values.Length]);
            
            for (int i = 0; i < lhs.Values.Length; i++)
            {
                a[i] = lhs[i] == rhs;
            }
            return a;
        }

        public static ArrayValue operator !=(ArrayValue lhs, dynamic rhs)
        {
            var a = new ArrayValue(new dynamic[lhs.Values.Length]);
            for (int i = 0; i < lhs.Values.Length; i++)
            {
                a[i] = lhs[i] != rhs;
            }
            return a;
        }

        public static ArrayValue operator <(ArrayValue lhs, dynamic rhs)
        {
            var a = new ArrayValue(new dynamic[lhs.Values.Length]);
            for (int i = 0; i < lhs.Values.Length; i++)
            {
                a[i] = lhs[i] < rhs;
            }
            return a;
        }

        public static ArrayValue operator >(ArrayValue lhs, dynamic rhs)
        {
            var a = new ArrayValue(new dynamic[lhs.Values.Length]);
            for (int i = 0; i < lhs.Values.Length; i++)
            {
                a[i] = lhs[i] > rhs;
            }
            return a;
        }

        public static ArrayValue operator >=(ArrayValue lhs, dynamic rhs)
        {
            var a = new ArrayValue(new dynamic[lhs.Values.Length]);
            for (int i = 0; i < lhs.Values.Length; i++)
            {
                a[i] = lhs[i] >= rhs;
            }
            return a;
        }

        public static ArrayValue operator <=(ArrayValue lhs, dynamic rhs)
        {
            var a = new ArrayValue(new dynamic[lhs.Values.Length]);
            for (int i = 0; i < lhs.Values.Length; i++)
            {
                a[i] = lhs[i] <= rhs;
            }
            return a;
        }

        public static ArrayValue operator *(ArrayValue lhs, ArrayValue rhs)
        {
            var a = new List<dynamic>();
            for (int i = 0; i < lhs.Values.Length; i++)
            {
                for (int j = 0; j < rhs.Values.Length; j++)
                {
                    a.Add(lhs[i] * rhs[j]);
                }
            }
            return new ArrayValue(a.ToArray());
        }

        public static ArrayValue operator +(ArrayValue lhs, ArrayValue rhs)
        {
            var a = new List<dynamic>();
            for (int i = 0; i < lhs.Values.Length; i++)
            {
                for (int j = 0; j < rhs.Values.Length; j++)
                {
                    a.Add(lhs[i] + rhs[j]);
                }
            }
            return new ArrayValue(a.ToArray());
        }

        public static bool operator ==(ArrayValue lhs, ArrayValue rhs)
        {
            if (lhs.Values.Length == rhs.Values.Length)
            {
                for (int i = 0; i < lhs.Values.Length; i++)
                {
                    if (lhs.Values[i] == rhs.Values[i])
                        continue;
                    else return false;
                }
                return true;
            }
            return false;
        }

        public static bool operator !=(ArrayValue lhs, ArrayValue rhs)
        {
            if (lhs.Values.Length == rhs.Values.Length)
            {
                for (int i = 0; i < lhs.Values.Length; i++)
                {
                    if (lhs.Values[i] != rhs.Values[i])
                        continue;
                    else return false;
                }
                return true;
            }
            return false;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;
            else if (obj is ArrayValue a)
            {
                if (Values.Length == a.Values.Length)
                {
                    for (int i = 0; i < Values.Length; i++)
                    {
                        if (Values[i] == a.Values[i])
                            continue;
                        else return false;
                    }
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode() =>
            Values.GetHashCode();
    }
}
