using System;

namespace Sexy.Emit
{
    public enum EmitTypeKind
    {
        Class, Interface, Struct, Enum, Array
    }

    public static class EmitTypeKinds
    {
        public static EmitTypeKind ToTypeKind(this Type type)
        {
            if (type.IsValueType)
                return EmitTypeKind.Struct;
            if (type.IsInterface)
                return EmitTypeKind.Interface;
            if (type.IsEnum)
                return EmitTypeKind.Enum;
            if (type.IsArray)
                return EmitTypeKind.Array;
            if (type.IsClass)
                return EmitTypeKind.Class;

            throw new Exception();
        }
    }
}
