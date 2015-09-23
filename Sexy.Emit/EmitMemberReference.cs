using System;
using System.Reflection;

namespace Sexy.Emit
{
    public abstract class EmitMemberReference : IReference<EmitMember>
    {
        EmitMember IReference<EmitMember>.Value => GetValue();

        protected abstract EmitMember GetValue();

        public static implicit operator EmitMemberReference(MemberInfo member)
        {
            if (member is Type)
                return (EmitTypeReference)(Type)member;
            if (member is FieldInfo)
                return (EmitFieldReference)(FieldInfo)member;
            if (member is MethodInfo)
                return (EmitMethodReference)(MethodInfo)member;
            if (member is PropertyInfo)
                return (EmitPropertyReference)(PropertyInfo)member;
            if (member is ConstructorInfo)
                return (EmitConstructorReference)(ConstructorInfo)member;
            throw new Exception("Unexpected member type: " + member.GetType().FullName);
        }
    }
}
