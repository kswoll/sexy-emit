namespace Sexy.Emit
{
    public interface IEmitField
    {
        string Name { get; }
        IEmitType FieldType { get; }
    }
}
