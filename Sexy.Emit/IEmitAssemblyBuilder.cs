using System.Collections.Generic;

namespace Sexy.Emit
{
    public interface IEmitAssemblyBuilder : IEmitAssembly
    {
        IEnumerable<IEmitTypeBuilder> TypeBuilders { get; }
        IEmitTypeBuilder DefineType(string name, EmitTypeAttributes typeAttributes = 0);
    }
}
