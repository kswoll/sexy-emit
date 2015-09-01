using System.Collections.Generic;

namespace Sexy.Emit
{
    public interface IEmitAssemblyBuilder : IEmitAssembly
    {
        IEnumerable<IEmitTypeBuilder> TypeBuilders { get; }
        IEmitTypeBuilder DefineType(string name, EmitTypeKind kind = EmitTypeKind.Class, EmitVisibility visibility = EmitVisibility.Public, bool isAbstract = false,
            bool isSealed = false);
    }
}
