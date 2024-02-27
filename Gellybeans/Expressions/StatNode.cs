using Gellybeans.Pathfinder;

namespace Gellybeans.Expressions
{
    public class StatNode : ExpressionNode
    {
        public Stat Stat { get; } = new Stat();

        public StatNode(int baseValue) =>
            Stat.Base = baseValue;

        public override int Eval()
        {
            return Stat.Value;
        }

        public override string ToString()
        {
            return Stat.Base.ToString();
        }

        public static StatNode operator +(StatNode lhs, Bonus rhs)
        {
            lhs.Stat.AddBonus(rhs);
            return lhs;
        }



        public static StatNode operator -(StatNode lhs, Bonus rhs) 
        {
            lhs.Stat.RemoveBonus(rhs);
            return lhs;
        }

        public static StatNode operator -(StatNode lhs, string rhs)
        {
            lhs.Stat.RemoveBonus(rhs);
            return lhs;
        }
    }   
}
