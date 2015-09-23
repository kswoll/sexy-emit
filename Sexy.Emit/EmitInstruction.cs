using System.Collections.Generic;
using System.Linq;
using Sexy.Emit.OpCodes;

namespace Sexy.Emit
{
    public class EmitInstruction
    {
        public IEmitOpCode OpCode { get; }
        public object Operand { get; }
        public IEnumerable<EmitLabel> Labels => labels ?? Enumerable.Empty<EmitLabel>();

        private List<EmitLabel> labels;

        public EmitInstruction(IEmitOpCode opCode, object operand)
        {
            OpCode = opCode;
            Operand = operand;
        }

        internal void AddLabel(EmitLabel label)
        {
            if (labels == null)
                labels = new List<EmitLabel>();
            labels.Add(label);
        }
    }
}
