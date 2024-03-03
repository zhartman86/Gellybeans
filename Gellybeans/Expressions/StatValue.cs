using Gellybeans.Pathfinder;
using System.Text;

namespace Gellybeans.Expressions
{
    public class StatValue : ValueNode
    {
        public Stat Stat { get { return (Stat)Value; } }
        
        public StatValue(int baseValue) : base(new Stat(baseValue)) { }

        public override ValueNode Eval(IContext ctx, StringBuilder sb) =>
            Value;
           
        public override string ToString() =>
            Value.ToString();


    }   
}
