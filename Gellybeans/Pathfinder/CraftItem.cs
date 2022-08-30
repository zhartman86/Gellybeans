using System.Text;

namespace Gellybeans.Pathfinder
{
    public class CraftItem
    {
        public string   Item        { get; set; }
        public int      Difficulty  { get; set; }
        public int      Price       { get; set; }

        public int      Progress    { get; set; } = 0;

        public void Craft(int result, StringBuilder sb)
        {
            if(result >= Difficulty)
            {
                sb.AppendLine("Craft succeeded!");
                Progress += result * Difficulty;
                if(Progress >= Price)
                {
                    if(Progress > Price * 2)
                    {
                        sb.AppendLine($"Completed {Item} at x{Progress / Price} time! :D");
                        return;
                    }
                    sb.AppendLine($"{Item} completed! :)");
                }
                else
                {
                    sb.AppendLine($"You've made {Progress} of {Price} toward a completed {Item}.");
                }
            }
            else if(Difficulty - result < 5)
            {
                sb.AppendLine($"You've made no progress toward {Item} this week :(");
            }
            else sb.AppendLine("Oh no! You've failed and destroyed half of the materials in the process!");
        }      
    }
}
