
namespace WGADemo.DesignPatterns.ObjectPool.Units
{
    public class Soldier : Unit
    {
        public override bool TypeMatches(UnitType type)
        {
            return type == UnitType.Soldier;
        }
    }
}
