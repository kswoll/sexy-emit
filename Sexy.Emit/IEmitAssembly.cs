using System.Collections.Generic;

namespace Sexy.Emit
{
    public interface IEmitAssembly
    {
        IEnumerable<IEmitType> Types { get; }
    }
}
