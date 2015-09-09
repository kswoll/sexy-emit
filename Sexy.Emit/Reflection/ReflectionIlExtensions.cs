using System;

namespace Sexy.Emit.Reflection
{
    public static class ReflectionIlExtensions
    {
        public static IEmitLocal DeclareLocal(this IEmitIl il, Type type)
        {
            return il.DeclareLocal(new ReflectionType(type));
        }
    }
}
