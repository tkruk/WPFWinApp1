using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Experiments.DynamicObjectSample
{
    public class AccessObjectInternals
    {
        List<int> sampleList = new List<int>();

        public bool Experiment1()
        {
            bool rtnVal = true;
            try
            {
                dynamic exposedList = new ExposedObjectSimple(sampleList);

                exposedList.EnsureCapacity(20);
            }
            catch (Exception ex)
            {
                rtnVal = false;
                Debug.WriteLine("Error: " + ex.Message);
            }

            return rtnVal;
        }

        public bool Experiment2()
        {
            bool rtnVal = true;

            try
            {
                dynamic exposedFileObj = new ExposedObjectSimple(typeof(System.IO.File));

                var fileExists = exposedFileObj.InternalExists("somefile.txt");
            }
            catch (Exception ex)
            {
                rtnVal = false;
                Debug.WriteLine("Error: " + ex.Message);
            }

            return rtnVal;
        }
    }
}
