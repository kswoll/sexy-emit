using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}
