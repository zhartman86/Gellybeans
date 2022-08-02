using System;
using System.Collections.Generic;
using System.Text;

namespace Gellybeans.Dice
{
    public class RollResult
    {
        private List<int[]> diceGroups = new List<int[]>();



        public void Add(int[] diceGroup) { diceGroups.Add(diceGroup); }        
            
        public override string ToString()
        {
            string s = "";
            for(int i = 0; i < diceGroups.Count; i++)
            {
                s += "(";
                for(var j = 0; j < diceGroups[i].Length; j++)
                {
                    s += "[" + diceGroups[i][j].ToString() + "]";
                }
                s += ")";
            }
            return s;
        }
    }
}
