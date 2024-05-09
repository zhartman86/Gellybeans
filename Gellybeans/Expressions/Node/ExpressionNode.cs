using System.Text;

namespace Gellybeans.Expressions
{
    /// <summary>
    /// Nodes are the post-parse tree
    /// 
    /// 
    /// </summary>

    public abstract class ExpressionNode
    {
        public abstract dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!);
    }
}


