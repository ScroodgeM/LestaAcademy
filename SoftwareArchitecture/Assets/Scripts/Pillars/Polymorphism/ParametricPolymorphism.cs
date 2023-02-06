using System;
using System.Collections.Generic;

namespace LestaAcademyDemo.Pillars.Polymorphism
{
    public class MyData : IComparable<MyData>
    {
        public int CompareTo(MyData other)
        {
            return 0;
        }

        public override string ToString()
        {
            return "bla";
        }
    }

    public class ParametricPolymorphism
    {
        private static readonly DateTime wgAcademyDeadLine = new DateTime(2023, 06, 15, 18, 0, 0);

        public void DoSomeJob()
        {
            List<int> values1 = new List<int>()
            {
                10,
                42,
            };

            List<string> values2 = new List<string>()
            {
                "WG",
                "Academy",
            };

            List<DateTime> values3 = new List<DateTime>()
            {
                DateTime.Now,
                wgAcademyDeadLine,
            };

            List<MyData> values4 = new List<MyData>()
            {
                new MyData(),
                new MyData(),
            };

            SortAndPrint(values1);
            SortAndPrint(values2);
            SortAndPrint(values3);
            SortAndPrint(values4);
        }

        private void SortAndPrint<T>(List<T> input) where T : IComparable<T>
        {
            input.Sort();

            foreach (T value in input)
            {
                Console.WriteLine(value.ToString());
            }
        }
    }
}
