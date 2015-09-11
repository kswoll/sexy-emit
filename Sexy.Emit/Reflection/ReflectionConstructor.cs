using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sexy.Emit.Reflection
{
    public class ReflectionConstructor : ReflectionMember, IEmitConstructor
    {
        public ConstructorInfo Constructor { get; }

        public ReflectionConstructor(ConstructorInfo constructor)
        {
            if (constructor == null)
                throw new ArgumentNullException(nameof(constructor));

            Constructor = constructor;
        }

        public IEnumerable<IEmitParameter> Parameters => Constructor.GetParameters().Select(x => new ReflectionParameter(x));
        public IEmitType DeclaringType => new ReflectionType(Constructor.DeclaringType);
        public bool IsStatic => Constructor.IsStatic;
    }
}
