using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ExperimentMemoryModel
{
    // Publication via threading API pattern in concurrent programming.
    public class SamplePublicationviaThreadingApi
    {
        static int s_value;

        static void Run()
        {
            s_value = 42;
            Task.Factory.StartNew(() =>
            {
                Debug.WriteLine(s_value);
            });
        }
    }
}