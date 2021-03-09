using System;
using System.Collections.Generic;

namespace WGADemo.DesignPatterns.ObjectPool
{
    public class ObjectPool<T, TI> where T : IObjectPoolMember<TI>
    {
        private readonly Func<TI, T> newObjectConstructor;
        private readonly List<T> members = new List<T>();

        public ObjectPool(Func<TI, T> newObjectConstructor)
        {
            this.newObjectConstructor = newObjectConstructor;
        }

        public T GetObject(TI typeIdentifier)
        {
            foreach (T member in members)
            {
                if (member.InUse == false)
                {
                    if (member.TypeMatches(typeIdentifier) == true)
                    {
                        member.Clear();
                        member.MarkAsUsed();
                        return member;
                    }
                }
            }

            T newMember = newObjectConstructor(typeIdentifier);
            newMember.MarkAsUsed();

            members.Add(newMember);
            return newMember;
        }
    }
}
