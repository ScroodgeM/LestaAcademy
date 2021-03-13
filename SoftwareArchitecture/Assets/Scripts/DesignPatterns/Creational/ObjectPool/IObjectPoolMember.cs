
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGADemo.DesignPatterns.Creational.ObjectPool
{
    public interface IObjectPoolMember<TI>
    {
        bool TypeMatches(TI type);
        bool InUse { get; }
        void MarkAsUsed();
        void Clear();
    }
}
