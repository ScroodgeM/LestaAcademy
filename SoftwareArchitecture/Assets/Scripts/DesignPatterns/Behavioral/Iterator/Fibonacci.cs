using System.Collections.Generic;

namespace WGADemo.DesignPatterns.Behavioral.Iterator
{
    public static class FibonacciNumbers
    {
        public static IEnumerator<long> GetNumbers(int count)
        {
            if (count <= 0)
            {
                yield break;
            }

            yield return 1;
            count--;

            long prev2 = 0;
            long prev1 = 1;

            while (count > 0)
            {
                long next = prev2 + prev1;

                prev2 = prev1;
                prev1 = next;

                yield return next;
                count--;
            }
        }
    }
}
