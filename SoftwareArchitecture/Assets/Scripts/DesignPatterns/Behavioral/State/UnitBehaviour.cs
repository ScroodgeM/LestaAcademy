
namespace WGADemo.DesignPatterns.Behavioral.State
{
    public class UnitBehaviour : IStateControlledUnit
    {
        private IState currentState = new IdleState();

        // external action, can be called by some timer, for example
        public void ProcessAction()
        {
            currentState = currentState.ProcessAction(this);
        }

        // external action, can be triggered by some stunning weapon like rocket or grenade
        public void Stun()
        {
            currentState = new StunnedState();
        }

        public void MoveForward()
        {
        }

        public void GunReloaded()
        {
        }

        public void NotifyAboutReloading()
        {
        }

        public void WithdrawOneBulletFromGun()
        {
        }

        public void FireToEnemy()
        {
        }
    }
}
