using System;
using System.Diagnostics;
using System.Linq;

namespace ExperimentMemoryModel
{
    public class VolatileMemoryExample
    {
        private int _a;
        private volatile int _b;
        private int _c;

        void Init()
        {
            int a = _a; // Read 1
            int b = _b; // Read 2 - volatile
            int c = _c; // Read 3
            // Valid reordering of the above example
            // Valid Reads 1    Valid Reads 2   Valid Reads 3
            // =============    =============   =============
            //      Read 1          Read 2          Read 2
            //      Read 2          Read 1          Read 3
            //      Read 3          Read 3          Read 1
        }

        // Writing example
        private int _x;
        private volatile int _y;
        private int _z;

        void Write()
        {
            _x = 1; // Write 1
            _y = 1; // Write 2
            _z = 3; // Write 3
            // Valid reordering of writes
            // Valid Write 1    Valid Write 2   Valid Write 3
            // =============    =============   =============
            //      Write 1          Write 1          Write 3
            //      Write 2          Write 3          Write 1
            //      Write 3          Write 2          Write 2
        }

        // Atomicity
        // Simple data type guranteed to be written atomically, but user-defined value type COULD be written into memory in multiple
        // atomic writes.

        private Guid _value;

        public void SetValue(Guid value)
        {
            _value = value;
        }

        public Guid GetValue()
        {
            return _value;
        }

        public void SettingGuidValue()
        {
            SetValue(Guid.NewGuid());
        }

        // Let's assume that the SetValue was (9,9,9,9) this means that GetValue can return the following possibilities
        // (0,0,9) or (0,0,9,9) etc... The reason behind this is that this operation can use multiple writes to update the values.
        // The main reason for this possibility is that _value = value does not execute atomically at the hardware level.
        // Note the above scenario can be controlled by the compiler. This means that some compiler optimizations can introduce or eliminate
        // certain memory operation.

        // The purpose of memory model is to enable thread communication. So the easiet way to share data among thread is locking...
        // Example of locking
        private int _t = 0;
        private int _k = 0;
        private object _lock = new object();

        public void Set()
        {
            lock (_lock)
            {
                _t = 1;
                _k = 1;
            }
        }

        public void Print()
        {
            lock (_lock)
            {
                int t = _t;
                int k = _k;

                Debug.WriteLine(string.Format("{0} {1}", t, k));
            }
        }

        // So, locking ensures that Set() and Print() execute atomically to each other and guarantees same sequential order for both methods
        // even if they are executed from the differet threads.

        // As a result of the above possibilities that is why we use lock and volatile when creating a singleton object in c#
    }
}