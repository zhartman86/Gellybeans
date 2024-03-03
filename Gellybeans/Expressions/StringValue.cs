using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Gellybeans.Expressions
{
    public class StringValue : ValueNode
    {
        public string String { get; set; }
        
        public StringValue(string str) : base(str) 
        {
            String = Value;
        }           

        static readonly Regex brackets = new(@"\{.*?\}", RegexOptions.Compiled);

        public override ValueNode Eval(IContext ctx, StringBuilder sb)
        {
            string s = String.Replace(@"\n", "\n");

            s = brackets.Replace(s!, m =>
            {
                var s = m.Value.Trim(new char[] { '{', '}' });
                var p = Parser.Parse(s, ctx).Eval(ctx, sb);
                return p.ToString();
            });

            return s;
        }
    }
}
