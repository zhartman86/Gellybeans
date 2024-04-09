using System.Collections.Generic;
using System.Text;

namespace Gellybeans.Expressions
{
    public class ForNode : ExpressionNode
    {
        readonly VarNode itr;
        readonly ExpressionNode enumerable;
        readonly List<Token> statement;

        public ForNode(VarNode itr, ExpressionNode enumerable, List<Token> statement)
        {
            this.itr = itr;
            this.enumerable = enumerable;
            this.statement = statement;
        }

        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            var iterable = itr.VarName.ToUpper();
            var variable = enumerable.Eval(depth, caller, sb, ctx);


            if(variable is IContainer a)
            {
                for(int i = 0; i < a.Values.Length; i++)
                {
                    var scope = new ScopedContext(ctx, iterable, a.Values[i]);
                    Parser.Parse(statement, caller, sb, scope)
                                    .Eval(depth, caller, sb, scope);
                    


                    //set back any changes made
                    a.Values[i] = scope[iterable];
                }

                if(enumerable is VarNode vn)
                    ctx[vn.VarName] = a;
            }

            if(variable is RangeValue r)
            {
                if(r.Lower > r.Upper)
                {
                    for(int i = r.Lower; i >= r.Upper; i--)
                    {
                        var scope = new ScopedContext(ctx, iterable, i);
                        Parser.Parse(statement, caller, sb, scope)
                            .Eval(depth, caller, sb, scope);
                    }                      
                }
                else
                {
                    for(int i = r.Lower; i <= r.Upper; i++)
                    {
                        var scope = new ScopedContext(ctx, iterable, i);
                        Parser.Parse(statement, caller, sb, scope)
                            .Eval(depth, caller, sb, scope);
                    }                        
                }
            }

            return 0;
        }


    }
}
