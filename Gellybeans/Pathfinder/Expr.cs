namespace Gellybeans.Pathfinder
{
    public class Expr
    {
        public string Name { get; init; }
        public string Expression { get; init; }

        public Expr() { }
        public Expr(string name, string expression)
        {
            Name = name;
            Expression = expression;
        }
    }
}
