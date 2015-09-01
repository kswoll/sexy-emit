using System.Reflection.Emit;

namespace Sexy.Emit.Reflection
{
    public class ReflectionFieldBuilder : ReflectionField, IEmitFieldBuilder
    {
        private FieldBuilder fieldBuilder;

        public ReflectionFieldBuilder(FieldBuilder fieldBuilder) : base(fieldBuilder)
        {
            this.fieldBuilder = fieldBuilder;
        }
    }
}
