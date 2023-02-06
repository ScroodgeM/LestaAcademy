//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Behavioral.State
{
    public interface IStateControlledUnit
    {
        void MoveForward();
        void FireToEnemy();
        void WithdrawOneBulletFromGun();
        void NotifyAboutReloading();
        void GunReloaded();
    }
}
