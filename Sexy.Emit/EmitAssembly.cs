using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sexy.Emit.Utils;

namespace Sexy.Emit
{
    public class EmitAssembly
    {
        public string Name { get; }
        public IReadOnlyList<EmitType> Types { get; }

        public EmitAssembly(string name, Func<EmitAssembly, IReadOnlyList<EmitTypeReference>> typesFactory) 
        {
            Name = name;
            Types = new ReadOnlyListWrapper<EmitTypeReference, EmitType>(typesFactory(this));
        }
        
        public static implicit operator EmitAssembly(Assembly assembly)
        {
            return (EmitAssemblyReference)assembly;
        }

        public static implicit operator Assembly(EmitAssembly assembly)
        {
            return AppDomain.CurrentDomain.GetAssemblies().Single(x => x.FullName == assembly.Name);
        }
    }
}
