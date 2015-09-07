using System.Reflection.Emit;

namespace Sexy.Emit.Reflection
{
    public class ReflectionLocal : EmitLocal
    {
        public LocalBuilder LocalBuilder { get; }

        public ReflectionLocal(LocalBuilder localBuilder)
        {
            LocalBuilder = localBuilder;
        }
    }
}
