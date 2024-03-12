using System.Text;
using System.Text.RegularExpressions;

namespace Gellybeans.Expressions
{
    public class StringValue : IDisplay
    {
        public string String { get; set; }

        static readonly Regex brackets = new(@"\{.*?\}", RegexOptions.Compiled);

        public StringValue(string value) =>
            String = value;

        public string Display(IContext ctx, StringBuilder sb)
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

        public override string ToString() =>
            $"\"{String}\"";

        public static implicit operator StringValue(string s) =>
            new(s);

        public static StringValue operator +(StringValue lhs, StringValue rhs) =>
             lhs.String + rhs.String;

        public static StringValue operator +(StringValue lhs, string rhs) =>
            lhs.String + rhs;

        public static StringValue operator +(string lhs, StringValue rhs) =>
            lhs + rhs.String;

        public static StringValue operator +(StringValue lhs, int rhs) =>
            lhs.String + rhs;

        public static StringValue operator +(int lhs, StringValue rhs) =>
           lhs + rhs.String;

        public static bool operator ==(StringValue lhs, StringValue rhs) =>
            lhs.String == rhs.String;
        public static bool operator !=(StringValue lhs, StringValue rhs) =>
            lhs.String == rhs.String;

        public override bool Equals(object? obj)
        {
            if (obj is StringValue s)
                return String == s.String;
            return false;
        }
    }


}
