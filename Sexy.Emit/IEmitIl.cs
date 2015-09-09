using Sexy.Emit.OpCodes;
using Sexy.Emit.Utils;

namespace Sexy.Emit
{
    public interface IEmitIl
    {
        IEmitLocal DeclareLocal(IEmitType type);
        IEmitLabel DefineLabel();
        void MarkLabel(IEmitLabel label);
        
        /// <summary>
        /// This overload only exists to ease tooling so that it provides a drop down of all possible op codes.  It 
        /// should NEVER be called.
        /// </summary>
        /// <param name="instruction">The instruction you are trying to pick out</param>
        /// <param name="impossible">A value you should never provide (no nulls, no calling!)</param>
        void Emit(EmitOpCode instruction, Impossible impossible);

        void Emit(IEmitOpCodeVoid instruction);
        void Emit(IEmitOpCodeType instruction, IEmitType type);
        void Emit(IEmitOpCodeMethod instruction, IEmitMethod method);
        void Emit(IEmitOpCodeField instruction, IEmitField field);
        void Emit(IEmitOpCodeInt instruction, int operand);
        void Emit(IEmitOpCodeShort instruction, short operand);
        void Emit(IEmitOpCodeByte instruction, byte operand);
        void Emit(IEmitOpCodeShort instruction, sbyte operand);
        void Emit(IEmitOpCodeLong instruction, long operand);
        void Emit(IEmitOpCodeDouble instruction, double operand);
        void Emit(IEmitOpCodeFloat instruction, float operand);
        void Emit(IEmitOpCodeString instruction, string operand);
        void Emit(IEmitOpCodeLocal instruction, IEmitLocal local);
        void Emit(IEmitOpCodeLabel instruction, IEmitLabel label);
        void Emit(IEmitOpCodeLabelArray instruction, IEmitLabel[] labels);
    }
}
