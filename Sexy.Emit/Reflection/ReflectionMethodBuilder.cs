using System.Reflection.Emit;

namespace Sexy.Emit.Reflection
{
    public class ReflectionMethodBuilder : ReflectionMethod, IEmitMethodBuilder
    {
        private MethodBuilder methodBuilder;
        private ReflectionIl il;

        public ReflectionMethodBuilder(MethodBuilder methodBuilder) : base(methodBuilder)
        {
            this.methodBuilder = methodBuilder;
            il = new ReflectionIl(methodBuilder.GetILGenerator());
        }

        public IEmitIl Il => il;
    }
}
