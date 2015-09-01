using System.Reflection.Emit;

namespace Sexy.Emit.Reflection
{
    public class ReflectionVariable : IEmitVariable
    {
        public LocalBuilder LocalBuilder { get; }

        public ReflectionVariable(LocalBuilder localBuilder)
        {
            LocalBuilder = localBuilder;
        }
    }
}
