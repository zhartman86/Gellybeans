using Gellybeans.Pathfinder;
using System.Text;

namespace Gellybeans.Expressions
{
    public class BonusNode : ExpressionNode
    {
        public string BonusName { get; }
        public ExpressionNode? BonusType;
        public ExpressionNode? BonusValue;

        public BonusNode(string bonusName, ExpressionNode bonusType = null!, ExpressionNode bonusValue = null!)
        {
            BonusName = bonusName;
            BonusType = bonusType;
            BonusValue = bonusValue;
        }

        public Bonus GetBonus()
        {
            Bonus b = new Bonus() { Name = BonusName };      
            
            if(BonusType != null && BonusValue != null)
            {
                var hasType = int.TryParse(BonusType.Eval(null!, null!).ToString(), out int type);
                var hasValue = int.TryParse(BonusValue.Eval(null!, null!).ToString(), out int value);

                b.Type = hasType ? (BonusType)type : (BonusType)(-1);
                b.Value = hasValue ? value : 0;
            }

            return b;
        }
            

        public override dynamic Eval(IContext ctx, StringBuilder sb) =>
            GetBonus();

    }
}
