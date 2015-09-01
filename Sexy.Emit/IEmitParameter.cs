namespace Sexy.Emit
{
    public interface IEmitParameter
    {
        string Name { get; }
        IEmitType ParameterType { get; }
    }
}
