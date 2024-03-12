using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gellybeans.Expressions
{
    public class FunctionValue
    {
        int ParamCount { get; set; } 
        string Body { get; set; }

        public FunctionValue(int paramCount, string body)
        {
            ParamCount = paramCount;
            Body = body;
        }

        public override string ToString() =>
            $"{Body}\nParamCount:**{ParamCount}**";
        
            



        public dynamic Invoke(string[] args, IContext ctx, StringBuilder sb)
        {
            if(args.Length != ParamCount)
                return "Arguments don't match parameter count for this function.";

            string s = Body;
            for(int i = 0; i < args.Length; i++)
            {
                s = s.Replace($"«{i}»", args[i]);
            }

            var result = Parser.Parse(s, ctx, sb).Eval();         
            return result;
        }
    }
}
