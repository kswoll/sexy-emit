using Sexy.Emit.OpCodes;

namespace Sexy.Emit.Reflection
{
    public class ReflectionInstruction : IEmitInstruction
    {
        public IEmitOpCode OpCode { get; }
        public object Operand { get; }

        public ReflectionInstruction(IEmitOpCode opCode, object operand)
        {
            OpCode = opCode;
            Operand = operand;
        }
    }
}
