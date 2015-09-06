namespace Sexy.Emit
{
    public interface IEmitIl
    {
        IEmitVariable DeclareLocal(IEmitType type);
        IEmitLabel DefineLabel();
        void MarkLabel(IEmitLabel label);
        void Emit(EmitOpCode instruction);
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
        void Emit(IEmitOpCodeVariable instruction, IEmitVariable variable);
        void Emit(IEmitOpCodeLabel instruction, IEmitLabel label);
        void Emit(IEmitOpCodeLabelArray instruction, IEmitLabel[] labels);
    }
}
