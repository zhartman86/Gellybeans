namespace Gellybeans.Expressions
{
    public interface IMember
    {
        bool TryGetMember(string name, out dynamic value);
    }
}
