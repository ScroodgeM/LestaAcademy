
using System;

namespace WGADemo.DesignPatterns.Behavioral.Observer
{
    public class PlayerState : IPlayerState
    {
        public event Action OnMoneyUpdated = () => { };

        private ulong currentMoney;

        public ulong GetMoney()
        {
            return currentMoney;
        }

        private void SetNewMoney(ulong newMoney)
        {
            this.currentMoney = newMoney;

            OnMoneyUpdated();
        }
    }
}
