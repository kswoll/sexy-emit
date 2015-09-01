using System.Reflection;

namespace Sexy.Emit.Reflection
{
    public static class ReflectionMethodAttributes
    {
        public static MethodAttributes ToMethodAttributes(EmitVisibility visibility, bool isAbstract, bool isSealed,
            bool isVirtual, bool isExtern)
        {
            MethodAttributes result = 0;

            switch (visibility)
            {
                case EmitVisibility.Public:
                    result |= MethodAttributes.Public;
                    break;
                case EmitVisibility.Internal:
                    result |= MethodAttributes.Assembly;
                    break;
                case EmitVisibility.Private:
                    result |= MethodAttributes.Private;
                    break;
                case EmitVisibility.Protected:
                    result |= MethodAttributes.Family;
                    break;
                case EmitVisibility.ProtectedInternal:
                    result |= MethodAttributes.FamORAssem;
                    break;
            }

            if (isAbstract)
                result |= MethodAttributes.Abstract;
            if (isVirtual)
                result |= MethodAttributes.Virtual;

            return result;
        }
    }
}
