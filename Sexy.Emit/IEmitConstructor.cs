using System.Collections.Generic;

namespace Sexy.Emit
{
    public interface IEmitConstructor : IEmitMember
    {
        IEnumerable<IEmitParameter> Parameters { get; }
        IEmitType DeclaringType { get; }
        bool IsStatic { get; }
    }
}
