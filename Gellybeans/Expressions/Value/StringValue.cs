using System.Text;
using System.Text.RegularExpressions;

namespace Gellybeans.Expressions
{
    public class StringValue : IDisplay
    {
        public string String { get; set; }

        public StringValue(string value) =>
            String = value;

        public string Display(int depth, object caller, StringBuilder sb, IContext ctx)
        {
            var str = Parse(depth, ctx, String);
            str = str.Replace(@"\n", "\n");
            return str;
        }

        public string Parse(int depth, IContext ctx, string s)
        {
            var reader = new StringReader(s);
            var sb = new StringBuilder();

            int c = reader.Peek();
            char chr = c < 0 ? '\0' : (char)c;

            while(chr != '\0') 
            {
                c = reader.Read();
                chr = c < 0 ? '\0' : (char)c;

                if(chr == '{')
                {
                    var r = ParseDepth('{', '}', reader, chr, out chr);

                    if(r.Length > 0)
                    {
                        Console.WriteLine($"FOUND STR EXPR: {r}");
                        var result = Parser.Parse(r[1..^1], this, ctx: ctx).Eval(depth: depth, caller: this, sb: sb, ctx : ctx);
                        if(result is IReduce rr)
                            result = rr.Reduce(depth: depth, caller: this, sb: sb, ctx : ctx);
                        Console.WriteLine(result.ToString());
                        sb.Append(result.ToString());
                    }
                }

                

                if(chr == '\0')
                    break;

                sb.Append(chr);              
            }

            return sb.ToString();
        }

        static string ParseDepth(char open, char close, StringReader reader, char chr, out char outChar)
        {
            var sb = new StringBuilder();
            int c;
            
            while(chr != close)
            {
                sb.Append(chr);
                
                c = reader.Read();
                chr = c < 0 ? '\0' : (char)c;

                if(chr == open)
                    sb.Append(ParseDepth(open, close, reader, chr, out chr));

                if(chr == '\0')
                    break;

                                 
            }
            sb.Append(chr);

            c = reader.Read();
            outChar = c < 0 ? '\0' : (char)c;

            return sb.ToString();
        }

        public override string ToString() =>
            String;

        public static implicit operator StringValue(string s) =>
            new(s);

        public static StringValue operator +(StringValue lhs, StringValue rhs) =>
             lhs.String + rhs.String;

        public static StringValue operator +(StringValue lhs, string rhs) =>
            lhs.String + rhs;

        public static StringValue operator +(string lhs, StringValue rhs) =>
            lhs + rhs.String;

        public static bool operator ==(StringValue lhs, StringValue rhs) =>
            lhs.String == rhs.String;
        public static bool operator !=(StringValue lhs, StringValue rhs) =>
            lhs.String == rhs.String;


        public static bool operator ==(StringValue lhs, int rhs) =>
            false;
        public static bool operator !=(StringValue lhs, int rhs) =>
            true;
        public static bool operator >(StringValue lhs, int rhs) =>
            false;
        public static bool operator >=(StringValue lhs, int rhs) =>
            false;
        public static bool operator <(StringValue lhs, int rhs) =>
            false;
        public static bool operator <=(StringValue lhs, int rhs) =>
            false;
        public static bool operator ==(int lhs, StringValue rhs) =>
            false;
        public static bool operator !=(int lhs, StringValue rhs) =>
            true;
        public static bool operator >(int lhs, StringValue rhs) =>
            false;
        public static bool operator >=(int lhs, StringValue rhs) =>
            false;
        public static bool operator <(int lhs, StringValue rhs) =>
            false;
        public static bool operator <=(int lhs, StringValue rhs) =>
            false;









        public override int GetHashCode() =>
            String.GetHashCode();

        public override bool Equals(object? obj)
        {
            if (obj is StringValue s)
                return String == s.String;
            return false;
        }
    }


}
