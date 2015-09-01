using System.Collections.Generic;

namespace Sexy.Emit
{
    public interface IEmitMethod : IEmitMember
    {
        string Name { get; }
        IEmitType ReturnType { get; }
        IEnumerable<IEmitParameter> Parameters { get; }
    }
}
