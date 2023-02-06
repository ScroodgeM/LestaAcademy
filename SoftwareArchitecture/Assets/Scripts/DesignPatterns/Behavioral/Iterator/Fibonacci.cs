//this empty line for UTF-8 BOM header
using System.Collections.Generic;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Iterator
{
    public static class FibonacciNumbers
    {
        public static IEnumerator<long> GetNumbers(int count)
        {
            if (count > 0)
            {
                yield return 0;
                count--;
            }

            if (count > 0)
            {
                yield return 1;
                count--;
            }

            long prev2 = 0;
            long prev1 = 1;

            while (count > 0)
            {
                long next = prev2 + prev1;

                yield return next;
                count--;

                prev2 = prev1;
                prev1 = next;
            }
        }
    }
}
