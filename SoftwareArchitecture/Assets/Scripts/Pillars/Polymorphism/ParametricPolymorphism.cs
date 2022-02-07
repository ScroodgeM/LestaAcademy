
using System;
using System.Collections.Generic;

namespace WGADemo
{
    public class ParametricPolymorphism
    {
        private static readonly DateTime wgAcademyDeadLine = new DateTime(2022, 06, 15, 12, 0, 0);

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

            SortAndPrint(values1);
            SortAndPrint(values2);
            SortAndPrint(values3);
        }

        private void SortAndPrint<T>(List<T> input)
        {
            input.Sort();

            foreach (T value in input)
            {
                Console.WriteLine(value.ToString());
            }
        }
    }
}
