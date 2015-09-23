using System.Collections.Generic;

namespace Sexy.Emit
{
    public interface IEmitProvider
    {
        IEnumerable<EmitAssembly> Assemblies { get; }
    }
}
