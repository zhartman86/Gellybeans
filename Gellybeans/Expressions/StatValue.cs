using Gellybeans.Pathfinder;
using System.Text;

namespace Gellybeans.Expressions
{
    public class StatValue : ValueNode
    {
        public Stat Stat { get; set; }
        
        public StatValue(int baseValue) : base(new Stat(baseValue)) 
        {
            Stat = Value;
        }

        public override ValueNode Eval(IContext ctx, StringBuilder sb) =>
            Stat;
           
        public override string ToString() =>
            Stat.ToString();


    }   
}
