using System.Reflection.Emit;

namespace Sexy.Emit.Reflection
{
    public class ReflectionConstructorBuilder : ReflectionConstructor, IEmitConstructorBuilder
    {
        public ReflectionConstructorBuilder(ConstructorBuilder constructor) : base(constructor)
        {
        }
    }
}
