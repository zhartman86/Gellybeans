using System.Text;

namespace Gellybeans.Expressions
{
    public class ArrayValue : IDisplay, IReduce
    {
        public dynamic[] Values { get; set; }

        public ArrayValue(dynamic[] values) =>
            Values = values;

        public dynamic this[int index]
        {
            get
            {
                if (index >= 0 && index < Values.Length)
                    return Values[index];
                return new StringValue("Index out of range");
            }
            set
            {
                if (index >= 0 && index < Values.Length)
                    Values[index] = value;
            }
        }

        public string Display(IContext ctx, StringBuilder sb)
        {
            var results = new StringBuilder();
            results.Append('[');
            for (int i = 0; i < Values.Length; i++)
            {
                var result = Values[i];
                results.Append($"{result}");
                if (i < Values.Length - 1)
                    results.Append(", ");
            }
            results.Append(']');
            return results.ToString();
        }

        public dynamic Reduce(IContext ctx, StringBuilder sb)
        {
            var a = new dynamic[Values.Length];
            for (int i = 0; i < Values.Length; i++)
            {
                if (Values[i] is IReduce r)
                    a[i] = r.Reduce(ctx, sb);
                else
                    a[i] = Values[i];
            }
            return new ArrayValue(a);
        }

        public override string ToString()
        {
            var s = new StringBuilder();

            s.Append("{ ");
            for (int i = 0; i < Values.Length; i++)
            {
                s.Append($"{Values[i]}");
                if (i < Values.Length - 1)
                    s.Append(", ");
            }
            s.Append(" }");
            return s.ToString();
        }

        public static implicit operator ArrayValue(dynamic[] values) =>
            new(values);

        public static ArrayValue operator +(ArrayValue lhs, dynamic rhs)
        {
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
            Console.WriteLine($"returning > array. count:{a.Values.Length}");
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
