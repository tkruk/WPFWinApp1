using System;
using System.Collections.Generic;
using System.Linq;

namespace Experiments.Other
{
    public static class ExtensionMethods
    {
        public static IEnumerable<int> intTo(this int initialValue, int maxValue)
        {
            for (var i = initialValue; i <= maxValue; i++)
            {
                yield return i;
            }
        }

        public static void DisplayAll<T>(this IEnumerable<T> values)
        {
            values.ToList().ForEach(value => Console.WriteLine("{0}", value));

            Console.WriteLine();
        }

        public static IEnumerable<int> FibonacciSequence
        {
            get
            {
                int a = 0; // seed value F0
                int b = 1; // seed value F1
                int t = 0;

                yield return a;
                yield return b;

                while (true)
                {
                    t = a + b;
                    a = b;
                    b = t;

                    yield return t;
                }
            }
        }
    }
}
