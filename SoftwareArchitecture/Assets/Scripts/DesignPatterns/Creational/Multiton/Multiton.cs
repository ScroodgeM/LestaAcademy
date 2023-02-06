using System;
using System.Collections.Generic;

namespace LestaAcademyDemo.DesignPatterns.Creational.Multiton
{
    public class Multiton<T, TI>
    {
        private Func<T> newMemberCreator;
        private readonly Dictionary<TI, T> members = new Dictionary<TI, T>();

        public Multiton(Func<T> newMemberCreator)
        {
            this.newMemberCreator = newMemberCreator;
        }

        public T GetMember(TI id)
        {
            if (members.TryGetValue(id, out T member) == true)
            {
                return member;
            }

            member = newMemberCreator();
            members.Add(id, member);
            return member;
        }
    }
}
