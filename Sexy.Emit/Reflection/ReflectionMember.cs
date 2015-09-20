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
            if (member is ConstructorBuilder)
                return new ReflectionConstructorBuilder((ConstructorBuilder)member);
            if (member is ConstructorInfo)
                return new ReflectionConstructor((ConstructorInfo)member);
            if (member is PropertyInfo)
                return new ReflectionProperty((PropertyInfo)member);

            return null;
        }
    }
}
