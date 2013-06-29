using System;
using System.Diagnostics;
using System.Linq;

namespace ExperimentMemoryModel
{
    // NOTE:
    // This is just an experiment based on the C# specifications not a working code. Just learning about different possibilities....
    // Based on the article from Microsoft...

    public class MemoryExperiment
    {
        // Ordinary initilization - In this case processor and compiler may reorder fields in the Intitialize method..
        private int _data = 0;
        private bool _initialized = false;

        public void Initialize()
        {
            _data = 42;
            _initialized = true;
        }

        public void Print()
        {
            // Single-threaded no problem, but multitheaded app MAY (not everytime) read Print after one field was 
            // modified but not other.
            if (_initialized)
            {
                Debug.WriteLine(_data);
            }
            else
            {
                Debug.WriteLine("Not Initialized");
            }
        }

    }
}
