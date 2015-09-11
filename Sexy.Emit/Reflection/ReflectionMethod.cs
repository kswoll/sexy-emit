using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sexy.Emit.Reflection
{
    public class ReflectionMethod : ReflectionMember, IEmitMethod
    {
        private readonly MethodInfo method;

        public ReflectionMethod(MethodInfo method)
        {
            this.method = method;
        }

        public MethodInfo Method => method;
        public string Name => method.Name;
        public IEmitType ReturnType => new ReflectionType(method.ReturnType);
        public IEnumerable<IEmitParameter> Parameters => method.GetParameters().Select(x => new ReflectionParameter(x));
        public IEmitType DeclaringType => new ReflectionType(method.DeclaringType);
        public bool IsStatic => method.IsStatic;
        public bool IsVirtual => method.IsVirtual;
    }
}
