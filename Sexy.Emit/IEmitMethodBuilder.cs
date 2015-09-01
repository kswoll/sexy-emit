namespace Sexy.Emit
{
    public interface IEmitMethodBuilder : IEmitMethod
    {
        IEmitIl Il { get; }
    }
}
