using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Sexy.Emit.Reflection
{
    public static class ReflectionOpCode
    {
        private static readonly Dictionary<EmitOpCode, OpCode> emitOpCodeToOpCode = typeof(EmitOpCodes)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .ToDictionary(x => (EmitOpCode)x.GetValue(null), x => (OpCode)typeof(OpCodes).GetField(x.Name).GetValue(null));
        private static readonly Dictionary<OpCode, EmitOpCode> opCodeToEmitOpCode = emitOpCodeToOpCode
            .ToDictionary(x => x.Value, x => x.Key);

        public static OpCode ToOpCode(this EmitOpCode instruction)
        {
            return emitOpCodeToOpCode[instruction];
        }

        public static EmitOpCode ToOpCode(this OpCode instruction)
        {
            return opCodeToEmitOpCode[instruction];
        }
    }
}
