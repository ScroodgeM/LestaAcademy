//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Creational.ObjectPool
{
    public interface IObjectPoolMember<TI>
    {
        bool TypeMatches(TI type);
        bool InUse { get; }
        void MarkAsUsed();
        void Clear();
    }
}
