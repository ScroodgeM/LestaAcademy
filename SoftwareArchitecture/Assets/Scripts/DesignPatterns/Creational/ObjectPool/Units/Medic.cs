
namespace LestaAcademyDemo.DesignPatterns.Creational.ObjectPool.Units
{
    public class Medic : Unit
    {
        public override bool TypeMatches(UnitType type)
        {
            return type == UnitType.Medic;
        }
    }
}
