
namespace WGADemo.DesignPatterns.Behavioral.State
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
