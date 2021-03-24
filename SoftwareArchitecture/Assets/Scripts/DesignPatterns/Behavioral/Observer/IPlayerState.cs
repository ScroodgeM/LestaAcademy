
using System;

namespace WGADemo.DesignPatterns.Behavioral.Observer
{
    public interface IPlayerState
    {
        event Action OnMoneyUpdated;
        ulong GetMoney();
    }
}
