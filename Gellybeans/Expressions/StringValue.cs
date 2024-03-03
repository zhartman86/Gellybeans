using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Gellybeans.Expressions
{
    public class StringValue : ValueNode
    {
        public string String { get { return Value; } }
        
        public StringValue(string value) : base(value: value) { }           

        static readonly Regex brackets = new(@"\{.*?\}", RegexOptions.Compiled);

        public override ValueNode Eval(IContext ctx, StringBuilder sb)
        {
            string str = Value.Replace(@"\n", "\n");

            str = brackets.Replace(str!, m =>
            {
                var str = m.Value.Trim(new char[] { '{', '}' });
                var p = Parser.Parse(str, ctx).Eval(ctx, sb);
                return p.ToString();
            });

            return str;
        }
    }
}
