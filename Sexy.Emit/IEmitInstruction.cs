using Sexy.Emit.OpCodes;

namespace Sexy.Emit
{
    public interface IEmitInstruction
    {
        IEmitOpCode OpCode { get; }
        object Operand { get; }
    }
}
