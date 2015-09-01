using System.Reflection;

namespace Sexy.Emit.Reflection
{
    public class ReflectionField : ReflectionMember, IEmitField
    {
        private readonly FieldInfo field;

        public ReflectionField(FieldInfo field)
        {
            this.field = field;
        }

        public FieldInfo Field => field;
        public string Name => field.Name;
        public IEmitType FieldType => new ReflectionType(field.FieldType);
    }
}
