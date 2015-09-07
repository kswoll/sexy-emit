using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Sexy.Emit.Reflection
{
    public static class ReflectionOpCode
    {
        private static readonly Dictionary<IEmitOpCode, OpCode> emitOpCodeToOpCode = typeof(EmitOpCodes)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .ToDictionary(x => (IEmitOpCode)x.GetValue(null), x => (OpCode)typeof(OpCodes).GetField(x.Name).GetValue(null));
        private static readonly Dictionary<OpCode, IEmitOpCode> opCodeToEmitOpCode = emitOpCodeToOpCode
            .ToDictionary(x => x.Value, x => x.Key);

        public static OpCode ToOpCode(this IEmitOpCode instruction)
        {
            return emitOpCodeToOpCode[instruction];
        }

        public static IEmitOpCode ToOpCode(this OpCode instruction)
        {
            return opCodeToEmitOpCode[instruction];
        }
    }
}
