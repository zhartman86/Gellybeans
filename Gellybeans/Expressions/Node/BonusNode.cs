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

        public Bonus GetBonus(int depth, IContext ctx)
        {
            Bonus b = new Bonus() { Name = BonusName };

            if (BonusType != null && BonusValue != null)
            {
                var typeValue = BonusType.Eval(depth, ctx, null!);
                if (typeValue is IReduce r)
                    typeValue = r.Reduce(depth, ctx, null!);

                var valueValue = BonusValue.Eval(depth, ctx, null!);
                if (valueValue is IReduce rr)
                    typeValue = rr.Reduce(depth, ctx, null!);

                var hasType = int.TryParse(typeValue.ToString(), out int type);
                var hasValue = int.TryParse(typeValue.ToString(), out int value);

                b.Type = hasType ? (BonusType)type : (BonusType)(-1);
                b.Value = hasValue ? value : 0;
            }

            return b;
        }


        public override dynamic Eval(int depth, IContext ctx, StringBuilder sb)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            return GetBonus(depth, ctx);
        }
            

    }
}
