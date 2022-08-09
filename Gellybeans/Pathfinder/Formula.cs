using System.Data;

namespace Gellybeans.Pathfinder
{
    public class Formula
    {
        public string       Name    { get; set; } = "FormulaNameMe";
        public string       Expr    { get; private set; } = "";
        public DataTable    Table   { get; private set; } = new DataTable();
        
        
        public int Value   
        { 
            get 
            {
                var v = Table.Compute("Sum(TOTAL)", "");
                return DBNull.Value.Equals(v) ? 
                    (int)v: 
                    0; 
            } 
        }             

        public static implicit operator int(Formula f) { return f.Value; }

        
        public static int operator +(Formula a, Formula b) { return a + b; }
        public static int operator -(Formula a, Formula b) { return a - b; }

        public Formula(string formula)
        {
            Expr = formula;
            
            Table.Columns.Add
                (
                    new DataColumn("TOTAL")
                    {
                        DataType = typeof(int),
                        Expression = formula
                    }
                );
        }
        
        public void AddVar(string varName, Stat stat)
        {
            Table.Columns.Add
                (
                    new DataColumn()
                    {
                        DataType        = typeof(int),
                        ColumnName      = varName,
                        DefaultValue    = stat
                    }
                );
        }
    
    
    }
}
