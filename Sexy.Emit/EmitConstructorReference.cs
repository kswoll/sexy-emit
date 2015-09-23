using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Sexy.Emit
{
    public class EmitConstructorReference : EmitMemberReference, IReference<EmitConstructor>
    {
        private readonly Lazy<EmitConstructor> value;

        public EmitConstructorReference(Func<EmitConstructor> value)
        {
            this.value = new Lazy<EmitConstructor>(value);
        }

        protected override EmitMember GetValue() => Value;
        public EmitConstructor Value => value.Value;

        private static readonly ConcurrentDictionary<ConstructorInfo, EmitConstructorReference> cache = new ConcurrentDictionary<ConstructorInfo, EmitConstructorReference>();

        public static implicit operator EmitConstructorReference(ConstructorInfo constructor)
        {
            return cache.GetOrAdd(constructor, _ => new EmitConstructorReference(() => new EmitConstructor(constructor.DeclaringType, result => constructor.GetParameters().Select(x => (EmitParameter)x).ToList(), constructor.ToVisibility(), constructor.IsStatic)));
        }

        public static implicit operator EmitConstructor(EmitConstructorReference reference)
        {
            return reference.Value;
        }
    }
}
