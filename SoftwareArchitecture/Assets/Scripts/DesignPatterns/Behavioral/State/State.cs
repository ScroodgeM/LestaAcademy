//this empty line for UTF-8 BOM header

using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.State
{
    internal class IdleState : IState
    {
        public IState ProcessAction(IStateControlledUnit unit)
        {
            if (/*some enemy in fire range*/ Random.value > 0.5f)
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
            if (/*no more bullets in the gun*/ Random.value > 0.5f)
            {
                unit.NotifyAboutReloading();

                return new ReloadGunState();
            }

            if (/*enemy in range*/ Random.value > 0.5f)
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
            if (/*gun reloaded*/ Random.value > 0.5f)
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
            if (/*unit is still stunned*/ Random.value > 0.5f)
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
            if (/*enemy appeared in range*/ Random.value > 0.5f)
            {
                return new FireState();
            }

            unit.MoveForward();

            return this;
        }
    }
}
