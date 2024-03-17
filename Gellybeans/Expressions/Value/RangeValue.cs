namespace Gellybeans.Expressions
{
    public class RangeValue
    {
        public int Lower { get; }
        public int Upper { get; }
       
        public RangeValue(int lower, int upper, bool oneRandom = false)
        {
            Lower = lower;
            Upper = upper;
        }

        public override string ToString() =>
            $"{Lower}..{Upper}";
        
    }
}
