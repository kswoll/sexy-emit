namespace Sexy.Emit
{
    public interface IEmitTypeBuilder : IEmitType
    {
        IEmitFieldBuilder DefineField(string name, IEmitType type);
    }
}
