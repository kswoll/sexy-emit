using System;

namespace Sexy.Emit
{
    public interface IEmitTypeSystem
    {
        EmitType GetType(Type type);
    }
}
