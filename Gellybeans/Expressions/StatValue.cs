using Gellybeans.Pathfinder;
using System.Text;

namespace Gellybeans.Expressions
{
    public class StatValue
    {
        public Stat Stat { get; set; } = new();
        
        public StatValue(int baseValue)
        {
            Stat.Base = baseValue;
        }
           
        public override string ToString() =>
            Stat.ToString();


    }   
}
