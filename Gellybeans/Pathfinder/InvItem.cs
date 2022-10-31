using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gellybeans.Pathfinder
{
    public class InvItem
    {
        //identifier for any item added from the item database
        public string Base { get; set; } = "";

        public string   Name        { get; set; } = "Name Me";
        public decimal  Value       { get; set; } = 0;
        public decimal  Weight      { get; set; } = 0;
        public int      Quantity    { get; set; } = 1;
        public string   Note        { get; set; } = "";
    }
}
