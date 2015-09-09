using System;
using System.Linq;

namespace Sexy.Emit.Reflection
{
    public static class ReflectionTypeBuilderExtensions
    {
        public static void SetBaseType(this IEmitTypeBuilder typeBuilder, Type baseType)
        {
            typeBuilder.SetBaseType(new ReflectionType(baseType));
        }

        public static IEmitFieldBuilder DefineField(this IEmitTypeBuilder typeBuilder, string name, Type type, 
            EmitVisibility visibility = EmitVisibility.Public, bool isStatic = false, bool isReadonly = false, 
            bool isVolatile = false)
        {
            return typeBuilder.DefineField(name, new ReflectionType(type), visibility, isStatic, isReadonly, isVolatile);
        }

        public static IEmitMethodBuilder DefineMethod(this IEmitTypeBuilder typeBuilder, string name, Type returnType, 
            EmitVisibility visibility = EmitVisibility.Public, bool isAbstract = false, bool isSealed = false, 
            bool isVirtual = false, bool isOverride = false, bool isExtern = false, bool isStatic = false, 
            params Type[] parameterTypes)
        {
            return typeBuilder.DefineMethod(name, new ReflectionType(returnType), visibility, isAbstract, isSealed,
                isVirtual, isOverride, isExtern, isStatic, parameterTypes.Select(x => new ReflectionType(x)).ToArray());
        }
    }
}
