
namespace WGADemo.DesignPatterns.Creational.ObjectPool
{
    public interface IObjectPoolMember<TI>
    {
        bool TypeMatches(TI type);
        bool InUse { get; }
        void MarkAsUsed();
        void Clear();
    }
}
