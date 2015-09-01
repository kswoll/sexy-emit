using System.Reflection;

namespace Sexy.Emit.Reflection
{
    public static class ReflectionFieldAttributes
    {
        public static FieldAttributes ToFieldAttributes(EmitVisibility visibility, bool isStatic, bool isReadonly,
            bool isVolatile)
        {
            FieldAttributes result = 0;

            switch (visibility)
            {
                case EmitVisibility.Public:
                    result |= FieldAttributes.Public;
                    break;
                case EmitVisibility.Protected:
                    result |= FieldAttributes.Family;
                    break;
                case EmitVisibility.Private:
                    result |= FieldAttributes.Private;
                    break;
                case EmitVisibility.Internal:
                    result |= FieldAttributes.Assembly;
                    break;
                case EmitVisibility.ProtectedInternal:
                    result |= FieldAttributes.FamORAssem;
                    break;
            }

            if (isReadonly)
                result |= FieldAttributes.InitOnly;
            if (isStatic)
                result |= FieldAttributes.Static;

            return result;
        }
    }
}
