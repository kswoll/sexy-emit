using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Sexy.Emit
{
    public class EmitMethodReference : EmitMemberReference, IReference<EmitMethod>
    {
        private readonly Lazy<EmitMethod> value;

        public EmitMethodReference(Func<EmitMethod> value)
        {
            this.value = new Lazy<EmitMethod>(value);
        }

        public EmitMethod Value => value.Value;
        protected override EmitMember GetValue() => Value;

        private static readonly ConcurrentDictionary<MethodInfo, EmitMethodReference> cache = new ConcurrentDictionary<MethodInfo, EmitMethodReference>();

        public static implicit operator EmitMethodReference(MethodInfo method)
        {
            return cache.GetOrAdd(method, _ => new EmitMethodReference(() => new EmitMethod(method.DeclaringType, method.Name, method.ReturnType, result => method.GetParameters().Select(x => (EmitParameter)x).ToList(), method.ToVisibility(), method.IsStatic, method.IsFinal, method.IsVirtual, method.IsAbstract)));
        }

        public static implicit operator EmitMethod(EmitMethodReference reference)
        {
            return reference.Value;
        }
    }
}
