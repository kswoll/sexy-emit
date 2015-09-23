namespace Sexy.Emit
{
    public class EmitFieldBuilder : EmitField
    {
        public EmitFieldBuilder(EmitType declaringType, string name, EmitType fieldType, EmitVisibility visibility, bool isStatic, bool isReadOnly) : base(declaringType, name, fieldType, visibility, isStatic, isReadOnly)
        {
        }
    }
}
