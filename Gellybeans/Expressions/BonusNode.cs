using System.Text;
using System.Xml.Serialization;

namespace Gellybeans.Expressions
{
    public class BonusNode : ExpressionNode
    {
        readonly string             bonusName;
        readonly ExpressionNode?    bonusType;
        readonly ExpressionNode?    bonusValue;
        
        public string BonusName { get { return bonusName; } }
        public int? BonusType;
        public int? BonusValue;

        public BonusNode(string bonusName, ExpressionNode bonusType = null!, ExpressionNode bonusValue = null!)
        {
            this.bonusName  = bonusName;
            this.bonusType  = bonusType;
            this.bonusValue = bonusValue;
        }

        public override int Eval()
        {
            BonusType = bonusType?.Eval();
            BonusValue = bonusValue?.Eval();
            return 0;
        }
    }
}
