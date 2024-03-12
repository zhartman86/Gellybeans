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

        public Bonus GetBonus(IContext ctx)
        {
            Bonus b = new Bonus() { Name = BonusName };

            if (BonusType != null && BonusValue != null)
            {
                var typeValue = BonusType.Eval(ctx, null!);
                if (typeValue is IReduce r)
                    typeValue = r.Reduce(ctx, null!);

                var valueValue = BonusValue.Eval(ctx, null!);
                if (valueValue is IReduce rr)
                    typeValue = rr.Reduce(ctx, null!);

                var hasType = int.TryParse(typeValue.ToString(), out int type);
                var hasValue = int.TryParse(typeValue.ToString(), out int value);

                b.Type = hasType ? (BonusType)type : (BonusType)(-1);
                b.Value = hasValue ? value : 0;
            }

            return b;
        }


        public override dynamic Eval(IContext ctx, StringBuilder sb) =>
            GetBonus(ctx);

    }
}
