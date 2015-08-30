using System.Reflection;

namespace Sexy.Emit.Reflection
{
    public class ReflectionField : IEmitField
    {
        private readonly FieldInfo field;

        public ReflectionField(FieldInfo field)
        {
            this.field = field;
        }

        public string Name => field.Name;
        public IEmitType FieldType => new ReflectionType(field.FieldType);
    }
}
