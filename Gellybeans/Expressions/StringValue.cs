using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Gellybeans.Expressions
{
    public class StringValue : IEval
    {
        public string String { get; set; }
        
        static readonly Regex brackets = new(@"\{.*?\}", RegexOptions.Compiled);
        
        public StringValue(string value)
        {
            String = value;
        }           

        public override string ToString()
        {
            string str = String.Replace(@"\n", "\n");

            str = brackets.Replace(str!, m =>
            {
                return $"**{m.Value}**";      
            });

            return str;
        }
        

        public dynamic Eval(IContext ctx, StringBuilder sb)
        {
            string str = String.Replace(@"\n", "\n");

            str = brackets.Replace(str!, m =>
            {
                var s = m.Value.Trim(new char[] { '{', '}' });
                var p = Parser.Parse(s, ctx).Eval(ctx);
                return p.ToString();
            });

            return str;
        }
    }
}
