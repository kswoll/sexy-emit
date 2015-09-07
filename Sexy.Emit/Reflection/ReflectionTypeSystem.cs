using System;

namespace Sexy.Emit.Reflection
{
    public class ReflectionTypeSystem : IEmitTypeSystem
    {
        public IEmitType GetType(Type type)
        {
            return new ReflectionType(type);
        }
    }
}
