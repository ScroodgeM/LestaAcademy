﻿//this empty line for UTF-8 BOM header
using System;
using System.Collections.Generic;

namespace LestaAcademyDemo.DesignPatterns.Creational.Multiton
{
    public class ObjectPool<T, TI> where T : IObjectPoolMember
    {
        private readonly Func<TI, T> newObjectConstructor;

        private readonly Multiton<List<T>, TI> poolsMultitone = new Multiton<List<T>, TI>(() => new List<T>());

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
