
namespace LestaAcademyDemo.DesignPatterns.Behavioral.State
{
    internal interface IState
    {
        IState ProcessAction(IStateControlledUnit unit);
    }

    internal class IdleState : IState
    {
        public IState ProcessAction(IStateControlledUnit unit)
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

    internal class FireState : IState
    {
        public IState ProcessAction(IStateControlledUnit unit)
        {
            if (/*no more bullets in the gun*/true)
            {
                unit.NotifyAboutReloading();

                return new ReloadGunState();
            }

            if (/*enemy in range*/true)
            {
                unit.FireToEnemy();
                unit.WithdrawOneBulletFromGun();

                return this;
            }

            // no enemies in range, move further
            return new MovingState();
        }
    }

    internal class ReloadGunState : IState
    {
        public IState ProcessAction(IStateControlledUnit unit)
        {
            if (/*gun reloaded*/true)
            {
                unit.GunReloaded();

                return new FireState();
            }

            // continue reloading gun [waiting for reload cooldown]
            return this;
        }
    }

    internal class StunnedState : IState
    {
        public IState ProcessAction(IStateControlledUnit unit)
        {
            if (/*unit is still stunned*/true)
            {
                // do nothing, stunning effect/animation should be playing
                return this;
            }

            return new IdleState();
        }
    }

    internal class MovingState : IState
    {
        public IState ProcessAction(IStateControlledUnit unit)
        {
            if (/*enemy appeared in range*/true)
            {
                return new FireState();
            }

            unit.MoveForward();

            return this;
        }
    }
}
