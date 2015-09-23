using Sexy.Emit.OpCodes;

namespace Sexy.Emit
{
    public class EmitInstruction
    {
        public IEmitOpCode OpCode { get; }
        public object Operand { get; }

        public EmitInstruction(IEmitOpCode opCode, object operand)
        {
            OpCode = opCode;
            Operand = operand;
        }
    }
}
