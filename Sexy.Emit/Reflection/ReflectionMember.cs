using System.Reflection;
using System.Reflection.Emit;

namespace Sexy.Emit.Reflection
{
    public class ReflectionMember : IEmitMember
    {
        public static ReflectionMember Create(MemberInfo member)
        {
            if (member is FieldBuilder)
                return new ReflectionFieldBuilder((FieldBuilder)member);
            if (member is FieldInfo)
                return new ReflectionField((FieldInfo)member);
            if (member is MethodBuilder)
                return new ReflectionMethodBuilder((MethodBuilder)member);
            if (member is MethodInfo)
                return new ReflectionMethod((MethodInfo)member);

            return null;
        }
    }
}
