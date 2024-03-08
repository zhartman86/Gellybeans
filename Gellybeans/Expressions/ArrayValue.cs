using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gellybeans.Expressions
{
    public class ArrayValue : IEval
    {
        public dynamic[] Values { get; set; }

        public ArrayValue(dynamic[] values) =>
            Values = values;

        public dynamic this[int index]
        {
            get
            {
                if(index >= 0 && index < Values.Length)
                    return Values[index];
                return new StringValue("Index out of range");
            }
            set 
            {
                if(index >= 0 && index < Values.Length)
                    Values[index] = value;
            }
        }
        
        public dynamic Eval(IContext ctx, StringBuilder sb)
        {
            var results = new StringBuilder();
            results.Append('[');
            for(int i = 0; i < Values.Length; i++)
            {
                var result = Values[i];
                if(result is IEval e)
                    result = e.Eval(ctx, sb);

                results.Append($"{result}");
                if(i < Values.Length - 1) 
                    results.Append(", ");

            }
            results.Append(']');
            return results.ToString();
        }


        public override string ToString()
        {
            var s = new StringBuilder();
            
            s.Append("[ ");
            for(int i = 0; i < Values.Length; i++)
            {
                s.Append($"{Values[i]}");
                if(i < Values.Length - 1 ) 
                    s.Append(", ");

            }
            s.Append(" ]");
            return s.ToString();
        }

        public static ArrayValue operator +(ArrayValue lhs, dynamic rhs) 
        { 
            for(int i = 0;i < lhs.Values.Length; i++)
            {
                lhs[i] += rhs;
            }
            return lhs;
        }
    }
}
