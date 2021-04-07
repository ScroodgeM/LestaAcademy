
namespace WGADemo.DesignPatterns.Creational.ObjectPool.Units
{
    public abstract class Unit : IUnit
    {
        private bool inUse;

        public bool InUse => inUse;

        public void MarkAsUsed()
        {
            inUse = true;
        }

        public abstract bool TypeMatches(UnitType type);

        public void Move()
        {
            // apply movements here
        }

        public void UseSkill()
        {
            // use skill here
        }

        public void Clear()
        {
            // clear all lifetime effects here and prepare for reusage
        }

        public void Kill()
        {
            inUse = false;
        }
    }
}
