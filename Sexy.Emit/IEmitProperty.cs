namespace Sexy.Emit
{
    public interface IEmitProperty : IEmitMember
    {
        string Name { get; }
        IEmitType PropertyType { get; }
        IEmitMethod GetMethod { get; }
        IEmitMethod SetMethod { get; }
    }
}
