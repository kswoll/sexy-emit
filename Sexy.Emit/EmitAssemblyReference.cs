using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Sexy.Emit
{
    public class EmitAssemblyReference : IReference<EmitAssembly>
    {
        private readonly Lazy<EmitAssembly> assembly;

        public EmitAssemblyReference(Func<EmitAssembly> assembly)
        {
            this.assembly = new Lazy<EmitAssembly>(assembly);
        }

        public EmitAssembly Value => assembly.Value;

        public static implicit operator EmitAssembly(EmitAssemblyReference reference)
        {
            return reference.assembly.Value;
        }

        private static readonly ConcurrentDictionary<Assembly, EmitAssemblyReference> cache = new ConcurrentDictionary<Assembly, EmitAssemblyReference>();

        public static implicit operator EmitAssemblyReference(Assembly assembly)
        {
            return cache.GetOrAdd(assembly, _ => new EmitAssemblyReference(() => new EmitAssembly(assembly.FullName, result => assembly.GetTypes().Select(x => (EmitTypeReference)x).ToList())));
        }
    }
}
