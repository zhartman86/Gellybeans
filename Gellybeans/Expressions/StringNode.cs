using System.Text;
using System.Text.RegularExpressions;

namespace Gellybeans.Expressions
{
    public class StringNode : ExpressionNode
    {
        string str;
        readonly StringBuilder sb;
        IContext ctx;

        public string String { get { return str; } }

        static readonly Regex brackets = new Regex(@"\{.*?\}", RegexOptions.Compiled);

        public StringNode(string str, IContext ctx = null!, StringBuilder sb = null!)
        {
            this.str = str;
            this.ctx = ctx;
            this.sb = sb;          
        }           

        public override int Eval()
        {
            int? result = null;

            str = str.Replace(@"\n", "\n");

            str = brackets.Replace(str!, m =>
            {
                var str = m.Value.Trim(new char[] { '{', '}' });
                var p = Parser.Parse(str, ctx).Eval();
                result ??= p;
                if(result != null && result == -999)
                {
                    result = null;
                    return "";
                }         
                return p.ToString();
            });
            sb?.AppendLine(str);

            return result != null ? result.Value : 0;
        }
    }
}
