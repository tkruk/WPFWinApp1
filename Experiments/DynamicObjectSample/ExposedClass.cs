﻿using System;
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


        public ExposedClass(Type type)
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
            return true;
        }

        public static dynamic Create(Type type)
        {
            return new ExposedClass(type);
        }
    }
}
