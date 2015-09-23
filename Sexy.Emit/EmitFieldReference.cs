using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Sexy.Emit
{
    public class EmitFieldReference : EmitMemberReference, IReference<EmitField>
    {
        private readonly Lazy<EmitField> value;

        public EmitFieldReference(Func<EmitField> value)
        {
            this.value = new Lazy<EmitField>(value);
        }

        public EmitField Value => value.Value;
        protected override EmitMember GetValue() => Value;

        private static readonly ConcurrentDictionary<FieldInfo, EmitFieldReference> cache = new ConcurrentDictionary<FieldInfo, EmitFieldReference>();

        public static implicit operator EmitFieldReference(FieldInfo field)
        {
            return cache.GetOrAdd(field, _ => new EmitFieldReference(() => new EmitField(field.DeclaringType, field.Name, field.FieldType, field.ToVisibility(), field.IsStatic, field.IsInitOnly)));
        }

        public static implicit operator EmitField(EmitFieldReference reference)
        {
            return reference.Value;
        }
    }
}
