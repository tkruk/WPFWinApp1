using System;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Experiments.DynamicObjectSample
{
    public class ExposedObjectSimple : DynamicObject
    {
        private object m_object;
        
        public ExposedObjectSimple(object obj)
        {
            m_object = obj;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            Debug.WriteLine("Name of the called method: " + binder.Name);

            var methodInfo = m_object.GetType().GetMethod(binder.Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            // Call method
            result = methodInfo.Invoke(m_object, args);

            return true;
        }
    }
}
