
namespace WGADemo.DesignPatterns.Behavioral.State
{
    public class UnitBehaviour
    {
        private interface IState
        {
            IState ProcessAction(UnitBehaviour unitBehaviour);
        }

        private IState currentState = new IdleState();

        private class IdleState : IState
        {
            public IState ProcessAction(UnitBehaviour unitBehaviour)
            {
                if (/*some enemy in fire range*/ true)
                {
                    return new FireState();
                }
                else
                {
                    return new MovingState();
                }
            }
        }

        private class FireState : IState
        {
            public IState ProcessAction(UnitBehaviour unitBehaviour)
            {
                if (/*no more bullets in the gun*/true)
                {
                    unitBehaviour.NotifyAboutReloading();

                    return new ReloadGunState();
                }

                if (/*enemy in range*/true)
                {
                    unitBehaviour.FireToEnemy();
                    unitBehaviour.WithdrawOneBullerFromGun();

                    return this;
                }

                // no enemies in range, move further
                return new MovingState();
            }
        }

        private class ReloadGunState : IState
        {
            public IState ProcessAction(UnitBehaviour unitBehaviour)
            {
                if (/*gun reloaded*/true)
                {
                    unitBehaviour.GunReloaded();

                    return new FireState();
                }

                // continue reloading gun [waiting for reload cooldown]
                return this;
            }
        }

        private class StunnedState : IState
        {
            public IState ProcessAction(UnitBehaviour unitBehaviour)
            {
                if (/*unit is still stunned*/true)
                {
                    // do nothing, stunning effect/animation should be playing
                    return this;
                }

                return new IdleState();
            }
        }

        private class MovingState : IState
        {
            public IState ProcessAction(UnitBehaviour unitBehaviour)
            {
                if (/*enemy appeared in range*/true)
                {
                    return new FireState();
                }

                unitBehaviour.MoveForward();

                return this;
            }
        }

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

        private void MoveForward()
        {
        }

        private void GunReloaded()
        {
        }

        private void NotifyAboutReloading()
        {
        }

        private void WithdrawOneBullerFromGun()
        {
        }

        private void FireToEnemy()
        {
        }
    }
}
