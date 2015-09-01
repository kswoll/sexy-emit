using System;
using System.Linq;

namespace Sexy.Emit.Reflection
{
    public static class ReflectionTypeBuilderExtensions
    {
        public static IEmitFieldBuilder DefineField(this IEmitTypeBuilder typeBuilder, string name, Type type)
        {
            return typeBuilder.DefineField(name, new ReflectionType(type));
        }

        public static IEmitMethodBuilder DefineMethod(this IEmitTypeBuilder typeBuilder, string name, Type returnType, 
            EmitVisibility visibility = EmitVisibility.Public, bool isAbstract = false, bool isSealed = false, 
            bool isVirtual = false, bool isOverride = false, bool isExtern = false, params Type[] parameterTypes)
        {
            return typeBuilder.DefineMethod(name, new ReflectionType(returnType), visibility, isAbstract, isSealed,
                isVirtual, isOverride, isExtern, parameterTypes.Select(x => new ReflectionType(x)).ToArray());
        }
    }
}
