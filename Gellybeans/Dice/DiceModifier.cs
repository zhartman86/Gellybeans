namespace Gellybeans.Dice
{
    public class DiceModifier
    {
        public enum ModifierOperator
        {
            Plus,
            Minus,
            Multiply,
            Divide
        }

        public readonly ModifierOperator Opr;
        public readonly int Value;

        public DiceModifier(char opr, int value)
        {
            Opr     = ToOperator(opr);
            Value   = value;
        }
        public DiceModifier(ModifierOperator opr, int value)
        {
            Opr     = opr; 
            Value   = value;
        }

        public static ModifierOperator ToOperator(char opr) => opr switch
        {
            '+' => ModifierOperator.Plus,
            '-' => ModifierOperator.Minus,
            '*' => ModifierOperator.Multiply,
            '/' => ModifierOperator.Divide,
            _   => 0
        };
        
        public override string ToString() => Opr switch
        {
            ModifierOperator.Plus       => "+" + Value,
            ModifierOperator.Minus      => "-" + Value,
            ModifierOperator.Multiply   => "*" + Value,
            ModifierOperator.Divide     => "/" + Value,
            _                           => "?" + Value
        };
    }
}
