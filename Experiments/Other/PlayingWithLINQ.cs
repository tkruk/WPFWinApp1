using System;
using System.Collections.Generic;
using System.Linq;

namespace Experiments.Other
{
    public class PlayingWithLINQ
    {
        public Dictionary<int, int[]> ArrayInit(int count)
        {
            Dictionary<int, int[]> rtnvValue = new Dictionary<int, int[]>();

            // Init array without loops.
            int[] a = Enumerable.Repeat(-1, count).ToArray();
            int[] b = Enumerable.Range(0, count).ToArray();
            int[] c = Enumerable.Range(0, count).Select(i => 100 + 10 * i).ToArray();

            rtnvValue.Add(1, a);
            rtnvValue.Add(2, b);
            rtnvValue.Add(3, c);

            return rtnvValue;
        }

        public int LoopMultiArraysInOneLoop(int count)
        {
            var rtnVal = ArrayInit(count);
            int keepingCount = 0;
            
            // Loop through all arrays without multiple FOR loops
            // NOTE: Interesting fact about Concat operator is that LINQ will not allocate a new array to hold
            // elements for all arrays, so this is also memory efficient.
            foreach (var d in rtnVal[1].Concat(rtnVal[2]).Concat(rtnVal[3]))
            {
                keepingCount++;
                Console.WriteLine("Array value now is: " + d);
            }

            return keepingCount;
        }

        public int LoopMultiArraysInOneLoopOnlyUniqueValues(int count)
        {
            var rtnVal = ArrayInit(count);

            int keepingCount = 0;
            // Loop through all arrays without multiple FOR loops - access only unique values.
            foreach (var d in rtnVal[1].Union(rtnVal[2]).Union(rtnVal[3]))
            {
                keepingCount++;
                Console.WriteLine("Array value now is: " + d);
            }

            return keepingCount;
        }

        public IEnumerable<int> GenerateRandomSequence(int count)
        {
            Random rand = new Random();
            var randSeq = Enumerable.Repeat(0, count).Select(i => rand.Next());

            return randSeq;
        }

        public string GenerateString(int count)
        {
            string rtnVal = new string(Enumerable.Range(0, count).Select(i => (char)('A' + i % 3)).ToArray());
            
            return rtnVal;
        }
    }
}