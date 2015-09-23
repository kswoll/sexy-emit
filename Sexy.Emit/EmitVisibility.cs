using System;
using System.Reflection;

namespace Sexy.Emit
{
    public enum EmitVisibility
    {
        Private,
        Protected,
        Internal,
        ProtectedInternal,
        Public
    }

    public static class EmitVisibilities
    {
        public static EmitVisibility ToVisibility(this Type type)
        {
            var attributes = type.Attributes;
            if ((attributes & (TypeAttributes.NestedPrivate)) == TypeAttributes.NestedPrivate)
                return EmitVisibility.Private;
            if ((attributes & (TypeAttributes.Public)) == TypeAttributes.Public || (attributes & (TypeAttributes.NestedPublic)) == TypeAttributes.NestedPublic)
                return EmitVisibility.Public;
            if ((attributes & (TypeAttributes.NestedFamORAssem)) == TypeAttributes.NestedFamORAssem)
                return EmitVisibility.ProtectedInternal;
            if ((attributes & (TypeAttributes.NotPublic)) == TypeAttributes.NotPublic || (attributes & (TypeAttributes.NestedAssembly)) == TypeAttributes.NestedPublic)
                return EmitVisibility.Internal;
            if ((attributes & (TypeAttributes.NestedFamily)) == TypeAttributes.NestedFamily)
                return EmitVisibility.Protected;

            throw new Exception();
        }

        public static EmitVisibility ToVisibility(this PropertyInfo property)
        {
            var attributes = property.Attributes;
            var methodAttributes = (property.GetMethod ?? property.SetMethod).Attributes;

            throw new Exception();
        }

        public static EmitVisibility ToVisibility(this MethodInfo method)
        {
            var attributes = method.Attributes;

            throw new Exception();
        }

        public static EmitVisibility ToVisibility(this ConstructorInfo constructor)
        {
            var attributes = constructor.Attributes;

            throw new Exception();
        }

        public static EmitVisibility ToVisibility(this FieldInfo field)
        {
            var attributes = field.Attributes;

            throw new Exception();
        }


    }
}
