namespace Sexy.Emit
{
    public interface IEmitIl
    {
        void Emit(EmitOpCode instruction);
        void Emit(EmitOpCode instruction, IEmitType type);
        void Emit(EmitOpCode instruction, IEmitMethod method);
        void Emit(EmitOpCode instruction, IEmitField field);
        void Emit(EmitOpCode instruction, int operand);
        void Emit(EmitOpCode instruction, short operand);
        void Emit(EmitOpCode instruction, byte operand);
        void Emit(EmitOpCode instruction, sbyte operand);
        void Emit(EmitOpCode instruction, long operand);
        void Emit(EmitOpCode instruction, double operand);
        void Emit(EmitOpCode instruction, float operand);
        void Emit(EmitOpCode instruction, string operand);
    }
}
