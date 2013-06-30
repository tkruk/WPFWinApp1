using System;
using System.Linq;
using System.Threading;

namespace ExperimentMemoryModel
{
    // Explanation: Intrelocked operations are atomic operations that can reduce locking in multithreaded applications 
    public class InterlockedOperExample
    {
        private int _value = int.MinValue;
        private object _lock = new object();

        public int IncrementValue()
        {
            lock (_lock)
            {
                _value++;
                
                return _value;
            }
        }

        // Using Intelocked.Increment we can rewrite above method
        // NOTE: Interesting fact about Interlocked methods is that they cannot be reordered with other memory operations.
        public int IncrementValuePartTwo()
        {
            return Interlocked.Increment(ref _value); // this approach may run faster then above example
        }
    }
}