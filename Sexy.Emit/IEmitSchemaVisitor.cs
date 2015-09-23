namespace Sexy.Emit
{
    public interface IEmitSchemaVisitor<in TInput, out TOutput>
    {
        TOutput VisitType(EmitType type, TInput input);
        TOutput VisitField(EmitField field, TInput input);
        TOutput VisitConstructor(EmitConstructor constructor, TInput input);
        TOutput VisitMethod(EmitMethod method, TInput input);
        TOutput VisitProperty(EmitProperty property, TInput input);
    }
}
