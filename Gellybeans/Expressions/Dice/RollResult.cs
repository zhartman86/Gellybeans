namespace Gellybeans.Expressions.Dice
{
    public class RollResult
    {
        private List<int[]> diceResultGroups = new List<int[]>();

        public int DiceTotal { get; set; } = 0;


        public void Add(int[] diceResultGroup) 
        { 
            diceResultGroups.Add(diceResultGroup);
            for(int i = 0; i < diceResultGroup.Length; i++)
            {
                DiceTotal += diceResultGroup[i];
            }
        }     
            
        public override string ToString()
        {
            string s = "";
            for(int i = 0; i < diceResultGroups.Count; i++)
            {
                s += "  (";
                for(var j = 0; j < diceResultGroups[i].Length; j++)
                {
                    s += "[" + diceResultGroups[i][j].ToString() + "]";
                }
                s += ")  ";
            }
            return s;
        }
    }
}
