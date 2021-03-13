
using System;
using System.Collections.Generic;

namespace WGADemo.DesignPatterns.Creational.Multiton
{
    public class ObjectPool<T, TI> where T : IObjectPoolMember
    {
        private readonly Func<TI, T> newObjectConstructor;
        private readonly Multiton<List<T>, TI> poolsMultitone = new Multiton<List<T>, TI>(() => { return new List<T>(); });

        public ObjectPool(Func<TI, T> newObjectConstructor)
        {
            this.newObjectConstructor = newObjectConstructor;
        }

        public T GetObject(TI typeIdentifier)
        {
            List<T> members = poolsMultitone.GetMember(typeIdentifier);

            foreach (T member in members)
            {
                if (member.InUse == false)
                {
                    member.Clear();
                    member.MarkAsUsed();
                    return member;
                }
            }

            T newMember = newObjectConstructor(typeIdentifier);
            newMember.MarkAsUsed();

            members.Add(newMember);
            return newMember;
        }
    }
}
