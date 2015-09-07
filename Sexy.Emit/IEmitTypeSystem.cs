using System;

namespace Sexy.Emit
{
    public interface IEmitTypeSystem
    {
        IEmitType GetType(Type type);
    }
}
