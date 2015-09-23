using System.Reflection;

namespace Sexy.Emit
{
    public class EmitField : EmitMember
    {
        public EmitType FieldType { get; }
        public EmitVisibility Visibility { get; }
        public bool IsStatic { get; }
        public bool IsReadOnly { get; }

        public EmitField(EmitType declaringType, string name, EmitType fieldType, EmitVisibility visibility, bool isStatic, bool isReadOnly) : base(declaringType, name)
        {
            FieldType = fieldType;
            Visibility = visibility;
            IsStatic = isStatic;
            IsReadOnly = isReadOnly;
        }

        public override TOutput Accept<TInput, TOutput>(IEmitSchemaVisitor<TInput, TOutput> visitor, TInput input)
        {
            return visitor.VisitField(this, input);
        }

        public static implicit operator EmitField(FieldInfo field)
        {
            return (EmitFieldReference)field;
        }
    }
}
