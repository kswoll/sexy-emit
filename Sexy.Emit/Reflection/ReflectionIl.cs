using System.Reflection.Emit;

namespace Sexy.Emit.Reflection
{
    public class ReflectionIl : IEmitIl
    {
        private readonly ILGenerator il;

        public ReflectionIl(ILGenerator il)
        {
            this.il = il;
        }

        public IEmitVariable DeclareLocal(IEmitType type)
        {
            return new ReflectionVariable(il.DeclareLocal(((ReflectionType)type).Type));
        }

        public IEmitLabel DefineLabel()
        {
            return new ReflectionLabel(il.DefineLabel());
        }

        public void MarkLabel(IEmitLabel label)
        {
            il.MarkLabel(((ReflectionLabel)label).Label);
        }

        public void Emit(EmitOpCode instruction)
        {
            il.Emit(instruction.ToOpCode());
        }

        public void Emit(EmitOpCode instruction, IEmitType type)
        {
            il.Emit(instruction.ToOpCode(), ((ReflectionType)type).Type);
        }

        public void Emit(EmitOpCode instruction, IEmitMethod method)
        {
            il.Emit(instruction.ToOpCode(), ((ReflectionMethod)method).Method);
        }

        public void Emit(EmitOpCode instruction, IEmitField field)
        {
            il.Emit(instruction.ToOpCode(), ((ReflectionField)field).Field);
        }

        public void Emit(EmitOpCode instruction, int operand)
        {
            il.Emit(instruction.ToOpCode(), operand);
        }

        public void Emit(EmitOpCode instruction, short operand)
        {
            il.Emit(instruction.ToOpCode(), operand);
        }

        public void Emit(EmitOpCode instruction, byte operand)
        {
            il.Emit(instruction.ToOpCode(), operand);
        }

        public void Emit(EmitOpCode instruction, sbyte operand)
        {
            il.Emit(instruction.ToOpCode(), operand);
        }

        public void Emit(EmitOpCode instruction, long operand)
        {
            il.Emit(instruction.ToOpCode(), operand);
        }

        public void Emit(EmitOpCode instruction, double operand)
        {
            il.Emit(instruction.ToOpCode(), operand);
        }

        public void Emit(EmitOpCode instruction, float operand)
        {
            il.Emit(instruction.ToOpCode(), operand);
        }

        public void Emit(EmitOpCode instruction, string operand)
        {
            il.Emit(instruction.ToOpCode(), operand);
        }

        public void Emit(EmitOpCode instruction, IEmitVariable variable)
        {
            il.Emit(instruction.ToOpCode(), ((ReflectionVariable)variable).LocalBuilder);
        }

        public void Emit(EmitOpCode instruction, IEmitLabel label)
        {
            il.Emit(instruction.ToOpCode(), ((ReflectionLabel)label).Label);
        }
    }
}
