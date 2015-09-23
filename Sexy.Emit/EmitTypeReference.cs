using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using Sexy.Emit.Utils;

namespace Sexy.Emit
{
    public class EmitTypeReference : EmitMemberReference, IReference<EmitType>
    {
        private readonly Lazy<EmitType> value;

        public EmitTypeReference(Func<EmitType> value)
        {
            this.value = new Lazy<EmitType>(value);
        }

        protected override EmitMember GetValue() => Value;
        public EmitType Value => value.Value;

        private static readonly ConcurrentDictionary<Type, EmitTypeReference> cache = new ConcurrentDictionary<Type, EmitTypeReference>();

        public static implicit operator EmitTypeReference(Type type)
        {
            if (type == null)
                return null;

            return cache.GetOrAdd(type, _ => new EmitTypeReference(() => new EmitType(type.Assembly, type.Namespace, 
                type.Name, type.ToTypeKind(), type.DeclaringType, 
                result => type.GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static).Select(x => (EmitMemberReference)x).ToList(), 
                type.ToVisibility(), type.BaseType, result => type.GetImplementedInterfaces().Select(x => (EmitTypeReference)x).ToArray(), type.IsAbstract, type.IsSealed)));
        }

        public static implicit operator EmitType(EmitTypeReference reference)
        {
            return reference?.Value;
        }

        public static implicit operator EmitTypeReference(EmitType reference)
        {
            return new EmitTypeReference(() => reference);
        }
    }
}
