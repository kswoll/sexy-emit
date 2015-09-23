using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sexy.Emit.Utils
{
    internal static class TypeExtensions
    {
        public static IEnumerable<Type> GetImplementedInterfaces(this Type type)
        {
            if (type.BaseType == null)
                return type.GetInterfaces();
            else
                return type.GetInterfaces().Except(type.BaseType.GetInterfaces());
        }

        public static MethodInfo FindMethod(this Type type, string name, Type[] parameterTypes)
        {
            return type.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, parameterTypes, null);
        }
    }
}
