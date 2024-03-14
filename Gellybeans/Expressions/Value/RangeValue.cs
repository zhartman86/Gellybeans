namespace Gellybeans.Expressions
{
    public class RangeValue
    {
        public int Lower { get; }
        public int Upper { get; }

        //pick one at random
        public bool OneRandom { get; }
        
        
        public RangeValue(int lower, int upper, bool oneRandom = false)
        {
            Lower = lower;
            Upper = upper;
            OneRandom = oneRandom;
        }

        public override string ToString() =>
            $"{Lower}..{Upper}";
        
    }
}
