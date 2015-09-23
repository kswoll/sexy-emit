using System;
using System.Reflection;

namespace Sexy.Emit
{
    public abstract class EmitMember
    {
        public EmitType DeclaringType { get; }
        public string Name { get; }

        protected EmitMember(EmitType declaringType, string name)
        {
            DeclaringType = declaringType;
            Name = name;
        }

        public abstract TOutput Accept<TInput, TOutput>(IEmitSchemaVisitor<TInput, TOutput> visitor, TInput input = default(TInput));

        public static implicit operator EmitMember(MemberInfo member)
        {
            if (member is Type)
                return (EmitType)(Type)member;
            if (member is FieldInfo)
                return (EmitField)(FieldInfo)member;
            if (member is MethodInfo)
                return (EmitMethod)(MethodInfo)member;
            if (member is PropertyInfo)
                return (EmitProperty)(PropertyInfo)member;
            throw new Exception();
        }
    }
}
