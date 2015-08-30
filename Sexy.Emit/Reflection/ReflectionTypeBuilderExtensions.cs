using System;

namespace Sexy.Emit.Reflection
{
    public static class ReflectionTypeBuilderExtensions
    {
        public static IEmitFieldBuilder DefineField(this IEmitTypeBuilder typeBuilder, string name, Type type)
        {
            return typeBuilder.DefineField(name, new ReflectionType(type));
        }
    }
}
