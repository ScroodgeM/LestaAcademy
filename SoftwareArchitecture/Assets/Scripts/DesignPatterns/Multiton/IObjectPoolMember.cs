﻿
namespace WGADemo.DesignPatterns.Multiton
{
    public interface IObjectPoolMember
    {
        bool InUse { get; }
        void MarkAsUsed();
        void Clear();
    }
}
