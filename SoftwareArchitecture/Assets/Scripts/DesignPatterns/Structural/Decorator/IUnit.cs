//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Structural.Decorator
{
    public interface IUnit
    {
        void Damage(int damagePoints);
        void Push(int forcePoint);
        void Kill();
    }
}
