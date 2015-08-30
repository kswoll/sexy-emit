using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sexy.Emit.Reflection
{
    public class ReflectionAssembly : IEmitAssembly
    {
        private Assembly assembly;

        public ReflectionAssembly(Assembly assembly)
        {
            this.assembly = assembly;
        }

        public IEnumerable<IEmitType> Types
        {
            get { return assembly.GetTypes().Select(x => new ReflectionType(x)); }
        }
    }
}
