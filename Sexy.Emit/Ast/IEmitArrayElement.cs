namespace Sexy.Emit.Ast
{
    public interface IEmitArrayElement
    {
        int Length { get; }
        IEmitArrayElement this[int index] { get; }
    }
}
