namespace Gellybeans.Expressions
{
    public interface IContext
    {
        int Resolve(string identifier);
        int Call(string methodName, int[] args);
    }
}
