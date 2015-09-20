using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sexy.Emit.Reflection
{
    public class ReflectionType : IEmitType
    {
        public Type Type { get; }

        public ReflectionType(Type type)
        {
            Type = type;
        }

        public string Namespace => Type.Namespace;
        public string Name => Type.Name;
        public IEmitType DeclaringType => new ReflectionType(Type.DeclaringType);

        public IEnumerable<IEmitMember> Members
        {
            get
            {
                return Type
                    .GetMembers(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
                    .Select(x => ReflectionMember.Create(x));
            }
        }

        public bool IsValueType => Type.IsValueType;
        public bool IsInterface => Type.IsInterface;
        public IEmitType BaseType => new ReflectionType(Type.BaseType);
        public string FullName => Type.FullName;

        public override string ToString()
        {
            return $"{Namespace}.{Name}";
        }

        protected bool Equals(ReflectionType other)
        {
            return Equals(Type, other.Type);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj is IEmitType) return ((IEmitType)obj).FullName == FullName;
            if (obj.GetType() != GetType()) return false;
            return Equals((ReflectionType)obj);
        }

        public override int GetHashCode()
        {
            return Type.FullName.GetHashCode();
        }

        public IEmitType MakeGenericType(params IEmitType[] typeArguments)
        {
            return new ReflectionType(Type.MakeGenericType(typeArguments.Select(x => ((ReflectionType)x).Type).ToArray()));
        }

        public IEmitType MakeArrayType(int rank)
        {
            return new ReflectionType(Type.MakeArrayType(rank));
        }
    }
}
