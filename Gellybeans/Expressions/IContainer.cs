
namespace Gellybeans
{
    public interface IContainer
    {
        dynamic this[int index] { get; set; }
        public dynamic[] Values { get; set; }
    }
}
