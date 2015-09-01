using System.Reflection;

namespace Sexy.Emit.Reflection
{
    public static class ReflectionTypeAttributes
    {
        public static TypeAttributes ToTypeAttributes(EmitTypeKind kind, EmitVisibility visibility, bool isNested, bool isAbstract, bool isSealed)
        {
            TypeAttributes result = 0;
            switch (kind)
            {
                case EmitTypeKind.Class:
                    result |= TypeAttributes.Class;
                    break;
                case EmitTypeKind.Interface:
                    result |= TypeAttributes.Interface;
                    break;
//                case EmitTypeKind.Struct:
//                    result |= 
            }
            switch (visibility)
            {
                case EmitVisibility.Public:
                    result |= isNested ? TypeAttributes.NestedPublic : TypeAttributes.Public;
                    break;
                case EmitVisibility.Internal:
                    result |= isNested ? TypeAttributes.NestedAssembly : TypeAttributes.NotPublic;
                    break;
                case EmitVisibility.Private:
                    result |= TypeAttributes.NestedPrivate;
                    break;
                case EmitVisibility.Protected:
                    result |= TypeAttributes.NestedFamily;
                    break;
                case EmitVisibility.ProtectedInternal:
                    result |= TypeAttributes.NestedFamORAssem;
                    break;
            }
            if (isAbstract)
                result |= TypeAttributes.Abstract;
            if (isSealed)
                result |= TypeAttributes.Sealed;

            return result;
        }
    }
}
