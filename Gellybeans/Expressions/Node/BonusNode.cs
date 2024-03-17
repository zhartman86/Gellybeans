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
                var typeValue = BonusType.Eval(depth: depth, caller: this, sb: null!, ctx: ctx);
                if (typeValue is IReduce r)
                    typeValue = r.Reduce(depth: depth, caller: this, sb: null!, ctx: ctx);

                var valueValue = BonusValue.Eval(depth: depth, caller: this, sb: null!, ctx: ctx);
                if (valueValue is IReduce rr)
                    valueValue = rr.Reduce(depth: depth, caller: this, sb: null!, ctx: ctx);

                var hasType = int.TryParse(typeValue.ToString(), out int type);
                var hasValue = int.TryParse(valueValue.ToString(), out int value);

                b.Type = hasType ? (BonusType)type : (BonusType)(-1);
                b.Value = hasValue ? value : 0;
            }

            return b;
        }


        public override dynamic Eval(int depth, object caller, StringBuilder sb, IContext ctx = null!)
        {
            depth++;
            if(depth > Parser.MAX_DEPTH)
                return "operation cancelled: maximum evaluation depth reached.";

            return GetBonus(depth, ctx);
        }
            

    }
}
