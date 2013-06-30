using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Experiments.DynamicObjectSample
{
    public class ExposedClass : DynamicObject
    {
        private Type _type;
        private Dictionary<string, Dictionary<int, List<MethodInfo>>> _staticMethods;
        private Dictionary<string, Dictionary<int, List<MethodInfo>>> _genericStaticMethods;

        public static dynamic Create(Type type)
        {
            return new ExposedClass(type);
        }

        private ExposedClass(Type type)
        {
            _type = type;

            _staticMethods = _type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
                .Where(m => !m.IsGenericMethod)
                .GroupBy(m => m.Name)
                .ToDictionary(p => p.Key, p => p.GroupBy(r => r.GetParameters().Length).ToDictionary(r => r.Key, r => r.ToList()));

            _genericStaticMethods = _type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
                    .Where(m => m.IsGenericMethod)
                    .GroupBy(m => m.Name)
                    .ToDictionary(p => p.Key, p => p.GroupBy(r => r.GetParameters().Length).ToDictionary(r => r.Key, r => r.ToList()));
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = null;

            Type[] typeArgs = ExposedHelper.GetTypeArgs(binder);

            if (typeArgs != null && typeArgs.Length == 0) typeArgs = null;

            if (typeArgs == null && _staticMethods.ContainsKey(binder.Name)
                && _staticMethods[binder.Name].ContainsKey(args.Length)
                && ExposedHelper.InvokeBestMethod(args, null, _staticMethods[binder.Name][args.Length], out result))
            {
                return true;
            }

            if (_staticMethods.ContainsKey(binder.Name)
                    && _staticMethods[binder.Name].ContainsKey(args.Length))
            {
                List<MethodInfo> methods = new List<MethodInfo>();

                foreach (var method in _genericStaticMethods[binder.Name][args.Length])
                {
                    if (method.GetGenericArguments().Length == typeArgs.Length)
                    {
                        methods.Add(method.MakeGenericMethod(typeArgs));
                    }
                }

                if (ExposedHelper.InvokeBestMethod(args, null, methods, out result))
                {
                    return true;
                }
            }

            return true;
        }
    }
}
