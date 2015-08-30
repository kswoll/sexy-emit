using System.Reflection;

namespace Sexy.Emit.Reflection
{
    public static class ReflectionEmitTypeAttributes
    {
        public static TypeAttributes ToTypeAttributes(this EmitTypeAttributes typeAttributes, bool isNested)
        {
            TypeAttributes result = 0;
            if ((typeAttributes & EmitTypeAttributes.Class) == EmitTypeAttributes.Class)
                result |= TypeAttributes.Class;
            if ((typeAttributes & EmitTypeAttributes.Interface) == EmitTypeAttributes.Interface)
                result |= TypeAttributes.Interface;
            if ((typeAttributes & EmitTypeAttributes.Abstract) == EmitTypeAttributes.Abstract)
                result |= TypeAttributes.Abstract;
            if ((typeAttributes & EmitTypeAttributes.Public) == EmitTypeAttributes.Public)
                result |= isNested ? TypeAttributes.NestedPublic : TypeAttributes.Public;
            if ((typeAttributes & EmitTypeAttributes.Protected) == EmitTypeAttributes.Protected && 
                (typeAttributes & EmitTypeAttributes.Internal) == EmitTypeAttributes.Internal)
                result |= TypeAttributes.NestedFamANDAssem;
            if ((typeAttributes & EmitTypeAttributes.Protected) == EmitTypeAttributes.Protected)
                result |= TypeAttributes.NestedFamily;
            if ((typeAttributes & EmitTypeAttributes.Internal) == EmitTypeAttributes.Internal)
                result |= isNested ? TypeAttributes.NestedAssembly : TypeAttributes.NotPublic;
            if ((typeAttributes & EmitTypeAttributes.Private) == EmitTypeAttributes.Private)
                result |= TypeAttributes.NestedPrivate;

            return result;
        }
    }
}
