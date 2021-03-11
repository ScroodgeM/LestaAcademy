using System;
using System.Collections.Generic;

namespace WGADemo.DesignPatterns.Multiton
{
    public class Multiton<T, TI>
    {
        private Func<T> newMemberCreator;
        private readonly Dictionary<TI, T> membersGrouped = new Dictionary<TI, T>();

        public Multiton(Func<T> newMemberCreator)
        {
            this.newMemberCreator = newMemberCreator;
        }

        public T GetMember(TI id)
        {
            if (membersGrouped.TryGetValue(id, out T members) == true)
            {
                return members;
            }

            members = newMemberCreator();
            membersGrouped.Add(id, members);
            return members;
        }
    }
}
