using System.Text;

namespace Gellybeans.Expressions
{
    public class StringValue : IDisplay, IComparable, IMember
    {
        public string String { get; set; }

        public StringValue(string value) =>
            String = value;

        public string Display(int depth, object caller, StringBuilder sb, IContext ctx)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";
            
            var str = String.Replace(@"\n", "\n");
            str = Parse(depth, ctx, str);
            
            return str;
        }

        public bool TryGetMember(string name, out dynamic value, dynamic[] args)
        {
            switch(name)
            {
                case "LEN":
                    value = String.Length;
                    return true;
                case "HAS":
                    if(args.Length > 0)
                    {
                        if(String.Contains(args[0].ToString()))
                            value = true;
                        else
                            value = false;

                        return true;
                    }
                    value = new StringValue("%");
                    return false;
                case "TRIM":
                    if(args.Length > 0)
                    {
                        value = new StringValue(String.Trim(char.Parse(args[0])));
                        return true;
                    }                         
                    value = String.Trim();
                    return true;
                case "REPLACE":
                    if(args.Length > 1)
                    {
                        value = new StringValue(String.Replace(args[0], args[1]));
                        return true;
                    }
                    value = new StringValue("%");
                    return false;
                case "SPLIT":
                    if(args.Length > 0)
                    {
                        var split = String.Split(args[0], StringSplitOptions.RemoveEmptyEntries);
                        var array = new dynamic[split.Length];
                        for(int i = 0; i < split.Length; i++)
                            array[i] = new StringValue(split[i]);

                        value = new ArrayValue(array);
                        return true;
                    }
                    value = value = new StringValue("%");
                    return false;
                case "SUB":
                    if(args.Length == 1)
                    {
                        value = new StringValue(String.Substring(args[0]));
                        return true;
                    }
                    if(args.Length == 2)
                    {
                        value = new StringValue(String.Substring(args[0], args[1]));
                        return true;
                    }
                    value = new StringValue("%");
                    return false;
                case "UPPER":
                    value = new StringValue(String.ToUpper());
                    return true;
                case "LOWER":
                    value = new StringValue(String.ToLower());
                    return true;              
                default:
                    value = new StringValue("%");
                    return false;
            }
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
                    var r = new StringBuilder();                          

                    var i = 1;
                    while(i != 0)
                    {
                        c = reader.Read();
                        chr = c < 0 ? '\0' : (char)c;

                        if(chr == '{')
                            i++;
                        else if(chr == '}')
                        {
                            i--;
                            if(i == 0)
                                break;                    
                        }
                        r.Append(chr);

                        if(chr == '\0')
                            break;
                    }
                    var result = Parser.Parse(r.ToString(), null!, ctx: ctx).Eval(depth: depth, caller: this, sb: null!, ctx: ctx);
                    if(result is IReduce rr)
                        result = rr.Reduce(depth: depth, caller: this, sb: null!, ctx: ctx);
                    sb.Append(result.ToString());
                }
                else
                {
                    if(chr == '\0')
                        break;

                    if(chr == '{')
                        continue;

                    sb.Append(chr);
                }                          
            }

            return sb.ToString();
        }     

        public override string ToString() =>
            String;

        public int CompareTo(object? obj)
        {
            if(obj == null)
                return 0;
            
            if(obj is StringValue s)
                return String.CompareTo(s.String);
            if(obj is string str)
                return String.CompareTo(str);

            return 0;
        }

        public static implicit operator StringValue(string s) =>
            new(s);

        public static implicit operator string(StringValue s) =>
            new(s.String);

        public static StringValue operator +(StringValue lhs, StringValue rhs) =>
             lhs.String + rhs.String;

        public static StringValue operator +(StringValue lhs, string rhs) =>
            lhs.String + rhs;

        public static StringValue operator +(string lhs, StringValue rhs) =>
            lhs + rhs.String;

        public static bool operator ==(StringValue lhs, StringValue rhs) =>
            lhs.String == rhs.String;
        public static bool operator !=(StringValue lhs, StringValue rhs) =>
            lhs.String != rhs.String;

        public static bool operator >(StringValue lhs, StringValue rhs) =>
            lhs.String.CompareTo(rhs.String) > 0;

        public static bool operator <(StringValue lhs, StringValue rhs) =>
            lhs.String.CompareTo(rhs.String) < 0;

        public static bool operator >=(StringValue lhs, StringValue rhs) =>
            lhs.String.CompareTo(rhs.String) >= 0;

        public static bool operator <=(StringValue lhs, StringValue rhs) =>
            lhs.String.CompareTo(rhs.String) <= 0;


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

        public static bool operator ==(StringValue lhs, bool rhs) =>
            false;
        public static bool operator !=(StringValue lhs, bool rhs) =>
            true;

        public static bool operator ==(bool lhs, StringValue rhs) =>
            false;
        public static bool operator !=(bool lhs, StringValue rhs) =>
            true;









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
