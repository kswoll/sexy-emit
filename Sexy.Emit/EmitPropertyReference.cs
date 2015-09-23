using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Sexy.Emit
{
    public class EmitPropertyReference : EmitMemberReference, IReference<EmitProperty>
    {
        private Lazy<EmitProperty> value;

        public EmitPropertyReference(Func<EmitProperty> value)
        {
            this.value = new Lazy<EmitProperty>(value);
        }

        protected override EmitMember GetValue() => Value;
        public EmitProperty Value => value.Value;

        private static readonly ConcurrentDictionary<PropertyInfo, EmitPropertyReference> cache = new ConcurrentDictionary<PropertyInfo, EmitPropertyReference>();

        public static implicit operator EmitPropertyReference(PropertyInfo property)
        {
            return cache.GetOrAdd(property, new EmitPropertyReference(() => new EmitProperty(property.DeclaringType, property.Name, 
                property.PropertyType, result => property.GetIndexParameters().Select(x => (EmitParameter)x).ToArray(), 
                property.GetMethod, property.SetMethod)));
        }

        public static implicit operator EmitProperty(EmitPropertyReference reference)
        {
            return reference.Value;
        }
    }
}
