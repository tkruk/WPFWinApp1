using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Experiments.DynamicObjectSample
{
    internal class ExposedHelper
    {
        private static Type _csharpInvokePropertyType = typeof(Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
            .Assembly
            .GetType("Microsoft.CSharp.RuntimeBinder.ICSharpInvokeOrInvokeMemberBinder");

        internal static bool InvokeBestMethod(object[] args, object target, List<MethodInfo> instanceMethods, out object result)
        {
            result = null;

            if (instanceMethods.Count == 1)
            {
                if (TryInvoke(instanceMethods[0], target, args, out result))
                {
                    return true;
                }
            }
            else if (instanceMethods.Count > 1)
            {
                MethodInfo mainMethod = null;
                Type[] mainParams = null;
                Type[] actualParams = args.Select(p => p == null ? typeof(object) : p.GetType()).ToArray();

                Func<Type[], Type[], bool> isAssignableFrom = (a, b) =>
                {
                    for (int i = 0; i < a.Length; i++)
                    {
                        if (!a[i].IsAssignableFrom(b[i])) return false;
                    }

                    return true;
                };

                foreach (var method in instanceMethods.Where(m => m.GetParameters().Length == args.Length))
                {
                    Type[] memParams = method.GetParameters().Select(x => x.ParameterType).ToArray();
                    if (isAssignableFrom(memParams, actualParams))
                    {
                        if (mainMethod == null || isAssignableFrom(mainParams, memParams))
                        {
                            mainMethod = method;
                            mainParams = memParams;
                        }
                    }
                }

                if (mainMethod != null && TryInvoke(mainMethod, target, args, out result))
                {
                    return true;
                }
            }

            return false;
        }

        internal static bool TryInvoke(MethodInfo methodInfo, object target, object[] args, out object result)
        {
            result = null;

            try
            {
                result = methodInfo.Invoke(target, args);
            }
            catch (TargetInvocationException tie)
            {
                Debug.WriteLine(tie.Message);
            }
            catch (TargetParameterCountException tpce)
            {
                Debug.WriteLine(tpce.Message);
            }

            return true;
        }

        internal static Type[] GetTypeArgs(InvokeMemberBinder binder)
        {
            Type[] rtnVal = null;

            if (_csharpInvokePropertyType.IsInstanceOfType(binder))
            {
                PropertyInfo propInfo = _csharpInvokePropertyType.GetProperty("TypeArguments");
                rtnVal = ((IEnumerable<Type>)propInfo.GetValue(binder, null)).ToArray();
            }

            return rtnVal;
        }
    }
}