using System.Collections.Generic;

namespace Sexy.Emit
{
    public interface IEmitMethodOrConstructorBuilder
    {
        EmitType ReturnType { get; }
        IReadOnlyList<EmitParameter> Parameters { get; }
    }
}
